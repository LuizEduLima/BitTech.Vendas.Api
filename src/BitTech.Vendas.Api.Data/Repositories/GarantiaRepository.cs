using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Data.Repositories;

public class GarantiaRepository : BaseRepository<Garantia>,IGarantiaRepository
{
    public GarantiaRepository()
    {       
        SeedData();
    }

    private void SeedData()
    {
        _entities.AddRange(new List<Garantia>
        {
            new Garantia
            {
                
                Nome = "Garantia Estendida Eletrônicos",
                Valor = 150.00m,
                Prazo = 2
            },
            new Garantia
            {
                
                Nome = "Garantia Premium Smartphones",
                Valor = 250.00m,
                Prazo = 3
            },
            new Garantia
            {
                
                Nome = "Garantia Básica",
                Valor = 50.00m,
                Prazo = 1
            }
        });
    }
}
