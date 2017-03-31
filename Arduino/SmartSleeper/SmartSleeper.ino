#include <Wire.h>
#include <Arduino.h>

#include <HTS221.h>


void setup() {
  Wire.begin();
  pinMode(PIN_LED_13, OUTPUT);
  
  smeHumidity.begin();

  SerialUSB.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:
  
}






/**
 * Read the temperatire sensor, returns a float
 * Datarange: -163.84 - 163.83
 * Size: 15 bits
 * 
 * TODO: Ensure range, also transform with +163.84
 * TODO: Ensure size
 */
float readTemperature() {
  float data = smeHumidity.readTemperature();
  return data;
}


/**
 * Read the temperatire sensor, returns a float
 * Datarange: 0.00 - 163.83
 * Size: 14 bits
 * 
 * TODO: Ensure range
 * TODO: Ensure size
 */
float readHumidity() {
  float data = smeHumidity.readHumidity();
  return data;
}
