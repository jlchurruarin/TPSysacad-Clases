using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaABMInscripcionEnCurso
    {
        private IABMIncripcionEnCurso _vistaInscripcion;
        public LogicaABMInscripcionEnCurso(IABMIncripcionEnCurso vistaInscripcion)
        {
            _vistaInscripcion = vistaInscripcion;
            _vistaInscripcion.AlSolicitarCursosDisponibles += MostrarCursos;
        }

        public List<Curso> MostrarCursos(Usuario estudiante)
        {
            List<Curso> cursosInscriptos = Curso.GetCursosInscripto(estudiante);
            
            List<Materia> materiasInscriptas = new List<Materia>();

            foreach(Curso curso in  cursosInscriptos)
            {
                Materia? materia = Materia.ObtenerMateriaPorCursoID(curso.Id);
                if (materia is not null) { materiasInscriptas.Add(materia); }
            }

            List<Materia> todasLasMaterias = Materia.GetAll();
            int creditosDelEstudiante = estudiante.GetCreditosObtenidos();

            foreach (Materia m in materiasInscriptas)
            {
                todasLasMaterias.RemoveAll(materia => materia.Id == m.Id);
                todasLasMaterias.RemoveAll(materia => materia.CreditosNecesarios > creditosDelEstudiante);
            }

            List<Curso> cursosDisponibles = new List<Curso>();

            foreach (Materia m in todasLasMaterias)
            {

                Dictionary<string, object> where = new Dictionary<string, object>();
                where.Add("MateriaID", m.Id);
                List<RequisitoMateria> requisitoMateria = RequisitoMateria.SearchWhere(where);

                if (requisitoMateria.All(rm => materiasInscriptas.Any(mi => rm.MateriaRequeridaId == mi.Id)))
                {
                    List<Curso> listaCursos = Curso.ObtenerCursosPorIDMateria(m.Id);
                    cursosDisponibles.AddRange(listaCursos);
                }
            }
            
            return cursosDisponibles;
        }

        public void AgregarInscripcion(string estudianteId, string cursoId)
        {
            try
            {
                ValidarInscripcion(estudianteId, cursoId);
            }
            catch (Exception ex)
            {
                _vistaInscripcion.OnAddError(ex.Message);
                return;
            }

            try
            {
                Curso? curso = Curso.ObtenerCursoPorID(cursoId);

                if (curso is not null)
                {
                    if (curso.CupoMaximo > curso.GetCantidadIncriptos())
                    {
                        Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, EstadoDeInscripcion.Cursando);
                        inscripcion.Add();
                    } 
                    else
                    {
                        Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, EstadoDeInscripcion.EnListaDeEspera);
                        inscripcion.Add();
                    }
                    
                }
                else
                {
                    _vistaInscripcion.OnAddError("Curso no encontrado, verifique el curso seleccionado");
                }
                
            }
            catch (Exception ex)
            {
                _vistaInscripcion.OnAddError(ex.Message);
                return;
            }

            _vistaInscripcion.OnAddOk();
        }

        private void ValidarInscripcion(string estudianteId, string cursoId)
        {
            if (string.IsNullOrEmpty(estudianteId)) { throw new Exception("Debe seleccionar un estudiante"); }
            if (string.IsNullOrEmpty(cursoId)) { throw new Exception("Debe seleccionar un curso"); }
        }
    }
}
