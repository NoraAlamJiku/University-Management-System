namespace UniversityManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemaingCredit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "RemainingCredit");
        }
    }
}
