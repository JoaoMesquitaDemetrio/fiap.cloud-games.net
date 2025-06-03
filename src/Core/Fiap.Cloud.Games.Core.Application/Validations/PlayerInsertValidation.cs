
using FluentValidation;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;

namespace Fiap.Cloud.Games.Core.Application.Validations;

public class PlayerInsertValidation : AbstractValidator<PlayerInsert>
{
    public PlayerInsertValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(500).WithMessage("O Nome não pode ser vazio e deve ter no máximo 500 caracteres.") ;
        RuleFor(x => x.Email).NotEmpty().MaximumLength(500).WithMessage("O E-mail não pode ser vazio e deve ter no máximo 500 caracteres.") ;
        RuleFor(x => x.Password).NotEmpty().MaximumLength(500).WithMessage("O Senha não pode ser vazia e deve ter no máximo 500 caracteres.") ;
    }
}
