using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
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

        public int Add()
        {

            ConfigurarParametros();

            string[] columnas = ObtenerListaColumnasBD();

            return base.Add(columnas);
        }


        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "MateriaID", Id }
            };

            return base.Delete(camposValor);
        }

        public static List<Materia> GetAll()
        {
            Materia mat = new Materia();
            return mat.InternalGetAll(ObtenerListaColumnasBD());
        }

        private List<Materia> InternalGetAll(string[] columnas)
        {
            return base.GetAll(Map, columnas);
        }

        public static List<Materia> SearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            Materia mat = new Materia();
            return mat.InternalSearchWhere(columnas, campoValores);
        }

        private List<Materia> InternalSearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            return base.SearchWhere(Map, columnas, campoValores);
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            string nombreCampoID = "MateriaID";
            string valorCampoId = Id;
            return base.Update(columnasBD, nombreCampoID, valorCampoId);
        }

        public Materia Map(IDataRecord reader)
        {
            var id = reader["MateriaID"].ToString() ?? "";
            var nombre = reader["Nombre"].ToString() ?? "";
            var descripcion = reader["Descripcion"].ToString() ?? "";

            var materia = new Materia(id, nombre, descripcion);

            return materia;
        }

        private static string[] ObtenerListaColumnasBD()
        {
            return 
            [ 
                "MateriaID",
                "Nombre",
                "Descripcion"
            ];
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@MateriaID", SqlDbType.VarChar);
            _comando.Parameters["@MateriaID"].Value = Id;
            _comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            _comando.Parameters["@Nombre"].Value = Nombre;
            _comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            _comando.Parameters["@Descripcion"].Value = Descripcion;
        }

    }
}

