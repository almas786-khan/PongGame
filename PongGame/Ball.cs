
/* PongGame
 * Ball Class
 * 
 * Revision History
 *      Almas Khan: 2016.11.15: Created
 *      Almas Khan: 2016.11.16: coded
 *      Almas Khan: 2016.11.16: Debugged
 *      
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PongGame
{
    /// <summary>
    /// Ball class inherit from DrawableGameComponent 
    /// all handling of ball is done in  this class
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        //Variables declaration

        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 stage;
        private Vector2 speed;
        private int score1 = 0;
        private int score2 = 0;
        private bool play = false;
        private string player = "";
        private SoundEffect clickSound;
        private SoundEffect dingSound;
        private bool done = false;
        private int counter = 0;
        private int maxScore = 2;
        private int min = 3;
        private int max = 9;
        public static Random rand = new Random();

        #region All getter setter of properties so can be accessed in other class
        public Vector2 Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public int Score1
        {
            get
            {
                return score1;
            }

            set
            {
                score1 = value;
            }
        }

        public int Score2
        {
            get
            {
                return score2;
            }

            set
            {
                score2 = value;
            }
        }

        public bool Done
        {
            get
            {
                return done;
            }

            set
            {
                done = value;
            }
        }

        public bool Play
        {
            get
            {
                return play;
            }

            set
            {
                play = value;
            }
        }

        public int Counter
        {
            get
            {
                return counter;
            }

            set
            {
                counter = value;
            }
        }

        public string Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
        #endregion


        /// <summary>
        /// Parameterised Constructor of class will set all passed arguments in the class variables
        /// </summary>
        /// <param name="game">Pong</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Texture2D</param>
        /// <param name="position">Vector2</param>
        /// <param name="speed">Vector2</param>
        /// <param name="stage">Vector2</param>
        /// <param name="font">SpriteFont</param>
        /// <param name="clickSound">SoundEffect</param>
        /// <param name="dingSound">SoundEffect</param>

        public Ball(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage,
            SpriteFont font,
            SoundEffect clickSound,
            SoundEffect dingSound
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.font = font;
            this.clickSound = clickSound;
            this.dingSound = dingSound;


        }
        /// <summary>
        /// Allows any initialization it needs to before starting to run.
        /// Initialize method of parent class will also run inside this
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// To pass random speed to the ball
        /// </summary>
        /// <returns>vector2 type data for speed of ball</returns>
        private Vector2 generateSpeed()
        {
            return new Vector2(getRandomNumber(min, max), getRandomNumber(min, max));
        }

        /// <summary>
        /// Generate random number between passed arguments
        /// </summary>
        /// <param name="n">minimum range</param>
        /// <param name="m">maximum range</param>
        /// <returns></returns>
        private int getRandomNumber(int n, int m)
        {
            int temp = Ball.rand.Next(n, m);
            if (temp % maxScore == 0)
                temp = -temp;
            return temp;
        }

        /// <summary>
        /// all logic for ball is done here. Action of ball when it hit any wall or paddle is handled here
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {            
            KeyboardState ks = Keyboard.GetState();
            
            if ((ks.IsKeyDown(Keys.Enter) && counter == 1))
            {
                play = true;
                speed = generateSpeed();
                counter = 0;
            }
            if (play == true)
            {                
                position += speed;
                //top wall
                if (position.Y < 0)
                {
                    speed.Y = Math.Abs(speed.Y);
                    clickSound.Play(); // click sound played
                }
                //left wall
                if (position.X < 0)
                {
                    //increase score of opposite player
                    score2 += 1;
                    //Check if game reach end condition by checking score
                    if (score2 == maxScore)
                    {
                        done = true;
                        player = "Jason Bourne";
                    }
                    dingSound.Play();
                    speed.X = Math.Abs(speed.X);
                    //play again
                    play = false;
                    counter = 1;
                    position.X = stage.X / 2 - tex.Width / 2;
                    position.Y = stage.Y / 2 - tex.Height / 2;
                }
                //right wall
                if (position.X + tex.Width > stage.X)
                {
                    //increase score of opposite player
                    score1 += 1;
                    //Check if game reach end condition by checking score
                    if (score1 == maxScore)
                    {
                        done = true;
                        player = "Almas Khan";
                    }
                    dingSound.Play(); ;
                    speed.X = -Math.Abs(speed.X);
                    //play again
                    play = false;
                    counter = 1;
                    position.X = stage.X / 2 - tex.Width / 2;
                    position.Y = stage.Y / 2 - tex.Height / 2;
                }
                ////bottom wall
                if (position.Y > stage.Y - tex.Width)
                {
                    speed.Y = -Math.Abs(speed.Y);
                    clickSound.Play();
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// To draw ball
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 2
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// This will get all four coordinated of rectangle in which ball is placed 
        /// used to find collision of ball with bat
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);

        }
        
    }
}