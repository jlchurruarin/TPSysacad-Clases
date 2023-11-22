using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMCurso : ISQLAddUpdateVista
    {
        public Curso? Curso { get; }

        public event Action? AlSolicitarCurso;

        public void MostrarCurso(Curso? curso);
    }
}
