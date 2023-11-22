using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void MostrarEstudiantes()
        {
            List<Usuario> listaEstudiantes = Usuario.GetAll(TipoDeUsuario.Estudiante);
            _menuAdministrador.MostrarListaEstudiantes(listaEstudiantes);
        }

        public void MostrarProfesores()
        {
            List<Usuario> listaProfesores = Usuario.GetAll(TipoDeUsuario.Profesor);
            _menuAdministrador.MostrarListaProfesores(listaProfesores);
        }

        public void MostrarAdministradores()
        {
            List<Usuario> listaAdministradores = Usuario.GetAll(TipoDeUsuario.Administrador);
            _menuAdministrador.MostrarListaAdministradores(listaAdministradores);
        }

        public void MostrarMaterias()
        {
            List<Materia> listaMaterias = Materia.GetAll();
            _menuAdministrador.MostrarListaMaterias(listaMaterias);
        }

        public void MostrarCursos()
        {
            List<Curso> listaCurso = Curso.GetAll();
            _menuAdministrador.MostrarListaCursos(listaCurso);
        }

        public void EliminarEstudiante(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Estudiante) { throw new Exception("Error al borrar el estudiante seleccionado."); }

            try
            {
                usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public void EliminarProfesor(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Profesor) { throw new Exception("Error al borrar el profesor seleccionado."); }

            try
            {
                usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public void EliminarAdministrador(Usuario usuario)
        {
            if (usuario.TipoDeUsuario != TipoDeUsuario.Administrador) { throw new Exception("Error al borrar el administrador seleccionado."); }

            try
            {
                usuario.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }

        public void EliminarMateria(Materia materiaSeleccionada)
        {

            try
            {
                materiaSeleccionada.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }

        }

        public void EliminarCurso(Curso cursoSeleccionado)
        {
            try
            {
                cursoSeleccionado.Delete();
                _menuAdministrador.OnRemoveOk();
            }
            catch (Exception ex) { _menuAdministrador.OnRemoveError(ex.Message); }
        }
    }
}
