using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    public struct OBB
    {
        public Vector2 updir;
        public Vector2 leftdir;
        public Vector2 topLeft, topRight, downLeft, downRight;
        public float orientation;
        public Vector2 size;
        public Vector2 center;
    }
    internal static class PhysicsManager
    {

    }
}
