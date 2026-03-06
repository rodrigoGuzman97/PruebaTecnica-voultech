using System;
using System.Collections.Generic;

namespace OrdenesAPI.Models;

public partial class OrdenProducto
{
    public int Id { get; set; }

    public int OrdenCompraId { get; set; }

    public int ProductoId { get; set; }

    public virtual OrdenCompra OrdenCompra { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
