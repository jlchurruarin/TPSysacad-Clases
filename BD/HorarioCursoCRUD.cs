using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public new async Task<int> Add()
        {
            AddSetValue("CursoID", Id);
            AddSetValue("Dia", (int) Dia);
            AddSetValue("HoraInicio", HoraInicio);
            AddSetValue("HoraFin", HoraFin);
            return await base.Add();
        }

        public new async Task<int> Delete()
        {
            AddWhereCondition("CursoID", Id);
            AddWhereCondition("Dia", (int) Dia);
            AddWhereCondition("HoraInicio", HoraInicio);

            return await base.Delete();
        }

        public new async Task<int> Update()
        {
            AddSetValue("HoraInicio", HoraInicio);
            AddSetValue("HoraFin", HoraFin);

            AddWhereCondition("CursoID", Id);
            AddWhereCondition("Dia", Dia);

            return await base.Update();
        }

        public static async Task<List<HorarioCurso>> GetAll()
        {
            HorarioCurso hc = new HorarioCurso();
            return await hc.InternalGetAll(hc.Map);
        }

        public static async Task<List<HorarioCurso>> SearchWhere(Dictionary<string, object> campoValores)
        {
            HorarioCurso hc = new HorarioCurso();
            return await hc.InternalSearchWhere(hc.Map, campoValores);
        }

        public static async Task<List<HorarioCurso>> GetHorarioCursos(Curso curso)
        {
            Dictionary<string, object> search = new Dictionary<string, object>();
            search.Add("CursoID", curso.Id);
            return await SearchWhere(search);
        }

        public static async Task<List<object>> GetCalendarioEstudiante(Usuario estudiante)
        {
            List<Curso> cursos = await Curso.GetCursosEnCurso(estudiante);
            
            List<object> lista = new List<object>();

            foreach (Curso curso in cursos)
            {
                List<HorarioCurso> horariosCurso = await GetHorarioCursos(curso);

                if (horariosCurso.Count == 0 )
                {
                    var obj = new
                    {
                        Dia = "Día no definido",
                        Curso = curso.Nombre,
                        Desde = "Horario no definido",
                        Hasta = "Horario no definido",
                    };

                    lista.Add(obj);
                }
                else { 
                    foreach (HorarioCurso hc in horariosCurso) 
                    {
                        var obj = new
                        {
                            Dia = Enum.GetName(typeof(Dia), hc.Dia),
                            Curso = curso.Nombre,
                            Desde = hc.HoraInicio,
                            Hasta = hc.HoraFin,
                        };

                        lista.Add(obj);
                    }
                }
            }

            return lista;
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
