using Entity.Concrete;
using Entity.Validations;
using FluentValidation.Results;
using System.Text;

namespace Entity
{
    public static class Util
    {
        public static string HasError(ValidationResult result)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                result.Errors.ForEach(error => builder.AppendLine(error.ErrorMessage));

                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
