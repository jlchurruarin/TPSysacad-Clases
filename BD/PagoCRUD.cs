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
        public decimal Monto { get; set; }

        public string DisplayText
        {
            get
            {
                return $"DisplayText: {ToString()}";
            }
        }

        public Pago(string id, string estudianteId, ConceptoPago conceptoDePago, EstadoPago estadoDePago, decimal monto) : base("Pagos") 
        {
            Id = id;
            EstudianteId = estudianteId;
            ConceptoDePago = conceptoDePago;
            EstadoDePago = estadoDePago;
            Monto = monto;
        }

        public Pago(string id, string estudianteId, ConceptoPago conceptoDePago, EstadoPago estadoDePago, MetodoPago? metodoDePago, decimal monto) : this(id, estudianteId, conceptoDePago, estadoDePago, monto)
        {
            MetodoDePago = metodoDePago;
        }


        public new int Add()
        {
            AddSetValue("PagoID", Id);
            AddSetValue("EstudianteID", EstudianteId);
            AddSetValue("ConceptoDePagoID", ConceptoDePago);
            AddSetValue("EstadoPagoID", EstadoDePago);
            AddSetValue("MetodoPagoID", MetodoDePago);
            AddSetValue("Monto", Monto);

            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("PagoID", Id);

            return base.Delete();
        }

        public new int Update()
        {
            AddWhereCondition("PagoID", Id);

            AddSetValue("EstudianteID", EstudianteId);
            AddSetValue("ConceptoDePagoID", ConceptoDePago);
            AddSetValue("EstadoPagoID", EstadoDePago);
            AddSetValue("MetodoPagoID", MetodoDePago);
            AddSetValue("Monto", Monto);

            return base.Update();
        }

        public static List<Pago> ObtenerPagoPorID(string id)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("PagoID", id);

            Pago pago = new Pago();
            return pago.InternalSearchWhere(pago.Map, where);
        }

        public static List<Pago> GetAll()
        {
            Pago pago = new Pago();
            return pago.InternalGetAll(pago.Map);
        }

        public static List<Pago> SearchWhere(Dictionary<string, object> campoValores)
        {
            Pago pago = new Pago();
            return pago.InternalSearchWhere(pago.Map, campoValores);
        }

        public Pago Map(IDataRecord reader)
        {
            var pagoId = reader["PagoID"].ToString() ?? "";
            var estudianteId = reader["EstudianteID"].ToString() ?? "";
            var conceptoDePago = (ConceptoPago) reader.GetByte(reader.GetOrdinal("ConceptoDePagoID"));
            var estadoDePago = (EstadoPago) reader.GetByte(reader.GetOrdinal("EstadoPagoID"));
            var metodoDePago = reader.IsDBNull(4) ? null : (MetodoPago?) reader.GetByte(reader.GetOrdinal("MetodoPagoID"));
            var monto = reader.GetDecimal(reader.GetOrdinal("Monto"));

            var curso = new Pago(pagoId, estudianteId, conceptoDePago, estadoDePago, metodoDePago, monto);

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
            else if (key == "MetodoPagoID") retorno = SqlDbType.TinyInt;
            else if (key == "Monto") retorno = SqlDbType.Decimal;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
