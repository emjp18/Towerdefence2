using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    internal class Enemy : DynamicObject
    {
        Timer m_animationTimer = new Timer();
        float m_secondsperFrame =0.250f;
        Rectangle m_sourcerect;
        Rectangle m_healthsourcerect;

        NonPhysicsObject m_healthbar;
        float m_health = 100.0f;
        public float health
        {
            get { return m_health; }
            set { m_health = value; }
        }
        public Enemy( OBB obb, string texname)
        : base( obb, texname)
        {
            m_animationTimer.ResetAndStart(m_secondsperFrame);
            m_sourcerect = GetSourceRectangle();
            m_sourcerect.Width /= 2;
            if (texName=="whitemonster")
            {
                m_spriteeffects = SpriteEffects.FlipHorizontally;
            }
            obb.center.Y -= obb.size.Y*0.5f;
            m_healthsourcerect = GetSourceRectangle();
            m_healthsourcerect.Height /= 4;
            obb.size.Y /= 4;
            m_healthbar = new NonPhysicsObject(obb, "health");
        }
        public override void Update(float dt)
        {
            m_animationTimer.Update((double)dt);
            if (m_health > 0)
            {
                Vector2 heatlhBarPos = obb.center;
                heatlhBarPos.Y -= obb.size.Y * 0.5f;
                m_healthbar.SetPosition(heatlhBarPos);
                m_healthbar.Update(dt);
                float percentageofhealth = m_healthsourcerect.X;
                percentageofhealth *= m_health;
                m_healthsourcerect.X = (int)percentageofhealth;
            }
               
            if (m_animationTimer.IsDone())
            {

                if (m_sourcerect.X == m_sourcerect.Width)
                    m_sourcerect.X = 0;
                else
                    m_sourcerect.X = m_sourcerect.Width;
                m_animationTimer.ResetAndStart(m_secondsperFrame);
            }
            base.Update(dt);
        }
        public override void Draw(SpriteBatch sb)
        {
            
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_sourcerect, m_color, m_obb.orientation, m_obb.size * 0.5f, m_spriteeffects, 0);
            if(m_health>0)
                sb.Draw(ResourceManager.GetSetAllTextures()[m_healthbar.texName], m_healthbar.GetDestinationRectangle(), m_sourcerect, m_healthbar.color, m_healthbar.obb.orientation, m_healthbar.obb.size*0.5f, m_healthbar.effect, 0);
        }
    }
}
