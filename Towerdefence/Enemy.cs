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
        }
        public override void Update(float dt)
        {
            m_animationTimer.Update((double)dt);
           

            
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
        }
    }
}
