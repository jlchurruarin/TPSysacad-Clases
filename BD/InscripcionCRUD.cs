using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Inscripcion : SQLCrud<Inscripcion>, ICRUDOps<Inscripcion>, IComparable<Inscripcion>
    {
        public string EstudianteId { get;  }
        public string CursoId { get; set; }
        public EstadoDeInscripcion EstadoDeInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }


        public Inscripcion(string estudianteId, string cursoId, EstadoDeInscripcion estadoDeInscripcion, DateTime fechaInscripcion) : base("Inscripciones")
        {
            EstudianteId = estudianteId;
            CursoId = cursoId;
            EstadoDeInscripcion = estadoDeInscripcion;
            FechaInscripcion = fechaInscripcion;
        }

        public new int Add()
        {
            AddSetValue("EstudianteID", EstudianteId);
            AddSetValue("CursoID", CursoId);
            AddSetValue("EstadoDeInscripcion", EstadoDeInscripcion);
            AddSetValue("FechaInscripcion", FechaInscripcion);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("EstudianteID", EstudianteId);
            AddWhereCondition("CursoID", CursoId);
            return base.Delete();
        }

        public new int Update()
        {
            AddWhereCondition("EstudianteID", EstudianteId);
            AddWhereCondition("CursoID", CursoId);

            AddSetValue("EstadoDeInscripcion", EstadoDeInscripcion);
            AddSetValue("FechaInscripcion", FechaInscripcion);

            return base.Update();
        }

        public static List<Inscripcion> GetAll()
        {
            Inscripcion inscripcion = new Inscripcion();
            return inscripcion.InternalGetAll(inscripcion.Map);
        }

        public static List<Inscripcion> SearchWhere(Dictionary<string, object> campoValores)
        {
            Inscripcion inscripcion = new Inscripcion();
            return inscripcion.InternalSearchWhere(inscripcion.Map, campoValores);
        }

        public static List<Inscripcion> GetInscripcionesDeCurso(string cursoId)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("CursoID", cursoId);
            return SearchWhere(where);
        }

        public static Inscripcion? GetInscripcion(string cursoId, string estudianteID)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("CursoID", cursoId);
            where.Add("EstudianteID", estudianteID);
            List<Inscripcion> listaInscripciones = SearchWhere(where);
            if (listaInscripciones.Count == 0) { return null; }
            else { return listaInscripciones[0]; }
            
        }

        public Inscripcion Map(IDataRecord reader)
        {
            var estudianteId = reader["EstudianteID"].ToString() ?? "";
            var cursoId = reader["CursoID"].ToString() ?? "";
            var estadoInscripcion = (EstadoDeInscripcion) reader.GetByte(reader.GetOrdinal("EstadoDeInscripcion"));

            var inscripcion = new Inscripcion(estudianteId, cursoId, estadoInscripcion);

            return inscripcion;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "EstudianteID",
                "CursoID",
                "EstadoDeInscripcion",
                "FechaInscripcion"
            ];
        }
        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "EstudianteID") retorno = SqlDbType.VarChar;
            else if (key == "CursoID") retorno = SqlDbType.VarChar;
            else if (key == "EstadoDeInscripcion") retorno = SqlDbType.TinyInt;
            else if (key == "FechaInscripcion") retorno = SqlDbType.Date;
            else retorno = SqlDbType.Variant;

            return retorno;
        }

        public int CompareTo(Inscripcion? other)
        {
            if (other is null)
                return 1;
            if (FechaInscripcion > other.FechaInscripcion)
                return 1;
            else if (FechaInscripcion < other.FechaInscripcion)
                return -1;
            else
                return 0;
        }
    }
}
