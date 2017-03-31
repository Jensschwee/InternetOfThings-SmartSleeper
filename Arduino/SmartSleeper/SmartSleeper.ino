#include <Wire.h>
#include <Arduino.h>

#include <HTS221.h>
#include <LPS25H.h>
#include <VL6180.h>


void setup() {
  Wire.begin();
  pinMode(PIN_LED_13, OUTPUT);
  smePressure.begin();
  smeAmbient.begin();
  smeHumidity.begin();
  SerialUSB.begin(115200);
}

void loop() {
  
}

/**
 * Read the temperature sensor, returns a float
 */
float readTemperature() {
  return smeHumidity.readTemperature();
}

/**
 * Transform temperature measurements:
 * Input datarange: -163.84 - 163.83
 * Output datarange: 0 - 32766
 * Size: 15 bits
 * 
 * TODO: Put some hardcoded values in '#define's
 */
int transformTemperature(float temperature) {
  temperature += 163.84f;
  temperature *= 100;
  int transformedTemp = (int) temperature;
  transformedTemp = ensureDatarange(0, 32766, transformedTemp)
  return transformedTemp;
}

/**
 * Read the humidity sensor, returns a float
 * Datarange: 0.00 - 163.83
 * Size: 14 bits
 */
float readHumidity() {
  return smeHumidity.readHumidity();
}

/**
 * Transform humidity measurements:
 * Input datarange: 0.00 - 163.83
 * Output datarange: 0 - 16383
 * Size: 14 bits
 * 
 * TODO: Put some hardcoded values in '#define's
 */
int transformHumidity(float humidity) {
  humidity *= 100;
  int transformedHumidity = (int) humidity;
  transformedHumidity = ensureDatarange(0, 16383, transformedHumidity)
  return transformedHumidity;
}

/**
 * Read the Barometer sensor returns a int
 * This sensor have 11 bits to take in the datastrame.
 * Datarange of 0-2047
 */
int readBarometer() {
  return smePressure.readPressure();
}

/**
 * Read the Lux sensor returns a float, in other to get the lux value you need to / 100000
 * This sensor have 20 bits to take in the datastrame.
 * Datarange of 0.0-104652.7
 */
int readLux(){
  return smeAmbient.ligthPollingRead()*10;
}

/**
 *  prints val with number of decimal places determine by precision
 *  NOTE: precision is 1 followed by the number of zeros for the desired number of decimal places
 *  example: printDouble( 3.1415, 100); // prints 3.14 (two decimal places)
 */
void printDouble( double val, unsigned int precision){
    SerialUSB.print (int(val));  //prints the int part
    SerialUSB.print("."); // print the decimal point
    unsigned int frac;
    if(val >= 0)
        frac = (val - int(val)) * precision;
    else
        frac = (int(val)- val ) * precision;
    int frac1 = frac;

    while( frac1 /= 10 )
        precision /= 10;
    precision /= 10;

    while(  precision /= 10)
        SerialUSB.print("0");

    SerialUSB.println(frac,DEC) ;
}


/**
 * Ensure that data is within a certain range.
 * Transform the data to an int before using this if you have a float.
 * 
 * min: lower bound
 * max: higher bound
 * value: surprise, this is the value to check! :D
 */
int ensureDatarange(int min, int max, int value) {
  if(value < min) {
    value = min;
  }
  else if(value > max) {
    value = max;
  }
  return value;
}

