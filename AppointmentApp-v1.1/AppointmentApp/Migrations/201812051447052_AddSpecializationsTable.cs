namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecializationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("insert into Specializations(Name)values('Immunologists')");
            Sql("insert into Specializations(Name)values('Cardiologists')");
            Sql("insert into Specializations(Name)values('Endocrinologists')");
            Sql("insert into Specializations(Name)values('Gastroenterologists')");
            Sql("insert into Specializations(Name)values('Geriatric Medicine Specialists')");


        }
        
        public override void Down()
        {
            DropTable("dbo.Specializations");
        }
    }
}
