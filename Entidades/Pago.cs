using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Pago
    {

        private Pago() : this(Sistema.GenerarUUID(), string.Empty, ConceptoPago.Matricula, 0m, EstadoPago.Pendiente, null, null)
        {

        }
        public Pago(Usuario estudiante, ConceptoPago conceptoPago, decimal monto) : this(estudiante.Id, conceptoPago, monto, EstadoPago.Pendiente, null, null) { }

        public Pago(string estudianteId, ConceptoPago conceptoPago, decimal monto, EstadoPago estadoPago, MetodoPago? metodoPago, DateTime? fechaPago) : this() 
        {
            EstudianteId = estudianteId;
            ConceptoDePago = conceptoPago;
            Monto = monto;
            EstadoDePago = estadoPago;
            MetodoDePago = metodoPago;
            FechaDePago = fechaPago;
        }

        public override string ToString()
        {
            string? conceptoPago = Enum.GetName(typeof(ConceptoPago), ConceptoDePago);
            string? estadoPago = Enum.GetName(typeof(EstadoPago), EstadoDePago);
            if (conceptoPago is null) { conceptoPago = "Pago sin concepto"; }
            if (estadoPago is null) { estadoPago = "Pago sin estado"; }
            return $"{conceptoPago} - ${Monto} - {estadoPago}";
        }
    }
}
