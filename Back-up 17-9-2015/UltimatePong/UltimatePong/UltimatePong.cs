﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace UltimatePong
{
    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();

        //sprites
        Texture2D spriteTexture;

        Texture2D topBorderTexture;
        Texture2D leftBorderTexture;
        Texture2D rightBorderTexture;
        Texture2D bottomBorderTexture;

        Rectangle ball;
        Rectangle topBar;
        Rectangle bottomBar;
        Rectangle leftBar;
        Rectangle rightBar;

        Rectangle topBorder;
        Rectangle bottomBorder;
        Rectangle leftBorder;
        Rectangle rightBorder;

        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWidth = 5;

        //ball properties
        float ballSpeed;
        float ballSpeedLimit;
        float ballSpeedInc;
        float ballXVelocity;
        float ballYVelocity;
        int ballSize;
        int ballStartPos;
        bool collision;
        float multiplier;
        int ballCenter;
        float maxOffset;
        float offset;
        int lastSpawnedDirection;
        bool pause;
        //pause
        int pauseTimer;
        int gameTime;


        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;
        const float bounceCorrection = 0.6f;

        //top bar properties
        float topBarSpeed;
        int topBarLength;
        int topBarSpeedMultiplier = 1;
        Keys[] topBarKeys = new Keys[3];

        //bottom bar properties
        float bottomBarSpeed;
        int bottomBarLength;
        int bottomBarSpeedMultiplier = 1;
        Keys[] bottomBarKeys = new Keys[3];

        //left bar properties
        float leftBarSpeed;
        int leftBarLength;
        int leftBarSpeedMultiplier = 1;
        Keys[] leftBarKeys = new Keys[3];

        //right bar properties
        float rightBarSpeed;
        int rightBarLength;
        int rightBarSpeedMultiplier = 1;
        Keys[] rightBarKeys = new Keys[3];

        //Player lives
        int topPlayerLives;
        int bottomPlayerLives;
        int leftPlayerLives;
        int rightPlayerLives;

        public int players;
        public int lives;
        public bool powerups;
        public bool bounceType;

        Powerup[] powerup;
        int powerupCount;

        SpriteFont font;

        public UltimatePong(int playerAmount, int livesAmount, bool powerups, bool bounceType)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";

            this.players = playerAmount;
            this.lives = livesAmount;
            this.powerups = powerups;
            this.bounceType = bounceType;

            //Printlines
            System.Console.WriteLine("players:" + players);
            System.Console.WriteLine("lives:" + lives);
            System.Console.WriteLine("powerups:" + powerups);
            System.Console.WriteLine("bounceType:" + bounceType);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Ball texture
            spriteTexture = Content.Load<Texture2D>("ball1.png");
            //Bar(player) textures
            topBorderTexture = Content.Load<Texture2D>("bar.png");
            leftBorderTexture = Content.Load<Texture2D>("bar.png");
            rightBorderTexture = Content.Load<Texture2D>("bar.png");
            bottomBorderTexture = Content.Load<Texture2D>("bar.png");
            //Font texture
            font = Content.Load<SpriteFont>("font");


            base.Window.AllowUserResizing = false;

            //initialize bar controls
            topBarKeys[0] = Keys.T; //LEFT
            topBarKeys[1] = Keys.U; //RIGHT
            topBarKeys[2] = Keys.Y; //BOOST

            bottomBarKeys[0] = Keys.V; //LEFT
            bottomBarKeys[1] = Keys.N; //RIGHT
            bottomBarKeys[2] = Keys.B; //BOOST

            leftBarKeys[0] = Keys.A; //UP
            leftBarKeys[1] = Keys.D; //DOWN
            leftBarKeys[2] = Keys.S; //BOOST

            rightBarKeys[0] = Keys.L; //UP
            rightBarKeys[1] = Keys.J; //DOWN
            rightBarKeys[2] = Keys.K; //BOOST

            //initialize bar properties
            topBarSpeed = barSpeed;
            bottomBarSpeed = barSpeed;
            leftBarSpeed = barSpeed;
            rightBarSpeed = barSpeed;

            topBarLength = barLength;
            bottomBarLength = barLength;
            leftBarLength = barLength;
            rightBarLength= barLength;


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


            //initialize bars
            int barStartPos = (fieldSize - barLength) / 2;
            topBar = new Rectangle(barStartPos, barToBorderDist, barLength, barWidth);
            bottomBar = new Rectangle(barStartPos, fieldSize-barToBorderDist-barWidth, barLength, barWidth);
            leftBar = new Rectangle(barToBorderDist, barStartPos, barWidth, barLength);
            rightBar = new Rectangle(fieldSize - barToBorderDist - barWidth, barStartPos, barWidth, barLength);

            //initialize borders
            topBorder = new Rectangle(0, 0, fieldSize, borderWidth);
            bottomBorder = new Rectangle(0, fieldSize - borderWidth, fieldSize, borderWidth);
            leftBorder = new Rectangle(0, 0, borderWidth, fieldSize);
            rightBorder = new Rectangle(fieldSize - borderWidth, 0, borderWidth, fieldSize);

            //initialize player lives
            topPlayerLives = bottomPlayerLives = leftPlayerLives = rightPlayerLives = lives;

            //powerup
            powerup = new Powerup[3];
            powerupCount = 0;
            for(int i=0;i<powerup.Length;i++)
            powerup[i] = new Powerup(spriteBatch, spriteTexture, GraphicsDevice,i);

            gameTime = 0;

            base.Initialize();
        }


  
        protected override void Update(GameTime gameTime)
        {
            checkInput(gameTime);
            // Collision detection and ball movement
            checkBallCollision(gameTime);

            //powerup
            
            powerupEvents(gameTime);
            if(pause)
                pauseBall(gameTime);
            base.Update(gameTime);
        }

        public void powerupEvents(GameTime gameTime)
        {
            if (powerupCount > 2)
                powerupCount = 0;

            powerup[powerupCount].startTimer(gameTime);
            powerup[powerupCount].checkCollision(ball);

            if (ball.Intersects(topBar))
                powerup[powerupCount].setBar(topBar,"topBar",topBarKeys);
            else if (ball.Intersects(bottomBar))
                powerup[powerupCount].setBar( bottomBar,"bottomBar", bottomBarKeys);
            else if (ball.Intersects(leftBar))
                powerup[powerupCount].setBar(leftBar,"leftBar",leftBarKeys);
            else if (ball.Intersects(rightBar))
                powerup[powerupCount].setBar(rightBar,"rightBar",rightBarKeys);
            
            if (powerup[powerupCount].hit == true)
            {
                switch (powerup[powerupCount].lastHitBar)
                {
                    case "leftBar":
                        if (powerup[powerupCount].powerupType < 2)
                            leftBar = powerup[powerupCount].updateBar(leftBar);
                        else if (powerup[powerupCount].powerupType == 3 && leftPlayerLives != 0)
                            leftPlayerLives += 1;
                        else if (powerup[powerupCount].powerupType == 4)
                            leftBarKeys = powerup[powerupCount].updateKeys();
                        break;
                    case "rightBar":
                        if (powerup[powerupCount].powerupType < 2)
                            rightBar = powerup[powerupCount].updateBar(rightBar);
                        else if (powerup[powerupCount].powerupType == 3 && rightPlayerLives != 0)
                            rightPlayerLives += 1;
                        else if (powerup[powerupCount].powerupType == 4)
                            rightBarKeys = powerup[powerupCount].updateKeys();
                        break;
                    case "topBar":
                        if (powerup[powerupCount].powerupType < 2)
                            topBar = powerup[powerupCount].updateBar(topBar);
                        else if (powerup[powerupCount].powerupType == 3 && topPlayerLives != 0)
                            topPlayerLives += 1;
                        else if (powerup[powerupCount].powerupType == 4)
                            topBarKeys = powerup[powerupCount].updateKeys();
                        break;
                    case "bottomBar":
                        if (powerup[powerupCount].powerupType < 2)
                            bottomBar = powerup[powerupCount].updateBar(bottomBar);
                        else if (powerup[powerupCount].powerupType == 3 && bottomPlayerLives != 0)
                            bottomPlayerLives += 1;
                        else if (powerup[powerupCount].powerupType == 4)
                            bottomBarKeys = powerup[powerupCount].updateKeys();
                        break;
                    case "ballEvent":
                        int randomVelocity = random.Next(0, 3); 
                        if (randomVelocity== 0 || randomVelocity == 2)
                            ballXVelocity *= -1;
                        if (randomVelocity == 1 || randomVelocity == 2)
                            ballYVelocity *= -1; 
                        break;
                    default:
                        break;
                }
                powerup[powerupCount].hit = false;
            }
            powerupCount++;
        }

        private void checkInput(GameTime gameTime)
        {

            
            var keyBoardstate = Keyboard.GetState();
                      

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controls, just for testing
            /* if (Keyboard.GetState().IsKeyDown(Keys.W))
                 ball.Offset(0, -ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

             if (Keyboard.GetState().IsKeyDown(Keys.S))
                 ball.Offset(0, ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

             if (Keyboard.GetState().IsKeyDown(Keys.A))
                 ball.Offset(-ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

             if (Keyboard.GetState().IsKeyDown(Keys.D))
                 ball.Offset(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);*/


            //Top bar controls
            if (keyBoardstate.IsKeyDown(topBarKeys[2]))
                topBarSpeedMultiplier = 3;
            else
                topBarSpeedMultiplier = 0;

            if (keyBoardstate.IsKeyDown(topBarKeys[0]))
                if (!topBar.Intersects(leftBorder))
                    topBar.Offset((-topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - topBarSpeedMultiplier, 0);
          
            if (keyBoardstate.IsKeyDown(topBarKeys[1]))
                if (!topBar.Intersects(rightBorder))
                    topBar.Offset((topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + topBarSpeedMultiplier, 0);



            //Bottom bar controlszcc n
            if (keyBoardstate.IsKeyDown(bottomBarKeys[2]))
                bottomBarSpeedMultiplier = 3;
            else
                bottomBarSpeedMultiplier = 0;

            if (keyBoardstate.IsKeyDown(bottomBarKeys[0]))
                if (!bottomBar.Intersects(leftBorder))
                    bottomBar.Offset((-bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - bottomBarSpeedMultiplier, 0);

            if (keyBoardstate.IsKeyDown(bottomBarKeys[1]))
                if (!bottomBar.Intersects(rightBorder))
                    bottomBar.Offset((bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + bottomBarSpeedMultiplier, 0);

            //Left bar controls
            if (keyBoardstate.IsKeyDown(leftBarKeys[2]))
                leftBarSpeedMultiplier = 3;
            else
                leftBarSpeedMultiplier = 0;

            if (keyBoardstate.IsKeyDown(leftBarKeys[0]))
                if (!leftBar.Intersects(topBorder))
                    leftBar.Offset(0, (-leftBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - leftBarSpeedMultiplier);

            if (keyBoardstate.IsKeyDown(leftBarKeys[1]))
                if (!leftBar.Intersects(bottomBorder))
                    leftBar.Offset(0, (leftBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + leftBarSpeedMultiplier);

            //Right bar controls
            if (keyBoardstate.IsKeyDown(rightBarKeys[2]))
                rightBarSpeedMultiplier = 3;
            else
                rightBarSpeedMultiplier = 0;

            if (keyBoardstate.IsKeyDown(rightBarKeys[0]))
                if (!rightBar.Intersects(topBorder))
                    rightBar.Offset(0, (-rightBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - rightBarSpeedMultiplier);

            if (keyBoardstate.IsKeyDown(rightBarKeys[1]))
                if (!rightBar.Intersects(bottomBorder))
                    rightBar.Offset(0, (rightBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + rightBarSpeedMultiplier);

        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            //ball
            spriteBatch.Draw(spriteTexture, ball, Color.White);
            //bars
            spriteBatch.Draw(spriteTexture, topBar, Color.White);
            spriteBatch.Draw(spriteTexture, bottomBar, Color.White);
            spriteBatch.Draw(spriteTexture, leftBar, Color.White);
            spriteBatch.Draw(spriteTexture, rightBar, Color.White);
            //borders
            spriteBatch.Draw(topBorderTexture, topBorder, Color.White);
            spriteBatch.Draw(bottomBorderTexture, bottomBorder, Color.White);
            spriteBatch.Draw(leftBorderTexture, leftBorder, Color.White);
            spriteBatch.Draw(rightBorderTexture, rightBorder, Color.White);
            //font
            spriteBatch.DrawString(font, topPlayerLives.ToString(),new Vector2(390, 50), Color.White);
            spriteBatch.DrawString(font, bottomPlayerLives.ToString(), new Vector2(390, 710), Color.White);
            spriteBatch.DrawString(font, leftPlayerLives.ToString(), new Vector2(70, 390), Color.White);
            spriteBatch.DrawString(font, rightPlayerLives.ToString(), new Vector2(700, 390), Color.White);
            //powerup
            foreach(Powerup powerup in powerup)
            powerup.drawPowerup();

            spriteBatch.End();

            base.Draw(gameTime);
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
            if(ballSpeed<ballSpeedLimit)
                ballSpeed += ballSpeedInc;
            Console.WriteLine(ballSpeed);
            int barCenter;
            
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
                        multiplier = (1-((-offset) / maxOffset) *bounceCorrection );
                        ballYVelocity = multiplier * ballSpeed;
                        ballXVelocity = (float)Math.Sqrt((double)(ballSpeed * ballSpeed - ballYVelocity * ballYVelocity)); 
                    }
                    //ball bounces left
                    else if (offset > 0)
                    {
                        multiplier = (1-(offset / maxOffset) * bounceCorrection);
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

        protected void ResetBall(Rectangle player)
        {
            if(player == topBar)
            {
                topPlayerLives = topPlayerLives - 1;

                if (topPlayerLives == 0)
                {
                    topBorderTexture = Content.Load<Texture2D>("deadBar.png");
                    topBar.Offset(-800, -800);
                }
            }

           else if (player == bottomBar)
            {
                bottomPlayerLives = bottomPlayerLives - 1;

                if (bottomPlayerLives == 0)
                {
                    bottomBorderTexture = Content.Load<Texture2D>("deadBar.png");
                    bottomBar.Offset(-800, -800);
                }
            }

            else if (player == leftBar)
            {
                leftPlayerLives = leftPlayerLives - 1;

                if (leftPlayerLives == 0)
                {
                    leftBorderTexture = Content.Load<Texture2D>("deadBar.png");
                    leftBar.Offset(-800, -800);
                }
            }

            else if (player == rightBar)
            {
                rightPlayerLives = rightPlayerLives - 1;
                

                if (rightPlayerLives == 0)
                {
                    rightBorderTexture = Content.Load<Texture2D>("deadBar.png");
                    rightBar.Offset(-800, -800);
                }
            }
           // ballSpeed = 500.0f;
            ball.Offset((-ball.X + ballStartPos), (-ball.Y + ballStartPos));
            pause = true;
            

        }
      
        public void pauseBall(GameTime gameTime)
        {
            Console.WriteLine("PAUSE!");
            ballXVelocity = ballYVelocity = ballSpeed = 0;
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                pauseTimer++;
                switch(pauseTimer)
                {
                    case 2:
                        ballXVelocity = ballYVelocity = ballSpeed = 500;
                        pauseTimer = 0;
                        pause = false;
                        spawnBallDirection();

                        break;
                    default:
                        break;
                }
            }
        }

        private void spawnBallDirection()
        {
            int[] playersLives = new int[4];
            playersLives[0] = topPlayerLives;
            playersLives[1] = bottomPlayerLives;
            playersLives[2] = leftPlayerLives;
            playersLives[3] = rightPlayerLives;

            int[] playersAlive = new int[4];
            int chosenPlayer = 0;
            int death = 0;

            for (int i = 0; i < playersLives.Length; i++)
            {
                if (playersLives[i] > 0)
                {
                    playersAlive[i] = i;
                }
                else
                {
                    playersAlive[i] = -1;
                    death++;
                }
            }

            foreach(int p in playersAlive)
            Console.WriteLine(p);
            int hussle1;
            int hussle2;
            int temp;

            for(int i=0;i<2;i++)
            {
                hussle1 = random.Next(1, 4);
                hussle2 = random.Next(1, 4);
                temp = playersAlive[hussle1];
                playersAlive[hussle1] = playersAlive[hussle2];
                playersAlive[hussle2] = temp;
            }
            
            foreach(int player in playersAlive)
            {
                if (player != -1 && player != lastSpawnedDirection)
                {
                    chosenPlayer = player;
                    break;
                }
            }

            if (death == 3)
                chosenPlayer = 4;

            switch (chosenPlayer)
            {
                case 0:
                    lastSpawnedDirection = 0;
                    ballXVelocity = 0;
                    ballYVelocity = -ballSpeed;
                    break;
                case 1:
                    lastSpawnedDirection = 1;
                    ballXVelocity = 0;
                    ballYVelocity = ballSpeed;
                    break;
                case 2:
                    lastSpawnedDirection = 2;
                    ballXVelocity = -ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 3:
                    lastSpawnedDirection = 3;
                    ballXVelocity = ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 4:
                    ballXVelocity = ballYVelocity = ballSpeed = 0;
                    foreach (Powerup powerup in powerup)
                        powerup.disable();
                    break;
                default:
                    break;
            }

            
        }

    }
}
