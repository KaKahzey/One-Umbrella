using System.ComponentModel.DataAnnotations;

namespace OneUmbrella.Server.DataTransferObjects
{
    public class ConfigurationProfileDTO
    {
        public int HumanId { get; set; }
        public string HumanLastName { get; set; }
        public string HumanFirstName { get; set; }
        public string HumanMail { get; set; }
        public string HumanPassword { get; set; }
        public string HumanPhoneNumber { get; set; }
        public DateTime HumanDateInscription { get; set; }
        public string HumanType { get; set; }
    }
    public class ConfigurationProfileDataDTO
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
    }
}
