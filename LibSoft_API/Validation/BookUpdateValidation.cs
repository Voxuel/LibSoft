using FluentValidation;
using LibSoft_Models;

namespace LibSoft_API.Validation;

public class BookUpdateValidation : AbstractValidator<BookUpdateDTO>
{
    public BookUpdateValidation()
    {
        RuleFor(model => model.Title).NotEmpty();
        RuleFor(model => model.Author).NotEmpty();
        RuleFor(model => model.Description).NotEmpty();
        
        RuleFor(model => model.Title).Length(1, 50);
        RuleFor(model => model.Author).Length(1, 50);
        RuleFor(model => model.Genre).Length(1, 50);
        
        RuleFor(model => model.Year).LessThanOrEqualTo(DateTime.Now.Year);
    }
}