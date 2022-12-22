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
        List<string> m_textures = new List<string>();
        public RenderManager(Game game) : base(game)
        {
            m_sb = new SpriteBatch(Game.GraphicsDevice);
            m_textures.Add("blackmonster");
            m_textures.Add("castle");
            m_textures.Add("halfmoon");
            m_textures.Add("moon");
            m_textures.Add("moonTower");
            m_textures.Add("star");
            m_textures.Add("sun");
            m_textures.Add("suntower");
            m_textures.Add("wall");
            m_textures.Add("whitemonster");
            m_textures.Add("path");
            m_textures.Add("day");
            m_textures.Add("night");
        }
        protected override void LoadContent()
        {
            foreach (string texn in m_textures)
            {

                Texture2D tex = Game.Content.Load<Texture2D>(texn);
                ResourceManager.GetSetAllTextures().Add(texn, tex);
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            //m_sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
            //     ResourceManager.GetCamera().vp);
            m_sb.Begin();
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
               
                obj.Draw(m_sb);
              
            }
            m_sb.End();
            base.Draw(gameTime);
        }
        
    }
}
