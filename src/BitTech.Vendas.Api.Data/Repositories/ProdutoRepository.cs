using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Data.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository()
    {       
        SeedData();
    }

    private void SeedData()
    {
        _entities.AddRange(new List<Produto>
        {
            new Produto
            {                
                Nome = "Smartphone Samsung Galaxy",
                Valor = 1200.00m,
                EstoqueMinimo = 5,
                EstoqueMaximo = 50,
                SaldoEmEstoque = 25,
                Fornecedor = "Samsung Electronics",
                PossuiGarantia = true
            },
            new Produto
            {                
                Nome = "Notebook Dell Inspiron",
                Valor = 2500.00m,
                EstoqueMinimo = 2,
                EstoqueMaximo = 20,
                SaldoEmEstoque = 8,
                Fornecedor = "Dell Technologies",
                PossuiGarantia = true
            },
            new Produto
            {               
                Nome = "Mouse Logitech",
                Valor = 45.90m,
                EstoqueMinimo = 10,
                EstoqueMaximo = 100,
                SaldoEmEstoque = 35,
                Fornecedor = "Logitech",
                PossuiGarantia = false
            }
        });
    }
}