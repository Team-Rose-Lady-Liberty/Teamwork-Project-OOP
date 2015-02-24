using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RouteDefense.Core;

namespace RouteDefense.Models.GameObjects.Items
{
    public class Bridge : Item
    {
        private Texture2D texture;
        private bool rotation;
        private bool isActive;
        private bool isMoovable = true;

        public Bridge(string id, Rectangle rectangle)
            : base(id, rectangle)
        {
            //this.texture = SubGameEngine.ContentManager.Load<Texture2D>("Terrain/bridge");         
            this.rotation = true;
        }

        public bool IsMoovable { get { return this.isMoovable; } set { this.isMoovable = value; } }

        public void Draw(SpriteBatch sprite)
        {
            if(isActive){
                sprite.Draw(this.texture, this.Rectangle, Color.White);
            } 
        }

        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }
    }
}
