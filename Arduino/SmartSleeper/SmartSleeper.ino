#include <Wire.h>

#include <LPS25H.h>
#include <VL6180.h>
#include <Arduino.h>

void setup() {
  // put your setup code here, to run once:
  Wire.begin();
  pinMode(PIN_LED_13, OUTPUT);
  smePressure.begin();
  if (!smeAmbient.begin()) {
    while(1){
      ; // endless loop due to error on VL6180 initialization
    }
  }
  SerialUSB.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:
}

/**
 * Read the Barometer sensor returns a int
 * This sensor have 11 bits to take in the datastrame.
 * Datarange of 0-2047
 */
int readBarometer(){
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

