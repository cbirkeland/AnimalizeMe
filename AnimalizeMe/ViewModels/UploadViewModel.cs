using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AnimalizeMe.ViewModels
{
    public class UploadViewModel
    {
		//[Required(ErrorMessage = "Please select file.")]
		//[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg)$", ErrorMessage = "Only Image files allowed.")]
		public string UploadedImage { get; set; }
        public string AnimalImage { get; set; }

		public IFormFile file { get; set; }
	}
}
