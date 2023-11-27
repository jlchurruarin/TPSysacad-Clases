using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ObjectiveC;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    [Serializable]
    internal class QueryNotWhere : Exception
    {
        public QueryNotWhere()
        {
        }

        public QueryNotWhere(string? message) : base(message)
        {
        }

        public QueryNotWhere(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QueryNotWhere(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class QueryNotSetValues : Exception
    {
        public QueryNotSetValues()
        {
        }

        public QueryNotSetValues(string? message) : base(message)
        {
        }

        public QueryNotSetValues(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QueryNotSetValues(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    public abstract class SQLCrud<T> : SQLDB<T> where T : class
    {
        private readonly string _tableName;
        private Dictionary<string, object> _where = new Dictionary<string, object>();
        private Dictionary<string, object> _set = new Dictionary<string, object>();
        private List<string> _columns = new List<string>();
        private List<string> _joins = new List<string>();


        public SQLCrud(string tableName)
        {
            _tableName = tableName;
        }

        internal List<T> InternalGetAll(Func<IDataRecord, T> mapeo)
        {
            AddColums(ObtenerListaColumnasBD());

            var query = PrepareSelectQuery();
            _comando.CommandText = query;

            List<T> resultado = ExecuteReader(query, mapeo);
            ClearQuery();
            return resultado;
        }

        internal List<T> InternalSearchWhere(Func<IDataRecord, T> mapeo, Dictionary<string, object> campoValores)
        {
            AddColums(ObtenerListaColumnasBD());

            foreach (var item in campoValores)
            {
                AddWhereCondition(item.Key, item.Value);
            }

            var query = PrepareSelectQuery();
            _comando.CommandText = query;

            List<T> result = ExecuteReader(query, mapeo);
            ClearQuery();
            return result;
        }

        public int Add()
        {
            _comando.CommandText = PrepareInsertQuery();
            int result = ExecuteNonQuery();
            ClearQuery();
            return result;
        }

        public int Delete()
        {
            _comando.CommandText = PrepareDeleteQuery();
            int result = ExecuteNonQuery();
            ClearQuery();
            return result;
        }

        public int Update()
        {
            _comando.CommandText = PrepareUpdateQuery();
            int result = ExecuteNonQuery();
            ClearQuery();
            return result;
        }

        private protected string PrepareSelectQuery()
        {
            StringBuilder query = new StringBuilder();

            var columnas = string.Join(",", _columns);

            query.AppendFormat("SELECT {0} FROM {1}", columnas, _tableName);

            if (_joins.Count > 0) 
            {
                foreach (var join in _joins)
                {
                    query.AppendFormat(" {0}", join);
                }
            }

            if (_where.Count > 0)
            {
                var where = string.Join(" AND ", _where.Select(x => $"{x.Key} = @w{x.Value}").ToArray());
                query.Append($" WHERE {where}");
            }

            return query.ToString();
        }

        private protected string PrepareInsertQuery()
        {
            if (_set.Count == 0) throw new QueryNotSetValues("No se han configurado valores a agregar");

            StringBuilder query = new StringBuilder();

            var valores = string.Join(",@s", _set.Keys).Insert(0, "@s");

            query.AppendFormat("INSERT INTO {0} VALUES ({1})", _tableName, valores);

            return query.ToString();
        }

        private protected string PrepareUpdateQuery()
        {
            if (_set.Count == 0) throw new QueryNotSetValues("No se han configurado valores a modificar");
            StringBuilder query = new StringBuilder();
            
            var set = string.Join(", ", _set.Select(x => $"{x.Key} = @s{x.Key}").ToArray());

            query.AppendFormat("UPDATE {0} SET {1}", _tableName, set);

            if (_where != null)
            {
                var where = string.Join(" AND ", _where.Select(x => $"{x.Key} = @w{x.Value}").ToArray());
                query.Append($" WHERE {where}");
            }

            return query.ToString();
        }

        private protected string PrepareDeleteQuery()
        {
            if (_where == null) throw new QueryNotWhere("No se han agregado clausulas where. Se aborta DELETE ya que borraría toda la tabla");

            StringBuilder query = new StringBuilder();

            var where = string.Join(" AND ", _where.Select(x => $"{x.Key} = @w{x.Value}").ToArray());

            query.AppendFormat("DELETE FROM {0} WHERE {1}", _tableName, where);

            return query.ToString();
        }

        private protected void AddColums(string columna)
        {
            _columns.Add($"{_tableName}.{columna}");
        }

        private protected void AddColums(string[] columnas)
        {
            foreach (var item in columnas)
            {
                AddColums(item);
            }
        }

        private protected void AddColums(string tableName, string columna)
        {
            AddColums($"{tableName}.{columna}");
        }

        private protected void AddColums(string tableName, string[] columnas)
        {
            foreach (var item in columnas)
            {
                AddColums(tableName, item);
            }
        }

        private protected void AddWhereCondition(string columna, object? valor)
        {
            _where.Add(columna, columna);
            _comando.Parameters.Add($"@w{columna}", GetSqlDbType(columna));
            if (valor != null ) _comando.Parameters[$"@w{columna}"].Value = valor;
            else _comando.Parameters[$"@w{columna}"].Value = DBNull.Value;
        }

        private protected void AddWhereCondition(string tableName, string columna, object? valor)
        {
            _where.Add($"{tableName}.{columna}", $"{tableName}{columna}");
            _comando.Parameters.Add($"@w{tableName}{columna}", GetSqlDbType(columna));
            if (valor != null) _comando.Parameters[$"@w{tableName}{columna}"].Value = valor;
            else _comando.Parameters[$"@w{tableName}{columna}"].Value = DBNull.Value;
        }

        private protected void AddSetValue(string columna, object? valor)
        {
            _set.Add(columna, valor);
            _comando.Parameters.Add($"@s{columna}", GetSqlDbType(columna));
            if (valor is null) { _comando.Parameters[$"@s{columna}"].Value = DBNull.Value; }
            else if (valor.ToString() != "") _comando.Parameters[$"@s{columna}"].Value = valor;
            else _comando.Parameters[$"@s{columna}"].Value = DBNull.Value;
        }

        private protected void AddJoin(string joinType, string externalTableName, string externalTableId, string tableId)
        {
            _joins.Add($"{joinType} {externalTableName} ON {externalTableName}.{externalTableId} = {_tableName}.{tableId}");
        }

        private void ClearQuery()
        {
            _where.Clear();
            _set.Clear();
            _comando.Parameters.Clear();
            _joins.Clear();
            _columns.Clear();
        }

        protected virtual SqlDbType GetSqlDbType(string key)
        {
            return SqlDbType.Variant;
        }

        protected abstract string[] ObtenerListaColumnasBD();
    }

}
