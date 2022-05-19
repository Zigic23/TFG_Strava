using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Base
{
    public abstract class DbManager<T>
    {
        protected String dbConnectionString;

        public DbManager(String dbConnectionString)
        {
            /* Todos los objetos de tipo DbManager deberan tener un constructor
             * que les permita construirse a partir de una cadena de conexión
             * a base de datos
             */

            this.dbConnectionString = dbConnectionString;
        }

        /// <summary>
        /// Recupera el objeto de base de datos para la cadena de conexión proporcionada en su construcción
        /// </summary>
        /// <returns>Database object</returns>
        protected SqlDatabase GetDatabase()
        {
            return new SqlDatabase(this.dbConnectionString);
        }

        /// <summary>
        /// Recupera todos los registros para la entidad foco del manager
        /// </summary>
        /// <returns>Lista de elementos seleccionados</returns>
        public abstract List<T> SelAll();

        /// <summary>
        /// Recupera todos los registros coincidentes con los parametros de la entidad foco del manager
        /// </summary>
        /// <param name="parameters">parametros de selección</param>
        /// <returns></returns>
        public abstract List<T> SelAll(dynamic parameters);

        /// <summary>
        /// Recupera un elemento por su entidad del tipo de la entidad foco del manager
        /// </summary>
        /// <param name="id">Identificador unico del elemento a seleccionar</param>
        /// <returns></returns>
        public abstract T SelById(object id);

        /// <summary>
        /// Inserta un elemento del tipo de la entidad foco del manager
        /// </summary>
        /// <param name="dbObject">Objeto a insertar</param>
        /// <returns></returns>
        public abstract T Insert(T dbObject);

        /// <summary>
        /// Actualiza un elemento del tipo de la entidad foco del manager
        /// </summary>
        /// <param name="dbObject">Objeto a actualizar</param>
        /// <returns></returns>
        public abstract T Update(T dbObject);

        /// <summary>
        /// Elimina un elemento del tipo de la entidad foco del manager
        /// </summary>
        /// <param name="id">Identificador unico del elemento a eliminar</param>
        /// <returns></returns>
        public abstract bool Delete(object id);
    }
}
