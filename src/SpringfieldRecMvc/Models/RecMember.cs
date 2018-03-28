using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class RecMember : BaseEntity
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        private List<TelephoneNumber> telephoneNumbers = new List<TelephoneNumber>();
        public IEnumerable<TelephoneNumber> TelephoneNumbers
        {
            get
            {
                return this.telephoneNumbers;
            }

            private set
            {
                this.telephoneNumbers = value?.ToList() ?? new List<TelephoneNumber>();
            }
        }

        public TelephoneNumber PrimaryTelephoneNumber
        {
            get
            {
                // TODO: return the first item by default for now, but allow future expansion to having multiple numbers
                return this.telephoneNumbers.FirstOrDefault();
            }
        }

        public void AddNumber(string number)
        {
            TelephoneNumber t = new TelephoneNumber();
            t.Number = number;
            t.Owner = this;

            this.telephoneNumbers.Add(t);
        }

        private List<EmailAddress> emailAddresses = new List<EmailAddress>();
        public IEnumerable<EmailAddress> EmailAddresses
        {
            get
            {
                return this.emailAddresses;
            }
        }

        public EmailAddress PrimaryEmailAddress
        {
            get
            {
                // TODO: return the first item by default for now, but allow future expansion to having multiple email addresses
                return this.emailAddresses.FirstOrDefault();
            }
        }

        public void AddEmailAddress(string address)
        {
            EmailAddress e = new EmailAddress();
            e.Address = address;
            e.Owner = this;

            this.emailAddresses.Add(e);
        }

    }
}
