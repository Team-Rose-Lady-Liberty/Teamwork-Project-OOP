namespace RoseLadyLibertyOOPProject.Core
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework;
    public class Menu
    {
        private List<MenuItem> menuItems;
        
        public Menu()
        {
            menuItems = new List<MenuItem>();
        }

        public void Update(MouseState mouseState)
        {
            foreach(MenuItem item in menuItems)
            {
                item.Update();
            }
        }

        public void AddMenuItem(MenuItem item)
        {
            this.menuItems.Add(item);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (MenuItem item in menuItems)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
