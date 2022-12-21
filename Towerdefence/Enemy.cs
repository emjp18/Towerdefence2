using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
namespace Towerdefence
{
    internal class Enemy : DynamicObject
    {
        public Enemy(Texture2D tex, OBB obb, string texname)
        : base(tex, obb, texname)
        {

        }
       
    }
}
