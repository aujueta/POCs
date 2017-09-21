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
            if (args.Length == 5)
            {
                var httpClient = new HttpClientWrapper(new System.Net.Http.HttpClient());
                var jsonConverter = new JsonConverter();

                var email = args[1];
                var firstName = args[2];
                var lastName = args[3];
                var listId = args[4];

                MailingListManagerMailChimp mailingListManager = new MailingListManagerMailChimp(httpClient, jsonConverter);
                await mailingListManager.AddUserToList(email, firstName, lastName, listId);
            }
            else
            {
                Console.WriteLine("This method receives exactly 5 parameters. add mail firstName lastName listId");
            }

            Console.ReadKey();
        }
        
    }
}
