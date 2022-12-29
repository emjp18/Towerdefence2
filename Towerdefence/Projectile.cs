using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class Projectile : DynamicObject
    {
   
        
        bool m_foundTarget;
        Vector2 m_shootDir;
        public void SetShootdir(Vector2 dir) { m_shootDir = dir; }
        public bool FoundTarget
        {
            get { return m_foundTarget; }
            set { m_foundTarget = value; }
        }
        public Projectile(OBB obb, string texname) : base(obb, texname)
        {
         
            
        }
       
        public override void Update(float dt)
        {
            if(m_update)
            {
                if (texName == "star")
                {
                    if (m_shootDir != Vector2.Zero)
                        m_shootDir.Normalize();
                    AddForce(m_shootDir * m_speed * m_speed);

                    AddTorque(m_r.X * m_shootDir.Y * m_speed - m_r.Y * m_shootDir.X * m_speed);
                }
                else
                {

                    AddForce(m_shootDir * m_speed * m_speed);

                    AddTorque(m_r.X * m_shootDir.Y * m_speed - m_r.Y * m_shootDir.X * m_speed);
                }

                base.Update(dt);
            }
            
        }
        public override void Draw(SpriteBatch sb)
        {
            if(m_draw)
                base.Draw(sb);


        }

    }
}
