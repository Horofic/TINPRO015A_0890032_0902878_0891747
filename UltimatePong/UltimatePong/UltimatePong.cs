using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //sprites
        Texture2D spriteTexture;
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
        float ballXVelocity;
        float ballYVelocity;
        int ballSize;
        bool collision;

        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;


        //top bar properties
        float topBarSpeed;
        int topBarLength;


        //bottom bar properties
        float bottomBarSpeed;
        int bottomBarLength;


        //left bar properties
        float leftBarSpeed;
        int leftBarLength;


        //right bar properties
        float rightBarSpeed;
        int rightBarLength;

        //Player lives
        int topPlayerLives;
        int bottomPlayerLives;
        int leftPlayerLives;
        int rightPlayerLives;

        private int players;
        private int lives;
        private bool powerups;
        

        SpriteFont font;

        public UltimatePong()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";
            
        }

        public void setGameSettings(int players, int lives, bool powerups)
        {
            this.players = players;
            this.lives = lives;
            this.powerups = powerups;
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteTexture = Content.Load<Texture2D>("bar.png");

            base.Window.AllowUserResizing = false;



            //initialize bar properties
            topBarSpeed = barSpeed;
            bottomBarSpeed = barSpeed;
            leftBarSpeed = barSpeed;
            rightBarSpeed = barSpeed;

            topBarLength = barLength;
            bottomBarLength = barLength;
            leftBarLength = barLength;
            rightBarSpeed = barLength;


            //initialize ball
            ballSize = 16;
            ballSpeed = 100.0f;
            ballXVelocity = 200.0f;
            ballYVelocity = -300.0f;
            collision = false;
            int ballStartPos = (fieldSize - ballSize) / 2;
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


            //initialize lives for now 3
            topPlayerLives = bottomPlayerLives = leftPlayerLives = rightPlayerLives = 3;

            base.Initialize();
        }


  
        protected override void Update(GameTime gameTime)
        {

            var keyBoardstate = Keyboard.GetState();
                      

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();

      
            //ball controls, just for testing
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                ball.Offset(0, -ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                ball.Offset(0, ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                ball.Offset(-ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                ball.Offset(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            //Top bar controls
            if (keyBoardstate.IsKeyDown(Keys.Z))
                topBar.Offset(-topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (keyBoardstate.IsKeyDown(Keys.X))
                topBar.Offset(topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);



            //Bottom bar controls
            if (keyBoardstate.IsKeyDown(Keys.Left))
                bottomBar.Offset(-bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (keyBoardstate.IsKeyDown(Keys.Right))
                bottomBar.Offset(bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            // Collision detection and ball movement
            checkBallCollision(gameTime);




            base.Update(gameTime);
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
            spriteBatch.Draw(spriteTexture, topBorder, Color.Black);
            spriteBatch.Draw(spriteTexture, bottomBorder, Color.Black);
            spriteBatch.Draw(spriteTexture, leftBorder, Color.Black);
            spriteBatch.Draw(spriteTexture, rightBorder, Color.Black);


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
        

           
        }

        protected void ResetBall(Rectangle player)
        {
            if(player == topBar)
            {
                topPlayerLives = topPlayerLives - 1;

                if (topPlayerLives == 0)
                {
                    //TODO make borders collidable
                    topBar.Offset(-800, -800);
                }
            }

           else if (player == bottomBar)
            {
                bottomPlayerLives = bottomPlayerLives - 1;

                if (bottomPlayerLives == 0)
                {
                    //TODO make borders collidable
                    bottomBar.Offset(-800, -800);
                }
            }

            else if (player == leftBar)
            {
                leftPlayerLives = leftPlayerLives - 1;

                if (leftPlayerLives == 0)
                {
                    //TODO make borders collidable
                    leftBar.Offset(-800, -800);
                }
            }

            else if (player == rightBar)
            {
                rightPlayerLives = rightPlayerLives - 1;

                if (rightPlayerLives == 0)
                {
                    //TODO make borders collidable
                    rightBar.Offset(-800, -800);
                }
            }

            ball.Offset((-ball.X + ((fieldSize - ball.Width) / 2)), (-ball.Y + ((fieldSize - ball.Width) / 2)));
        }
    }
}
