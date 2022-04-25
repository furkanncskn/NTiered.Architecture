using Entity.Concrete;
using Entity.Validations;
using FluentValidation.Results;
using System.Text;

namespace Entity
{
    public static class Util
    {
        public static string GetHasError(ValidationResult result)
        {
            StringBuilder builder = new StringBuilder();

            if (!result.IsValid)
            {
                result.Errors.ForEach(error => builder.AppendLine(error.ErrorMessage));

                return builder.ToString();
            }

            return null;
        }
    }
}
