using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class Projectile : DynamicObject
    {
        Enemy m_target;
        Vector2 m_center;
        public Projectile(OBB obb, string texname) : base(obb, texname)
        {
            m_center = obb.center;
        }
        public Enemy Target
        {
            set { m_target = value; }
        }
        public override void Update(float dt)
        {
            Vector2 dir = m_target.obb.center - m_obb.center;
            if (PhysicsManager.SAT(m_target, this))
            {
                m_draw = false;
                m_update = false;
                m_target.health = m_target.health - 10;
                m_obb.center = m_center;
                return;
            }


            dir.Normalize();
            AddForce(dir*m_speed*m_speed);

            AddTorque(m_r.X * dir.Y * m_speed - m_r.Y * dir.X * m_speed);

            base.Update(dt);
        }
    }
}
