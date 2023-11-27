using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaABMCurso
    {
        private IABMCurso _cursoVista;

        public LogicaABMCurso(IABMCurso cursoVista)
        {
            _cursoVista = cursoVista;
            _cursoVista.AlSolicitarCurso += MostrarCurso;
        }

        public void MostrarCurso(string cursoid)
        {
            Curso? curso = Curso.ObtenerCursoPorID(cursoid);
            _cursoVista.MostrarCurso(curso);
        }

        public void AddCurso(string nombre, string aula, string cupoMaximo, string materiaId, string? profesorId)
        {
            try
            {
                ValidarCurso(nombre, aula, cupoMaximo, materiaId);
            } 
            catch (Exception ex)
            {
                _cursoVista.OnAddError(ex.Message);
            }

            Int32.TryParse(cupoMaximo, out int intCupoMaximo);

            Curso curso = new Curso(nombre, aula, intCupoMaximo, profesorId);
            MateriaCurso materiaCurso = new MateriaCurso(materiaId, curso.Id);

            try
            {
                curso.Add();
                materiaCurso.Add();
                _cursoVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _cursoVista.OnAddError(ex.Message);
            }

        }

        public void UpdateCurso(string id, string nombre, string aula, string cupoMaximo, string materiaId, string? profesorId)
        {
            try
            {
                ValidarCurso(nombre, aula, cupoMaximo, materiaId);
            }
            catch (Exception ex)
            {
                _cursoVista.OnAddError(ex.Message);
            }

            Int32.TryParse(cupoMaximo, out int intCupoMaximo);

            Curso Curso = new Curso(nombre, aula, intCupoMaximo, profesorId);
            Curso.Id = id;

            try
            {
                Curso.Update();
                _cursoVista.OnUpdateOk();
            }
            catch (Exception ex)
            {
                _cursoVista.OnAddError(ex.Message);
            }
        }

        private void ValidarCurso(string nombre, string aula, string cupoMaximo, string materiaId)
        {
            if (string.IsNullOrEmpty(nombre)) { throw new Exception("El nombre no puede estar vacio"); }
            if (string.IsNullOrEmpty(aula)) { throw new Exception("El aula no puede estar vacio"); }
            if (string.IsNullOrEmpty(cupoMaximo)) { throw new Exception("Cupo máximo no puede estar vacio"); }
            if (string.IsNullOrEmpty(materiaId)) { throw new Exception("La materia no puede estar vacia"); }
            else
            {
                bool cupoMaximoYry = int.TryParse(cupoMaximo, out int cupoMaximoResult);
                if (!cupoMaximoYry) { throw new Exception("El cupo máximo solo puede contener numeros"); }
                if (cupoMaximoResult < 0) { throw new Exception("Cupo máximo debe ser igual o mayor a 0"); }
            }
        }
    }

}
