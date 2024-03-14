using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InFornoPizzeria.Models
{
    public class Carrello
    {
        public int ArticoloId { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public decimal PrezzoVendita { get; set; }
        public int Quantita { get; set; }
        public decimal Totale { get; set; }
    }
}