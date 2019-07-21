const int ledPin = 13;

void setup() 
{  
  pinMode(ledPin, OUTPUT);
  Serial.begin(9600);
}

void loop() 
{
  if(Serial.available())
  {
      char data = Serial.read();
      Serial.print(data);
      
      switch(data)
      {
        case '0':
          digitalWrite(ledPin, LOW);
          break;

        case '1':
          digitalWrite(ledPin, HIGH);
          break;
      }
  }
}
