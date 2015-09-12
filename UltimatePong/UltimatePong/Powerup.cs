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
        double gameTime;
        int aliveTimer;
        bool alive;
        Color[] type;
        int rnd;
        int x;
        int y;
        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture)
        {
            this.spriteTexture = spriteTexture;
            this.spriteBatch = spriteBatch;
            gameTime = 0;
            aliveTimer = 0;
            type = new Color[3];
            type[0] = Color.Red;
            type[1] = Color.Blue;
            type[2] = Color.Green;
        }


        public void timer(GameTime gameTime)
        {
            if (this.gameTime < gameTime.TotalGameTime.Seconds)
            {
                this.gameTime = gameTime.TotalGameTime.Seconds;
                aliveTimer++;
                int randomWaitTime = new Random().Next(1, 3);

                switch (aliveTimer)
                {
                    case 3:
                        Console.WriteLine("POWERUP: Alive");
                        rng();
                        powerup = new Rectangle(x,y, 30, 30);
                        rnd = new Random().Next(0, 2);
                        alive = true;
                        break;
                    case 10:
                        Console.WriteLine("POWERUP: Killed");
                        alive = false;
                        break;
                }
                if (aliveTimer == 10 + randomWaitTime)
                    aliveTimer = 0;
            }

        }

        public void drawPowerup()
        {
            if(alive==true)
            spriteBatch.Draw(spriteTexture, powerup, type[rnd]);

        }

        public void rng()
        {
            x = new Random().Next(175, 625);
            y = new Random().Next(175, 625);
            if (new Random().Next(1, 3) == 1)
            {
                Console.WriteLine("reverse X");
                x = 650 - x;
            }
            if (new Random().Next(1, 3) == 2)
            {
                Console.WriteLine("reverse Y");
                y = 650 - y;
            }

        }

    }
}
