using TrabalhoDeCompra.Models.Dtos;
using TrabalhoDeCompra.Models;
using TrabalhoDeCompra.Models.View;


namespace TrabalhoDeCompra.Service
{
    public class ListaCompraService
    {
        private static List<ItemCompra> lista = new List<ItemCompra>();

        public void Inserir(ItemCompraDto dto)
        {
            var item = new ItemCompra(dto.Nome, dto.Prioridade,dto.Quantidade,dto.LocalItem);
            lista.Add(item);
        }

        public List<ItemCompraView> ObterTodosOrdenadosPorPrioridade()
        {
            var copia = new List<ItemCompra>(lista); // não altera a original
            HeapSort(copia); // ordena por prioridade (do menor pro maior)
            copia.Reverse(); // inverte para ficar do maior pro menor

            var resultado = copia.Select(i => new ItemCompraView
            {
                Id = i.Id,
                Nome = i.Nome,
                Prioridade = i.Prioridade,
                LocalItem = i.Local,      // Verifique se na sua classe Model o campo é "Local" ou "LocalItem"
                Quantidade = i.Quantidade
            }).ToList();

            return resultado;
        }

        // HeapSort como mostrado antes
        private void HeapSort(List<ItemCompra> lista)
        {
            int n = lista.Count;

            // Constrói o heap (máximo)
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(lista, n, i);
            }

            // Extrai os elementos do heap um a um
            for (int i = n - 1; i > 0; i--)
            {
                // Troca o primeiro (maior) com o último não ordenado
                var temp = lista[0];
                lista[0] = lista[i];
                lista[i] = temp;

                // Reorganiza o heap reduzido
                Heapify(lista, i, 0);
            }



        }

        private void Heapify(List<ItemCompra> lista, int tamanho, int indicePai)
        {
            int maior = indicePai;
            int esquerda = 2 * indicePai + 1;
            int direita = 2 * indicePai + 2;

            // Se o filho da esquerda é maior que o pai
            if (esquerda < tamanho && lista[esquerda].Prioridade > lista[maior].Prioridade)
            {
                maior = esquerda;
            }

            // Se o filho da direita é maior que o maior até agora
            if (direita < tamanho && lista[direita].Prioridade > lista[maior].Prioridade)
            {
                maior = direita;
            }

            // Se o maior não é o pai, faz a troca e continua o heapify
            if (maior != indicePai)
            {
                var temp = lista[indicePai];
                lista[indicePai] = lista[maior];
                lista[maior] = temp;

                Heapify(lista, tamanho, maior); // chamada recursiva
            }
        }

        public bool DeletarPorId(Guid id)
        {
            var item = lista.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                lista.Remove(item);
                return true;
            }
            return false;
        }
    }
}
