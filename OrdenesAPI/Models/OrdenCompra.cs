using System;
using System.Collections.Generic;

namespace OrdenesAPI.Models;

public partial class OrdenCompra
{
    public int Id { get; set; }

    public string Cliente { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<OrdenProducto> OrdenProductos { get; set; } = new List<OrdenProducto>();
}
