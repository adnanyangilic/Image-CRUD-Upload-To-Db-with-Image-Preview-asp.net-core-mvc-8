using System.ComponentModel.DataAnnotations;

namespace ImageToDb.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Image description is required")]
        public string? ImageDesc { get; set; }
        [Required(ErrorMessage = "Uploader's Name is required")]
        public string? UploadedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Selection of an image is required")]
        [DataType(DataType.Upload)]
        public byte[] Img { get; set; }

    }
}
