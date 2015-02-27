using System;
using Microsoft.Xna.Framework;

namespace RouteDefense.Models
{
    public class GameObject
    {
        public GameObject(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }

        public Rectangle Rectangle { get; protected set; }

        //public string ID { get; private set; }
    }
}
