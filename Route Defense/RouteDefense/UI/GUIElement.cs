using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;

using IDrawable = RouteDefense.Interfaces.IDrawable;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.UI
{
    public abstract class GUIElement : IDrawable, IUpdateable
    {

        public GUIElement(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }

        public Rectangle Rectangle { get; private set; }

        public Action OnHover;

        public bool IsHovered;

        public virtual void Update(GameTime game)
        {
            if (this.Rectangle.Contains(InputHandler.MouseState.Position))
            {
                if (IsHovered == false && OnHover != null)
                {
                    OnHover();
                }
                IsHovered = true;
            }
            else
            {
                IsHovered = false;
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}