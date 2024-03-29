﻿using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class AppDbContext : DbContext {
	// this is where tables are defined
	public DbSet<User> Users {
		get; set;
	}
	public DbSet<Message> Messages {
		get; set;
	}
	public DbSet<Achievement> Achievements {
		get; set;
	}
	public DbSet<Community> Communities {
		get; set;
	}
	public DbSet<Post> Posts {
		get; set;
	}
	public DbSet<Comment> Comments {
		get; set;
	}
	public DbSet<AchievementUser> AchievementUsers {
		get; set;
	}
	public DbSet<CommunityUser> CommunityUsers {
		get; set;
	}
	public DbSet<Vote> Votes {
		get; set;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		// SqlServer authentication
		// optionsBuilder.UseSqlServer("Data Source = myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;").LogTo(Console.WriteLine);

		// Windows authentication (the one we use)
		optionsBuilder
			.UseNpgsql("Server=localhost;Database=RedditApp;User Id=postgres;Password=postgres;TrustServerCertificate=True;")
			.LogTo(Console.WriteLine);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		// Message related configuration
		modelBuilder.Entity<Message>()
			.HasOne(m => m.Sender)
			.WithMany(u => u.SentMessages)
			.HasForeignKey(m => m.SenderId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Message>()
			.HasOne(m => m.Receiver)
			.WithMany(u => u.ReceivedMessages)
			.HasForeignKey(m => m.ReceiverId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<User>()
			.HasMany(u => u.SentMessages)
			.WithOne(m => m.Sender)
			.HasForeignKey(m => m.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<User>()
			.HasMany(u => u.ReceivedMessages)
			.WithOne(m => m.Receiver)
			.HasForeignKey(m => m.ReceiverId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<User>()
			.HasOne(u => u.ModeratedCommunity)
			.WithOne(c => c.Moderator)
			.HasForeignKey<User>(u => u.ModeratedCommunityId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Community>()
			.HasOne(c => c.Moderator)
			.WithOne(u => u.ModeratedCommunity)
			.HasForeignKey<Community>(c => c.ModeratorId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<User>()
			.HasMany(u => u.Comments)
			.WithOne(c => c.Author)
			.HasForeignKey(c => c.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Comment>()
			.HasOne(c => c.Author)
			.WithMany(a => a.Comments)
			.HasForeignKey(c => c.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);

		base.OnModelCreating(modelBuilder);
	}
}