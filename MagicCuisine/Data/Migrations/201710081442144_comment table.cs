namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Recipe_ID = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Recipes", t => t.Recipe_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Recipe_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Recipe_ID", "dbo.Recipes");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Recipe_ID" });
            DropTable("dbo.Comments");
        }
    }
}
