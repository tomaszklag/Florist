namespace Florist.Infrastructure.Data
{
    internal class DbInitializer
    {
        private readonly AppDbContext _ctx;

        private DbInitializer(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public static void Initialize(AppDbContext ctx)
        {
            var initializer = new DbInitializer(ctx);
            initializer.Seed();
        }

        private void Seed()
        {
            _ctx.Database.EnsureCreated();

            // initialize some data

            _ctx.SaveChanges();
        }
    }
}
