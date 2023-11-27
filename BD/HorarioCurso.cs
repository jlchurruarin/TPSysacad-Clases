using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class HorarioCurso
    {

        public string ObtenerHoraInicio()
        {
            return TimeOnly.FromDateTime(HoraInicio).ToString("HH:mm");
        }

        public string ObtenerHoraFin()
        {
            return TimeOnly.FromDateTime(HoraFin).ToString("HH:mm");
        }

        public HorarioCurso(Curso curso) : this(curso.Id, Dia.NoDefinido, new DateTime(0), new DateTime(0)) { }

        public HorarioCurso(Curso curso, Dia dia, TimeOnly horaInicio, TimeOnly horaFin) : this(curso) 
        { 
            Dia = dia;
            HoraInicio = new DateTime(1753, 1, 1, horaInicio.Hour, horaInicio.Minute, horaInicio.Second);
            HoraFin = new DateTime(1753, 1, 1, horaFin.Hour, horaFin.Minute, horaFin.Second);
        }

        public HorarioCurso(string cursoId, Dia dia, TimeOnly horaInicio, TimeOnly horaFin) : this()
        {
            Id = cursoId;
            Dia = dia;
            HoraInicio = new DateTime(1753, 1, 1, horaInicio.Hour, horaInicio.Minute, horaInicio.Second);
            HoraFin = new DateTime(1753, 1, 1, horaFin.Hour, horaFin.Minute, horaFin.Second);
        }

        public override string ToString()
        {
            return $"{Enum.GetName(typeof(Dia), Dia)} - {ObtenerHoraInicio()} a {ObtenerHoraFin()}";
        }
    }
}
