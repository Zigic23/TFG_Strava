using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Base
{
    public class MObject
    {
        public MObject()
        {

        }

        public MObject(DbObject dbObject)
        {
            /* Todos los objetos del manager deberan tener un constructor que les 
             * permita construirse a partir de un DbObject
             */ 
        }
    }
}
