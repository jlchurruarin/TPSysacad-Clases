using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public class Administrador : Usuario
    {

        private Administrador() :base()
        { }

        public Administrador(int legajo, string nombre, string apellido,
                             string correoElectronico, string contraseña, int dni) : 
                                base(TipoDeUsuario.Administrador, legajo, nombre, apellido, 
                                correoElectronico, contraseña, dni) { }

        public Administrador(int legajo, string nombre, string apellido,
                             string correoElectronico, string contraseña, int dni,
                             bool cambioDeContraseñaObligatorio, string numeroDeTelefojo, string direccion) : 
                                base(TipoDeUsuario.Administrador, legajo, nombre, apellido, 
                                correoElectronico, contraseña, dni, cambioDeContraseñaObligatorio, 
                                numeroDeTelefojo, direccion) { }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {CorreoElectronico}";
        }
        

    }
}