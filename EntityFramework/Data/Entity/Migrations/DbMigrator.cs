using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Resources;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020006D0 RID: 1744
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class DbMigrator : MigratorBase
	{
		// Token: 0x0600458F RID: 17807 RVA: 0x00146FAC File Offset: 0x001451AC
		internal DbMigrator(DbContext usersContext = null, DbProviderFactory providerFactory = null, MigrationAssembly migrationAssembly = null) : base(null)
		{
			this._usersContext = usersContext;
			this._providerFactory = providerFactory;
			this._migrationAssembly = migrationAssembly;
			this._usersContextInfo = new DbContextInfo(typeof(DbContext));
			this._configuration = new DbMigrationsConfiguration();
			this._calledByCreateDatabase = true;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00146FFC File Offset: 0x001451FC
		public DbMigrator(DbMigrationsConfiguration configuration) : this(configuration, null, DatabaseExistenceState.Unknown, false)
		{
			Check.NotNull<DbMigrationsConfiguration>(configuration, "configuration");
			Check.NotNull<Type>(configuration.ContextType, "configuration.ContextType");
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x00147025 File Offset: 0x00145225
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal DbMigrator(DbMigrationsConfiguration configuration, DbContext usersContext) : this(configuration, usersContext, DatabaseExistenceState.Unknown, false)
		{
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x00147078 File Offset: 0x00145278
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal DbMigrator(DbMigrationsConfiguration configuration, DbContext usersContext, DatabaseExistenceState existenceState, bool calledByCreateDatabase) : base(null)
		{
			Check.NotNull<DbMigrationsConfiguration>(configuration, "configuration");
			Check.NotNull<Type>(configuration.ContextType, "configuration.ContextType");
			this._configuration = configuration;
			this._calledByCreateDatabase = calledByCreateDatabase;
			this._existenceState = existenceState;
			if (usersContext != null)
			{
				this._usersContextInfo = new DbContextInfo(usersContext, null);
			}
			else
			{
				this._usersContextInfo = ((configuration.TargetDatabase == null) ? new DbContextInfo(configuration.ContextType) : new DbContextInfo(configuration.ContextType, configuration.TargetDatabase));
				if (!this._usersContextInfo.IsConstructible)
				{
					throw Error.ContextNotConstructible(configuration.ContextType);
				}
			}
			this._modelDiffer = this._configuration.ModelDiffer;
			DbContext dbContext = usersContext ?? this._usersContextInfo.CreateInstance();
			this._usersContext = dbContext;
			try
			{
				this._migrationAssembly = new MigrationAssembly(this._configuration.MigrationsAssembly, this._configuration.MigrationsNamespace);
				this._currentModel = dbContext.GetModel();
				DbConnection connection = dbContext.Database.Connection;
				this._providerFactory = DbProviderServices.GetProviderFactory(connection);
				this._defaultSchema = (dbContext.InternalContext.DefaultSchema ?? "dbo");
				this._historyContextFactory = this._configuration.GetHistoryContextFactory(this._usersContextInfo.ConnectionProviderName);
				this._historyRepository = new HistoryRepository(dbContext.InternalContext, this._usersContextInfo.ConnectionString, this._providerFactory, this._configuration.ContextKey, this._configuration.CommandTimeout, this._historyContextFactory, new string[]
				{
					this._defaultSchema
				}.Concat(this.GetHistorySchemas()), this._usersContext, this._existenceState);
				this._providerManifestToken = ((dbContext.InternalContext.ModelProviderInfo != null) ? dbContext.InternalContext.ModelProviderInfo.ProviderManifestToken : DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(connection));
				DbModelBuilder modelBuilder = dbContext.InternalContext.CodeFirstModel.CachedModelBuilder;
				this._modificationCommandTreeGenerator = new Lazy<ModificationCommandTreeGenerator>(() => new ModificationCommandTreeGenerator(modelBuilder.BuildDynamicUpdateModel(new DbProviderInfo(this._usersContextInfo.ConnectionProviderName, this._providerManifestToken)), this.CreateConnection()));
				DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
				dbInterceptionContext = dbInterceptionContext.WithDbContext(this._usersContext);
				this._targetDatabase = Strings.LoggingTargetDatabaseFormat(DbInterception.Dispatch.Connection.GetDataSource(connection, dbInterceptionContext), DbInterception.Dispatch.Connection.GetDatabase(connection, dbInterceptionContext), this._usersContextInfo.ConnectionProviderName, (this._usersContextInfo.ConnectionStringOrigin == DbConnectionStringOrigin.DbContextInfo) ? Strings.LoggingExplicit : this._usersContextInfo.ConnectionStringOrigin.ToString());
				this._legacyContextKey = dbContext.InternalContext.DefaultContextKey;
				this._emptyModel = this.GetEmptyModel();
			}
			finally
			{
				if (usersContext == null)
				{
					this._usersContext = null;
					dbContext.Dispose();
				}
			}
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x0014737B File Offset: 0x0014557B
		private Lazy<XDocument> GetEmptyModel()
		{
			return new Lazy<XDocument>(() => new DbModelBuilder().Build(new DbProviderInfo(this._usersContextInfo.ConnectionProviderName, this._providerManifestToken)).GetModel());
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00147390 File Offset: 0x00145590
		private XDocument GetHistoryModel(string defaultSchema)
		{
			DbConnection dbConnection = null;
			XDocument model;
			try
			{
				dbConnection = this.CreateConnection();
				using (HistoryContext historyContext = this._historyContextFactory(dbConnection, defaultSchema))
				{
					model = historyContext.GetModel();
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(dbConnection, new DbInterceptionContext());
				}
			}
			return model;
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0014753B File Offset: 0x0014573B
		private IEnumerable<string> GetHistorySchemas()
		{
			return from migrationId in this._migrationAssembly.MigrationIds
			let migration = this._migrationAssembly.GetMigration(migrationId)
			select DbMigrator.GetDefaultSchema(migration);
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x0014757B File Offset: 0x0014577B
		public override DbMigrationsConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x00147583 File Offset: 0x00145783
		internal override string TargetDatabase
		{
			get
			{
				return this._targetDatabase;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x0014758C File Offset: 0x0014578C
		private MigrationSqlGenerator SqlGenerator
		{
			get
			{
				MigrationSqlGenerator result;
				if ((result = this._sqlGenerator) == null)
				{
					result = (this._sqlGenerator = this._configuration.GetSqlGenerator(this._usersContextInfo.ConnectionProviderName));
				}
				return result;
			}
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x001475C2 File Offset: 0x001457C2
		public override IEnumerable<string> GetLocalMigrations()
		{
			return this._migrationAssembly.MigrationIds;
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x001475CF File Offset: 0x001457CF
		public override IEnumerable<string> GetDatabaseMigrations()
		{
			return this._historyRepository.GetMigrationsSince("0");
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x001475E1 File Offset: 0x001457E1
		public override IEnumerable<string> GetPendingMigrations()
		{
			return this._historyRepository.GetPendingMigrations(this._migrationAssembly.MigrationIds);
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x001475FC File Offset: 0x001457FC
		internal ScaffoldedMigration ScaffoldInitialCreate(string @namespace)
		{
			string migrationId;
			string text;
			XDocument lastModel = this._historyRepository.GetLastModel(out migrationId, out text, this._legacyContextKey);
			if (lastModel == null || !migrationId.MigrationName().Equals(Strings.InitialCreate))
			{
				return null;
			}
			List<MigrationOperation> operations = this._modelDiffer.Diff(this._emptyModel.Value, lastModel, this._modificationCommandTreeGenerator, this.SqlGenerator, null, null).ToList<MigrationOperation>();
			ScaffoldedMigration scaffoldedMigration = this._configuration.CodeGenerator.Generate(migrationId, operations, null, Convert.ToBase64String(new ModelCompressor().Compress(this._currentModel)), @namespace, Strings.InitialCreate);
			scaffoldedMigration.MigrationId = migrationId;
			scaffoldedMigration.Directory = this._configuration.MigrationsDirectory;
			scaffoldedMigration.Resources.Add("DefaultSchema", this._defaultSchema);
			return scaffoldedMigration;
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001476E0 File Offset: 0x001458E0
		internal ScaffoldedMigration Scaffold(string migrationName, string @namespace, bool ignoreChanges)
		{
			DbMigrator.<>c__DisplayClass9 CS$<>8__locals1 = new DbMigrator.<>c__DisplayClass9();
			CS$<>8__locals1.<>4__this = this;
			string migrationId = null;
			bool flag = false;
			List<string> list = this.GetPendingMigrations().ToList<string>();
			if (list.Any<string>())
			{
				string text = list.Last<string>();
				if (!text.EqualsIgnoreCase(migrationName) && !text.MigrationName().EqualsIgnoreCase(migrationName))
				{
					throw Error.MigrationsPendingException(list.Join(null, ", "));
				}
				flag = true;
				migrationId = text;
				migrationName = text.MigrationName();
			}
			CS$<>8__locals1.sourceModel = null;
			this.CheckLegacyCompatibility(delegate
			{
				CS$<>8__locals1.sourceModel = CS$<>8__locals1.<>4__this._currentModel;
			});
			string migrationId2 = null;
			string sourceModelVersion = null;
			DbMigrator.<>c__DisplayClass9 CS$<>8__locals2 = CS$<>8__locals1;
			XDocument sourceModel;
			if ((sourceModel = CS$<>8__locals1.sourceModel) == null)
			{
				sourceModel = (this._historyRepository.GetLastModel(out migrationId2, out sourceModelVersion, null) ?? this._emptyModel.Value);
			}
			CS$<>8__locals2.sourceModel = sourceModel;
			IEnumerable<MigrationOperation> operations = ignoreChanges ? Enumerable.Empty<MigrationOperation>() : this._modelDiffer.Diff(CS$<>8__locals1.sourceModel, this._currentModel, this._modificationCommandTreeGenerator, this.SqlGenerator, sourceModelVersion, null).ToList<MigrationOperation>();
			if (!flag)
			{
				migrationName = this._migrationAssembly.UniquifyName(migrationName);
				migrationId = MigrationAssembly.CreateMigrationId(migrationName);
			}
			ModelCompressor modelCompressor = new ModelCompressor();
			ScaffoldedMigration scaffoldedMigration = this._configuration.CodeGenerator.Generate(migrationId, operations, (CS$<>8__locals1.sourceModel == this._emptyModel.Value || CS$<>8__locals1.sourceModel == this._currentModel || !migrationId2.IsAutomaticMigration()) ? null : Convert.ToBase64String(modelCompressor.Compress(CS$<>8__locals1.sourceModel)), Convert.ToBase64String(modelCompressor.Compress(this._currentModel)), @namespace, migrationName);
			scaffoldedMigration.MigrationId = migrationId;
			scaffoldedMigration.Directory = this._configuration.MigrationsDirectory;
			scaffoldedMigration.IsRescaffold = flag;
			scaffoldedMigration.Resources.Add("DefaultSchema", this._defaultSchema);
			return scaffoldedMigration;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x001478A0 File Offset: 0x00145AA0
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private void CheckLegacyCompatibility(Action onCompatible)
		{
			if (!this._calledByCreateDatabase && !this._historyRepository.Exists(null))
			{
				DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
				try
				{
					bool flag;
					try
					{
						flag = dbContext.Database.CompatibleWithModel(true);
					}
					catch
					{
						return;
					}
					if (!flag)
					{
						throw Error.MetadataOutOfDate();
					}
					onCompatible();
				}
				finally
				{
					if (this._usersContext == null)
					{
						dbContext.Dispose();
					}
				}
			}
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x00147944 File Offset: 0x00145B44
		public override void Update(string targetMigration)
		{
			base.EnsureDatabaseExists(delegate
			{
				this.UpdateInternal(targetMigration);
			});
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00147A08 File Offset: 0x00145C08
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private void UpdateInternal(string targetMigration)
		{
			IEnumerable<MigrationOperation> upgradeOperations = this._historyRepository.GetUpgradeOperations();
			if (upgradeOperations.Any<MigrationOperation>())
			{
				base.UpgradeHistory(upgradeOperations);
			}
			IEnumerable<string> enumerable = this.GetPendingMigrations();
			if (!enumerable.Any<string>())
			{
				this.CheckLegacyCompatibility(delegate
				{
					this.ExecuteOperations(MigrationAssembly.CreateBootstrapMigrationId(), new VersionedModel(this._currentModel, null), Enumerable.Empty<MigrationOperation>(), this._modelDiffer.Diff(this._emptyModel.Value, this.GetHistoryModel(this._defaultSchema), this._modificationCommandTreeGenerator, this.SqlGenerator, null, null), false, false);
				});
			}
			string targetMigrationId = targetMigration;
			if (!string.IsNullOrWhiteSpace(targetMigrationId))
			{
				if (!targetMigrationId.IsValidMigrationId())
				{
					if (targetMigrationId == Strings.AutomaticMigration)
					{
						throw Error.AutoNotValidTarget(Strings.AutomaticMigration);
					}
					targetMigrationId = this.GetMigrationId(targetMigration);
				}
				if (enumerable.Any((string m) => m.EqualsIgnoreCase(targetMigrationId)))
				{
					enumerable = from m in enumerable
					where string.CompareOrdinal(m.ToLowerInvariant(), targetMigrationId.ToLowerInvariant()) <= 0
					select m;
				}
				else
				{
					enumerable = this._historyRepository.GetMigrationsSince(targetMigrationId);
					if (enumerable.Any<string>())
					{
						base.Downgrade(enumerable.Concat(new string[]
						{
							targetMigrationId
						}));
						return;
					}
				}
			}
			base.Upgrade(enumerable, targetMigrationId, null);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00147B44 File Offset: 0x00145D44
		internal override void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			IEnumerable<MigrationStatement> migrationStatements = this.SqlGenerator.Generate(upgradeOperations, this._providerManifestToken);
			base.ExecuteStatements(migrationStatements);
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00147B88 File Offset: 0x00145D88
		internal override string GetMigrationId(string migration)
		{
			if (migration.IsValidMigrationId())
			{
				return migration;
			}
			string text = this.GetPendingMigrations().SingleOrDefault((string m) => m.MigrationName().EqualsIgnoreCase(migration)) ?? this._historyRepository.GetMigrationId(migration);
			if (text == null)
			{
				throw Error.MigrationNotFound(migration);
			}
			return text;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x00147BF4 File Offset: 0x00145DF4
		internal override void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			DbMigration lastMigration = null;
			if (lastMigrationId != null)
			{
				lastMigration = this._migrationAssembly.GetMigration(lastMigrationId);
			}
			foreach (string text in pendingMigrations)
			{
				DbMigration migration = this._migrationAssembly.GetMigration(text);
				base.ApplyMigration(migration, lastMigration);
				lastMigration = migration;
				this._emptyMigrationNeeded = false;
				if (text.EqualsIgnoreCase(targetMigrationId))
				{
					break;
				}
			}
			if (string.IsNullOrWhiteSpace(targetMigrationId) && ((this._emptyMigrationNeeded && this._configuration.AutomaticMigrationsEnabled) || this.IsModelOutOfDate(this._currentModel, lastMigration)))
			{
				if (!this._configuration.AutomaticMigrationsEnabled)
				{
					throw Error.AutomaticDisabledException();
				}
				base.AutoMigrate(MigrationAssembly.CreateMigrationId(this._calledByCreateDatabase ? Strings.InitialCreate : Strings.AutomaticMigration), this._calledByCreateDatabase ? new VersionedModel(this._emptyModel.Value, null) : this.GetLastModel(lastMigration, null), new VersionedModel(this._currentModel, null), false);
			}
			if (!this._calledByCreateDatabase && !this.IsModelOutOfDate(this._currentModel, lastMigration))
			{
				base.SeedDatabase();
			}
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00147D1C File Offset: 0x00145F1C
		internal override void SeedDatabase()
		{
			DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
			if (this._usersContext != null)
			{
				dbContext.InternalContext.UseTempObjectContext();
			}
			try
			{
				this._configuration.OnSeed(dbContext);
				dbContext.SaveChanges();
			}
			finally
			{
				if (this._usersContext == null)
				{
					dbContext.Dispose();
				}
				else
				{
					dbContext.InternalContext.DisposeTempObjectContext();
				}
			}
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00147D94 File Offset: 0x00145F94
		internal virtual bool IsModelOutOfDate(XDocument model, DbMigration lastMigration)
		{
			VersionedModel lastModel = this.GetLastModel(lastMigration, null);
			return this._modelDiffer.Diff(lastModel.Model, model, null, null, lastModel.Version, null).Any<MigrationOperation>();
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00147DCC File Offset: 0x00145FCC
		private VersionedModel GetLastModel(DbMigration lastMigration, string currentMigrationId = null)
		{
			if (lastMigration != null)
			{
				return lastMigration.GetTargetModel();
			}
			string strA;
			string version;
			XDocument lastModel = this._historyRepository.GetLastModel(out strA, out version, null);
			if (lastModel != null && (currentMigrationId == null || string.CompareOrdinal(strA, currentMigrationId) < 0))
			{
				return new VersionedModel(lastModel, version);
			}
			return new VersionedModel(this._emptyModel.Value, null);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00147E20 File Offset: 0x00146020
		internal override void Downgrade(IEnumerable<string> pendingMigrations)
		{
			for (int i = 0; i < pendingMigrations.Count<string>() - 1; i++)
			{
				string migrationId = pendingMigrations.ElementAt(i);
				DbMigration migration = this._migrationAssembly.GetMigration(migrationId);
				string text = pendingMigrations.ElementAt(i + 1);
				string version = null;
				XDocument xdocument = (text != "0") ? this._historyRepository.GetModel(text, out version) : this._emptyModel.Value;
				string text2;
				XDocument model = this._historyRepository.GetModel(migrationId, out text2);
				if (migration == null)
				{
					base.AutoMigrate(migrationId, new VersionedModel(model, null), new VersionedModel(xdocument, version), true);
				}
				else
				{
					base.RevertMigration(migrationId, migration, xdocument);
				}
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00147ECC File Offset: 0x001460CC
		internal override void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			IEnumerable<MigrationOperation> systemOperations = Enumerable.Empty<MigrationOperation>();
			string defaultSchema = DbMigrator.GetDefaultSchema(migration);
			XDocument historyModel = this.GetHistoryModel(defaultSchema);
			if (object.ReferenceEquals(targetModel, this._emptyModel.Value) && !this._historyRepository.IsShared())
			{
				systemOperations = this._modelDiffer.Diff(historyModel, this._emptyModel.Value, null, null, null, null);
			}
			else
			{
				string lastDefaultSchema = this.GetLastDefaultSchema(migrationId);
				if (!string.Equals(lastDefaultSchema, defaultSchema, StringComparison.Ordinal))
				{
					XDocument historyModel2 = this.GetHistoryModel(lastDefaultSchema);
					systemOperations = this._modelDiffer.Diff(historyModel, historyModel2, null, null, null, null);
				}
			}
			migration.Down();
			this.ExecuteOperations(migrationId, new VersionedModel(targetModel, null), migration.Operations, systemOperations, true, false);
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00147F78 File Offset: 0x00146178
		internal override void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			IMigrationMetadata migrationMetadata = (IMigrationMetadata)migration;
			VersionedModel versionedModel = this.GetLastModel(lastMigration, migrationMetadata.Id);
			VersionedModel sourceModel = migration.GetSourceModel();
			VersionedModel targetModel = migration.GetTargetModel();
			if (sourceModel != null && this.IsModelOutOfDate(sourceModel.Model, lastMigration))
			{
				base.AutoMigrate(migrationMetadata.Id.ToAutomaticMigrationId(), versionedModel, sourceModel, false);
				versionedModel = sourceModel;
			}
			string defaultSchema = DbMigrator.GetDefaultSchema(migration);
			XDocument historyModel = this.GetHistoryModel(defaultSchema);
			IEnumerable<MigrationOperation> systemOperations = Enumerable.Empty<MigrationOperation>();
			if (object.ReferenceEquals(versionedModel.Model, this._emptyModel.Value) && !base.HistoryExists())
			{
				systemOperations = this._modelDiffer.Diff(this._emptyModel.Value, historyModel, null, null, null, null);
			}
			else
			{
				string lastDefaultSchema = this.GetLastDefaultSchema(migrationMetadata.Id);
				if (!string.Equals(lastDefaultSchema, defaultSchema, StringComparison.Ordinal))
				{
					XDocument historyModel2 = this.GetHistoryModel(lastDefaultSchema);
					systemOperations = this._modelDiffer.Diff(historyModel2, historyModel, null, null, null, null);
				}
			}
			migration.Up();
			this.ExecuteOperations(migrationMetadata.Id, targetModel, migration.Operations, systemOperations, false, false);
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00148080 File Offset: 0x00146280
		private static string GetDefaultSchema(DbMigration migration)
		{
			string result;
			try
			{
				string @string = new ResourceManager(migration.GetType()).GetString("DefaultSchema");
				result = ((!string.IsNullOrWhiteSpace(@string)) ? @string : "dbo");
			}
			catch (MissingManifestResourceException)
			{
				result = "dbo";
			}
			return result;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x001480EC File Offset: 0x001462EC
		private string GetLastDefaultSchema(string migrationId)
		{
			string text = this._migrationAssembly.MigrationIds.LastOrDefault((string m) => string.CompareOrdinal(m, migrationId) < 0);
			if (text != null)
			{
				return DbMigrator.GetDefaultSchema(this._migrationAssembly.GetMigration(text));
			}
			return "dbo";
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0014813D File Offset: 0x0014633D
		internal override bool HistoryExists()
		{
			return this._historyRepository.Exists(null);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00148160 File Offset: 0x00146360
		internal override void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			IEnumerable<MigrationOperation> systemOperations = Enumerable.Empty<MigrationOperation>();
			if (!this._historyRepository.IsShared())
			{
				if (object.ReferenceEquals(targetModel.Model, this._emptyModel.Value))
				{
					systemOperations = this._modelDiffer.Diff(this.GetHistoryModel("dbo"), this._emptyModel.Value, null, null, null, null);
				}
				else if (object.ReferenceEquals(sourceModel.Model, this._emptyModel.Value))
				{
					systemOperations = this._modelDiffer.Diff(this._emptyModel.Value, this._calledByCreateDatabase ? this.GetHistoryModel(this._defaultSchema) : this.GetHistoryModel("dbo"), null, null, null, null);
				}
			}
			List<MigrationOperation> list = this._modelDiffer.Diff(sourceModel.Model, targetModel.Model, (targetModel.Model == this._currentModel) ? this._modificationCommandTreeGenerator : null, this.SqlGenerator, sourceModel.Version, targetModel.Version).ToList<MigrationOperation>();
			if (!this._calledByCreateDatabase && object.ReferenceEquals(targetModel.Model, this._currentModel))
			{
				string lastDefaultSchema = this.GetLastDefaultSchema(migrationId);
				if (!string.Equals(lastDefaultSchema, this._defaultSchema, StringComparison.Ordinal))
				{
					throw Error.UnableToMoveHistoryTableWithAuto();
				}
			}
			if (!this._configuration.AutomaticMigrationDataLossAllowed)
			{
				if (list.Any((MigrationOperation o) => o.IsDestructiveChange))
				{
					throw Error.AutomaticDataLoss();
				}
			}
			if (targetModel.Model != this._currentModel)
			{
				if (list.Any((MigrationOperation o) => o is ProcedureOperation))
				{
					throw Error.AutomaticStaleFunctions(migrationId);
				}
			}
			this.ExecuteOperations(migrationId, targetModel, list, systemOperations, downgrading, true);
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x00148490 File Offset: 0x00146690
		private void ExecuteOperations(string migrationId, VersionedModel targetModel, IEnumerable<MigrationOperation> operations, IEnumerable<MigrationOperation> systemOperations, bool downgrading, bool auto = false)
		{
			DbMigrator.FillInForeignKeyOperations(operations, targetModel.Model);
			List<AddForeignKeyOperation> second = (from ct in operations.OfType<CreateTableOperation>()
			from afk in operations.OfType<AddForeignKeyOperation>()
			where ct.Name.EqualsIgnoreCase(afk.DependentTable)
			select afk).ToList<AddForeignKeyOperation>();
			List<MigrationOperation> list = operations.Except(second).Concat(second).Concat(systemOperations).ToList<MigrationOperation>();
			CreateTableOperation createTableOperation = systemOperations.OfType<CreateTableOperation>().FirstOrDefault<CreateTableOperation>();
			if (createTableOperation != null)
			{
				this._historyRepository.CurrentSchema = DatabaseName.Parse(createTableOperation.Name).Schema;
			}
			MoveTableOperation moveTableOperation = systemOperations.OfType<MoveTableOperation>().FirstOrDefault<MoveTableOperation>();
			if (moveTableOperation != null)
			{
				this._historyRepository.CurrentSchema = moveTableOperation.NewSchema;
				moveTableOperation.ContextKey = this._configuration.ContextKey;
				moveTableOperation.IsSystem = true;
			}
			if (!downgrading)
			{
				list.Add(this._historyRepository.CreateInsertOperation(migrationId, targetModel));
			}
			else if (!systemOperations.Any((MigrationOperation o) => o is DropTableOperation))
			{
				list.Add(this._historyRepository.CreateDeleteOperation(migrationId));
			}
			IEnumerable<MigrationStatement> enumerable = base.GenerateStatements(list, migrationId);
			if (auto)
			{
				enumerable = enumerable.Distinct((MigrationStatement m1, MigrationStatement m2) => string.Equals(m1.Sql, m2.Sql, StringComparison.Ordinal));
			}
			base.ExecuteStatements(enumerable);
			this._historyRepository.ResetExists();
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0014865F File Offset: 0x0014685F
		internal override IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			return this._historyRepository.CreateDiscoveryQueryTrees();
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x0014866C File Offset: 0x0014686C
		internal override IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			return this.SqlGenerator.Generate(operations, this._providerManifestToken);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00148680 File Offset: 0x00146880
		internal override void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			this.ExecuteStatements(migrationStatements, null);
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x001486AC File Offset: 0x001468AC
		internal void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements, DbTransaction existingTransaction)
		{
			DbConnection connection = null;
			try
			{
				if (existingTransaction != null)
				{
					DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
					dbInterceptionContext = dbInterceptionContext.WithDbContext(this._usersContext);
					this.ExecuteStatementsWithinTransaction(migrationStatements, existingTransaction, dbInterceptionContext);
				}
				else
				{
					connection = this.CreateConnection();
					DbProviderServices.GetExecutionStrategy(connection).Execute(delegate()
					{
						this.ExecuteStatementsInternal(migrationStatements, connection);
					});
				}
			}
			finally
			{
				if (connection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
				}
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00148760 File Offset: 0x00146960
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection)
		{
			DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
			DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
			dbInterceptionContext = dbInterceptionContext.WithDbContext(dbContext);
			TransactionHandler transactionHandler = null;
			try
			{
				if (DbInterception.Dispatch.Connection.GetState(connection, dbInterceptionContext) == ConnectionState.Broken)
				{
					DbInterception.Dispatch.Connection.Close(connection, dbInterceptionContext);
				}
				if (DbInterception.Dispatch.Connection.GetState(connection, dbInterceptionContext) == ConnectionState.Closed)
				{
					DbInterception.Dispatch.Connection.Open(connection, dbInterceptionContext);
				}
				if (!(dbContext is TransactionContext))
				{
					string name = DbConfiguration.DependencyResolver.GetService(DbProviderServices.GetProviderFactory(connection)).Name;
					string dataSource = DbInterception.Dispatch.Connection.GetDataSource(connection, dbInterceptionContext);
					Func<TransactionHandler> service = DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(name, dataSource));
					if (service != null)
					{
						transactionHandler = service();
						transactionHandler.Initialize(dbContext, connection);
					}
				}
				this.ExecuteStatementsInternal(migrationStatements, connection, dbInterceptionContext);
				this._committedStatements = true;
			}
			finally
			{
				if (transactionHandler != null)
				{
					transactionHandler.Dispose();
				}
				if (this._usersContext == null)
				{
					dbContext.Dispose();
				}
			}
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00148870 File Offset: 0x00146A70
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			foreach (MigrationStatement migrationStatement in migrationStatements)
			{
				base.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x001488CC File Offset: 0x00146ACC
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbInterceptionContext interceptionContext)
		{
			List<MigrationStatement> list = new List<MigrationStatement>();
			foreach (MigrationStatement migrationStatement in from s in migrationStatements
			where !string.IsNullOrEmpty(s.Sql)
			select s)
			{
				if (!migrationStatement.SuppressTransaction)
				{
					list.Add(migrationStatement);
				}
				else
				{
					if (list.Any<MigrationStatement>())
					{
						this.ExecuteStatementsWithinNewTransaction(list, connection, interceptionContext);
						list.Clear();
					}
					base.ExecuteSql(migrationStatement, connection, null, interceptionContext);
				}
			}
			if (list.Any<MigrationStatement>())
			{
				this.ExecuteStatementsWithinNewTransaction(list, connection, interceptionContext);
			}
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00148978 File Offset: 0x00146B78
		private void ExecuteStatementsWithinTransaction(IEnumerable<MigrationStatement> migrationStatements, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			DbConnection connection = DbInterception.Dispatch.Transaction.GetConnection(transaction, interceptionContext);
			this.ExecuteStatementsInternal(migrationStatements, connection, transaction, interceptionContext);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001489A4 File Offset: 0x00146BA4
		private void ExecuteStatementsWithinNewTransaction(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbInterceptionContext interceptionContext)
		{
			BeginTransactionInterceptionContext interceptionContext2 = new BeginTransactionInterceptionContext(interceptionContext).WithIsolationLevel(IsolationLevel.Serializable);
			DbTransaction dbTransaction = null;
			try
			{
				dbTransaction = DbInterception.Dispatch.Connection.BeginTransaction(connection, interceptionContext2);
				this.ExecuteStatementsWithinTransaction(migrationStatements, dbTransaction, interceptionContext);
				DbInterception.Dispatch.Transaction.Commit(dbTransaction, interceptionContext);
			}
			finally
			{
				if (dbTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Dispose(dbTransaction, interceptionContext);
				}
			}
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00148A18 File Offset: 0x00146C18
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		internal override void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			if (string.IsNullOrWhiteSpace(migrationStatement.Sql))
			{
				return;
			}
			DbCommand command = connection.CreateCommand();
			using (InterceptableDbCommand interceptableDbCommand = this.ConfigureCommand(command, migrationStatement.Sql, interceptionContext))
			{
				if (transaction != null)
				{
					interceptableDbCommand.Transaction = transaction;
				}
				interceptableDbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x00148A78 File Offset: 0x00146C78
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		private InterceptableDbCommand ConfigureCommand(DbCommand command, string commandText, DbInterceptionContext interceptionContext)
		{
			command.CommandText = commandText;
			if (this._configuration.CommandTimeout != null)
			{
				command.CommandTimeout = this._configuration.CommandTimeout.Value;
			}
			return new InterceptableDbCommand(command, interceptionContext, null);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00148B70 File Offset: 0x00146D70
		private static void FillInForeignKeyOperations(IEnumerable<MigrationOperation> operations, XDocument targetModel)
		{
			using (IEnumerator<AddForeignKeyOperation> enumerator = (from fk in operations.OfType<AddForeignKeyOperation>()
			where fk.PrincipalTable != null && !fk.PrincipalColumns.Any<string>()
			select fk).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DbMigrator.<>c__DisplayClass3f CS$<>8__locals1 = new DbMigrator.<>c__DisplayClass3f();
					CS$<>8__locals1.foreignKeyOperation = enumerator.Current;
					string principalTable = DbMigrator.GetStandardizedTableName(CS$<>8__locals1.foreignKeyOperation.PrincipalTable);
					string entitySetName = (from es in targetModel.Descendants(EdmXNames.Ssdl.EntitySetNames)
					where new DatabaseName(es.TableAttribute(), es.SchemaAttribute()).ToString().EqualsIgnoreCase(principalTable)
					select es.NameAttribute()).SingleOrDefault<string>();
					if (entitySetName != null)
					{
						XElement container = targetModel.Descendants(EdmXNames.Ssdl.EntityTypeNames).Single((XElement et) => et.NameAttribute().EqualsIgnoreCase(entitySetName));
						container.Descendants(EdmXNames.Ssdl.PropertyRefNames).Each(delegate(XElement pr)
						{
							CS$<>8__locals1.foreignKeyOperation.PrincipalColumns.Add(pr.NameAttribute());
						});
					}
					else
					{
						CreateTableOperation createTableOperation = operations.OfType<CreateTableOperation>().SingleOrDefault((CreateTableOperation ct) => DbMigrator.GetStandardizedTableName(ct.Name).EqualsIgnoreCase(principalTable));
						if (createTableOperation == null || createTableOperation.PrimaryKey == null)
						{
							throw Error.PartialFkOperation(CS$<>8__locals1.foreignKeyOperation.DependentTable, CS$<>8__locals1.foreignKeyOperation.DependentColumns.Join(null, ", "));
						}
						createTableOperation.PrimaryKey.Columns.Each(delegate(string c)
						{
							CS$<>8__locals1.foreignKeyOperation.PrincipalColumns.Add(c);
						});
					}
				}
			}
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00148D48 File Offset: 0x00146F48
		private static string GetStandardizedTableName(string tableName)
		{
			DatabaseName databaseName = DatabaseName.Parse(tableName);
			if (!string.IsNullOrWhiteSpace(databaseName.Schema))
			{
				return tableName;
			}
			return new DatabaseName(tableName, "dbo").ToString();
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x00148D7C File Offset: 0x00146F7C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal override void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			bool flag = false;
			System.Data.Entity.Migrations.Utilities.DatabaseCreator databaseCreator = new System.Data.Entity.Migrations.Utilities.DatabaseCreator(this._configuration.CommandTimeout);
			DbConnection dbConnection = null;
			try
			{
				dbConnection = this.CreateConnection();
				if (this._existenceState == DatabaseExistenceState.DoesNotExist || (this._existenceState == DatabaseExistenceState.Unknown && !databaseCreator.Exists(dbConnection)))
				{
					databaseCreator.Create(dbConnection);
					flag = true;
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(dbConnection, new DbInterceptionContext());
				}
			}
			this._emptyMigrationNeeded = flag;
			try
			{
				this._committedStatements = false;
				mustSucceedToKeepDatabase();
			}
			catch
			{
				if (flag && !this._committedStatements)
				{
					DbConnection dbConnection2 = null;
					try
					{
						dbConnection2 = this.CreateConnection();
						databaseCreator.Delete(dbConnection2);
					}
					catch
					{
					}
					finally
					{
						if (dbConnection2 != null)
						{
							DbInterception.Dispatch.Connection.Dispose(dbConnection2, new DbInterceptionContext());
						}
					}
				}
				throw;
			}
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00148E6C File Offset: 0x0014706C
		private DbConnection CreateConnection()
		{
			DbConnection dbConnection = this._providerFactory.CreateConnection();
			DbConnectionPropertyInterceptionContext<string> dbConnectionPropertyInterceptionContext = new DbConnectionPropertyInterceptionContext<string>().WithValue(this._usersContextInfo.ConnectionString);
			if (this._usersContext != null)
			{
				dbConnectionPropertyInterceptionContext = dbConnectionPropertyInterceptionContext.WithDbContext(this._usersContext);
			}
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, dbConnectionPropertyInterceptionContext);
			return dbConnection;
		}

		// Token: 0x0400197D RID: 6525
		public const string InitialDatabase = "0";

		// Token: 0x0400197E RID: 6526
		private const string DefaultSchemaResourceKey = "DefaultSchema";

		// Token: 0x0400197F RID: 6527
		private readonly Lazy<XDocument> _emptyModel;

		// Token: 0x04001980 RID: 6528
		private readonly DbMigrationsConfiguration _configuration;

		// Token: 0x04001981 RID: 6529
		private readonly XDocument _currentModel;

		// Token: 0x04001982 RID: 6530
		private readonly DbProviderFactory _providerFactory;

		// Token: 0x04001983 RID: 6531
		private readonly HistoryRepository _historyRepository;

		// Token: 0x04001984 RID: 6532
		private readonly MigrationAssembly _migrationAssembly;

		// Token: 0x04001985 RID: 6533
		private readonly DbContextInfo _usersContextInfo;

		// Token: 0x04001986 RID: 6534
		private readonly EdmModelDiffer _modelDiffer;

		// Token: 0x04001987 RID: 6535
		private readonly Lazy<ModificationCommandTreeGenerator> _modificationCommandTreeGenerator;

		// Token: 0x04001988 RID: 6536
		private readonly DbContext _usersContext;

		// Token: 0x04001989 RID: 6537
		private readonly Func<DbConnection, string, HistoryContext> _historyContextFactory;

		// Token: 0x0400198A RID: 6538
		private readonly bool _calledByCreateDatabase;

		// Token: 0x0400198B RID: 6539
		private readonly DatabaseExistenceState _existenceState;

		// Token: 0x0400198C RID: 6540
		private readonly string _providerManifestToken;

		// Token: 0x0400198D RID: 6541
		private readonly string _targetDatabase;

		// Token: 0x0400198E RID: 6542
		private readonly string _legacyContextKey;

		// Token: 0x0400198F RID: 6543
		private readonly string _defaultSchema;

		// Token: 0x04001990 RID: 6544
		private MigrationSqlGenerator _sqlGenerator;

		// Token: 0x04001991 RID: 6545
		private bool _emptyMigrationNeeded;

		// Token: 0x04001992 RID: 6546
		private bool _committedStatements;
	}
}
