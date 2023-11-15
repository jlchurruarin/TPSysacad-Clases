using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class HorarioCurso : SQLCrud<HorarioCurso>, ICRUDOps<HorarioCurso>
    {
        public string Id { get; set; }
        public Dia Dia { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }

        private HorarioCurso() : base("HorarioCurso") { }

        private HorarioCurso(string id, Dia dia, DateTime horaInicio,DateTime horaFin) : base("HorarioCurso")
        {
            Id = id;
            Dia = dia;
            HoraInicio = new DateTime(1753, 1, 1, horaInicio.Hour, horaInicio.Minute, horaInicio.Second);
            HoraFin = new DateTime(1753 , 1, 1, horaFin.Hour, horaFin.Minute, horaFin.Second);
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
                { "CursoID", Id },
                { "Dia", Dia },
                { "HoraInicio", HoraInicio }
            };

            return base.Delete();
        }

        public static List<HorarioCurso> GetAll()
        {
            HorarioCurso hc = new HorarioCurso();
            return hc.InternalGetAll(ObtenerListaColumnasBD());
        }

        private List<HorarioCurso> InternalGetAll(string[] columnas)
        {
            return base.GetAll(Map);
        }

        public static List<HorarioCurso> SearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            HorarioCurso hc = new HorarioCurso();
            return hc.InternalSearchWhere(columnas, campoValores);
        }

        private List<HorarioCurso> InternalSearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            return base.SearchWhere(Map);
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "CursoID", Id },
                { "Dia", Dia },
                { "HoraInicio", HoraInicio },
            };

            return base.Update();
        }

        public HorarioCurso Map(IDataRecord reader)
        {
            var id = reader["CursoID"].ToString() ?? "";
            var dia = (Dia) reader.GetByte(reader.GetOrdinal("Dia"));
            var horaInicio = reader.GetDateTime(reader.GetOrdinal("HoraInicio"));
            var HoraFin = reader.GetDateTime(reader.GetOrdinal("HoraFin"));
            
            var curso = new HorarioCurso(id, dia, horaInicio, HoraFin);

            return curso;
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@CursoID", SqlDbType.VarChar);
            _comando.Parameters["@CursoID"].Value = Id;
            _comando.Parameters.Add("@Dia", SqlDbType.TinyInt);
            _comando.Parameters["@Dia"].Value = (int) Dia;
            _comando.Parameters.Add("@HoraInicio", SqlDbType.DateTime2);
            _comando.Parameters["@HoraInicio"].Value = HoraInicio;
            _comando.Parameters.Add("@HoraFin", SqlDbType.DateTime2);
            _comando.Parameters["@HoraFin"].Value = HoraFin;
        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "CursoID",
                "Dia",
                "HoraInicio",
                "HoraFin"
            ];
        }
    }
}
