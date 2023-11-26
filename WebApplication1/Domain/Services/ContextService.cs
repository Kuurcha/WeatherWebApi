using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Domain.Services
{
    public class ContextService
    {
        private readonly ApplicationDbContext _context;

        public ContextService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
