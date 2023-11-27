using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IGestionCursoEstudiante
    {

        public event Func<Usuario, Task<List<Curso>>>? AlSolicitarCursos;

        public void MostrarListaCursos(List<Curso> listaCursos);
        public void OnRemoveOk();
        public void OnRemoveError(string errorMessage);
    }
}
