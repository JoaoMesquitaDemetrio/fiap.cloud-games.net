using FluentValidation;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Sample.Utils.Extensions;

namespace Fiap.Cloud.Games.Core.Application.Validations;

public class GameUpdateValidation : AbstractValidator<GameUpdate>
{
    public GameUpdateValidation()
    {
        RuleFor(x => x.Id).Custom((id, context) =>
        {
            if (id.IsNullGuid())
                context.AddFailure("Id", "Jogo inválido.");
        });

        RuleFor(x => x.Name).NotEmpty().MaximumLength(500).WithMessage("O Nome não pode ser vazio e deve ter no máximo 500 caracteres.");
        RuleFor(x => x.Studio).NotEmpty().MaximumLength(500).WithMessage("O Estúdio não pode ser vazio e deve ter no máximo 500 caracteres.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("O Preço deve ser maior que a zero.");
    }
}
