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

        public new async Task<int> Add()
        {
            AddSetValue("CursoID", Id);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);
            AddSetValue("ProfesorId", ProfesorId);
            return await base.Add();
        }

        public new async Task<int> Delete()
        {
            AddWhereCondition("CursoID", Id);
            return await base.Delete();
        }

        public new async Task<int> Update()
        {
            AddSetValue("Nombre", Nombre);
            AddSetValue("Aula", Aula);
            AddSetValue("CupoMaximo", CupoMaximo);
            AddSetValue("ProfesorId", ProfesorId);

            AddWhereCondition("CursoID", Id);
            return await base.Update();
        }

        public static async Task<List<Curso>> GetAll()
        {
            Curso curso = new Curso();
            return await curso.InternalGetAll(curso.Map);
        }

        public static async Task<List<Curso>> SearchWhere(Dictionary<string, object> campoValores)
        {
            Curso curso = new Curso();
            return await curso.InternalSearchWhere(curso.Map, campoValores);
        }

        public static async Task<List<Curso>> GetCursosInscripto(Usuario usuario)
        {
            List<Curso> listaCursos = new List<Curso>();
            List<Curso> listaCursosEnEspera = await GetCursosEnEspera(usuario);
            List<Curso> listaCursosEnCurso = await GetCursosEnCurso(usuario);
            List<Curso> listaCursosCursadaAprobada = await GetCursosCursoAprobado(usuario);
            List<Curso> listaCursosFinalAprobado = await GetCursosFinalAprobado(usuario);
            List<Curso> listaCursosLibre = await GetCursosLibre(usuario);

            listaCursos.AddRange(listaCursosEnEspera);
            listaCursos.AddRange(listaCursosEnCurso);
            listaCursos.AddRange(listaCursosCursadaAprobada);
            listaCursos.AddRange(listaCursosFinalAprobado);
            listaCursos.AddRange(listaCursosLibre);

            return listaCursos;
        }

        private static async Task<List<Curso>> GetCursosPorEstadoDeCursada(Usuario usuario, EstadoDeInscripcion estado)
        {
            Curso curso = new Curso();
            curso.AddJoin("INNER JOIN", "Inscripciones", "CursoID", "CursoID");
            curso.AddWhereCondition("Inscripciones", "EstudianteID", usuario.Id);
            curso.AddWhereCondition("Inscripciones", "EstadoDeInscripcion", estado);
            return await curso.InternalSearchWhere(curso.Map, new Dictionary<string, object>());
        }

        public static async Task<List<Curso>> GetCursosEnEspera(Usuario usuario)
        {
            return await GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.EnListaDeEspera);
        }

        public static async Task<List<Curso>> GetCursosEnCurso(Usuario usuario)
        {
            return await GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.Cursando);
        }

        public static async Task<List<Curso>> GetCursosCursoAprobado(Usuario usuario)
        {
            return await GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.CursadaAprobada);
        }

        public static async Task<List<Curso>> GetCursosFinalAprobado(Usuario usuario)
        {
            return await GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.FinalAprobado);
        }

        public static async Task<List<Curso>> GetCursosLibre(Usuario usuario)
        {
            return await GetCursosPorEstadoDeCursada(usuario, EstadoDeInscripcion.Libre);
        }

        public static async Task<Curso?> ObtenerCursoPorID(string id)
        {
            Curso curso = new Curso();
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("CursoID", id);
            List<Curso> cursos = await curso.InternalSearchWhere(curso.Map, where);

            if (cursos.Count == 0) return null;

            return cursos[0];
        }

        public static async Task<List<Curso>> ObtenerCursosPorIDMateria(string id)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("MateriaID", id);
            List<MateriaCurso> materiaCurso = await MateriaCurso.SearchWhere(where);

            List<Curso> listaCursos = new List<Curso>();

            foreach(MateriaCurso mc in materiaCurso)
            {
                Curso? curso = await ObtenerCursoPorID(mc.CursoId);
                if (curso is not null) { listaCursos.Add(curso); }
            }

            return listaCursos;
        }

        public static async Task<List<Curso>> ObtenerCursosPorIDMateria(string[] id)
        {
            List<Curso> listaCursos = new List<Curso>();
            foreach(string materiaId in id)
            {
                List<Curso> lcursos = await ObtenerCursosPorIDMateria(materiaId);
                listaCursos.AddRange(lcursos);
            }
            return listaCursos;
        }

        public async Task<List<Usuario>> GetIncriptos()
        {
            return await Usuario.GetUsuariosInscriptos(this);
        }

        public async Task<int> GetCantidadIncriptos()
        {
            List<Usuario> listaInscriptos = await Usuario.GetUsuariosInscriptos(this);
            return listaInscriptos.Count;
        }

        public async Task<List<HorarioCurso>> GetAllHorarios()
        {
            return await HorarioCurso.GetHorarioCursos(this);
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
