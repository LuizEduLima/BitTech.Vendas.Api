using AutoMapper;
using BitTech.Vendas.Api.Application.Dtos.Venda;
using BitTech.Vendas.Api.Application.Interfaces;
using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Application.Services;

public class VendaServices(IUnitOfWork unitOfWork, IMapper mapper) : IVendaServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<VendaDto>> CreateAsync(CreateVendaDto createDto)
    {
        if (createDto is null)
            return Result<VendaDto>.Failure("Dados para criação não podem ser nulos");

        if (createDto.Itens == null || createDto.Itens.Count == 0)
            return Result<VendaDto>.Failure("A venda deve conter pelo menos um item");

        var venda = _mapper.Map<Venda>(createDto);

        if (venda.IsValid())
            return Result<VendaDto>.Failure(venda.ObterErros);

        var errors = await AdicionarVendaAsync(venda);

        if (errors.Count != 0)
            return Result<VendaDto>.Failure(errors);

        var result = await _unitOfWork.VendaRepository.CreateAsync(venda);

        return Result<VendaDto>.Success(_mapper.Map<VendaDto>(result));
    }

    private async Task<List<string>> AdicionarVendaAsync(Venda venda)
    {
        var errors = new List<string>();
        foreach (var item in venda.Itens)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(item.ProdutoId);

            if (produto is null)
            {
                errors.Add($"Produto com ID {item.ProdutoId} não encontrado");
                continue;
            }
            else
            {
                item.Produto = produto;
            }

            if (item.GarantiaId != Guid.Empty)
            {
                var garantia = await _unitOfWork.GarantiaRepository.GetByIdAsync(item.GarantiaId);

                if (garantia is null)
                {
                    errors.Add($"Garantia com ID {item?.Garantia?.Id} não encontrado");
                    continue;
                }
                else
                {
                    item.Garantia = garantia;
                }
            }

            if (!item.IsValid())
            {
                item.ObterErros.ForEach(e => errors.Add(e));
                continue;
            }

            await AtualizarEstoqueProdutoAsync(produto, item.Quantidade);
        }

        return errors;
    }

    private async Task AtualizarEstoqueProdutoAsync(Produto produto, int quantidade)
    {
        produto.DebitarDoEstoque(quantidade);
        await _unitOfWork.ProdutoRepository.UpdateAsync(produto);
    }

    public async Task<Result<IEnumerable<VendaDto>>> GetAllAsync()
    {
        var vendas = await _unitOfWork.VendaRepository.GetAllAsync();
        return Result<IEnumerable<VendaDto>>.Success(_mapper.Map<IEnumerable<VendaDto>>(vendas));
    }

    public async Task<Result<VendaDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            return Result<VendaDto>.Failure("ID não pode ser vazio");

        var venda = await _unitOfWork.VendaRepository.GetByIdAsync(id);

        if (venda is null)
            return Result<VendaDto>.Failure("Venda não encontrada");

        return Result<VendaDto>.Success(_mapper.Map<VendaDto>(venda));
    }

    public async Task<Result<VendaDto>> UpdateAsync(UpdateVendaDto updateVendaDto)
    {
        if (updateVendaDto is null)
            return Result<VendaDto>.Failure("Dados para criação não podem ser nulos");

        if (updateVendaDto.Itens is null || updateVendaDto.Itens.Count == 0)
            return Result<VendaDto>.Failure("A venda deve conter pelo menos um item");

        var venda = await _unitOfWork.VendaRepository.GetByIdAsync(updateVendaDto.Id);

        if (venda is null)
            return Result<VendaDto>.Failure("Venda não encontrada!");

        if (!venda.IsValid())
            return Result<VendaDto>.Failure(venda.ObterErros);

        var errors = new List<string>();

        List<(Guid ProdutoId, int Quantidade)> itensAtual = venda.Itens.Select(i => (i.ProdutoId, i.Quantidade)).ToList();
        var vendaUpdate = _mapper.Map<Venda>(updateVendaDto);

        var erros = await AtualizarVendaAsync(vendaUpdate, itensAtual);

        if (errors.Count != 0)
            return Result<VendaDto>.Failure(errors);

        var result = await _unitOfWork.VendaRepository.UpdateAsync(vendaUpdate);

        return Result<VendaDto>.Success(_mapper.Map<VendaDto>(result));

    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            return Result<bool>.Failure("ID não pode ser vazio");

        var existeVenda = await _unitOfWork.VendaRepository.GetByIdAsync(id);

        if (existeVenda is null)
            return Result<bool>.Failure("Venda não encontrada para exclusão");

        List<(Guid ProdutoId, int Quantidade)> itensAtual = existeVenda.Itens.Select(i => (i.ProdutoId, i.Quantidade)).ToList();

        await AtualizarEstoqueProduto(itensAtual);

        await _unitOfWork.VendaRepository.DeleteAsync(id);

        return Result<bool>.Success(true);

    }

    private async Task AtualizarEstoqueProduto(List<(Guid ProdutoId, int Quantidade)> itensAtual)
    {
        foreach (var prod in itensAtual)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(prod.ProdutoId);
            produto.ReporEstoque(prod.Quantidade);
            await _unitOfWork.ProdutoRepository.UpdateAsync(produto);
        }

    }

    private async Task<List<string>> AtualizarVendaAsync(Venda vendaUpdate, List<(Guid ProdutoId, int Quantidade)> itensAtual)
    {
        var errors = new List<string>();

        foreach (var item in vendaUpdate.Itens)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(item.ProdutoId);

            if (produto is null)
            {
                errors.Add($"Produto com ID {item.ProdutoId} não encontrado");
                continue;
            }
            else
            {
                item.Produto = produto;
            }

            if (item.GarantiaId != Guid.Empty)
            {
                var garantia = await _unitOfWork.GarantiaRepository.GetByIdAsync(item.GarantiaId);

                if (garantia is null)
                {
                    errors.Add($"Garantia com ID {item?.Garantia?.Id} não encontrado");
                    continue;
                }
                else
                {
                    item.Garantia = garantia;
                }
            }

            if (!item.IsValid())
            {
                item.ObterErros.ForEach(e => errors.Add(e));
                continue;
            }

            await AtualizarEstoqueProduto(produto, item, itensAtual);

            await _unitOfWork.ProdutoRepository.UpdateAsync(produto);
        }
        return errors;
    }

    private async Task AtualizarEstoqueProduto(Produto produto, ItemVenda item, List<(Guid ProdutoId, int Quantidade)> itensAtual)
    {
        var itemProdutoVendaAtual = itensAtual.FirstOrDefault(p => p.ProdutoId == produto.Id);

        if (itemProdutoVendaAtual.Quantidade > item.Quantidade)
            produto.ReporEstoque(itemProdutoVendaAtual.Quantidade - item.Quantidade);

        if (itemProdutoVendaAtual.Quantidade < item.Quantidade)
            produto.DebitarDoEstoque(item.Quantidade - itemProdutoVendaAtual.Quantidade);

        await _unitOfWork.ProdutoRepository.UpdateAsync(produto);

    }
}
