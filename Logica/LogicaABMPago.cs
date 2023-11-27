using Azure;
using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaABMPago
    {
        private ISQLAddUpdateVista _pagoVista;

        public LogicaABMPago(ISQLAddUpdateVista pagoVista)
        {
            _pagoVista = pagoVista;
        }

        public void AddPago(string idEstudiante, string conceptoPago, decimal monto)
        {
            try
            {
                ValidarPago(idEstudiante, conceptoPago, monto);
            }
            catch (Exception ex)
            {
                _pagoVista.OnAddError(ex.Message);
                return;
            }

            Enum.TryParse(typeof(ConceptoPago), conceptoPago, out object? objConceptoPago);

            ConceptoPago enumConceptoPago = (ConceptoPago)objConceptoPago;

            Pago pago = new Pago(idEstudiante, enumConceptoPago, monto);

            try
            {
                pago.Add();
            }
            catch (Exception ex)
            {
                _pagoVista.OnAddError(ex.Message);
                return;
            }

            _pagoVista.OnAddOk();
        }

        private void ValidarPago(string idEstudiante, string conceptoPago, decimal monto)
        {
            if (string.IsNullOrEmpty(idEstudiante)) { throw new Exception("Debe seleccionar un estudiante"); }
            if (string.IsNullOrEmpty(conceptoPago)) { throw new Exception("Debe seleccionar un concepto de pago"); }
            if (monto <= 0) { throw new Exception("No se permiten numeros menores o iguales a 0"); }
        }
    }
}
