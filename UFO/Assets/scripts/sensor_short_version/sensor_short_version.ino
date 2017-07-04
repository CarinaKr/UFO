#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_ADXL345_U.h>



Adafruit_ADXL345_Unified accel = Adafruit_ADXL345_Unified(12345);

void setup()
{
  Serial.println("Sensor Test");
  Serial.begin(9600);
  accel.begin();
}

void loop()
{
  sensors_event_t event; 
  accel.getEvent(&event);
 
  Serial.flush();
  /* Display the results (acceleration is measured in m/s^2) */
  //Serial.print("X: "); Serial.print(event.acceleration.x); Serial.print("  ");
  //Serial.print("Y: "); Serial.print(event.acceleration.y); Serial.print("  ");
  //Serial.print("Z: "); Serial.print(event.acceleration.z); Serial.print("  ");Serial.println("m/s^2 ");
  Serial.print(event.acceleration.x);Serial.print(";");
  Serial.print(event.acceleration.y);Serial.print(";");
  Serial.print(event.acceleration.z);Serial.print(";");Serial.println();
  delay(250);
}
