using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RouteDefense.UI;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.MasterStates
{
    public class MainMenuState : MasterState
    {
        private List<GUIElement> elements;

        public MainMenuState(GameEngine context)
        {
            this.Context = context; 
            elements = new List<GUIElement>()
            {
                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2 -  65, 178, 130, 40),
                    Context.Textures["button"], "Start",
                    delegate() { this.Context.ChangeState(new CharacterSelectionState(Context)); }),

                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2  -  65, 228, 130, 40),
                    Context.Textures["button"], "Temp",
                    delegate(){this.Context.ChangeState(new CharacterSelectionState(Context));}),

                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2 -  65, 278, 130, 40), 
                    Context.Textures["button"], "Quit",
                    delegate(){Context.Exit();}),
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GUIElement item in elements)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override IMasterState Update(GameTime gameTime)
        {
            foreach (GUIElement item in elements)
            {
                item.Update(gameTime);
            }

            return null;
        }

        public override IMasterState HandleInput()
        {
            return null;
        }
    }
}
