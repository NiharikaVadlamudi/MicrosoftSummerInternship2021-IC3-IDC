using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;

namespace Microsoft.Azure.Cosmos.Samples.Bulk
{
    public class Program
    {
        private const string EndpointUrl = "https://niharika11988.documents.azure.com:443/";
        private const string AuthorizationKey = "Z2x9jKwrBbBFR4tXxshJWX0yqnz2BLd9GbBxd109Lm8TD7gSlrrVqfdveTsr0bWERl6wqHZSJFdyQvhXChQXeQ==";
        private const string DatabaseName = "TenantUserData";
        private const string ContainerName = "UserSet";
       
        static async Task Main(string[] args)
        {

           
            CosmosClient cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey, new CosmosClientOptions() { AllowBulkExecution = true });

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Program.DatabaseName);

            await database.DefineContainer(Program.ContainerName, "/pk")
                    .WithIndexingPolicy()
                        .WithIndexingMode(IndexingMode.Consistent)
                        .WithIncludedPaths()
                            .Attach()
                        .WithExcludedPaths()
                            .Path("/*")
                            .Attach()
                    .Attach()
                .CreateAsync(40000);

            // </Initialize>
            string configJson = System.IO.File.ReadAllText("configFile.json");
            var tenantUsers = JsonConvert.DeserializeObject<Dictionary<string, int>>(configJson);

            foreach(KeyValuePair<string, int> entry in tenantUsers)
            {
                try
                {
                    // Prepare items for insertion
                    Console.WriteLine($"Starting...");
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    Container container = database.GetContainer(ContainerName);
                    List<Task> tasks = new List<Task>(entry.Value);

                    IReadOnlyCollection<User> itemsToInsert = Program.GetItemsToInsert(entry.Key,entry.Value);

                    foreach (User item in itemsToInsert)
                    {
                        tasks.Add(container.CreateItemAsync(item, new PartitionKey(item.pk))
                            .ContinueWith(itemResponse =>
                            {
                                if (!itemResponse.IsCompletedSuccessfully)
                                {
                                    AggregateException innerExceptions = itemResponse.Exception.Flatten();
                                    if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                                    {
                                        Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                                    }
                                }
                            }));
                    }

                    // Wait until all are done
                    await Task.WhenAll(tasks);
                    // </ConcurrentTasks>
                    stopwatch.Stop();

                    Console.WriteLine($"Finished in writing items in {stopwatch.Elapsed}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }

        private static IReadOnlyCollection<User> GetItemsToInsert(string tenantName, int numUsers)
        {
            // Providing options for MS Specific Terms.

            List<string> interpretedUserTypeOptions = new List<string> { "PureOnlineEnabledUserWithSfBAndTeamsLicense" };
            List<string> authorityOptions = new List<string>() { "host", "guest" };
            List<string> nameOptions = new List<string>() { "ServiceAllowed", "ServiceNotAllowed" };
            List<string> capabilityOptions = new List<string> { "Teams", "MCOMEETADD", "MCOEV", "MCOProfessional" };
            List<string> capabilityStatusOptions = new List<string> { "Enabled", "Disabled" };
            List<string> serviceInstanceOptions = new List<string> { "TeamspaceAPI/", "MicrosoftCommunicationsOnline/" };


            // Other Faker Objects .

            Faker<OnlineDialinConferencingPolicy> onlineDailFaker = new Faker<OnlineDialinConferencingPolicy>();
                onlineDailFaker.StrictMode(true)
                        .RuleFor(u => u.authority, f => f.PickRandom(authorityOptions))
                        .RuleFor(u => u.name, f => f.PickRandom(nameOptions));

            Faker<AssignedPlans> AssignedPlansFaker = new Faker<AssignedPlans>();
            AssignedPlansFaker.StrictMode(true)
               .RuleFor(u => u.subscribedPlanId, f => Guid.NewGuid().ToString())
               .RuleFor(u => u.assignedTimestamp, f => f.Person.DateOfBirth)
               .RuleFor(u => u.capability, f => f.PickRandom(capabilityOptions))
               .RuleFor(u => u.capabilityStatus, f => f.PickRandom(capabilityStatusOptions))
               .RuleFor(u => u.servicePlanId, f => f.Random.Guid().ToString())
               .RuleFor(u => u.serviceInstance, f => f.PickRandom(serviceInstanceOptions) + new Randomizer().Replace("????-#M-???"));


            // Final Generation of objects .

            return new Bogus.Faker<User>()
            .StrictMode(false)
            .RuleFor(o => o.id, f => Guid.NewGuid().ToString()) //id
            .RuleFor(o => o.pk, f=>tenantName) //partitionkey
            .RuleFor(u => u.displayName, f => f.Person.FullName)
            .RuleFor(u => u.enterpriseVoiceEnabled, f => f.Random.Bool())
            .RuleFor(u => u.surname, f => f.Person.LastName)
            .RuleFor(u => u.givenName, (f, u) => f.Internet.UserName(u.displayName))
            .RuleFor(u => u.userPrincipalName, (f, u) => f.Person.Email)
            .RuleFor(u => u.objectId, (f,u) => u.id)
            .RuleFor(u => u.usageLocation, f => f.Address.CountryCode())
            .RuleFor(u=>u.interpretedUserType,f=>f.PickRandom(interpretedUserTypeOptions))
            .RuleFor(u => u.onlineDialinConferencingPolicy, f => onlineDailFaker.Generate())
            .RuleFor(u => u.assignedPlans, f => AssignedPlansFaker.Generate(f.Random.Number(1, 20)))
            .Generate(numUsers);
        }
      

        
    }
}
