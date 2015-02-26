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
                new Picture(new Rectangle(0, 0, 1024, 672), Context.Content.Load<Texture2D>("Menu Items/menu background2.png")),

                new Picture(new Rectangle(240, 75, 544, 87), Context.Content.Load<Texture2D>("Menu Items/castle defense.png")),

                new Picture(new Rectangle(413, 235, 255, 200), Context.Content.Load<Texture2D>("Menu Items/panel_brown.png")),

                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2 - 65, 266, 190, 45),
                    Context.Textures["button"], "New Game",
                    delegate() { this.Context.ChangeState(new CharacterSelectionState(Context)); }),

                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2  - 65, 316, 190, 45),
                    Context.Textures["button"], "Temp",
                    delegate(){this.Context.ChangeState(new CharacterSelectionState(Context));}),

                new Button(new Rectangle(Context.GraphicsDevice.Viewport.Width / 2 - 65, 366, 190, 45), 
                    Context.Textures["button"], "Quit Game",
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
