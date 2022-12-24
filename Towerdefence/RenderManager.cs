using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
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
        SimplePath m_enmypath1;
        SimplePath m_enemypath2;
        Vector2[] points = new Vector2[6];
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

            m_enmypath1 = new SimplePath(game.GraphicsDevice);
            m_enemypath2 = new SimplePath(game.GraphicsDevice);
            m_enmypath1.Clean();
            m_enemypath2.Clean();
            points[0] = new Vector2(250, 398);
            points[1] = new Vector2(250, 180+ 398);
            points[2] = new Vector2(490, 180 + 398);
            points[3] = new Vector2(490, 400 + 398);
            points[4] = new Vector2(900, 400 + 398);
            points[5] = new Vector2(900, 1085);
            m_enmypath1.AddPoint(points[0]);
            m_enmypath1.AddPoint(points[1]);
            m_enmypath1.AddPoint(points[2]);
            m_enmypath1.AddPoint(points[3]);
            m_enmypath1.AddPoint(points[4]);
            m_enmypath1.AddPoint(points[5]);
            points[0] = new Vector2(1920-300, 398);
            points[1] = new Vector2(1920 - 300, 180 + 398);
            points[2] = new Vector2(1920 - 640, 180 + 398);
            points[3] = new Vector2(1920 - 640, 400 + 398);
            points[4] = new Vector2(1920 -1050, 400 + 398);
            points[5] = new Vector2(1920 - 1050, 1085);
            m_enemypath2.AddPoint(points[0]);
            m_enemypath2.AddPoint(points[1]);
            m_enemypath2.AddPoint(points[2]);
            m_enemypath2.AddPoint(points[3]);
            m_enemypath2.AddPoint(points[4]);
            m_enemypath2.AddPoint(points[5]);
        }
        protected override void LoadContent()
        {
            foreach (string texn in m_textures)
            {

                Texture2D tex = Game.Content.Load<Texture2D>(texn);
                ResourceManager.GetSetAllTextures().Add(texn, tex);
            }
            ResourceManager.pathLeft = m_enmypath1;
            ResourceManager.pathRight = m_enemypath2;
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

            m_enmypath1.DrawPoints(m_sb);
            m_enmypath1.Draw(m_sb);
            m_enemypath2.DrawPoints(m_sb);
            m_enemypath2.Draw(m_sb);
            m_sb.End();
            base.Draw(gameTime);
        }
        
    }
}
