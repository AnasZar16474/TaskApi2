using FluentValidation;

namespace Task2.DTOs.Product
{
    public class ProductCreateValidationDTO : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateValidationDTO()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("the name is Required!!!!!!!!!");
            RuleFor(p => p.Name).MinimumLength(5).MaximumLength(30).WithMessage("the name must be between 5 and 30 char");
            RuleFor(p => p.Price).NotEmpty().WithMessage("the Price is Required!!!!!!!!!");
            RuleFor(p => p.Price).InclusiveBetween(20, 3000).WithMessage("The price must be between 20 and 3000.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("the Description is Required!!!!!!!!!");
            RuleFor(p => p.Description).MinimumLength(10);
        }
    }
}
