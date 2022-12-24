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
        float m_speed;
        float m_mass = 1;
        public float speed
        {
            get => m_speed;
            set { m_speed = value; }
        }
        public float mass
        {
            get => m_mass;
            set { m_mass = value; }
        }
        public DynamicObject( OBB obb, string texname)
        : base( obb, texname)
        {

        }
        public void AddForce(Vector2 force)
        {
            m_force += force* m_speed;
        }
        public override void Update(float dt)
        {
            m_force /= mass;
            m_force *= dt;
            m_obb.center += m_force * dt;
            m_force = Vector2.Zero;
            base.Update(dt);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), GetSourceRectangle(),m_color, m_obb.orientation, m_obb.size * 0.5f, m_spriteeffects, 0); 
        }

        
        
    }
}
