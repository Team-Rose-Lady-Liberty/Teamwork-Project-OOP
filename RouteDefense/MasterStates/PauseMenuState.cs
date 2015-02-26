using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.UI;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.MasterStates
{
    class PauseMenuState : MasterState
    {
        private List<GUIElement> elements;

        public PauseMenuState(GameEngine context)
        {
            this.Context = context;
            
            elements = new List<GUIElement>()
            {
                new Picture(new Rectangle(0, 0, 1024, 672), Context.Content.Load<Texture2D>("Menu Items/backgroundTransperant.png")),

                new Button(new Rectangle(447, 300, 130, 40), Context.Textures["button"], "Resume",
                    delegate()
                    {
                        Context.ChangeState(new GameplayState(context, null));
                    }),
                new Button(new Rectangle(447, 350, 130, 40), Context.Textures["button"], "Main menu",
                    delegate()
                    {
                        Context.DeleteState(Context.GetStates().Where(state => state.GetType().Name == "GameplayState").First());
                        
                        Context.ChangeState(new MainMenuState(context));
                    })
            };
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (GUIElement item in elements)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
