using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using static System.Net.Mime.MediaTypeNames;

namespace Towerdefence
{
    public enum SOUND_FX{ OVER, BUILD, SHOOT, MONEY};
    internal class SoundManager : DrawableGameComponent
    {
        static SoundEffect m_lose;
       static SoundEffect m_build;
       static SoundEffect m_shoot;
        static SoundEffect m_money;
        public SoundManager(Game game) : base(game)
        {
        }
        public static void Play(SOUND_FX fx) { switch (fx)
            {
                case SOUND_FX.OVER:
                    {
                        m_lose.Play();
                        break;
                    }
                case SOUND_FX.BUILD:
                    {
                        m_build.Play();
                        break;
                    }
                case SOUND_FX.SHOOT:
                    {
                        m_shoot.Play();
                        break;
                    }
                case SOUND_FX.MONEY:
                    {
                        m_money.Play();
                        break;
                    }
            }
        }
      
        protected override void LoadContent()
        {
            m_lose = Game.Content.Load<SoundEffect>("over");
            m_build = Game.Content.Load<SoundEffect>("build");
            m_shoot = Game.Content.Load<SoundEffect>("shoot");
            m_money = Game.Content.Load<SoundEffect>("money");
            base.LoadContent();
        }
    }
}
