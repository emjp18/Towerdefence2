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
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
        }

        public override void Update(float dt)
        {
            m_obb.updir = PhysicsManager.TransformVector2x2(PhysicsManager.GetRotationMatrix2x2(m_obb.orientation), -Vector2.UnitY);
            m_obb.leftdir = PhysicsManager.TransformVector2x2(PhysicsManager.GetRotationMatrix2x2(m_obb.orientation), -Vector2.UnitX);
            m_obb.topLeft = m_obb.center - m_obb.updir * m_obb.size.Y - m_obb.leftdir * m_obb.size.X;
            m_obb.downLeft = m_obb.center + m_obb.updir * m_obb.size.Y + m_obb.leftdir * m_obb.size.X;
            m_obb.topRight = m_obb.center + -m_obb.updir * m_obb.size.Y + m_obb.leftdir * m_obb.size.X;
            m_obb.downRight = m_obb.center + m_obb.updir * m_obb.size.Y - m_obb.leftdir * m_obb.size.X;
        }
    }
}
