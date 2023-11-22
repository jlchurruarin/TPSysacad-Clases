﻿using BibliotecaClases.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface IABMMateria : ISQLAddUpdateVista
    {
        public Materia? Materia { get; }

        public event Action? AlSolicitarMateria;

        public void MostrarMateria(Materia? Materia);
    }
}