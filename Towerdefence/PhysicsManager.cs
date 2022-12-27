using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

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
        public static Vector2 Support(OBB obbA, OBB obbB, Vector2 direction)
        {
            float maxA = float.MinValue;
            float maxB = float.MinValue;
            Vector2[] pointsA = new Vector2[4];
            pointsA[0] = obbA.topLeft;
            pointsA[1] = obbA.topRight;
            pointsA[2] = obbA.downLeft;
            pointsA[3] = obbA.downRight;
            Vector2[] pointsB = new Vector2[4];
            pointsB[0] = obbB.topLeft;
            pointsB[1] = obbB.topRight;
            pointsB[2] = obbB.downLeft;
            pointsB[3] = obbB.downRight;
            Vector2 a = Vector2.Zero;
            Vector2 b = Vector2.Zero;
            for (int i = 0; i < 4; i++)
            {
                float dotA = Vector2.Dot(pointsA[i], direction);
                if (dotA > maxA)
                {
                    maxA = dotA;
                    a = pointsA[i];
                }
                float dotB = Vector2.Dot(pointsB[i], -direction);
                if (dotB > maxB)
                {
                    maxB = dotB;
                    b = pointsB[i];
                }
            }



            return a - b;

        }
        public static Vector2 TripleCross(Vector2 a, Vector2 b, Vector2 c)
        {

           
            Vector2 resutl = (b * Vector2.Dot(c, a)) - (a * Vector2.Dot(c, b));

            return resutl;



        }
        public static bool EvolveSimplex(OBB obbA, OBB obbB, ref List<Vector2> simplex, ref Vector2 direction)
        {

           
            Vector2 a = simplex[simplex.Count() - 1];
            // compute AO (same thing as -A)
            Vector2 ao = -a;
            if (simplex.Count() == 3)
            {
                // then its the triangle case
                // get b and c
                Vector2 b = simplex[1];
                Vector2 c = simplex[0];
                // compute the edges
                Vector2 ab = b - a;
                Vector2 ac = c - a;
                // compute the normals
                Vector2 abPerp = TripleCross(ac, ab, ab);
                Vector2 acPerp = TripleCross(ab, ac, ac);
                // is the origin in R4
                if (SameDirection(abPerp, ao))
                {
                    // remove point c

                    simplex.RemoveAt(2);
                    // set the new direction to abPerp
                    direction = abPerp;
                }
                else
                {
                    // is the origin in R3
                    if (SameDirection(acPerp, ao))
                    {
                        // remove point b
                        simplex.RemoveAt(1);
                        // set the new direction to acPerp
                        direction = acPerp;
                    }
                    else
                    {
                        // otherwise we know its in R5 so we can return true
                        return true;
                    }
                }
            }
            else
            {
                // then its the line segment case
                Vector2 b = simplex[0];
                // compute AB
                Vector2 ab = b - a;
                // get the perp to AB in the direction of the origin
                Vector2 abPerp = TripleCross(ab, ao, ab);
                // set the direction to abPerp
                direction = abPerp;
            }
            return false;






        }
        public static bool SameDirection(
             Vector2 direction,
             Vector2 ao)
        {
            return Vector2.Dot(direction, ao) > 0;
        }
        public static Vector2 EPA(OBB obba, OBB obbb, List<Vector2> simplex)
        {
            int minIndex = 0;
            float minDistance = float.MaxValue;
            Vector2 minNormal = new Vector2();

            while (minDistance == float.MaxValue)
            {
                for (int i = 0; i < simplex.Count; i++)
                {
                    int j = (i + 1) % simplex.Count;

                    Vector2 vertexI = simplex[i];
                    Vector2 vertexJ = simplex[j];

                    Vector2 ij = vertexJ - vertexI;

                    Vector2 normal = new Vector2(ij.Y, -ij.X);
                    normal.Normalize();
                    float distance = Vector2.Dot(normal, vertexI);

                    if (distance < 0)
                    {
                        distance *= -1;
                        normal *= -1;
                    }

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minNormal = normal;
                        minIndex = j;
                    }
                    Vector2 support = Support(obba, obbb, minNormal);
                    float sDistance = Vector2.Dot(minNormal, support);

                    if (MathF.Abs(sDistance - minDistance) > 0.001)
                    {
                        minDistance = float.MaxValue;

                        List<Vector2> simplexCopy = new List<Vector2>();
                        foreach (Vector2 point in simplex)
                        {
                            simplexCopy.Add(point);
                        }

                        simplex.Clear();

                        for (int k = 0; k < simplexCopy.Count; k++)
                        {
                            if (k == minIndex)
                                simplex.Add(support);

                            simplex.Add(simplexCopy[k]);
                        }




                    }

                }

            }
            return minNormal * (minDistance + 0.001f);
        }
        public static bool SAT(GameObject a, GameObject b)
        {

            float[] max = new float[4] { float.MinValue, float.MinValue, float.MinValue, float.MinValue };
            float[] max2 = new float[4] { float.MinValue, float.MinValue, float.MinValue, float.MinValue };
            float[] min = new float[4] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
            float[] min2 = new float[4] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };

            Vector2[] axis = new Vector2[2] { a.obb.updir, a.obb.leftdir };
            Vector2[] axis2 = new Vector2[2] { b.obb.updir, b.obb.leftdir };
            Vector2[] corners = new Vector2[4] { a.obb.topLeft, a.obb.downLeft, a.obb.topRight, a.obb.downRight };
            Vector2[] corners2 = new Vector2[4] { b.obb.topLeft, b.obb.downLeft, b.obb.topRight, b.obb.downRight };

            for (int i = 0; i < 2; i++)
            {
                axis[i].Normalize();
                axis2[i].Normalize();
            }
            for (int i = 0; i < 4; i++)
            {
                float d = Vector2.Dot(corners[i], axis[0]);
                if (d > max[0])
                    max[0] = d;
                if (d < min[0])
                    min[0] = d;

                float d2 = Vector2.Dot(corners2[i], axis[0]);
                if (d2 > max2[0])
                    max2[0] = d2;
                if (d2 < min2[0])
                    min2[0] = d2;


                d = Vector2.Dot(corners[i], axis[1]);
                if (d > max[1])
                    max[1] = d;
                if (d < min[1])
                    min[1] = d;

                d2 = Vector2.Dot(corners2[i], axis[1]);
                if (d2 > max2[1])
                    max2[1] = d2;
                if (d2 < min2[1])
                    min2[1] = d2;

                d = Vector2.Dot(corners[i], axis2[0]);
                if (d > max[2])
                    max[2] = d;
                if (d < min[2])
                    min[2] = d;

                d2 = Vector2.Dot(corners2[i], axis2[0]);
                if (d2 > max2[2])
                    max2[2] = d2;
                if (d2 < min2[2])
                    min2[2] = d2;

                d = Vector2.Dot(corners[i], axis2[1]);
                if (d > max[3])
                    max[3] = d;
                if (d < min[3])
                    min[3] = d;

                d2 = Vector2.Dot(corners2[i], axis2[1]);
                if (d2 > max2[3])
                    max2[3] = d2;
                if (d2 < min2[3])
                    min2[3] = d2;
            }
            if ((min[0] > max2[0]) || min2[0] > max[0])
                return false;
            if ((min[1] > max2[1]) || min2[1] > max[1])
                return false;
            if ((min[2] > max2[2]) || min2[2] > max[2])
                return false;
            if ((min[3] > max2[3]) || min2[3] > max[3])
                return false;

            return true;
        }
        public static bool GJK(GameObject a, GameObject b, ref Vector2 mtv)
        {
            List<Vector2> simplex = new List<Vector2>();

            Vector2 direction = Vector2.UnitX;
            Vector2 startsupport = Support(a.obb, b.obb, direction);

            simplex.Add(startsupport);

            //direction = -startsupport;
            direction = -direction;
            bool colliding = false;
            while (true)
            {

                Vector2 support = Support(a.obb, b.obb, direction);
                if (Vector2.Dot(support, direction) <= 0)
                {
                    break;
                }
                
                simplex.Add(support);
                if (EvolveSimplex(a.obb, b.obb, ref simplex, ref direction))
                {
                    colliding = true;
                    break;
                }

            }
            if (colliding)
            {
                mtv = EPA(a.obb, b.obb, simplex);
                mtv += simplex[simplex.Count() - 1];
                
            }

            return colliding;


        }
        
    }
    
}
