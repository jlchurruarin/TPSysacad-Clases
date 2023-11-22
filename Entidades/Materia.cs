using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Materia
    {

        private List<string> _listaIdMateriasRequeridas;

        public List<string> ListaIdMateriasRequeridas
        {
            get { return _listaIdMateriasRequeridas; }
            set { _listaIdMateriasRequeridas = value; }
        }

        public Materia() : this(string.Empty, string.Empty) { }

        public Materia(string nombre, string descripcion): this(Sistema.GenerarUUID(), nombre, descripcion) { }

        public static bool operator ==(Materia materia1, Materia Materia2)
        {
            if (materia1.Id == Materia2.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Materia m1, Materia m2) { return !(m1 == m2); }

        public override string ToString()
        {
            return $"{Nombre} - {Descripcion}";
        }
    }
}
