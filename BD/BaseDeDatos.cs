using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{

    internal class BaseDeDatos
    {
        public static SqlConnection conexion;
        private static string cadenaConexion;
        private static SqlCommand comando;

        static BaseDeDatos()
        {
            //cadenaConexion = @"Server=localhost;Database=Sysacad;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";
            cadenaConexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=Sysacad;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";
            conexion = new SqlConnection(cadenaConexion);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.Connection = conexion;
        }

    }
}
