namespace TrabalhoDeCompra.Models
{
    public class ItemCompra
    {
        public Guid Id { get;  set; }
        public string Nome { get; set; }
        public int Prioridade { get; set; }

        public int Quantidade { get; set; }

        public string Local { get; set; }

        public ItemCompra(string nome, int prioridade, int quantidade, string local)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Prioridade = prioridade;
            Quantidade = quantidade;
            Local = local;
        }
    }
}
