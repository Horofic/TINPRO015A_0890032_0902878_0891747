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
        bool enable;
        int gameTime;
        int aliveTimer;
        bool alive;
        Random randomNumber;
        int randomDeathTime;

        Color[] Colortype;
        public int powerupType;

        int powerupX;
        int powerupY;

        Rectangle bar;
        public int lastHitBar;
        private bool hit;

        int difference = 50;

        Keys[] keys;

        GraphicsDevice GraphicsDevice;
        Timer timer;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, GraphicsDevice graphicsDevice,int  offset)
        {
            timer = new Timer();
            enable = true;
            this.spriteTexture = spriteTexture;
            this.spriteBatch = spriteBatch;
            this.GraphicsDevice = graphicsDevice;

            gameTime = 0;
            aliveTimer = 0;

            Colortype = new Color[5];
            Colortype[0] = Color.Red;
            Colortype[1] = Color.Green;
            Colortype[2] = Color.Blue;
            Colortype[3] = Color.Gold;
            Colortype[4] = Color.Purple;

            powerupX = 400;
            powerupY = 400;
            
            randomNumber = new Random(DateTime.Now.Millisecond+offset);

        }

        //Counts the second of the powerup life
        public void startTimer(GameTime gameTime)
        {
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

        //draws the powerup
        public void drawPowerup()
        {
            if (alive == true&&enable)
                spriteBatch.Draw(spriteTexture, powerup, Colortype[powerupType]);
            else
                powerup.Offset(-100, -100);
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

        //check if powerup gets hit. Execute powerup event.
        public void checkCollision(ref Rectangle ball,ref Bar[] bar)
        {
            if (ball.Intersects(powerup))
                hit = true;
            if(hit)
            {
                Console.WriteLine("Powerup got hit");

                switch(powerupType)
                {
                    case 0:
                        redEvent(ref bar[lastHitBar]);
                        break;
                    case 1:
                        greenEvent(ref bar[lastHitBar]);
                        break;
                    case 2:
                        blueEvent();
                        break;
                    case 3:
                        goldEvent();
                        break;
                    case 4:
                        purpleEvent();
                        break;
                    default:
                        break;
                }

                alive = false;
                aliveTimer = 0;
                timer.reset();
                hit = false;
            }
        }

        //Bad for the player
        public void redEvent(ref Bar bar) 
        {
            Console.WriteLine("redEvent");
            bar.barLength -= difference;
            if (lastHitBar > 1)
                bar.bar.Y = bar.bar.Y - (difference / 2);
            else
                bar.bar.X = bar.bar.X - (difference / 2);

        }

        //good for the player
        public void greenEvent(ref Bar bar)
        {
            Console.WriteLine("greenEvent");
            bar.barLength += difference;
            if (lastHitBar > 1)
                bar.bar.Y = bar.bar.Y - (difference / 2);
            else
                bar.bar.X = bar.bar.X - (difference / 2);

        }

        //Change ball direction to random value
        public void blueEvent()
        {
            Console.WriteLine("blueEvent");
            /*int randomVelocity = randomNumber.Next(0, 3);
            if (randomVelocity == 0 || randomVelocity == 2)
                ballXVelocity *= -1;
            if (randomVelocity == 1 || randomVelocity == 2)
                ballYVelocity *= -1;*/
        }

        //+1 life
        public void goldEvent()
        {
            Console.WriteLine("goldEvent");

        }

        //inverted controls
        public void purpleEvent()
        {
            Console.WriteLine("purpleEvent");
            /*Keys temp = keys[0];
            keys[0] = keys[1];
            keys[1] = temp;*/
            
        }

        //purpleEvent: Invert keys
       /* public Keys[] updateKeys()
        {
            return this.keys;
        }*/

        public void disable()
        {
            enable = false;
        }
    }
}
