using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeninTravel.Models
{
    public class ViajerosViewModel
    {
        public int id { get; set; }
        public string via_nombre { get; set; }
        public string via_apellido { get; set; }
        public string via_ci { get; set; }
        public string via_pasaporte { get; set; }
        public string via_direccion { get; set; }
        public string via_telefono { get; set; }
    }
}