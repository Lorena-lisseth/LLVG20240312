using System;
using System.Collections.Generic;

namespace LLVG20240312.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            NumerosTelefonos = new HashSet<NumerosTelefono>();
        }

        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? CorreoElectronico { get; set; }

        public virtual ICollection<NumerosTelefono> NumerosTelefonos { get; set; }
    }
}
