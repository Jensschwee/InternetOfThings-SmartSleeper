#include <HTS221.h>
#include <LPS25H.h>
#include <VL6180.h>

#include <Wire.h>
#include <Arduino.h>

#define SIGFOX_FRAME_LENGTH 12
#define INTERVAL 1000
#define DEBUG 1

struct data {
  boolean patting[4];
  boolean tempBits[15];
  boolean luxBits[20];
  boolean humBits[14];
  boolean psmBits[11];
};

void setup() {
  Wire.begin();
  pinMode(PIN_LED_13, OUTPUT);
  smePressure.begin();
  smeAmbient.begin();
  smeHumidity.begin();
  SerialUSB.begin(19200);

  SigFox.begin(19200);

  //initSigfox();
}

void loop() {
  int temperature = transformTemperature(readTemp());
  int lux =  transformLux(readLux());
  int hum = transformHumidity(readHum());
  int psm = transformPressure(readBarometer());

  data frame;

  bitify(0, 4, frame.patting);
  bitify(temperature, 15, frame.tempBits);
  bitify(lux, 20, frame.luxBits);
  bitify(hum, 14, frame.humBits);
  bitify(psm, 11, frame.psmBits);
  
  SerialUSB.println(getSigfoxFrame(&frame, sizeof(data)));
  SerialUSB.println(sizeof(data));
  //bool answer = sendSigfox(&frame, sizeof(data));

  //SerialUSB.println(temperature);
  //SerialUSB.println(frame.tempBits);
  //SerialUSB.println(hum);
  //SerialUSB.println(psm);

  delay(INTERVAL);
}

/**
   Read the temperature sensor, returns a float
*/
float readTemp() {
  return smeHumidity.readTemperature();
}

/**
   Transform temperature measurements:
   Input datarange: -163.84 - 163.83
   Output datarange: 0 - 32766
   Size: 15 bits

   TODO: Put some hardcoded values in '#define's
*/
int transformTemperature(float temperature) {
  temperature += 163.84f;
  temperature *= 100;
  int transformedTemp = (int) temperature;
  transformedTemp = ensureDatarange(0, 32766, transformedTemp);
  return transformedTemp;
}

/**
   Read the humidity sensor, returns a float
   Datarange: 0.00 - 163.83
   Size: 14 bits
*/
float readHum() {
  return smeHumidity.readHumidity();
}

/**
   Transform humidity measurements:
   Input datarange: 0.00 - 163.83
   Output datarange: 0 - 16383
   Size: 14 bits

   TODO: Put some hardcoded values in '#define's
*/
int transformHumidity(float humidity) {
  humidity *= 100;
  int transformedHumidity = (int) humidity;
  transformedHumidity = ensureDatarange(0, 16383, transformedHumidity);
  return transformedHumidity;
}

/**
   Read the Barometer sensor returns a int
   This sensor have 11 bits to take in the datastrame.
   Datarange of 0-2047
*/
float readBarometer() {
  return smePressure.readPressure();
}

int transformPressure(float pressure) {
  int transformedPressure = (int) pressure;
  transformedPressure = ensureDatarange(0, 2047, transformedPressure);
  return transformedPressure;
}

/**
   Read the Lux sensor returns a float, in other to get the lux value you need to / 10000
   This sensor have 20 bits to take in the datastrame.
   Datarange of 0.00-10465.27
*/
float readLux() {
  return smeAmbient.ligthPollingRead() / 10000.0;
}

/**
   Transform humidity measurements:
   Input datarange: 0.00 - 10465.27
   Output datarange: 0 - 1046527
   Size: 14 bits

   TODO: Put some hardcoded values in '#define's
*/
int transformLux(float lux) {
  lux *= 100;
  int transformedLux = (int) lux;
  transformedLux = ensureDatarange(0, 1046527, transformedLux);
  return transformedLux;
}

/**
    prints val with number of decimal places determine by precision
    NOTE: precision is 1 followed by the number of zeros for the desired number of decimal places
    example: printDouble( 3.1415, 100); // prints 3.14 (two decimal places)
*/
void printDouble(double val, unsigned int precision) {
  //SerialUSB.print (int(val));  //prints the int part
  //SerialUSB.print("."); // print the decimal point
  unsigned int frac;
  if (val >= 0)
    frac = (val - int(val)) * precision;
  else
    frac = (int(val) - val ) * precision;
  int frac1 = frac;

  while ( frac1 /= 10 )
    precision /= 10;
  precision /= 10;

  while (  precision /= 10) {
    //SerialUSB.print("0");
  }

  //SerialUSB.println(frac, DEC) ;
}

/**
   used to bitify an int.
   input is the int to bitify
   the amont of bits to bitify
   averagingBuffer boolean array to save the result in
*/
void bitify(unsigned int input, unsigned int bitSize, boolean *averagingBuffer) {
  //SerialUSB.print(bitSize);
  for (int i = bitSize - 1; i >= 0; i--) {
    if (input >= pow(2, i))
    {
      averagingBuffer[i] = true;
      input -= pow(2, i);
    }
    else {
      averagingBuffer[i] = false;
    }
  }
}


/**
   Ensure that data is within a certain range.
   Transform the data to an int before using this if you have a float.

   min: lower bound
   max: higher bound
   value: surprise, this is the value to check! :D
*/
int ensureDatarange(int min, int max, int value) {
  if (value < min) {
    value = min;
  }
  else if (value > max) {
    value = max;
  }
  return value;
}



void initSigfox() {
  SigFox.print("+++");
  while (!SigFox.available()) {
    delay(100);
  }
  while (SigFox.available()) {
    byte serialByte = SigFox.read();
    if (DEBUG) {
      SerialUSB.print(serialByte);
    }
  }
  if (DEBUG) {
    SerialUSB.println("\n ** Setup OK **");
  }
}

String getSigfoxFrame(const void* data, uint8_t len) {
  String frame = "";
  bool* bytes = (bool*)data;
  /*if (len < SIGFOX_FRAME_LENGTH) {
    //fill with zeros
    uint8_t i = SIGFOX_FRAME_LENGTH;
    while (i-- > len) {
      frame += "00";
    }
  }*/

  
  //0-1 == 255 --> (0-1) > len
  for (uint8_t i = len - 1; i < len; --i) {
    if (bytes[i] < 16) {
      //frame += "0";
    }
    
    frame += String(bytes[i], HEX);
  }

  const char* binary = frame.c_str();
  char hex[17] = "" ;
  
  uint32_t integer = 0 ;
  
  for( int i = 0; binary[i+1] != '\0'; i++ )
  {
      if( binary[i] == '1' )
      {
          integer |= 1 ;
      }
      integer <<= 1 ;
  }

  sprintf( hex, "0x%06x", integer ) ;
  SerialUSB.println(hex);
  return hex;
}

bool sendSigfox(const void* data, uint8_t len) {
  String frame = getSigfoxFrame(data, len);
  String status = "";
  char output;
  if (DEBUG) {
    SerialUSB.print("AT$SF=");
    SerialUSB.println(frame);
  }
  SigFox.print("AT$SF=");
  SigFox.print(frame);
  SigFox.print("\r");
  while (!SigFox.available());

  while (SigFox.available()) {
    output = (char)SigFox.read();
    status += output;
    delay(10);
  }
  if (DEBUG) {
    SerialUSB.print("Status \t");
    SerialUSB.println(status);
  }
  if (status == "OK\r") {
    //Success :)
    return true;
  }
  else {
    return false;
  }
}



