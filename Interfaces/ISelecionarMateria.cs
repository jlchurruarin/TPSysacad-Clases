using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IRecibidorDeItemSeleccionado<T> where T : class
    {

        public List<T> ItemsAMostrar();

        public void RecibirItemSeleccionada(object item);

    }
}
