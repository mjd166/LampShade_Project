using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitationAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }


        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;

            var extensions = Path.GetExtension(file.FileName);

            return _validExtensions.Contains(extensions);
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-FileExtensionLimit", ErrorMessage);
        }
    }
}
