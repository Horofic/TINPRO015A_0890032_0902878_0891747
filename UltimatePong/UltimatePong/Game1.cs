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
        Texture2D ball;
        float xpos;
        float ypos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            xpos = 300.0f;
            ypos = 400.0f;
            ballPosition = new Vector2(xpos, ypos);
            ball = Content.Load<Texture2D>("ball1.png");
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                ypos = ypos - 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                ypos = ypos + 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                xpos = xpos - 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                xpos = xpos + 10;

            ballPosition = new Vector2(xpos, ypos);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(ball, ballPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
