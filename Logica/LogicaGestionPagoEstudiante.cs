using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaGestionPagoEstudiante
    {
        private IGestionPagoEstudiante _gestionPagoVista;
        public LogicaGestionPagoEstudiante(IGestionPagoEstudiante gestionPagoVista)
        {
            _gestionPagoVista = gestionPagoVista;
            _gestionPagoVista.AlSolicitarPagos += ObtenerPagos;
        }

        public void EliminarPago(object pago)
        {
            Pago? pagoAEliminar = pago as Pago;

            if (pagoAEliminar is null) { _gestionPagoVista.OnRemoveError("No se pudo eliminar el pago, verificar pago seleccionado"); }

            else
            {
                try { 
                    pagoAEliminar.Delete();
                    _gestionPagoVista.OnRemoveOk();
                }
                catch (Exception ex)
                {
                    _gestionPagoVista.OnRemoveError($"No se pudo eliminar el pago: {ex.Message}");
                }
            }
        }

        public List<Pago> ObtenerPagos(Usuario estudiante)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("EstudianteID", estudiante.Id);
            return Pago.SearchWhere(where);
        }
    }
}
