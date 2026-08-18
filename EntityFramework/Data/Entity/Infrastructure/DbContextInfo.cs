using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200074C RID: 1868
	public class DbContextInfo
	{
		// Token: 0x06005480 RID: 21632 RVA: 0x0017146B File Offset: 0x0016F66B
		public DbContextInfo(Type contextType) : this(contextType, null)
		{
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x00171475 File Offset: 0x0016F675
		internal DbContextInfo(Type contextType, Func<IDbDependencyResolver> resolver) : this(Check.NotNull<Type>(contextType, "contextType"), null, AppConfig.DefaultInstance, null, resolver)
		{
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x00171490 File Offset: 0x0016F690
		public DbContextInfo(Type contextType, DbConnectionInfo connectionInfo) : this(Check.NotNull<Type>(contextType, "contextType"), null, AppConfig.DefaultInstance, Check.NotNull<DbConnectionInfo>(connectionInfo, "connectionInfo"), null)
		{
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x001714B5 File Offset: 0x0016F6B5
		[Obsolete("The application configuration can contain multiple settings that affect the connection used by a DbContext. To ensure all configuration is taken into account, use a DbContextInfo constructor that accepts System.Configuration.Configuration")]
		public DbContextInfo(Type contextType, ConnectionStringSettingsCollection connectionStringSettings) : this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<ConnectionStringSettingsCollection>(connectionStringSettings, "connectionStringSettings")), null, null)
		{
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x001714DB File Offset: 0x0016F6DB
		public DbContextInfo(Type contextType, Configuration config) : this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<Configuration>(config, "config")), null, null)
		{
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x00171501 File Offset: 0x0016F701
		public DbContextInfo(Type contextType, Configuration config, DbConnectionInfo connectionInfo) : this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<Configuration>(config, "config")), Check.NotNull<DbConnectionInfo>(connectionInfo, "connectionInfo"), null)
		{
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x00171531 File Offset: 0x0016F731
		public DbContextInfo(Type contextType, DbProviderInfo modelProviderInfo) : this(Check.NotNull<Type>(contextType, "contextType"), Check.NotNull<DbProviderInfo>(modelProviderInfo, "modelProviderInfo"), AppConfig.DefaultInstance, null, null)
		{
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00171556 File Offset: 0x0016F756
		public DbContextInfo(Type contextType, Configuration config, DbProviderInfo modelProviderInfo) : this(Check.NotNull<Type>(contextType, "contextType"), Check.NotNull<DbProviderInfo>(modelProviderInfo, "modelProviderInfo"), new AppConfig(Check.NotNull<Configuration>(config, "config")), null, null)
		{
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00171594 File Offset: 0x0016F794
		internal DbContextInfo(DbContext context, Func<IDbDependencyResolver> resolver = null)
		{
			this._resolver = (() => DbConfiguration.DependencyResolver);
			base..ctor();
			Check.NotNull<DbContext>(context, "context");
			Func<IDbDependencyResolver> resolver2 = resolver;
			if (resolver == null)
			{
				resolver2 = (() => DbConfiguration.DependencyResolver);
			}
			this._resolver = resolver2;
			this._contextType = context.GetType();
			this._appConfig = AppConfig.DefaultInstance;
			InternalContext internalContext = context.InternalContext;
			this._connectionProviderName = internalContext.ProviderName;
			this._connectionInfo = new DbConnectionInfo(internalContext.OriginalConnectionString, this._connectionProviderName);
			this._connectionString = internalContext.OriginalConnectionString;
			this._connectionStringName = internalContext.ConnectionStringName;
			this._connectionStringOrigin = internalContext.ConnectionStringOrigin;
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x00171674 File Offset: 0x0016F874
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		private DbContextInfo(Type contextType, DbProviderInfo modelProviderInfo, AppConfig config, DbConnectionInfo connectionInfo, Func<IDbDependencyResolver> resolver = null)
		{
			this._resolver = (() => DbConfiguration.DependencyResolver);
			base..ctor();
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentOutOfRangeException("contextType");
			}
			Func<IDbDependencyResolver> resolver2 = resolver;
			if (resolver == null)
			{
				resolver2 = (() => DbConfiguration.DependencyResolver);
			}
			this._resolver = resolver2;
			this._contextType = contextType;
			this._modelProviderInfo = modelProviderInfo;
			this._appConfig = config;
			this._connectionInfo = connectionInfo;
			this._activator = this.CreateActivator();
			if (this._activator != null)
			{
				DbContext dbContext = this.CreateInstance();
				if (dbContext != null)
				{
					this._isConstructible = true;
					using (dbContext)
					{
						this._connectionString = DbInterception.Dispatch.Connection.GetConnectionString(dbContext.InternalContext.Connection, new DbInterceptionContext().WithDbContext(dbContext));
						this._connectionStringName = dbContext.InternalContext.ConnectionStringName;
						this._connectionProviderName = dbContext.InternalContext.ProviderName;
						this._connectionStringOrigin = dbContext.InternalContext.ConnectionStringOrigin;
					}
				}
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x001717B0 File Offset: 0x0016F9B0
		public virtual Type ContextType
		{
			get
			{
				return this._contextType;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x001717B8 File Offset: 0x0016F9B8
		public virtual bool IsConstructible
		{
			get
			{
				return this._isConstructible;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x001717C0 File Offset: 0x0016F9C0
		public virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x001717C8 File Offset: 0x0016F9C8
		public virtual string ConnectionStringName
		{
			get
			{
				return this._connectionStringName;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x0600548E RID: 21646 RVA: 0x001717D0 File Offset: 0x0016F9D0
		public virtual string ConnectionProviderName
		{
			get
			{
				return this._connectionProviderName;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x001717D8 File Offset: 0x0016F9D8
		public virtual DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return this._connectionStringOrigin;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x001717E0 File Offset: 0x0016F9E0
		// (set) Token: 0x06005491 RID: 21649 RVA: 0x001717E8 File Offset: 0x0016F9E8
		public virtual Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				return this._onModelCreating;
			}
			set
			{
				this._onModelCreating = value;
			}
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0017180C File Offset: 0x0016FA0C
		public virtual DbContext CreateInstance()
		{
			bool flag = DbConfigurationManager.Instance.PushConfiguration(this._appConfig, this._contextType);
			DbContextInfo.CurrentInfo = this;
			DbContext dbContext = null;
			DbContext result;
			try
			{
				try
				{
					dbContext = ((this._activator == null) ? null : this._activator());
				}
				catch (TargetInvocationException ex)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
					throw ex.InnerException;
				}
				if (dbContext == null)
				{
					result = null;
				}
				else
				{
					dbContext.InternalContext.OnDisposing += delegate(object _, EventArgs __)
					{
						DbContextInfo.CurrentInfo = null;
					};
					if (flag)
					{
						dbContext.InternalContext.OnDisposing += delegate(object _, EventArgs __)
						{
							DbConfigurationManager.Instance.PopConfiguration(this._appConfig);
						};
					}
					dbContext.InternalContext.ApplyContextInfo(this);
					result = dbContext;
				}
			}
			catch (Exception)
			{
				if (dbContext != null)
				{
					dbContext.Dispose();
				}
				throw;
			}
			finally
			{
				if (dbContext == null)
				{
					DbContextInfo.CurrentInfo = null;
					if (flag)
					{
						DbConfigurationManager.Instance.PopConfiguration(this._appConfig);
					}
				}
			}
			return result;
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x00171924 File Offset: 0x0016FB24
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		internal void ConfigureContext(DbContext context)
		{
			if (this._modelProviderInfo != null)
			{
				context.InternalContext.ModelProviderInfo = this._modelProviderInfo;
			}
			context.InternalContext.AppConfig = this._appConfig;
			if (this._connectionInfo != null)
			{
				context.InternalContext.OverrideConnection(new LazyInternalConnection(context, this._connectionInfo));
			}
			else if (this._modelProviderInfo != null && this._appConfig == AppConfig.DefaultInstance)
			{
				context.InternalContext.OverrideConnection(new EagerInternalConnection(context, this._resolver().GetService(this._modelProviderInfo.ProviderInvariantName).CreateConnection(), true));
			}
			if (this._onModelCreating != null)
			{
				context.InternalContext.OnModelCreating = this._onModelCreating;
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x00171A30 File Offset: 0x0016FC30
		private Func<DbContext> CreateActivator()
		{
			ConstructorInfo publicConstructor = this._contextType.GetPublicConstructor(new Type[0]);
			if (publicConstructor != null)
			{
				return () => (DbContext)Activator.CreateInstance(this._contextType);
			}
			Func<DbContext> service = this._resolver().GetService(this._contextType);
			if (service != null)
			{
				return service;
			}
			Type type = (from t in this._contextType.Assembly().GetAccessibleTypes()
			where t.IsClass() && typeof(IDbContextFactory<>).MakeGenericType(new Type[]
			{
				this._contextType
			}).IsAssignableFrom(t)
			select t).FirstOrDefault<Type>();
			if (type == null)
			{
				return null;
			}
			if (type.GetPublicConstructor(new Type[0]) == null)
			{
				throw Error.DbContextServices_MissingDefaultCtor(type);
			}
			return new Func<DbContext>(((IDbContextFactory<DbContext>)Activator.CreateInstance(type)).Create);
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00171AEC File Offset: 0x0016FCEC
		// (set) Token: 0x06005496 RID: 21654 RVA: 0x00171AF3 File Offset: 0x0016FCF3
		internal static DbContextInfo CurrentInfo
		{
			get
			{
				return DbContextInfo._currentInfo;
			}
			set
			{
				DbContextInfo._currentInfo = value;
			}
		}

		// Token: 0x04002283 RID: 8835
		[ThreadStatic]
		private static DbContextInfo _currentInfo;

		// Token: 0x04002284 RID: 8836
		private readonly Type _contextType;

		// Token: 0x04002285 RID: 8837
		private readonly DbProviderInfo _modelProviderInfo;

		// Token: 0x04002286 RID: 8838
		private readonly DbConnectionInfo _connectionInfo;

		// Token: 0x04002287 RID: 8839
		private readonly AppConfig _appConfig;

		// Token: 0x04002288 RID: 8840
		private readonly Func<DbContext> _activator;

		// Token: 0x04002289 RID: 8841
		private readonly string _connectionString;

		// Token: 0x0400228A RID: 8842
		private readonly string _connectionProviderName;

		// Token: 0x0400228B RID: 8843
		private readonly bool _isConstructible;

		// Token: 0x0400228C RID: 8844
		private readonly DbConnectionStringOrigin _connectionStringOrigin;

		// Token: 0x0400228D RID: 8845
		private readonly string _connectionStringName;

		// Token: 0x0400228E RID: 8846
		private readonly Func<IDbDependencyResolver> _resolver;

		// Token: 0x0400228F RID: 8847
		private Action<DbModelBuilder> _onModelCreating;
	}
}
