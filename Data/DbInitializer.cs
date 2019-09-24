using System;
using System.Linq;
using System.Threading.Tasks;

using MCRoll.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MCRoll.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(IServiceProvider serviceProvider, Boolean isDev)
        {
            using (var context = new MCRollDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MCRollDbContext>>()))
            {
                if (await context.Database.EnsureCreatedAsync())
                {
                    if (isDev)
                    {
                        if (context.Rolls.Any())
                        {
                            return;
                        }

                        await InsertData(context);
                    }
                }
            }
        }

        private async static Task InsertData(MCRollDbContext context)
        {
            Roll roll1 = new Roll()
            {
                Name = "roll1",
                Description = "熔炼炉熔炼炉熔炼炉熔炼炉熔熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉",
                Creator = "roll1c",
                CreateTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromDays(1),
                WinnerNumber = 1,
                IsOpen = true
            };
            Roll roll2 = new Roll()
            {
                Name = "roll2",
                Description = "roll 2",
                Creator = "roll2c",
                CreateTime = DateTime.Now - TimeSpan.FromDays(1),
                EndTime = DateTime.Now,
                WinnerNumber = 1,
                IsOpen = false,
            };
            Roll roll3 = new Roll()
            {
                Name = "roll3",
                Description = "熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉熔炼炉",
                Creator = "roll3c",
                CreateTime = DateTime.Now - TimeSpan.FromDays(1),
                EndTime = DateTime.Now,
                WinnerNumber = 1,
                IsOpen = false
            };
            Roll roll4 = new Roll()
            {
                Name = "roll4",
                Description = "roll 4",
                Creator = "roll4c",
                CreateTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromDays(1),
                WinnerNumber = 1,
                IsOpen = true
            };
            context.Add(roll1);
            context.Add(roll2);
            context.Add(roll3);
            context.Add(roll4);

            Participant participant1 = new Participant()
            {
                Username = "Test User 1",
                Email = "Test1@Test.com",
                Comment = "I am Test User1",
                Roll = roll1
            };
            Participant participant2 = new Participant()
            {
                Username = "Test User 2",
                Email = "Test2@Test.com",
                Comment = "I am Test User2",
                Roll = roll1
            };
            Participant participant3 = new Participant()
            {
                Username = "Test User 1",
                Email = "Test1@Test.com",
                Comment = "I am Test User1",
                Roll = roll2
            };
            Participant participant4 = new Participant()
            {
                Username = "Test User 2",
                Email = "Test2@Test.com",
                Comment = "I am Test User2",
                Roll = roll2
            };
            Participant participant5 = new Participant()
            {
                Username = "Test User 1",
                Email = "Test1@Test.com",
                Comment = "I am Test User1",
                Roll = roll3
            };
            Participant participant6 = new Participant()
            {
                Username = "Test User 2",
                Email = "Test2@Test.com",
                Comment = "I am Test User2",
                Roll = roll3
            };
            Participant participant7 = new Participant()
            {
                Username = "Test User 1",
                Email = "Test1@Test.com",
                Comment = "I am Test User1",
                Roll = roll4
            };
            Participant participant8 = new Participant()
            {
                Username = "Test User 2",
                Email = "Test2@Test.com",
                Comment = "I am Test User2",
                Roll = roll4
            };
            context.Add(participant1);
            context.Add(participant2);
            context.Add(participant3);
            context.Add(participant4);
            context.Add(participant5);
            context.Add(participant6);
            context.Add(participant7);
            context.Add(participant8);

            Winner winner1 = Winner.FromParticipant(participant3);
            Winner winner2 = Winner.FromParticipant(participant6);
            context.Add(winner1);
            context.Add(winner2);

            await context.SaveChangesAsync();
        }
    }
}