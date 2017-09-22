namespace MailChimpPOC
{
    using System;
    using MailChimpPOC.HttpClient;
    using MailChimpPOC.JSonConverter;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            if (args.Length > 1)
            {
                var command = args[0];

                var httpClient = new HttpClientWrapper(new System.Net.Http.HttpClient());
                var jsonConverter = new JsonConverter();
                MailingListManagerMailChimp mailingListManager = new MailingListManagerMailChimp(httpClient, jsonConverter);

                switch (command)
                {
                    case "add":
                        if (args.Length == 5)
                        {
                            var email = args[1];
                            var firstName = args[2];
                            var lastName = args[3];
                            var listId = args[4];

                            var response = await mailingListManager.AddUserToList(email, firstName, lastName, listId);

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine(String.Format("User {0} added successfuly to list id {1}", email, listId));
                            }
                            else
                            {
                                Console.WriteLine(String.Format("Error addind user {0} to list id {1}. StatusCode: {2}. Reason: {3}", email, listId, response.StatusCode, response.ReasonPhrase));
                            }
                        }
                        else
                        {
                            Console.WriteLine("This method receives exactly 5 parameters. add mail firstName lastName listId");
                        }
                        break;

                    case "get":
                        var result = await mailingListManager.GetAllList();
                        Console.WriteLine(String.Format("List result:{0}", result));
                        break;

                    default:
                        Console.WriteLine("Unknown command. The only available options are add and get");
                        break;
                }
            }
            else
            {
                Console.WriteLine("You must especify the command you want to execute. Available options: add get");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
    }
}
