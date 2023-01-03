using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    public enum GAME_STATE { MENU, GAME,EDITOR, EXIT};
    internal class GameManager
    {
        LevelManager m_levelManager;
        RenderManager m_renderManager;
        FileManager m_fileManager;
        static GAME_STATE m_state = GAME_STATE.MENU;
        static GAME_STATE m_oldstate = GAME_STATE.EDITOR;
        int m_level = 0;
        UIControls m_controls;
        static bool m_pause = false;
        List<Microsoft.Xna.Framework.Input.Keys> m_keys = new List<Microsoft.Xna.Framework.Input.Keys>();
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
        public UIControls controls
        {
            get => m_controls;

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
        public static bool pause
        {
            get => m_pause;
            
        }
        public GameManager(Game game)
        {
            m_levelManager = new LevelManager(game);
            m_renderManager= new RenderManager(game);
            m_fileManager = new FileManager("../../../");
            m_controls = new UIControls(game);


        }
        private void InputString()
        {
            var oldKb = KeyMouseReader.oldKeyState;
           var  kb = KeyMouseReader.keyState;
            string s = "";
            //Koll så att keyboardstate har ändrats
            if (!kb.Equals(oldKb) && m_controls.m_isInTxt)
            {
                //säkerställa så det finns input
                int ki = kb.GetPressedKeyCount();
                //lägger till kanpptyrckningen i en lista
                if (ki > 0)
                    m_keys.Add(kb.GetPressedKeys()[0]);

                //loopar runt listan och får ut en sträng
                for (int i = 0; i < m_keys.Count; i++)
                {
                    s = s + m_keys[i].ToString();
                    m_controls.SetText(s);
                }

                //För att avsluta inputen
                if (kb.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && oldKb.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    m_keys.Clear();
                    m_controls.SetText("                   ");
                    m_controls.m_isInTxt = false;
                }
            }
        }
        public void GameLoop()
        {
            KeyMouseReader.Update();
            m_controls.CurrentIncome = m_levelManager.currentmoney.ToString();
            m_controls.survivedDays = m_levelManager.days.ToString();
            m_controls.DarkMonstersKilled = m_levelManager.darkmonsterskilled.ToString();
            m_controls.WhiteMonstersKilled = m_levelManager.whitemonsterskilled.ToString();
            m_controls.moneyspent = m_levelManager.spentmoney.ToString();
            bool m_gameover = false;
            if(m_levelManager.gameover&& !m_gameover)
            {
                m_gameover = true;
                m_controls.AddPauseMenu();
            }
            if (KeyMouseReader.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape)&&!m_levelManager.gameover)
            {
                m_pause = !m_pause;
                if(m_pause)
                {
                    m_controls.AddPauseMenu();
                }
                else
                {
                    m_controls.RemovePauseMenu();
                }
            }
           
           if ( m_controls.GetButton() == BUTTON_CLICK.PLAY)
            {
                m_state = GAME_STATE.GAME;
                m_controls.SetButton(BUTTON_CLICK.NONE);
            }
           else if (m_controls.GetButton() == BUTTON_CLICK.QUIT)
            {
                m_state = GAME_STATE.EXIT;
                m_controls.SetButton(BUTTON_CLICK.NONE);
            }
            if (m_state == GAME_STATE.MENU)
                InputString();
            if (m_state!=m_oldstate)
            {
                switch (m_state)
                {
                    case GAME_STATE.MENU:
                        {
                            m_controls.AddMenu();
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
                            m_controls.RemoveMenu();
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
