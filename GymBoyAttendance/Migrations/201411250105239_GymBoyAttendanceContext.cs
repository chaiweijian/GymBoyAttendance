namespace GymBoyAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GymBoyAttendanceContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrainingLocation = c.String(),
                        TrainingDate = c.DateTime(),
                        TotalPresent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        LivingArea = c.String(),
                        PresentCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
            DropTable("dbo.Attendances");
        }
    }
}
