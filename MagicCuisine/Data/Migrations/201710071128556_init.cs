namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AddressId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AddressId", c => c.Guid(nullable: false));
        }
    }
}
