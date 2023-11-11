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

        internal Usuario() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, false, 0,string.Empty, string.Empty)
        {
        }

        public Usuario(string nombre, string apellido, string correoElectronico) : this()
        {
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correoElectronico;
        }

        public Usuario(string nombre, string apellido, string correoElectronico, string contraseña) : this(nombre, apellido, correoElectronico)
        {
            Contraseña = Sistema.EncriptarTexto(contraseña);
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
            return $"{Apellido}, {Nombre} - {CorreoElectronico}";
        }
    }
}
