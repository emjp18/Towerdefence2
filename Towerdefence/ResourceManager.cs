using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal static class ResourceManager
    {
        static Camera m_camera;
        static Dictionary<string, Texture2D> m_textures = new Dictionary<string, Texture2D>();
        public static Camera GetCamera() { return m_camera; }
        static List<GameObject> m_objects = new List<GameObject>();
        public static ref Dictionary<string, Texture2D> GetSetAllTextures() { return ref m_textures; }
        public static ref List<GameObject> GetSetAllObjects() { return ref m_objects; }
        public static GameObject GetObject(int index) { return m_objects[index]; }
        public static void AddObject(GameObject obj) { m_objects.Add(obj); }
    }
}
