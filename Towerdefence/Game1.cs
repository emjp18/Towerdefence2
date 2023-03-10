using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace Towerdefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GameManager m_gameManager;
        static int m_resolutionX = 1920;
        static int m_resolutionY = 1080;

        
        public static int resolutionX
        {
            get { return m_resolutionX; }

        }
        public static int resolutionY
        {
            get { return m_resolutionY; }

        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_gameManager = new GameManager(this);
            _graphics.PreferredBackBufferWidth = m_resolutionX;
            _graphics.PreferredBackBufferHeight = m_resolutionY;
            _graphics.ApplyChanges();
            Components.Add(m_gameManager.rendermanager);
            Components.Add(m_gameManager.levelmanager);
            Components.Add(m_gameManager.controls);
            Components.Add(m_gameManager.soundmanager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            if (GameManager.state == GAME_STATE.GAME&& !GameManager.pause)
                IsMouseVisible = false;
            else
                IsMouseVisible = true;
            if (GameManager.state == GAME_STATE.EXIT)
                Exit();

            m_gameManager.GameLoop();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

     

            base.Draw(gameTime);
        }
    }
}