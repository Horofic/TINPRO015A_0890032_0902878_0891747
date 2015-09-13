﻿using System;
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

        Rectangle bar;
        public String lastHitBar;
        public bool hit;

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

        //Counts the second of the powerup life
        public void timer(GameTime gameTime)
        {
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                aliveTimer++;

                switch (aliveTimer)
                {
                    case 3: //spawn a powerup after 3 seconds of the previous powerup death
                        Console.WriteLine("POWERUP: Alive");
                        rngPosition();
                        powerup = new Rectangle(powerupX,powerupY, 30, 30);
                        powerupType = new Random().Next(0, 2);
                        alive = true;
                        break;
                    case 6: //despawns the powerup after X seconds
                        Console.WriteLine("POWERUP: Killed");
                        aliveTimer = 0;
                        alive = false;
                        break;
                }
 
            }

        }

        //draws the powerup
        public void drawPowerup()
        {
            if (alive == true)
                spriteBatch.Draw(spriteTexture, powerup, type[powerupType]);
            else
                powerup.Offset(-100, -100);
        }

        //randomize the powerup spawn position
        public void rngPosition()
        {
            powerupX = new Random().Next(175, 625);
            powerupY = new Random().Next(175, 625);
            if(new Random().Next(1,2)==1)
            {
                Console.WriteLine("Reverse");
                powerupX = 175 + (625 - powerupX);
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
                bar.Height = 128;
            else
                bar.Width = 128;
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

        //the last hit bar gets modified according to the hit powerup
        public Rectangle updateBar()
        {
            return bar;
        }

        //this.bar becomes the last hit bar. The program knows which bar get the powerup
        public void setBar(Rectangle bar, String lastHitBar)
        {
            this.lastHitBar = lastHitBar;
            this.bar = bar;
        }

    }
}
