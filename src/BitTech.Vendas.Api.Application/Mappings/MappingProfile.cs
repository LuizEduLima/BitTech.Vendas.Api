using AutoMapper;
using BitTech.Vendas.Api.Application.Dtos.Garantia;
using BitTech.Vendas.Api.Application.Dtos.Produto;
using BitTech.Vendas.Api.Application.Dtos.Venda;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Produto mappings
        CreateMap<Produto, ProdutoDto>();
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<UpdateProdutoDto, Produto>();

        // Garantia mappings
        CreateMap<Garantia, GarantiaDto>();
        CreateMap<CreateGarantiaDto, Garantia>();
        CreateMap<UpdateGarantiaDto, Garantia>();

        // Venda mappings
        CreateMap<Venda, VendaDto>();  
        CreateMap<CreateVendaDto, Venda>();
        CreateMap<UpdateVendaDto, Venda>();
        CreateMap<ItemVenda, ItemVendaDto>();
        CreateMap<CreateItemVendaDto, ItemVenda>();
        CreateMap<UpdateItemVendaDto, ItemVenda>();
    }
}
