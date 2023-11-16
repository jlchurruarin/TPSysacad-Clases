using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Inscripcion : SQLCrud<Inscripcion>, ICRUDOps<Inscripcion>
    {
        public string EstudianteId { get; set; }
        public string CursoId { get; set; }
        public EstadoCursada EstadoDeInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }


        public Inscripcion(string estudianteId, string cursoId, EstadoCursada estadoDeInscripcion, DateTime fechaInscripcion) : base("Inscripciones")
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


        public Inscripcion Map(IDataRecord reader)
        {
            var estudianteId = reader["EstudianteID"].ToString() ?? "";
            var cursoId = reader["CursoID"].ToString() ?? "";
            var estadoInscripcion = (EstadoCursada) reader.GetByte(reader.GetOrdinal("EstadoDeInscripcion"));

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
    }
}
