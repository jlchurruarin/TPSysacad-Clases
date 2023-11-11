using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public class Profesor : Usuario
    {   

        private int _cuit;


        public int Cuit
        {
            set { _cuit = value; }
            get { return _cuit; }
        }

        public Profesor() : base() 
        { 
        }

        public Profesor(string nombre, string apellido, int cuit, string correoElectronico) : base(nombre, apellido, correoElectronico)
        {
            _cuit = cuit;
        }

        public Profesor(string nombre, string apellido, int cuit, string correoElectronico, string contraseña) : this(nombre, apellido, cuit, correoElectronico)
        {
            Contraseña = contraseña;
        }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {_cuit} - {CorreoElectronico}";
        }

        public void DarClases()
        { }

        public void TomarExamen()
        { }

        public void CargarNotas()
        { }
    }
}
