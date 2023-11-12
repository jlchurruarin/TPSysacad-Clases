using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Curso
    {

        private Curso() : this(Sistema.GenerarUUID(), string.Empty, string.Empty, string.Empty, 0)
        {
        }

        public Curso(string materiaID, string nombreCursada, string aula, int cupoMaximo) : this()
        {
            MateriaId = materiaID;
            Nombre = nombreCursada;
            Aula = aula;
            CupoMaximo = cupoMaximo;
        }

        public Curso(Materia materia, string nombreCursada, string aula, int cupoMaximo) : this(materia.Id, nombreCursada, aula, cupoMaximo)
        {
        }

        public override string ToString()
        {
            return $"Nombre Curso: {Nombre} - Aula: {Aula} - Cupo Maximo: {CupoMaximo}";
        }

        private bool CursoLleno()
        {
            if (GetCantidadIncriptos() < CupoMaximo) return false;
            else { return true; }
        }

        public static bool operator +(Curso curso, Inscripcion inscripcion)
        {
            if (!curso.CursoLleno())
            {
                //TODO
                /*
                if (curso.GetIncriptos().Any(inscripcionTemporal => inscripcionTemporal.IdEstudiante == inscripcion.IdEstudiante))
                {
                    throw new Exception("El estudiante ya está inscripto en está materia");
                }
                else
                {
                    curso.ListaDeInscripciones.Add(inscripcion);
                    return true;
                }
                */
                return true;
            }
            else
            {
                /*
                curso.ListaIdEstudiantesEnEspera.Add(inscripcion.IdEstudiante);
                */
                return false;
            }
        }

        public static bool operator -(Curso curso, Estudiante estudiante)
        {
            //TODO
            /*
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
            */
            return true;
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
