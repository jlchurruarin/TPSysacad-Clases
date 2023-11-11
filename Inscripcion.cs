using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Inscripcion
    {
        private string _idEstudiante;
        private DateTime _fechaInscripcion;
        private EstadoCursada _estadoCursada;

        public string IdEstudiante
        {
            get { return _idEstudiante; }
            set { _idEstudiante = value; }
        }

        public DateTime FechaIncripcion
        {
            get { return _fechaInscripcion; }
            set { _fechaInscripcion = value; }
        }

        public EstadoCursada EstadoCursada
        {
            get { return _estadoCursada; }
            set { _estadoCursada = value; }
        }

        public Inscripcion()
        {
        }

        public Inscripcion(string idEstudiante, DateTime fechaInscripcion)
        {
            _idEstudiante = idEstudiante;
            _fechaInscripcion = fechaInscripcion;
        }

        public Inscripcion(string idEstudiante, DateTime fechaInscripcion, EstadoCursada estadoCursada) : this(idEstudiante, fechaInscripcion)
        {
            _estadoCursada = estadoCursada;
        }

        public Inscripcion(Estudiante estudiante, DateTime fechaInscripcion) : this(estudiante.Id, fechaInscripcion)
        {
            _estadoCursada = EstadoCursada.EnCurso;
        }

        public Inscripcion(Estudiante estudiante, DateTime fechaInscripcion, EstadoCursada estadoCursada) : this(estudiante.Id, fechaInscripcion, estadoCursada) { }

        public static bool operator ==(Inscripcion cursoInscriptoUno, Inscripcion cursoInscriptoDos)
        {

            if (cursoInscriptoUno.IdEstudiante == cursoInscriptoDos.IdEstudiante && cursoInscriptoUno.FechaIncripcion == cursoInscriptoDos.FechaIncripcion) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }

        }

        public static bool operator !=(Inscripcion cursoInscriptoUno, Inscripcion cursoInscriptoDos) { return !(cursoInscriptoUno == cursoInscriptoDos); }

    }
}
