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

        public void MostrarMateria()
        {
            //Usuario? usuario = Usuario.ObtenerUsuarioPorID(_usuarioVista.TipoDeUsuario, _usuarioVista.Usuario.Id);
            Materia? materia = Materia.ObtenerMateriaPorID(_materiaVista.Materia.Id);
            _materiaVista.MostrarMateria(materia);
        }

        public void AddMateria(string nombre, string descripcion)
        {
            try
            {
                ValidarMateria(nombre, descripcion);
            }
            catch (Exception ex)
            {
                _materiaVista.OnAddError(ex.Message);
                return;
            }

            Materia materia = new Materia(nombre, descripcion);

            try
            {
                materia.Add();
                _materiaVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _materiaVista.OnAddError(ex.Message);
            }
        }


        public void UpdateMateria(string id, string nombre, string descripcion)
        {
            try
            {
                ValidarMateria(nombre, descripcion);
            }
            catch (Exception ex)
            {
                _materiaVista.OnUpdateError(ex.Message);
                return;
            }

            Materia materia = new Materia(nombre, descripcion);
            materia.Id = id;

            try
            {
                materia.Update();
            }
            catch (Exception ex)
            {
                _materiaVista.OnUpdateError(ex.Message);
            }
        }

        private void ValidarMateria(string nombre, string descripcion)
        {
            if (string.IsNullOrEmpty(nombre)) { throw new Exception("Nombre no puede estar vacio"); }
        }
    }
}
