using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000A RID: 10
	public class IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : DbContext where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim> where TRole : IdentityRole<TKey, TUserRole> where TUserLogin : IdentityUserLogin<TKey> where TUserRole : IdentityUserRole<TKey> where TUserClaim : IdentityUserClaim<TKey>
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000062F7 File Offset: 0x000044F7
		public IdentityDbContext() : this("DefaultConnection")
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00006304 File Offset: 0x00004504
		public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000630D File Offset: 0x0000450D
		public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006318 File Offset: 0x00004518
		public IdentityDbContext(DbCompiledModel model) : base(model)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006321 File Offset: 0x00004521
		public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000632B File Offset: 0x0000452B
		public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00006335 File Offset: 0x00004535
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000633D File Offset: 0x0000453D
		public virtual IDbSet<TUser> Users { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00006346 File Offset: 0x00004546
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000634E File Offset: 0x0000454E
		public virtual IDbSet<TRole> Roles { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00006357 File Offset: 0x00004557
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000635F File Offset: 0x0000455F
		public bool RequireUniqueEmail { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x000065F8 File Offset: 0x000047F8
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			if (modelBuilder == null)
			{
				throw new ArgumentNullException("modelBuilder");
			}
			EntityTypeConfiguration<TUser> entityTypeConfiguration = modelBuilder.Entity<TUser>().ToTable("AspNetUsers");
			entityTypeConfiguration.HasMany<TUserRole>((TUser u) => u.Roles).WithRequired().HasForeignKey<TKey>((TUserRole ur) => ur.UserId);
			entityTypeConfiguration.HasMany<TUserClaim>((TUser u) => u.Claims).WithRequired().HasForeignKey<TKey>((TUserClaim uc) => uc.UserId);
			entityTypeConfiguration.HasMany<TUserLogin>((TUser u) => u.Logins).WithRequired().HasForeignKey<TKey>((TUserLogin ul) => ul.UserId);
			entityTypeConfiguration.Property((TUser u) => u.UserName).IsRequired().HasMaxLength(new int?(256)).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex")
			{
				IsUnique = true
			}));
			entityTypeConfiguration.Property((TUser u) => u.Email).HasMaxLength(new int?(256));
			modelBuilder.Entity<TUserRole>().HasKey((TUserRole r) => new
			{
				r.UserId,
				r.RoleId
			}).ToTable("AspNetUserRoles");
			modelBuilder.Entity<TUserLogin>().HasKey((TUserLogin l) => new
			{
				l.LoginProvider,
				l.ProviderKey,
				l.UserId
			}).ToTable("AspNetUserLogins");
			modelBuilder.Entity<TUserClaim>().ToTable("AspNetUserClaims");
			EntityTypeConfiguration<TRole> entityTypeConfiguration2 = modelBuilder.Entity<TRole>().ToTable("AspNetRoles");
			entityTypeConfiguration2.Property((TRole r) => r.Name).IsRequired().HasMaxLength(new int?(256)).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex")
			{
				IsUnique = true
			}));
			entityTypeConfiguration2.HasMany<TUserRole>((TRole r) => r.Users).WithRequired().HasForeignKey<TKey>((TUserRole ur) => ur.RoleId);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006C1C File Offset: 0x00004E1C
		protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			if (entityEntry != null && entityEntry.State == EntityState.Added)
			{
				List<DbValidationError> list = new List<DbValidationError>();
				TUser user = entityEntry.Entity as TUser;
				if (user != null)
				{
					if (this.Users.Any((TUser u) => string.Equals(u.UserName, user.UserName)))
					{
						list.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateUserName, new object[]
						{
							user.UserName
						})));
					}
					if (this.RequireUniqueEmail && this.Users.Any((TUser u) => string.Equals(u.Email, user.Email)))
					{
						list.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateEmail, new object[]
						{
							user.Email
						})));
					}
				}
				else
				{
					TRole role = entityEntry.Entity as TRole;
					if (role != null && this.Roles.Any((TRole r) => string.Equals(r.Name, role.Name)))
					{
						list.Add(new DbValidationError("Role", string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleAlreadyExists, new object[]
						{
							role.Name
						})));
					}
				}
				if (list.Any<DbValidationError>())
				{
					return new DbEntityValidationResult(entityEntry, list);
				}
			}
			return base.ValidateEntity(entityEntry, items);
		}
	}
}
