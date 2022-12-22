using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class LevelManager : DrawableGameComponent
    {
        bool pickup = false;
        public LevelManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gametime)
        {
            switch(GameManager.state)
            {
                case GAME_STATE.EDITOR:
                    {
                        GameObject pickedup = null;
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
                            obj.Update(dt);

                            if (obj.GetDestinationRectangle().Contains(KeyMouseReader.mouseState.Position)&& KeyMouseReader.LeftClick()&&!pickup)
                            {
                                pickedup = obj;
                                pickup = true;

                            }
                        }
                        if (pickup)
                        {
                            pickedup.SetPosition(KeyMouseReader.mouseState.Position.ToVector2());
                            if(KeyMouseReader.LeftClick())
                            {
                                pickup = false;
                            }
                        }
                       

                        break;
                    }
                case GAME_STATE.GAME:
                    {
                        foreach (GameObject obj in ResourceManager.GetSetAllObjects())
                        {
                            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
                            obj.Update(dt);

                        }
                        break;
                    }
            }
            
        }
    }
}
