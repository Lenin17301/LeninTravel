using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeninTravel.Models
{
    public class DestinosViewModel
    {
        public int id { get; set; }
        public int cod_viaje { get; set; }
        public int nro_plazas { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal precio { get; set; }        
    }
}