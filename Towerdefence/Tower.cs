using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class Tower : GameObject
    {
        public Tower(Texture2D tex, OBB obb, string texname) : base(tex, obb, texname)
        {
           
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(m_tex, GetDestinationRectangle(), m_color);
        }

        public override void Update(float dt)
        {
            
        }
    }
}
