using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Towerdefence
{
    internal class Particle : GameObject
    {
        Timer m_timer = new Timer();
        Random m_random = new Random();
        Vector2 m_pos = Vector2.Zero;
       
        int m_speed;
        public Particle(OBB obb, string texName, double lifetime = 2.5, int speed = 10) : base(obb, texName)
        {
            m_timer.ResetAndStart(lifetime);
            m_speed = speed;
            m_pos.X = m_random.Next(m_speed / 2, m_speed);
            m_pos.Y = m_random.Next(m_speed / 2, m_speed);
        }

        public override void Draw(SpriteBatch sb)
        {
            if(m_draw)
                sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
        }

        public override void Update(float dt)
        {
           if(m_update)
            {
                m_pos = PhysicsManager.TransformVector2x2(PhysicsManager.GetRotationMatrix2x2(m_speed * dt), m_pos);

                SetPosition(m_pos + m_obb.center);
                m_timer.Update((double)dt);
                base.Update(dt);
            }

            
        }
        public bool GetTimerDone()
        {
            return m_timer.IsDone();
        }
    }
}
