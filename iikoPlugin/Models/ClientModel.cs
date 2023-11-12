using System.Runtime.Serialization;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Security;

namespace iikoPlugin
{
    internal class ClientModel : IClient
    {
        List<IPhone> PhonesList;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string? Nick { get; private set; }

        public string? Comment { get; private set; }

        public string CardNumber { get; private set; }

        public bool InBlacklist { get; private set; }

        public string? BlacklistReason { get; private set; }

        public IReadOnlyList<IAddress>? Addresses { get; set; }

        public int? MainAddressIndex { get; private set; }

        public IReadOnlyList<IPhone> Phones => PhonesList;

        public IReadOnlyList<IEmail>? Emails { get; set; }

        public IMarketingSource? MarketingSource { get; set; }

        public Guid IikoNetId { get; private set; }

        public Guid IikoBizId { get; private set; }

        public DateTime? DateCreated { get; private set; }

        public IUser? LinkedCounteragent { get; private set; }

        public int Revision { get; private set; }

        public bool ReceivesNotifications { get; private set; }

        public DateTime? BirthDate { get; private set; }

        public DateTime? LastOrderDate { get; private set; }

        public Gender Gender { get; private set; }

        public bool? PersonalDataConsent { get; private set; }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        public ClientModel(SerializationInfo info,
                                 StreamingContext context)
        {
         
        }

        public ClientModel(Guid id, string name, 
            string surname, string cardNumber, IPhone phone, Gender gender,
            int revision, bool receivesNotifications)
        {
            PhonesList = new List<IPhone>();
            Id = id;
            Name = name;
            Surname = surname;
            CardNumber = cardNumber;
            Gender = gender;
            ReceivesNotifications = receivesNotifications;
            Revision = revision;
            PhonesList.Add(phone);
        }
    }
}
