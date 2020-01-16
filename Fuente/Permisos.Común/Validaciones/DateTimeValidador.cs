using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Permisos.Validations
{
    public class DateTimeValidador : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParse(Convert.ToString(value),
                out dateTime);

            return (isValid && dateTime > DateTime
                .Parse("January 1, 1753"));
        }
    }
}
