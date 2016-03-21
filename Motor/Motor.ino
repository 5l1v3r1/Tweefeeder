//Tweefeeder  motor kontrol yazılımı.
// Copyright (C) (2016) (Enis Getmez) E-mail: mentalistler@mit.tc
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software Foundation,
// Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
// Gnomovision version 42, Copyright (C) Enis Getmez
// Gnomovision comes with ABSOLUTELY NO WARRANTY; for details type `show w.
// This is free software, and you are welcome to redistribute it
// under certain conditions; type `show c' for details.

const int motor1_ileri = 6; // motor1 => kapağı açma motoru.
const int motor1_geri= 5; 
const int motor2_ileri = 10; // motor2 => Makineyi titretmek için kullandığım motor.
byte speed = 255; // Motorların hızını 255 olarak ayarlıyoruz.

void setup() 
{ 
  Serial.begin(9600);  
   pinMode(5, OUTPUT); //Motorların pin çıkışlarını atıyoruz.
  pinMode(6, OUTPUT);
  pinMode(10, OUTPUT);


} 
 
 
void loop() 
{
 if(Serial.available())
  {
    int a = Serial.read(); // Serial porttan eğer 1 geliyor ise robotun kapağını açmaya yarayan  ve robotu titreten komut.
    if (a == '1')
    {
{ 

  digitalWrite(motor1_ileri, speed);
  digitalWrite(motor2_ileri, speed);    



  }
}
    }
     if(Serial.available())
  {
    int a = Serial.read(); // Serial porttan eğer 0 geliyor ise robotun kapağını kapatmaya ve titreşim motorunu kapatmaya yarayan komut.
    if (a == '0')

    {
      

  digitalWrite(motor1_geri, speed); 
    digitalWrite(motor2_ileri, 0);
    }
  }

  }

  




    







