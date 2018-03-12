namespace UniversityManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdsdasdsfds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "AssiginTo", c => c.String());
            DropColumn("dbo.Courses", "TeacherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "AssiginTo");
        }
    }
}
