﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Interfaces
{
    public interface ILoginVista
    {
        public void OnLoginOk();
        public void OnLoginFail();
        public void OnLoginCambioDeContraseñaObligatorio();
    }
}
