namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchedulesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("insert into Schedules(Time)values('8.00 am - 12.00 pm')");
            Sql("insert into Schedules(Time)values('2.00 pm - 5.00 pm')");
            Sql("insert into Schedules(Time)values('7.00 pm - 10.00 pm')");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schedules");
        }
    }
}
