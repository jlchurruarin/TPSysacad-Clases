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

        private int _legajo;
        private int _dni;
        private int _numeroTelefono;
        private string _direccion;
        private List<Pago> _listaPagos;

        public int Legajo
        {
            get { return _legajo; }
            set { _legajo = value;}
        }

        public int Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public int NumeroTelefono
        {
            get { return _numeroTelefono; }
            set { _numeroTelefono = value; }
        }
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public List<Pago> ListaPagos
        {
            get { return _listaPagos; }
            set { _listaPagos = value; }
        }

        public Estudiante() : base()
        {
            _direccion = string.Empty;
            _listaPagos = new List<Pago>();
        }

        public Estudiante(int legajo,string nombre, string apellido, int dni, string direccion, int numeroTelefono, string correoElectronico) : base(nombre, apellido, correoElectronico)
        {
            _legajo = legajo;
            _dni = dni;
            _direccion = direccion;
            _numeroTelefono = numeroTelefono;
            _listaPagos = new List<Pago>();
        }

        public Estudiante(int legajo, string nombre, string apellido, int dni, string direccion, int numeroTelefono, string correoElectronico, List<Pago> listaPagos) : this(legajo, nombre, apellido, dni, direccion, numeroTelefono, correoElectronico)
        {
            _listaPagos = listaPagos;
        }

        public Estudiante(int legajo,string nombre, string apellido, int dni, string direccion, int numeroTelefono, string correoElectronico, string contraseña, List<Pago> listaPagos) : this(legajo,nombre, apellido, dni, direccion, numeroTelefono, correoElectronico, listaPagos)
        {
            Contraseña = contraseña;
        }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - {_legajo} - {CorreoElectronico}";
        }

        public static bool operator +(Estudiante e, Pago p)
        {
            e._listaPagos.Add(p);
            return true;
        }

        public static bool operator ==(Estudiante e, Estudiante e2)
        {
            if(e._legajo == e2._legajo)
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
