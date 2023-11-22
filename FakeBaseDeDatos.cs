using BibliotecaClases.BD;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BibliotecaClases
{

    public class FakeBaseDeDatos
	{
        // @"Server=.;Database=prog2;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;"

        private List<Usuario> _listaEstudiantes;
        private List<Usuario> _listaProfesores;
        private List<Usuario> _listaAdministradores;
        private List<Materia> _listaMaterias;
        private List<Curso> _listaCursos;

        public List <Usuario> ListaEstudiantes
        {
            get { return _listaEstudiantes; }
            set { _listaEstudiantes = value; }
        }

        public List<Usuario> ListaProfesores
        {
            get { return _listaProfesores; }
            set { _listaProfesores = value; }
        }

        public List<Usuario> ListaAdministradores
        {
            get { return _listaAdministradores; }
            set { _listaAdministradores = value; }
        }

        public List<Materia> ListaMaterias
        {
            get { return _listaMaterias; }
            set { _listaMaterias = value; }
        }

        public List<Curso> ListaCursos
        {
            get { return _listaCursos; }
            set { _listaCursos = value; }
        }

        public FakeBaseDeDatos()
        {
            _listaAdministradores = new List<Usuario>();
            _listaEstudiantes = new List<Usuario>();
            _listaProfesores = new List<Usuario>();
            _listaMaterias = new List<Materia>();
            _listaCursos = new List<Curso>();
        }
        public bool VerificarSiIDEstudianteExiste(int ID)
        {
            return false;
        }

        public bool VerificarSiCorreoExiste(string correoElectronico)
        {
            return false;
        }

        public bool VerificarSiCursoExiste(Materia materia, string idCurso)
        {
            return false;
        }

        public Usuario? BuscarEstudiantePorID(string id)
        {
            Usuario? estudiante = _listaEstudiantes.Find(estudiante => estudiante.Id == id);
            return estudiante;
        }

        public Usuario? BuscarProfesorPorID(string id)
        {
            Usuario? profesor = _listaProfesores.Find(profesor => profesor.Id == id);
            return profesor;
        }

        public Materia? BuscarMateriaPorID(string id)
        {
            Materia? materia = _listaMaterias.Find(materia => materia.Id == id);
            return materia;
        }

        public Curso? BuscarCursoPorID(string id)
        {
            Curso? curso = _listaCursos.Find(curso => curso.Id == id);
            return curso;
        }

        public Usuario? BuscarEstudiantePorCorreo(string correo)
        {
            Usuario? usuario = BuscarPorCorreo(_listaEstudiantes.ConvertAll(estudiante => (Usuario) estudiante), correo);
            if (usuario == null) 
            { 
                return null; 
            }
            else 
            { 
                return usuario; 
            }
        }

        public Usuario? BuscarProfesorPorCorreo(string correo)
        {
            Usuario? usuario = BuscarPorCorreo(_listaProfesores.ConvertAll(profesor => (Usuario) profesor), correo);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                return usuario;
            }
        }

        public Usuario? BuscarAdministradorPorCorreo(string correo)
        {
            Usuario? usuario = BuscarPorCorreo(_listaAdministradores.ConvertAll(administrador => (Usuario) administrador), correo);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                return usuario;
            }
        }

        private Usuario? BuscarPorCorreo(List<Usuario> listaUsuario, string correo)
        {
            List<Usuario> listaResultado = listaUsuario.Where(usuario => usuario.CorreoElectronico == correo).ToList();
            foreach (Usuario usuario in listaResultado)
            {
                return usuario;
            }
            return null;
        }

        
        public List<Curso> BuscarCursosInscriptos(Usuario estudiante)
        {
            List<Curso> listaCursosFiltrados = new List<Curso>();

            foreach (Curso curso in ListaCursos)
            {
                //List<Inscripcion> listaInscripcion = curso.ListaDeInscripciones.Where(inscripcion => inscripcion.IdEstudiante == estudiante.Id).ToList();
                List<Inscripcion> listaInscripcion = new List<Inscripcion> { };
                if (listaInscripcion.Count > 0)
                {
                    listaCursosFiltrados.Add(curso);
                }
            }
            return listaCursosFiltrados;
        }

        public List<Curso> BuscarCursos(Usuario profesor)
        {
            //List<Curso> ListaCursosFiltrados = _listaCursos.Where(cursoInscripto => cursoInscripto.IdProfesor == profesor.Id).ToList();
            List<Curso> ListaCursosFiltrados = new List<Curso> { };
            return ListaCursosFiltrados;
        }

        public List<Curso> ObtenerCursosDisponibles(Usuario estudiante)
        {
            List<Curso> cursosDisponibles = new List<Curso>();
            List<Curso> cursosEstudiante = BuscarCursosInscriptos(estudiante);

            foreach (Curso curso in _listaCursos)
            {
                Materia? materiaDelCurso = BuscarMateriaPorID(curso.Id);
                bool cumpleRequisitos = true;
                if (materiaDelCurso is not null) { 
                    if (cursosEstudiante.Any(ce => ce.Id == materiaDelCurso.Id)) {
                        cumpleRequisitos = false;
                    }
                    else
                    {
                        foreach (string materiaRequerida in materiaDelCurso.ListaIdMateriasRequeridas)
                        {
                            if (cursosEstudiante.Any(ce => ce.Id == materiaRequerida) == false) {
                                cumpleRequisitos = false;
                            }
                        }
                    }
                    if (cumpleRequisitos) {
                        cursosDisponibles.Add(curso);
                    }
                }
            }

            return cursosDisponibles;
        }

        public static bool operator +(FakeBaseDeDatos bd, Usuario estudiante)
        {
            if (bd.BuscarEstudiantePorCorreo(estudiante.CorreoElectronico) is null && 
                bd.BuscarEstudiantePorID(estudiante.Id) is null && 
                bd._listaEstudiantes.Any(e=> e.Legajo == estudiante.Legajo) == false)
            {
                bd._listaEstudiantes.Add(estudiante);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator -(FakeBaseDeDatos bd, Usuario estudiante)
        {
            if (bd._listaEstudiantes.Any(e => e.Id == estudiante.Id))
            {
                bd._listaEstudiantes.RemoveAll(e => e.Id == estudiante.Id);
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool operator -(FakeBaseDeDatos bd, Curso curso)
        {
            int CantidadCursosEliminadas = bd._listaCursos.RemoveAll(c => c.Id == curso.Id);
            if (CantidadCursosEliminadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator +(FakeBaseDeDatos bd, Materia materia)
        {
            if (!bd._listaMaterias.Any(m => m.Id == materia.Id))
            {
                bd._listaMaterias.Add(materia);
                materia.Add();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator -(FakeBaseDeDatos bd, Materia materia)
        {
            int CantidadMateriasEliminadas = bd._listaMaterias.RemoveAll(m => m.Id == materia.Id);
            materia.Delete();
            if (CantidadMateriasEliminadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }


}
