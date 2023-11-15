using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Pago
    {

        private Pago() : this(string.Empty, ConceptoPago.Matricula, EstadoPago.Pendiente, 0m)
        {

        }
        public Pago(ConceptoPago conceptoPago,decimal monto) : this()
        {
            ConceptoDePago = conceptoPago;
            Monto = monto; 
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
