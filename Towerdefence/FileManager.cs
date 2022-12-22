using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpDX.Direct3D9;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefence
{
    struct JFILE_INFO
    {
       public DataType dt;
       public Vector2 cp;
       public string texName;
        public Vector2 size;
    }
    public enum DataType { Enemy = 0, Tower = 1};
    internal class FileManager
    {
  
        JObject m_wholeObj;
        string m_fileName;
        string m_directory;

        public FileManager(string directory)
        {
            m_directory = directory;
        }
      
        public void ReadFromFile(string fileName)
        {
            ResourceManager.GetSetAllObjects().Clear();

            List<JFILE_INFO> everythng = GetJFileInfo(m_directory + fileName + ".json", "everything");

            OBB obb = new OBB();
            foreach (JFILE_INFO fi in everythng)
            {
                switch(fi.dt)
                {
                    case DataType.Enemy:
                        {
                            obb.center = fi.cp;
                            obb.size = fi.size;
                            ResourceManager.AddObject(new Enemy( obb, fi.texName));
                            break;

                        }
                    case DataType.Tower:
                        {
                            ResourceManager.AddObject(new Tower( obb, fi.texName));
                            break;

                        }
                }
            }

           
        }
        public void WriteToFile(string fileName, List<GameObject> gameObjectList)
        {


            WriteJsonToFile(m_directory + fileName, gameObjectList);

        }
        private void GetJObjectFromFile(string fileName)
        {
            m_fileName = fileName;
            StreamReader file = File.OpenText(fileName);
            JsonTextReader reader = new JsonTextReader(file);
            m_wholeObj = JObject.Load(reader);
            file.Close();
        }
        //private JFILE_INFO GetJFile(string fileName, string
        //propertyName)
        //{
        //    if (m_wholeObj == null || m_fileName == null ||
        //    m_fileName != fileName)
        //    {
        //        GetJObjectFromFile(fileName);
        //    }
        //    JObject obj = (JObject)m_wholeObj.GetValue(propertyName);
        //    return GetJFileInfo(obj);
        //}
        private List<JFILE_INFO> GetJFileInfo(string fileName, string propertyName)
        {
            if (m_wholeObj == null || m_fileName == null ||
            m_fileName != fileName)
            {
                GetJObjectFromFile(fileName);
            }

            List<JFILE_INFO> fileinfoList = new List<JFILE_INFO>();
            JArray arrayObj = (JArray)m_wholeObj.GetValue(propertyName);
            if (arrayObj != null)
            {
                for (int i = 0; i < arrayObj.Count; i++)
                {
                    JObject obj = (JObject)arrayObj[i];
                    JFILE_INFO info = GetJFileInfo(obj);
                    fileinfoList.Add(info);
                }
            }

            return fileinfoList;
        }
        private JFILE_INFO GetJFileInfo(JObject obj)
        {
            JFILE_INFO info;
            info.dt = (DataType)Convert.ToInt32(obj.GetValue("datatype"));
            info.cp.X = Convert.ToInt32(obj.GetValue("centerpositionX"));
            info.cp.Y = Convert.ToInt32(obj.GetValue("centerpositionY"));
            info.size.X = Convert.ToInt32(obj.GetValue("sizeX"));
            info.size.Y = Convert.ToInt32(obj.GetValue("sizeY"));
            info.cp.Y = Convert.ToInt32(obj.GetValue("centerpositionY"));
            info.texName = Convert.ToString(obj.GetValue("tex"));
            return info;
        }
        private void WriteJsonToFile(string filename,
        List<GameObject> gList)
        {
            JArray everything = new JArray();
            m_wholeObj = new JObject();

            for (int i = 0; i < gList.Count; i++)
            {
                everything.Add(CreateObject(gList[i]));

            }

            m_wholeObj.Add("everything", everything);
           
            File.WriteAllText(m_directory+filename+".json", m_wholeObj.ToString());
        }
        private JObject CreateObject(GameObject obj)
        {
            JObject jobj = new JObject();
            if (obj is Enemy)
            {
                jobj.Add("datatype", (int)DataType.Enemy);
            }
            else if(obj is Tower)
            {
                jobj.Add("datatype", (int)DataType.Tower);
            }
            jobj.Add("sizeX", obj.obb.size.X);
            jobj.Add("sizeY", obj.obb.size.Y);

            jobj.Add("centerpositionX", obj.obb.center.X);
            jobj.Add("centerpositionY", obj.obb.center.Y);
  
            jobj.Add("tex", obj.texName);

            return jobj;
        }
    }
}

