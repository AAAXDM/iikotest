using Resto.Front.Api;


namespace iikoPlugin
{
    public class TestPlugin : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        public TestPlugin()
        {
            Mock mock = new Mock();
            subscriptions.Push(mock);
            PluginContext.Log.Info("TestPlugin started");
        }

        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                subscription.Dispose();
            }
        }

    }
}