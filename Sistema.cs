using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;

namespace BibliotecaClases
{
    public static class Sistema
    {
        private const string NOMBRE_BASE_DE_DATOS = "BaseDeDatos.json";
        private static string _rutaBaseDeDatos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), NOMBRE_BASE_DE_DATOS);

        public static void GuardarJson(FakeBaseDeDatos baseDeDatos)
        {
            var options = new JsonSerializerOptions {WriteIndented = true};
            string jsonString = JsonSerializer.Serialize(baseDeDatos, options);
            File.WriteAllText(_rutaBaseDeDatos, jsonString);
        }

        public static FakeBaseDeDatos LeerJson()
        {
            try { 
                string jsonString = File.ReadAllText(_rutaBaseDeDatos);
                return JsonSerializer.Deserialize<FakeBaseDeDatos>(jsonString);
            } 
            catch
            {
                FakeBaseDeDatos baseDeDatos = new FakeBaseDeDatos();
                Usuario administrador = new Usuario(TipoDeUsuario.Administrador, 1234, "Admin", "Admin", "Admin@admin.com", "1234", 1234);
                if (baseDeDatos + administrador)
                {
                    return baseDeDatos;
                }
                else
                {
                    throw new Exception("No se encontró base de datos y no se pudo generar administrador generico");
                }
            }
        }

        public static string GenerarUUID()
        {
            Guid myuuid = Guid.NewGuid();
            string stringUUID = myuuid.ToString();
            return stringUUID;
        }

        public static bool VerificarDisponibilidadHoraria(Usuario estudiante, Curso curso)
        {
            return true;
        }

        public static string EncriptarTexto(string texto)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(texto, 9);
        }

        public static async void ValidarLogin(ILoginVista loginVista, TipoDeUsuario tipoDeUsuario, string correo, string cotraseña)
        {
            Usuario? usuario = await Usuario.ObtenerUsuario(tipoDeUsuario, correo, cotraseña);

            if (usuario == null)
            {
                loginVista.OnLoginFail();
            }
            else if (usuario.CambioDeContraseñaObligatorio)
            {
                loginVista.OnLoginCambioDeContraseñaObligatorio();
            }
            else
            {
                loginVista.OnLoginOk();
            }

        }

    }
}
