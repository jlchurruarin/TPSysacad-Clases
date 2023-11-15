using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public interface ICRUDOps<T> where T : class
    {

        int Add();
        int Delete();
        int Update();

        static List<T> GetAll() => throw new NotImplementedException();
        static List<T> SearchWhere() => throw new NotImplementedException();

        static SqlDbType GetSqlDbType(string key) => throw new NotImplementedException();

        T Map(IDataRecord reader);

    }
}
