using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Usuario
    {
        private const int LONGITUDCONTRASENIA = 10;

        protected Usuario() : this(string.Empty, TipoDeUsuario.Estudiante, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, string.Empty, string.Empty)
        {
        }

        internal Usuario(string id, TipoDeUsuario tipoDeUsuario) : this(id, tipoDeUsuario, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, string.Empty, string.Empty)
        {
        }

        public Usuario(TipoDeUsuario tipoDeUsuario, int legajo, string nombre, string apellido,
                        string correoElectronico, string contraseña, int dni) : this()
        {
            Id = Sistema.GenerarUUID();
            TipoDeUsuario = tipoDeUsuario;
            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
            Contraseña = Sistema.EncriptarTexto(contraseña);
            Dni = dni;

        }

        public Usuario(TipoDeUsuario tipoDeUsuario, int legajo, string nombre, string apellido,
                        string correoElectronico, string contraseña, int dni, 
                        bool cambioDeContraseñaObligatorio, string numeroDeTelefojo, string direccion) : this(tipoDeUsuario, legajo, nombre, apellido, correoElectronico, contraseña, dni)
        {
            CambioDeContraseñaObligatorio = cambioDeContraseñaObligatorio;
            NumeroDeTelefono = numeroDeTelefojo;
            Direccion = direccion;
        }


        public bool ValidarContraseña(string contraseñaIngresada)
        {
            if (string.IsNullOrEmpty(contraseñaIngresada)) { throw new ArgumentException("No se puede validar una contraseña vacia"); }
            return BCrypt.Net.BCrypt.EnhancedVerify(contraseñaIngresada, Contraseña);
        }

        private string GenerarNuevaContraseña()
        {
            string contraseñaTextoPlano = GenerarContraseñaAleatoria();
            Contraseña = Sistema.EncriptarTexto(contraseñaTextoPlano);
            return contraseñaTextoPlano;
        }

        private static string GenerarContraseñaAleatoria()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < LONGITUDCONTRASENIA; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

        public bool ResetContraseña(bool enviarCorreo = false)
        {
            string nuevaContraseñaTextoPlano = GenerarNuevaContraseña();

            if (enviarCorreo)
            {
                Correo.EnviarCorreo(CorreoElectronico, "Solicitud de nueva contraseña", $"Su nueva contraseña es: {nuevaContraseñaTextoPlano}");
            }

            return true;
        }

        public override string ToString()
        {
            if (TipoDeUsuario == TipoDeUsuario.Estudiante)
            {
                return $"{Apellido}, {Nombre} - {CorreoElectronico} - Creditos Obtenidos: {GetCreditosObtenidos().Result}";
            } 
            else
            {
                return $"{Apellido}, {Nombre} - {CorreoElectronico}";
            }
            
        }
    }
}
