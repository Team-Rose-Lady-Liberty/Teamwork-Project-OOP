namespace RoseLadyLibertyOOPProject
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;
    
    abstract class GameObject
    {
        public GameObject(string id, int x, int y)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            
        }

        public string ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
