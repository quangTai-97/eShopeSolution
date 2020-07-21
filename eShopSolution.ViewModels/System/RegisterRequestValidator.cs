using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace eShopSolution.ViewModels.System
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Can not over 200 characters");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Can not over 200 characters");

            //k đc lơn hơn 1l tuổi
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100years is required");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email not for match");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Pass word is required")
                .MinimumLength(6).WithMessage("Pass is leatest min 6  character");

            //Custom validator
            RuleFor(x => x).Custom((request,context) =>{
                if(request.Password != request.ComfirmPassword)
                {
                    context.AddFailure("Comfirm password is not match");
                }    
            });
            
        }

    
    }
}
