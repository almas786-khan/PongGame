/* PongGame
 * Bat Class
 * Revision History
 *      Almas Khan: 2016.11.15: Created
 *      Almas Khan: 2016.11.16: coded
 *      Almas Khan: 2016.11.16: Debugged
 *      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Bat : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 stage;
        private string batName;
        private const int width = 70;

        /// <summary>
        /// Getter setter of property so can be accessed in other class
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        /// <summary>
        ///  Parameterised Constructor of class will set all passed arguments in the class variables
        /// </summary>
        /// <param name="game">Pong</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Texture2D</param>
        /// <param name="position">Vector2</param>
        /// <param name="speed">Vector2</param>
        /// <param name="stage">Vector2</param>
        /// <param name="batName">string</param>
        public Bat(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage,
            string batName) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.batName = batName;
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
        /// Action of both bats are handled here according to batName passed in the constructor
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            //keyboard controlls for leftbat and right bat
            if (batName == "leftBat")
            {
                if (ks.IsKeyDown(Keys.A))
                {
                    position.Y -= speed.Y;
                }
                if (ks.IsKeyDown(Keys.Z))
                {
                    position.Y += speed.Y;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Up))
                {
                    position.Y -= speed.Y;
                }
                if (ks.IsKeyDown(Keys.Down))
                {
                    position.Y += speed.Y;
                }
            }
            //Bottom Bound
            if (position.Y >= stage.Y - (tex.Width + width) )
            {
                position.Y = stage.Y - (tex.Width + width);
            }
            //Top bound
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// To draw bat
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// This will get all four coordinated of rectangle in which bat is placed 
        /// used to find collision of ball with bat
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
