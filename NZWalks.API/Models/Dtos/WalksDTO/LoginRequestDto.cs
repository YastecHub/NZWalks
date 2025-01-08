using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Dtos.WalksDTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
