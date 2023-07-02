using System.ComponentModel.DataAnnotations.Schema;

namespace OptimunLegalServices.Entities
{
    public class Expertice : BaseEntity
    {
        public string Titles_expertice { get; set; }
        public string Image { get; set; }
        public string Infos { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
