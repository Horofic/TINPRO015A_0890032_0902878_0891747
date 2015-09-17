using Microsoft.Xna.Framework;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class Ball

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
        public bool classicBounce;



        //local use

        int ballStartPos;
        float ballXVelocity;
        float ballYVelocity;
        bool collision;
        bool active;
        Rectangle ball;



        /*
         *  CONSTRUCTORS
         */
        public Ball()
        {
            fieldSize = 800;
            ballSize = 16;
            ballSpeed = 500.0f;
            ballSpeedLimit = 800f;
            ballSpeedInc = 20.0f;
            bounceCorrection = 0.6f;
            classicBounce = false;

            collision = false;
            active = true;
            ballStartPos = (fieldSize - ballSize) / 2;
            ball = new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize);
        }

        public Ball(int fieldSize_, int ballSize_, float ballSpeed_, float ballSpeedLimit_, float ballSpeedInc_, float bounceCorrection_, bool classicBounce_)
        {
            this.fieldSize = fieldSize_;
            this.ballSize = ballSize_;
            this.ballSpeed = ballSpeed_;
            this.ballSpeedLimit = ballSpeedLimit_;
            this.ballSpeedInc = ballSpeedInc_;
            this.bounceCorrection = bounceCorrection_;
            this.classicBounce = classicBounce_;

            collision = false;
            int ballStartPos = (fieldSize - ballSize) / 2;
            ball = new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize);
        }

        /*
         *  ----------------
         *  PUBLIC FUNCTIONS
         *  ----------------
         */

        /*
         *  Spawns the ball in the center of the playingfield and lets it move in a random direction
         */
        public void spawnBall(int player, float speed)
        {
            ballSpeed = speed;
            collision = false;
            active = true;
            ball.Offset((-ball.X + ballStartPos), (-ball.Y + ballStartPos));

            switch (player)
            {

            }

        }

        public void removeBall()
        {
            throw new NotImplementedException();
        }

        public void drawBall()
        {
            throw new NotImplementedException();
        }


        /*
         * Updates the ball position and makes the ball bounce on a collision. Returns an int to indicate that a player loses a live. default = -1, top = 0 bottom = 1
         */
        public int update(GameTime gameTime, Rectangle[] bars, Rectangle[] borders, int[] lives)
        {
            if (!active)
                return -1;

            //barcollition
            for(int i = 0; i < 4; i++)
            {
                if (ball.Intersects(bars[i]))
                {
                    barBounce(i, bars[i]);
                    while (ball.Intersects(bars[i]))
                        moveBall(gameTime);
                    return -1;
                }
            }

            // check if ball touches the border if does that player loses a life and ball is reset

            for (int i = 0; i < 4; i++)
            {
                if (ball.Intersects(borders[i]))
                {
                    if (lives[i].Equals(0))
                    {
                        simpleBounce(i);
                        while (ball.Intersects(borders[i]))
                            moveBall(gameTime);
                        return -1;
                    }
                    else
                    {
                        return i;
                    }
                }
            }
            //the ball isnt in collision
             moveBall(gameTime);
            return -1;
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

        private void moveBall(GameTime gameTime)
        {
            ball.Offset((ballXVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds), (ballYVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
        }


        private void simpleBounce(int i)
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

            //classic Bounce type
            if (classicBounce)
                simpleBounce(i);


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
                    ballCenter = ball.Center.X;
                    maxOffset = ((bar.Width / 2.0f) + ball.Width + 2.0f);
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
                    ballCenter = ball.Center.X;
                    maxOffset = ((bar.Width / 2.0f) + ball.Width + 2.0f);
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
                    ballCenter = ball.Center.Y;
                    maxOffset = ((bar.Height / 2.0f) + ball.Height + 2.0f);
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
                    ballCenter = ball.Center.Y;
                    maxOffset = ((bar.Height / 2.0f) + ball.Height + 2.0f);
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