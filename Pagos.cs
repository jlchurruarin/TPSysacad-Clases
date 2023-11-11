using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Pago
    {
        private ConceptoPago _conceptoPago;
        private decimal _monto;
        private EstadoDePago _estadoDePago;
        private MetodoDePago? _metodoDePago;

        public ConceptoPago ConceptoDePago
        {
            get { return _conceptoPago; }
            set { _conceptoPago = value; }
        }

        public decimal Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }

        public EstadoDePago EstadoDePago
        {
            get { return _estadoDePago; }
            set { _estadoDePago = value; }
        }

        public MetodoDePago? MetodoDePago
        {
            get { return _metodoDePago; }
            set { _metodoDePago = value; }
        }

        public Pago()
        {

        }
        public Pago(ConceptoPago conceptoPago,decimal monto)
        {
            _conceptoPago = conceptoPago;
            _monto = monto; 
        }

        public override string ToString()
        {
            string? conceptoPago = Enum.GetName(typeof(ConceptoPago), _conceptoPago);
            string? estadoPago = Enum.GetName(typeof(EstadoDePago), _estadoDePago);
            if (conceptoPago is null) { conceptoPago = "Pago sin concepto"; }
            if (estadoPago is null) { estadoPago = "Pago sin estado"; }
            return $"{conceptoPago} - ${_monto} - {estadoPago}";
        }
    }
}
