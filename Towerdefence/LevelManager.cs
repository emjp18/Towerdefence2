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
        double m_dayTime = 5*60;
        double m_nightTime = 3*60;
        Vector2 m_celestialObjectPos = new Vector2(1856, 125);
        float m_celestialSpeed;
        bool m_day = true;
        Timer m_enemySpawnTimer = new Timer();
        double m_enemyspawn = 10;
   
        
        Random m_random= new Random();
        public LevelManager(Game game) : base(game)
        {
            m_dayNightCycle.ResetAndStart(m_nightTime);
            m_celestialSpeed = m_celestialObjectPos.X / (float)m_dayTime;
            m_enemySpawnTimer.ResetAndStart(m_enemyspawn);

        }

        public override void Update(GameTime gametime)
        {
            switch(GameManager.state)
            {
                case GAME_STATE.EDITOR:
                    {
                        //if(KeyMouseReader.KeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
                        //{
                        //    m_obb.center = KeyMouseReader.mouseState.Position.ToVector2();
                        //    m_obb.size = new Vector2(240.0f, 135.0f);
                        //    ResourceManager.AddObject(new Tower(m_obb, "wall"));
                        //}
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
                        m_celestialObjectPos.X -= (float)dt * m_celestialSpeed;
                        m_dayNightCycle.Update(dt);
                        m_enemySpawnTimer.Update(dt);
                        m_day = oldday;      
                       
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            if(obj is Enemy)
                            {
                                for(int i=0; i<5; i++)
                                {
                                    Vector2 pos1 = ResourceManager.pathLeft.GetPos(i);
                                    Vector2 pos2 = ResourceManager.pathLeft.GetPos(i+1);

                                    if (obj.obb.center.X > pos1.X && obj.obb.center.Y > pos1.Y
                                        && obj.obb.center.X < pos2.X && obj.obb.center.Y < pos2.Y)
                                    {
                                        Vector2 dir = pos2 - pos1;
                                        dir.Normalize();
                                        (obj as Enemy).AddForce(dir);
                                        break;
                                    }
                                    if (
                                      obj.obb.center.X < pos1.X && obj.obb.center.Y < pos1.Y)
                                    {
                                        Vector2 dir = pos2 - pos1;
                                        dir.Normalize();
                                        (obj as Enemy).AddForce(dir);
                                        break;
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
                                }
                            }
                            if (obj.texName =="sun"||obj.texName=="moon")
                            {
                                if(m_day == false&& oldday != m_day)
                                {
                                    m_celestialObjectPos = new Vector2(1856, 125);
                                    obj.texName = "moon";
                                    oldday = m_day;

                                }
                                else if(oldday!= m_day && m_day == true)
                                {
                                    obj.texName = "sun";
                                    m_celestialObjectPos = new Vector2(1856, 125);
                                    oldday = m_day;
                                }
                                obj.SetPosition(m_celestialObjectPos);
                            }


                           

                            obj.Update((float)dt);

                        }
                        if (m_day)
                        {
                            if (m_enemySpawnTimer.IsDone())
                            {
                                m_obb.center = ResourceManager.pathLeft.GetPos(0);
                                GameObject go = new Enemy(m_obb, "whitemonster");
                                (go as Enemy).speed = m_random.Next(500, 700);
                                ResourceManager.AddObject(go);
                                m_enemySpawnTimer.ResetAndStart(m_enemyspawn);
                            }
                        }
                        break;
                    }
            }
            
        }
    }
}
