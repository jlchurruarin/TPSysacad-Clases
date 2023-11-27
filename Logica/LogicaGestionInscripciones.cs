using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaGestionInscripciones
    {
        private IGestionarInscripciones _gestionDeInscripcioneVista;

        public LogicaGestionInscripciones(IGestionarInscripciones vista)
        {
            _gestionDeInscripcioneVista = vista;
            _gestionDeInscripcioneVista.AlSolicitarEstudiantes += ObtenerUsuariosPorInscripcion;
        }

        public async Task<List<Usuario>> ObtenerUsuariosPorInscripcion(Curso curso, EstadoDeInscripcion? estadoDeInscripcion)
        {
            List<Inscripcion> inscripciones = await Inscripcion.GetInscripcionesDeCurso(curso.Id);

            if (estadoDeInscripcion != null)
            {
                inscripciones = inscripciones.FindAll(ins => ins.EstadoDeInscripcion == estadoDeInscripcion);
            }

            inscripciones.Sort();

            List<Usuario> estudiantesInscriptos = new List<Usuario>();

            foreach (Inscripcion i in inscripciones)
            {
                Usuario estudiante = await Usuario.ObtenerUsuarioPorID(TipoDeUsuario.Estudiante, i.EstudianteId);

                if (estudiante is not null) { estudiantesInscriptos.Add(estudiante); }
            }

            return estudiantesInscriptos;
        }

        public async void EliminarInscripcion(Usuario estudiante, Curso curso)
        {
            try
            {
                Inscripcion inscripcion = new Inscripcion(estudiante.Id, curso.Id, EstadoDeInscripcion.Cursando);
                await inscripcion.Delete();
                _gestionDeInscripcioneVista.OnRemoveOK();
            } catch (Exception ex)
            {
                _gestionDeInscripcioneVista.OnRemoveError(ex.Message);
            }
            
        }


    }
}
