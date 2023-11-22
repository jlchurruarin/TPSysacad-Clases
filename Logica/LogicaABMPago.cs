using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaABMPago
    {
        private IABMPago _pagoVista;

        public LogicaABMPago(IABMPago pagoVista)
        {
            _pagoVista = pagoVista;
            _pagoVista.AlSolicitarPago += MostrarPago;
        }

        public void MostrarPago(string id)
        {
            List<Pago>? pagos = Pago.ObtenerPagoPorID(id);

            if (pagos.Count > 0) { _pagoVista.MostrarPago(pagos[0]); }
        }


    }
}
