using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public class Administrador : Usuario
    {

        public Administrador() :base()
        { }

        public Administrador(string nombre, string apellido, string correoElectronico) : base(nombre, apellido,correoElectronico)
        { }

        public Administrador(string nombre, string apellido, string correoElectronico, string contraseña) : base(nombre, apellido,correoElectronico, contraseña)
        { }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {CorreoElectronico}";
        }
        

    }
}