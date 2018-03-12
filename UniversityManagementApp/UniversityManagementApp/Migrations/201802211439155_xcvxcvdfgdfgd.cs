namespace UniversityManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xcvxcvdfgdfgd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "AssiginTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "AssiginTo", c => c.String());
            DropColumn("dbo.Courses", "TeacherId");
        }
    }
}
