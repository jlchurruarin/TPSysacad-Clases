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
        public TipoDeUsuario TipoDeUsuario { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public bool CambioDeContraseñaObligatorio { get; set; }
        public int Dni { get; set; }
        public string NumeroDeTelefono { get; set; }
        public string Direccion { get; set; }

        protected Usuario(string id, TipoDeUsuario tipoDeUsuario, int legajo, string nombre, string apellido, 
                        string correoElectronico, string contraseña, int dni, 
                        bool cambioDeContraseñaObligatorio, string numeroDeTelefojo, string direccion) : base("Usuarios")
        {
            Id = id;
            TipoDeUsuario = tipoDeUsuario;
            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
            Contraseña = contraseña;
            CambioDeContraseñaObligatorio = cambioDeContraseñaObligatorio;
            Dni = dni;
            NumeroDeTelefono = numeroDeTelefojo;
            Direccion = direccion;
        }

        public int Add()
        {

            //Microsoft.Data.SqlClient.SqlException a validar

            ConfigurarParametros();

            string[] columnasBD = ObtenerListaColumnasBD();

            return base.Add();
        }

        public int Delete()
        {
            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "UsuarioId", Id }
            };

            return base.Delete();
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
            return base.GetAll(Map);
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
            return base.SearchWhere(Map);
        }

        public int Update()
        {
            string[] columnasBD = ObtenerListaColumnasBD();
            ConfigurarParametros();

            Dictionary<string, object> camposValor = new Dictionary<string, object>
            {
                { "UsuarioId", Id }
            };

            return base.Update();
        }

        public Usuario Map(IDataRecord reader)
        {
            var id = reader["UsuarioId"].ToString() ?? "";
            var tipoUsuario = (TipoDeUsuario) reader.GetByte(reader.GetOrdinal("TipoUsuario"));
            var legajo = reader.GetInt32(reader.GetOrdinal("Legajo"));
            var nombre = reader["Nombre"].ToString() ?? "";
            var apellido = reader["Apellido"].ToString() ?? "";
            var correoElectronico = reader["CorreoElectronico"].ToString() ?? "";
            var contraseña = reader["Contrasenia"].ToString() ?? "";
            var cambioDeContraseñaObligatorio = reader.GetBoolean(reader.GetOrdinal("CambioObligatorio"));
            var dni = reader.GetInt32(reader.GetOrdinal("DNI"));
            var numeroDeTelefono = reader["NumeroDeTelefono"].ToString() ?? "";
            var direccion = reader["Direccion"].ToString() ?? "";

            var usuario = new Usuario(id, tipoUsuario, legajo, nombre, apellido, 
                                correoElectronico, contraseña, dni,
                                cambioDeContraseñaObligatorio, numeroDeTelefono, direccion);

            return usuario;
        }

        private static string[] ObtenerListaColumnasBD()
        {
            return
            [
                "UsuarioId",
                "TipoUsuario",
                "Legajo",
                "Nombre",
                "Apellido",
                "CorreoElectronico",
                "Contrasenia",
                "CambioObligatorio",
                "DNI",
                "NumeroDeTelefono",
                "Direccion",
            ];
        }

        private void ConfigurarParametros()
        {
            _comando.Parameters.Clear();
            _comando.Parameters.Add("@UsuarioId", SqlDbType.VarChar);
            _comando.Parameters["@UsuarioId"].Value = Id;
            _comando.Parameters.Add("@TipoUsuario", SqlDbType.TinyInt);
            _comando.Parameters["@TipoUsuario"].Value = (int) TipoDeUsuario;
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
