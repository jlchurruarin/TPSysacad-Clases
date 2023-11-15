﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.BD
{
    public partial class Inscripcion
    {

        public Inscripcion() : this(string.Empty, string.Empty, EstadoCursada.EnCurso, new DateTime())
        {
            
        }

        public Inscripcion(string estudianteId, string cursoId, EstadoCursada estadoDeInscripcion) : this()
        {
            CursoId = cursoId;
            EstudianteId = estudianteId;
            EstadoDeInscripcion = estadoDeInscripcion;
        }

        public Inscripcion(Estudiante estudiante, Curso curso, EstadoCursada estadoDeInscripcion) : this(estudiante.Id, curso.Id, estadoDeInscripcion) { }



        public static bool operator ==(Inscripcion cursoInscriptoUno, Inscripcion cursoInscriptoDos)
        {

            if (cursoInscriptoUno.CursoId == cursoInscriptoDos.CursoId && cursoInscriptoUno.EstudianteId == cursoInscriptoDos.EstudianteId)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool operator !=(Inscripcion cursoInscriptoUno, Inscripcion cursoInscriptoDos) { return !(cursoInscriptoUno == cursoInscriptoDos); }

    }
}
