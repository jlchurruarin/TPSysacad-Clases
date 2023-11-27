using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaGestionCursoEstudiante
    {
        private IGestionCursoEstudiante _gestionCursoVista;
        public LogicaGestionCursoEstudiante(IGestionCursoEstudiante gestionCursoVista)
        {
            _gestionCursoVista = gestionCursoVista;
            _gestionCursoVista.AlSolicitarCursos += ObtenerCursos;
        }

        public void EliminarCurso(object curso, object estudiante)
        {
            Curso? cursoSelecionado = curso as Curso;
            Usuario? estudianteSeleccionado = estudiante as Usuario;

            if (cursoSelecionado is null) { _gestionCursoVista.OnRemoveError("No se pudo eliminar el Curso, verificar curso seleccionado"); }
            else if (estudianteSeleccionado is null) { _gestionCursoVista.OnRemoveError("No se pudo eliminar el Curso, verificar estudiante seleccionado"); }
            else
            {
                Inscripcion inscripcion = new Inscripcion(estudianteSeleccionado.Id, cursoSelecionado.Id, EstadoDeInscripcion.Cursando);

                try {
                    inscripcion.Delete();
                    _gestionCursoVista.OnRemoveOk();
                }
                catch (Exception ex)
                {
                    _gestionCursoVista.OnRemoveError($"No se pudo eliminar la inscripción: {ex.Message}");
                }
            }
        }

        public List<Curso> ObtenerCursos(Usuario estudiante)
        {

            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("EstudianteID", estudiante.Id);

            List<Inscripcion> inscripciones = Inscripcion.SearchWhere(where);

            List<Curso> cursos = new List<Curso>();

            foreach (Inscripcion i in inscripciones)
            {
                Curso? c = Curso.ObtenerCursoPorID(i.CursoId);
                if (c is not null) { cursos.Add(c); }
            }

            return cursos;
        }
    }
}
