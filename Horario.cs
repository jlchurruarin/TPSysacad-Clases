using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Horario
    {
        private Dia _dia;
        private DateTime _hora;
        private float _cargaHoraria;

        public Dia Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }

        public DateTime Hora 
        {
            get { return _hora; }
            set { _hora = value; }
        }

        public float CargaHoraria
        {
            get { return _cargaHoraria; }
            set { _cargaHoraria = value; }
        }

        public string ObtenerHoraInicio()
        {
            return TimeOnly.FromDateTime(_hora).ToString("HH:mm");
        }

        public string ObtenerHoraFin()
        {
            return _hora.AddHours(CargaHoraria).ToString("HH:mm");
        }

        public Horario()
        {
            _hora = new DateTime();
            _dia = Dia.NoDefinido;
        }

        public Horario(Dia dia, TimeOnly hora)
        {
            _dia = dia;
            _hora = new DateTime(0, 0, 0, hora.Hour, hora.Minute, hora.Second);
        }

        public override string ToString()
        {
            return $"{Enum.GetName(typeof(Dia), _dia)} - {ObtenerHoraInicio()} a {ObtenerHoraFin()}";
        }
    }
}
