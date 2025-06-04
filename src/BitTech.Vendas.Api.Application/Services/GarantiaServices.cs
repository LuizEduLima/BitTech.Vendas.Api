using AutoMapper;
using BitTech.Vendas.Api.Application.Dtos.Garantia;
using BitTech.Vendas.Api.Application.Interfaces;
using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;

namespace BitTech.Vendas.Api.Application.Services;

public class GarantiaServices(IUnitOfWork unitOfWork, IMapper mapper) : IGarantiaServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GarantiaDto>> CreateAsync(CreateGarantiaDto createDto)
    {
        if (createDto is null)
            return Result<GarantiaDto>.Failure("Dados para criação não podem ser nulos");

        var garantia = _mapper.Map<Garantia>(createDto);

        if (!garantia.IsValid())
        {
            return Result<GarantiaDto>.Failure(garantia.ObterErros);
        }

        await _unitOfWork.GarantiaRepository.CreateAsync(garantia);
        var garantiaDto = _mapper.Map<GarantiaDto>(garantia);

        return Result<GarantiaDto>.Success(garantiaDto);
    }


    public async Task<Result<bool>> DeleteAsync(Guid id)
    {

        if (id == Guid.Empty)
            return Result<bool>.Failure("ID não pode ser vazio");

        var garantiaExistente = await _unitOfWork.GarantiaRepository.GetByIdAsync(id);

        if (garantiaExistente == null)
            return Result<bool>.Failure("Garantia não encontrada para exclusão");

        await _unitOfWork.GarantiaRepository.DeleteAsync(id);
        return Result<bool>.Success(true);

    }

    public async Task<Result<IEnumerable<GarantiaDto>>> GetAllAsync()
    {
        var garantias = await _unitOfWork.GarantiaRepository.GetAllAsync();

        return Result<IEnumerable<GarantiaDto>>.Success(_mapper.Map<IEnumerable<GarantiaDto>>(garantias));

    }
    public async Task<Result<GarantiaDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            return Result<GarantiaDto>.Failure("ID não pode ser vazio");

        var garantia = await _unitOfWork.GarantiaRepository.GetByIdAsync(id);

        if (garantia is null)
            return Result<GarantiaDto>.Failure("Garantia não encontrada");

        return Result<GarantiaDto>.Success(_mapper.Map<GarantiaDto>(garantia));

    }

    public async Task<Result<GarantiaDto>> UpdateAsync(UpdateGarantiaDto updateDto)
    {
        if (updateDto is null)
            return Result<GarantiaDto>.Failure("Dados para atualização não podem ser nulos");

        if (updateDto.Id == Guid.Empty)
            return Result<GarantiaDto>.Failure("ID não pode ser vazio");

        var garantiaExistente = await _unitOfWork.GarantiaRepository.GetByIdAsync(updateDto.Id);

        if (garantiaExistente is null)
            return Result<GarantiaDto>.Failure("Garantia não encontrada para atualização");

        var garantia = _mapper.Map<Garantia>(updateDto);

        if (!garantia.IsValid())
        {
            var errors = garantia.ObterErros.ToList();
            return Result<GarantiaDto>.Failure(errors);
        }

        var garantiaAtualizada = await _unitOfWork.GarantiaRepository.UpdateAsync(garantia);
        var garantiaDto = _mapper.Map<GarantiaDto>(garantiaAtualizada);

        return Result<GarantiaDto>.Success(garantiaDto);

    }
}
