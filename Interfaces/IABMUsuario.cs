using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMUsuario : ISQLAddUpdateVista
    {
        public TipoDeUsuario TipoDeUsuario { get; }
        public Usuario? Usuario { get; }

        public event Action? AlSolicitarUsuario;

        public void MostrarUsuario(Usuario? usuario);
    }
}
