using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Inscripcion
    {

        private Inscripcion() : this(string.Empty, string.Empty, EstadoDeInscripcion.Cursando, DateTime.Now)
        {
            
        }

        public Inscripcion(string estudianteId, string cursoId, EstadoDeInscripcion estadoDeInscripcion) : this()
        {
            CursoId = cursoId;
            EstudianteId = estudianteId;
            EstadoDeInscripcion = estadoDeInscripcion;
        }

        public Inscripcion(Usuario estudiante, Curso curso, EstadoDeInscripcion estadoDeInscripcion) : this(estudiante.Id, curso.Id, estadoDeInscripcion) { }



        public static bool operator ==(Inscripcion cursoInscriptoUno, Inscripcion cursoInscriptoDos)
        {

            if (cursoInscriptoUno.CursoId == cursoInscriptoDos.CursoId && cursoInscriptoUno.EstudianteId == cursoInscriptoDos.EstudianteId)
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
