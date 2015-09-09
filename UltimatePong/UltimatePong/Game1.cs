using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 ballPosition;
        Vector2 topBarPosition;
        Vector2 bottomBarPosition;

        Texture2D ball;
        Texture2D topBar;
        Texture2D bottomBar;

        float ballXPos;
        float ballYPos;

        float topBarXPos;
        float bottomBarXPos;

        int barSpeed;
        int ballSpeed;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Add your initialization logic here
            topBar = Content.Load<Texture2D>("bar.png");
            bottomBar = Content.Load<Texture2D>("bar.png");
            ball = Content.Load<Texture2D>("ball1.png");

            base.Window.AllowUserResizing = false;

            ballSpeed = 10;
            barSpeed = 5;

            ballXPos = (GraphicsDevice.Viewport.Bounds.Width - ball.Width) / 2;
            ballYPos = (GraphicsDevice.Viewport.Bounds.Height - ball.Height) / 2;

            topBarXPos = (GraphicsDevice.Viewport.Bounds.Width - topBar.Width) / 2;
            bottomBarXPos = (GraphicsDevice.Viewport.Bounds.Width - bottomBar.Width) / 2;
            base.Initialize();
        }


  
        protected override void Update(GameTime gameTime)
        {

            var keyBoardstate = Keyboard.GetState();
            var prev_topBarXPos = topBarXPos;
            var prev_bottomBarXPos = bottomBarXPos;
            var prev_ballXPos = ballXPos;
            var prev_ballYPos = ballYPos;

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controls
            if (keyBoardstate.IsKeyDown(Keys.W))
                ballYPos = ballYPos - ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.S))
                ballYPos = ballYPos + ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.A))
                ballXPos = ballXPos - ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.D))
                ballXPos = ballXPos + ballSpeed;

            //Top bar controls
            if (keyBoardstate.IsKeyDown(Keys.Left))
                topBarXPos = topBarXPos - barSpeed;
            if (keyBoardstate.IsKeyDown(Keys.Right))
                topBarXPos = topBarXPos + barSpeed;
            //Bottom bar controls
            if (keyBoardstate.IsKeyDown(Keys.End))
                bottomBarXPos = bottomBarXPos - barSpeed;
            if (keyBoardstate.IsKeyDown(Keys.PageDown))
                bottomBarXPos = bottomBarXPos + barSpeed;


            //bar boundries
            /* if (xbarpos <= 0)
                 xbarpos = 0;
             if (xbarpos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                 xbarpos = GraphicsDevice.Viewport.Bounds.Width - bar.Width;*/
            if (topBarXPos <= 0 || topBarXPos > GraphicsDevice.Viewport.Bounds.Width - topBar.Width)
                topBarXPos = prev_topBarXPos;

            /* if (xbar2pos <= 0)
                 xbar2pos = 0;
             if (xbar2pos > GraphicsDevice.Viewport.Bounds.Width - bar2.Width)
                 xbar2pos = GraphicsDevice.Viewport.Bounds.Width - bar2.Width;*/
            if (bottomBarXPos <= 0 || bottomBarXPos > GraphicsDevice.Viewport.Bounds.Width - bottomBar.Width)
                bottomBarXPos = prev_bottomBarXPos;

            //ball boundries
            /* if (xpos <= 0)
                 xpos = 0;
             if (xpos >= GraphicsDevice.Viewport.Bounds.Width -ball.Width)
                 xpos = GraphicsDevice.Viewport.Bounds.Width - ball.Width;
             if (ypos < 0)
                 ypos = 0;
             if (ypos > GraphicsDevice.Viewport.Bounds.Height-ball.Height)
                 ypos = GraphicsDevice.Viewport.Bounds.Height-ball.Height;*/
            if (ballXPos <= 0 || ballXPos >= GraphicsDevice.Viewport.Bounds.Width - ball.Width || ballYPos < 0 || ballYPos > GraphicsDevice.Viewport.Bounds.Height - ball.Height)
            {
                ballXPos = prev_ballXPos;
                ballYPos = prev_ballYPos;
            }


            ballPosition = new Vector2(ballXPos, ballYPos);
            topBarPosition = new Vector2(topBarXPos, 0.0f);
            bottomBarPosition = new Vector2(bottomBarXPos, GraphicsDevice.Viewport.Bounds.Height - bottomBar.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(topBar, topBarPosition, Color.White);
            spriteBatch.Draw(bottomBar, bottomBarPosition, Color.White);
            spriteBatch.Draw(ball, ballPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
