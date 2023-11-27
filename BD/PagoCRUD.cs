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
        public string Id { get; internal set; }
        public string EstudianteId { get; set; }
        public ConceptoPago ConceptoDePago { get; set; }
        public EstadoPago EstadoDePago { get; set; }
        public MetodoPago? MetodoDePago { get; set; }
        public DateTime? FechaDePago { get; set; }
        public decimal Monto { get; set; }

        public string DisplayText
        {
            get
            {
                return $"DisplayText: {ToString()}";
            }
        }

        public Pago(string id, string estudianteId, ConceptoPago conceptoDePago, decimal monto, EstadoPago estadoDePago) : base("Pagos") 
        {
            Id = id;
            EstudianteId = estudianteId;
            ConceptoDePago = conceptoDePago;
            EstadoDePago = estadoDePago;
            Monto = monto;
        }

        public Pago(string id, string estudianteId, ConceptoPago conceptoDePago, decimal monto, EstadoPago estadoDePago, MetodoPago? metodoDePago, DateTime? fechaDePago) : this(id, estudianteId, conceptoDePago, monto, estadoDePago)
        {
            MetodoDePago = metodoDePago;
            FechaDePago = fechaDePago;
        }


        public new async Task<int> Add()
        {
            AddSetValue("PagoID", Id);
            AddSetValue("EstudianteID", EstudianteId);
            AddSetValue("ConceptoDePagoID", ConceptoDePago);
            AddSetValue("EstadoPagoID", EstadoDePago);
            AddSetValue("MetodoPagoID", MetodoDePago);
            AddSetValue("FechaDePago", FechaDePago);
            AddSetValue("Monto", Monto);

            return await base.Add();
        }

        public new async Task<int> Delete()
        {
            AddWhereCondition("PagoID", Id);

            return await base.Delete();
        }

        public new async Task<int> Update()
        {
            AddWhereCondition("PagoID", Id);

            AddSetValue("EstudianteID", EstudianteId);
            AddSetValue("ConceptoDePagoID", ConceptoDePago);
            AddSetValue("EstadoPagoID", EstadoDePago);
            AddSetValue("MetodoPagoID", MetodoDePago);
            AddSetValue("FechaDePago", FechaDePago);
            AddSetValue("Monto", Monto);

            return await base.Update();
        }

        public static async Task<List<Pago>> ObtenerPagoPorID(string id)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("PagoID", id);

            Pago pago = new Pago();
            return await pago.InternalSearchWhere(pago.Map, where);
        }

        public static async Task<List<Pago>> ObtenerPagoPorEstudiante(string estudianteId)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("EstudianteID", estudianteId);

            Pago pago = new Pago();
            List<Pago> pagosPendientes = await pago.InternalSearchWhere(pago.Map, where);
            pagosPendientes.RemoveAll(item => item.EstadoDePago == EstadoPago.Pagado);
            return pagosPendientes;
        }

        public static async Task<List<Pago>> GetAll()
        {
            Pago pago = new Pago();
            return await pago.InternalGetAll(pago.Map);
        }

        public static async Task<List<Pago>> SearchWhere(Dictionary<string, object> campoValores)
        {
            Pago pago = new Pago();
            return await pago.InternalSearchWhere(pago.Map, campoValores);
        }

        public static async Task<List<Pago>> GetPagosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Pago> pagos = await Pago.GetAll();

            DateTime fInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
            DateTime fFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 0, 0, 0);

            pagos.RemoveAll(item => item.FechaDePago is null);
            pagos.RemoveAll(item => DateTime.Compare((DateTime) item.FechaDePago, fInicio) < 0);
            pagos.RemoveAll(item => DateTime.Compare((DateTime) item.FechaDePago, fFin) > 0);

            return pagos;
        }

        public Pago Map(IDataRecord reader)
        {
            var pagoId = reader["PagoID"].ToString() ?? "";
            var estudianteId = reader["EstudianteID"].ToString() ?? "";
            var conceptoDePago = (ConceptoPago) reader.GetByte(reader.GetOrdinal("ConceptoDePagoID"));
            var estadoDePago = (EstadoPago) reader.GetByte(reader.GetOrdinal("EstadoPagoID"));
            var metodoDePago = reader.IsDBNull(4) ? null : (MetodoPago?) reader.GetByte(reader.GetOrdinal("MetodoPagoID"));
            var fechaDeInscripcion = reader.IsDBNull(5) ? null : (DateTime?) reader.GetDateTime(reader.GetOrdinal("FechaDePago"));
            var monto = reader.GetDecimal(reader.GetOrdinal("Monto"));


            var curso = new Pago(pagoId, estudianteId, conceptoDePago, monto, estadoDePago, metodoDePago, fechaDeInscripcion);

            return curso;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "PagoID",
                "EstudianteID",
                "ConceptoDePagoID",
                "EstadoPagoID",
                "MetodoPagoID",
                "FechaDePago",
                "Monto"
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "PagoID") retorno = SqlDbType.VarChar;
            else if (key == "EstudianteID") retorno = SqlDbType.VarChar;
            else if (key == "ConceptoDePagoID") retorno = SqlDbType.TinyInt;
            else if (key == "EstadoPagoID") retorno = SqlDbType.TinyInt;
            else if (key == "FechaDePago") retorno = SqlDbType.Date;
            else if (key == "MetodoPagoID") retorno = SqlDbType.TinyInt;
            else if (key == "Monto") retorno = SqlDbType.Decimal;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
