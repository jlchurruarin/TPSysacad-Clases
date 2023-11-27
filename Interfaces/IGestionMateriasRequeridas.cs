using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IGestionMateriasRequeridas : ISQLAddUpdateVista
    {
        public event Func<Materia, Task<List<Materia>>>? AlSolicitarMateria;

        public void MostrarListaMaterias(List<Materia> listaMaterias);
        public void OnRemoveOk();
        public void OnRemoveError(string errorMessage);
    }
}
