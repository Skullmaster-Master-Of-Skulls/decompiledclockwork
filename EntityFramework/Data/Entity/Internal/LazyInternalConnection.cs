using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000789 RID: 1929
	internal class LazyInternalConnection : InternalConnection
	{
		// Token: 0x06005740 RID: 22336 RVA: 0x00178534 File Offset: 0x00176734
		public LazyInternalConnection(string nameOrConnectionString) : this(null, nameOrConnectionString)
		{
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0017853E File Offset: 0x0017673E
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LazyInternalConnection(DbContext context, string nameOrConnectionString) : base((context == null) ? null : new DbInterceptionContext().WithDbContext(context))
		{
			this._nameOrConnectionString = nameOrConnectionString;
			this.AppConfig = AppConfig.DefaultInstance;
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x00178569 File Offset: 0x00176769
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LazyInternalConnection(DbContext context, DbConnectionInfo connectionInfo) : base(new DbInterceptionContext().WithDbContext(context))
		{
			this._connectionInfo = connectionInfo;
			this.AppConfig = AppConfig.DefaultInstance;
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x0017858E File Offset: 0x0017678E
		public override DbConnection Connection
		{
			get
			{
				this.Initialize();
				return base.Connection;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x0017859C File Offset: 0x0017679C
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				this.Initialize();
				return this._connectionStringOrigin;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x001785AA File Offset: 0x001767AA
		public override string ConnectionStringName
		{
			get
			{
				this.Initialize();
				return this._connectionStringName;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005746 RID: 22342 RVA: 0x001785B8 File Offset: 0x001767B8
		public override string ConnectionKey
		{
			get
			{
				this.Initialize();
				return base.ConnectionKey;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005747 RID: 22343 RVA: 0x001785C6 File Offset: 0x001767C6
		public override string OriginalConnectionString
		{
			get
			{
				this.Initialize();
				return base.OriginalConnectionString;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x001785D4 File Offset: 0x001767D4
		// (set) Token: 0x06005749 RID: 22345 RVA: 0x001785E2 File Offset: 0x001767E2
		public override string ProviderName
		{
			get
			{
				this.Initialize();
				return base.ProviderName;
			}
			set
			{
				base.ProviderName = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600574A RID: 22346 RVA: 0x001785EC File Offset: 0x001767EC
		public override bool ConnectionHasModel
		{
			get
			{
				if (this._hasModel == null)
				{
					if (base.UnderlyingConnection == null)
					{
						string nameOrConnectionString = this._nameOrConnectionString;
						string text;
						if (this._connectionInfo != null)
						{
							nameOrConnectionString = this._connectionInfo.GetConnectionString(this.AppConfig).ConnectionString;
						}
						else if (DbHelpers.TryGetConnectionName(this._nameOrConnectionString, out text))
						{
							ConnectionStringSettings connectionStringSettings = LazyInternalConnection.FindConnectionInConfig(text, this.AppConfig);
							if (connectionStringSettings == null && DbHelpers.TreatAsConnectionString(this._nameOrConnectionString))
							{
								throw Error.DbContext_ConnectionStringNotFound(text);
							}
							if (connectionStringSettings != null)
							{
								nameOrConnectionString = connectionStringSettings.ConnectionString;
							}
						}
						this._hasModel = new bool?(DbHelpers.IsFullEFConnectionString(nameOrConnectionString));
					}
					else
					{
						this._hasModel = new bool?(base.UnderlyingConnection is EntityConnection);
					}
				}
				return this._hasModel.Value;
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x001786AD File Offset: 0x001768AD
		public override ObjectContext CreateObjectContextFromConnectionModel()
		{
			this.Initialize();
			return base.CreateObjectContextFromConnectionModel();
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x001786BC File Offset: 0x001768BC
		public override void Dispose()
		{
			if (base.UnderlyingConnection != null)
			{
				if (base.UnderlyingConnection is EntityConnection)
				{
					base.UnderlyingConnection.Dispose();
				}
				else
				{
					DbInterception.Dispatch.Connection.Dispose(base.UnderlyingConnection, base.InterceptionContext);
				}
				base.UnderlyingConnection = null;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600574D RID: 22349 RVA: 0x0017870D File Offset: 0x0017690D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal bool IsInitialized
		{
			get
			{
				return base.UnderlyingConnection != null;
			}
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x0017871C File Offset: 0x0017691C
		private void Initialize()
		{
			if (base.UnderlyingConnection == null)
			{
				string text;
				if (this._connectionInfo != null)
				{
					ConnectionStringSettings connectionString = this._connectionInfo.GetConnectionString(this.AppConfig);
					this.InitializeFromConnectionStringSetting(connectionString);
					this._connectionStringOrigin = DbConnectionStringOrigin.DbContextInfo;
					this._connectionStringName = connectionString.Name;
				}
				else if (!DbHelpers.TryGetConnectionName(this._nameOrConnectionString, out text) || !this.TryInitializeFromAppConfig(text, this.AppConfig))
				{
					if (text != null && DbHelpers.TreatAsConnectionString(this._nameOrConnectionString))
					{
						throw Error.DbContext_ConnectionStringNotFound(text);
					}
					if (DbHelpers.IsFullEFConnectionString(this._nameOrConnectionString))
					{
						base.UnderlyingConnection = new EntityConnection(this._nameOrConnectionString);
					}
					else if (base.ProviderName != null)
					{
						this.CreateConnectionFromProviderName(base.ProviderName);
					}
					else
					{
						base.UnderlyingConnection = DbConfiguration.DependencyResolver.GetService<IDbConnectionFactory>().CreateConnection(text ?? this._nameOrConnectionString);
						if (base.UnderlyingConnection == null)
						{
							throw Error.DbContext_ConnectionFactoryReturnedNullConnection();
						}
					}
					if (text != null)
					{
						this._connectionStringOrigin = DbConnectionStringOrigin.Convention;
						this._connectionStringName = text;
					}
					else
					{
						this._connectionStringOrigin = DbConnectionStringOrigin.UserCode;
					}
				}
				base.OnConnectionInitialized();
			}
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x0017882C File Offset: 0x00176A2C
		private bool TryInitializeFromAppConfig(string name, AppConfig config)
		{
			ConnectionStringSettings connectionStringSettings = LazyInternalConnection.FindConnectionInConfig(name, config);
			if (connectionStringSettings != null)
			{
				this.InitializeFromConnectionStringSetting(connectionStringSettings);
				this._connectionStringOrigin = DbConnectionStringOrigin.Configuration;
				this._connectionStringName = connectionStringSettings.Name;
				return true;
			}
			return false;
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x0017888C File Offset: 0x00176A8C
		private static ConnectionStringSettings FindConnectionInConfig(string name, AppConfig config)
		{
			List<string> list = new List<string>
			{
				name
			};
			int num = name.LastIndexOf('.');
			if (num >= 0 && num + 1 < name.Length)
			{
				list.Add(name.Substring(num + 1));
			}
			return (from c in list
			where config.GetConnectionString(c) != null
			select config.GetConnectionString(c)).FirstOrDefault<ConnectionStringSettings>();
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x0017890C File Offset: 0x00176B0C
		private void InitializeFromConnectionStringSetting(ConnectionStringSettings appConfigConnection)
		{
			string providerName = appConfigConnection.ProviderName;
			if (string.IsNullOrWhiteSpace(providerName))
			{
				throw Error.DbContext_ProviderNameMissing(appConfigConnection.Name);
			}
			if (string.Equals(providerName, "System.Data.EntityClient", StringComparison.OrdinalIgnoreCase))
			{
				base.UnderlyingConnection = new EntityConnection(appConfigConnection.ConnectionString);
				return;
			}
			this.CreateConnectionFromProviderName(providerName);
			DbInterception.Dispatch.Connection.SetConnectionString(base.UnderlyingConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(appConfigConnection.ConnectionString));
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00178980 File Offset: 0x00176B80
		private void CreateConnectionFromProviderName(string providerInvariantName)
		{
			DbProviderFactory service = DbConfiguration.DependencyResolver.GetService(providerInvariantName);
			base.UnderlyingConnection = service.CreateConnection();
			if (base.UnderlyingConnection == null)
			{
				throw Error.DbContext_ProviderReturnedNullConnection();
			}
		}

		// Token: 0x04002329 RID: 9001
		private readonly string _nameOrConnectionString;

		// Token: 0x0400232A RID: 9002
		private DbConnectionStringOrigin _connectionStringOrigin;

		// Token: 0x0400232B RID: 9003
		private string _connectionStringName;

		// Token: 0x0400232C RID: 9004
		private readonly DbConnectionInfo _connectionInfo;

		// Token: 0x0400232D RID: 9005
		private bool? _hasModel;
	}
}
