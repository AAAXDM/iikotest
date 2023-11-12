using iikoPlugin.Models;
using Resto.Framework.Common;
using Resto.Framework.Common.Print.Tags.Xml;
using Resto.Front.Api;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Common;
using Resto.Front.Api.Data.Organization.Sections;


namespace iikoPlugin
{
    internal class Mock : IDisposable
    {
        List<IReserve> reserves;
        readonly IDisposable subscription;
        private const string Pin = "12344321";


        public Mock() 
        {
            reserves = new List<IReserve>();
            CreateTestData();
            PushToIiko();
            subscription = PluginContext.Notifications.ReserveChanged.Subscribe(x => ChangeReserve(x));
        }

        public void Dispose() => subscription.Dispose();

        void CreateTestData()
        {
            Guid guid = Guid.NewGuid();
            Models.Table table = new Models.Table(1,guid);
            ClientModel client = new ClientModel(guid, "Oleg", "Petrov", "555", new Phone("32535", true), Gender.Male, 1, false);
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromHours(2);
            ReserveModel reserveModel = new ReserveModel(guid,client,dateTime,"add Mock data",ReserveStatus.New,table,2,timeSpan,true);
            reserves.Add(reserveModel);
        }

        void PushToIiko()
        {
            var credentials = PluginContext.Operations.AuthenticateByPin(Pin);

            foreach (var reserve in reserves)
            {
                var session = PluginContext.Operations.CreateEditSession();
                session.CreateReserve(reserve.EstimatedStartTime, reserve.Client, reserve.Tables);
                PluginContext.Operations.SubmitChanges(credentials,session);
            }
        }

        void ChangeReserve(EntityChangedEventArgs<IReserve> res )
        {
            EntityEventType eventType = res.EventType;
            IReserve reserve = res.Entity;
            string tableNames = GetTableNames(reserve.Tables);
            if (eventType == EntityEventType.Created)
            {
                reserves.Add(reserve);
            }
            else if (reserves.Count > 0)
            {
                IReserve baseReserve = reserves.Where(x => x.Id == reserve.Id).FirstOrDefault();
                if (baseReserve != null)
                {
                    tableNames = GetTableNames(baseReserve.Tables);
                    if (eventType == EntityEventType.Removed) reserves.Remove(baseReserve);
                    if (eventType == EntityEventType.Updated) baseReserve = reserve;

                    PluginContext.Log.Info($"tables {tableNames} change status");
                }
                else
                {
                    if (eventType != EntityEventType.Removed) reserves.Add(reserve);
                    PluginContext.Log.Info($"tables {tableNames} change status");
                }

            }
            else
            {
                if (eventType != EntityEventType.Removed) reserves.Add(reserve);
                PluginContext.Log.Info($"tables {tableNames} change status");
            }

            PluginContext.Log.Info("Reserve Changed");
        }

        string GetTableNames(IReadOnlyList<ITable> tables)
        {
            string[] tableNames = new string[tables.Count];
            int i = 0;
            foreach (var table in tables)
            {
                tableNames[i] = table.Number.ToString();
                i++;
            }
            string allTables = String.Join(",", tableNames);

            return allTables;
        }
    }
}
