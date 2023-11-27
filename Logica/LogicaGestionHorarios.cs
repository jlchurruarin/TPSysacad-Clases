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

        public async Task<List<HorarioCurso>> ObtenerHorariosCurso(Curso curso)
        {
            return await HorarioCurso.GetHorarioCursos(curso);
        }

        public async void AgregarHorario(string cursoId, Dia dia, TimeOnly horaInicio, TimeOnly horaFin)
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
                await hc.Add();
                _gestionHorariosVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _gestionHorariosVista.OnAddError(ex.Message);
            }
        }

        public async void EliminarHorario(HorarioCurso horarioCurso)
        {
            try
            {
                await horarioCurso.Delete();
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
