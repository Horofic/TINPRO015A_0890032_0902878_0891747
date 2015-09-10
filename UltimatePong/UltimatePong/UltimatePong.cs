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

        
        Rectangle emptyRectangle;
        

        

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

        int players;
        int lives;
        bool powerups;
        

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
            emptyRectangle = new Rectangle(0, 0, 0, 0);
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
            ballXVelocity = 0.0f;
            ballYVelocity = -200.0f;
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



            //ball logic


            Rectangle collidedRectangle = checkBallCollision();
            if (ball.Intersects(topBar))
            {
                if (collision==false)
                {
                    ballYVelocity = -ballYVelocity;
                    collision = true;
                }
                else
                {
                    ball.Offset((ballXVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds), (ballYVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

                }
            }
            else
            {
                ball.Offset((ballXVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds), (ballYVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
                collision = false;
            }

            //ball controls, just for testing
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                ball.Offset(0, -ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                ball.Offset(0, ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                ball.Offset(-ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                ball.Offset(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            //ball movemment


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

            // Border collision detection
            BorderCollision(ball);



            /*
            //bar boundries

            if (topBarXPos <= 0 || topBarXPos > GraphicsDevice.Viewport.Bounds.Width - topBar.Width)
                topBarXPos = prev_topBarXPos;

    
            if (bottomBarXPos <= 0 || bottomBarXPos > GraphicsDevice.Viewport.Bounds.Width - bottomBar.Width)
                bottomBarXPos = prev_bottomBarXPos;

            //ball boundries
          
            if (ballXPos <= 0 || ballXPos >= GraphicsDevice.Viewport.Bounds.Width - ball.Width || ballYPos < 0 || ballYPos > GraphicsDevice.Viewport.Bounds.Height - ball.Height)
            {
                ballXPos = prev_ballXPos;
                ballYPos = prev_ballYPos;
            }


            ballPosition = new Vector2(ballXPos, ballYPos);
            topBarPosition = new Vector2(topBarXPos, 0.0f);
            bottomBarPosition = new Vector2(bottomBarXPos, GraphicsDevice.Viewport.Bounds.Height - bottomBar.Height);

    */
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

        protected void BorderCollision(Rectangle checkBall)
        {
            if (checkBall.Intersects(topBorder))
            {
                System.Console.WriteLine("Top border hit");

                topPlayerLives = topPlayerLives- 1;

                if (topPlayerLives == 0)
                {
                    topBar.Offset(800, 800);
                }

                ResetBall();
            }

            if (checkBall.Intersects(rightBorder))
            {
                System.Console.WriteLine("Right border hit");
                ResetBall();
            }

            if (checkBall.Intersects(leftBorder))
            {
                System.Console.WriteLine("Left border hit");
                ResetBall();
            }

            if (checkBall.Intersects(bottomBorder))
            {
                System.Console.WriteLine("Bottom border hit");
                ResetBall();
            }
        }


        protected void ResetBall()
        {
            ball.Offset((-ball.X + ((fieldSize - ball.Width) / 2)), (-ball.Y + ((fieldSize - ball.Width) / 2)));
        }
    }
}
