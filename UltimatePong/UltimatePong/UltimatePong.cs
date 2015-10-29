using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

namespace UltimatePong
{

    enum BallMovementInstructionResult
    {
        Running,
        RunningAndPowerupHit,
        OutOfBounds,
        DoneAndPlayer0LostALife,
        DoneAndPlayer1LostALife,
        DoneAndPlayer2LostALife,
        DoneAndPlayer3LostALife
    }
    enum PowerupResponse
    {
        done,
        addBall,
        changeBallDirection
    }

    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random(DateTime.Now.Millisecond+ DateTime.Now.Second);
        SpriteFont fontPlayerLives;
        SpriteFont fontVictoryScreen;

        //sprites
        Texture2D spriteTexture;
        Texture2D barTexture;
        Texture2D borderTexture;

        //Sounds
        SoundEffect bleepHigh;
        SoundEffect bleepLow;

        //LISTS
        List<Entity> borders;
        List<BallController> balls;
        List<Entity> playerBars;
        List<PowerupController> powerupController;
        
        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWidth = 5;

        //ball properties
        float ballSpeed = 500.0f;
        float ballSpeedLimit = 80000.0f;
        float ballSpeedInc = 20.0f;
        int ballSize = 16;
        int lastSpawnedDirection;

        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;
        const float bounceCorrection = 0.6f;

        //Player lives
        int[] playerLives;

        //Game Settings
        public int players;
        public int lives;
        public bool powerupEnabled;
        private bool firstCycle;
        private bool gameDone;

        //powerup
        Power_up power_up = new Power_up();

        //input
        InputManagement input;

        //color array
        Color[] colorArray = {Color.White,Color.TransparentBlack,Color.Green,Color.Red,Color.Goldenrod };

        //Victory screen values
        int[] selectionLocation = {130, 500, 250, 600};
        int selection = 0;
        double selectionTimer = 0;
        public bool restart = false;

        public UltimatePong(int playerAmount, int livesAmount, bool powerup)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";

            this.players = playerAmount;
            this.lives = livesAmount;
            this.powerupEnabled = powerup;
            this.gameDone = false;

            //Printlines
            System.Console.WriteLine("players:" + players);
            System.Console.WriteLine("lives:" + lives);
            System.Console.WriteLine("powerups:" + powerupEnabled);
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
            fontPlayerLives = Content.Load<SpriteFont>("font");
            fontVictoryScreen = Content.Load<SpriteFont>("fontVictoryScreen");

            base.Window.AllowUserResizing = false;

