using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IMenuAdministrador
    {
        public event Action? AlSolicitarEstudiantes;
        public event Action? AlSolicitarProfesores;
        public event Action? AlSolicitarAdministradores;
        public event Action? AlSolicitarMaterias;
        public event Action? AlSolicitarCursos;

        public void MostrarListaEstudiantes(List<Usuario> listaEstudiantes);
        public void MostrarListaProfesores(List<Usuario> listaEstudiantes);
        public void MostrarListaAdministradores(List<Usuario> listaEstudiantes);
        public void MostrarListaMaterias(List<Materia> listaEstudiantes);
        public void MostrarListaCursos(List<Curso> listaEstudiantes);
        public void OnRemoveOk();
        public void OnRemoveError(string errorMessage);
    }
}
