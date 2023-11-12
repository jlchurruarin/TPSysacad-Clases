using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BibliotecaClases.BD;

namespace BibliotecaClases
{
    public static class Sistema
    {
        private const string NOMBRE_BASE_DE_DATOS = "BaseDeDatos.json";
        private static string _rutaBaseDeDatos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), NOMBRE_BASE_DE_DATOS);

        public static void GuardarJson(FakeBaseDeDatos baseDeDatos)
        {
            var options = new JsonSerializerOptions {WriteIndented = true};
            string jsonString = JsonSerializer.Serialize(baseDeDatos, options);
            File.WriteAllText(_rutaBaseDeDatos, jsonString);
        }

        public static FakeBaseDeDatos LeerJson()
        {
            try { 
                string jsonString = File.ReadAllText(_rutaBaseDeDatos);
                return JsonSerializer.Deserialize<FakeBaseDeDatos>(jsonString);
            } 
            catch
            {
                FakeBaseDeDatos baseDeDatos = new FakeBaseDeDatos();
                Administrador administrador = new Administrador("Admin", "Admin", "Admin@admin.com", "1234");
                if (baseDeDatos + administrador)
                {
                    return baseDeDatos;
                }
                else
                {
                    throw new Exception("No se encontró base de datos y no se pudo generar administrador generico");
                }
            }
        }

        public static string GenerarUUID()
        {
            Guid myuuid = Guid.NewGuid();
            string stringUUID = myuuid.ToString();
            return stringUUID;
        }

        public static bool VerificarDisponibilidadHoraria(Estudiante estudiante, Curso curso)
        {
            return true;
        }

        /*
        public static List<string> GenerarCalendario(List<Curso> listaCursos, Estudiante estudiante)
        {
            List<String> calendarioCursos = new List<string>();

            foreach (Curso curso in listaCursos)
            {
                if (curso.ListaDeInscripciones.Any(inscripcion => inscripcion.IdEstudiante == estudiante.Id))
                {
                    foreach(Horario horario in curso.Horario)
                    {
                        calendarioCursos.Add($"{horario.Dia} {horario.ObtenerHoraInicio()}: {curso.NombreCursada} - Aula: {curso.Aula} ({horario.CargaHoraria} horas)");
                    }
                }
            }

            return calendarioCursos;
        }
        */

        public static string EncriptarTexto(string texto)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(texto, 9);
        }
    }
}
