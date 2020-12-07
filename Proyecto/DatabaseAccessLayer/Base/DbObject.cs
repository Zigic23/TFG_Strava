using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Base
{
    public class DbObject
    {
        public DbObject()
        {

        }

        public DbObject(DataRow row)
        {
            /* Todos los objetos de tipo DbObject deberan tener un constructor
             * que les permita construirse a partir de un objeto DataRow de base de datos
             */
        }
    }
}
