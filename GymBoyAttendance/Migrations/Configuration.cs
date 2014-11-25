namespace GymBoyAttendance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GymBoyAttendance.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GymBoyAttendance.Models.GymBoyAttendanceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GymBoyAttendance.Models.GymBoyAttendanceContext context)
        {
            var r = new Random();
            var items = Enumerable.Range(1, 50).Select(o => new Attendance
            {
                TrainingDate = new DateTime(2014, r.Next(1, 12), r.Next(1, 28)),
                TrainingLocation = o.ToString(),
                TotalPresent = r.Next(30)
            }).ToArray();
            context.Attendances.AddOrUpdate(item => new { item.TrainingLocation }, items);

            var items2 = Enumerable.Range(1, 50).Select(o => new Person
            {
                FullName = o.ToString(),
                PhoneNumber = o.ToString(),
                LivingArea = o.ToString(),
                PresentCount = r.Next(30)
            }).ToArray();
            context.People.AddOrUpdate(item => new { item.FullName }, items2);
        }
    }
}
