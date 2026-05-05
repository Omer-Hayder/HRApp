using API.Data;
using API.DTOs;
using API.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Validators
{
    public class CreateEmployeeValidator: AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator(AppDbContext context)
        {

            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.DepartmentId).GreaterThan(0);
            RuleFor(x => x.ProjectIds).NotEmpty();

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (id, cancellation) => 
                { return await context.Departments.AnyAsync(d => d.Id == id); })
                .WithMessage("Department not found");

            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellation) =>
                {
                    return !await context.Employees.AnyAsync(e => e.Name.Equals(name));
                }).WithMessage("Name is already exist");
        }
    }
}
