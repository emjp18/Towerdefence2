using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal abstract class GameObject
    {
        protected string m_texName;
        protected Texture2D m_tex;
        protected OBB m_obb;
        protected Color m_color = Color.White;
        protected bool m_draw = true;
        protected bool m_update = true;
        protected SpriteEffects m_spriteeffects= SpriteEffects.None;
        protected Point m_source = Vector2.Zero.ToPoint();
        public Point source
        {
            get { return m_source; }
            set { m_source = value; }
        }
        public OBB obb
        {
            get { return obb; }
            set { obb = value; }
        }
        public string texName
        {
            get { return m_texName; }
            set { m_texName = value; }
        }
        public Color color
        {
            get => m_color;
            set { m_color = value; }
        }
        public SpriteEffects effect
        {
            get => m_spriteeffects;
            set { m_spriteeffects = value; }
        }
        public Texture2D tex
        {
            get => m_tex;
            set { m_tex = value; }
        }
        public bool draw
        {
            get => m_draw;
            set { m_draw = value; }
        }
        public bool update
        {
            get => m_update;
            set { m_update = value; }
        }
        protected GameObject(Texture2D tex, OBB obb, string texName)
        {
            m_tex = tex;
            m_obb = obb;
            m_texName = texName;
        }
        public abstract void Draw(SpriteBatch sb);
        
        public abstract void Update(float dt);
        
        public Rectangle GetDestinationRectangle()
        {
            return new Rectangle(m_obb.topLeft.ToPoint(), m_obb.size.ToPoint());
        }
        public Rectangle GetSourceRectangle() { return new Rectangle(source, m_obb.size.ToPoint()); }
    }
}
