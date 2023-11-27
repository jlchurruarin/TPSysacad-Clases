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
    public class LogicaABMMateria
    {
        private IABMMateria _materiaVista;

        public LogicaABMMateria(IABMMateria materiaVista)
        {
            _materiaVista = materiaVista;
            _materiaVista.AlSolicitarMateria += MostrarMateria;
        }

        public async void MostrarMateria()
        {
            Materia? materia = await Materia.ObtenerMateriaPorID(_materiaVista.Materia.Id);
            _materiaVista.MostrarMateria(materia);
        }

        public async void AddMateria(string nombre, string descripcion, string creditosBrindados, string creditosNecesarios)
        {
            try
            {
                ValidarMateria(nombre, descripcion, creditosBrindados, creditosNecesarios);
            }
            catch (Exception ex)
            {
                _materiaVista.OnAddError(ex.Message);
                return;
            }

            Int32.TryParse(creditosBrindados, out int intCreditosBrindados);
            Int32.TryParse(creditosNecesarios, out int intcreditosNecesarios);

            Materia materia = new Materia(nombre, descripcion, intCreditosBrindados, intcreditosNecesarios);

            try
            {
                await materia.Add();
                _materiaVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _materiaVista.OnAddError(ex.Message);
            }
        }


        public async void UpdateMateria(string id, string nombre, string descripcion, string creditosBrindados, string creditosNecesarios)
        {
            try
            {
                ValidarMateria(nombre, descripcion, creditosBrindados, creditosNecesarios);
            }
            catch (Exception ex)
            {
                _materiaVista.OnUpdateError(ex.Message);
                return;
            }

            Int32.TryParse(creditosBrindados, out int intCreditosBrindados);
            Int32.TryParse(creditosNecesarios, out int intcreditosNecesarios);

            Materia materia = new Materia(nombre, descripcion, intCreditosBrindados, intcreditosNecesarios);
            materia.Id = id;

            try
            {
                await materia.Update();
                _materiaVista.OnUpdateOk();
            }
            catch (Exception ex)
            {
                _materiaVista.OnUpdateError(ex.Message);
            }
        }

        private void ValidarMateria(string nombre, string descripcion, string creditosBrindados, string creditosNecesarios)
        {
            if (string.IsNullOrEmpty(nombre)) { throw new Exception("Nombre no puede estar vacio"); }
            if (string.IsNullOrEmpty(descripcion)) { throw new Exception("Descripcion no puede estar vacio"); }

            bool cb = Int32.TryParse(creditosBrindados, out int intCreditosBrindados);
            
            if (cb == false) { throw new Exception("Creditos brindados debe ser un numero"); }
            else
            {
                if (intCreditosBrindados < 0) { throw new Exception("Creditos brindados no puede ser menor a 0"); }
            }

            bool cn = Int32.TryParse(creditosNecesarios, out int intcreditosNecesarios);

            if (cn == false) { throw new Exception("Creditos necesarios debe ser un numero"); }
            else
            {
                if (intcreditosNecesarios < 0) { throw new Exception("Creditos necesarios no puede ser menor a 0"); }
            }
        }
    }
}
