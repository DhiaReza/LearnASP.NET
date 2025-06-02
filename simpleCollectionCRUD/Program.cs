using System;
using System.Security.Cryptography;

namespace simpleCollectionCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose Mode: \n0.Exit\n1.Add new consumer\n2.Find consumer\n3.Delete consumer\n4.List all consumer");
                Console.Write("Input mode number : ");
                string Mode = Console.ReadLine();
                switch (Mode)
                {
                    case "0":
                        Console.WriteLine("Thank you for using this program");
                        return;
                    case "1":
                        Console.Write("Enter Consumer Name");
                        string name = Console.ReadLine();

                        break;
                    case "2":

                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Mode not recognized");
                        break;
                }
            }
        }
        public class Consumers()
        {
            int id;
            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }
        public static class ConsumerManager
        {
            private static List<Consumers> People = new List<Consumers>();

            public static void addConsumer(string Consname)
            {
                Random rand = new Random();
                int randomID = rand.Next(10001, 1000000);
                People.Add(new Consumers { Id = randomID - rand.Next(0, 10000), Name = Consname });
            }

            public static List<Consumers> ListAllConsumers()
            {
                return People;
            }
            public static Consumers FindConsumer(string Consname)
            {
                Consumers name = People.Find(x => x.Name.Contains(Consname));
                return name;
            }
        }
    }
}
