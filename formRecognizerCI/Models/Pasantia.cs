using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasantIARecognizer.Models
{
    public class Pasantia
    {
        [Key]
        public int Titulo { get; set; }
        public string Nombre { get; set; }
        public string FechaNacimiento { get; set; }
        public int CarreraPreferencia { get; set; }
        public string Modalidad { get; set; }
        public string Horario { get; set; }
        public string Ubicacion { get; set; }
        public string Requisitos { get; set; }
        public int IdContacto { get; set; }
    }
}
