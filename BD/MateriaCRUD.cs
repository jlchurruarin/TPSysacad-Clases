using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Materia : SQLCrud<Materia>, ICRUDOps<Materia>
    {

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        public Materia(string id, string nombre, string descripcion) : base("Materias")
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public new int Add()
        {
            AddSetValue("MateriaID", Id);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Descripcion", Descripcion);
            return base.Add();
        }


        public new int Delete()
        {
            AddWhereCondition("MateriaID", Id);
            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("Nombre", Nombre);
            AddSetValue("Descripcion", Descripcion);
            AddWhereCondition("MateriaID", Id);
            return base.Update();
        }

        public static List<Materia> GetAll()
        {
            Materia mat = new Materia();
            return mat.InternalGetAll(mat.Map);
        }

        public static List<Materia> SearchWhere(Dictionary<string, object> campoValores)
        {
            Materia mat = new Materia();
            return mat.InternalSearchWhere(mat.Map, campoValores);
        }

        public Materia Map(IDataRecord reader)
        {
            var id = reader["MateriaID"].ToString() ?? "";
            var nombre = reader["Nombre"].ToString() ?? "";
            var descripcion = reader["Descripcion"].ToString() ?? "";

            var materia = new Materia(id, nombre, descripcion);

            return materia;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return 
            [ 
                "MateriaID",
                "Nombre",
                "Descripcion"
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "MateriaID") retorno = SqlDbType.VarChar;
            else if (key == "Nombre") retorno = SqlDbType.VarChar;
            else if (key == "Descripcion") retorno = SqlDbType.VarChar;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}

