using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class RenderManager : DrawableGameComponent
    {
        SpriteBatch m_sb;
        public RenderManager(Game game) : base(game)
        {
        }
        protected override void LoadContent()
        {
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {

                obj.tex = Game.Content.Load<Texture2D>(obj.texName);

            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            m_sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                 ResourceManager.GetCamera().vp);
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
               
                obj.Draw(m_sb);
              
            }
            m_sb.End();
            base.Draw(gameTime);
        }
        
    }
}
