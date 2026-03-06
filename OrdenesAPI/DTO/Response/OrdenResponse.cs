namespace OrdenesAPI.DTO.Response
{
    public class OrdenResponse
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }

        public List<ProductoResponse> Productos { get; set; } = new();
    }
}
