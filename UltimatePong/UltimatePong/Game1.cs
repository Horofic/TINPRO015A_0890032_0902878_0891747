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
        Vector2 barPosition;
        Vector2 bar2Position;
        Texture2D ball;
        Texture2D bar;
        Texture2D bar2;

        float xpos;
        float ypos;

        float xbarPos;
        float xbar2Pos;
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
            // TODO: Add your initialization logic here
            bar = Content.Load<Texture2D>("bar.png");
            bar2 = Content.Load<Texture2D>("bar.png");
            ball = Content.Load<Texture2D>("ball1.png");

            base.Window.AllowUserResizing = false;

            ballSpeed = 10;
            barSpeed = 5;
            xpos = (GraphicsDevice.Viewport.Bounds.Width - ball.Width) / 2;
            ypos = (GraphicsDevice.Viewport.Bounds.Height - ball.Height) / 2;

            xbarPos = (GraphicsDevice.Viewport.Bounds.Width - bar.Width) / 2;
            xbar2Pos = (GraphicsDevice.Viewport.Bounds.Width - bar2.Width) / 2;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


        }


        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {

            var keyBoardstate = Keyboard.GetState();
            var prevX_barPos = xbarPos;
            var prevX_bar2Pos = xbar2Pos;
            var prevX_ballPos = xpos;
            var prevY_ballPos = ypos;

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controls
            if (keyBoardstate.IsKeyDown(Keys.W))
                ypos = ypos - ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.S))
                ypos = ypos + ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.A))
                xpos = xpos - ballSpeed;
            if (keyBoardstate.IsKeyDown(Keys.D))
                xpos = xpos + ballSpeed;

            //bar controls
            if (keyBoardstate.IsKeyDown(Keys.Left))
                xbarPos = xbarPos - barSpeed;
            if (keyBoardstate.IsKeyDown(Keys.Right))
                xbarPos = xbarPos + barSpeed;
            //bar2 controls
            if (keyBoardstate.IsKeyDown(Keys.End))
                xbar2Pos = xbar2Pos - barSpeed;
            if (keyBoardstate.IsKeyDown(Keys.PageDown))
                xbar2Pos = xbar2Pos + barSpeed;


            //bar boundries
            /* if (xbarpos <= 0)
                 xbarpos = 0;
             if (xbarpos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                 xbarpos = GraphicsDevice.Viewport.Bounds.Width - bar.Width;*/
            if (xbarPos <= 0 || xbarPos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                xbarPos = prevX_barPos;

            /* if (xbar2pos <= 0)
                 xbar2pos = 0;
             if (xbar2pos > GraphicsDevice.Viewport.Bounds.Width - bar2.Width)
                 xbar2pos = GraphicsDevice.Viewport.Bounds.Width - bar2.Width;*/
            if (xbar2Pos <= 0 || xbar2Pos > GraphicsDevice.Viewport.Bounds.Width - bar2.Width)
                xbar2Pos = prevX_bar2Pos;

            //ball boundries
            /* if (xpos <= 0)
                 xpos = 0;
             if (xpos >= GraphicsDevice.Viewport.Bounds.Width -ball.Width)
                 xpos = GraphicsDevice.Viewport.Bounds.Width - ball.Width;
             if (ypos < 0)
                 ypos = 0;
             if (ypos > GraphicsDevice.Viewport.Bounds.Height-ball.Height)
                 ypos = GraphicsDevice.Viewport.Bounds.Height-ball.Height;*/
            if (xpos <= 0 || xpos >= GraphicsDevice.Viewport.Bounds.Width - ball.Width || ypos < 0 || ypos > GraphicsDevice.Viewport.Bounds.Height - ball.Height)
            {
                xpos = prevX_ballPos;
                ypos = prevY_ballPos;
            }


            ballPosition = new Vector2(xpos, ypos);
            barPosition = new Vector2(xbarPos, 0.0f);
            bar2Position = new Vector2(xbar2Pos, GraphicsDevice.Viewport.Bounds.Height - bar.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(bar, barPosition, Color.White);
            spriteBatch.Draw(bar2, bar2Position, Color.White);
            spriteBatch.Draw(ball, ballPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
