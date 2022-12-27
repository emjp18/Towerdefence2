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


            if (pos.X > Game1.resolutionX * 0.75f)
            {
                pos.X = Game1.resolutionX * 0.75f;
            }
            if (pos.Y > Game1.resolutionY*0.75f)
            {
                pos.Y = Game1.resolutionY * 0.75f;
            }
            if (pos.X < Game1.resolutionX / 4)
            {
                pos.X = Game1.resolutionX / 4;
            }
            if (pos.Y < Game1.resolutionY / 4)
            {
                pos.Y = Game1.resolutionY / 4;
            }
            m_pos = pos;

            m_mv = Matrix.CreateTranslation(-pos.X, -pos.Y, 0) * Matrix.CreateScale(2, 2, 1) * Matrix.CreateTranslation(Game1.resolutionX / 2, Game1.resolutionY / 2, 0);
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
