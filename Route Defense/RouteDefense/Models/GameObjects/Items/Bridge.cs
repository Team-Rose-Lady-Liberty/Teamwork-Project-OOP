using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RouteDefense.Core;

namespace RouteDefense.Models.GameObjects.Items
{
    public class Bridge 
    {
        private Texture2D texture;
        private bool rotation;
        private bool isActive;
        private bool isMoovable = true;
        private Rectangle rectangle;

        public Bridge(string id, Rectangle rect) 
        {
            this.texture = SubGameEngine.ContentManager.Load<Texture2D>("Terrain/bridge");         
            this.rotation = true;
            this.rectangle = rect;
        }

        public Rectangle Rectangle { get { return this.rectangle; } set { this.rectangle = value; } }

        public bool IsMoovable { get { return this.isMoovable; } set { this.isMoovable = value; } }

        public void Draw(SpriteBatch sprite)
        {
            if(isActive){
                sprite.Draw(this.texture, this.rectangle, Color.White);
            } 
        }

        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }
    }
}
