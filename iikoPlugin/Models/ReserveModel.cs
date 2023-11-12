using System.Runtime.Serialization;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Orders;
using Resto.Front.Api.Data.Organization.Sections;

namespace iikoPlugin
{
    internal class ReserveModel : IReserve
    {
        List<ITable> TablesList;
        public Guid Id { get; private set; }

        public IOrder Order { get; private set; }

        public IClient Client { get; private set; }

        public DateTime? GuestsComingTime { get; private set; }

        public DateTime EstimatedStartTime { get; private set; }

        public string Comment { get; private set; }

        public ReserveStatus Status { get; private set; }

        public ReserveCancelReason? CancelReason { get; private set; }

        public IReadOnlyList<ITable> Tables => TablesList;

        public int GuestsCount { get; private set; }

        public TimeSpan Duration { get; private set; }

        public bool ShouldRemind { get; private set; }

        public Guid ExternalId { get; private set; }

        public Guid LastChangedTerminalId { get; private set; }

        public int Revision { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        public ReserveModel(SerializationInfo info,
                                 StreamingContext context) 
        { 
        }

        public ReserveModel(Guid id, IClient client, DateTime estimatedStartTime, 
           string comment, ReserveStatus status, ITable table, 
           int guestsCount, TimeSpan duration, bool shouldRemind)
        {
            TablesList = new List<ITable>();
            Id = id;
            Client = client;
            EstimatedStartTime = estimatedStartTime;
            Comment = comment;
            Status = status;
            GuestsCount = guestsCount;
            Duration = duration;
            ShouldRemind = shouldRemind;
            TablesList.Add(table);
        }

        public void AddTable(ITable table) => TablesList.Add(table);
        
    }
}
