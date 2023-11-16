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

        public new int Add()
        {
            AddSetValue("CursoID", Id);
            AddSetValue("MateriaID", MateriaId);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("CursoID", Id);
            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("MateriaID", MateriaId);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);

            AddWhereCondition("CursoID", Id);
            return base.Update();
        }

        public static List<Curso> GetAll()
        {
            Curso curso = new Curso();
            return curso.InternalGetAll(curso.Map);
        }

        public static List<Curso> SearchWhere(Dictionary<string, object> campoValores)
        {
            Curso curso = new Curso();
            return curso.InternalSearchWhere(curso.Map, campoValores);
        }

        public List<Usuario> GetIncriptos()
        {
            return Usuario.GetUsuariosInscriptos(this);
        }

        public int GetCantidadIncriptos()
        {
            List<Usuario> listaInscriptos = Usuario.GetUsuariosInscriptos(this);
            return listaInscriptos.Count;
        }

        public List<HorarioCurso> GetAllHorarios()
        {
            return HorarioCurso.GetHorarioCursos(this);
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

        protected override string[] ObtenerListaColumnasBD()
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

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "CursoID") retorno = SqlDbType.VarChar;
            else if (key == "MateriaID") retorno = SqlDbType.VarChar;
            else if (key == "Nombre") retorno = SqlDbType.VarChar;
            else if (key == "Aula") retorno = SqlDbType.VarChar;
            else if (key == "CupoMaximo") retorno = SqlDbType.Int;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
