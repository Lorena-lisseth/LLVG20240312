using System;
using System.Collections.Generic;

namespace LLVG20240312.Models
{
    public partial class NumerosTelefono
    {
        public int IdTelefono { get; set; }
        public int? IdCliente { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? TipoTelefono { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
    }
}
