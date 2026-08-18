using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020006CD RID: 1741
	public class DbMigrationsConfiguration
	{
		// Token: 0x06004552 RID: 17746 RVA: 0x00146AA8 File Offset: 0x00144CA8
		public DbMigrationsConfiguration() : this(new Lazy<IDbDependencyResolver>(() => DbConfiguration.DependencyResolver))
		{
			this.CodeGenerator = new CSharpMigrationCodeGenerator();
			this.ContextKey = base.GetType().ToString();
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x00146AF9 File Offset: 0x00144CF9
		internal DbMigrationsConfiguration(Lazy<IDbDependencyResolver> resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06004554 RID: 17748 RVA: 0x00146B34 File Offset: 0x00144D34
		// (set) Token: 0x06004555 RID: 17749 RVA: 0x00146B3C File Offset: 0x00144D3C
		public bool AutomaticMigrationsEnabled { get; set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x00146B45 File Offset: 0x00144D45
		// (set) Token: 0x06004557 RID: 17751 RVA: 0x00146B4D File Offset: 0x00144D4D
		public string ContextKey
		{
			get
			{
				return this._contextKey;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._contextKey = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x00146B62 File Offset: 0x00144D62
		// (set) Token: 0x06004559 RID: 17753 RVA: 0x00146B6A File Offset: 0x00144D6A
		public bool AutomaticMigrationDataLossAllowed { get; set; }

		// Token: 0x0600455A RID: 17754 RVA: 0x00146B73 File Offset: 0x00144D73
		public void SetSqlGenerator(string providerInvariantName, MigrationSqlGenerator migrationSqlGenerator)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<MigrationSqlGenerator>(migrationSqlGenerator, "migrationSqlGenerator");
			this._sqlGenerators[providerInvariantName] = migrationSqlGenerator;
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00146B9C File Offset: 0x00144D9C
		public MigrationSqlGenerator GetSqlGenerator(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			MigrationSqlGenerator result;
			if (!this._sqlGenerators.TryGetValue(providerInvariantName, out result))
			{
				Func<MigrationSqlGenerator> service = this._resolver.Value.GetService(providerInvariantName);
				if (service == null)
				{
					throw Error.NoSqlGeneratorForProvider(providerInvariantName);
				}
				result = service();
			}
			return result;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00146BE9 File Offset: 0x00144DE9
		public void SetHistoryContextFactory(string providerInvariantName, Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._historyContextFactories[providerInvariantName] = factory;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00146C10 File Offset: 0x00144E10
		public Func<DbConnection, string, HistoryContext> GetHistoryContextFactory(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Func<DbConnection, string, HistoryContext> result;
			if (!this._historyContextFactories.TryGetValue(providerInvariantName, out result))
			{
				return this._resolver.Value.GetService(providerInvariantName) ?? this._resolver.Value.GetService<Func<DbConnection, string, HistoryContext>>();
			}
			return result;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x00146C60 File Offset: 0x00144E60
		// (set) Token: 0x0600455F RID: 17759 RVA: 0x00146C68 File Offset: 0x00144E68
		public Type ContextType
		{
			get
			{
				return this._contextType;
			}
			set
			{
				Check.NotNull<Type>(value, "value");
				if (!typeof(DbContext).IsAssignableFrom(value))
				{
					throw new ArgumentException(Strings.DbMigrationsConfiguration_ContextType(value.Name));
				}
				this._contextType = value;
				DbConfigurationManager.Instance.EnsureLoadedForContext(this._contextType);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x00146CBB File Offset: 0x00144EBB
		// (set) Token: 0x06004561 RID: 17761 RVA: 0x00146CC3 File Offset: 0x00144EC3
		public string MigrationsNamespace { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x00146CCC File Offset: 0x00144ECC
		// (set) Token: 0x06004563 RID: 17763 RVA: 0x00146CD4 File Offset: 0x00144ED4
		public string MigrationsDirectory
		{
			get
			{
				return this._migrationsDirectory;
			}
			set
			{
				Check.NotEmpty(value, "value");
				if (Path.IsPathRooted(value))
				{
					throw new MigrationsException(Strings.DbMigrationsConfiguration_RootedPath(value));
				}
				this._migrationsDirectory = value;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x00146CFD File Offset: 0x00144EFD
		// (set) Token: 0x06004565 RID: 17765 RVA: 0x00146D05 File Offset: 0x00144F05
		public MigrationCodeGenerator CodeGenerator
		{
			get
			{
				return this._codeGenerator;
			}
			set
			{
				Check.NotNull<MigrationCodeGenerator>(value, "value");
				this._codeGenerator = value;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x00146D1A File Offset: 0x00144F1A
		// (set) Token: 0x06004567 RID: 17767 RVA: 0x00146D22 File Offset: 0x00144F22
		public Assembly MigrationsAssembly
		{
			get
			{
				return this._migrationsAssembly;
			}
			set
			{
				Check.NotNull<Assembly>(value, "value");
				this._migrationsAssembly = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00146D37 File Offset: 0x00144F37
		// (set) Token: 0x06004569 RID: 17769 RVA: 0x00146D3F File Offset: 0x00144F3F
		public DbConnectionInfo TargetDatabase
		{
			get
			{
				return this._connectionInfo;
			}
			set
			{
				Check.NotNull<DbConnectionInfo>(value, "value");
				this._connectionInfo = value;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x00146D54 File Offset: 0x00144F54
		// (set) Token: 0x0600456B RID: 17771 RVA: 0x00146D5C File Offset: 0x00144F5C
		public int? CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				if (value != null && value < 0)
				{
					throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout);
				}
				this._commandTimeout = value;
			}
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00146D9C File Offset: 0x00144F9C
		internal virtual void OnSeed(DbContext context)
		{
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x00146D9E File Offset: 0x00144F9E
		// (set) Token: 0x0600456E RID: 17774 RVA: 0x00146DA6 File Offset: 0x00144FA6
		internal EdmModelDiffer ModelDiffer
		{
			get
			{
				return this._modelDiffer;
			}
			set
			{
				this._modelDiffer = value;
			}
		}

		// Token: 0x0400196C RID: 6508
		public const string DefaultMigrationsDirectory = "Migrations";

		// Token: 0x0400196D RID: 6509
		private readonly Dictionary<string, MigrationSqlGenerator> _sqlGenerators = new Dictionary<string, MigrationSqlGenerator>();

		// Token: 0x0400196E RID: 6510
		private readonly Dictionary<string, Func<DbConnection, string, HistoryContext>> _historyContextFactories = new Dictionary<string, Func<DbConnection, string, HistoryContext>>();

		// Token: 0x0400196F RID: 6511
		private MigrationCodeGenerator _codeGenerator;

		// Token: 0x04001970 RID: 6512
		private Type _contextType;

		// Token: 0x04001971 RID: 6513
		private Assembly _migrationsAssembly;

		// Token: 0x04001972 RID: 6514
		private EdmModelDiffer _modelDiffer = new EdmModelDiffer();

		// Token: 0x04001973 RID: 6515
		private DbConnectionInfo _connectionInfo;

		// Token: 0x04001974 RID: 6516
		private string _migrationsDirectory = "Migrations";

		// Token: 0x04001975 RID: 6517
		private readonly Lazy<IDbDependencyResolver> _resolver;

		// Token: 0x04001976 RID: 6518
		private string _contextKey;

		// Token: 0x04001977 RID: 6519
		private int? _commandTimeout;
	}
}
