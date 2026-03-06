namespace OrdenesAPI.DTO.Request
{
    public class CreateOrdenRequest
    {
        public string Cliente { get; set; } = string.Empty;
        public List<int> ProductosIds { get; set; } = new();
    }
}
