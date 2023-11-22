using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface ISQLAddUpdateVista
    {

        public void OnAddOk();
        public void OnUpdateOk();
        public void OnAddError(string errorMessage);
        public void OnUpdateError(string errorMessage);

    }
}