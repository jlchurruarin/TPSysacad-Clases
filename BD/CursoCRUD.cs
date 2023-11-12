using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Curso : SQLCrud<Curso>, ICRUDOps<Curso>
    {
        public string Id { get; set; }
        public string MateriaId { get; set; }
        public string Nombre { get; set; }
        public string Aula { get; set; }
        public int CupoMaximo { get; set; }


        private Curso(string id, string materiaId, string nombre, string aula, int cupoMaximo) : base("Cursos")
        {
            Id = id;
            MateriaId = materiaId;
            Nombre = nombre;
            Aula = aula;
            CupoMaximo = cupoMaximo;
        }

        public int Add()
        {
            ConfigurarParametros();

            string[] columnasBD = ObtenerListaColumnasBD();

            return base.Add(columnasBD);
        }

        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "CursoID", Id }
            };

            return base.Delete(camposValor);
        }

        public static List<Curso> GetAll()
        {
            Curso curso = new Curso();
            return curso.InternalGetAll(ObtenerListaColumnasBD());
        }

        public static List<Curso> GetAll(string[] columnas)
        {
            Curso curso = new Curso();
            return curso.InternalGetAll(columnas);
        }

        private List<Curso> InternalGetAll(string[] columnas)
        {
            return base.GetAll(Map, columnas);
        }

        public static List<Curso> SearchWhere(Dictionary<string, object> campoValores)
        {
            Curso curso = new Curso();
            return curso.InternalSearchWhere(ObtenerListaColumnasBD(), campoValores);
        }

        public static List<Curso> SearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            Curso curso = new Curso();
            return curso.InternalSearchWhere(columnas, campoValores);
        }

        private List<Curso> InternalSearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            return base.SearchWhere(Map, columnas, campoValores);
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            string nombreCampoID = "CursoID";
            string valorCampoId = Id;
            return base.Update(columnasBD, nombreCampoID, valorCampoId);
        }

        public List<Curso> GetIncriptos()
        {
            return this.GetIncriptos(ObtenerListaColumnasBD());
        }

        public List<Curso> GetIncriptos(string[] columnas)
        {
            //TODO
            return new List<Curso> { new Curso() };
        }

        public int GetCantidadIncriptos()
        {
            //TODO
            return 0;
        }

        public Curso Map(IDataRecord reader)
        {
            var id = reader["CursoID"].ToString() ?? "";
            var materiaId = reader["MateriaID"].ToString() ?? "";
            var nombre = reader["Nombre"].ToString() ?? "";
            var aula = reader["Aula"].ToString() ?? "";
            var cupoMaximo = reader.GetInt32(reader.GetOrdinal("CupoMaximo"));

            var curso = new Curso(id, materiaId, nombre, aula, cupoMaximo);

            return curso;
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@CursoID", SqlDbType.VarChar);
            _comando.Parameters["@CursoID"].Value = Id;
            _comando.Parameters.Add("@MateriaID", SqlDbType.VarChar);
            _comando.Parameters["@MateriaID"].Value = MateriaId;
            _comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            _comando.Parameters["@Nombre"].Value = Nombre;
            _comando.Parameters.Add("@Aula", SqlDbType.VarChar);
            _comando.Parameters["@Aula"].Value = Aula;
            _comando.Parameters.Add("@CupoMaximo", SqlDbType.Int);
            _comando.Parameters["@CupoMaximo"].Value = CupoMaximo;

        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "CursoID",
                "MateriaID",
                "Nombre",
                "Aula",
                "CupoMaximo"
            ];
        }
    }
}
