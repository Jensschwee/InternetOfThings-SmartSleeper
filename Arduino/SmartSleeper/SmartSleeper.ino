#include <Wire.h>

#include <LPS25H.h>
#include <Arduino.h>

void setup() {
  // put your setup code here, to run once:
  Wire.begin();
  pinMode(PIN_LED_13, OUTPUT);
  smePressure.begin();
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

