using System;
using System.Collections.Generic;

namespace OrdenesAPI.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<OrdenProducto> OrdenProductos { get; set; } = new List<OrdenProducto>();
}
