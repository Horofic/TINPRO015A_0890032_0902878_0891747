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

        Color[] type;
        int powerupType;

        int powerupX;
        int powerupY;

        UltimatePong ulti;
        Rectangle botbar;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, Rectangle botbar)
        {
            this.spriteTexture = spriteTexture;
            this.spriteBatch = spriteBatch;
            gameTime = 0;
            aliveTimer = 0;
            type = new Color[3];
            type[0] = Color.Red;
            type[1] = Color.Blue;
            type[2] = Color.Green;

            this.botbar = botbar;
        }


        public void timer(GameTime gameTime)
        {
            Console.WriteLine((int)gameTime.TotalGameTime.TotalSeconds);
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                aliveTimer++;
               // Console.WriteLine("aliveTimer: " + aliveTimer);

                switch (aliveTimer)
                {
                    case 3:
                        Console.WriteLine("POWERUP: Alive");
                        rngPosition();
                        powerup = new Rectangle(powerupX,powerupY, 30, 30);
                        powerupType = new Random().Next(0, 2);
                        alive = true;
                        break;
                    case 6:
                        Console.WriteLine("POWERUP: Killed");
                        aliveTimer = 0;
                        alive = false;
                        break;
                }
 
            }

        }

        public void drawPowerup()
        {
            if (alive == true)
                spriteBatch.Draw(spriteTexture, powerup, type[powerupType]);
            else
                powerup.Offset(-100, -100);
        }

        public void rngPosition()
        {
            powerupX = new Random().Next(175, 625);
            powerupY = new Random().Next(175, 625);
            /*if (new Random().Next(1, 3) == 1)
            {
                powerupX = 650 - powerupX;
            }
            if (new Random().Next(1, 3) == 2)
            {
                powerupY = 650 - powerupY;
            }*/
            if(new Random().Next(1,2)==1)
            {
                Console.WriteLine("Reverse");
                powerupX = 175 + (625 - powerupX);
            }

        }

        public void checkCollision(Rectangle ball)
        {
            if(ball.Intersects(powerup))
            {
                Console.WriteLine("Powerup got hit");

                switch(powerupType)
                {
                    case 0 :
                        redEvent();
                        break;
                    case 1 :
                        blueEvent();
                        break;
                    case 2 :
                        greenEvent();
                        break;
                }

                alive = false;
                aliveTimer = 0;
            }
        }

        public void redEvent() //Bad for the player
        {
            Console.WriteLine("redEvent");

            botbar.Width /= 2;
        }

        public void blueEvent() //Neutral
        {
            Console.WriteLine("blueEvent");

            botbar.Width /= 2;


        }

        public void greenEvent()//good for the player
        {
            Console.WriteLine("greenEvent");
            botbar.Width *= 2;

        }

    }
}
