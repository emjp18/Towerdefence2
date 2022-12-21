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
        public LevelManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gametime)
        {
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
                float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
                obj.Update(dt);

            }
        }
    }
}
