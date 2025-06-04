using FluentValidation.Results;

namespace BitTech.Vendas.Api.Domain.Models;

public abstract class Entity
{
    public Guid Id { get; private set; }
    protected abstract ValidationResult Validator { get; }

    protected Entity()
    {
        Id = Guid.NewGuid();        
    }

    public bool IsValid() => Validator.IsValid;
    public List<string> ObterErros => Validator.Errors.Select(e => e.ErrorMessage).ToList();
}