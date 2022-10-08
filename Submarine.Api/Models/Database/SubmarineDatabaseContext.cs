using Microsoft.EntityFrameworkCore;
using Submarine.Core.Database;
using Submarine.Core.Provider;

namespace Submarine.Api.Models.Database;

/// <summary>
/// Submarine Database Context
/// </summary>
public class SubmarineDatabaseContext : DbContext
{
	public DbSet<Provider> Providers { get; set; }
	
	/// <summary>
	/// Configuration of this Database Context
	/// </summary>
	protected readonly IConfiguration Configuration;

	/// <inheritdoc />
	public SubmarineDatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
		=> Configuration = configuration;

	/// <inheritdoc />
	public override int SaveChanges()
	{
		var changedEntries = ChangeTracker.Entries()
			.Where(e => e.State is EntityState.Added or EntityState.Modified)
			.ToList();
		
		var now = DateTimeOffset.Now;

		foreach (var entry in changedEntries)
		{
			if (entry.State == EntityState.Added && entry.Entity is ICreatable creatable)
				creatable.CreatedAt = now;

			if (entry.Entity is IUpdatable updatable)
				updatable.UpdatedAt = now;
		}
		
		return base.SaveChanges();
	}
}
