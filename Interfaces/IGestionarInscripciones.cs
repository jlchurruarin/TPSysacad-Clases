using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IGestionarInscripciones
    {
        public event Func<Curso, EstadoDeInscripcion?, Task<List<Usuario>>>? AlSolicitarEstudiantes;

        public void OnRemoveOK();
        public void OnRemoveError(string errorMessage);
    }
}
