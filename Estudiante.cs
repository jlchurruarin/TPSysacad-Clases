using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public class Estudiante : Usuario
    {

        public Estudiante(int legajo, int dni, string nombre, string apellido, string correoElectronico, string contraseña) : base(TipoDeUsuario.Estudiante,legajo, nombre, apellido, correoElectronico, contraseña, dni)
        {}

        public Estudiante(int legajo,string nombre, string apellido, int dni, string direccion, string numeroTelefono, string correoElectronico, string contraseña) : this(legajo, dni, nombre, apellido, correoElectronico, contraseña)
        {
            Direccion = direccion;
            NumeroDeTelefono = numeroTelefono;
        }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {Legajo} - {CorreoElectronico}";
        }


        public static bool operator ==(Estudiante e, Estudiante e2)
        {
            if(e.Id == e2.Id)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public static bool operator !=(Estudiante e, Estudiante e2) { return !(e == e2); }

    }
}
