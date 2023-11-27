using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMInscripcion : ISQLAddUpdateVista
    {
        public event Action<Curso, Usuario>? AlSolicitarInscripcion;

        public void MostrarInscripcion(Inscripcion inscripcion);
    }
}
