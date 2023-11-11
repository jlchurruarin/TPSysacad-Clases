using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Curso
    {
        private string _id;
        private string _idMateria;
        private string _nombreCurso;
        private string? _idProfesor;
        private List<Horario> _horario;
        private string _aula;
        private int _cupoMaximo;
        private List<Inscripcion> _listaDeInscripciones;
        private List<string> _listaIdEstudiantesEnEspera;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string IdMateria
        {
            get { return _idMateria; }
            set { _idMateria = value; }
        }

        public string NombreCursada
        {
            get { return _nombreCurso; }
            set { _nombreCurso = value; }
        }

        public string IdProfesor
        {
            get { return _idProfesor; }
            set { _idProfesor = value; }
        }

        public List<Horario> Horario
        {
            get { return _horario; }
            set { _horario = value; }
        }

        public string Aula
        {
            get { return _aula; }
            set { _aula = value; }
        }

        public int CupoMaximo
        {
            get { return _cupoMaximo; }
            set { _cupoMaximo = value; }
        }

        public List<Inscripcion> ListaDeInscripciones
        {
            get { return _listaDeInscripciones; }
            set { _listaDeInscripciones = value; }
        }

        public List<string> ListaIdEstudiantesEnEspera
        {
            get { return _listaIdEstudiantesEnEspera; }
            set { _listaIdEstudiantesEnEspera = value; }
        }

        public Curso()
        {
            _id = Sistema.GenerarUUID();
            _idProfesor = string.Empty;
            _idMateria = string.Empty;
            _nombreCurso = string.Empty;
            _aula = string.Empty;
            _horario = new List<Horario>();
            _listaDeInscripciones = new List<Inscripcion>();
            _listaIdEstudiantesEnEspera = new List<string>();
        }

        public Curso(string idMateria, string nombreCursada, string aula, int cupoMaximo) : this()
        {
            _idMateria = idMateria;
            _nombreCurso = nombreCursada;
            _aula = aula;
            _cupoMaximo = cupoMaximo;
        }

        public Curso(string idMateria, string nombreCursada, string aula, int cupoMaximo, List<Horario> horario) : this(idMateria, nombreCursada, aula, cupoMaximo)
        {
            _horario = horario;
        }

        public Curso(string idMateria, string nombreCursada, string aula, int cupoMaximo, string idProfesor, List<Horario> horario) : this(idMateria, nombreCursada, aula, cupoMaximo, horario)
        {
            _idProfesor = idProfesor;
        }

        public override string ToString()
        {
            return $"Nombre Curso: {_nombreCurso} - Aula: {_aula} - Cupo Disponible: {_cupoMaximo - _listaDeInscripciones.Count}";
        }

        private bool CursoLleno()
        {
            if (ListaDeInscripciones.Count() < _cupoMaximo) return false;
            else { return true; }
        }

        public static bool operator +(Curso curso, Inscripcion inscripcion)
        {
            if (!curso.CursoLleno())
            {
                if (curso.ListaDeInscripciones.Any(inscripcionTemporal => inscripcionTemporal.IdEstudiante == inscripcion.IdEstudiante))
                {
                    throw new Exception("El estudiante ya está inscripto en está materia");
                }
                else
                {
                    curso.ListaDeInscripciones.Add(inscripcion);
                    return true;
                }
            }
            else
            {
                curso.ListaIdEstudiantesEnEspera.Add(inscripcion.IdEstudiante);
                return false;
            }
        }

        public static bool operator -(Curso curso, Estudiante estudiante)
        {
            Inscripcion? inscripcion = curso.ListaDeInscripciones.Find(inscripcion => inscripcion.IdEstudiante == estudiante.Id);

            if (inscripcion is null)
            {
                return false;
            }
            else
            {
                curso.ListaDeInscripciones.Remove(inscripcion);
                return true;
            }
        }


        public static bool operator ==(Curso cursoUno, Curso cursoDos)
        {
            if (cursoUno.Id == cursoDos.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Curso cursoUno, Curso cursoDos) { return !(cursoUno == cursoDos); }

    }
}
