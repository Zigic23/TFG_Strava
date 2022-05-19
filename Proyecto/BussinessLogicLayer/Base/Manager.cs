using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Base
{
    public class Manager
    {
        protected string dbConnectionString;

        public Manager(string dbConnectionString)
        {
            /* Todos los objetos de tipo Manager deberan tener un constructor
             * que les permita construirse a partir de una cadena de conexión
             * a base de datos
             */

            this.dbConnectionString = dbConnectionString;
        }

    }
}
