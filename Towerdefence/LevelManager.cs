using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spline;
using System.Runtime.Remoting;
namespace Towerdefence
{
    internal class LevelManager : DrawableGameComponent
    {
        Timer m_dayNightCycle=new Timer();
        OBB m_obb = new OBB();
        double m_dayTime = 10;
        double m_nightTime = 30;
        Vector2 m_celestialObjectPos = new Vector2(1856, 125);
        float m_celestialSpeed;
        bool m_day = true;
        Timer m_enemySpawnTimer = new Timer();
        Timer m_enemytoughTimer = new Timer();
        double m_enemyspawn = 2.5;
        Vector2 m_camPos;
        float m_camSpeed = 750;
        Random m_random= new Random();
        float m_pathwidth = 100;
        int m_money = 200;
        Timer m_buyTimer = new Timer();
        bool m_gameover = false;
        int m_moneyspent = 0;
        int m_whitemonsterskilled = 0;
        int m_darkmonterskilled = 0;
        int m_days = 1;
        float m_enemySpeedBoost = 0;
        public bool gameover
        {
            get { return m_gameover; }
        }
        public int currentmoney
        {
            get { return m_money; }
        }
        public int spentmoney
        {
            get { return m_moneyspent; }
        }
        public int whitemonsterskilled
        {
            get { return m_whitemonsterskilled; }
        }
        public int darkmonsterskilled
        {
            get { return m_darkmonterskilled; }
        }
        public int days
        {
            get { return m_days; }
        }
        public LevelManager(Game game) : base(game)
        {
            m_dayNightCycle.ResetAndStart(m_dayTime);
            m_celestialSpeed = m_celestialObjectPos.X / (float)m_dayTime;
            m_enemySpawnTimer.ResetAndStart(m_enemyspawn);
            m_enemytoughTimer.ResetAndStart(m_enemyspawn * 2);
            m_camPos = new Vector2(1000,1000);
            m_buyTimer.ResetAndStart(3);


        }
        public bool WithinPath(Point p)
        {
            if(p.X<m_pathwidth&&p.X>0)
            {
                if(p.Y>ResourceManager.pathLeft.GetPos(0).Y&&p.Y<ResourceManager.pathLeft.GetPos(1).Y)
                {
                    return true;
                }
                if (p.Y > ResourceManager.pathLeft.GetPos(1).Y && p.Y < ResourceManager.pathLeft.GetPos(2).Y)
                {
                    return true;
                }
                if (p.Y > ResourceManager.pathLeft.GetPos(2).Y && p.Y < ResourceManager.pathLeft.GetPos(3).Y)
                {
                    return true;
                }
                if (p.Y > ResourceManager.pathLeft.GetPos(3).Y && p.Y < ResourceManager.pathLeft.GetPos(4).Y)
                {
                    return true;
                }
                if (p.Y > ResourceManager.pathLeft.GetPos(4).Y && p.Y < ResourceManager.pathLeft.GetPos(5).Y)
                {
                    return true;
                }
                if (p.Y > ResourceManager.pathLeft.GetPos(5).Y)
                {
                    return true;
                }
            }
            return false;
        }
        public override void Update(GameTime gametime)
        {
            switch(GameManager.state)
            {
                case GAME_STATE.MENU:
                    {

                        break;
                    }
                case GAME_STATE.EDITOR:
                    {
                        
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
                            obj.Update(dt);

                            if(KeyMouseReader.mouseState.LeftButton==Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                            {
                                if (obj.GetDestinationRectangle().Contains(KeyMouseReader.mouseState.Position))
                                {
                                    obj.SetPosition(KeyMouseReader.mouseState.Position.ToVector2());

                                }
                            }
                            
                        }
                       
                       

                        break;
                    }
                case GAME_STATE.GAME:
                    {
                       
                        bool oldday = m_day;
                        double dt = gametime.ElapsedGameTime.TotalSeconds;
                        if (!GameManager.pause)
                        {
                            m_celestialObjectPos.X -= (float)dt * m_celestialSpeed;
                            m_dayNightCycle.Update(dt);
                            m_enemySpawnTimer.Update(dt);
                            m_enemytoughTimer.Update(dt);
                            m_buyTimer.Update(dt);
                            m_day = oldday;
                        }
                       
                       

                        Vector3 cameratranslation = ResourceManager.camera.MV.Translation;
                        Vector2 mouseP = new Vector2(-(int)cameratranslation.X / 2, -(int)cameratranslation.Y / 2) 
                            + KeyMouseReader.mouseState.Position.ToVector2() * 0.5f - new Vector2(
                120, 120) * 0.5f;
                       



                        if (KeyMouseReader.LeftClick()&& !WithinPath(mouseP.ToPoint()) &&m_day&& m_money >= 100 && mouseP.Y < 900)
                        {
                            
                            m_obb.size = new Vector2(120.0f, 67.0f);
                            m_obb.center = mouseP + m_obb.size * 0.5f ;
                            GameObject t = new Tower(m_obb, "suntower");
                            t.Update((float)dt);
                            bool collidingwithtower = false;
                            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                            {
                                if(obj is Tower)
                                {
                                    if (PhysicsManager.SAT(obj, t))
                                    {
                                        collidingwithtower = true;
                                        ResourceManager.GetSetAllObjects().Remove(obj);
                                        m_money += 100;
                                        m_moneyspent -= 100;
                                        break;
                                    }
                                }
                            }
                            if(!collidingwithtower)
                            {
                                ResourceManager.AddObject(t);
                               m_money -= 100;
                               m_moneyspent += 100;
                            }
                          

                        }
                        if (KeyMouseReader.RightClick() && !WithinPath(mouseP.ToPoint()) && m_day && m_money >= 100 && mouseP.Y < 900)
                        {
                           
                            m_obb.size = new Vector2(120.0f, 67.0f);
                            m_obb.center = mouseP + m_obb.size * 0.5f;
                            GameObject t = new Tower(m_obb, "moonTower");
                            t.Update((float)dt);
                            bool collidingwithtower = false;
                            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                            {
                                if (obj is Tower)
                                {
                                    if (PhysicsManager.SAT(obj, t))
                                    {
                                        collidingwithtower = true;
                                        ResourceManager.GetSetAllObjects().Remove(obj);
                                        m_money += 100;
                                        m_moneyspent -= 100;
                                        break;
                                    }
                                }
                            }
                            if (!collidingwithtower)
                            {
                                ResourceManager.AddObject(t);
                                m_money -= 100;
                                m_moneyspent += 100;
                            }


                        }

                        if (KeyMouseReader.KeyHeld(Microsoft.Xna.Framework.Input.Keys.Left))
                        {
                            m_camPos.X -= (float)dt * m_camSpeed;
                         }
                        if (KeyMouseReader.KeyHeld(Microsoft.Xna.Framework.Input.Keys.Right))
                        {
                            m_camPos.X += (float)dt * m_camSpeed;
                        }
                        if (KeyMouseReader.KeyHeld(Microsoft.Xna.Framework.Input.Keys.Up))
                        {
                            m_camPos.Y -= (float)dt * m_camSpeed;
                        }
                        if (KeyMouseReader.KeyHeld(Microsoft.Xna.Framework.Input.Keys.Down))
                        {
                            m_camPos.Y += (float)dt * m_camSpeed;
                        }
                        ResourceManager.camera.Transform(m_camPos);
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            if (obj is Tower)
                            {
                                (obj as Tower).night = !m_day;
                            }


                            if (obj is Enemy)
                            {
                                if(obj.GetDestinationRectangle().Location.Y>1080)
                                {
                                    m_gameover = true;
                                }

                               if(obj.texName=="whitemonster")
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        Vector2 pos1 = ResourceManager.pathRight.GetPos(i);
                                        Vector2 pos2 = ResourceManager.pathRight.GetPos(i + 1);
                                        if (obj.obb.center.X > pos2.X
                                            && obj.obb.center.X <= pos1.X)
                                        {
                                            Vector2 dir = pos2 - pos1;
                                            dir.Normalize();
                                            (obj as Enemy).AddForce(dir*((obj as Enemy).speed* m_enemySpeedBoost));
                                            break;
                                        }
                                        else if (obj.obb.center.Y > pos1.Y
                                            && obj.obb.center.Y < pos2.Y)
                                        {
                                            Vector2 dir = pos2 - pos1;
                                            dir.Normalize();
                                            (obj as Enemy).AddForce(dir* ((obj as Enemy).speed * m_enemySpeedBoost));
                                            break;
                                        }
                                        else if (
                                          obj.obb.center.Y <= pos1.Y)
                                        {
                                            obj.SetPosition(ResourceManager.pathRight.GetPos(0) + Vector2.UnitY * 10);

                                            break;
                                        }

                                    }
                                }
                               else
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        Vector2 pos1 = ResourceManager.pathLeft.GetPos(i);
                                        Vector2 pos2 = ResourceManager.pathLeft.GetPos(i + 1);
                                        if (obj.obb.center.X >= pos1.X
                                            && obj.obb.center.X < pos2.X)
                                        {
                                            Vector2 dir = pos2 - pos1;
                                            dir.Normalize();
                                            (obj as Enemy).AddForce(dir*(obj as Enemy).speed * m_enemySpeedBoost);
                                            break;
                                        }
                                        else if (obj.obb.center.Y > pos1.Y
                                            && obj.obb.center.Y < pos2.Y)
                                        {
                                            Vector2 dir = pos2 - pos1;
                                            dir.Normalize();
                                            (obj as Enemy).AddForce(dir*(obj as Enemy).speed * m_enemySpeedBoost);
                                            break;
                                        }
                                        else if (
                                          obj.obb.center.Y <= pos1.Y)
                                        {
                                            obj.SetPosition(ResourceManager.pathLeft.GetPos(0) + Vector2.UnitY * 10);

                                            break;
                                        }
                                        
                                    }
                                }
                                    
                                
                            }
                            if (obj.texName == "day")
                            {
                                
                                if(m_dayNightCycle.IsDone())
                                {
                                    m_dayNightCycle.ResetAndStart(m_nightTime);
                                    obj.texName = "night";
                                    m_day = false;
                                }
                            }
                            else if(obj.texName == "night")
                            {
                                if (m_dayNightCycle.IsDone())
                                {
                                    m_dayNightCycle.ResetAndStart(m_dayTime);
                                    obj.texName = "day";
                                    m_day = true;
                                    m_days++;
                                    m_enemySpeedBoost += 1;
                                }
                            }
                            if (obj.texName =="sun"||obj.texName=="moon")
                            {
                                if(m_day == false&& oldday != m_day)
                                {
                                    m_celestialObjectPos = new Vector2(1856, 125);
                                    obj.texName = "moon";
                                    oldday = m_day;
                                    m_celestialSpeed = m_celestialObjectPos.X / (float)m_nightTime;

                                }
                                else if(oldday!= m_day && m_day == true)
                                {
                                    obj.texName = "sun";
                                    m_celestialObjectPos = new Vector2(1856, 125);
                                    oldday = m_day;
                                    m_celestialSpeed = m_celestialObjectPos.X / (float)m_dayTime;
                                }
                                obj.SetPosition(m_celestialObjectPos);
                            }

