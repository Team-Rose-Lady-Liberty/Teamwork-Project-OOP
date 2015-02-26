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
    public class RaceSelectionState : MasterState
    {
        private List<GUIElement> characters;

        public RaceSelectionState(GameEngine context)
        {
            this.Context = context;
            characters = new List<GUIElement>()
            {
                new Picture(new Rectangle(0, 0, 1024, 672), Context.Content.Load<Texture2D>("Menu Items/backgroundTransperant.png")),

                new Button(new Rectangle(410, 50, 205, 43),
                    Context.Content.Load<Texture2D>("Menu Items/raceSelect.png"), "",
                    delegate() {}),

                new Button(new Rectangle(250, 150, 200, 200), 
                    Context.Content.Load<Texture2D>("Menu Items/human select.png"), "",
                    delegate() { this.Context.ChangeState(new CharacterSelectionState(Context)); }),
                new Label(new Rectangle(325, 180, 120, 50), "Human"), 

                new Picture(new Rectangle(550, 150, 200, 200), Context.Content.Load<Texture2D>("Menu Items/skeleton select.png")),
                new Label(new Rectangle(610, 165, 120, 50), "Skeleton"), 
                new Label(new Rectangle(590, 200, 120, 50), "Coming Soon"),

                new Picture(new Rectangle(250, 400, 200, 200), Context.Content.Load<Texture2D>("Menu Items/elf select.png")),
                new Label(new Rectangle(335, 420, 120, 50), "Elf"), 
                new Label(new Rectangle(290, 455, 120, 50), "Coming Soon"),

                new Picture(new Rectangle(550, 400, 200, 200), Context.Content.Load<Texture2D>("Menu Items/orc select.png")),
                new Label(new Rectangle(635, 420, 120, 50), "Orc"), 
                new Label(new Rectangle(590, 455, 120, 50), "Coming Soon"),

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
