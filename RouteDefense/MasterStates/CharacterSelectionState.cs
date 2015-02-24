using System.CodeDom.Compiler;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RouteDefense.Core;
using RouteDefense.Models.GameObjects.Units;
using RouteDefense.UI;
using RouteDefense.UI.GUIElements;


namespace RouteDefense.MasterStates
{
    public class CharacterSelectionState : MasterState
    {
        private List<GUIElement> characters;

        public CharacterSelectionState(GameEngine context)
        {
            this.Context = context;
            characters = new List<GUIElement>()
            {
                new Button(new Rectangle(263, 30, 305, 42),
                    Context.Content.Load<Texture2D>("Menu Items/character select.png"), "",
                    delegate() {}),

                new Button(new Rectangle(35, 90, 206, 356), 
                    Context.Content.Load<Texture2D>("Menu Items/charTest.png"), "",
                    delegate() { this.Context.ChangeState(new GameplayState(Context, 
                        new Warrior("temp", new Rectangle(0, 0, 48, 48), Context.Content))); }),

                new Button(new Rectangle(450, 90, 206, 356),
                    Context.Textures["button"], "Archer",
                    delegate() { this.Context.ChangeState(new GameplayState(Context, 
                        new Archer("temp", new Rectangle(0, 0, 48, 48), Context.Content)));}),
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GUIElement item in characters)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override IMasterState Update(GameTime gameTime)
        {
            foreach (GUIElement item in characters)
            {
                item.Update(gameTime);
            }

            return null;
        }

        public override IMasterState HandleInput()
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                return new MainMenuState(Context);
            }

            return null;
        }
    }
}
