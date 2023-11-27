using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaABMInscripcion
    {
        private IABMInscripcion _inscripcionVista;
        public LogicaABMInscripcion(IABMInscripcion vista)
        {
            _inscripcionVista = vista;
            _inscripcionVista.AlSolicitarInscripcion += ObtenerInscripcion;
        }

        private async void ObtenerInscripcion(Curso curso, Usuario estudiante) 
        {
            Inscripcion? inscripcion = await Inscripcion.GetInscripcion(curso.Id, estudiante.Id);

            if (inscripcion is not null ) { _inscripcionVista.MostrarInscripcion(inscripcion); }
        }

        public async void AgregarInscripcion(string estudianteId, string cursoId, string estadoDeInscripcion, DateTime fechaDeInscripcion)
        {
            try
            {
                ValidarInscripcion(estudianteId, cursoId, estadoDeInscripcion, fechaDeInscripcion);
            } 
            catch (Exception ex)
            {
                _inscripcionVista.OnAddError(ex.Message);
                return;
            }

            try
            {
                Enum.TryParse(typeof(EstadoDeInscripcion), estadoDeInscripcion, out object? objEstadoDeInscripcion);
                EstadoDeInscripcion enumEstadoDeInscripcion = (EstadoDeInscripcion)objEstadoDeInscripcion;
                Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, enumEstadoDeInscripcion, fechaDeInscripcion);
                await inscripcion.Add();
                _inscripcionVista.OnAddOk();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("restricción PRIMARY KEY"))
                {
                    _inscripcionVista.OnAddError($"El estudiante ya se encuentra inscripto en el curso");
                }
                else
                {
                    _inscripcionVista.OnAddError(ex.Message);
                }
                return;
            }
        }

        public async void ModificarInscripcion(string estudianteId, string cursoId, string estadoDeInscripcion, DateTime fechaDeInscripcion)
        {
            try
            {
                ValidarInscripcion(estudianteId, cursoId, estadoDeInscripcion, fechaDeInscripcion);
            }
            catch (Exception ex)
            {
                _inscripcionVista.OnUpdateError(ex.Message);
                return;
            }

            try
            {
                Enum.TryParse(typeof(EstadoDeInscripcion), estadoDeInscripcion, out object? objEstadoDeInscripcion);
                EstadoDeInscripcion enumEstadoDeInscripcion = (EstadoDeInscripcion)objEstadoDeInscripcion;
                Inscripcion inscripcion = new Inscripcion(estudianteId, cursoId, enumEstadoDeInscripcion, fechaDeInscripcion);
                await inscripcion.Update();
                _inscripcionVista.OnUpdateOk();
            }
            catch (Exception ex)
            {
                _inscripcionVista.OnUpdateError(ex.Message);
            }
        }

        public void ValidarInscripcion(string estudianteId, string cursoId, string estadoDeInscripcion, DateTime fechaDeInscripcion)
        {
            if (string.IsNullOrEmpty(estudianteId)) { throw new Exception("Estudiante no valido"); }
            if (string.IsNullOrEmpty(cursoId)) { throw new Exception("Curso no valido"); }
            
        }
    }
}
