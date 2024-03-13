using System;
using System.Collections.Generic;

namespace LLVG20240312.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            NumerosTelefono = new List<NumerosTelefono>();
        }

        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? CorreoElectronico { get; set; }

        public virtual IList<NumerosTelefono> NumerosTelefono { get; set; }
    }
}
