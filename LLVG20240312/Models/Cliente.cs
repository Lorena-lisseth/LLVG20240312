    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LLVG20240312.Models
{
   public partial class Cliente
    {
        public Cliente()
        {
            NumerosTelefonos = new List<NumerosTelefono>();
        }

        public int IdCliente { get; set; }
        [Required(ErrorMessage ="Es necesario ingresar el nombre")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Es necesario ingresar la dirección")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "Es necesario ingresar la dirección")]
        [Display(Name = "Correo Electonico")]
        public string? CorreoElectronico { get; set; }

        public virtual IList<NumerosTelefono> NumerosTelefonos { get; set; }
    }
}
