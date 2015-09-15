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
        int gameTime;
        int aliveTimer;
        bool alive;
        Random randomNumber;
        int randomDeathTime;

        Color[] Colortype;
        int powerupType;

        int powerupX;
        int powerupY;

        Rectangle bar;
        public String lastHitBar;
        public bool hit;

        GraphicsDevice GraphicsDevice;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, GraphicsDevice graphicsDevice,int  offset)
        {
            this.spriteTexture = spriteTexture;
            this.spriteBatch = spriteBatch;
            this.GraphicsDevice = graphicsDevice;

            gameTime = 0;
            aliveTimer = 0;

            Colortype = new Color[4];
            Colortype[0] = Color.Red;
            Colortype[1] = Color.Blue;
            Colortype[2] = Color.Green;
            Colortype[3] = Color.Gold;

            powerupX = 400;
            powerupY = 400;
            randomNumber = new Random(DateTime.Now.Millisecond+offset);

        }

        //Counts the second of the powerup life
        public void timer(GameTime gameTime)
        {
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                aliveTimer++;

                switch (aliveTimer)
                {
                    case 1: //spawn a powerup after 3 seconds of the previous powerup death
                        Console.WriteLine("POWERUP: Alive");
                        rngPosition();
                        powerup = new Rectangle(powerupX,powerupY, 30, 30);
                        powerupType = new Random().Next(0, 3); 
                        alive = true;
                        break;
                    case 3: //despawns the powerup after X seconds
                        Console.WriteLine("POWERUP: Killed");
                        randomDeathTime = randomNumber.Next(1, 4);
                        alive = false;
                        break;
                    default:
                        break;
                }
                if (aliveTimer == 3 + randomDeathTime)
                    aliveTimer = 0;

            }
        }

        //draws the powerup
        public void drawPowerup()
        {
            if (alive == true)
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
        public void checkCollision(Rectangle ball)
        {
            if(ball.Intersects(powerup))
            {
                Console.WriteLine("Powerup got hit");

                switch(powerupType)
                {
                    case 0:
                        redEvent();
                        break;
                    case 1:
                        blueEvent();
                        break;
                    case 2:
                        greenEvent();
                        break;
                    case 3:
                        goldEvent();
                        break;
                    default:
                        break;
                }

                alive = false;
                aliveTimer = 0;
                hit = true;
            }
        }

        //Bad for the player
        public void redEvent() 
        {
            Console.WriteLine("redEvent");
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                bar.Height = bar.Height-50;
            else
                bar.Width = bar.Width - 50;
        }

        //Neutral
        public void blueEvent() 
        {
            Console.WriteLine("blueEvent");
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                bar.Height = bar.Height + 50;
            else
                bar.Width = bar.Width + 50;
        }

        //good for the player
        public void greenEvent()
        {
            Console.WriteLine("greenEvent");
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                bar.Height = bar.Height + 50;
            else
                bar.Width = bar.Width + 50;
        }

        //Special power
        public void goldEvent()
        {
            Console.WriteLine("goldEvent");

        }

        //the last hit bar gets modified according to the hit powerup
        public Rectangle updateBar(Rectangle bar)
        {
            //fix the position of the bar
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                this.bar.Y = bar.Y - (this.bar.Height - bar.Height) / 2;
            else
                this.bar.X = bar.X - (this.bar.Width - bar.Width) / 2;

            return this.bar;
        }

        //this.bar becomes the last hit bar. The program knows which bar get the powerup
        public void setBar(Rectangle bar, String lastHitBar)
        {
            this.lastHitBar = lastHitBar;
            this.bar = bar;
        }

    }
}
