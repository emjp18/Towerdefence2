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
        public Enemy( OBB obb, string texname)
        : base( obb, texname)
        {

        }
       
    }
}
