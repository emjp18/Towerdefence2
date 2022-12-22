using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    public enum GAME_STATE { MENU, GAME,EDITOR};
    internal class GameManager
    {
        LevelManager m_levelManager;
        RenderManager m_renderManager;
        FileManager m_fileManager;
        static GAME_STATE m_state = GAME_STATE.EDITOR;
        static GAME_STATE m_oldstate = GAME_STATE.MENU;
        int m_level = 0;
        public FileManager filemanager
        {
            get => m_fileManager;
           
        }
        public LevelManager levelmanager
        {
            get => m_levelManager;

        }
        public RenderManager rendermanager
        {
            get => m_renderManager;

        }
        public int level
        {
            get => m_level;
            set { m_level = value; }
        }
        public static GAME_STATE state
        {
            get => m_state;
            set { m_state = value; }
        }
        public GameManager(Game game)
        {
            m_levelManager = new LevelManager(game);
            m_renderManager= new RenderManager(game);
            m_fileManager = new FileManager("../../../");
           


        }
        public void GameLoop()
        {
            KeyMouseReader.Update();
            if(m_state!=m_oldstate)
            {
                switch (m_state)
                {
                    case GAME_STATE.MENU:
                        {
                            break;
                        }
                    case GAME_STATE.EDITOR:
                        {
                            switch (level)
                            {
                                case 0:
                                    {
                                        m_fileManager.ReadFromFile("scene");
                                        break;

                                    }
                                case 1:
                                    {
                                        break;

                                    }
                                case 2:
                                    {
                                        break;

                                    }
                            }
                            break;
                        }
                    case GAME_STATE.GAME:
                        {
                            switch (level)
                            {
                                case 0:
                                    {
                                        m_fileManager.ReadFromFile("scene");
                                        break;

                                    }
                                case 1:
                                    {
                                        break;

                                    }
                                case 2:
                                    {
                                        break;

                                    }
                            }
                            break;
                        }
                }
                m_oldstate = m_state;
            }

           
        }
        
    }
}
