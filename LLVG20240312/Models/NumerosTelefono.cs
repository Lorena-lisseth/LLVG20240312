using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LLVG20240312.Models
{
    public partial class NumerosTelefono
    {
        public int IdTelefono { get; set; }
        public int IdCliente { get; set; }
        [Display(Name = "Numero de Telefono")]
        public string? NumeroTelefono { get; set; }
        [Display(Name = "Tipo de Telefono")]
        public string? TipoTelefono { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
    }
}
