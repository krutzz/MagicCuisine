namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcountrytotown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Towns", "Country_ID", c => c.Int());
            CreateIndex("dbo.Towns", "Country_ID");
            AddForeignKey("dbo.Towns", "Country_ID", "dbo.Countries", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Towns", "Country_ID", "dbo.Countries");
            DropIndex("dbo.Towns", new[] { "Country_ID" });
            DropColumn("dbo.Towns", "Country_ID");
        }
    }
}
