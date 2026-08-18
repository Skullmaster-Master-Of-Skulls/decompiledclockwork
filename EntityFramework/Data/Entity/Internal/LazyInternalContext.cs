using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200078A RID: 1930
	internal class LazyInternalContext : InternalContext
	{
		// Token: 0x06005753 RID: 22355 RVA: 0x001789B4 File Offset: 0x00176BB4
		public LazyInternalContext(DbContext owner, IInternalConnection internalConnection, DbCompiledModel model, Func<DbContext, IDbModelCacheKey> cacheKeyFactory = null, AttributeProvider attributeProvider = null, Lazy<DbDispatchers> dispatchers = null, ObjectContext objectContext = null) : base(owner, dispatchers)
		{
			this._internalConnection = internalConnection;
			this._model = model;
			this._cacheKeyFactory = (cacheKeyFactory ?? new Func<DbContext, IDbModelCacheKey>(new DefaultModelCacheKeyFactory().Create));
			this._attributeProvider = (attributeProvider ?? new AttributeProvider());
			this._objectContext = objectContext;
			this._createdWithExistingModel = (model != null);
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x00178A2F File Offset: 0x00176C2F
		public override ObjectContext ObjectContext
		{
			get
			{
				base.Initialize();
				return this.ObjectContextInUse;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x00178A3D File Offset: 0x00176C3D
		public override DbCompiledModel CodeFirstModel
		{
			get
			{
				this.InitializeContext();
				return this._model;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005756 RID: 22358 RVA: 0x00178A4B File Offset: 0x00176C4B
		public override DbModel ModelBeingInitialized
		{
			get
			{
				this.InitializeContext();
				return this._modelBeingInitialized;
			}
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x00178A59 File Offset: 0x00176C59
		public override ObjectContext GetObjectContextWithoutDatabaseInitialization()
		{
			this.InitializeContext();
			return this.ObjectContextInUse;
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005758 RID: 22360 RVA: 0x00178A67 File Offset: 0x00176C67
		public virtual ObjectContext ObjectContextInUse
		{
			get
			{
				return base.TempObjectContext ?? this._objectContext;
			}
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x00178A79 File Offset: 0x00176C79
		public override int SaveChanges()
		{
			if (this.ObjectContextInUse != null)
			{
				return base.SaveChanges();
			}
			return 0;
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x00178A8B File Offset: 0x00176C8B
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.ObjectContextInUse != null)
			{
				return base.SaveChangesAsync(cancellationToken);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00178AAA File Offset: 0x00176CAA
		public override void DisposeContext(bool disposing)
		{
			if (!base.IsDisposed)
			{
				base.DisposeContext(disposing);
				if (disposing)
				{
					if (this._objectContext != null)
					{
						this._objectContext.Dispose();
					}
					this._internalConnection.Dispose();
				}
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600575C RID: 22364 RVA: 0x00178ADC File Offset: 0x00176CDC
		public override DbConnection Connection
		{
			get
			{
				base.CheckContextNotDisposed();
				if (base.TempObjectContext != null)
				{
					return ((EntityConnection)base.TempObjectContext.Connection).StoreConnection;
				}
				return this._internalConnection.Connection;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600575D RID: 22365 RVA: 0x00178B0D File Offset: 0x00176D0D
		public override string OriginalConnectionString
		{
			get
			{
				return this._internalConnection.OriginalConnectionString;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600575E RID: 22366 RVA: 0x00178B1A File Offset: 0x00176D1A
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._internalConnection.ConnectionStringOrigin;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600575F RID: 22367 RVA: 0x00178B2D File Offset: 0x00176D2D
		// (set) Token: 0x06005760 RID: 22368 RVA: 0x00178B35 File Offset: 0x00176D35
		public override AppConfig AppConfig
		{
			get
			{
				return base.AppConfig;
			}
			set
			{
				base.AppConfig = value;
				this._internalConnection.AppConfig = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06005761 RID: 22369 RVA: 0x00178B4A File Offset: 0x00176D4A
		public override string ConnectionStringName
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._internalConnection.ConnectionStringName;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005762 RID: 22370 RVA: 0x00178B5D File Offset: 0x00176D5D
		// (set) Token: 0x06005763 RID: 22371 RVA: 0x00178B6B File Offset: 0x00176D6B
		public override DbProviderInfo ModelProviderInfo
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._modelProviderInfo;
			}
			set
			{
				base.CheckContextNotDisposed();
				this._modelProviderInfo = value;
				this._internalConnection.ProviderName = this._modelProviderInfo.ProviderInvariantName;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005764 RID: 22372 RVA: 0x00178B90 File Offset: 0x00176D90
		public override string ProviderName
		{
			get
			{
				return this._internalConnection.ProviderName;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x00178B9D File Offset: 0x00176D9D
		// (set) Token: 0x06005766 RID: 22374 RVA: 0x00178BAB File Offset: 0x00176DAB
		public override Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._onModelCreating;
			}
			set
			{
				base.CheckContextNotDisposed();
				this._onModelCreating = value;
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x00178BBC File Offset: 0x00176DBC
		public override void OverrideConnection(IInternalConnection connection)
		{
			connection.AppConfig = this.AppConfig;
			if (connection.ConnectionHasModel != this._internalConnection.ConnectionHasModel)
			{
				throw this._internalConnection.ConnectionHasModel ? Error.LazyInternalContext_CannotReplaceEfConnectionWithDbConnection() : Error.LazyInternalContext_CannotReplaceDbConnectionWithEfConnection();
			}
			this._internalConnection.Dispose();
			this._internalConnection = connection;
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00178C28 File Offset: 0x00176E28
		protected override void InitializeContext()
		{
			base.CheckContextNotDisposed();
			if (this._objectContext == null)
			{
				if (this._creatingModel)
				{
					throw Error.DbContext_ContextUsedInModelCreating();
				}
				try
				{
					DbContextInfo currentInfo = DbContextInfo.CurrentInfo;
					if (currentInfo != null)
					{
						base.ApplyContextInfo(currentInfo);
					}
					this._creatingModel = true;
					if (this._createdWithExistingModel)
					{
						if (this._internalConnection.ConnectionHasModel)
						{
							throw Error.DbContext_ConnectionHasModel();
						}
						this._objectContext = this._model.CreateObjectContext<ObjectContext>(this._internalConnection.Connection);
					}
					else if (this._internalConnection.ConnectionHasModel)
					{
						this._objectContext = this._internalConnection.CreateObjectContextFromConnectionModel();
					}
					else
					{
						IDbModelCacheKey key = this._cacheKeyFactory(base.Owner);
						DbCompiledModel value = LazyInternalContext._cachedModels.GetOrAdd(key, (IDbModelCacheKey t) => new RetryLazy<LazyInternalContext, DbCompiledModel>(new Func<LazyInternalContext, DbCompiledModel>(LazyInternalContext.CreateModel))).GetValue(this);
						this._objectContext = value.CreateObjectContext<ObjectContext>(this._internalConnection.Connection);
						this._model = value;
					}
					this._objectContext.ContextOptions.EnsureTransactionsForFunctionsAndCommands = this._initialEnsureTransactionsForFunctionsAndCommands;
					this._objectContext.ContextOptions.LazyLoadingEnabled = this._initialLazyLoadingFlag;
					this._objectContext.ContextOptions.ProxyCreationEnabled = this._initialProxyCreationFlag;
					this._objectContext.ContextOptions.UseCSharpNullComparisonBehavior = !this._useDatabaseNullSemanticsFlag;
					this._objectContext.CommandTimeout = this._commandTimeout;
					this._objectContext.ContextOptions.UseConsistentNullReferenceBehavior = true;
					this._objectContext.InterceptionContext = this._objectContext.InterceptionContext.WithDbContext(base.Owner);
					base.ResetDbSets();
					this._objectContext.InitializeMappingViewCacheFactory(base.Owner);
				}
				finally
				{
					this._creatingModel = false;
				}
			}
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x00178E00 File Offset: 0x00177000
		public static DbCompiledModel CreateModel(LazyInternalContext internalContext)
		{
			DbModelBuilder dbModelBuilder = internalContext.CreateModelBuilder();
			DbModel dbModel = (internalContext._modelProviderInfo == null) ? dbModelBuilder.Build(internalContext._internalConnection.Connection) : dbModelBuilder.Build(internalContext._modelProviderInfo);
			internalContext._modelBeingInitialized = dbModel;
			return dbModel.Compile();
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x00178E4C File Offset: 0x0017704C
		public DbModelBuilder CreateModelBuilder()
		{
			DbModelBuilderVersionAttribute dbModelBuilderVersionAttribute = this._attributeProvider.GetAttributes(base.Owner.GetType()).OfType<DbModelBuilderVersionAttribute>().FirstOrDefault<DbModelBuilderVersionAttribute>();
			DbModelBuilderVersion modelBuilderVersion = (dbModelBuilderVersionAttribute != null) ? dbModelBuilderVersionAttribute.Version : DbModelBuilderVersion.Latest;
			DbModelBuilder dbModelBuilder = new DbModelBuilder(modelBuilderVersion);
			string text = LazyInternalContext.StripInvalidCharacters(base.Owner.GetType().Namespace);
			if (!string.IsNullOrWhiteSpace(text))
			{
				dbModelBuilder.Conventions.Add(new IConvention[]
				{
					new ModelNamespaceConvention(text)
				});
			}
			string text2 = LazyInternalContext.StripInvalidCharacters(base.Owner.GetType().Name);
			if (!string.IsNullOrWhiteSpace(text2))
			{
				dbModelBuilder.Conventions.Add(new IConvention[]
				{
					new ModelContainerConvention(text2)
				});
			}
			new DbSetDiscoveryService(base.Owner).RegisterSets(dbModelBuilder);
			base.Owner.CallOnModelCreating(dbModelBuilder);
			if (this.OnModelCreating != null)
			{
				this.OnModelCreating(dbModelBuilder);
			}
			return dbModelBuilder;
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x00178F40 File Offset: 0x00177140
		private static string StripInvalidCharacters(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(value.Length);
			bool flag = true;
			foreach (char c in value)
			{
				if (c == '.')
				{
					if (!flag)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					switch (char.GetUnicodeCategory(c))
					{
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.TitlecaseLetter:
					case UnicodeCategory.ModifierLetter:
					case UnicodeCategory.OtherLetter:
					case UnicodeCategory.LetterNumber:
						flag = false;
						stringBuilder.Append(c);
						break;
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.DecimalDigitNumber:
					case UnicodeCategory.ConnectorPunctuation:
						if (!flag)
						{
							stringBuilder.Append(c);
						}
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x0017901C File Offset: 0x0017721C
		public override void MarkDatabaseNotInitialized()
		{
			if (!base.InInitializationAction)
			{
				RetryAction<InternalContext> retryAction;
				LazyInternalContext.InitializedDatabases.TryRemove(Tuple.Create<DbCompiledModel, string>(this._model, this._internalConnection.ConnectionKey), out retryAction);
			}
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00179056 File Offset: 0x00177256
		public override void MarkDatabaseInitialized()
		{
			this.InitializeContext();
			this.InitializeDatabaseAction(delegate(InternalContext c)
			{
			});
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00179089 File Offset: 0x00177289
		protected override void InitializeDatabase()
		{
			this.InitializeDatabaseAction(delegate(InternalContext c)
			{
				c.PerformDatabaseInitialization();
			});
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x001790C4 File Offset: 0x001772C4
		private void InitializeDatabaseAction(Action<InternalContext> action)
		{
			if (!this._inDatabaseInitialization && !base.InitializerDisabled)
			{
				try
				{
					this._inDatabaseInitialization = true;
					LazyInternalContext.InitializedDatabases.GetOrAdd(Tuple.Create<DbCompiledModel, string>(this._model, this._internalConnection.ConnectionKey), (Tuple<DbCompiledModel, string> t) => new RetryAction<InternalContext>(action)).PerformAction(this);
				}
				finally
				{
					this._inDatabaseInitialization = false;
					this._modelBeingInitialized = null;
				}
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005770 RID: 22384 RVA: 0x00179150 File Offset: 0x00177350
		public override IDatabaseInitializer<DbContext> DefaultInitializer
		{
			get
			{
				if (this._model == null)
				{
					return null;
				}
				return LazyInternalContext._defaultCodeFirstInitializer;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x00179164 File Offset: 0x00177364
		// (set) Token: 0x06005772 RID: 22386 RVA: 0x00179190 File Offset: 0x00177390
		public override bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialEnsureTransactionsForFunctionsAndCommands;
				}
				return objectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands = value;
					return;
				}
				this._initialEnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x001791BC File Offset: 0x001773BC
		// (set) Token: 0x06005774 RID: 22388 RVA: 0x001791E8 File Offset: 0x001773E8
		public override bool LazyLoadingEnabled
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialLazyLoadingFlag;
				}
				return objectContextInUse.ContextOptions.LazyLoadingEnabled;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.LazyLoadingEnabled = value;
					return;
				}
				this._initialLazyLoadingFlag = value;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005775 RID: 22389 RVA: 0x00179214 File Offset: 0x00177414
		// (set) Token: 0x06005776 RID: 22390 RVA: 0x00179240 File Offset: 0x00177440
		public override bool ProxyCreationEnabled
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialProxyCreationFlag;
				}
				return objectContextInUse.ContextOptions.ProxyCreationEnabled;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.ProxyCreationEnabled = value;
					return;
				}
				this._initialProxyCreationFlag = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x0017926C File Offset: 0x0017746C
		// (set) Token: 0x06005778 RID: 22392 RVA: 0x00179298 File Offset: 0x00177498
		public override bool UseDatabaseNullSemantics
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._useDatabaseNullSemanticsFlag;
				}
				return !objectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior = !value;
					return;
				}
				this._useDatabaseNullSemanticsFlag = value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x001792C8 File Offset: 0x001774C8
		// (set) Token: 0x0600577A RID: 22394 RVA: 0x001792EC File Offset: 0x001774EC
		public override int? CommandTimeout
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._commandTimeout;
				}
				return objectContextInUse.CommandTimeout;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.CommandTimeout = value;
					return;
				}
				this._commandTimeout = value;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x00179312 File Offset: 0x00177512
		public override string DefaultSchema
		{
			get
			{
				return this.CodeFirstModel.DefaultSchema;
			}
		}

		// Token: 0x0400232E RID: 9006
		private static readonly CreateDatabaseIfNotExists<DbContext> _defaultCodeFirstInitializer = new CreateDatabaseIfNotExists<DbContext>();

		// Token: 0x0400232F RID: 9007
		private static readonly ConcurrentDictionary<IDbModelCacheKey, RetryLazy<LazyInternalContext, DbCompiledModel>> _cachedModels = new ConcurrentDictionary<IDbModelCacheKey, RetryLazy<LazyInternalContext, DbCompiledModel>>();

		// Token: 0x04002330 RID: 9008
		private static readonly ConcurrentDictionary<Tuple<DbCompiledModel, string>, RetryAction<InternalContext>> InitializedDatabases = new ConcurrentDictionary<Tuple<DbCompiledModel, string>, RetryAction<InternalContext>>();

		// Token: 0x04002331 RID: 9009
		private IInternalConnection _internalConnection;

		// Token: 0x04002332 RID: 9010
		private bool _creatingModel;

		// Token: 0x04002333 RID: 9011
		private ObjectContext _objectContext;

		// Token: 0x04002334 RID: 9012
		private DbCompiledModel _model;

		// Token: 0x04002335 RID: 9013
		private readonly bool _createdWithExistingModel;

		// Token: 0x04002336 RID: 9014
		private bool _initialEnsureTransactionsForFunctionsAndCommands = true;

		// Token: 0x04002337 RID: 9015
		private bool _initialLazyLoadingFlag = true;

		// Token: 0x04002338 RID: 9016
		private bool _initialProxyCreationFlag = true;

		// Token: 0x04002339 RID: 9017
		private bool _useDatabaseNullSemanticsFlag;

		// Token: 0x0400233A RID: 9018
		private int? _commandTimeout;

		// Token: 0x0400233B RID: 9019
		private bool _inDatabaseInitialization;

		// Token: 0x0400233C RID: 9020
		private Action<DbModelBuilder> _onModelCreating;

		// Token: 0x0400233D RID: 9021
		private readonly Func<DbContext, IDbModelCacheKey> _cacheKeyFactory;

		// Token: 0x0400233E RID: 9022
		private readonly AttributeProvider _attributeProvider;

		// Token: 0x0400233F RID: 9023
		private DbModel _modelBeingInitialized;

		// Token: 0x04002340 RID: 9024
		private DbProviderInfo _modelProviderInfo;
	}
}
