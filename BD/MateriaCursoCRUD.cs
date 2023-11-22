using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public class MateriaCurso : SQLCrud<MateriaCurso>, ICRUDOps<MateriaCurso>
    {
        public string MateriaId { get; }
        public string CursoId { get; }

        private MateriaCurso() : base("MateriaCurso") { }

        public MateriaCurso(string materiaId, string cursoId) : this()
        {
            MateriaId = materiaId;
            CursoId = cursoId;
        }

        public new int Add()
        {
            AddSetValue("MateriaID", MateriaId);
            AddSetValue("CursoID", CursoId);
            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("MateriaID", MateriaId);
            AddWhereCondition("CursoID", CursoId);

            return base.Delete();
        }

        public static List<MateriaCurso> GetAll()
        {
            MateriaCurso mc = new MateriaCurso();
            return mc.InternalGetAll(mc.Map);
        }

        public static List<MateriaCurso> SearchWhere(Dictionary<string, object> campoValores)
        {
            MateriaCurso mc = new MateriaCurso();
            return mc.InternalSearchWhere(mc.Map, campoValores);
        }

        public MateriaCurso Map(IDataRecord reader)
        {
            var materiaId = reader["MateriaID"].ToString() ?? "";
            var cursoId = reader["CursoID"].ToString() ?? "";

            var requisitoMateria = new MateriaCurso(materiaId, cursoId);

            return requisitoMateria;
        }

        protected override string[] ObtenerListaColumnasBD()
        {
            return
            [
                "MateriaID",
                "CursoID",
            ];
        }

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "MateriaID") retorno = SqlDbType.VarChar;
            else if (key == "CursoID") retorno = SqlDbType.VarChar;
            else retorno = SqlDbType.Variant;

            return retorno;
        }
    }
}
