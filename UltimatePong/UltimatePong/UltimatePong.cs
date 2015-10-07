using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

namespace UltimatePong
{
    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random(DateTime.Now.Millisecond+ DateTime.Now.Second);

        //sprites
        Texture2D spriteTexture;

        Texture2D barTexture;
        Texture2D borderTexture;

        SoundEffect bleepHigh;
        SoundEffect bleepLow;

        //LISTS
        List<Ball> balls;
        List<Entity> playerBars;
        //Entity testBar;
        
        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWidth = 5;

        //ball properties
        float ballSpeed = 400.0f;
        float ballSpeedLimit = 80000.0f;
        float ballSpeedInc = 20.0f;
        int ballSize = 16;
        int lastSpawnedDirection;
        //pause
        int pauseTimer;
        int gameTime;

        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;
        const float bounceCorrection = 0.6f;

        //top bar properties
        Keys[] topBarKeys = new Keys[3];

        //bottom bar properties
        Keys[] bottomBarKeys = new Keys[3];

        //left bar properties
        Keys[] leftBarKeys = new Keys[3];

        //right bar properties
        Keys[] rightBarKeys = new Keys[3];

        //Player lives
        int[] playerLives;

        //Game Settings
        public int players;
        public int lives;
        public bool powerups;
        public bool classicBounce;
        private bool firstCycle;


        Powerup[] powerup;
        int powerupCount;

        SpriteFont font;

        InputManagement input;
        //test
        Keys[,] controls = new Keys[4,3];

        //Border border;
        Border[] borders;
        //playerBars
        //Bar[] playerBars;
        int lastHitBar;

        public UltimatePong(int playerAmount, int livesAmount, bool powerups, bool bounceType)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";

            this.players = playerAmount;
            this.lives = livesAmount;
            this.powerups = powerups;
            this.classicBounce = bounceType;

            //Printlines
            System.Console.WriteLine("players:" + players);
            System.Console.WriteLine("lives:" + lives);
            System.Console.WriteLine("powerups:" + powerups);
            System.Console.WriteLine("bounceType:" + bounceType);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bleepHigh = Content.Load<SoundEffect>("bleep-high");
            bleepLow = Content.Load<SoundEffect>("bleep-low");
            //Ball texture
            spriteTexture = Content.Load<Texture2D>("ball1.png");
            //Border textures
            borderTexture = Content.Load<Texture2D>("bar.png");
            //Player Textures
            barTexture = Content.Load<Texture2D>("ball1.png");
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

            controls[0, 0] = topBarKeys[0];
            controls[0, 1] = topBarKeys[1];
            controls[0, 2] = topBarKeys[2];

            controls[1, 0] = bottomBarKeys[0];
            controls[1, 1] = bottomBarKeys[1];
            controls[1, 2] = bottomBarKeys[2];

            controls[2, 0] = leftBarKeys[0];
            controls[2, 1] = leftBarKeys[1];
            controls[2, 2] = leftBarKeys[2];

            controls[3, 0] = rightBarKeys[0];
            controls[3, 1] = rightBarKeys[1];
            controls[3, 2] = rightBarKeys[2];


            //initialize ball
            balls = new List<Ball>();
            balls.Insert(0, new Ball(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, false, spriteBatch, spriteTexture));
            firstCycle = true;

            int barStartPos = (fieldSize - barLength) / 2;

            //bar start pos
            Point topBarPos = new Point(barStartPos, barToBorderDist);
            Point botBarPos = new Point(barStartPos, fieldSize - barToBorderDist - barWidth);
            Point leftBarPos = new Point(barToBorderDist, barStartPos);
            Point rightBarPos = new Point(fieldSize - barToBorderDist - barWidth, barStartPos);
            if (players != 4)
                topBarPos = new Point(-100, -100);
            if (players == 2)
                botBarPos = new Point(-100, -100);

            //Initialize Bar list
            playerBars = new List<Entity>();
            Entity topBar = new Entity(barTexture, new Rectangle(), 128, 8, topBarPos);
            Entity botBar = new Entity(barTexture, new Rectangle(), 128, 8, botBarPos);
            Entity leftBar = new Entity(barTexture, new Rectangle(), 8, 128, leftBarPos);
            Entity rightBar = new Entity(barTexture, new Rectangle(), 8, 128, rightBarPos);

            playerBars.Insert(0, topBar);
            playerBars.Insert(1, botBar);
            playerBars.Insert(2, leftBar);
            playerBars.Insert(3, rightBar);

            //initialize player lives
            playerLives = new int[4] {lives,lives,lives,lives}; 

            //powerup
            powerup = new Powerup[3];
            powerupCount = 0;
            for(int i=0;i<powerup.Length;i++)
            powerup[i] = new Powerup(spriteBatch, spriteTexture, GraphicsDevice,i);

            //Inititialize borders
            borders = new Border[4];
            borders[0] = new Border(spriteBatch, borderTexture, 0, 0, "Lying");//Top border
            borders[1] = new Border(spriteBatch, borderTexture, 0, (fieldSize - borderWidth), "Lying");//Bottom border
            borders[2] = new Border(spriteBatch, borderTexture, 0, 0, "Standing");//Left border
            borders[3] = new Border(spriteBatch, borderTexture, (fieldSize - borderWidth), 0, "Standing");//Right border

            //Initialize input
            input = new InputManagement(controls);
            gameTime = 0;

