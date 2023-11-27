using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IGestionPagoEstudiante
    {

        public event Func<Usuario, Task<List<Pago>>>? AlSolicitarPagos;

        public void MostrarListaPagos(List<Pago> listaPagos);
        public void OnRemoveOk();
        public void OnRemoveError(string errorMessage);
    }
}
