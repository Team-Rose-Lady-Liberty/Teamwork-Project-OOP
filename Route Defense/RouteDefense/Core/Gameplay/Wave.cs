using System.Collections.Generic;
using RouteDefense.Interfaces;
using RouteDefense.Models.GameObjects.Units;

namespace RouteDefense.Core.Gameplay
{
    public class Wave : IUpdateable
    {
        private List<Enemy> enemies;

        public Wave()
        {
            enemies = new List<Enemy>();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
