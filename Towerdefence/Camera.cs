using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towerdefence
{
    internal class Camera
    {
        Matrix m_mv = new Matrix();
        Vector2 m_pos;
        public Camera()
        {
            m_mv = Matrix.CreateTranslation(Game1.resolutionX / 2, Game1.resolutionY, 0);
            m_pos = new Vector2();
        }
        public void Transform(Vector2 pos)
        {


            if (pos.X > Game1.resolutionX)
            {
                pos.X = Game1.resolutionX;
            }
            if (pos.Y > Game1.resolutionY) 
            {
                pos.Y = Game1.resolutionY;
            }
            if (pos.X <0)
            {
                pos.X = 0;
            }
            if (pos.Y < 0)
            {
                pos.Y = 0;
            }
            m_pos = pos;

            m_mv =Matrix.CreateScale(2,2,1)* Matrix.CreateTranslation(-pos.X, -pos.Y, 0);
        }
        public Matrix MV
        {
            get { return m_mv; }
            
        }
        public Vector2 Pos
        {
            get { return m_pos; }

        }
    }
}
