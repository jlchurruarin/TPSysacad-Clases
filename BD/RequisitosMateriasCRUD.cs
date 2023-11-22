using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public class RequisitoMateria : SQLCrud<RequisitoMateria>, ICRUDOps<RequisitoMateria>
    {
        public string MateriaId { get; }
        public string MateriaRequeridaId { get; }

        private RequisitoMateria() : base("RequisitosMaterias") { }

        public RequisitoMateria(string materiaId, string materiaRequeridaId) : this()
        {
            MateriaId = materiaId;
            MateriaRequeridaId = materiaRequeridaId;
        }

        public RequisitoMateria(Materia materia, Materia materiaRequerida) : this(materia.Id, materiaRequerida.Id) { }

        public RequisitoMateria(Materia materia, string materiaRequeridaId) : this(materia.Id, materiaRequeridaId) { }

        public RequisitoMateria(string materiaId, Materia materiaRequerida) : this(materiaId, materiaRequerida.Id) { }

        public new int Add()
        {
            AddSetValue("MateriaID", MateriaId);
            AddSetValue("MateriaRequeridaID", MateriaRequeridaId);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("MateriaID", MateriaId);
            AddWhereCondition("MateriaRequeridaID", MateriaRequeridaId);

            return base.Delete();
        }

        public static List<RequisitoMateria> GetAll()
        {
            RequisitoMateria rm = new RequisitoMateria();
            return rm.InternalGetAll(rm.Map);
        }

        public static List<RequisitoMateria> SearchWhere(Dictionary<string, object> campoValores)
        {
            RequisitoMateria rm = new RequisitoMateria();
            return rm.InternalSearchWhere(rm.Map, campoValores);
        }

        public RequisitoMateria Map(IDataRecord reader)
        {
            var materiaId = reader["MateriaID"].ToString() ?? "";
            var materiaRequeridaId = reader["MateriaRequeridaID"].ToString() ?? "";

            var requisitoMateria = new RequisitoMateria(materiaId, materiaRequeridaId);

            return requisitoMateria;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "MateriaID",
                "MateriaRequeridaID",
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "MateriaID") retorno = SqlDbType.VarChar;
            else if (key == "MateriaRequeridaID") retorno = SqlDbType.VarChar;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
