using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IGestionHorarios : ISQLAddUpdateVista
    {
        public event Func<Curso, List<HorarioCurso>>? AlSolicitarHorarios;

        public void MostrarListaHorarios(List<HorarioCurso> listaHorarios);
        public void OnRemoveOk();
        public void OnRemoveError(string errorMessage);

    }
}
