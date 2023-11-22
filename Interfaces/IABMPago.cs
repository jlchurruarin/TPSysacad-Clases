using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMPago : ISQLAddUpdateVista
    {

        public event Action<string>? AlSolicitarPago;

        public void MostrarPago(Pago? pago);

    }
}
