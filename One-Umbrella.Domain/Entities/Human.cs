using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_Umbrella.Domain.Entities
{
    internal class Human
    {
        private string humanType;
        public int HumanId { get; set; }
        public string HumanLastName { get; set; }
        public string HumanFirstName { get; set; }
        public string HumanMail { get; set; }
        public string HumanPassword { get; set; }
        public string HumanPhoneNumber { get; set; }
        public DateTime HumanDateInscription { get; }
        public string HumanType
        {
            get
            {
                return humanType;
            }
            set
            {
                if (value == "Owner" || value == "Customer")
                {
                    humanType = value;
                }
            }
        }
    }
}
