using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaMenuAdministrador
    {
        private IMenuAdministrador _menuAdministrador;

        public LogicaMenuAdministrador(IMenuAdministrador menuAdministrador)
        {
            _menuAdministrador = menuAdministrador;
            _menuAdministrador.AlSolicitarEstudiantes += MostrarEstudiantes;
            _menuAdministrador.AlSolicitarProfesores += MostrarProfesores;
            _menuAdministrador.AlSolicitarAdministradores += MostrarAdministradores;
            _menuAdministrador.AlSolicitarMaterias += MostrarMaterias;
            _menuAdministrador.AlSolicitarCursos += MostrarCursos;
        }

        public async Task MostrarEstudiantes()
        {
            List<Usuario> listaEstudiantes = await Usuario.GetAll(TipoDeUsuario.Estudiante);
            _menuAdministrador.MostrarListaEstudiantes(listaEstudiantes);
        }

        public async Task MostrarProfesores()
        {
            List<Usuario> listaProfesores = await Usuario.GetAll(TipoDeUsuario.Profesor);
            _menuAdministrador.MostrarListaProfesores(listaProfesores);
        }

        public async Task MostrarAdministradores()
        {
            List<Usuario> listaAdministradores = await Usuario.GetAll(TipoDeUsuario.Administrador);
            _menuAdministrador.MostrarListaAdministradores(listaAdministradores);
        }

        public async Task MostrarMaterias()
        {
            List<Materia> listaMaterias = await Materia.GetAll();
            _menuAdministrador.MostrarListaMaterias(listaMaterias);
        }

        public async Task MostrarCursos()
        {
            List<Curso> listaCurso = await Curso.GetAll();
            _menuAdministrador.MostrarListaCursos(listaCurso);
        }

        public async void EliminarEstudiante(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Estudiante) { throw new TipoDeUsuarioErroneoException("Error al borrar el estudiante seleccionado."); }

            try
            {
                await usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public async void EliminarProfesor(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Profesor) { throw new TipoDeUsuarioErroneoException("Error al borrar el profesor seleccionado."); }

            try
            {
                await usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public async void EliminarAdministrador(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Administrador) { throw new TipoDeUsuarioErroneoException("Error al borrar el administrador seleccionado."); }

            try
            {
                await usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public async void EliminarMateria(Materia materiaSeleccionada)
        {

            try
            {
                await materiaSeleccionada.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }

        }

        public async void EliminarCurso(Curso cursoSeleccionado)
        {
            try
            {
                await cursoSeleccionado.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public async Task<List<object>> ObtenerInformeInscripciones(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Inscripcion> listaInscripciones = await Inscripcion.GetInscripcionesPorFecha(fechaInicio, fechaFin);

            List<object> informeInscripciones = new List<object>();
            foreach (Inscripcion inscripcion in listaInscripciones)
            {
                Usuario? estudiante = await Usuario.ObtenerUsuarioPorID(TipoDeUsuario.Estudiante, inscripcion.EstudianteId);
                Curso? curso = await Curso.ObtenerCursoPorID(inscripcion.CursoId);

                if (estudiante is not null && curso is not null)
                {
                    var obj = new { 
                                    Legajo = estudiante.Legajo,
                                    Nombre = estudiante.Nombre, 
                                    Apellido = estudiante.Apellido, 
                                    DNI = estudiante.Dni,
                                    CorreoElectronico = estudiante.CorreoElectronico,
                                    EstadoDeInscripcion = Enum.GetName(typeof(EstadoDeInscripcion), inscripcion.EstadoDeInscripcion),
                                    FechaInscripcio = inscripcion.FechaInscripcion,
                                    Curso = curso.Nombre,
                                   };

                    informeInscripciones.Add(obj);
                }
                
            }

            return informeInscripciones;
        }

        public async Task<List<object>> ObtenerInformePagos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Pago> listaPagos = await Pago.GetPagosPorFecha(fechaInicio, fechaFin);

            List<object> informePagos = new List<object>();

            foreach (Pago pago in listaPagos)
            {
                Usuario? estudiante = await Usuario.ObtenerUsuarioPorID(TipoDeUsuario.Estudiante, pago.EstudianteId);

                if (estudiante is not null)
                {
                    var obj = new
                    {
                        Legajo = estudiante.Legajo,
                        Nombre = estudiante.Nombre,
                        Apellido = estudiante.Apellido,
                        DNI = estudiante.Dni,
                        CorreoElectronico = estudiante.CorreoElectronico,
                        ConceptoPago = Enum.GetName(typeof(ConceptoPago), pago.ConceptoDePago),
                        EstadoPago = Enum.GetName(typeof(EstadoPago), pago.EstadoDePago),
                        MetodoPago = Enum.GetName(typeof(MetodoPago), pago.MetodoDePago),
                        FechaPago = pago.FechaDePago,
                        Monto = pago.Monto,
                    };

                    informePagos.Add(obj);
                }

            }

            return informePagos;
        }
    }

    [Serializable]
    public class TipoDeUsuarioErroneoException : Exception
    {
        public TipoDeUsuarioErroneoException()
        {
        }

        public TipoDeUsuarioErroneoException(string? message) : base(message)
        {
        }

        public TipoDeUsuarioErroneoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TipoDeUsuarioErroneoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
