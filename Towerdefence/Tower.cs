using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Towerdefence
{
    internal class Tower : GameObject
    {
        public Tower(OBB obb, string texname) : base( obb, texname)
        {
           
        }
        public override void Update(float dt)
        {

            base.Update(dt);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
        
        }

        
    }
}
