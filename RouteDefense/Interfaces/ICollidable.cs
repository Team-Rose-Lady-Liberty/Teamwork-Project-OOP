using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteDefense.Interfaces
{
    public interface ICollidable
    {
        void HandleCollide(ICollidable collideInfo);
    }
}
