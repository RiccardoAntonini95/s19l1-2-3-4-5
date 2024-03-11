namespace InFornoPizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdine")]
    public partial class DettagliOrdine
    {
        [Key]
        public int DettaglioOrdineId { get; set; }

        public int ArticoloId { get; set; }

        public int OrdineId { get; set; }

        public int Quantita { get; set; }

        [Required]
        [StringLength(50)]
        public string Indirizzo { get; set; }

        public string NoteCliente { get; set; }

        public virtual Articoli Articoli { get; set; }

        public virtual Ordini Ordini { get; set; }
    }
}
