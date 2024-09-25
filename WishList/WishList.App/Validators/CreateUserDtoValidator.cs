using FluentValidation;
using WishList.BusinessLogic.Models;

namespace WishList.App.Validators
{
    public class CreateUserDtoValidator
    {
        public class CreateToDoItemDtoValidator : AbstractValidator<CreateUserDto>
        {
            public CreateToDoItemDtoValidator()
            {
                RuleFor(x => x.Name)
                    .MaximumLength(100)
                    .Must(x => x.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
            }
        }
    }
}
