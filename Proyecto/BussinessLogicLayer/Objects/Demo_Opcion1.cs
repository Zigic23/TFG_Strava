using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Objects
{
    /// <summary>
    /// Equivalente a la Opcion 2, lo usaremos cuando el contenido del objeto de
    /// base de datos se pueda retornar integro a la solución
    /// </summary>
    public class Demo_Opcion1 : MObject
    {
        private DemoDbObject dbObject;

        public int id
        {
            get { return dbObject.cdIdentifier; }
            set { dbObject.cdIdentifier = value; }
        }

        public string param1
        {
            get { return dbObject.dsParameter1; }
            set { dbObject.dsParameter1 = value; }
        }

        public string param2
        {
            get { return dbObject.dsParameter2; }
            set { dbObject.dsParameter2 = value; }
        }

        public string param3
        {
            get { return dbObject.dsParameter3; }
            set { dbObject.dsParameter3 = value; }
        }

        public string param4
        {
            get { return dbObject.dsParameter4; }
            set { dbObject.dsParameter4 = value; }
        }

        public Demo_Opcion1()
        {
            dbObject = new DemoDbObject();
        }

        public Demo_Opcion1(DbObject dbObject) : base(dbObject)
        {
            // hacemos el cast son tipos compatibles por herencia
            this.dbObject = (DemoDbObject)dbObject;
        }

    }
}
