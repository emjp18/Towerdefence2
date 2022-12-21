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
        public static Camera GetCamera() { return m_camera; }
        static List<GameObject> objects;
        public static ref List<GameObject> GetSetAllObjects() { return ref objects; }
        public static GameObject GetObject(int index) { return objects[index]; }
        public static void AddObject(GameObject obj) { objects.Add(obj); }
    }
}
