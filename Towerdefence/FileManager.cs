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
    }
    public enum DataType { Enemy = 0, Tower = 1};
    internal class FileManager
    {
        Texture2D m_placeholderTex;
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

            List<JFILE_INFO> everythng = GetJFileInfo(m_directory + fileName, "everything");

            OBB obb = new OBB();
            foreach (JFILE_INFO fi in everythng)
            {
                switch(fi.dt)
                {
                    case DataType.Enemy:
                        {
                            obb.center = fi.cp;
                            
                            ResourceManager.AddObject(new Enemy(m_placeholderTexx,))
                            break;

                        }
                    case DataType.Tower:
                        {
                            break;

                        }
                }
            }

            foreach (Vector2 pos in enemies)
            {
                enemydata0.x = (int)pos.X;
                enemydata0.y = (int)pos.Y;

                Enemy p = new Enemy(enemydata0);

                m_enemyList.Add(p);
            }
            List<Vector2> mushrooms = GetPosList(m_directory + fileName, "pickups");
            foreach (Vector2 pos in mushrooms)
            {
                mushroomData.x = (int)pos.X;
                mushroomData.y = (int)pos.Y;

                StaticObject p = new StaticObject(mushroomData);

                m_pickups.Add(p);
            }

            List<Vector2> coinblocks = GetPosList(m_directory + fileName, "coinblocks");
            foreach (Vector2 pos in coinblocks)
            {
                coinblockata.x = (int)pos.X;
                coinblockata.y = (int)pos.Y;

                StaticObject p = new StaticObject(coinblockata);

                m_blockList.Add(p);
            }
            List<Vector2> blocks = GetPosList(m_directory + fileName, "blocks");
            foreach (Vector2 pos in blocks)
            {
                blockata.x = (int)pos.X;
                blockata.y = (int)pos.Y;

                StaticObject p = new StaticObject(blockata);

                m_blockList.Add(p);
            }
            List<Vector2> background = GetPosList(m_directory + fileName, "backgrounds");
            foreach (Vector2 pos in background)
            {
                backgroundData.x = (int)pos.X;
                backgroundData.y = (int)pos.Y;

                StaticObject n = new StaticObject(backgroundData);

                n.SetIsEditable(false);
                m_backgroundObjects.Add(n);
            }
            List<Vector2> pipes = GetPosList(m_directory + fileName, "pipes");
            foreach (Vector2 pos in pipes)
            {
                pipeData.x = (int)pos.X;
                pipeData.y = (int)pos.Y;

                StaticObject n = new StaticObject(pipeData);


                m_pipeList.Add(n);
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
        private JFILE_INFO GetJFile(string fileName, string
        propertyName)
        {
            if (m_wholeObj == null || m_fileName == null ||
            m_fileName != fileName)
            {
                GetJObjectFromFile(fileName);
            }
            JObject obj = (JObject)m_wholeObj.GetValue(propertyName);
            return GetJFileInfo(obj);
        }
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
           
            File.WriteAllText(m_directory+filename, m_wholeObj.ToString());
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
            

            jobj.Add("centerpositionX", obj.obb.center.X);
            jobj.Add("centerpositionY", obj.obb.center.Y);
  
            jobj.Add("tex", obj.texName);

            return jobj;
        }
    }
}

