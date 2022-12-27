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
        float m_range = 200;
        int m_ammo = 10;
        Projectile[] m_projectiles;
        Timer m_shootTimer = new Timer();
        double m_shootDelay = 2;
        public Tower(OBB obb, string texname) : base( obb, texname)
        {
            m_projectiles = new Projectile[m_ammo];
            obb.size = new Vector2(1920 / 30, 1080 / 30);
           for (int i=0; i<m_ammo; i++)
            {
                if (texName == "suntower")
                    m_projectiles[i] = new Projectile(obb, "star");
                else
                    m_projectiles[i] = new Projectile(obb, "halfmoon");

                m_projectiles[i].draw = false;
                m_projectiles[i].update = false;
                m_projectiles[i].speed = 100;
            }
            m_shootTimer.ResetAndStart(m_shootDelay);
        }
      
        public float range
        {
            get { return m_range; }
            set { m_range = value; }
        }
        public override void Update(float dt)
        {
            m_shootTimer.Update((double)dt);
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
                if ( obj is Enemy)
                {
                    for (int i = 0; i < m_ammo; i++)
                    {
                        if(m_projectiles[i].update==false)
                        {
                            if ((obj.obb.center - m_obb.center).Length() < m_range &&m_shootTimer.IsDone())
                            {
                                m_projectiles[i].Target = obj as Enemy;
                                m_projectiles[i].draw = true;
                                m_projectiles[i].update = true;
                                m_shootTimer.ResetAndStart(m_shootDelay);
                            }
                        }
                       
                    }
                }
                    

                
            }
            for (int i = 0; i < m_ammo; i++)
            {
                if (m_projectiles[i].update == true)
                {
                    m_projectiles[i].Update(dt);
                }

            }

            base.Update(dt);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
            for (int i = 0; i < m_ammo; i++)
            {
               if(m_projectiles[i].draw)
                {
                    m_projectiles[i].Draw(sb);
                }
               
            }

        }

        
    }
}
