using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006D6 RID: 1750
	public class ToolingFacade : IDisposable
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x0014C5EF File Offset: 0x0014A7EF
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x0014C5F7 File Offset: 0x0014A7F7
		public Action<string> LogInfoDelegate { get; set; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x0014C600 File Offset: 0x0014A800
		// (set) Token: 0x0600463D RID: 17981 RVA: 0x0014C608 File Offset: 0x0014A808
		public Action<string> LogWarningDelegate { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x0014C611 File Offset: 0x0014A811
		// (set) Token: 0x0600463F RID: 17983 RVA: 0x0014C619 File Offset: 0x0014A819
		public Action<string> LogVerboseDelegate { get; set; }

		// Token: 0x06004640 RID: 17984 RVA: 0x0014C624 File Offset: 0x0014A824
		[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
		public ToolingFacade(string migrationsAssemblyName, string contextAssemblyName, string configurationTypeName, string workingDirectory, string configurationFilePath, string dataDirectory, DbConnectionInfo connectionStringInfo)
		{
			Check.NotEmpty(migrationsAssemblyName, "migrationsAssemblyName");
			this._migrationsAssemblyName = migrationsAssemblyName;
			this._contextAssemblyName = contextAssemblyName;
			this._configurationTypeName = configurationTypeName;
			this._connectionStringInfo = connectionStringInfo;
			AppDomainSetup appDomainSetup = new AppDomainSetup
			{
				ShadowCopyFiles = "true"
			};
			if (!string.IsNullOrWhiteSpace(workingDirectory))
			{
				appDomainSetup.ApplicationBase = workingDirectory;
			}
			this._configurationFile = new ConfigurationFileUpdater().Update(configurationFilePath);
			appDomainSetup.ConfigurationFile = this._configurationFile;
			string friendlyName = "MigrationsToolingFacade" + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			this._appDomain = AppDomain.CreateDomain(friendlyName, null, appDomainSetup);
			if (!string.IsNullOrWhiteSpace(dataDirectory))
			{
				this._appDomain.SetData("DataDirectory", dataDirectory);
			}
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x0014C6E8 File Offset: 0x0014A8E8
		internal ToolingFacade()
		{
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x0014C6F0 File Offset: 0x0014A8F0
		~ToolingFacade()
		{
			this.Dispose(false);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0014C720 File Offset: 0x0014A920
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IEnumerable<string> GetContextTypes()
		{
			ToolingFacade.GetContextTypesRunner runner = new ToolingFacade.GetContextTypesRunner();
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x0014C758 File Offset: 0x0014A958
		public string GetContextType(string contextTypeName)
		{
			ToolingFacade.GetContextTypeRunner runner = new ToolingFacade.GetContextTypeRunner
			{
				ContextTypeName = contextTypeName
			};
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (string)this._appDomain.GetData("result");
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x0014C798 File Offset: 0x0014A998
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual IEnumerable<string> GetDatabaseMigrations()
		{
			ToolingFacade.GetDatabaseMigrationsRunner runner = new ToolingFacade.GetDatabaseMigrationsRunner();
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0014C7D0 File Offset: 0x0014A9D0
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual IEnumerable<string> GetPendingMigrations()
		{
			ToolingFacade.GetPendingMigrationsRunner runner = new ToolingFacade.GetPendingMigrationsRunner();
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x0014C808 File Offset: 0x0014AA08
		public void Update(string targetMigration, bool force)
		{
			ToolingFacade.UpdateRunner runner = new ToolingFacade.UpdateRunner
			{
				TargetMigration = targetMigration,
				Force = force
			};
			this.ConfigureRunner(runner);
			this.Run(runner);
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0014C83C File Offset: 0x0014AA3C
		public string ScriptUpdate(string sourceMigration, string targetMigration, bool force)
		{
			ToolingFacade.ScriptUpdateRunner runner = new ToolingFacade.ScriptUpdateRunner
			{
				SourceMigration = sourceMigration,
				TargetMigration = targetMigration,
				Force = force
			};
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (string)this._appDomain.GetData("result");
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x0014C88C File Offset: 0x0014AA8C
		public virtual ScaffoldedMigration Scaffold(string migrationName, string language, string rootNamespace, bool ignoreChanges)
		{
			ToolingFacade.ScaffoldRunner runner = new ToolingFacade.ScaffoldRunner
			{
				MigrationName = migrationName,
				Language = language,
				RootNamespace = rootNamespace,
				IgnoreChanges = ignoreChanges
			};
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (ScaffoldedMigration)this._appDomain.GetData("result");
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x0014C8E4 File Offset: 0x0014AAE4
		public ScaffoldedMigration ScaffoldInitialCreate(string language, string rootNamespace)
		{
			ToolingFacade.InitialCreateScaffoldRunner runner = new ToolingFacade.InitialCreateScaffoldRunner
			{
				Language = language,
				RootNamespace = rootNamespace
			};
			this.ConfigureRunner(runner);
			this.Run(runner);
			return (ScaffoldedMigration)this._appDomain.GetData("result");
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0014C92A File Offset: 0x0014AB2A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0014C939 File Offset: 0x0014AB39
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._appDomain != null)
			{
				AppDomain.Unload(this._appDomain);
				this._appDomain = null;
			}
			if (this._configurationFile != null)
			{
				File.Delete(this._configurationFile);
			}
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0014C96B File Offset: 0x0014AB6B
		private void ConfigureRunner(ToolingFacade.BaseRunner runner)
		{
			runner.MigrationsAssemblyName = this._migrationsAssemblyName;
			runner.ContextAssemblyName = this._contextAssemblyName;
			runner.ConfigurationTypeName = this._configurationTypeName;
			runner.ConnectionStringInfo = this._connectionStringInfo;
			runner.Log = new ToolingFacade.ToolLogger(this);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0014C9A9 File Offset: 0x0014ABA9
		private void Run(ToolingFacade.BaseRunner runner)
		{
			this._appDomain.DoCallBack(new CrossAppDomainDelegate(runner.Run));
		}

		// Token: 0x040019C0 RID: 6592
		private readonly string _migrationsAssemblyName;

		// Token: 0x040019C1 RID: 6593
		private readonly string _contextAssemblyName;

		// Token: 0x040019C2 RID: 6594
		private readonly string _configurationTypeName;

		// Token: 0x040019C3 RID: 6595
		private readonly string _configurationFile;

		// Token: 0x040019C4 RID: 6596
		private readonly DbConnectionInfo _connectionStringInfo;

		// Token: 0x040019C5 RID: 6597
		private AppDomain _appDomain;

		// Token: 0x020006D8 RID: 1752
		private class ToolLogger : MigrationsLogger
		{
			// Token: 0x06004653 RID: 18003 RVA: 0x0014C9CB File Offset: 0x0014ABCB
			public ToolLogger(ToolingFacade facade)
			{
				this._facade = facade;
			}

			// Token: 0x06004654 RID: 18004 RVA: 0x0014C9DA File Offset: 0x0014ABDA
			public override void Info(string message)
			{
				if (this._facade.LogInfoDelegate != null)
				{
					this._facade.LogInfoDelegate(message);
				}
			}

			// Token: 0x06004655 RID: 18005 RVA: 0x0014C9FA File Offset: 0x0014ABFA
			public override void Warning(string message)
			{
				if (this._facade.LogWarningDelegate != null)
				{
					this._facade.LogWarningDelegate(message);
				}
			}

			// Token: 0x06004656 RID: 18006 RVA: 0x0014CA1A File Offset: 0x0014AC1A
			public override void Verbose(string sql)
			{
				if (this._facade.LogVerboseDelegate != null)
				{
					this._facade.LogVerboseDelegate(sql);
				}
			}

			// Token: 0x040019C9 RID: 6601
			private readonly ToolingFacade _facade;
		}

		// Token: 0x020006D9 RID: 1753
		[Serializable]
		private abstract class BaseRunner
		{
			// Token: 0x17000A8A RID: 2698
			// (get) Token: 0x06004657 RID: 18007 RVA: 0x0014CA3A File Offset: 0x0014AC3A
			// (set) Token: 0x06004658 RID: 18008 RVA: 0x0014CA42 File Offset: 0x0014AC42
			public string MigrationsAssemblyName { get; set; }

			// Token: 0x17000A8B RID: 2699
			// (get) Token: 0x06004659 RID: 18009 RVA: 0x0014CA4B File Offset: 0x0014AC4B
			// (set) Token: 0x0600465A RID: 18010 RVA: 0x0014CA53 File Offset: 0x0014AC53
			public string ContextAssemblyName { get; set; }

			// Token: 0x17000A8C RID: 2700
			// (get) Token: 0x0600465B RID: 18011 RVA: 0x0014CA5C File Offset: 0x0014AC5C
			// (set) Token: 0x0600465C RID: 18012 RVA: 0x0014CA64 File Offset: 0x0014AC64
			public string ConfigurationTypeName { get; set; }

			// Token: 0x17000A8D RID: 2701
			// (get) Token: 0x0600465D RID: 18013 RVA: 0x0014CA6D File Offset: 0x0014AC6D
			// (set) Token: 0x0600465E RID: 18014 RVA: 0x0014CA75 File Offset: 0x0014AC75
			public DbConnectionInfo ConnectionStringInfo { get; set; }

			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x0600465F RID: 18015 RVA: 0x0014CA7E File Offset: 0x0014AC7E
			// (set) Token: 0x06004660 RID: 18016 RVA: 0x0014CA86 File Offset: 0x0014AC86
			public ToolingFacade.ToolLogger Log { get; set; }

			// Token: 0x06004661 RID: 18017
			public abstract void Run();

			// Token: 0x06004662 RID: 18018 RVA: 0x0014CA8F File Offset: 0x0014AC8F
			protected MigratorBase GetMigrator()
			{
				return this.DecorateMigrator(new DbMigrator(this.GetConfiguration()));
			}

			// Token: 0x06004663 RID: 18019 RVA: 0x0014CAA4 File Offset: 0x0014ACA4
			protected DbMigrationsConfiguration GetConfiguration()
			{
				DbMigrationsConfiguration dbMigrationsConfiguration = this.FindConfiguration();
				this.OverrideConfiguration(dbMigrationsConfiguration);
				return dbMigrationsConfiguration;
			}

			// Token: 0x06004664 RID: 18020 RVA: 0x0014CAC0 File Offset: 0x0014ACC0
			protected virtual void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				if (this.ConnectionStringInfo != null)
				{
					configuration.TargetDatabase = this.ConnectionStringInfo;
				}
			}

			// Token: 0x06004665 RID: 18021 RVA: 0x0014CAD6 File Offset: 0x0014ACD6
			private MigratorBase DecorateMigrator(DbMigrator migrator)
			{
				return new MigratorLoggingDecorator(migrator, this.Log);
			}

			// Token: 0x06004666 RID: 18022 RVA: 0x0014CAEC File Offset: 0x0014ACEC
			private DbMigrationsConfiguration FindConfiguration()
			{
				return new MigrationsConfigurationFinder(new TypeFinder(this.LoadMigrationsAssembly())).FindMigrationsConfiguration(null, this.ConfigurationTypeName, new Func<string, Exception>(Error.AssemblyMigrator_NoConfiguration), (string assembly, IEnumerable<Type> types) => Error.AssemblyMigrator_MultipleConfigurations(assembly), new Func<string, string, Exception>(Error.AssemblyMigrator_NoConfigurationWithName), new Func<string, string, Exception>(Error.AssemblyMigrator_MultipleConfigurationsWithName));
			}

			// Token: 0x06004667 RID: 18023 RVA: 0x0014CB56 File Offset: 0x0014AD56
			protected Assembly LoadMigrationsAssembly()
			{
				return ToolingFacade.BaseRunner.LoadAssembly(this.MigrationsAssemblyName);
			}

			// Token: 0x06004668 RID: 18024 RVA: 0x0014CB63 File Offset: 0x0014AD63
			protected Assembly LoadContextAssembly()
			{
				return ToolingFacade.BaseRunner.LoadAssembly(this.ContextAssemblyName);
			}

			// Token: 0x06004669 RID: 18025 RVA: 0x0014CB70 File Offset: 0x0014AD70
			private static Assembly LoadAssembly(string name)
			{
				Assembly result;
				try
				{
					result = Assembly.Load(name);
				}
				catch (FileNotFoundException ex)
				{
					throw new MigrationsException(Strings.ToolingFacade_AssemblyNotFound(ex.FileName), ex);
				}
				return result;
			}
		}

		// Token: 0x020006DA RID: 1754
		[Serializable]
		private class GetDatabaseMigrationsRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x0600466C RID: 18028 RVA: 0x0014CBB4 File Offset: 0x0014ADB4
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				IEnumerable<string> databaseMigrations = base.GetMigrator().GetDatabaseMigrations();
				AppDomain.CurrentDomain.SetData("result", databaseMigrations);
			}
		}

		// Token: 0x020006DB RID: 1755
		[Serializable]
		private class GetPendingMigrationsRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x0600466E RID: 18030 RVA: 0x0014CBE8 File Offset: 0x0014ADE8
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				IEnumerable<string> pendingMigrations = base.GetMigrator().GetPendingMigrations();
				AppDomain.CurrentDomain.SetData("result", pendingMigrations);
			}
		}

		// Token: 0x020006DC RID: 1756
		[Serializable]
		private class UpdateRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17000A8F RID: 2703
			// (get) Token: 0x06004670 RID: 18032 RVA: 0x0014CC19 File Offset: 0x0014AE19
			// (set) Token: 0x06004671 RID: 18033 RVA: 0x0014CC21 File Offset: 0x0014AE21
			public string TargetMigration { get; set; }

			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x06004672 RID: 18034 RVA: 0x0014CC2A File Offset: 0x0014AE2A
			// (set) Token: 0x06004673 RID: 18035 RVA: 0x0014CC32 File Offset: 0x0014AE32
			public bool Force { get; set; }

			// Token: 0x06004674 RID: 18036 RVA: 0x0014CC3B File Offset: 0x0014AE3B
			public override void Run()
			{
				base.GetMigrator().Update(this.TargetMigration);
			}

			// Token: 0x06004675 RID: 18037 RVA: 0x0014CC4E File Offset: 0x0014AE4E
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Force)
				{
					configuration.AutomaticMigrationDataLossAllowed = true;
				}
			}
		}

		// Token: 0x020006DD RID: 1757
		[Serializable]
		private class ScriptUpdateRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17000A91 RID: 2705
			// (get) Token: 0x06004677 RID: 18039 RVA: 0x0014CC6E File Offset: 0x0014AE6E
			// (set) Token: 0x06004678 RID: 18040 RVA: 0x0014CC76 File Offset: 0x0014AE76
			public string SourceMigration { get; set; }

			// Token: 0x17000A92 RID: 2706
			// (get) Token: 0x06004679 RID: 18041 RVA: 0x0014CC7F File Offset: 0x0014AE7F
			// (set) Token: 0x0600467A RID: 18042 RVA: 0x0014CC87 File Offset: 0x0014AE87
			public string TargetMigration { get; set; }

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x0600467B RID: 18043 RVA: 0x0014CC90 File Offset: 0x0014AE90
			// (set) Token: 0x0600467C RID: 18044 RVA: 0x0014CC98 File Offset: 0x0014AE98
			public bool Force { get; set; }

			// Token: 0x0600467D RID: 18045 RVA: 0x0014CCA4 File Offset: 0x0014AEA4
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				MigratorBase migrator = base.GetMigrator();
				string data = new MigratorScriptingDecorator(migrator).ScriptUpdate(this.SourceMigration, this.TargetMigration);
				AppDomain.CurrentDomain.SetData("result", data);
			}

			// Token: 0x0600467E RID: 18046 RVA: 0x0014CCE0 File Offset: 0x0014AEE0
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Force)
				{
					configuration.AutomaticMigrationDataLossAllowed = true;
				}
			}
		}

		// Token: 0x020006DE RID: 1758
		[Serializable]
		private class ScaffoldRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17000A94 RID: 2708
			// (get) Token: 0x06004680 RID: 18048 RVA: 0x0014CD00 File Offset: 0x0014AF00
			// (set) Token: 0x06004681 RID: 18049 RVA: 0x0014CD08 File Offset: 0x0014AF08
			public string MigrationName { get; set; }

			// Token: 0x17000A95 RID: 2709
			// (get) Token: 0x06004682 RID: 18050 RVA: 0x0014CD11 File Offset: 0x0014AF11
			// (set) Token: 0x06004683 RID: 18051 RVA: 0x0014CD19 File Offset: 0x0014AF19
			public string Language { get; set; }

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x06004684 RID: 18052 RVA: 0x0014CD22 File Offset: 0x0014AF22
			// (set) Token: 0x06004685 RID: 18053 RVA: 0x0014CD2A File Offset: 0x0014AF2A
			public string RootNamespace { get; set; }

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x06004686 RID: 18054 RVA: 0x0014CD33 File Offset: 0x0014AF33
			// (set) Token: 0x06004687 RID: 18055 RVA: 0x0014CD3B File Offset: 0x0014AF3B
			public bool IgnoreChanges { get; set; }

			// Token: 0x06004688 RID: 18056 RVA: 0x0014CD44 File Offset: 0x0014AF44
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				DbMigrationsConfiguration configuration = base.GetConfiguration();
				MigrationScaffolder migrationScaffolder = new MigrationScaffolder(configuration);
				string text = configuration.MigrationsNamespace;
				if (this.Language == "vb" && !string.IsNullOrWhiteSpace(this.RootNamespace))
				{
					if (this.RootNamespace.EqualsIgnoreCase(text))
					{
						text = null;
					}
					else
					{
						if (text == null || !text.StartsWith(this.RootNamespace + ".", StringComparison.OrdinalIgnoreCase))
						{
							throw Error.MigrationsNamespaceNotUnderRootNamespace(text, this.RootNamespace);
						}
						text = text.Substring(this.RootNamespace.Length + 1);
					}
				}
				migrationScaffolder.Namespace = text;
				ScaffoldedMigration data = this.Scaffold(migrationScaffolder);
				AppDomain.CurrentDomain.SetData("result", data);
			}

			// Token: 0x06004689 RID: 18057 RVA: 0x0014CDF5 File Offset: 0x0014AFF5
			protected virtual ScaffoldedMigration Scaffold(MigrationScaffolder scaffolder)
			{
				return scaffolder.Scaffold(this.MigrationName, this.IgnoreChanges);
			}

			// Token: 0x0600468A RID: 18058 RVA: 0x0014CE09 File Offset: 0x0014B009
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Language == "vb" && configuration.CodeGenerator is CSharpMigrationCodeGenerator)
				{
					configuration.CodeGenerator = new VisualBasicMigrationCodeGenerator();
				}
			}
		}

		// Token: 0x020006DF RID: 1759
		[Serializable]
		private class InitialCreateScaffoldRunner : ToolingFacade.ScaffoldRunner
		{
			// Token: 0x0600468C RID: 18060 RVA: 0x0014CE44 File Offset: 0x0014B044
			protected override ScaffoldedMigration Scaffold(MigrationScaffolder scaffolder)
			{
				return scaffolder.ScaffoldInitialCreate();
			}
		}

		// Token: 0x020006E0 RID: 1760
		[Serializable]
		private class GetContextTypesRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x0600468E RID: 18062 RVA: 0x0014CE80 File Offset: 0x0014B080
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				Assembly assembly = base.LoadContextAssembly();
				List<string> data = (from t in assembly.GetAccessibleTypes()
				where !t.IsAbstract && !t.IsGenericType && typeof(DbContext).IsAssignableFrom(t)
				select t.FullName).ToList<string>();
				AppDomain.CurrentDomain.SetData("result", data);
			}
		}

		// Token: 0x020006E1 RID: 1761
		[Serializable]
		private class GetContextTypeRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x06004692 RID: 18066 RVA: 0x0014CEFC File Offset: 0x0014B0FC
			// (set) Token: 0x06004693 RID: 18067 RVA: 0x0014CF04 File Offset: 0x0014B104
			public string ContextTypeName { get; set; }

			// Token: 0x06004694 RID: 18068 RVA: 0x0014CFD4 File Offset: 0x0014B1D4
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
			public override void Run()
			{
				Type type = new TypeFinder(base.LoadContextAssembly()).FindType(typeof(DbContext), this.ContextTypeName, (IEnumerable<Type> types) => from t in types
				where !typeof(HistoryContext).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType
				select t, new Func<string, Exception>(Error.EnableMigrations_NoContext), delegate(string assembly, IEnumerable<Type> types)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Strings.EnableMigrations_MultipleContexts(assembly));
					foreach (Type type2 in types)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append(Strings.EnableMigrationsForContext(type2.FullName));
					}
					return new MigrationsException(stringBuilder.ToString());
				}, new Func<string, string, Exception>(Error.EnableMigrations_NoContextWithName), new Func<string, string, Exception>(Error.EnableMigrations_MultipleContextsWithName));
				AppDomain.CurrentDomain.SetData("result", type.FullName);
			}
		}
	}
}
