using BusinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;

namespace BusinessLogicLayer.Objects
{
    /// <summary>
    /// Equivalente a la Opcion 1, lo usaremos cuando sea necesario hacer
    /// alguna conversión entre los datos de la base de datos y el tipo final
    /// </summary>
    public class Demo_Opcion2 : MObject
    {        
        public int id { get; set; }

        public string param1 { get; set; }

        public string param2 { get; set; }

        public string param3 { get; set; }

        public string param4 { get; set; }

        public Demo_Opcion2(DbObject dbObject) : base(dbObject)
        {
            // hacemos el cast son tipos compatibles por herencia
            DemoDbObject _dbObject = (DemoDbObject)dbObject;

            this.id = _dbObject.cdIdentifier;
            this.param1 = _dbObject.dsParameter1;
            this.param2 = _dbObject.dsParameter2;
            this.param3 = _dbObject.dsParameter3;
            this.param4 = _dbObject.dsParameter4;
        }
    }
}
