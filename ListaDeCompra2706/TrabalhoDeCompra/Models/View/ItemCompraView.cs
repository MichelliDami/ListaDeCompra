namespace TrabalhoDeCompra.Models.View
{
    public class ItemCompraView
    {
        public string Nome { get; set; }
        public int Prioridade { get; set; }

        public string LocalItem { get; set; }

        public int Quantidade { get; set; }
        public Guid Id { get; set; }
    }
}
