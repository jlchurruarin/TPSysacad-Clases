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

        public async Task<List<Curso>> MostrarCursos(Usuario estudiante)
        {
            List<Curso> cursosInscriptos = await Curso.GetCursosInscripto(estudiante);
            
            List<Materia> materiasInscriptas = new List<Materia>();

            foreach(Curso curso in  cursosInscriptos)
            {
                Materia? materia = await Materia.ObtenerMateriaPorCursoID(curso.Id);
                if (materia is not null) { materiasInscriptas.Add(materia); }
            }

            List<Materia> todasLasMaterias = await Materia.GetAll();
            int creditosDelEstudiante = await estudiante.GetCreditosObtenidos();

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
                List<RequisitoMateria> requisitoMateria = await RequisitoMateria.SearchWhere(where);

                if (requisitoMateria.All(rm => materiasInscriptas.Any(mi => rm.MateriaRequeridaId == mi.Id)))
                {
                    List<Curso> listaCursos = await Curso.ObtenerCursosPorIDMateria(m.Id);
                    cursosDisponibles.AddRange(listaCursos);
                }
            }
            
            return cursosDisponibles;
        }

        public async void AgregarInscripcion(string estudianteId, string cursoId)
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
                Curso? curso = await Curso.ObtenerCursoPorID(cursoId);

                if (curso is not null)
                {
                    if (curso.CupoMaximo > await curso.GetCantidadIncriptos())
                    {
                        Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, EstadoDeInscripcion.Cursando);
                        await inscripcion.Add();
                    } 
                    else
                    {
                        Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, EstadoDeInscripcion.EnListaDeEspera);
                        await inscripcion.Add();
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
