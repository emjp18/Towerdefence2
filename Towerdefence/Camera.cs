using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class Camera
    {
        Matrix m_vp;

        public Matrix vp
        {
            get { return m_vp; }
            set { m_vp = value; }
        }
    }
}
