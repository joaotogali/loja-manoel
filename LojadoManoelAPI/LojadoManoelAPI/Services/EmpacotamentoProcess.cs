using LojadoManoelAPI.Models;

namespace LojadoManoelAPI.Services
{
    public class EmpacotamentoProcess
    {
        private class Caixa
        {
            public string CaixaId { get; set; }
            public double VolumeDisponivel { get; set; }
            public List<string> Produtos { get; set; } = new List<string>();
            public string Observacao { get; set; }
        }

        private readonly List<Caixa> caixas = new List<Caixa>()
        {
            new Caixa { CaixaId = "Caixa 1", VolumeDisponivel = 30000},
            new Caixa { CaixaId = "Caixa 1", VolumeDisponivel = 30000},
            new Caixa { CaixaId = "Caixa 1", VolumeDisponivel = 30000}
        };

        public List<object> EmpacotarProduto(List<Produto> produtos)
        {
            var resultado = new List<object>();

            foreach (var produto in produtos)
            {
                double volumeProd = produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;

                var caixa = caixas.FirstOrDefault(c => c.VolumeDisponivel >= volumeProd);

                if (caixa != null)
                {
                    caixa.Produtos.Add(produto.ProdutoId);
                    caixa.VolumeDisponivel -= volumeProd;

                    resultado.Add(new
                    {
                        caixa_id = caixa.CaixaId,
                        produtos = new List<string> { produto.ProdutoId }
                    });
                }
                else
                {
                    resultado.Add(new
                    {
                        caixa_id = (string)null,
                        produtos = new List<string> { produto.ProdutoId },
                        observacao = "Produto não cabe em caixa"
                    });
                }
            }

            return resultado;
        }
    }
}
