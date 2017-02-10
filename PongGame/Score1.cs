﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Score1 : DrawableGameComponent
    {
    
        protected SpriteBatch spriteBatch;
        protected SpriteFont font;
        protected string message;
        protected Vector2 position;
        protected Color color;
       
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

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

        
        public Score1(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            string message,
            Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.message = message;
            this.color = color;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
