namespace ApiGateway.Dto
{
    public class ArticuloInventarioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal Stock { get; set; }

        public decimal PrecioVenta { get; set; }
    }
}