                            if(!GameManager.pause)
                            {
                                if (!m_day || obj is not Enemy)
                                    obj.Update((float)dt);
                            }
                           
                            

                        }
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            if(obj is Enemy)
                            {
                                if((obj as Enemy).health<=0)
                                {
                                    ResourceManager.GetSetAllObjects().Remove(obj);
                                    if(obj.texName=="whitemonster")
                                    {
                                        m_whitemonsterskilled++;
                                    }
                                    else
                                    {
                                        m_darkmonterskilled++;
                                    }
                                    m_money += 10;
                                    break;
                                }
                            }
                        }
                        if (!m_day)
                        {
                            if (m_enemySpawnTimer.IsDone())
                            {
                                m_obb.center = ResourceManager.pathLeft.GetPos(0) + Vector2.UnitY * 10;
                                m_obb.size = new Vector2(240, 60);
                                GameObject go = new Enemy(m_obb, "blackmonster");
                                (go as Enemy).speed = m_random.Next(500, 700);
                                ResourceManager.AddObject(go);
                                m_enemySpawnTimer.ResetAndStart(m_enemyspawn);
                            }
                            if(m_enemytoughTimer.IsDone())
                            {
                                m_obb.center = ResourceManager.pathRight.GetPos(0) + Vector2.UnitY * 10;
                                m_obb.size = new Vector2(240, 60);
                                GameObject go = new Enemy(m_obb, "whitemonster");
                                (go as Enemy).speed = m_random.Next(700, 900);
                                ResourceManager.AddObject(go);
                                m_enemytoughTimer.ResetAndStart(m_enemyspawn * 2);
                            }

                        }
                        
                        break;
                    }
            }
            
        }
    }
}
