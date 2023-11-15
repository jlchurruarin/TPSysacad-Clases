using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public class Profesor : Usuario
    {   

        private int _legajo;


        public int Legajo
        {
            set { _legajo = value; }
            get { return _legajo; }
        }

        private Profesor() : base()
        {
        }

        public Profesor(int legajo, string nombre, string apellido, string correoElectronico, string contraseña, int dni) : base(TipoDeUsuario.Profesor, legajo, nombre, apellido, correoElectronico, contraseña, dni)
        {
        }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {_legajo} - {CorreoElectronico}";
        }

        public void DarClases()
        { }

        public void TomarExamen()
        { }

        public void CargarNotas()
        { }
    }
}
