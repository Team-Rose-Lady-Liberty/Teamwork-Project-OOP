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
                new Picture(new Rectangle(0, 0, 1024, 672), Context.Content.Load<Texture2D>("Menu Items/backgroundTransperant.png")),

                new Button(new Rectangle(360, 50, 305, 42),
                    Context.Content.Load<Texture2D>("Menu Items/character select.png"), "",
                    delegate() {}),

                new Button(new Rectangle(212, 150, 200, 400), 
                    Context.Content.Load<Texture2D>("Menu Items/warriorSelectionMenu.png"), "",
                    delegate() { this.Context.ChangeState(new GameplayState(Context, 
                        new Warrior(new Rectangle(0, 0, 48, 48), Context.Content))); }),

                new Label(new Rectangle(276, 180, 120, 50), "Warrior"), 
                new Label(new Rectangle(230, 370, 120, 50), "Melee Fighter"), 
                new Label(new Rectangle(230, 400, 120, 50), "Using Spears &"), 
                new Label(new Rectangle(230, 425, 120, 50), "Swords"), 

                new Button(new Rectangle(612, 150, 200, 400),
                    Context.Content.Load<Texture2D>("Menu Items/archerSelectionMenu.png"), "",
                    delegate() { this.Context.ChangeState(new GameplayState(Context, 
                        new Archer(new Rectangle(0, 0, 48, 48), Context.Content)));}),

                new Label(new Rectangle(682, 180, 120, 50), "Archer"), 
                new Label(new Rectangle(632, 370, 120, 50), "Range Fighter"), 
                new Label(new Rectangle(632, 400, 120, 50), "Using Bows"), 
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
            if (InputHandler.IsClicked(Keys.Escape))
            {
                return new MainMenuState(Context);
            }

            return null;
        }
    }
}