            base.Initialize();
        }
        
        private void setPlayersAmount()
        {
            switch(players)
            {
                case 2:
                    borders[0].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    borders[1].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerLives[0] = 0;
                    playerLives[1] = 0;
                    break;
                case 3:
                    borders[0].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerLives[0] = 0;
                    break;
                default:
                    break;
            }
        }

        protected void firstSpawnBall(GameTime gameTime)
        {
            int direction = 0;
            switch(players)
            {
                case 2:
                    direction = random.Next(2, 4);
                    break;
                case 3:
                    direction = random.Next(1, 4);
                    break;
                case 4:
                    direction = random.Next(0, 4);
                    break;
            }
            lastSpawnedDirection = direction;
            balls[0].spawnBall(direction, ballSpeed, gameTime);
            firstCycle = false;
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (firstCycle)
            {
                setPlayersAmount();
                firstSpawnBall(gameTime);
            }
            

            //input.PlayerMovement();
            input.Update(deltaTime);
            if (input.quit)
            {
                Ball newBall = new Ball(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, false, spriteBatch, spriteTexture);

                balls.Add(newBall);
                newBall.spawnBall(spawnBallDirection(), ballSpeed, gameTime);

            }

            //BAR ENTITIES
            List<Entity> tempBars = new List<Entity>();
            //tempBars.Insert(0, playerBars.ElementAt(0).CreateMoved(input.TopBar));
            //tempBars.Insert(1, playerBars.ElementAt(1).CreateMoved(input.BotBar));
            //tempBars.Insert(2, playerBars.ElementAt(2).CreateMoved(input.LeftBar));
            //tempBars.Insert(3, playerBars.ElementAt(3).CreateMoved(input.RightBar));

            tempBars.Insert(0, playerBars[0].CreateMoved(input.TopBar));
            tempBars.Insert(1, playerBars[1].CreateMoved(input.BotBar));
            tempBars.Insert(2, playerBars[2].CreateMoved(input.LeftBar));
            tempBars.Insert(3, playerBars[3].CreateMoved(input.RightBar));
            
            //COMMIT CHANGES
            playerBars = tempBars;

            // Collision detection and ball movement

            //creation of a new list of balls
            List<Ball> updatedBalls = balls;
            List<Ball> ballsToRemove = new List<Ball>();
            foreach (Ball ball in updatedBalls)
            {
                int playerLostALife = ball.updateBall(gameTime, playerBars, borders, playerLives);
                if(playerLostALife>-1)
                {
                    playerLives[playerLostALife] -= 1;
                    if (updatedBalls.Count > 1)
                    {
                        ballsToRemove.Add(ball);
                    }
                    else
                    {
                        ball.spawnBall(spawnBallDirection(), 400.0f, gameTime);
                        foreach (int i in playerLives)
                            System.Console.WriteLine("Playerlive: "+ i);
                    }
                }
                
            }
            foreach (Ball ball in ballsToRemove)
                updatedBalls.Remove(ball);
                
            //move bars
            //foreach (Bar bar in playerBars)
              //  bar.updateBar();
            //borders
            foreach (Border border in borders)
                border.updateBorder();

            //Power-ups
            if(powerups)
            powerupEvents(gameTime);


            //update Entities
            balls = updatedBalls;

            base.Update(gameTime);
        }

        public void powerupEvents(GameTime gameTime)
        {
            if (powerupCount > 2)
                powerupCount = 0;

            powerup[powerupCount].startTimer(gameTime);
            for (int i = 0; i < playerBars.Count; i++)
                if (balls[0].ball.Intersects(playerBars[i].rectangle))
                {
                    lastHitBar = i;
                    break;
                }
            //powerup[powerupCount].checkCollision(ref balls[0].ball, ref playerBars, lastHitBar,ref playerLives,ref balls[0].ballXVelocity,ref balls[0].ballYVelocity);
            powerupCount++;
        }
        /*
        private void checkInput(GameTime gameTime)
        {
            var keyBoardstate = Keyboard.GetState();
            foreach (Bar bar in playerBars)
                bar.moveBar(gameTime);
            

        }*/

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            spriteBatch.Begin();
            //balls
            foreach (Ball ball in balls)
                ball.drawBall();
            //bars
            //foreach (Bar bar in playerBars)
              //  bar.DrawBar();
            //borders
            foreach (Border border in borders)
                border.DrawBorder();
            //font
            if (players==4)
                spriteBatch.DrawString(font, playerLives[0].ToString(),new Vector2(390, 50), Color.White);
            if(players!=2)
                spriteBatch.DrawString(font, playerLives[1].ToString(), new Vector2(390, 710), Color.White);
                spriteBatch.DrawString(font, playerLives[2].ToString(), new Vector2(70, 383), Color.White);
                spriteBatch.DrawString(font, playerLives[3].ToString(), new Vector2(700, 383), Color.White);
            //powerup
            foreach(Powerup powerup in powerup)
                powerup.drawPowerup();
            //entities bar
            foreach(Entity bar in playerBars)
              spriteBatch.Draw(barTexture, bar.rectangle, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        private int spawnBallDirection()
        {
            int[] playersAlive = new int[4];
            int chosenPlayer = 0;
            int death = 0;

            for (int i = 0; i < playerLives.Length; i++)
            {
                if (playerLives[i] > 0)
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

            for(int i=0;i<3;i++)
            {
                hussle1 = random.Next(0, 4);
                hussle2 = random.Next(0, 4);
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

            if (death==3)
                chosenPlayer = 4;

            lastSpawnedDirection = chosenPlayer;
            if(chosenPlayer == 4)
                foreach (Powerup powerup in powerup)
                    powerup.disabled(true);

            if(chosenPlayer!=4)
                foreach (Powerup powerup in powerup)
                    powerup.disabled(false);
            return chosenPlayer;
        }
    }
}
