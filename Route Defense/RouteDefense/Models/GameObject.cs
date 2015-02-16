using System;
using Microsoft.Xna.Framework;

namespace RouteDefense.Models
{
    public class GameObject
    {
        public GameObject(string id, Rectangle rectangle)
        {
            this.Rectangle = rectangle;
            this.ID = id;
        }

        public Rectangle Rectangle { get; protected set; }

        public string ID { get; private set; }
    }
}
