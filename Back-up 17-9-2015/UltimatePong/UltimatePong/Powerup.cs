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
        public String lastHitBar;
        public bool hit;

        Keys[] keys;

        GraphicsDevice GraphicsDevice;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, GraphicsDevice graphicsDevice,int  offset)
        {
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
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds&&enable)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                aliveTimer++;

                switch (aliveTimer)
                {
                    case 1: //spawn a powerup after 3 seconds of the previous powerup death
                        rngPosition();
                        powerup = new Rectangle(powerupX,powerupY, 30, 30);
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
                    aliveTimer = 0;

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
                        greenEvent();
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
                hit = true;
            }
        }

        //Bad for the player
        public void redEvent() 
        {
            Console.WriteLine("redEvent");
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar" && bar.Height>50)
                bar.Height = bar.Height-50;
            else if (lastHitBar == "topBar" || lastHitBar == "bottomBar" && bar.Width > 50)
                bar.Width = bar.Width - 50;
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

        //Change ball direction to random value
        public void blueEvent()
        {
            Console.WriteLine("blueEvent");
            lastHitBar = "ballEvent";
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
            Keys temp = keys[0];
            keys[0] = keys[1];
            keys[1] = temp;
            
        }

        //Green/Red Event: the last hit bar gets modified according to the hit powerup
        public Rectangle updateBar(Rectangle bar)
        {
            //fix the position of the bar
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                this.bar.Y = bar.Y - (this.bar.Height - bar.Height) / 2;
            else
                this.bar.X = bar.X - (this.bar.Width - bar.Width) / 2;

            return this.bar;
        }

        //purpleEvent: Invert keys
        public Keys[] updateKeys()
        {
            return this.keys;
        }

        //this.bar becomes the last hit bar. The program knows which bar hit the ball most recent.
        public void setBar(Rectangle bar, String lastHitBar,Keys[] keys)
        {
            this.lastHitBar = lastHitBar;
            this.bar = bar;
            this.keys = keys;
        }

        public void disable()
        {
            enable = false;
        }
    }
}
