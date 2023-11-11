using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Usuario : SQLCrud<Usuario>, ICRUDOps<Usuario>
    {


        public string Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public bool CambioDeContraseñaObligatorio { get; set; }
        public int Dni { get; set; }
        public string NumeroDeTelefono { get; set; }
        public string Direccion { get; set; }

        public Usuario(int legajo, string nombre, string apellido, 
                        string correoElectronico, string contraseña, bool cambioDeContraseñaObligatorio, 
                        int dni, string numeroDeTelefono, string direccion) : base("Usuarios")
        {
            Id = Sistema.GenerarUUID();
            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
            Contraseña = contraseña;
            CambioDeContraseñaObligatorio = cambioDeContraseñaObligatorio;
            Dni = dni;
            NumeroDeTelefono = numeroDeTelefono;
            Direccion = direccion;
        }

        public int Add()
        {
            ConfigurarParametros();

            List<string> columnas = new List<string>
            {
                "UsuarioId",
                "Legajo",
                "Nombre",
                "Apellido",
                "CorreoElectronico",
                "Contrasenia",
                "CambioObligatorio",
                "DNI",
                "NumeroDeTelefono",
                "Direccion",
            };

            return base.Add(columnas);
        }

        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "UsuarioId", Id }
            };

            return base.Delete(camposValor);
        }

        public static List<Usuario> GetAll()
        {
            Usuario usuario = new Usuario();
            return usuario.InternalGetAll(ObtenerListaColumnasBD());
        }

        public static List<Usuario> GetAll(string[] columnas)
        {
            Usuario usuario = new Usuario();
            return usuario.InternalGetAll(columnas);
        }

        private List<Usuario> InternalGetAll(string[] columnas)
        {
            return base.GetAll(Map, columnas);
        }

        public static List<Usuario> SearchWhere(Dictionary<string, object> campoValores)
        {
            Usuario usuario = new Usuario();
            return usuario.InternalSearchWhere(ObtenerListaColumnasBD(), campoValores);
        }

        public static List<Usuario> SearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            Usuario usuario = new Usuario();
            return usuario.InternalSearchWhere(columnas, campoValores);
        }

        private List<Usuario> InternalSearchWhere(string[] columnas, Dictionary<string, object> campoValores)
        {
            return base.SearchWhere(Map, columnas, campoValores);
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            string nombreCampoID = "UsuarioId";
            string valorCampoId = Id;
            return base.Update(columnasBD, nombreCampoID, valorCampoId);
        }

        public Usuario Map(IDataRecord reader)
        {
            var id = reader["UsuarioId"].ToString() ?? "";
            var legajo = reader.GetInt32(reader.GetOrdinal("Legajo"));
            var nombre = reader["Nombre"].ToString() ?? "";
            var apellido = reader["Apellido"].ToString() ?? "";
            var correoElectronico = reader["CorreoElectronico"].ToString() ?? "";
            var contraseña = reader["Contrasenia"].ToString() ?? "";
            var cambioDeContraseñaObligatorio = reader.GetBoolean(reader.GetOrdinal("CambioObligatorio"));
            var dni = reader.GetInt32(reader.GetOrdinal("DNI"));
            var numeroDeTelefono = reader["NumeroDeTelefono"].ToString() ?? "";
            var direccion = reader["Direccion"].ToString() ?? "";

            var usuario = new Usuario(legajo, nombre, apellido, correoElectronico, contraseña, cambioDeContraseñaObligatorio, dni, numeroDeTelefono, direccion);
            usuario.Id = id;

            return usuario;
        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "UsuarioId",
                "Legajo",
                "Nombre",
                "Apellido",
                "CorreoElectronico",
                "Contrasenia",
                "CambioObligatorio",
                "DNI",
                "NumeroDeTelefono",
                "Direccion"
            ];
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@UsuarioId", SqlDbType.VarChar);
            _comando.Parameters["@UsuarioId"].Value = Id;
            _comando.Parameters.Add("@Legajo", SqlDbType.Int);
            _comando.Parameters["@Legajo"].Value = Legajo;
            _comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            _comando.Parameters["@Nombre"].Value = Nombre;
            _comando.Parameters.Add("@Apellido", SqlDbType.VarChar);
            _comando.Parameters["@Apellido"].Value = Apellido;
            _comando.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar);
            _comando.Parameters["@CorreoElectronico"].Value = CorreoElectronico;
            _comando.Parameters.Add("@Contrasenia", SqlDbType.VarChar);
            _comando.Parameters["@Contrasenia"].Value = Contraseña;
            _comando.Parameters.Add("@CambioObligatorio", SqlDbType.Bit);
            _comando.Parameters["@CambioObligatorio"].Value = CambioDeContraseñaObligatorio;
            _comando.Parameters.Add("@DNI", SqlDbType.Int);
            _comando.Parameters["@DNI"].Value = Dni;
            _comando.Parameters.Add("@NumeroDeTelefono", SqlDbType.VarChar);
            _comando.Parameters["@NumeroDeTelefono"].Value = NumeroDeTelefono;
            _comando.Parameters.Add("@Direccion", SqlDbType.VarChar);
            _comando.Parameters["@Direccion"].Value = Direccion;
        }

    }
}
