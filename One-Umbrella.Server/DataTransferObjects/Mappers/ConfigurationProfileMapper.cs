using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class ConfigurationProfileMapper
    {
        public static ConfigurationProfileDTO ToDTO(this Human user)
        {
            return new ConfigurationProfileDTO()
            {
                HumanId = user.HumanId,
                HumanLastName = user.HumanLastName,
                HumanFirstName = user.HumanFirstName,
                HumanMail = user.HumanMail,
                HumanPassword = user.HumanPassword,
                HumanPhoneNumber = user.HumanPhoneNumber,
                HumanDateInscription = user.HumanDateInscription
            };
        }
        public static Human ToEntity(this ConfigurationProfileDataDTO user)
        {
            return new Human()
            {
                HumanLastName = user.HumanLastName,
                HumanFirstName = user.HumanFirstName,
                HumanMail = user.HumanMail,
                HumanPassword = user.HumanPassword,
                HumanPhoneNumber = user.HumanPhoneNumber,
            };
        }
    }
}
