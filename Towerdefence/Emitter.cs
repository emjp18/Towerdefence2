using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefence
{
    internal class Emitter : GameObject
    {
        Timer m_timer = new Timer();
        Timer m_spawnTimer = new Timer();
        float m_spawntime;
        double m_lifetime;
        List<Particle> m_particles= new List<Particle>();
        public Emitter(OBB obb, string texName, double lifetime = 2.5, float spawnTime = 0.25f) : base(obb, texName)
        {
            m_timer.ResetAndStart(lifetime);
            m_spawnTimer.ResetAndStart(spawnTime);
            m_spawntime = spawnTime;
            m_lifetime = lifetime;
        }

        public override void Draw(SpriteBatch sb)
        {
            if(m_draw)
            {
                sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
                foreach (Particle p in m_particles)
                {
                    p.Draw(sb);
                }
            }
            
        }

        public override void Update(float dt)
        {
            if(m_update)
            {
                m_spawnTimer.Update(dt);
                m_timer.Update((double)dt);
                if (m_spawnTimer.IsDone())
                {
                    m_particles.Add(new Particle(m_obb, m_texName));
                    m_spawnTimer.ResetAndStart(m_spawntime);
                }

                for (int i = 0; i < m_particles.Count; i++)
                {
                    m_particles[i].Update(dt);
                    if (m_particles[i].GetTimerDone())
                    {
                        m_particles.RemoveAt(i);
                    }
                }

                base.Update(dt);
            }
           
        }
        public bool GetTimerDone()
        {
            return m_timer.IsDone();
        }
        public void ResetTimer() { m_timer.ResetAndStart(m_lifetime); }
    }
}
