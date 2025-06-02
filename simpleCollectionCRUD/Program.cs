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
                string name;
                Console.WriteLine("Choose Mode: \n0.Exit\n1.Add new consumer\n2.Find consumer\n3.Delete consumer\n4.Update Consumer name\n5.List all consumer");
                Console.Write("Input mode number : ");
                string Mode = Console.ReadLine();
                switch (Mode)
                {
                    case "0":
                        Console.WriteLine("Thank you for using this program");
                        return;
                    case "1":
                        Console.Write("Enter Consumer Name to add : ");
                        name = Console.ReadLine();
                        bool res = ConsumerManager.addConsumer(name);
                        if (res == true)
                        {
                            Console.WriteLine($"Successfully add consumer named {name}");
                            name = "";
                        }
                        else
                        {
                            Console.WriteLine("Failed to add consumer");
                            name = "";
                        }
                        break;
                    case "2":
                        Console.Write("Enter Consumer Name to find : ");
                        name = Console.ReadLine();
                        var consfind = ConsumerManager.FindConsumer(name);
                        if (consfind == null)
                        {
                            Console.WriteLine($"There are no consumer with name {name}");
                            name = "";
                        }
                        else
                        {
                            Console.WriteLine($"Found {consfind.Name} with Id of {consfind.Id}");
                            name = "";
                        }

                        break;
                    case "3":
                        Console.WriteLine("Enter Consumer Name to delete : ");
                        name = Console.ReadLine();
                        var consdel = ConsumerManager.DeleteConsumers(name);
                        if(consdel == true)
                        {
                            Console.WriteLine($"Consumer with the name {name} been successfully deleted");
                        }
                        else
                        {
                            Console.WriteLine($"There are no consumer with the name {name}");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Enter Consumer Name to change : ");
                        name = Console.ReadLine();
                        consfind = ConsumerManager.FindConsumer(name);
                        if (consfind == null)
                        {
                            Console.WriteLine($"There are no consumer with the name {name}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter new consumer name : ");
                            string newName = Console.ReadLine();
                            ConsumerManager.UpdateConsumer(name, newName);
                            Console.WriteLine($"{name} been successfully changed to {newName}");
                            break;
                        }
                    case "5":
                        List<Consumers> ListConsumers = ConsumerManager.ListAllConsumers();
                        if(ListConsumers.Count == 0)
                        {
                            Console.WriteLine("There are no consumers in the list yet");
                        }
                        else
                        {
                            foreach (var consumers in ListConsumers)
                            {
                                Console.WriteLine($"ID: {consumers.Id}, Name: {consumers.Name}");
                            }
                        }
                       
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

            public static bool addConsumer(string Consname)
            {
                Random rand = new Random();
                int randomID = rand.Next(10001, 1000000);
                People.Add(new Consumers { Id = randomID - rand.Next(0, 10000), Name = Consname });
                Consumers name = People.Find(x => x.Name.Contains(Consname));
                if(name == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public static List<Consumers> ListAllConsumers()
            {
                return People;
            }
            public static Consumers FindConsumer(string Consname)
            {
                Consumers name = People.Find(x => x.Name.Contains(Consname));
                if (name == null)
                {
                    return null;
                }
                else
                {
                    return name;
                }
                
            }
            public static Consumers UpdateConsumer(string Consname, string newName)
            {
                Consumers name = People.Find(x => x.Name.Contains(Consname));
                if (name == null)
                {
                    return null;
                }
                else
                {
                    name.Name = newName;
                    return name;
                }
            }
            public static bool DeleteConsumers(string Consname)
            {
                Consumers name = People.Find(x => x.Name.Contains(Consname));
                if(name != null)
                {
                    People.Remove(name);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
