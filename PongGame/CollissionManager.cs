using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PongGame
{
    public class CollissionManager : GameComponent
    {
        private Ball ball;
        private Bat bat;
        private string batName;
        private SoundEffect clickSound;

        public CollissionManager(Game game,
            Ball ball,
            Bat bat,
            string batName,
            SoundEffect clickSound

            ) : base(game)
        {
            this.ball = ball;
            this.bat = bat;
            this.batName = batName;
            this.clickSound = clickSound;

        }
        public override void Initialize()
        {
            base.Initialize();  
        }
        public override void Update(GameTime gameTime)
        {
            Rectangle ballRect = ball.getBounds();
            Rectangle batRect = bat.getBounds();

            if (batName == "leftBat")
            {

                if (ballRect.Intersects(batRect))
                {
                    ball.Speed = new Vector2(-ball.Speed.X, -Math.Abs(ball.Speed.Y));
                    clickSound.Play();
                }
            }
            if (batName == "rightBat")
            {
                if (ballRect.Intersects(batRect))
                {
                    ball.Speed = new Vector2(-Math.Abs(ball.Speed.X), -Math.Abs(ball.Speed.Y));
                    clickSound.Play();
                }   
            }

            base.Update(gameTime);
        }
      
    }
}
