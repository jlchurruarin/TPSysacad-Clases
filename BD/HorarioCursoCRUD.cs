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
        public string Id { get; }
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

        public new int Add()
        {
            AddSetValue("CursoID", Id);
            AddSetValue("Dia", (int) Dia);
            AddSetValue("HoraInicio", HoraInicio);
            AddSetValue("HoraFin", HoraFin);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("CursoID", Id);
            AddWhereCondition("Dia", (int) Dia);

            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("HoraInicio", HoraInicio);
            AddSetValue("HoraFin", HoraFin);

            AddWhereCondition("CursoID", Id);
            AddWhereCondition("Dia", Dia);

            return base.Update();
        }

        public static List<HorarioCurso> GetAll()
        {
            HorarioCurso hc = new HorarioCurso();
            return hc.InternalGetAll(hc.Map);
        }

        public static List<HorarioCurso> SearchWhere(Dictionary<string, object> campoValores)
        {
            HorarioCurso hc = new HorarioCurso();
            return hc.InternalSearchWhere(hc.Map, campoValores);
        }

        public static List<HorarioCurso> GetHorarioCursos(Curso curso)
        {
            Dictionary<string, object> search = new Dictionary<string, object>();
            search.Add("CursoID", curso.Id);
            return SearchWhere(search);
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

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "CursoID",
                "Dia",
                "HoraInicio",
                "HoraFin"
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "CursoID") retorno = SqlDbType.VarChar;
            else if (key == "Dia") retorno = SqlDbType.VarChar;
            else if (key == "HoraInicio") retorno = SqlDbType.DateTime2;
            else if (key == "HoraFin") retorno = SqlDbType.DateTime2;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
