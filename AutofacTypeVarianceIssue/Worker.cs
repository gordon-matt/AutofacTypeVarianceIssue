using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AutofacTypeVarianceIssue.Data;
using Bogus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutofacTypeVarianceIssue
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private static int userId = 1;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var repository = Context.Container.Resolve<ITestRepository<Data.Entities.Person, int>>();
                var person = FakePeople.Generate();
                await repository.InsertAsync(person);

                var test = await repository.FindOneAsync(x => x.FamilyName == person.FamilyName && x.GivenNames == person.GivenNames);
                Console.WriteLine($"Inserted person with ID: {test.Id}");

                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(2000, stoppingToken);
            }
        }

        private static Faker<Data.Entities.Person> FakePeople
        {
            get
            {
                return new Faker<Data.Entities.Person>()
                    .RuleFor(x => x.Id, x => userId++)
                    .RuleFor(x => x.FamilyName, x => x.Name.LastName())
                    .RuleFor(x => x.GivenNames, x => x.Name.FirstName())
                    .RuleFor(x => x.DateOfBirth, x => x.Date.Between(new DateTime(1945, 1, 1), DateTime.Now));
            }
        }
    }
}