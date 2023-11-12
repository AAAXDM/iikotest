using Resto.Front.Api.Data.Organization.Sections;
using System.Runtime.Serialization;

namespace iikoPlugin.Models
{
    internal class Table : ITable
    {
        public int Number { get; private set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int SeatingCapacity { get; private set; }

        public bool IsActive { get; private set; }

        public IRestaurantSection RestaurantSection { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Number", Number, typeof(int));
            info.AddValue("Id", Number, typeof(Guid));
        }

        public Table(SerializationInfo info, StreamingContext context)
        {
            Number = info.GetInt32("Number");
            Id = (Guid)info.GetValue("Id", typeof(Guid));
        }

        public Table(int number, Guid id)
        {
            Number = number;
            Id = id;
        }
    }
}
