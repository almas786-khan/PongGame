/* PongGame
 * Main Game class : Pong.cs
 * Revision History
 *      Almas Khan: 2016.11.15: Created
 *      Almas Khan: 2016.11.16: coded
 *      Almas Khan: 2016.11.16: Debugged
 *      
 */


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        // Class references declartions
        Ball ball;
        Bat batLeft;
        Bat batRight;
        ScoreBar scoreBar;
        Score1 score1;
        Score2 score2;
        Winner winner;
        CollissionManager cm1;
        CollissionManager cm2;

        //variable declarations
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D scoreBarTex;
        Vector2 dimension1;
        string rightBatName;
        string leftBatName;
        private Vector2 str1Pos;
        private Vector2 str2Pos;
        private Vector2 winnerPos;
        Vector2 batPos;
        private Vector2 strPos;
        private bool isPlay = false;
        private Vector2 batRPos;
        private Vector2 stage;
        private Texture2D batTex;
        private Texture2D batRTex;
        private SoundEffect winSound;

        /// <summary>
        /// Class default constructor
        /// </summary>
        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //All contents loaded used in program
            SoundEffect clickSound = Content.Load<SoundEffect>("sounds/click");
            SoundEffect dingSound = Content.Load<SoundEffect>("sounds/ding");
            winSound = Content.Load<SoundEffect>("sounds/applause1");
            font = Content.Load<SpriteFont>("fonts/myfont");
            scoreBarTex = Content.Load<Texture2D>("images/scorebar");
            Texture2D ballTex = Content.Load<Texture2D>("images/ball");
            batRTex = Content.Load<Texture2D>("Images/BatRight");
            batTex = Content.Load<Texture2D>("Images/BatLeft");

            string message = "";
            strPos = Vector2.Zero;

            stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight - scoreBarTex.Height);
           
            //ScorBar added at the bottom of screen
            Vector2 scoreBarPos = new Vector2(0, stage.Y);
            scoreBar = new ScoreBar(this, spriteBatch, scoreBarTex,
            scoreBarPos, stage);
            this.Components.Add(scoreBar);

            //Ball added at center of screen
            Vector2 ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2,
                stage.Y / 2 - ballTex.Height / 2);
            Vector2 ballSpeed =  Vector2.Zero;
            ball = new PongGame.Ball(this, spriteBatch, ballTex,
                ballPos, ballSpeed, stage, font, clickSound, dingSound);
            this.Components.Add(ball);
            
            //Right bat added at right center of screen
           
            batRPos = new Vector2(stage.X - batRTex.Width,
                stage.Y / 2 - batRTex.Height / 2);
            Vector2 batRSpeed = new Vector2(0, 4);
            rightBatName = "rightBat";
            batRight = new Bat(this, spriteBatch, batRTex,
                batRPos, batRSpeed, stage, rightBatName);
            this.Components.Add(batRight);

            //Left bat added at left center of screen

            batPos = new Vector2(0,
                stage.Y /2 - batTex.Height / 2);
            Vector2 batSpeed = new Vector2(0, 4);
            leftBatName = "leftBat";
            batLeft = new Bat(this, spriteBatch, batTex,
                batPos, batSpeed, stage, leftBatName);
            this.Components.Add(batLeft);
                        
            //handle collision
            cm1 = new CollissionManager(this, ball, batLeft, leftBatName, clickSound);
            this.Components.Add(cm1);
            //handle collision
            cm2 = new CollissionManager(this, ball, batRight, rightBatName, clickSound);
            this.Components.Add(cm2);

            ball.Counter = 1;

            // All strings used in program are added
            winner = new Winner(this, spriteBatch, font, strPos, message, Color.Purple);
            this.Components.Add(winner);
            score1 = new Score1(this, spriteBatch, font,strPos , message, Color.Black);
            this.Components.Add(score1);
            score2 = new Score2(this, spriteBatch, font, strPos, message, Color.Black);
            this.Components.Add(score2);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //When game is finished varible done is checked
            KeyboardState ks = Keyboard.GetState();
            if (ball.Done == true)
            {
                string winnerStr = "        " + ball.Player + " Wins!! \n Press spacebar to restart.";
                if (!isPlay)
                {
                    winSound.Play(); // Applaude sound played
                    isPlay = true;
                }
                winner.Message = winnerStr;
                // Winner string updated
                Vector2 dimension3 = font.MeasureString(winnerStr);
                winnerPos = new Vector2(graphics.PreferredBackBufferWidth / 2 - dimension3.X / 2,
                graphics.PreferredBackBufferHeight - scoreBarTex.Height / 2 - dimension1.Y);
                winner.Position = winnerPos;

                if (ks.IsKeyDown(Keys.Space))
                {
                    ball.Counter = 1;
                    ball.Done = false;
                    ball.Score1 = 0;
                    ball.Score2 = 0;
                    winner.Message = "";
                    batPos = new Vector2(0,
                        stage.Y / 2 - batTex.Height / 2);
                    batLeft.Position = batPos;
                    batRPos = new Vector2(stage.X - batRTex.Width,
                        stage.Y / 2 - batRTex.Height / 2);
                    batRight.Position = batRPos;
                }
                if (ks.IsKeyDown(Keys.Enter))
                {
                    ball.Play = false;
                    ball.Counter = 2;
                }
            }

            // Positions of string messages is updated here
            string message1 = "Almas Khan : " + ball.Score1;
            Vector2 dimension = font.MeasureString(message1);
            score1.Message = message1;

            string message2 = "Jason Bourne : " + ball.Score2;
            dimension1 = font.MeasureString(message2);
            score2.Message = message2;
                       
            str1Pos = new Vector2(0,
                    graphics.PreferredBackBufferHeight - scoreBarTex.Height / 2 - dimension.Y);
            score1.Position = str1Pos;

            str2Pos = new Vector2(graphics.PreferredBackBufferWidth - dimension1.X,
                    graphics.PreferredBackBufferHeight - scoreBarTex.Height / 2 - dimension1.Y);
            score2.Position = str2Pos; 
                      
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
