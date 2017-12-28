#define BaudRate 9600
#define LED1    13
#define LED2    12
#define LED3    11
#define LED4    10

char incomingOption;

void setup()
{
  pinMode(LED1, OUTPUT);
  pinMode(LED2, OUTPUT);
  pinMode(LED3, OUTPUT);
  pinMode(LED4, OUTPUT);
  Serial.begin(BaudRate);
}
void loop()
{
     incomingOption = Serial.read();
     switch(incomingOption){
        case '1':
          alarm();
          break;
     }
}

void alarm(){

  for(int i =1; i <= 5; i++){
    digitalWrite(LED1, HIGH);   
    delay(100);              
    digitalWrite(LED1, LOW);    
    delay(100); 
    
    {digitalWrite(LED2, HIGH);
    delay(100);
    digitalWrite(LED2, LOW);
    delay(100);}
    
    {digitalWrite(LED3, HIGH);
    delay(100);
    digitalWrite(LED3, LOW);
    delay(100);}
    
    {digitalWrite(LED4, HIGH);
    delay(100);
    digitalWrite(LED4, LOW);
    delay(100);}
  }
}

