using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace Towerdefence
{
    internal class Tower : GameObject
    {
        float m_range = 300;
        int m_ammo = 10;
        Projectile[] m_projectiles;
        Timer m_shootTimer = new Timer();
        Timer m_aoeTimer = new Timer();
        double m_shootDelay = 0.5;
        double m_aoeShootDelay = 2;
        Emitter[] m_particlesystem;
        Vector2 m_aoeDir = Vector2.UnitY;
        bool m_night = false;
        Enemy[] m_particleTargets;
        public bool night
        {
            get { return m_night; }
            set { m_night = value; }
        }
        public Tower(OBB obb, string texname) : base( obb, texname)
        {
            m_projectiles = new Projectile[m_ammo];
            m_particlesystem = new Emitter[m_ammo];
            m_particleTargets = new Enemy[m_ammo];
            m_aoeTimer.ResetAndStart(m_aoeShootDelay);
            obb.size = new Vector2(1920 / 30, 1080 / 30);
            if(texname == "suntower"||texname == "moonTower")
            {
                for (int i = 0; i < m_ammo; i++)
                {
                    if (texName == "suntower")
                        m_projectiles[i] = new Projectile(obb, "star");
                    else
                        m_projectiles[i] = new Projectile(obb, "halfmoon");

                    m_projectiles[i].draw = false;
                    m_projectiles[i].update = false;
                    m_projectiles[i].speed = 100;



                    obb.size *= 0.25f;
                    if (texName == "suntower")
                        m_particlesystem[i] = new Emitter(obb, "star");
                    else
                        m_particlesystem[i] = new Emitter(obb, "halfmoon");

                    obb.size *= 4;
                    m_particlesystem[i].draw = false;
                    m_particlesystem[i].update = false;
                }
                m_shootTimer.ResetAndStart(m_shootDelay);
            }
           
        }
      
        public float range
        {
            get { return m_range; }
            set { m_range = value; }
        }
        public override void Update(float dt)
        {

            if(m_update)
            {
                if(m_night)
                {
                    if (m_texName == "suntower")
                    {
                        m_shootTimer.Update((double)dt);
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            if (obj is Enemy)
                            {
                                for (int i = 0; i < m_ammo; i++)
                                {
                                    if (PhysicsManager.SAT(obj, m_projectiles[i]))
                                    {
                                        m_projectiles[i].SetPosition(m_obb.center);
                                        m_projectiles[i].Update(dt);
                                        m_projectiles[i].draw = false;
                                        m_projectiles[i].update = false;
                                        
                                        
                                        (obj as Enemy).health = (obj as Enemy).health - 10;

                                        m_particlesystem[i].draw = true;
                                        m_particlesystem[i].update = true;
                                        m_particlesystem[i].ResetTimer();
                                        m_particlesystem[i].SetPosition(obj.obb.center);
                                        m_particleTargets[i] = obj as Enemy;
                                    }
                                    var dist = obj.obb.center - m_obb.center;
                                    m_projectiles[i].SetShootdir(dist);
                                    if (dist.Length() > m_range)
                                    {
                                        m_projectiles[i].draw = false;
                                        m_projectiles[i].update = false;
                                    }
                                    else
                                    {
                                        m_projectiles[i].draw = true;
                                        m_projectiles[i].update = true;
                                        
                                        break;
                                    }
                                    
                                    

                                }
                            }



                        }
                        for (int i = 0; i < m_ammo; i++)
                        {
                            m_projectiles[i].Update(dt);
                            m_particlesystem[i].Update(dt);
                            if (m_particleTargets[i] != null)
                            {
                                if (m_particlesystem[i].GetTimerDone() || m_particleTargets[i].health <= 0)
                                {
                                    m_particlesystem[i].draw = false;
                                    m_particlesystem[i].update = false;
                                }
                            }
                            else
                            {
                                if (m_particlesystem[i].GetTimerDone())
                                {
                                    m_particlesystem[i].draw = false;
                                    m_particlesystem[i].update = false;
                                }
                            }

                        }
                    }
                    else if (m_texName == "moonTower")
                    {
                        m_aoeTimer.Update((double)dt);

                        if (m_aoeTimer.IsDone())
                        {
                            for (int i = 0; i < m_ammo; i++)
                            {
                                m_projectiles[i].SetPosition(m_obb.center);
                                m_aoeDir = PhysicsManager.TransformVector2x2(PhysicsManager.GetRotationMatrix2x2(360 / m_ammo), m_aoeDir);
                                m_aoeDir.Normalize();
                                m_projectiles[i].SetShootdir(m_aoeDir);
                                m_projectiles[i].draw = false;
                                m_projectiles[i].update = false;
                            }
                                
                            m_aoeTimer.ResetAndStart(m_aoeShootDelay);
                        }
                        else
                        {
                            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                            {
                                if (obj is Enemy)
                                {
                                    for (int i = 0; i < m_ammo; i++)
                                    {
                                        if (Vector2.Distance(obj.obb.center, m_obb.center) <= m_range)
                                        {
                                            m_projectiles[i].draw = true;
                                            m_projectiles[i].update = true;
                                          
                                            if (PhysicsManager.SAT(obj, m_projectiles[i]))
                                            {
                                                (obj as Enemy).health = (obj as Enemy).health - 10;
                                                m_projectiles[i].SetPosition(m_obb.center);
                                                m_projectiles[i].Update(dt);
                                                m_projectiles[i].update = false;
                                                m_projectiles[i].draw = false;
                                                m_particlesystem[i].draw = true;
                                                m_particlesystem[i].update = true;
                                                m_particlesystem[i].ResetTimer();
                                                m_particlesystem[i].SetPosition(obj.obb.center);
                                                m_particleTargets[i] =  obj as Enemy;


                                            }
                                        }
                                    }

                                }
                            }
                        }
                        for (int i = 0; i < m_ammo; i++)
                        {
                            m_projectiles[i].Update(dt);
                            m_particlesystem[i].Update(dt);
                            if (m_particleTargets[i] != null)
                            {
                                if (m_particlesystem[i].GetTimerDone() || m_particleTargets[i].health <= 0)
                                {
                                    m_particlesystem[i].draw = false;
                                    m_particlesystem[i].update = false;
                                }
                            }
                            else
                            {
                                if (m_particlesystem[i].GetTimerDone())
                                {
                                    m_particlesystem[i].draw = false;
                                    m_particlesystem[i].update = false;
                                }
                            }
                            

                        }

                    }
                }
               

               
                
                base.Update(dt);
            }


           
        }
        public override void Draw(SpriteBatch sb)
        {
            if(m_draw)
            {
                sb.Draw(ResourceManager.GetSetAllTextures()[m_texName], GetDestinationRectangle(), m_color);
                if (m_texName == "suntower" || m_texName == "moonTower")
                {
                    for (int i = 0; i < m_ammo; i++)
                    {
                        m_projectiles[i].Draw(sb);
                        m_particlesystem[i].Draw(sb);


                    }
                }
            }
            
               

        }

        
    }
}
