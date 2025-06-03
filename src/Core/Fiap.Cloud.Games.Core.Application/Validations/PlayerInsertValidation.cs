
using FluentValidation;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;

namespace Fiap.Cloud.Games.Core.Application.Validations;

public class PlayerInsertValidation : AbstractValidator<PlayerInsert>
{
    public PlayerInsertValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(500).WithMessage("O Nome não pode ser vazio e deve ter no máximo 500 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().MaximumLength(500).WithMessage("O E-mail não pode ser vazio e deve ter no máximo 500 caracteres.")
            .EmailAddress().WithMessage("O E-mail deve ser um endereço de e-mail válido.");

        RuleFor(x => x.Password)
            .NotEmpty().MaximumLength(500).WithMessage("A Senha não pode ser vazia e deve ter no máximo 500 caracteres.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um dígito numérico")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("A senha deve conter pelo menos um caractere especial");
    }
}
