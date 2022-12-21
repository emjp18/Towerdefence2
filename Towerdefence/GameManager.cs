using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    public enum GAME_STATE { MENU, GAME,};
    internal class GameManager
    {
        LevelManager m_levelManager;
        RenderManager m_renderManager;
        GAME_STATE m_state = GAME_STATE.MENU;
        GAME_STATE m_oldstate = GAME_STATE.MENU;
        int m_level = 0;
        
        public int level
        {
            get => m_level;
            set { m_level = value; }
        }
        public GAME_STATE state
        {
            get => m_state;
            set { m_state = value; }
        }
        public GameManager(Game game)
        {
            m_levelManager = new LevelManager(game);
            m_renderManager= new RenderManager(game);
        }
        public void GameLoop()
        {
            if(m_state!=m_oldstate)
            {
                switch (m_state)
                {
                    case GAME_STATE.MENU:
                        {
                            break;
                        }
                    case GAME_STATE.GAME:
                        {
                            switch (level)
                            {
                                case 0:
                                    {
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
