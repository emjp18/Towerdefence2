using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    internal abstract class DynamicObject : GameObject
    {
        Vector2 m_force;
        float m_torque;
        

        
        public DynamicObject(Texture2D tex, OBB obb, string texname)
        : base(tex, obb, texname)
        {

        }
        
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(m_tex, GetDestinationRectangle(), GetSourceRectangle(),m_color, m_obb.orientation, m_obb.size * 0.5f, m_spriteeffects, 0); 
        }

        public override void Update(float dt)
        {

        }
        
    }
}
