using OneUmbrella.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OneUmbrella.Server.DataTransferObjects
{
    public class AuthLoginDataDTO
    {
        [Required]
        public string HumanIdentifier { get; set; }

        [Required]
        public string HumanPassword { get; set; }
    }

    public class AuthRegisterDataDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string HumanLastName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string HumanFirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string HumanMail { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string HumanPassword { get; set; }

        [RegularExpression(@"^(\+\d{1,4}\s?)?(\d{1,5}[-.\s]?)?(\d{2,15})$")]
        public string HumanPhoneNumber { get; set; }

        [RegularExpression("^(Customer|Owner)$")]
        public string HumanType { get; set; }
    }

    public class AuthTokenDTO
    {
        public string? Token { get; set; }
        public ConfigurationProfileDTO? User { get; set; }
    }
}
