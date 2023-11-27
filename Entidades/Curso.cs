using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Curso
    {

        private Curso() : this(Sistema.GenerarUUID(), string.Empty, string.Empty, 0, string.Empty)
        {
        }

        public Curso(string nombreCursada, string aula, int cupoMaximo, string? profesorId) : this()
        {
            Nombre = nombreCursada;
            Aula = aula;
            CupoMaximo = cupoMaximo;
            ProfesorId = profesorId;
        }

        public override string ToString()
        {
            string cursoLleno = CursoLleno().Result ? "Si" : "No";
            
            return $"Nombre Curso: {Nombre} - Aula: {Aula} - Cupo Maximo: {CupoMaximo} - Curso Lleno: {cursoLleno}";
        }

        private async Task<bool> CursoLleno()
        {
            if (await GetCantidadIncriptos() < CupoMaximo) return false;
            else { return true; }
        }

        public static bool operator ==(Curso cursoUno, Curso cursoDos)
        {
            if (cursoUno.Id == cursoDos.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Curso cursoUno, Curso cursoDos) { return !(cursoUno == cursoDos); }

    }
}
