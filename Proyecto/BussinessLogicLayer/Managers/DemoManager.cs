using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Managers
{
    public class DemoManager : Manager
    {
        // Todos los manager deberan tener una instancia privada de su DbManager
        private DemoDbManager dbManager;

        public DemoManager(string dbConnectionString) : base(dbConnectionString)
        {
            // Todos los manager deberan instanciar su DbManager en el constructor
            dbManager = new DemoDbManager(dbConnectionString);
        }
    }
}
