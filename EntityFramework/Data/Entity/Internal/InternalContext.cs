using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Internal.MockingProxies;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076C RID: 1900
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal abstract class InternalContext : IDisposable
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060055AD RID: 21933 RVA: 0x00174860 File Offset: 0x00172A60
		// (remove) Token: 0x060055AE RID: 21934 RVA: 0x00174898 File Offset: 0x00172A98
		public event EventHandler<EventArgs> OnDisposing;

		// Token: 0x060055AF RID: 21935 RVA: 0x001748D4 File Offset: 0x00172AD4
		protected InternalContext(DbContext owner, Lazy<DbDispatchers> dispatchers = null)
		{
			this._owner = owner;
			Lazy<DbDispatchers> dispatchers2 = dispatchers;
			if (dispatchers == null)
			{
				dispatchers2 = new Lazy<DbDispatchers>(() => DbInterception.Dispatch);
			}
			this._dispatchers = dispatchers2;
			this.AutoDetectChangesEnabled = true;
			this.ValidateOnSaveEnabled = true;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00174960 File Offset: 0x00172B60
		protected InternalContext()
		{
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060055B1 RID: 21937 RVA: 0x0017499F File Offset: 0x00172B9F
		public DbContext Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060055B2 RID: 21938
		public abstract ObjectContext ObjectContext { get; }

		// Token: 0x060055B3 RID: 21939
		public abstract ObjectContext GetObjectContextWithoutDatabaseInitialization();

		// Token: 0x060055B4 RID: 21940 RVA: 0x001749A7 File Offset: 0x00172BA7
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual ClonedObjectContext CreateObjectContextForDdlOps()
		{
			this.InitializeContext();
			return new ClonedObjectContext(new ObjectContextProxy(this.GetObjectContextWithoutDatabaseInitialization()), this.Connection, this.OriginalConnectionString, false);
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060055B5 RID: 21941 RVA: 0x001749CC File Offset: 0x00172BCC
		protected ObjectContext TempObjectContext
		{
			get
			{
				return (this._tempObjectContext == null) ? null : this._tempObjectContext.ObjectContext;
			}
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x001749EC File Offset: 0x00172BEC
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual void UseTempObjectContext()
		{
			this._tempObjectContextCount++;
			if (this._tempObjectContext == null)
			{
				this._tempObjectContext = new ClonedObjectContext(new ObjectContextProxy(this.GetObjectContextWithoutDatabaseInitialization()), this.Connection, this.OriginalConnectionString, true);
				this.ResetDbSets();
			}
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x00174A38 File Offset: 0x00172C38
		public virtual void DisposeTempObjectContext()
		{
			if (this._tempObjectContextCount > 0 && --this._tempObjectContextCount == 0 && this._tempObjectContext != null)
			{
				this._tempObjectContext.Dispose();
				this._tempObjectContext = null;
				this.ResetDbSets();
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00174A81 File Offset: 0x00172C81
		public virtual DbCompiledModel CodeFirstModel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060055B9 RID: 21945 RVA: 0x00174A84 File Offset: 0x00172C84
		public virtual DbModel ModelBeingInitialized
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00174AA0 File Offset: 0x00172CA0
		public virtual void CreateDatabase(ObjectContext objectContext, DatabaseExistenceState existenceState)
		{
			new DatabaseCreator().CreateDatabase(this, (DbMigrationsConfiguration config, DbContext context) => new DbMigrator(config, context, existenceState, true), objectContext);
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00174AD2 File Offset: 0x00172CD2
		public virtual bool CompatibleWithModel(bool throwIfNoMetadata, DatabaseExistenceState existenceState)
		{
			return new ModelCompatibilityChecker().CompatibleWithModel(this, new ModelHashCalculator(), throwIfNoMetadata, existenceState);
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00174AE6 File Offset: 0x00172CE6
		public virtual bool ModelMatches(VersionedModel model)
		{
			return !new EdmModelDiffer().Diff(model.Model, this.Owner.GetModel(), null, null, model.Version, null).Any<MigrationOperation>();
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00174B1C File Offset: 0x00172D1C
		public virtual string QueryForModelHash()
		{
			EdmMetadataRepository edmMetadataRepository = new EdmMetadataRepository(this, this.OriginalConnectionString, this.ProviderFactory);
			return edmMetadataRepository.QueryForModelHash((DbConnection c) => new EdmMetadataContext(c));
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x00174B60 File Offset: 0x00172D60
		public virtual VersionedModel QueryForModel(DatabaseExistenceState existenceState)
		{
			string text;
			string version;
			XDocument lastModel = this.CreateHistoryRepository(existenceState).GetLastModel(out text, out version, null);
			if (lastModel == null)
			{
				return null;
			}
			return new VersionedModel(lastModel, version);
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x00174BAC File Offset: 0x00172DAC
		public virtual void SaveMetadataToDatabase()
		{
			if (this.CodeFirstModel != null)
			{
				this.PerformInitializationAction(delegate
				{
					this.CreateHistoryRepository(DatabaseExistenceState.Unknown).BootstrapUsingEFProviderDdl(new VersionedModel(this.Owner.GetModel(), null));
				});
			}
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00174BDA File Offset: 0x00172DDA
		public virtual bool HasHistoryTableEntry()
		{
			return this.CreateHistoryRepository(DatabaseExistenceState.Unknown).HasMigrations();
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00174BE8 File Offset: 0x00172DE8
		private HistoryRepository CreateHistoryRepository(DatabaseExistenceState existenceState = DatabaseExistenceState.Unknown)
		{
			this.DiscoverMigrationsConfiguration();
			return new HistoryRepository(this, this.OriginalConnectionString, this.ProviderFactory, this._migrationsConfiguration().ContextKey, this.CommandTimeout, this.HistoryContextFactory, (this.DefaultSchema != null) ? new string[]
			{
				this.DefaultSchema
			} : Enumerable.Empty<string>(), this.Owner, existenceState);
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00174C50 File Offset: 0x00172E50
		public virtual DbTransaction TryGetCurrentStoreTransaction()
		{
			EntityTransaction currentTransaction = ((EntityConnection)this.GetObjectContextWithoutDatabaseInitialization().Connection).CurrentTransaction;
			if (currentTransaction == null)
			{
				return null;
			}
			return currentTransaction.StoreTransaction;
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x00174C7E File Offset: 0x00172E7E
		// (set) Token: 0x060055C4 RID: 21956 RVA: 0x00174C86 File Offset: 0x00172E86
		protected bool InInitializationAction { get; set; }

		// Token: 0x060055C5 RID: 21957 RVA: 0x00174C90 File Offset: 0x00172E90
		public void PerformInitializationAction(Action action)
		{
			if (this.InInitializationAction)
			{
				action();
				return;
			}
			try
			{
				this.InInitializationAction = true;
				action();
			}
			catch (DataException innerException)
			{
				throw new DataException(Strings.Database_InitializationException, innerException);
			}
			finally
			{
				this.InInitializationAction = false;
			}
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00174CF0 File Offset: 0x00172EF0
		public virtual void RegisterObjectStateManagerChangedEvent(CollectionChangeEventHandler handler)
		{
			this.ObjectContext.ObjectStateManager.ObjectStateManagerChanged += handler;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x00174D04 File Offset: 0x00172F04
		public virtual bool EntityInContextAndNotDeleted(object entity)
		{
			ObjectStateEntry objectStateEntry;
			return this.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry) && objectStateEntry.State != EntityState.Deleted;
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x00174D34 File Offset: 0x00172F34
		public virtual int SaveChanges()
		{
			int result;
			try
			{
				if (this.ValidateOnSaveEnabled)
				{
					IEnumerable<DbEntityValidationResult> validationErrors = this.Owner.GetValidationErrors();
					if (validationErrors.Any<DbEntityValidationResult>())
					{
						throw new DbEntityValidationException(Strings.DbEntityValidationException_ValidationFailed, validationErrors);
					}
				}
				bool flag = this.AutoDetectChangesEnabled && !this.ValidateOnSaveEnabled;
				System.Data.Entity.Core.Objects.SaveOptions options = System.Data.Entity.Core.Objects.SaveOptions.AcceptAllChangesAfterSave | (flag ? System.Data.Entity.Core.Objects.SaveOptions.DetectChangesBeforeSave : System.Data.Entity.Core.Objects.SaveOptions.None);
				result = this.ObjectContext.SaveChanges(options);
			}
			catch (UpdateException updateException)
			{
				throw this.WrapUpdateException(updateException);
			}
			return result;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00174E54 File Offset: 0x00173054
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.ValidateOnSaveEnabled)
			{
				IEnumerable<DbEntityValidationResult> validationErrors = this.Owner.GetValidationErrors();
				if (validationErrors.Any<DbEntityValidationResult>())
				{
					throw new DbEntityValidationException(Strings.DbEntityValidationException_ValidationFailed, validationErrors);
				}
			}
			TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
			bool flag = this.AutoDetectChangesEnabled && !this.ValidateOnSaveEnabled;
			System.Data.Entity.Core.Objects.SaveOptions options = System.Data.Entity.Core.Objects.SaveOptions.AcceptAllChangesAfterSave | (flag ? System.Data.Entity.Core.Objects.SaveOptions.DetectChangesBeforeSave : System.Data.Entity.Core.Objects.SaveOptions.None);
			this.ObjectContext.SaveChangesAsync(options, cancellationToken).ContinueWith(delegate(Task<int> t)
			{
				if (t.IsFaulted)
				{
					IEnumerable<Exception> exceptions = t.Exception.InnerExceptions.Select(delegate(Exception ex)
					{
						UpdateException ex2 = ex as UpdateException;
						if (ex2 != null)
						{
							return this.WrapUpdateException(ex2);
						}
						return ex;
					});
					tcs.TrySetException(exceptions);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				tcs.TrySetResult(t.Result);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x00174EF6 File Offset: 0x001730F6
		public void Initialize()
		{
			this.InitializeContext();
			this.InitializeDatabase();
		}

		// Token: 0x060055CB RID: 21963
		protected abstract void InitializeContext();

		// Token: 0x060055CC RID: 21964
		public abstract void MarkDatabaseNotInitialized();

		// Token: 0x060055CD RID: 21965
		protected abstract void InitializeDatabase();

		// Token: 0x060055CE RID: 21966
		public abstract void MarkDatabaseInitialized();

		// Token: 0x060055CF RID: 21967 RVA: 0x00174F04 File Offset: 0x00173104
		public void PerformDatabaseInitialization()
		{
			object obj;
			if ((obj = DbConfiguration.DependencyResolver.GetService(typeof(IDatabaseInitializer<>).MakeGenericType(new Type[]
			{
				this.Owner.GetType()
			}))) == null)
			{
				obj = (this.DefaultInitializer ?? new NullDatabaseInitializer<DbContext>());
			}
			object obj2 = obj;
			Action action = (Action)InternalContext.CreateInitializationActionMethod.MakeGenericMethod(new Type[]
			{
				this.Owner.GetType()
			}).Invoke(this, new object[]
			{
				obj2
			});
			bool autoDetectChangesEnabled = this.AutoDetectChangesEnabled;
			bool validateOnSaveEnabled = this.ValidateOnSaveEnabled;
			try
			{
				if (!(this.Owner is TransactionContext))
				{
					this.UseTempObjectContext();
				}
				this.PerformInitializationAction(action);
			}
			finally
			{
				if (!(this.Owner is TransactionContext))
				{
					this.DisposeTempObjectContext();
				}
				this.AutoDetectChangesEnabled = autoDetectChangesEnabled;
				this.ValidateOnSaveEnabled = validateOnSaveEnabled;
			}
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0017501C File Offset: 0x0017321C
		private Action CreateInitializationAction<TContext>(IDatabaseInitializer<TContext> initializer) where TContext : DbContext
		{
			return delegate()
			{
				initializer.InitializeDatabase((TContext)((object)this.Owner));
			};
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060055D1 RID: 21969
		public abstract IDatabaseInitializer<DbContext> DefaultInitializer { get; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060055D2 RID: 21970
		// (set) Token: 0x060055D3 RID: 21971
		public abstract bool EnsureTransactionsForFunctionsAndCommands { get; set; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060055D4 RID: 21972
		// (set) Token: 0x060055D5 RID: 21973
		public abstract bool LazyLoadingEnabled { get; set; }

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060055D6 RID: 21974
		// (set) Token: 0x060055D7 RID: 21975
		public abstract bool ProxyCreationEnabled { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060055D8 RID: 21976
		// (set) Token: 0x060055D9 RID: 21977
		public abstract bool UseDatabaseNullSemantics { get; set; }

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060055DA RID: 21978
		// (set) Token: 0x060055DB RID: 21979
		public abstract int? CommandTimeout { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00175049 File Offset: 0x00173249
		// (set) Token: 0x060055DD RID: 21981 RVA: 0x00175051 File Offset: 0x00173251
		public bool AutoDetectChangesEnabled { get; set; }

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x0017505A File Offset: 0x0017325A
		// (set) Token: 0x060055DF RID: 21983 RVA: 0x00175062 File Offset: 0x00173262
		public bool ValidateOnSaveEnabled { get; set; }

		// Token: 0x060055E0 RID: 21984 RVA: 0x0017506C File Offset: 0x0017326C
		~InternalContext()
		{
			this.DisposeContext(false);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0017509C File Offset: 0x0017329C
		public void Dispose()
		{
			this.DisposeContext(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x001750AC File Offset: 0x001732AC
		public virtual void DisposeContext(bool disposing)
		{
			if (!this.IsDisposed)
			{
				if (disposing && this.OnDisposing != null)
				{
					this.OnDisposing(this, new EventArgs());
					this.OnDisposing = null;
				}
				if (this._tempObjectContext != null)
				{
					this._tempObjectContext.Dispose();
				}
				this.Log = null;
				this.IsDisposed = true;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x00175105 File Offset: 0x00173305
		// (set) Token: 0x060055E4 RID: 21988 RVA: 0x0017510D File Offset: 0x0017330D
		public bool IsDisposed { get; private set; }

		// Token: 0x060055E5 RID: 21989 RVA: 0x00175116 File Offset: 0x00173316
		public virtual void DetectChanges(bool force = false)
		{
			if (this.AutoDetectChangesEnabled || force)
			{
				this.ObjectContext.DetectChanges();
			}
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00175130 File Offset: 0x00173330
		public virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			if (typeof(TEntity) != ObjectContextTypeCache.GetObjectType(typeof(TEntity)))
			{
				throw Error.CannotCallGenericSetWithProxyType();
			}
			IInternalSetAdapter internalSetAdapter;
			if (!this._genericSets.TryGetValue(typeof(TEntity), out internalSetAdapter))
			{
				IInternalSet internalSet = this._nonGenericSets.TryGetValue(typeof(TEntity), out internalSetAdapter) ? internalSetAdapter.InternalSet : new InternalSet<TEntity>(this);
				internalSetAdapter = new DbSet<TEntity>((InternalSet<TEntity>)internalSet);
				this._genericSets.Add(typeof(TEntity), internalSetAdapter);
			}
			return (IDbSet<TEntity>)internalSetAdapter;
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x001751CC File Offset: 0x001733CC
		public virtual IInternalSetAdapter Set(Type entityType)
		{
			entityType = ObjectContextTypeCache.GetObjectType(entityType);
			IInternalSetAdapter internalSetAdapter;
			if (!this._nonGenericSets.TryGetValue(entityType, out internalSetAdapter))
			{
				internalSetAdapter = this.CreateInternalSet(entityType, this._genericSets.TryGetValue(entityType, out internalSetAdapter) ? internalSetAdapter.InternalSet : null);
				this._nonGenericSets.Add(entityType, internalSetAdapter);
			}
			return internalSetAdapter;
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00175220 File Offset: 0x00173420
		private IInternalSetAdapter CreateInternalSet(Type entityType, IInternalSet internalSet)
		{
			Func<InternalContext, IInternalSet, IInternalSetAdapter> func;
			if (!InternalContext._setFactories.TryGetValue(entityType, out func))
			{
				if (entityType.IsValueType())
				{
					throw Error.DbSet_EntityTypeNotInModel(entityType.Name);
				}
				Type type = typeof(InternalDbSet<>).MakeGenericType(new Type[]
				{
					entityType
				});
				MethodInfo declaredMethod = type.GetDeclaredMethod("Create", new Type[]
				{
					typeof(InternalContext),
					typeof(IInternalSet)
				});
				func = (Func<InternalContext, IInternalSet, IInternalSetAdapter>)Delegate.CreateDelegate(typeof(Func<InternalContext, IInternalSet, IInternalSetAdapter>), declaredMethod);
				InternalContext._setFactories.TryAdd(entityType, func);
			}
			return func(this, internalSet);
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x001752CC File Offset: 0x001734CC
		public virtual EntitySetTypePair GetEntitySetAndBaseTypeForType(Type entityType)
		{
			this.Initialize();
			this.UpdateEntitySetMappingsForType(entityType);
			return this.GetEntitySetMappingForType(entityType);
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x001752E2 File Offset: 0x001734E2
		public virtual EntitySetTypePair TryGetEntitySetAndBaseTypeForType(Type entityType)
		{
			this.Initialize();
			if (!this.TryUpdateEntitySetMappingsForType(entityType))
			{
				return null;
			}
			return this.GetEntitySetMappingForType(entityType);
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x001752FC File Offset: 0x001734FC
		public virtual bool IsEntityTypeMapped(Type entityType)
		{
			this.Initialize();
			return this.TryUpdateEntitySetMappingsForType(entityType);
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00175328 File Offset: 0x00173528
		public virtual IEnumerable<TEntity> GetLocalEntities<TEntity>()
		{
			return from e in this.ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged | EntityState.Added | EntityState.Modified)
			where e.Entity is TEntity
			select (TEntity)((object)e.Entity);
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0017539C File Offset: 0x0017359C
		public virtual IEnumerator<TElement> ExecuteSqlQuery<TElement>(string sql, bool? streaming, object[] parameters)
		{
			this.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyEnumerator<TElement>(delegate()
			{
				this.Initialize();
				return this.ObjectContext.ExecuteStoreQuery<TElement>(sql, new ExecutionOptions(MergeOption.AppendOnly, streaming), parameters);
			});
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0017542C File Offset: 0x0017362C
		public virtual IDbAsyncEnumerator<TElement> ExecuteSqlQueryAsync<TElement>(string sql, bool? streaming, object[] parameters)
		{
			this.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyAsyncEnumerator<TElement>(delegate(CancellationToken cancellationToken)
			{
				this.Initialize();
				return this.ObjectContext.ExecuteStoreQueryAsync<TElement>(sql, new ExecutionOptions(MergeOption.AppendOnly, streaming), cancellationToken, parameters);
			});
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0017547C File Offset: 0x0017367C
		public virtual IEnumerator ExecuteSqlQuery(Type elementType, string sql, bool? streaming, object[] parameters)
		{
			Func<InternalContext, string, bool?, object[], IEnumerator> func;
			if (!InternalContext._queryExecutors.TryGetValue(elementType, out func))
			{
				MethodInfo method = InternalContext.ExecuteSqlQueryAsIEnumeratorMethod.MakeGenericMethod(new Type[]
				{
					elementType
				});
				func = (Func<InternalContext, string, bool?, object[], IEnumerator>)Delegate.CreateDelegate(typeof(Func<InternalContext, string, bool?, object[], IEnumerator>), method);
				InternalContext._queryExecutors.TryAdd(elementType, func);
			}
			return func(this, sql, streaming, parameters);
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x001754DD File Offset: 0x001736DD
		private IEnumerator ExecuteSqlQueryAsIEnumerator<TElement>(string sql, bool? streaming, object[] parameters)
		{
			return this.ExecuteSqlQuery<TElement>(sql, streaming, parameters);
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001754E8 File Offset: 0x001736E8
		public virtual IDbAsyncEnumerator ExecuteSqlQueryAsync(Type elementType, string sql, bool? streaming, object[] parameters)
		{
			Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator> func;
			if (!InternalContext._asyncQueryExecutors.TryGetValue(elementType, out func))
			{
				MethodInfo method = InternalContext.ExecuteSqlQueryAsIDbAsyncEnumeratorMethod.MakeGenericMethod(new Type[]
				{
					elementType
				});
				func = (Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>)Delegate.CreateDelegate(typeof(Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>), method);
				InternalContext._asyncQueryExecutors.TryAdd(elementType, func);
			}
			return func(this, sql, streaming, parameters);
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00175549 File Offset: 0x00173749
		private IDbAsyncEnumerator ExecuteSqlQueryAsIDbAsyncEnumerator<TElement>(string sql, bool? streaming, object[] parameters)
		{
			return this.ExecuteSqlQueryAsync<TElement>(sql, streaming, parameters);
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00175554 File Offset: 0x00173754
		public virtual int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, object[] parameters)
		{
			this.Initialize();
			return this.ObjectContext.ExecuteStoreCommand(transactionalBehavior, sql, parameters);
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0017556A File Offset: 0x0017376A
		public virtual Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, object[] parameters)
		{
			this.Initialize();
			return this.ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00175584 File Offset: 0x00173784
		public virtual IEntityStateEntry GetStateEntry(object entity)
		{
			this.DetectChanges(false);
			ObjectStateEntry stateEntry;
			if (!this.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
			{
				return null;
			}
			return new StateEntryAdapter(stateEntry);
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x001755C3 File Offset: 0x001737C3
		public virtual IEnumerable<IEntityStateEntry> GetStateEntries()
		{
			return this.GetStateEntries((ObjectStateEntry e) => e.Entity != null);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x001755F8 File Offset: 0x001737F8
		public virtual IEnumerable<IEntityStateEntry> GetStateEntries<TEntity>() where TEntity : class
		{
			return this.GetStateEntries((ObjectStateEntry e) => e.Entity is TEntity);
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00175614 File Offset: 0x00173814
		private IEnumerable<IEntityStateEntry> GetStateEntries(Func<ObjectStateEntry, bool> predicate)
		{
			this.DetectChanges(false);
			return from e in this.ObjectContext.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached).Where(predicate)
			select new StateEntryAdapter(e);
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x00175670 File Offset: 0x00173870
		public virtual DbUpdateException WrapUpdateException(UpdateException updateException)
		{
			if (updateException.StateEntries != null)
			{
				if (updateException.StateEntries.Any((ObjectStateEntry e) => e.Entity == null))
				{
					return new DbUpdateException(this, updateException, true);
				}
			}
			OptimisticConcurrencyException ex = updateException as OptimisticConcurrencyException;
			if (ex == null)
			{
				return new DbUpdateException(this, updateException, false);
			}
			return new DbUpdateConcurrencyException(this, ex);
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x001756D2 File Offset: 0x001738D2
		public virtual TEntity CreateObject<TEntity>() where TEntity : class
		{
			return this.ObjectContext.CreateObject<TEntity>();
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x001756E0 File Offset: 0x001738E0
		public virtual object CreateObject(Type type)
		{
			Func<InternalContext, object> func;
			if (!InternalContext._entityFactories.TryGetValue(type, out func))
			{
				MethodInfo method = InternalContext.CreateObjectAsObjectMethod.MakeGenericMethod(new Type[]
				{
					type
				});
				func = (Func<InternalContext, object>)Delegate.CreateDelegate(typeof(Func<InternalContext, object>), method);
				InternalContext._entityFactories.TryAdd(type, func);
			}
			return func(this);
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x0017573D File Offset: 0x0017393D
		private object CreateObjectAsObject<TEntity>() where TEntity : class
		{
			return this.CreateObject<TEntity>();
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060055FD RID: 22013
		public abstract DbConnection Connection { get; }

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060055FE RID: 22014
		public abstract string OriginalConnectionString { get; }

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060055FF RID: 22015
		public abstract DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x06005600 RID: 22016
		public abstract void OverrideConnection(IInternalConnection connection);

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005601 RID: 22017 RVA: 0x0017574A File Offset: 0x0017394A
		// (set) Token: 0x06005602 RID: 22018 RVA: 0x00175758 File Offset: 0x00173958
		public virtual AppConfig AppConfig
		{
			get
			{
				this.CheckContextNotDisposed();
				return this._appConfig;
			}
			set
			{
				this.CheckContextNotDisposed();
				this._appConfig = value;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005603 RID: 22019 RVA: 0x00175767 File Offset: 0x00173967
		// (set) Token: 0x06005604 RID: 22020 RVA: 0x0017576A File Offset: 0x0017396A
		public virtual DbProviderInfo ModelProviderInfo
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005605 RID: 22021 RVA: 0x0017576C File Offset: 0x0017396C
		public virtual string ConnectionStringName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x0017576F File Offset: 0x0017396F
		public virtual string ProviderName
		{
			get
			{
				return this.Connection.GetProviderInvariantName();
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06005607 RID: 22023 RVA: 0x0017577C File Offset: 0x0017397C
		public DbProviderFactory ProviderFactory
		{
			get
			{
				DbProviderFactory result;
				if ((result = this._providerFactory) == null)
				{
					result = (this._providerFactory = DbProviderServices.GetProviderFactory(this.Connection));
				}
				return result;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x001757A7 File Offset: 0x001739A7
		// (set) Token: 0x06005609 RID: 22025 RVA: 0x001757AA File Offset: 0x001739AA
		public virtual Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600560A RID: 22026 RVA: 0x001757AC File Offset: 0x001739AC
		// (set) Token: 0x0600560B RID: 22027 RVA: 0x001757B4 File Offset: 0x001739B4
		public bool InitializerDisabled { get; set; }

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x0600560C RID: 22028 RVA: 0x001757BD File Offset: 0x001739BD
		public virtual DatabaseOperations DatabaseOperations
		{
			get
			{
				return new DatabaseOperations();
			}
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x001757C4 File Offset: 0x001739C4
		protected void CheckContextNotDisposed()
		{
			if (this.IsDisposed)
			{
				throw Error.DbContext_Disposed();
			}
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001757D4 File Offset: 0x001739D4
		protected void ResetDbSets()
		{
			foreach (IInternalSetAdapter internalSetAdapter in this._genericSets.Values.Union(this._nonGenericSets.Values))
			{
				internalSetAdapter.InternalSet.ResetQuery();
			}
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0017583C File Offset: 0x00173A3C
		public void ForceOSpaceLoadingForKnownEntityTypes()
		{
			if (!this._oSpaceLoadingForced)
			{
				this._oSpaceLoadingForced = true;
				this.Initialize();
				foreach (IInternalSetAdapter internalSetAdapter in this._genericSets.Values.Union(this._nonGenericSets.Values))
				{
					internalSetAdapter.InternalSet.TryInitialize();
				}
			}
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x001758B8 File Offset: 0x00173AB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool TryUpdateEntitySetMappingsForType(Type entityType)
		{
			return this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace.MetadataOptimization.TryUpdateEntitySetMappingsForType(entityType);
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x001758D0 File Offset: 0x00173AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private EntitySetTypePair GetEntitySetMappingForType(Type entityType)
		{
			return this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace.MetadataOptimization.EntitySetMappingCache[entityType];
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x001758ED File Offset: 0x00173AED
		private void UpdateEntitySetMappingsForType(Type entityType)
		{
			if (this.TryUpdateEntitySetMappingsForType(entityType))
			{
				return;
			}
			if (this.IsComplexType(entityType))
			{
				throw Error.DbSet_DbSetUsedWithComplexType(entityType.Name);
			}
			if (InternalContext.IsPocoTypeInNonPocoAssembly(entityType))
			{
				throw Error.DbSet_PocoAndNonPocoMixedInSameAssembly(entityType.Name);
			}
			throw Error.DbSet_EntityTypeNotInModel(entityType.Name);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0017592D File Offset: 0x00173B2D
		private static bool IsPocoTypeInNonPocoAssembly(Type entityType)
		{
			return entityType.Assembly().GetCustomAttributes<EdmSchemaAttribute>().Any<EdmSchemaAttribute>() && !entityType.GetCustomAttributes(true).Any<EdmEntityTypeAttribute>();
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00175974 File Offset: 0x00173B74
		private bool IsComplexType(Type clrType)
		{
			MetadataWorkspace metadataWorkspace = this.GetObjectContextWithoutDatabaseInitialization().MetadataWorkspace;
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);
			ReadOnlyCollection<ComplexType> items = metadataWorkspace.GetItems<ComplexType>(DataSpace.OSpace);
			return items.Any((ComplexType t) => objectItemCollection.GetClrType(t) == clrType);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001759C6 File Offset: 0x00173BC6
		public void ApplyContextInfo(DbContextInfo info)
		{
			if (this._contextInfo != null)
			{
				return;
			}
			this.InitializerDisabled = true;
			this._contextInfo = info;
			this._contextInfo.ConfigureContext(this.Owner);
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005616 RID: 22038 RVA: 0x001759F0 File Offset: 0x00173BF0
		public virtual ValidationProvider ValidationProvider
		{
			get
			{
				return this._validationProvider;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005617 RID: 22039 RVA: 0x001759F8 File Offset: 0x00173BF8
		public virtual string DefaultSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x001759FB File Offset: 0x00173BFB
		// (set) Token: 0x06005619 RID: 22041 RVA: 0x00175A0D File Offset: 0x00173C0D
		public string DefaultContextKey
		{
			get
			{
				return this._defaultContextKey ?? this.OwnerShortTypeName;
			}
			set
			{
				this._defaultContextKey = value;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600561A RID: 22042 RVA: 0x00175A16 File Offset: 0x00173C16
		public DbMigrationsConfiguration MigrationsConfiguration
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfiguration();
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x00175A29 File Offset: 0x00173C29
		public Func<DbConnection, string, HistoryContext> HistoryContextFactory
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfiguration().GetHistoryContextFactory(this.ProviderName);
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x00175A47 File Offset: 0x00173C47
		public virtual bool MigrationsConfigurationDiscovered
		{
			get
			{
				this.DiscoverMigrationsConfiguration();
				return this._migrationsConfigurationDiscovered.Value;
			}
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x00175B10 File Offset: 0x00173D10
		private void DiscoverMigrationsConfiguration()
		{
			if (this._migrationsConfigurationDiscovered == null)
			{
				Type contextType = this.Owner.GetType();
				DbMigrationsConfiguration discoveredConfig = new MigrationsConfigurationFinder(new TypeFinder(contextType.Assembly)).FindMigrationsConfiguration(contextType, null, null, null, null, null);
				if (discoveredConfig != null)
				{
					this._migrationsConfiguration = (() => discoveredConfig);
					this._migrationsConfigurationDiscovered = new bool?(true);
					return;
				}
				this._migrationsConfiguration = (() => new Lazy<DbMigrationsConfiguration>(() => new DbMigrationsConfiguration
				{
					ContextType = contextType,
					AutomaticMigrationsEnabled = true,
					MigrationsAssembly = contextType.Assembly,
					MigrationsNamespace = contextType.Namespace,
					ContextKey = this.DefaultContextKey,
					TargetDatabase = new DbConnectionInfo(this.OriginalConnectionString, this.ProviderName),
					CommandTimeout = this.CommandTimeout
				}).Value);
				this._migrationsConfigurationDiscovered = new bool?(false);
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x00175BC9 File Offset: 0x00173DC9
		internal virtual string OwnerShortTypeName
		{
			get
			{
				return this.Owner.GetType().ToString();
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x00175BDB File Offset: 0x00173DDB
		// (set) Token: 0x06005620 RID: 22048 RVA: 0x00175BF4 File Offset: 0x00173DF4
		public virtual Action<string> Log
		{
			get
			{
				if (this._logFormatter == null)
				{
					return null;
				}
				return this._logFormatter.WriteAction;
			}
			set
			{
				if (this._logFormatter == null || this._logFormatter.WriteAction != value)
				{
					if (this._logFormatter != null)
					{
						this._dispatchers.Value.RemoveInterceptor(this._logFormatter);
						this._logFormatter = null;
					}
					if (value != null)
					{
						this._logFormatter = DbConfiguration.DependencyResolver.GetService<Func<DbContext, Action<string>, DatabaseLogFormatter>>()(this.Owner, value);
						this._dispatchers.Value.AddInterceptor(this._logFormatter);
					}
				}
			}
		}

		// Token: 0x040022CC RID: 8908
		public static readonly MethodInfo CreateObjectAsObjectMethod = typeof(InternalContext).GetOnlyDeclaredMethod("CreateObjectAsObject");

		// Token: 0x040022CD RID: 8909
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, object>> _entityFactories = new ConcurrentDictionary<Type, Func<InternalContext, object>>();

		// Token: 0x040022CE RID: 8910
		public static readonly MethodInfo ExecuteSqlQueryAsIEnumeratorMethod = typeof(InternalContext).GetOnlyDeclaredMethod("ExecuteSqlQueryAsIEnumerator");

		// Token: 0x040022CF RID: 8911
		public static readonly MethodInfo ExecuteSqlQueryAsIDbAsyncEnumeratorMethod = typeof(InternalContext).GetOnlyDeclaredMethod("ExecuteSqlQueryAsIDbAsyncEnumerator");

		// Token: 0x040022D0 RID: 8912
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IEnumerator>> _queryExecutors = new ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IEnumerator>>();

		// Token: 0x040022D1 RID: 8913
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>> _asyncQueryExecutors = new ConcurrentDictionary<Type, Func<InternalContext, string, bool?, object[], IDbAsyncEnumerator>>();

		// Token: 0x040022D2 RID: 8914
		private static readonly ConcurrentDictionary<Type, Func<InternalContext, IInternalSet, IInternalSetAdapter>> _setFactories = new ConcurrentDictionary<Type, Func<InternalContext, IInternalSet, IInternalSetAdapter>>();

		// Token: 0x040022D3 RID: 8915
		public static readonly MethodInfo CreateInitializationActionMethod = typeof(InternalContext).GetOnlyDeclaredMethod("CreateInitializationAction");

		// Token: 0x040022D4 RID: 8916
		private AppConfig _appConfig = AppConfig.DefaultInstance;

		// Token: 0x040022D5 RID: 8917
		private readonly DbContext _owner;

		// Token: 0x040022D6 RID: 8918
		private ClonedObjectContext _tempObjectContext;

		// Token: 0x040022D7 RID: 8919
		private int _tempObjectContextCount;

		// Token: 0x040022D8 RID: 8920
		private readonly Dictionary<Type, IInternalSetAdapter> _genericSets = new Dictionary<Type, IInternalSetAdapter>();

		// Token: 0x040022D9 RID: 8921
		private readonly Dictionary<Type, IInternalSetAdapter> _nonGenericSets = new Dictionary<Type, IInternalSetAdapter>();

		// Token: 0x040022DA RID: 8922
		private readonly ValidationProvider _validationProvider = new ValidationProvider(null, DbConfiguration.DependencyResolver.GetService<AttributeProvider>());

		// Token: 0x040022DB RID: 8923
		private bool _oSpaceLoadingForced;

		// Token: 0x040022DC RID: 8924
		private DbProviderFactory _providerFactory;

		// Token: 0x040022DD RID: 8925
		private readonly Lazy<DbDispatchers> _dispatchers;

		// Token: 0x040022DF RID: 8927
		private DatabaseLogFormatter _logFormatter;

		// Token: 0x040022E0 RID: 8928
		private Func<DbMigrationsConfiguration> _migrationsConfiguration;

		// Token: 0x040022E1 RID: 8929
		private bool? _migrationsConfigurationDiscovered;

		// Token: 0x040022E2 RID: 8930
		private DbContextInfo _contextInfo;

		// Token: 0x040022E3 RID: 8931
		private string _defaultContextKey;
	}
}
