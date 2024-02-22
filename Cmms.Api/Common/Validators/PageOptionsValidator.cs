namespace Cmms.Api.Common.Validators;

using Cmms.Api.Common.ViewModels;
using FluentValidation;

/// <summary>
/// 
/// </summary>
public class PageOptionsValidator : AbstractValidator<PageOptions>
{
    /// <summary>
    /// 
    /// </summary>
    public PageOptionsValidator()
    {
        this.RuleFor(x => x.First).InclusiveBetween(1, 20);
        this.RuleFor(x => x.Last).InclusiveBetween(1, 20);
    }
}
