using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Pago : SQLCrud<Pago>, ICRUDOps<Pago>
    {
        public string EstudianteId { get; set; }
        public ConceptoPago ConceptoDePago { get; set; }
        public EstadoPago EstadoDePago { get; set; }
        public MetodoPago? MetodoDePago { get; set; }
        public decimal Monto { get; set; }


        public Pago(string estudianteId, ConceptoPago conceptoDePago, EstadoPago estadoDePago, decimal monto) : base("Pagos") 
        {
            EstudianteId = estudianteId;
            ConceptoDePago = conceptoDePago;
            EstadoDePago = estadoDePago;
        }

        public Pago(string estudianteId, ConceptoPago conceptoDePago, EstadoPago estadoDePago, MetodoPago? metodoDePago, decimal monto) : this(estudianteId, conceptoDePago, estadoDePago, monto)
        {
            MetodoDePago = metodoDePago;
        }


        public int Add()
        {
            ConfigurarParametros();

            string[] columnasBD = ObtenerListaColumnasBD();

            return base.Add();
        }

        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "EstudianteID", EstudianteId },
                { "ConceptoDePagoID", ConceptoDePago },
                { "EstadoPagoID", EstadoDePago },
                { "Monto", Monto}
            };

            if (MetodoDePago != null) { camposValor.Add("MetodoPagoID", MetodoDePago); }

            return base.Delete();
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "EstudianteID", EstudianteId },
                { "ConceptoDePagoID", (int) ConceptoDePago },
                { "EstadoPagoID", (int) EstadoDePago },
            };

            if (MetodoDePago != null)
            {
                camposValor.Add("MetodoPagoID", (int) MetodoDePago);
            }
            else
            {
                camposValor.Add("MetodoPagoID", DBNull.Value);
            }

            return base.Update();
        }

        public Pago Map(IDataRecord reader)
        {
            var estudianteId = reader["CursoID"].ToString() ?? "";
            var conceptoDePago = (ConceptoPago) reader.GetByte(reader.GetOrdinal("ConceptoDePagoID"));
            var estadoDePago = (EstadoPago) reader.GetByte(reader.GetOrdinal("EstadoPagoID"));
            var metodoDePago = (MetodoPago?) reader.GetByte(reader.GetOrdinal("MetodoPagoID"));
            var monto = reader.GetDecimal(reader.GetOrdinal("Monto"));

            var curso = new Pago(estudianteId, conceptoDePago, estadoDePago, metodoDePago, monto);

            return curso;
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@EstudianteID", SqlDbType.VarChar);
            _comando.Parameters["@EstudianteID"].Value = EstudianteId;
            _comando.Parameters.Add("@ConceptoDePagoID", SqlDbType.TinyInt);
            _comando.Parameters["@ConceptoDePagoID"].Value = (int) ConceptoDePago;
            _comando.Parameters.Add("@EstadoPagoID", SqlDbType.TinyInt);
            _comando.Parameters["@EstadoPagoID"].Value = (int) EstadoDePago;
            _comando.Parameters.Add("@MetodoPagoID", SqlDbType.TinyInt);
            if (MetodoDePago != null) { 
                _comando.Parameters["@MetodoPagoID"].Value = (int?) MetodoDePago;
            } else
            {
                _comando.Parameters["@MetodoPagoID"].Value = DBNull.Value;
            }
            _comando.Parameters.Add("@Monto", SqlDbType.Decimal);
            _comando.Parameters["@Monto"].Value = Monto;
        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "EstudianteID",
                "ConceptoDePagoID",
                "EstadoPagoID",
                "MetodoPagoID",
                "Monto"
            ];
        }
    }
}
