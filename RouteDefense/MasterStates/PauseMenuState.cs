using System.Collections.Generic;
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
                new Button(new Rectangle(450, 300, 130, 40), Context.Textures["button"], "Resume",
                    delegate()
                    {
                        Context.ChangeState(new GameplayState(context, null));
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
