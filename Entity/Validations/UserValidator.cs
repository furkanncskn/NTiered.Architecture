using Entity.Concrete;
using FluentValidation;
using FluentValidation.Results;

namespace Entity.Validations
{
    public class UserValidator: AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(u => u.USER_NAME).NotNull().WithMessage("İsim alanı boş geçilemez");
            RuleFor(u => u.USER_NAME).Length(1, 150).WithMessage("Kullanıcı ismi geçersiz uzunlukta");
            
            RuleFor(u => u.USER_PASSWORD).NotNull().WithMessage("Şifre alanı boş geçilemez");
            RuleFor(u => u.USER_PASSWORD).Length(1, 150).WithMessage("Şifre geçersiz uzunlukta");

            RuleFor(u => u.USER_EMAIL).NotNull().WithMessage("Mail alanı boş geçilemez");
            RuleFor(u => u.USER_EMAIL).Length(1, 150).WithMessage("Mail geçersiz uzunlukta");
            RuleFor(u => u.USER_EMAIL).EmailAddress().WithMessage("Geçersiz mail adresi");
        }

        public static string CheckValidateUser(Users user)
        {
            if (user != null)
            {
                return Util.GetHasError(new UserValidator().Validate(user));
            }
            
            return null;
        }

        public static string CheckValidateUser(ValidationContext<Users> context)
        {
            if (context != null)
            {
                return Util.GetHasError(new UserValidator().Validate(context));
            }

            return null;
        }

        public static string CheckValidateUser(ValidationResult result)
        {
            if (result != null)
            {
                return Util.GetHasError(result);
            }

            return null;
        }
    }
}
