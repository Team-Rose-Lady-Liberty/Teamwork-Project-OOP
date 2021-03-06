﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense.Models.GameObjects
{
    public class Unit : GameObject, Interfaces.IDrawable, Interfaces.IUpdateable
    {
        private Texture2D texture;
        private int range;

        public Unit(Rectangle rectangle) 
            : base(rectangle)
        {
        }

        public Rectangle RangeRectangle { get; set; }

        public int Speed { get; set; }

        public int Health { get; set; }

        public int Attack { get; set; }

        public int Range
        {
            get { return this.range; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The range of character cannot be negative number!");
                }
                this.range = value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
