using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BibliotecaClases.BD
{
    public partial class Curso : SQLCrud<Curso>, ICRUDOps<Curso>
    {

        public string Id { get; internal set; }
        public string Nombre { get; set; }
        public string Aula { get; set; }
        public int CupoMaximo { get; set; }
        public string? ProfesorId { get; set; }

        public string DisplayText
        {
            get
            {
                return $"DisplayText: {ToString()}";
            }
        }

        private Curso(string id, string nombre, string aula, int cupoMaximo, string? profesorId) : base("Cursos")
        {
            Id = id;
            Nombre = nombre;
            Aula = aula;
            CupoMaximo = cupoMaximo;
            ProfesorId = profesorId;
        }

        public new int Add()
        {
            AddSetValue("CursoID", Id);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);
            AddSetValue("ProfesorId", ProfesorId);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("CursoID", Id);
            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);
            AddSetValue("ProfesorId", ProfesorId);

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

        public static List<Curso> GetCursosInscripto(Usuario usuario)
        {
            List<Curso> listaCursos = new List<Curso>();
            List<Curso> listaCursosEnEspera = GetCursosEnEspera(usuario);
            List<Curso> listaCursosEnCurso = GetCursosEnCurso(usuario);
            List<Curso> listaCursosCursadaAprobada = GetCursosCursoAprobado(usuario);
            List<Curso> listaCursosFinalAprobado = GetCursosFinalAprobado(usuario);
            List<Curso> listaCursosLibre = GetCursosLibre(usuario);

            listaCursos.AddRange(listaCursosEnEspera);
            listaCursos.AddRange(listaCursosEnCurso);
            listaCursos.AddRange(listaCursosCursadaAprobada);
            listaCursos.AddRange(listaCursosFinalAprobado);
            listaCursos.AddRange(listaCursosLibre);

            return listaCursos;
        }

        private static List<Curso> GetCursosPorEstadoDeCursada(Usuario usuario, EstadoDeInscripcion estado)
        {
            Curso curso = new Curso();
            curso.AddJoin("INNER JOIN", "Inscripciones", "CursoID", "CursoID");
            curso.AddWhereCondition("Inscripciones", "EstudianteID", usuario.Id);
            curso.AddWhereCondition("Inscripciones", "EstadoDeInscripcion", estado);
            return curso.InternalSearchWhere(curso.Map, new Dictionary<string, object>());
        }

        public static List<Curso> GetCursosEnEspera(Usuario usuario)
        {
            return GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.EnListaDeEspera);
        }

        public static List<Curso> GetCursosEnCurso(Usuario usuario)
        {
            return GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.Cursando);
        }

        public static List<Curso> GetCursosCursoAprobado(Usuario usuario)
        {
            return GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.CursadaAprobada);
        }

        public static List<Curso> GetCursosFinalAprobado(Usuario usuario)
        {
            return GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.FinalAprobado);
        }

        public static List<Curso> GetCursosLibre(Usuario usuario)
        {
            return GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.Libre);
        }

        public static Curso? ObtenerCursoPorID(string id)
        {
            Curso curso = new Curso();
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("CursoID", id);
            List<Curso> cursos = curso.InternalSearchWhere(curso.Map, where);

            if (cursos.Count == 0) return null;

            return cursos[0];
        }

        public static List<Curso> ObtenerCursosPorIDMateria(string id)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("MateriaID", id);
            List<MateriaCurso> materiaCurso = MateriaCurso.SearchWhere(where);

            List<Curso> listaCursos = new List<Curso>();

            foreach(MateriaCurso mc in materiaCurso)
            {
                Curso? curso = Curso.ObtenerCursoPorID(mc.CursoId);
                if (curso is not null) { listaCursos.Add(curso); }
            }

            return listaCursos;
        }

        public static List<Curso> ObtenerCursosPorIDMateria(string[] id)
        {
            List<Curso> listaCursos = new List<Curso>();
            foreach(string materiaId in id)
            {
                List<Curso> lcursos = ObtenerCursosPorIDMateria(materiaId);
                listaCursos.AddRange(lcursos);
            }
            return listaCursos;
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
            var nombre = reader["Nombre"].ToString() ?? "";
            var aula = reader["Aula"].ToString() ?? "";
            var cupoMaximo = reader.GetInt32(reader.GetOrdinal("CupoMaximo"));
            var profesorId = reader["ProfesorId"].ToString() ?? "";

            var curso = new Curso(id, nombre, aula, cupoMaximo, profesorId);

            return curso;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "CursoID",
                "Nombre",
                "Aula",
                "CupoMaximo",
                "ProfesorId"
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "CursoID") retorno = SqlDbType.VarChar;
            else if (key == "Nombre") retorno = SqlDbType.VarChar;
            else if (key == "Aula") retorno = SqlDbType.VarChar;
            else if (key == "CupoMaximo") retorno = SqlDbType.Int;
            else if (key == "ProfesorId") retorno = SqlDbType.VarChar;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
