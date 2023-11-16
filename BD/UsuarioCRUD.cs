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

        public new int Add()
        {

            //Microsoft.Data.SqlClient.SqlException a validar
            AddSetValue("UsuarioId", Id);
            AddSetValue("TipoUsuario", TipoDeUsuario);
            AddSetValue("Legajo", Legajo);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Apellido", Apellido);
            AddSetValue("CorreoElectronico", CorreoElectronico);
            AddSetValue("Contrasenia", Contraseña);
            AddSetValue("CambioObligatorio", CambioDeContraseñaObligatorio);
            AddSetValue("DNI", Dni);
            AddSetValue("NumeroDeTelefono", NumeroDeTelefono);
            AddSetValue("Direccion", Direccion);

            return base.Add();
        }

        public new int Delete()
        {
            AddWhereCondition("UsuarioId", Id);

            return base.Delete();
        }

        public new int Update()
        {
            AddSetValue("TipoUsuario", TipoDeUsuario);
            AddSetValue("Legajo", Legajo);
            AddSetValue("Nombre", Nombre);
            AddSetValue("Apellido", Apellido);
            AddSetValue("CorreoElectronico", CorreoElectronico);
            AddSetValue("Contrasenia", Contraseña);
            AddSetValue("CambioObligatorio", CambioDeContraseñaObligatorio);
            AddSetValue("DNI", Dni);
            AddSetValue("NumeroDeTelefono", NumeroDeTelefono);
            AddSetValue("Direccion", Direccion);

            AddWhereCondition("UsuarioId", Id);

            return base.Update();
        }

        public static List<Usuario> GetAll()
        {
            Usuario usuario = new Usuario();
            return usuario.InternalGetAll(usuario.Map);
        }

        public static List<Usuario> SearchWhere(Dictionary<string, object> campoValores)
        {
            Usuario usuario = new Usuario();
            return usuario.InternalSearchWhere(usuario.Map, campoValores);
        }

        public static List<Usuario> GetUsuariosInscriptos(Curso curso)
        {
            Usuario usuario = new Usuario();
            usuario.AddJoin("INNER JOIN", "Inscripciones", "EstudianteID", "EstudianteID");
            usuario.AddWhereCondition("Inscripciones", "CursoID", curso.Id);
            return usuario.InternalSearchWhere(usuario.Map, new Dictionary<string, object>());
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

        protected override string[] ObtenerListaColumnasBD()
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

        protected override SqlDbType GetSqlDbType(string key)
        {
            SqlDbType retorno;

            if (key == "UsuarioId") retorno = SqlDbType.VarChar;
            else if (key == "TipoUsuario") retorno = SqlDbType.TinyInt;
            else if (key == "Legajo") retorno = SqlDbType.Int;
            else if (key == "Nombre") retorno = SqlDbType.VarChar;
            else if (key == "Apellido") retorno = SqlDbType.VarChar;
            else if (key == "CorreoElectronico") retorno = SqlDbType.VarChar;
            else if (key == "Contrasenia") retorno = SqlDbType.VarChar;
            else if (key == "CambioObligatorio") retorno = SqlDbType.Bit;
            else if (key == "DNI") retorno = SqlDbType.Int;
            else if (key == "NumeroDeTelefono") retorno = SqlDbType.VarChar;
            else if (key == "Direccion") retorno = SqlDbType.VarChar;
            else retorno = SqlDbType.Variant;

            return retorno;
        }

    }
}
