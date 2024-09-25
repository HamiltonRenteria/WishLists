using Application.DTOs.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ProductValidator :AbstractValidator<ProductRequestDto>
    {
        public ProductValidator() {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El nombre del producto no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre del producto no puede estar vacio.");

            RuleFor(x => x.Category)
                .NotNull().WithMessage("La categoria del producto no puede ser nulo.")
                .NotEmpty().WithMessage("La categoria del producto no puede estar vacio.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("El precio del producto no puede ser nulo.")
                .NotEmpty().WithMessage("El precio del producto no puede estar vacio.");
        }
    }
}
