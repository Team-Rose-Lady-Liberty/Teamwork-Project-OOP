namespace RoseLadyLibertyOOPProject
{
    using Microsoft.Xna.Framework;
    
    public abstract class GameObject
    {
        public GameObject(string id)
        {
            this.ID = id;
        }
            
        public GameObject(string id, int x, int y, int width, int height)
            : this(id)
        {
            this.Rectangle = new Rectangle(x, y, width, height);
        }

        public GameObject(string id, Rectangle rectangle)
            : this(id)
        {
            this.Rectangle = rectangle;
        }

        public string ID { get; set; }
        public Rectangle Rectangle { get; set; }
    }
}
