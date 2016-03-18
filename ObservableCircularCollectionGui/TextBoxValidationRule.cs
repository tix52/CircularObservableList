using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ObservableCircularCollectionGui
{
    public class TextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = value as string;
            if (string.IsNullOrEmpty(content)) return new ValidationResult(false, value);
            int result;
            if (int.TryParse(content, out result))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, content);
        }
    }
}
