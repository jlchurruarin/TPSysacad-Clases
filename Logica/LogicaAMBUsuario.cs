using Azure.Core;
using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaAMBUsuario
    {
        private IABMUsuario _usuarioVista;

        public LogicaAMBUsuario(IABMUsuario usuarioVista)
        {
            _usuarioVista = usuarioVista;
            _usuarioVista.AlSolicitarUsuario += MostrarUsuario;
        }

        public void MostrarUsuario()
        {
            Usuario? usuario = Usuario.ObtenerUsuarioPorID(_usuarioVista.TipoDeUsuario, _usuarioVista.Usuario.Id);
            _usuarioVista.MostrarUsuario(usuario);
        }

        public void AddUsuario(TipoDeUsuario tipoDeUsuario, string legajo, string nombre, string apellido,
                        string correoElectronico, string contraseña, string dni,
                        bool cambioDeContraseñaObligatorio, string numeroDeTelefojo, string direccion)
        {
            try
            {
                ValidarUsuario(legajo, nombre, apellido, correoElectronico, contraseña, dni, numeroDeTelefojo, direccion);
            }
            catch (Exception ex)
            {
                _usuarioVista.OnAddError(ex.Message);
                return;
            }
            int.TryParse(legajo, out int intLegajo);
            int.TryParse(dni, out int intDni);
            Usuario usuario = new Usuario(tipoDeUsuario, intLegajo, nombre, apellido, correoElectronico, contraseña, intDni, cambioDeContraseñaObligatorio, numeroDeTelefojo, direccion);

            try
            {
                usuario.Add();
                _usuarioVista.OnAddOk();
            }
            catch (Exception ex)
            {
                var campo = "";
                if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioCorreo'."))
                {
                    campo = "correo electronico";
                }
                else if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioDNI'"))
                {
                    campo = "dni";
                }
                else if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioLegajo'"))
                {
                    campo = "legajo";
                }

                _usuarioVista.OnAddError($"El {campo} ingresado ya se encuentra utilizado por otro usaurio");
            }

        }

        public void UpdateUsuario(string id, TipoDeUsuario tipoDeUsuario, string legajo, string nombre, string apellido,
                        string correoElectronico, string contraseña, string dni,
                        bool cambioDeContraseñaObligatorio, string numeroDeTelefojo, string direccion)
        {
            try
            {
                ValidarUsuario(legajo, nombre, apellido, correoElectronico, dni, numeroDeTelefojo, direccion);
            }
            catch (Exception ex)
            {
                _usuarioVista.OnUpdateError(ex.Message);
                return;
            }

            int.TryParse(legajo, out int intLegajo);
            int.TryParse(dni, out int intDni);
            Usuario usuario = new Usuario(tipoDeUsuario, intLegajo, nombre, apellido, correoElectronico, contraseña, intDni, cambioDeContraseñaObligatorio, numeroDeTelefojo, direccion);
            usuario.Id = id;

            try
            {
                usuario.Update();
            }
            catch (Exception ex)
            {
                var campo = "";
                if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioCorreo'."))
                {
                    campo = "correo electronico";
                }
                else if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioDNI'"))
                {
                    campo = "dni";
                }
                else if (ex.Message.Contains("Infracción de la restricción UNIQUE KEY 'UC_TipoUsuarioLegajo'"))
                {
                    campo = "legajo";
                }

                _usuarioVista.OnAddError($"El {campo} ingresado ya se encuentra utilizado por otro usaurio");
                return;
            }

            _usuarioVista.OnUpdateOk();
        }

        private void ValidarUsuario(string legajo, string nombre, string apellido,
                        string correoElectronico, string dni,
                        string numeroDeTelefojo, string direccion)
        {
            //Validación de legajo
            if (string.IsNullOrEmpty(legajo)) { throw new Exception("El legajo no puede estar vacio"); }
            else
            {
                bool legajoTry = int.TryParse(legajo, out int legajoResult);
                if (!legajoTry) { throw new Exception("El legajo deben contener sólo números"); }
                if (legajoResult < 0) { throw new Exception("El legajo no puede ser negativo"); }
            }

            if (string.IsNullOrEmpty(nombre)) { throw new Exception("Nombre no puede estar vacio"); }
            if (string.IsNullOrEmpty(apellido)) { throw new Exception("Apellido no puede estar vacio"); }
            if (string.IsNullOrEmpty(correoElectronico)) { throw new Exception("Correo electronico no puede estar vacio"); }

            //Validación de DNI
            if (string.IsNullOrEmpty(dni)) { throw new Exception("DNI no puede estar vacio"); }
            else
            {
                bool dniTry = int.TryParse(dni, out int dniResult);
                if (!dniTry) { throw new Exception("El DNI deben contener sólo números"); }
                if (dniResult < 1000000) { throw new Exception("No se permiten numeros menores a 1 millon"); }
            }

            //Validación de Numero de telefono
            if (numeroDeTelefojo != "")
            {
                bool nTelefonoTry = int.TryParse(numeroDeTelefojo, out int nTelResult);
                if (!nTelefonoTry) { throw new Exception("El Numero de telefono deben contener sólo números"); }
                if (nTelResult < 10000000) { throw new Exception("El numero de telefono debe contener el codigo de area"); }
            }
        }

        private void ValidarUsuario(string legajo, string nombre, string apellido,
                        string correoElectronico, string contraseña, string dni,
                        string numeroDeTelefojo, string direccion)
        {
            ValidarUsuario(legajo, nombre, apellido, contraseña, dni, numeroDeTelefojo, direccion);

            if (string.IsNullOrEmpty(contraseña)) { throw new Exception("Contraseña no puede estar vacia"); }
        }
    }
}
