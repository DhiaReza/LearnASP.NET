namespace MVCdemo.Models
{
    public class ConsumerManager
    {
        public Consumers getConsumer()
        {
            Consumers consumer = new Consumers()
            {
                Id = 1,
                name = "test",
                age = 3
            };
            return consumer;
        }
    }
}
