using Microsoft.Xna.Framework;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class Ball:Rectangle

    {

        /*
         * FIELDS
         */

        float ballSpeed;
        float ballSpeedLimit;
        float ballSpeedInc;
        float ballXVelocity;
        float ballYVelocity;
        int ballSize;
        int ballStartPos;
        bool collision;
        const float bounceCorrection = 0.6f;


        public Ball()
        {

        }


        


    }



    //initialize ball
    ballSize = 16;
            ballSpeed = 500.0f;
            ballSpeedInc = 20.0f;
            ballSpeedLimit = 800f;
            ballXVelocity = -300.0f;
            ballYVelocity = 0.0f;
            collision = false;
            ballStartPos = (fieldSize - ballSize) / 2;
            ball = new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize);
}


private void checkBallCollision(GameTime gameTime)
{
    if (collision)
        moveBall(gameTime);


    collision = true;

    //barcollition
    if (ball.Intersects(topBar))
        barBounce("top");
    else if (ball.Intersects(bottomBar))
        barBounce("bottom");
    else if (ball.Intersects(leftBar))
        barBounce("left");
    else if (ball.Intersects(rightBar))
        barBounce("right");


    // check if ball touches the border if does that player loses a life and ball is reset
    else if (ball.Intersects(topBorder))
    {
        if (topPlayerLives.Equals(0))
            simpleBounce("y");
        else
            ResetBall(topBar);
    }

    else if (ball.Intersects(bottomBorder))
    {
        if (bottomPlayerLives.Equals(0))
            simpleBounce("y");
        else
            ResetBall(bottomBar);
    }

    else if (ball.Intersects(leftBorder))
    {
        if (leftPlayerLives.Equals(0))
            simpleBounce("x");
        else
            ResetBall(leftBar);
    }

    else if (ball.Intersects(rightBorder))
    {
        if (rightPlayerLives.Equals(0))
            simpleBounce("x");
        else
            ResetBall(rightBar);
    }


    else
    {
        collision = false;
        moveBall(gameTime);
    }
}

private void moveBall(GameTime gameTime)
{
    ball.Offset((ballXVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds), (ballYVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
}

private void simpleBounce(string v)
{

    switch (v)
    {
        case "x":
            ballXVelocity = -ballXVelocity;
            break;
        case "y":
            ballYVelocity = -ballYVelocity;
            break;
        default:
            break;
    }
}



private void barBounce(string v)
{
    /*
    switch (v)
    {
        case "top":
            simpleBounce("y");
            break;
        case "bottom":
            simpleBounce("y");
            break;
        case "left":
            simpleBounce("x");
            break;
        case "right":
            simpleBounce("x");
            break;
        default:
            break;
    }
    */
    if (ballSpeed < ballSpeedLimit)
        ballSpeed += ballSpeedInc;
    Console.WriteLine(ballSpeed);
    int barCenter;
    int ballCenter;
    float maxOffset;
    float offset;
    float multiplier;
    switch (v)
    {
        case "top":
            barCenter = topBar.Center.X;
            ballCenter = ball.Center.X;
            maxOffset = ((topBar.Width / 2.0f) + ball.Width + 2.0f);
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


        case "bottom":
            barCenter = bottomBar.Center.X;
            ballCenter = ball.Center.X;
            maxOffset = ((bottomBar.Width / 2.0f) + ball.Width + 2.0f);
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



        case "left":
            barCenter = leftBar.Center.Y;
            ballCenter = ball.Center.Y;
            maxOffset = ((leftBar.Height / 2.0f) + ball.Height + 2.0f);
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



        case "right":
            barCenter = rightBar.Center.Y;
            ballCenter = ball.Center.Y;
            maxOffset = ((rightBar.Height / 2.0f) + ball.Height + 2.0f);
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
