using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Logica
{
    public class LogicaGestionHorarios
    {
        private IGestionHorarios _gestionHorariosVista;

        public LogicaGestionHorarios(IGestionHorarios vista)
        {
            _gestionHorariosVista = vista;
            _gestionHorariosVista.AlSolicitarHorarios += ObtenerHorariosCurso;
        }

        public List<HorarioCurso> ObtenerHorariosCurso(Curso curso)
        {
            return HorarioCurso.GetHorarioCursos(curso);
        }

        public void AgregarHorario(string cursoId, Dia dia, TimeOnly horaInicio, TimeOnly horaFin)
        {
            try
            {
                ValidarHorario(cursoId, dia, horaInicio, horaFin);
            }
            catch (Exception ex)
            {
                _gestionHorariosVista.OnAddError(ex.Message);
            }

            try
            {
                HorarioCurso hc = new HorarioCurso(cursoId, dia, horaInicio, horaFin);
                hc.Add();
                _gestionHorariosVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _gestionHorariosVista.OnAddError(ex.Message);
            }
        }

        public void EliminarHorario(HorarioCurso horarioCurso)
        {
            try
            {
                horarioCurso.Delete();
                _gestionHorariosVista.OnRemoveOk();
            }
            catch (Exception ex)
            {
                _gestionHorariosVista.OnRemoveError(ex.Message);
            }
        }

        public void ValidarHorario(string curdoId, Dia dia, TimeOnly horaInicio, TimeOnly horaFin)
        {
            if (string.IsNullOrEmpty(curdoId)) { throw new Exception("Error en el curso, verifique esté seleccionado"); }

        }
    }
}
