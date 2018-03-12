namespace UniversityManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfddfdfsdf : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teachers", "RemainingCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Double(nullable: false));
        }
    }
}
