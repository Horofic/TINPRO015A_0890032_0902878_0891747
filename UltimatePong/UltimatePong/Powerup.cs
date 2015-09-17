using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimatePong;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    class Powerup : Game
    {
        SpriteBatch spriteBatch;
        Texture2D spriteTexture;
        public Rectangle powerup;
        bool disable;
        int aliveTimer;
        bool alive;
        Random randomNumber;
        int randomDeathTime;

        Color[] Colortype;
        public int powerupType;

        int powerupX;
        int powerupY;

        public int lastHitBar;

        int difference = 50;

        GraphicsDevice GraphicsDevice;
        Timer timer;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, GraphicsDevice graphicsDevice,int  offset)
        {
            timer = new Timer();
            disable = false;
            this.spriteTexture = spriteTexture;
            this.spriteBatch = spriteBatch;
            this.GraphicsDevice = graphicsDevice;

            aliveTimer = 0;

            Colortype = new Color[5];
            Colortype[0] = Color.Red;
            Colortype[1] = Color.Green;
            Colortype[2] = Color.Blue;
            Colortype[3] = Color.Gold;
            Colortype[4] = Color.Purple;

            //test variables
            //powerupX = 400;
            //powerupY = 400;
            powerupType = 2;
            
            randomNumber = new Random(DateTime.Now.Millisecond+offset);
        }

        //Counts the second of the powerup life
        public void startTimer(GameTime gameTime)
        {
            if(!disable)
            timer.runTimer(gameTime);
            if (aliveTimer < timer.getElapsedTime())
            {
                aliveTimer = timer.getElapsedTime();

                switch (aliveTimer)
                {
                    case 1: //spawn a powerup after 3 seconds of the previous powerup death
                        rngPosition();
                        powerup = new Rectangle(powerupX, powerupY, 30, 30);
                        powerupType = new Random().Next(0, Colortype.Length);
                        alive = true;
                        break;
                    case 8: //despawns the powerup after X seconds
                        randomDeathTime = 8 + randomNumber.Next(1, 4);
                        alive = false;
                        break;
                    default:
                        break;
                }
                if (aliveTimer == randomDeathTime)
                {
                    aliveTimer = 0;
                    timer.reset();
                }
            }
            
        }

        //randomize the powerup spawn position
        public void rngPosition()
        {
            powerupX = randomNumber.Next(175, 625);
            powerupY = randomNumber.Next(175, 625);
            if(randomNumber.Next(1,3)==1)
            {
                powerupX = 175 + (625 - powerupX);
                powerupY = 175 + (625 - powerupY);
            }
        }

        //draws the powerup
        public void drawPowerup()
        {
            if (alive == true && !disable)
                spriteBatch.Draw(spriteTexture, powerup, Colortype[powerupType]);
            else
                powerup.Offset(-100, -100);
        }

        //check if powerup gets hit. Execute powerup event.
        public void checkCollision(ref Rectangle ball,ref Bar[] bar, int lastHitBar,ref int[] playerlives, ref float ballXVelocity,ref  float ballYVelocity)
        {
            if(ball.Intersects(powerup)&&alive&&this.lastHitBar>=0)
            {
                Console.WriteLine("Powerup got hit");
                this.lastHitBar = lastHitBar;

                switch(powerupType)
                {
                    case 0:
                        redEvent(ref bar[lastHitBar]);
                        break;
                    case 1:
                        greenEvent(ref bar[lastHitBar]);
                        break;
                    case 2:
                        blueEvent(ref ballXVelocity,ref ballYVelocity);
                        break;
                    case 3:
                        goldEvent(ref playerlives[lastHitBar]);
                        break;
                    case 4:
                        purpleEvent(ref bar[lastHitBar]);
                        break;
                    default:
                        break;
                }

                alive = false;
                aliveTimer = 0;
                timer.reset();
            }
        }

        //Bad for the player
        public void redEvent(ref Bar bar) 
        {
            Console.WriteLine("redEvent");
            if(bar.barLength-50>0)
            bar.barLength -= difference;
            if (lastHitBar > 1)
                bar.bar.Offset(0, (difference / 2));
            else
                bar.bar.Offset((difference / 2), 0);
        }

        //good for the player
        public void greenEvent(ref Bar bar)
        {
            Console.WriteLine("greenEvent");
            if (bar.barLength - 50 > 0)
                bar.barLength += difference;
            if (lastHitBar > 1)
                bar.bar.Offset(0, -(difference / 2));
            else
                bar.bar.Offset(-(difference / 2), 0);
        }

        //Change ball direction to random value
        public void blueEvent(ref float ballXVelocity, ref float ballYVelocity)
        {
            Console.WriteLine("blueEvent");
            int randomVelocity = randomNumber.Next(0, 3);
            if(ballXVelocity==0)
            {
                ballXVelocity += 200;
            }
            else if (ballYVelocity == 0)
            {
                ballYVelocity += 200;
            }
            if (randomVelocity == 0 || randomVelocity == 2)
                ballXVelocity *= -1;
            if (randomVelocity == 1 || randomVelocity == 2)
                ballYVelocity *= -1;
        }

        //+1 life
        public void goldEvent(ref int playerlife)
        {
            Console.WriteLine("goldEvent");
            playerlife++;
        }

        //inverted controls
        public void purpleEvent(ref Bar bar)
        {
            Console.WriteLine("purpleEvent");
            Keys temp = bar.controls[0];
            bar.controls[0] = bar.controls[1];
            bar.controls[1] = temp;
        }

        public void disabled(bool disabled)
        {
            timer.reset();
            aliveTimer = 0;
            disable = disabled;
        }
    }
}
