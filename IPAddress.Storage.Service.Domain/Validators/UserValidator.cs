using FluentValidation;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;

namespace IPAddress.Storage.Service.Domain.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(IRepository<UserDTO> repository)
        {
            RuleSet("Create", () =>
            {          
                RuleFor(x=>repository.GetAllAsync().Result.FirstOrDefault(y=>y.Id == x.Id)).Null().WithMessage("Запись с таким id существует");
            });

            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id).NotNull().NotEqual(0);
                RuleFor(x => repository.GetById(x.Id)).NotNull().When(x => x.Id != 0).WithMessage("Запись с таким id не существует");
            });

            RuleSet("Delete", () =>
            {
                RuleFor(x => repository.GetById(x.Id)).NotNull().WithMessage("Запись с таким id не существует");
            });
        }
    }
}
