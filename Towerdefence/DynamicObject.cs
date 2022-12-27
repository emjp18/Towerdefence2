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
        protected Vector2 m_force;
        protected float m_torque;
        protected float m_speed;
        protected float m_mass = 1;
        protected Vector2 m_r;
        protected float m_angularVelocity;
        float m_inertia;
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
            m_r = obb.size * 0.5f;
            m_angularVelocity = 0;
            m_inertia = 1f / 12f * mass * ((obb.size.Y * obb.size.Y) + (obb.size.X * obb.size.X));
        }
        public void AddForce(Vector2 force)
        {
            m_force += force;
            
        }
        public void AddTorque(float torque)
        {
            m_torque += torque;

        }
        public override void Update(float dt)
        {
            m_force /= mass;
            m_force *= dt;
            m_obb.center += m_force * dt;
            float angularAcceleration = m_torque / m_inertia;
            m_angularVelocity += angularAcceleration * dt;
            m_obb.orientation += m_angularVelocity * dt;
            m_force = Vector2.Zero;
            m_torque = 0;
            base.Update(dt);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), GetSourceRectangle(),m_color, m_obb.orientation, m_obb.size * 0.5f, m_spriteeffects, 0); 
        }

        
        
    }
}
