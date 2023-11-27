using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMIncripcionEnCurso
    {
        public event Func<Usuario, List<Curso>>? AlSolicitarCursosDisponibles;

        public void MostrarListaCursosDisponibles(List<Curso> listaCursos);

        public void OnAddOk();
        public void OnAddListaEsperaOk();
        public void OnAddError(string errorMessage);

    }
}
