using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Objects
{
    public class DemoDbObject : DbObject
    {
        public int cdIdentifier { get; set; }

        public string dsParameter1 { get; set; }
        public string dsParameter2 { get; set; }
        public string dsParameter3 { get; set; }
        public string dsParameter4 { get; set; }

        public DemoDbObject()
        {

        }

        public DemoDbObject(DataRow row) : base(row)
        {
            // El identificador nunca puede ser nulo por modelo no se permitira
            this.cdIdentifier = (int)row["CD_IDENTIFIER"];
            
            // parametros nulables, no olvidar nunca comprobar el NULL
            this.dsParameter1 = row.IsNull("DS_PARAMETRO_1") ? null : (string)row["DS_PARAMETRO_1"];
            this.dsParameter2 = row.IsNull("DS_PARAMETRO_2") ? null : (string)row["DS_PARAMETRO_2"];
            this.dsParameter3 = row.IsNull("DS_PARAMETRO_3") ? null : (string)row["DS_PARAMETRO_3"];
            this.dsParameter4 = row.IsNull("DS_PARAMETRO_4") ? null : (string)row["DS_PARAMETRO_4"];
        }
    }
}
