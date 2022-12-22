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
        public static float[,] GetRotationMatrix2x2(float angle)
        {
            float[,] m = new float[2, 2];
            m[0, 0] = MathF.Cos(angle);
            m[0, 1] = -MathF.Sin(angle);
            m[1, 0] = MathF.Sin(angle);
            m[1, 1] = MathF.Cos(angle);

            return m;
        }
        public static Vector2 TransformVector2x2(float[,] m, Vector2 vec)
        {
            Vector2 v2 = new Vector2();
            v2.X = m[0, 0] * vec.X + m[0, 1] * vec.Y;
            v2.Y = m[1, 0] * vec.X + m[1, 1] * vec.Y;
            return v2;
        }
    }
    
}
