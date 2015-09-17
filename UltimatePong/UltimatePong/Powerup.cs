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
        private bool hit;

        int difference = 50;

        Keys[] keys;

        GraphicsDevice GraphicsDevice;
        Timer timer;

        public Powerup(SpriteBatch spriteBatch,Texture2D spriteTexture, GraphicsDevice graphicsDevice,int  offset)
        {
            timer = new Timer(10);
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
        public void checkCollision(ref Rectangle ball,ref Rectangle topBar, ref Rectangle bottomBar, ref Rectangle leftBar, ref Rectangle rightBar)
        {
            if (ball.Intersects(powerup))
                hit = true;
            if(hit)
            {
                Console.WriteLine("Powerup got hit");

                switch(powerupType)
                {
                    case 0:
                        redEvent(ref topBar, ref bottomBar, ref leftBar, ref rightBar);
                        break;
                    case 1:
                        greenEvent(ref topBar, ref bottomBar, ref leftBar, ref rightBar);
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
        public void redEvent(ref Rectangle topBar, ref Rectangle bottomBar, ref Rectangle leftBar, ref Rectangle rightBar) 
        {
            Console.WriteLine("redEvent");
            switch(lastHitBar)
            {
                case "topBar":
                    topBar.Width -= difference;
                    fixBarPosition(ref topBar);
                    break;
                case "bottomBar":
                    bottomBar.Width -= difference;
                    fixBarPosition(ref bottomBar);
                    break;
                case "leftBar":
                    leftBar.Height -= difference;
                    fixBarPosition(ref leftBar);
                    break;
                case "rightBar":
                    rightBar.Height -= difference;
                    fixBarPosition(ref rightBar);
                    break;
            }
        }

        //good for the player
        public void greenEvent(ref Rectangle topBar, ref Rectangle bottomBar, ref Rectangle leftBar, ref Rectangle rightBar)
        {
            Console.WriteLine("greenEvent");
            switch (lastHitBar)
            {
                case "topBar":
                    topBar.Width += difference;
                    fixBarPosition(ref topBar);
                    break;
                case "bottomBar":
                    bottomBar.Width += difference;
                    fixBarPosition(ref bottomBar);
                    break;
                case "leftBar":
                    leftBar.Height += difference;
                    fixBarPosition(ref leftBar);
                    break;
                case "rightBar":
                    rightBar.Height += difference;
                    fixBarPosition(ref rightBar);
                    break;
            }
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

        //Green/Red Event: the last hit bar gets modified according to the hit powerup
        public void fixBarPosition(ref Rectangle bar)
        {
            //fix the position of the bar
            if (lastHitBar == "leftBar" || lastHitBar == "rightBar")
                bar.Y = bar.Y - (difference / 2);
            else
                bar.X = bar.X - (difference / 2);
        }

        //purpleEvent: Invert keys
       /* public Keys[] updateKeys()
        {
            return this.keys;
        }*/

        //this.bar becomes the last hit bar. The program knows which bar hit the ball most recent.
       /* public void setBar(Rectangle bar, String lastHitBar,Keys[] keys)
        {
            this.lastHitBar = lastHitBar;
            this.bar = bar;
            this.keys = keys;
        }*/

        public void disable()
        {
            enable = false;
        }
    }
}
