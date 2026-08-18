using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x02000198 RID: 408
	public class DbContext : IDisposable, IObjectContextAdapter
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0003D561 File Offset: 0x0003B761
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected DbContext()
		{
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, this.GetType().DatabaseName()), null);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003D581 File Offset: 0x0003B781
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected DbContext(DbCompiledModel model)
		{
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, this.GetType().DatabaseName()), model);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003D5AD File Offset: 0x0003B7AD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public DbContext(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, nameOrConnectionString), null);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003D5CF File Offset: 0x0003B7CF
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public DbContext(string nameOrConnectionString, DbCompiledModel model)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, nameOrConnectionString), model);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003D5FD File Offset: 0x0003B7FD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public DbContext(DbConnection existingConnection, bool contextOwnsConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			this.InitializeLazyInternalContext(new EagerInternalConnection(this, existingConnection, contextOwnsConnection), null);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003D620 File Offset: 0x0003B820
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public DbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new EagerInternalConnection(this, existingConnection, contextOwnsConnection), model);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003D64F File Offset: 0x0003B84F
		public DbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
		{
			Check.NotNull<ObjectContext>(objectContext, "objectContext");
			DbConfigurationManager.Instance.EnsureLoadedForContext(this.GetType());
			this._internalContext = new EagerInternalContext(this, objectContext, dbContextOwnsObjectContext);
			this.DiscoverAndInitializeSets();
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003D687 File Offset: 0x0003B887
		internal virtual void InitializeLazyInternalContext(IInternalConnection internalConnection, DbCompiledModel model = null)
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(this.GetType());
			this._internalContext = new LazyInternalContext(this, internalConnection, model, DbConfiguration.DependencyResolver.GetService<Func<DbContext, IDbModelCacheKey>>(), DbConfiguration.DependencyResolver.GetService<AttributeProvider>(), null, null);
			this.DiscoverAndInitializeSets();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003D6C3 File Offset: 0x0003B8C3
		private void DiscoverAndInitializeSets()
		{
			new DbSetDiscoveryService(this).InitializeSets();
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003D6D0 File Offset: 0x0003B8D0
		protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003D6D2 File Offset: 0x0003B8D2
		internal void CallOnModelCreating(DbModelBuilder modelBuilder)
		{
			this.OnModelCreating(modelBuilder);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003D6DB File Offset: 0x0003B8DB
		public Database Database
		{
			get
			{
				if (this._database == null)
				{
					this._database = new Database(this.InternalContext);
				}
				return this._database;
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set")]
		public virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return (DbSet<TEntity>)this.InternalContext.Set<TEntity>();
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003D70E File Offset: 0x0003B90E
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set")]
		public virtual DbSet Set(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			return (DbSet)this.InternalContext.Set(entityType);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003D72D File Offset: 0x0003B92D
		public virtual int SaveChanges()
		{
			return this.InternalContext.SaveChanges();
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003D73A File Offset: 0x0003B93A
		public virtual Task<int> SaveChangesAsync()
		{
			return this.SaveChangesAsync(CancellationToken.None);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003D747 File Offset: 0x0003B947
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cancellationToken")]
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return this.InternalContext.SaveChangesAsync(cancellationToken);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003D755 File Offset: 0x0003B955
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		ObjectContext IObjectContextAdapter.ObjectContext
		{
			get
			{
				this.InternalContext.ForceOSpaceLoadingForKnownEntityTypes();
				return this.InternalContext.ObjectContext;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003D770 File Offset: 0x0003B970
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IEnumerable<DbEntityValidationResult> GetValidationErrors()
		{
			List<DbEntityValidationResult> list = new List<DbEntityValidationResult>();
			foreach (DbEntityEntry dbEntityEntry in this.ChangeTracker.Entries())
			{
				if (dbEntityEntry.InternalEntry.EntityType != typeof(EdmMetadata) && this.ShouldValidateEntity(dbEntityEntry))
				{
					DbEntityValidationResult dbEntityValidationResult = this.ValidateEntity(dbEntityEntry, new Dictionary<object, object>());
					if (dbEntityValidationResult != null && !dbEntityValidationResult.IsValid)
					{
						list.Add(dbEntityValidationResult);
					}
				}
			}
			return list;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003D808 File Offset: 0x0003BA08
		protected virtual bool ShouldValidateEntity(DbEntityEntry entityEntry)
		{
			Check.NotNull<DbEntityEntry>(entityEntry, "entityEntry");
			return (entityEntry.State & (EntityState.Added | EntityState.Modified)) != (EntityState)0;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003D825 File Offset: 0x0003BA25
		protected virtual DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			Check.NotNull<DbEntityEntry>(entityEntry, "entityEntry");
			return entityEntry.InternalEntry.GetValidationResult(items);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003D83F File Offset: 0x0003BA3F
		internal virtual DbEntityValidationResult CallValidateEntity(DbEntityEntry entityEntry)
		{
			return this.ValidateEntity(entityEntry, new Dictionary<object, object>());
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003D84D File Offset: 0x0003BA4D
		public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
		{
			Check.NotNull<TEntity>(entity, "entity");
			return new DbEntityEntry<TEntity>(new InternalEntityEntry(this.InternalContext, entity));
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003D871 File Offset: 0x0003BA71
		public DbEntityEntry Entry(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			return new DbEntityEntry(new InternalEntityEntry(this.InternalContext, entity));
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0003D890 File Offset: 0x0003BA90
		public DbChangeTracker ChangeTracker
		{
			get
			{
				return new DbChangeTracker(this.InternalContext);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003D89D File Offset: 0x0003BA9D
		public DbContextConfiguration Configuration
		{
			get
			{
				return new DbContextConfiguration(this.InternalContext);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003D8AA File Offset: 0x0003BAAA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003D8B9 File Offset: 0x0003BAB9
		protected virtual void Dispose(bool disposing)
		{
			this._internalContext.Dispose();
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003D8C6 File Offset: 0x0003BAC6
		internal virtual InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003D8CE File Offset: 0x0003BACE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003D8D6 File Offset: 0x0003BAD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003D8DF File Offset: 0x0003BADF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003D8E7 File Offset: 0x0003BAE7
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003B6 RID: 950
		private InternalContext _internalContext;

		// Token: 0x040003B7 RID: 951
		private Database _database;
	}
}