            //initialize ball
            balls = new List<BallController>();
            balls.Insert(0, new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture, bleepHigh, bleepLow));
            firstCycle = true;
            int barStartPos = (fieldSize - barLength) / 2;

            //bar start pos
            Point topBarPos = new Point(barStartPos, barToBorderDist);
            Point botBarPos = new Point(barStartPos, fieldSize - barToBorderDist - barWidth);
            Point leftBarPos = new Point(barToBorderDist, barStartPos);
            Point rightBarPos = new Point(fieldSize - barToBorderDist - barWidth, barStartPos);

            //Initialize Bar list
            playerBars = new List<Entity>();
            playerBars.Insert(0, new Entity(barTexture, new Rectangle(), 128, 8, topBarPos)); //Top Bar
            playerBars.Insert(1, new Entity(barTexture, new Rectangle(), 128, 8, botBarPos)); //Bot Bar
            playerBars.Insert(2, new Entity(barTexture, new Rectangle(), 8, 128, leftBarPos)); //Left Bar
            playerBars.Insert(3, new Entity(barTexture, new Rectangle(), 8, 128, rightBarPos)); //Right Bar

            //initialize player lives
            playerLives = new int[4] {lives,lives,lives,lives};

            //Initialize powerups
            powerupController = new List<PowerupController>();
            powerupController.Insert(0, new GreenPowerupController(barTexture,spriteBatch));
            powerupController.Insert(1, new RedPowerupController(barTexture, spriteBatch));
            powerupController.Insert(2, new GoldPowerupController(barTexture, spriteBatch));
            powerupController.Insert(3, new PinkPowerupController(barTexture, spriteBatch));
            powerupController.Insert(4, new BluePowerupController(barTexture, spriteBatch));


            //Inititialize borders
            borders = new List<Entity>();
            borders.Insert(0,new Entity(borderTexture,  new Rectangle(), fieldSize, borderWidth, new Point(0, 0))); //Top Border
            borders.Insert(1, new Entity(borderTexture, new Rectangle(), fieldSize, borderWidth, new Point(0, fieldSize - borderWidth))); //Bot Border
            borders.Insert(2, new Entity(borderTexture, new Rectangle(), borderWidth, fieldSize, new Point(0, 0))); //Left Border
            borders.Insert(3, new Entity(borderTexture, new Rectangle(), borderWidth, fieldSize, new Point(fieldSize - borderWidth, 0))); //Right Border

            //Initialize input
            input = new InputManagement();

            Console.WriteLine("initialize");
            base.Initialize();
        }
        
        private void setPlayersAmount()
        {
            List<Entity> temp = playerBars;
            switch(players)
            {
                case 2:
                    playerLives[0] = 0;
                    playerLives[1] = 0;
                    temp.Insert(0,playerBars[0].CreateNewPos(new Point(-100,-100)));
                    temp.Insert(1, playerBars[1].CreateNewPos(new Point(-100, -100)));
                    temp.RemoveAt(2);
                    temp.RemoveAt(2);
                    break;
                case 3:
                    playerLives[0] = 0;
                    temp.Insert(0, playerBars[0].CreateNewPos(new Point(-100, -100)));
                    temp.RemoveAt(1);
                    break;
                default:
                    break;
            }
            playerBars = temp;
        }

        protected void firstSpawnBall(float deltaTime)
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
            balls[0].spawnBall(direction, deltaTime);
            firstCycle = false;
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            double elapsedTime = gameTime.TotalGameTime.TotalMilliseconds;

            if (firstCycle)
            {
                setPlayersAmount();
                firstSpawnBall(deltaTime);
            }
    
            input.Update(deltaTime);
            if (input.newBall)
            {
                BallController newBall = new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture,bleepHigh,bleepLow);

                balls.Add(newBall);
                newBall.spawnBall(spawnBallDirection(), elapsedTime);
            }



            //BAR ENTITIES to be moved
            List<Entity> tempBars = new List<Entity>();
            for(int i=0;i<4;i++)
                if(tempBars)
                tempBars.Insert(i, playerBars[i].CreateMoved(input.moveBar(i, i)));








            //creation of a new list of balls
            List<BallController> updatedBalls = new List<BallController>();

            foreach (BallController ball in balls)
            {
                switch (ball.updateBall(deltaTime, elapsedTime, playerBars, borders, powerupController, playerLives))
                {
                    case BallMovementInstructionResult.Running:
                        updatedBalls.Add(ball);
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer0LostALife:
                        playerLives[0] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer1LostALife:
                        playerLives[1] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer2LostALife:
                        playerLives[2] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer3LostALife:
                        playerLives[3] -= 1;
                        break;
                    case BallMovementInstructionResult.RunningAndPowerupHit:

                        if (ball.lastHitPlayer != -1)
                        {
                            switch (ball.hitPowerup.powerupEvent(ball.lastHitPlayer, ref tempBars, ref playerLives))
                            {
                                case PowerupResponse.addBall:
                                    BallController tmpBallController = new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture, bleepHigh, bleepLow);
                                    tmpBallController.spawnBall(spawnBallDirection(), elapsedTime);
                                    updatedBalls.Add(tmpBallController);
                                    break;
                                case PowerupResponse.changeBallDirection:
                                    ball.simpleBounce(random.Next(0, 3));
                                    break;
                                default:
                                    break;
                            }
                        }
                        
                        
                        updatedBalls.Add(ball);
                        powerupController.Remove(ball.hitPowerup);
                        break;


                    case BallMovementInstructionResult.OutOfBounds:

                    default:
                        break;

                }

            }
            int playersAlive = 0;
            for (int i = 0; i < 4; i++)
            {
                if (playerLives[i] > 0)
                    playersAlive++;
            }

            if (playersAlive < 2)
            {
                updatedBalls.Clear();
                powerupController.Clear();
                gameDone = true;
            }
            else if (updatedBalls.Count < 1)
            {
                updatedBalls.Insert(0, new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture,bleepHigh,bleepLow));
                updatedBalls[0].spawnBall(spawnBallDirection(), elapsedTime);
            }
          
            //move player to void if dead
            for(int i=0;i<playerLives.Length;i++)
                if (playerLives[i] < 1)
                {
                    tempBars.Insert(i, playerBars[i].CreateNewPos(new Point(900, 900)));
                    tempBars.RemoveAt(i+1);
                }



            foreach(Entity player in playerBars)
            {
                if(player.rectangle.Y < 0)
                {
                    Console.WriteLine("COLLISION");
                }
            }

            
            foreach (Entity player in playerBars)
            {
                foreach (Entity border in borders)
                {
                    if (player.rectangle.Intersects(border.rectangle))
                    {
                        tempBars.Insert(tempBars.Count, player.CreateNewPos(new Point(player.rectangle.X, player.rectangle.Y+50)));
                        tempBars.RemoveAt(playerBars.IndexOf(player));
                        Console.WriteLine("Collision with border");
                    }
                }
            }
            

            //inputs for victory screen selection
            if (gameDone)
            {
                if (input.selection && gameTime.TotalGameTime.TotalMilliseconds - selectionTimer > 200)
                {
                    selectionTimer = gameTime.TotalGameTime.TotalMilliseconds;
                    selection += 2;
                    if (selection > 2)
                        selection = 0;
                }
                if(input.confirmation)
                {
                    if(selection == 0)
                    {
                        restart = true;
                        base.Exit();
                    }
                    else 
                        base.Exit();
                }
            }

            //update Entities
            balls = updatedBalls;
            playerBars = tempBars;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            spriteBatch.Begin();
            //balls
            foreach (BallController ball in balls)
                ball.drawBall();
            //font
            if (players==4)
                spriteBatch.DrawString(fontPlayerLives, playerLives[0].ToString(),new Vector2(390, 50), Color.White);
            if(players!=2)
                spriteBatch.DrawString(fontPlayerLives, playerLives[1].ToString(), new Vector2(390, 710), Color.White);
            spriteBatch.DrawString(fontPlayerLives, playerLives[2].ToString(), new Vector2(70, 383), Color.White);
            spriteBatch.DrawString(fontPlayerLives, playerLives[3].ToString(), new Vector2(700, 383), Color.White);
            //entities border
            for(int i=0;i<borders.Count;i++)
            {
                Color color;
                if (playerLives[i] != 0)
                    color = Color.TransparentBlack;
                else
                    color = Color.White;
                spriteBatch.Draw(barTexture, borders[i].rectangle, color);
            }
            //entities bar
            foreach (Entity bar in playerBars)
                spriteBatch.Draw(barTexture, bar.rectangle, Color.White);
            //powerups
            foreach (PowerupController powerup in powerupController)
                powerup.Draw();

            if(gameDone)
            {
                spriteBatch.DrawString(fontVictoryScreen, "Winner!", new Vector2(240, 200), Color.White);
                String[] winningPlayer = { "Player 4", "Player 3", "Player 1", "Player 2" };
                int win;
                for (win = 0; win < playerLives.Length; win++)
                    if (playerLives[win] > 0)
                        break;
                spriteBatch.DrawString(fontVictoryScreen, winningPlayer[win], new Vector2(218, 300), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, "Play Again", new Vector2(183, 500), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, "Quit", new Vector2(303, 600), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, ">", new Vector2(selectionLocation[selection], selectionLocation[selection + 1]), Color.White);
            }

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
            return chosenPlayer;
        }
    }
}
