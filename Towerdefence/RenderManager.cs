using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Towerdefence
{
    internal class RenderManager : DrawableGameComponent
    {
        SpriteBatch m_sb;
        List<string> m_textures = new List<string>();
        SimplePath m_enmypath1;
        SimplePath m_enemypath2;
        Vector2[] m_points = new Vector2[6];

        Rectangle m_destRenderTargetRectangle;
        RenderTarget2D m_rendertarget;
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
            m_textures.Add("health");
            m_enmypath1 = new SimplePath(game.GraphicsDevice);
            m_enemypath2 = new SimplePath(game.GraphicsDevice);
            m_enmypath1.Clean();
            m_enemypath2.Clean();
            m_points[0] = new Vector2(250, 398);
            m_points[1] = new Vector2(250, 180+ 398);
            m_points[2] = new Vector2(490, 180 + 398);
            m_points[3] = new Vector2(490, 400 + 398);
            m_points[4] = new Vector2(900, 400 + 398);
            m_points[5] = new Vector2(900, 1085);
            m_enmypath1.AddPoint(m_points[0]);
            m_enmypath1.AddPoint(m_points[1]);
            m_enmypath1.AddPoint(m_points[2]);
            m_enmypath1.AddPoint(m_points[3]);
            m_enmypath1.AddPoint(m_points[4]);
            m_enmypath1.AddPoint(m_points[5]);
            m_points[0] = new Vector2(1920-300, 398);
            m_points[1] = new Vector2(1920 - 300, 180 + 398);
            m_points[2] = new Vector2(1920 - 640, 180 + 398);
            m_points[3] = new Vector2(1920 - 640, 400 + 398);
            m_points[4] = new Vector2(1920 -1050, 400 + 398);
            m_points[5] = new Vector2(1920 - 1050, 1085);
            m_enemypath2.AddPoint(m_points[0]);
            m_enemypath2.AddPoint(m_points[1]);
            m_enemypath2.AddPoint(m_points[2]);
            m_enemypath2.AddPoint(m_points[3]);
            m_enemypath2.AddPoint(m_points[4]);
            m_enemypath2.AddPoint(m_points[5]);
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

            OBB obb = new OBB();
            obb.topLeft = new Vector2(Game1.resolutionX - 150, Game1.resolutionY - 150);
            obb.size = new Vector2(150, 150);

            m_rendertarget = new RenderTarget2D(Game.GraphicsDevice, Game1.resolutionX, Game1.resolutionY);
            m_destRenderTargetRectangle = new Rectangle(obb.topLeft.ToPoint(), obb.size.ToPoint());
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            
            Game.GraphicsDevice.SetRenderTarget(m_rendertarget);
            Game.GraphicsDevice.Clear(Color.AliceBlue);
            m_sb.Begin();
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
                obj.Draw(m_sb);




            }
            m_sb.End();
            Game.GraphicsDevice.SetRenderTarget(null);
            Game.GraphicsDevice.Clear(Color.AliceBlue);
            m_sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                 ResourceManager.camera.MV);
            foreach (GameObject obj in ResourceManager.GetSetAllObjects())
            {
                obj.Draw(m_sb);




            }
            m_destRenderTargetRectangle.Location = ResourceManager.camera.Pos.ToPoint();
            m_destRenderTargetRectangle.Location += new Point(Game1.resolutionX / 6, Game1.resolutionY / 8);
            m_sb.Draw(m_rendertarget, m_destRenderTargetRectangle, Color.White);
            m_sb.End();
            base.Draw(gameTime);
        }
        
    }
}
