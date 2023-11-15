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

        public int Add()
        {
            ConfigurarParametros();

            string[] columnasBD = ObtenerListaColumnasBD();

            return base.Add();
        }

        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "EstudianteID", EstudianteId },
                { "CursoID", CursoId },
            };

            return base.Delete();
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "EstudianteID", EstudianteId },
                { "CursoID", CursoId },
            };

            return base.Update();
        }

        public Inscripcion Map(IDataRecord reader)
        {
            var estudianteId = reader["EstudianteID"].ToString() ?? "";
            var cursoId = reader["CursoID"].ToString() ?? "";
            var estadoInscripcion = (EstadoCursada) reader.GetByte(reader.GetOrdinal("EstadoDeInscripcion"));

            var inscripcion = new Inscripcion(estudianteId, cursoId, estadoInscripcion);

            return inscripcion;
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@EstudianteID", SqlDbType.VarChar);
            _comando.Parameters["@EstudianteID"].Value = EstudianteId;
            _comando.Parameters.Add("@CursoID", SqlDbType.VarChar);
            _comando.Parameters["@CursoID"].Value = CursoId;
            _comando.Parameters.Add("@EstadoDeInscripcion", SqlDbType.TinyInt);
            _comando.Parameters["@EstadoDeInscripcion"].Value = (int) EstadoDeInscripcion;
            _comando.Parameters.Add("@FechaInscripcion", SqlDbType.DateTime);
            _comando.Parameters["@FechaInscripcion"].Value = FechaInscripcion;

        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "EstudianteID",
                "CursoID",
                "EstadoDeInscripcion",
                "FechaInscripcion"
            ];
        }
    }
}
