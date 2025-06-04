using AutoMapper;
using BitTech.Vendas.Api.Application.Dtos.Produto;
using BitTech.Vendas.Api.Application.Interfaces;
using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Application.Services;

public class ProdutoServices(IUnitOfWork unitOfWork, IMapper mapper) : IProdutoServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProdutoDto>> CreateAsync(CreateProdutoDto createDto)
    {
        if (createDto is null)
            return Result<ProdutoDto>.Failure("Dados para criação não podem ser nulos");

        var produto = _mapper.Map<Produto>(createDto);

        if (!produto.IsValid())
            return Result<ProdutoDto>.Failure(produto.ObterErros);

        await _unitOfWork.ProdutoRepository.CreateAsync(produto);

        return Result<ProdutoDto>.Success(_mapper.Map<ProdutoDto>(produto));

    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {

        if (id == Guid.Empty)
            return Result<bool>.Failure("ID não pode ser vazio");

        var produtoExistente = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);

        if (produtoExistente is null)
            return Result<bool>.Failure("Produto não encontrado para exclusão");

        await _unitOfWork.ProdutoRepository.DeleteAsync(id);
        return Result<bool>.Success(true);

    }

    public async Task<Result<IEnumerable<ProdutoDto>>> GetAllAsync()
    {
        var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
        return Result<IEnumerable<ProdutoDto>>.Success(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));
    }

    public async Task<Result<ProdutoDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            return Result<ProdutoDto>.Failure("ID não pode ser vazio");

        var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);

        if (produto is null)
            return Result<ProdutoDto>.Failure("Produto não encontrado");

        return Result<ProdutoDto>.Success(_mapper.Map<ProdutoDto>(produto));
    }

    public async Task<Result<ProdutoDto>> UpdateAsync(UpdateProdutoDto updateDto)
    {

        if (updateDto is null)
            return Result<ProdutoDto>.Failure("Dados para atualização não podem ser nulos");

        if (updateDto.Id == Guid.Empty)
            return Result<ProdutoDto>.Failure("ID não pode ser vazio");

        var produtoExistente = await _unitOfWork.ProdutoRepository.GetByIdAsync(updateDto.Id);

        if (produtoExistente is null)
            return Result<ProdutoDto>.Failure("Produto não encontrado para atualização");

        var produto = _mapper.Map<Produto>(updateDto);


        if (!produto.IsValid())
            return Result<ProdutoDto>.Failure(produto.ObterErros);

        var produtoAtualizado = await _unitOfWork.ProdutoRepository.UpdateAsync(produto);

        return Result<ProdutoDto>.Success(_mapper.Map<ProdutoDto>(produtoAtualizado));
    }
}