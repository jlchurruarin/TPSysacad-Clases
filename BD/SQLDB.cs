using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public abstract class SQLDB<T>
    {
        private SqlConnection _conexion;
        protected static SqlCommand _comando;

        public SQLDB()
        {
            _conexion = BaseDeDatos.conexion;
            _comando = new SqlCommand();
            _comando.CommandType = CommandType.Text;
            _comando.Connection = _conexion;
        }

        public async Task<List<T>> ExecuteReader(string query, Func<IDataRecord, T> mapeo)
        {
            var lista = new List<T>();

            try
            {
                await _conexion.OpenAsync();

                _comando.CommandText = query;

                using (var reader = _comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T objeto = mapeo(reader);

                        lista.Add(objeto);
                    }

                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _conexion.CloseAsync();
            }
        }

        protected async Task<int> ExecuteNonQuery()
        {
            try
            {
                await _conexion.OpenAsync();

                var filasAfectadas = _comando.ExecuteNonQuery();

                return filasAfectadas;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await _conexion.CloseAsync();
            }
        }
    }
}
