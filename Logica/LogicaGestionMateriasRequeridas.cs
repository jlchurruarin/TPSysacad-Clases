using BibliotecaClases.BD;
using BibliotecaClases.Interfaces;

namespace BibliotecaClases.Logica
{
    public class LogicaGestionMateriasRequeridas
    {
        private IGestionMateriasRequeridas _gestionMateriasRequeridasVista;

        public LogicaGestionMateriasRequeridas(IGestionMateriasRequeridas vista)
        {
            _gestionMateriasRequeridasVista = vista;
            _gestionMateriasRequeridasVista.AlSolicitarMateria += ObtenerMateriasRequeridas;
        }

        public List<Materia> ObtenerMateriasRequeridas(Materia materia)
        {
            Dictionary<string, object> where = new Dictionary<string, object>();
            where.Add("MateriaID", materia.Id);
            List<RequisitoMateria> listaRequisitos = RequisitoMateria.SearchWhere(where);

            List<Materia> materiasRequeridas = new List<Materia>();

            foreach(RequisitoMateria rm in listaRequisitos)
            {
                Materia? m = Materia.ObtenerMateriaPorID(rm.MateriaRequeridaId);
                if (m is not null ) { materiasRequeridas.Add(m); }
            }

            return materiasRequeridas;
        }

        public List<Materia> ObtenerMateriasRequeridasPosibles(Materia materia)
        {
            List<Materia> listaMaterias = Materia.GetAll();
            listaMaterias.RemoveAll(item => item.Id == materia.Id);

            List<Materia> materiasRequeridasActuales = ObtenerMateriasRequeridas(materia);

            foreach(Materia m in materiasRequeridasActuales)
            {
                listaMaterias.RemoveAll(item => item.Id == m.Id);
            }

            return listaMaterias;
        }


        public void AgregarMateriaRequerida(Materia materia, Materia materiaRequerida)
        {
            try
            {
                ValidarMateriaRequerida(materia.Id, materiaRequerida.Id);
            }
            catch (Exception ex)
            {
                _gestionMateriasRequeridasVista.OnAddError(ex.Message);
            }

            try
            {
                RequisitoMateria rm = new RequisitoMateria(materia, materiaRequerida);
                rm.Add();
                _gestionMateriasRequeridasVista.OnAddOk();
            }
            catch (Exception ex)
            {
                _gestionMateriasRequeridasVista.OnAddError(ex.Message);
            }
        }

        public void EliminarMateriaRequerida(Materia materia, Materia materiaRequerida)
        {
            try
            {
                ValidarMateriaRequerida(materia.Id, materiaRequerida.Id);
            }
            catch (Exception ex)
            {
                _gestionMateriasRequeridasVista.OnRemoveError(ex.Message);
            }

            try
            {
                RequisitoMateria rm = new RequisitoMateria(materia, materiaRequerida);
                rm.Delete();
                _gestionMateriasRequeridasVista.OnRemoveOk();
            } 
            catch (Exception ex)
            {
                _gestionMateriasRequeridasVista.OnRemoveError(ex.Message);
            }

        }

        private void ValidarMateriaRequerida(string materiaId, string materiaRequeriaId)
        {
            if (string.IsNullOrEmpty(materiaId)) { throw new Exception("Debe seleccionar una materia"); }
            if (string.IsNullOrEmpty(materiaRequeriaId)) { throw new Exception("Debe seleccionar una materia requerida"); }
        }
    }
}
