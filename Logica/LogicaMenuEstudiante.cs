using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaMenuEstudiante
    {
        private IMenuEstudiante _menuEstudianteVista;

        public LogicaMenuEstudiante(IMenuEstudiante vista)
        {
            _menuEstudianteVista = vista;
        }

        public async void AgregarInscripcion(string estudianteId, string cursoId, DateTime fechaDeInscripcion)
        {
            try
            {
                ValidarInscripcion(estudianteId, cursoId, fechaDeInscripcion);
            }
            catch (Exception ex)
            {
                _menuEstudianteVista.OnAddError(ex.Message);
                return;
            }

            try
            {
                EstadoDeInscripcion enumEstadoDeInscripcion = EstadoDeInscripcion.Cursando;
                Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, enumEstadoDeInscripcion, fechaDeInscripcion);
                await inscripcion.Add();
                _menuEstudianteVista.OnAddOk();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("restricción PRIMARY KEY"))
                {
                    _menuEstudianteVista.OnAddError($"El estudiante ya se encuentra inscripto en el curso");
                }
                else
                {
                    _menuEstudianteVista.OnAddError(ex.Message);
                }
                return;
            }
        }

        public async Task<List<Curso>> ObtenerInscripcionesPosibles(Usuario estudiante)
        {
            List<Curso> cursosInscriptos = await Curso.GetCursosInscripto(estudiante);

            List<Materia> materiasInscriptas = new List<Materia>();

            foreach (Curso curso in cursosInscriptos)
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

        public void ValidarInscripcion(string estudianteId, string cursoId, DateTime fechaDeInscripcion)
        {
            if (string.IsNullOrEmpty(estudianteId)) { throw new Exception("Estudiante no valido"); }
            if (string.IsNullOrEmpty(cursoId)) { throw new Exception("Curso no valido"); }

        }
    }
}
