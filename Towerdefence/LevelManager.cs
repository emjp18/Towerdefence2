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

        OBB m_obb = new OBB();
        public LevelManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gametime)
        {
            switch(GameManager.state)
            {
                case GAME_STATE.EDITOR:
                    {
                        if(KeyMouseReader.KeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
                        {
                            m_obb.center = KeyMouseReader.mouseState.Position.ToVector2();
                            m_obb.size = new Vector2(240.0f, 135.0f);
                            ResourceManager.AddObject(new Tower(m_obb, "wall"));
                        }
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
