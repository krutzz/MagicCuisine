namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CuisineDbContext context;

        public UnitOfWork(CuisineDbContext context)
        {
            this.context = context;
        }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
