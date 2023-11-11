using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public class SQLCrud<T> : SQLDB<T>
    {
        private readonly string _tableName;

        public SQLCrud(string tableName)
        {
            _tableName = tableName;
        }

        public List<T> GetAll(Func<IDataRecord, T> mapeo, string[] columnas)
        {
            var query = PrepareQuery(columnas);
            _comando.CommandText = query;
            _comando.Parameters.Clear();

            return ExecuteReader(query, mapeo);
        }

        public List<T> SearchWhere(Func<IDataRecord, T> mapeo, string[] columnas, Dictionary<string, object> campoValores)
        {
            var select = PrepareQuery(columnas);
            var where = string.Join(" AND ", campoValores.Select(x => $"{x.Key} = @{x.Key}").ToArray());
            var query = $"{select} WHERE {where}";
            _comando.CommandText = query;
            _comando.Parameters.Clear();

            foreach ( var item in campoValores )
            {
                _comando.Parameters.AddWithValue($"@{item.Key}", item.Value);
            }
            return ExecuteReader(query, mapeo);
        }

        public int Add(List<string> campos)
        {
            var valores = string.Join(",@", campos).Insert(0, "@");

            _comando.CommandText = $"INSERT INTO {_tableName} VALUES ({valores})";

            return ExecuteNonQuery();
        }

        public int Delete(Dictionary<string, object> campoValores)
        {
            var where = string.Join(" AND ", campoValores.Select(x => $"{x.Key} = @{x.Key}").ToArray());
            
            _comando.CommandText = $"DELETE FROM {_tableName} WHERE {where}";
            _comando.Parameters.Clear();

            foreach (var item in campoValores)
            {
                _comando.Parameters.AddWithValue($"@{item.Key}", item.Value);
            }

            return ExecuteNonQuery();
        }

        public int Update(string[] columnasBD, string nombreCampoId, string valorCampoId)
        {
            var valores = string.Join(",@", columnasBD).Insert(0, "@");
            _comando.CommandText = $"INSERT INTO {_tableName} VALUES ({valores}) WHERE {nombreCampoId} == @id";
            _comando.Parameters.Clear();

            _comando.Parameters["@id"].Value = valorCampoId;

            return ExecuteNonQuery();
        }


        protected string PrepareQuery(string[] columnas)
        {
            var cols = string.Join(",", columnas);

            var query = $"SELECT {cols} FROM {_tableName}";

            return query;
        }

    }
}
