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

        public async void AddPago(string idEstudiante, string conceptoPago, decimal monto, string estadoPago, string? metodoPago, DateTime? fechaDePago)
        {
            try
            {
                ValidarPago(idEstudiante, conceptoPago, monto, estadoPago, metodoPago, fechaDePago);
            }
            catch (Exception ex)
            {
                _pagoVista.OnAddError(ex.Message);
                return;
            }

            Enum.TryParse(typeof(ConceptoPago), conceptoPago, out object? objConceptoPago);
            Enum.TryParse(typeof(EstadoPago), estadoPago, out object? objEstadoPago);
            Enum.TryParse(typeof(MetodoPago), metodoPago, out object? objMetodoPago);

            ConceptoPago enumConceptoPago = ConceptoPago.Matricula;
            if (objConceptoPago is not null) { enumConceptoPago = (ConceptoPago)objConceptoPago; }

            EstadoPago enumEstadoPago = EstadoPago.Pendiente;
            if (objEstadoPago is not null) { enumEstadoPago = (EstadoPago)objEstadoPago; }

            MetodoPago? enumMetodoPago = null;
            if (objMetodoPago is not null) { enumMetodoPago = (MetodoPago)objMetodoPago; }

            if (enumEstadoPago == EstadoPago.Pendiente)
            {
                enumMetodoPago = null;
                fechaDePago = null;
            }

            Pago pago = new Pago(idEstudiante, enumConceptoPago, monto, enumEstadoPago, enumMetodoPago, fechaDePago);

            try
            {
                await pago.Add();
            }
            catch (Exception ex)
            {
                _pagoVista.OnAddError(ex.Message);
                return;
            }

            _pagoVista.OnAddOk();
        }

        private void ValidarPago(string idEstudiante, string conceptoPago, decimal monto, string estadoPago, string? metodoPago, DateTime? fechaDePago)
        {
            if (string.IsNullOrEmpty(idEstudiante)) { throw new Exception("Debe seleccionar un estudiante"); }
            if (string.IsNullOrEmpty(conceptoPago)) { throw new Exception("Debe seleccionar un concepto de pago"); }
            if (string.IsNullOrEmpty(estadoPago)) { throw new Exception("Debe seleccionar un estado de pago"); }

            if (monto <= 0) { throw new Exception("No se permiten numeros menores o iguales a 0"); }
        }
    }
}
