using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{

    class BallController

    {

        /*
         * FIELDS
         */

        //public
        public int fieldSize;
        public int ballSize;
        public float ballSpeed;
        public float ballSpeedLimit;
        public float ballSpeedInc;
        public float bounceCorrection;
        public float ballXVelocity;
        public float ballYVelocity;
        public int lastHitPlayer;
        public Entity lastHitPlayerBar;
        public PowerupController hitPowerup; 
        public Entity ball;



        //local use

        int ballStartPos;
        bool spawning;
        Timer timer;

        SpriteBatch spriteBatch;
        Texture2D spriteTexture;

        SoundEffect bleepHigh;
        SoundEffect bleepLow;


        /*
         *  CONSTRUCTORS
         */

        public BallController(int fieldSize_, int ballSize_, float ballSpeed_, float ballSpeedLimit_, float ballSpeedInc_, float bounceCorrection_, SpriteBatch sb, Texture2D texture, SoundEffect high, SoundEffect low)
        {
            this.fieldSize = fieldSize_;
            this.ballSize = ballSize_;
            this.ballSpeed = ballSpeed_;
            this.ballSpeedLimit = ballSpeedLimit_;
            this.ballSpeedInc = ballSpeedInc_;
            this.bounceCorrection = bounceCorrection_;
            bleepLow = low;
            bleepHigh = high;
            ballStartPos = (fieldSize - ballSize) / 2;
            ball = new Entity(texture, new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize));
            timer = new Timer();
            lastHitPlayer = -1;
            spriteBatch = sb;
            spriteTexture = texture;
            lastHitPlayer = -1;
        }

        /*
         *  ----------------
         *  PUBLIC FUNCTIONS
         *  ----------------
         */

        /*
         *  Spawns the ball in the center of the playingfield and lets it move in a random direction
         */
        public void spawnBall(int player, double elapsedTime)
        {

            ball = new Entity(spriteTexture, new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize));
            timer.setTime(elapsedTime);
            spawning = true;

            switch (player)
            {
                case 0:
                    ballYVelocity = -ballSpeed;
                    ballXVelocity = 0;
                    break;
                case 1:
                    ballYVelocity = ballSpeed;
                    ballXVelocity = 0;
                    break;
                case 2:
                    ballXVelocity = -ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 3:
                    ballXVelocity = ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 4:
                    ballSpeed = 0;
                    ballXVelocity = 0;
                    ballYVelocity = 0;
                    break;
                default:
                    break;
            }

        }



        public void drawBall()
        {
            spriteBatch.Draw(spriteTexture, ball.rectangle, Color.White);
        }


        /*
         * Updates the ball position and makes the ball bounce on a collision. Returns an int to indicate that a player loses a live. default = -1, top = 0 bottom = 1
         */
        public BallMovementInstructionResult updateBall(float deltaTime, double elapsedTime, List<Entity> bars, List<Entity> borders, List<PowerupController> powerups, int[] lives)
        {

            if (spawning)
            {
                if (!timer.getTimeDone(elapsedTime, 2000))
                {
                    return BallMovementInstructionResult.Running;
                }
                spawning = false;

            }


            //barcollision
            for (int i = 0; i < 4; i++)
            {
                if (ball.rectangle.Intersects(bars[i].rectangle))
                {
                    barBounce(i, bars[i].rectangle);
                    while (ball.rectangle.Intersects(bars[i].rectangle))
                        moveBall(deltaTime);
                    lastHitPlayer = i;
                    lastHitPlayerBar = bars[i];
                    bleepHigh.Play();
                    return BallMovementInstructionResult.Running;
                }
            }

            //bordercollision

            for (int i = 0; i < 4; i++)
            {
                if (ball.rectangle.Intersects(borders[i].rectangle))
                {
                    if (lives[i].Equals(0))
                    {
                        simpleBounce(i);
                        while (ball.rectangle.Intersects(borders[i].rectangle))
                            moveBall(deltaTime);
                        bleepLow.Play();
                        return BallMovementInstructionResult.Running;
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                return BallMovementInstructionResult.DoneAndPlayer0LostALife;
                            case 1:
                                return BallMovementInstructionResult.DoneAndPlayer1LostALife;
                            case 2:
                                return BallMovementInstructionResult.DoneAndPlayer2LostALife;
                            case 3:
                                return BallMovementInstructionResult.DoneAndPlayer3LostALife;
                            default:
                                return BallMovementInstructionResult.OutOfBounds;
                        }
                    }
                }
            }
            //powerup collision
            foreach(PowerupController powerupController in powerups)
            {
                if (powerupController.powerup.rectangle.Intersects(ball.rectangle))
                {
                    hitPowerup = powerupController;
                    return BallMovementInstructionResult.RunningAndPowerupHit;
                }
            }



            //ball is out of bounds
            if (ball.rectangle.Left > fieldSize || ball.rectangle.Right < 0 || ball.rectangle.Top > fieldSize || ball.rectangle.Bottom < 0)
            {
                return BallMovementInstructionResult.OutOfBounds;
            }


            //the ball isnt in collision
            moveBall(deltaTime);
            return BallMovementInstructionResult.Running;
        }







        /*
         *  -----------------
         *  PRIVATE FUNCTIONS
         *  -----------------
         */


        /*
         *  MOVEBALL()
         *  Moves the ball 
         */

        private void moveBall(float deltaTime)
        {
            ball = ball.CreateMoved((int)(ballXVelocity * deltaTime), (int)(ballYVelocity * deltaTime));
        }


        public void simpleBounce(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                    ballYVelocity = -ballYVelocity;
                    break;
                case 2:
                case 3:
                    ballXVelocity = -ballXVelocity;
                    break;
                default:
                    break;
            }
        }

        private void barBounce(int i, Rectangle bar)
        {
            //increase speed
            if (ballSpeed < ballSpeedLimit)
                ballSpeed += ballSpeedInc;
            Console.WriteLine(ballSpeed);


          

            // Advanced bounce

            int barCenter;
            int ballCenter;
            float maxOffset;
            float offset;
            float multiplier;
            switch (i)
            {
                case 0:
                    barCenter = bar.Center.X;
                    ballCenter = ball.rectangle.Center.X;
                    maxOffset = ((bar.Width / 2.0f) + ball.rectangle.Width + 2.0f);
                    offset = barCenter - ballCenter;

                    if (offset == 0)
                    {
                        ballXVelocity = 0;
                        ballYVelocity = ballSpeed;
                    }
                    //ball bounces right
                    else if (offset < 0)
                    {
                        multiplier = (1 - ((-offset) / maxOffset) * bounceCorrection);
                        ballYVelocity = multiplier * ballSpeed;
                        ballXVelocity = (float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballYVelocity * ballYVelocity));
                    }
                    //ball bounces left
                    else if (offset > 0)
                    {
                        multiplier = (1 - (offset / maxOffset) * bounceCorrection);
                        ballYVelocity = multiplier * ballSpeed;
                        ballXVelocity = -(float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballYVelocity * ballYVelocity));
                    }
                    break;


                case 1:
                    barCenter = bar.Center.X;
                    ballCenter = ball.rectangle.Center.X;
                    maxOffset = ((bar.Width / 2.0f) + ball.rectangle.Width + 2.0f);
                    offset = barCenter - ballCenter;

                    if (offset == 0)
                    {
                        ballXVelocity = 0;
                        ballYVelocity = -ballSpeed;
                    }


                    //ball bounces right
                    else if (offset < 0)
                    {
                        multiplier = (1 - ((-offset) / maxOffset) * bounceCorrection);
                        ballYVelocity = -multiplier * ballSpeed;
                        ballXVelocity = (float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballYVelocity * ballYVelocity));
                    }

                    //ball bounces left
                    else if (offset > 0)
                    {
                        multiplier = (1 - (offset / maxOffset) * bounceCorrection);
                        ballYVelocity = -multiplier * ballSpeed;
                        ballXVelocity = -(float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballYVelocity * ballYVelocity));
                    }
                    break;



                case 2:
                    barCenter = bar.Center.Y;
                    ballCenter = ball.rectangle.Center.Y;
                    maxOffset = ((bar.Height / 2.0f) + ball.rectangle.Height + 2.0f);
                    offset = barCenter - ballCenter;

                    if (offset == 0)
                    {
                        ballXVelocity = ballSpeed;
                        ballYVelocity = 0;
                    }


                    //ball bounces down
                    else if (offset < 0)
                    {
                        multiplier = (1 - ((-offset) / maxOffset) * bounceCorrection);
                        ballXVelocity = multiplier * ballSpeed;
                        ballYVelocity = (float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballXVelocity * ballXVelocity));
                    }

                    //ball bounces up
                    else if (offset > 0)
                    {
                        multiplier = (1 - (offset / maxOffset) * bounceCorrection);
                        ballXVelocity = multiplier * ballSpeed;
                        ballYVelocity = -(float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballXVelocity * ballXVelocity));
                    }
                    break;



                case 3:
                    barCenter = bar.Center.Y;
                    ballCenter = ball.rectangle.Center.Y;
                    maxOffset = ((bar.Height / 2.0f) + ball.rectangle.Height + 2.0f);
                    offset = barCenter - ballCenter;

                    if (offset == 0)
                    {
                        ballXVelocity = -ballSpeed;
                        ballYVelocity = 0;
                    }


                    //ball bounces down
                    else if (offset < 0)
                    {
                        multiplier = (1 - ((-offset) / maxOffset) * bounceCorrection);
                        ballXVelocity = -multiplier * ballSpeed;
                        ballYVelocity = (float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballXVelocity * ballXVelocity));
                    }

                    //ball bounces up
                    else if (offset > 0)
                    {
                        multiplier = (1 - (offset / maxOffset) * bounceCorrection);
                        ballXVelocity = -multiplier * ballSpeed;
                        ballYVelocity = -(float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballXVelocity * ballXVelocity));
                    }
                    break;
                default:
                    break;
            }
        }

    }
}