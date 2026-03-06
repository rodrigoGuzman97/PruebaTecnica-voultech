namespace OrdenesAPI.DTO.Response
{
    public class ProductoResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Precio { get; set; }
    }
}
