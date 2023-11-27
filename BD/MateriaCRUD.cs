using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Materia : SQLCrud<Materia>, ICRUDOps<Materia>
    {

        public string Id { get; internal set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CreditosBrindados { get; set; }
        public int CreditosNecesarios { get; set; }

        public string DisplayText
        {
            get
            {
                return $"DisplayText: {ToString()}";
            }
        }

        public Materia(string id, string nombre, string descripcion, int creditosBrindados, int creditosNecesarios) : base("Materias")
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            CreditosBrindados = creditosBrindados;
            CreditosNecesarios = creditosNecesarios;
        }

        public new int Add()
        {
            AddSetValue("MateriaID", Id);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Descripcion", Descripcion);
            AddSetValue("CreditosBrindados", CreditosBrindados);
            AddSetValue("CreditosNecesarios", CreditosNecesarios);
            return base.Add();
        }


        public new int Delete()
        {
            AddWhereCondition("MateriaID", Id);
            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("Nombre", Nombre);
            AddSetValue("Descripcion", Descripcion);
            AddSetValue("CreditosBrindados", CreditosBrindados);
            AddSetValue("CreditosNecesarios", CreditosNecesarios);

            AddWhereCondition("MateriaID", Id);

            return base.Update();
        }

        public static Materia? ObtenerMateriaPorID(string id)
        {
            Materia materia = new Materia();
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("MateriaID", id);
            List<Materia> materias = materia.InternalSearchWhere(materia.Map, where);

            if (materias.Count == 0) return null;

            return materias[0];
        }

        public static Materia? ObtenerMateriaPorCursoID(string id)
        {
            Materia materia = new Materia();
            materia.AddJoin("INNER JOIN", "MateriaCurso", "MateriaID", "MateriaID");
            materia.AddWhereCondition("MateriaCurso", "CursoID", id);
            List<Materia> materias = materia.InternalSearchWhere(materia.Map, new Dictionary<string, object>());

            if (materias.Count == 0) return null;

            return materias[0];
        }

        public static List<Materia> GetAll()
        {
            Materia mat = new Materia();
            return mat.InternalGetAll(mat.Map);
        }

        public List<Materia> GetMateriasRequeridas()
        {
            AddJoin("INNER JOIN", "RequisitosMaterias", "MateriaID", "RequisitoMateriaID");
            AddWhereCondition("RequisitosMaterias", "MateriaID", Id);
            return InternalSearchWhere(Map, new Dictionary<string, object>());
        }

        public static List<Materia> SearchWhere(Dictionary<string, object> campoValores)
        {
            Materia mat = new Materia();
            return mat.InternalSearchWhere(mat.Map, campoValores);
        }

        public Materia Map(IDataRecord reader)
        {
            var id = reader["MateriaID"].ToString() ?? "";
            var nombre = reader["Nombre"].ToString() ?? "";
            var descripcion = reader["Descripcion"].ToString() ?? "";
            var creditosBrindados = reader.GetInt32(reader.GetOrdinal("CreditosBrindados"));
            var creditosNecesarios = reader.GetInt32(reader.GetOrdinal("CreditosNecesarios"));

            var materia = new Materia(id, nombre, descripcion, creditosBrindados, creditosNecesarios);

            return materia;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return 
            [ 
                "MateriaID",
                "Nombre",
                "Descripcion",
                "CreditosBrindados",
                "CreditosNecesarios"

            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "MateriaID") retorno = SqlDbType.VarChar;
            else if (key == "Nombre") retorno = SqlDbType.VarChar;
            else if (key == "Descripcion") retorno = SqlDbType.VarChar;
            else if (key == "CreditosBrindados") retorno = SqlDbType.Int;
            else if (key == "CreditosNecesarios") retorno = SqlDbType.Int;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}

