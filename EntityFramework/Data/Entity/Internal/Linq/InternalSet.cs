using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000794 RID: 1940
	internal class InternalSet<TEntity> : InternalQuery<TEntity>, IInternalSet<TEntity>, IInternalSet, IInternalQuery<TEntity>, IInternalQuery where TEntity : class
	{
		// Token: 0x060057DC RID: 22492 RVA: 0x00179C72 File Offset: 0x00177E72
		public InternalSet(InternalContext internalContext) : base(internalContext)
		{
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x00179C7B File Offset: 0x00177E7B
		public override void ResetQuery()
		{
			this._entitySet = null;
			this._localView = null;
			base.ResetQuery();
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x00179C94 File Offset: 0x00177E94
		public TEntity Find(params object[] keyValues)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.InternalContext.DetectChanges(false);
			WrappedEntityKey key = new WrappedEntityKey(this.EntitySet, this.EntitySetName, keyValues, "keyValues");
			object obj = this.FindInStateManager(key) ?? this.FindInStore(key, "keyValues");
			if (obj != null && !(obj is TEntity))
			{
				throw Error.DbSet_WrongEntityTypeFound(obj.GetType().Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)obj);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x00179D23 File Offset: 0x00177F23
		public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return this.FindInternalAsync(cancellationToken, keyValues);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x00179EF0 File Offset: 0x001780F0
		private async Task<TEntity> FindInternalAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			this.InternalContext.DetectChanges(false);
			WrappedEntityKey key = new WrappedEntityKey(this.EntitySet, this.EntitySetName, keyValues, "keyValues");
			object entity = this.FindInStateManager(key) ?? (await this.FindInStoreAsync(key, "keyValues", cancellationToken).WithCurrentCulture<object>());
			if (entity != null && !(entity is TEntity))
			{
				throw Error.DbSet_WrongEntityTypeFound(entity.GetType().Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)entity);
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x00179F70 File Offset: 0x00178170
		private object FindInStateManager(WrappedEntityKey key)
		{
			ObjectStateEntry objectStateEntry;
			if (!key.HasNullValues && this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(key.EntityKey, out objectStateEntry))
			{
				return objectStateEntry.Entity;
			}
			object obj = null;
			foreach (ObjectStateEntry objectStateEntry2 in from e in this.InternalContext.ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added)
			where !e.IsRelationship && e.Entity != null && this.EntitySetBaseType.IsAssignableFrom(e.Entity.GetType())
			select e)
			{
				bool flag = true;
				foreach (KeyValuePair<string, object> keyValuePair in key.KeyValuePairs)
				{
					int ordinal = objectStateEntry2.CurrentValues.GetOrdinal(keyValuePair.Key);
					if (!DbHelpers.KeyValuesEqual(keyValuePair.Value, objectStateEntry2.CurrentValues.GetValue(ordinal)))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					if (obj != null)
					{
						throw Error.DbSet_MultipleAddedEntitiesFound();
					}
					obj = objectStateEntry2.Entity;
				}
			}
			return obj;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0017A094 File Offset: 0x00178294
		private object FindInStore(WrappedEntityKey key, string keyValuesParamName)
		{
			if (key.HasNullValues)
			{
				return null;
			}
			object result;
			try
			{
				result = this.BuildFindQuery(key).SingleOrDefault<TEntity>();
			}
			catch (EntitySqlException innerException)
			{
				throw new ArgumentException(Strings.DbSet_WrongKeyValueType, keyValuesParamName, innerException);
			}
			return result;
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x0017A214 File Offset: 0x00178414
		private async Task<object> FindInStoreAsync(WrappedEntityKey key, string keyValuesParamName, CancellationToken cancellationToken)
		{
			object result;
			if (key.HasNullValues)
			{
				result = null;
			}
			else
			{
				try
				{
					result = await this.BuildFindQuery(key).SingleOrDefaultAsync(cancellationToken).WithCurrentCulture<TEntity>();
				}
				catch (EntitySqlException innerException)
				{
					throw new ArgumentException(Strings.DbSet_WrongKeyValueType, keyValuesParamName, innerException);
				}
			}
			return result;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0017A274 File Offset: 0x00178474
		private ObjectQuery<TEntity> BuildFindQuery(WrappedEntityKey key)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", this.QuotedEntitySetName);
			EntityKeyMember[] entityKeyValues = key.EntityKey.EntityKeyValues;
			ObjectParameter[] array = new ObjectParameter[entityKeyValues.Length];
			for (int i = 0; i < entityKeyValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" AND ");
				}
				string text = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[]
				{
					i.ToString(CultureInfo.InvariantCulture)
				});
				stringBuilder.AppendFormat("X.{0} = @{1}", DbHelpers.QuoteIdentifier(entityKeyValues[i].Key), text);
				array[i] = new ObjectParameter(text, entityKeyValues[i].Value);
			}
			return this.InternalContext.ObjectContext.CreateQuery<TEntity>(stringBuilder.ToString(), array);
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0017A33C File Offset: 0x0017853C
		public ObservableCollection<TEntity> Local
		{
			get
			{
				this.InternalContext.DetectChanges(false);
				DbLocalView<TEntity> result;
				if ((result = this._localView) == null)
				{
					result = (this._localView = new DbLocalView<TEntity>(this.InternalContext));
				}
				return result;
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0017A3A4 File Offset: 0x001785A4
		public virtual void Attach(object entity)
		{
			this.ActOnSet(delegate()
			{
				this.InternalContext.ObjectContext.AttachTo(this.EntitySetName, entity);
			}, EntityState.Unchanged, entity, "Attach");
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0017A414 File Offset: 0x00178614
		public virtual void Add(object entity)
		{
			this.ActOnSet(delegate()
			{
				this.InternalContext.ObjectContext.AddObject(this.EntitySetName, entity);
			}, EntityState.Added, entity, "Add");
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x0017A46C File Offset: 0x0017866C
		public virtual void AddRange(IEnumerable entities)
		{
			this.InternalContext.DetectChanges(false);
			this.ActOnSet(delegate(object entity)
			{
				this.InternalContext.ObjectContext.AddObject(this.EntitySetName, entity);
			}, EntityState.Added, entities, "AddRange");
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x0017A494 File Offset: 0x00178694
		public virtual void Remove(object entity)
		{
			if (!(entity is TEntity))
			{
				throw Error.DbSet_BadTypeForAddAttachRemove("Remove", entity.GetType().Name, typeof(TEntity).Name);
			}
			this.InternalContext.DetectChanges(false);
			this.InternalContext.ObjectContext.DeleteObject(entity);
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x0017A4EC File Offset: 0x001786EC
		public virtual void RemoveRange(IEnumerable entities)
		{
			List<object> list = entities.Cast<object>().ToList<object>();
			this.InternalContext.DetectChanges(false);
			foreach (object obj in list)
			{
				Check.NotNull<object>(obj, "entity");
				if (!(obj is TEntity))
				{
					throw Error.DbSet_BadTypeForAddAttachRemove("RemoveRange", obj.GetType().Name, typeof(TEntity).Name);
				}
				this.InternalContext.ObjectContext.DeleteObject(obj);
			}
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x0017A598 File Offset: 0x00178798
		private void ActOnSet(Action action, EntityState newState, object entity, string methodName)
		{
			if (!(entity is TEntity))
			{
				throw Error.DbSet_BadTypeForAddAttachRemove(methodName, entity.GetType().Name, typeof(TEntity).Name);
			}
			this.InternalContext.DetectChanges(false);
			ObjectStateEntry objectStateEntry;
			if (this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out objectStateEntry))
			{
				objectStateEntry.ChangeState(newState);
				return;
			}
			action();
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x0017A604 File Offset: 0x00178804
		private void ActOnSet(Action<object> action, EntityState newState, IEnumerable entities, string methodName)
		{
			foreach (object obj in entities)
			{
				Check.NotNull<object>(obj, "entity");
				if (!(obj is TEntity))
				{
					throw Error.DbSet_BadTypeForAddAttachRemove(methodName, obj.GetType().Name, typeof(TEntity).Name);
				}
				ObjectStateEntry objectStateEntry;
				if (this.InternalContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(obj, out objectStateEntry))
				{
					objectStateEntry.ChangeState(newState);
				}
				else
				{
					action(obj);
				}
			}
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x0017A6AC File Offset: 0x001788AC
		public TEntity Create()
		{
			return this.InternalContext.CreateObject<TEntity>();
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x0017A6BC File Offset: 0x001788BC
		public TEntity Create(Type derivedEntityType)
		{
			if (!typeof(TEntity).IsAssignableFrom(derivedEntityType))
			{
				throw Error.DbSet_BadTypeForCreate(derivedEntityType.Name, typeof(TEntity).Name);
			}
			return (TEntity)((object)this.InternalContext.CreateObject(ObjectContextTypeCache.GetObjectType(derivedEntityType)));
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x0017A70C File Offset: 0x0017890C
		public override ObjectQuery<TEntity> ObjectQuery
		{
			get
			{
				this.Initialize();
				return base.ObjectQuery;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060057F0 RID: 22512 RVA: 0x0017A71A File Offset: 0x0017891A
		public string EntitySetName
		{
			get
			{
				this.Initialize();
				return this._entitySetName;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0017A728 File Offset: 0x00178928
		public string QuotedEntitySetName
		{
			get
			{
				this.Initialize();
				return this._quotedEntitySetName;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x0017A736 File Offset: 0x00178936
		public EntitySet EntitySet
		{
			get
			{
				this.Initialize();
				return this._entitySet;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0017A744 File Offset: 0x00178944
		public Type EntitySetBaseType
		{
			get
			{
				this.Initialize();
				return this._baseType;
			}
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x0017A754 File Offset: 0x00178954
		public virtual void Initialize()
		{
			if (this._entitySet == null)
			{
				EntitySetTypePair entitySetAndBaseTypeForType = base.InternalContext.GetEntitySetAndBaseTypeForType(typeof(TEntity));
				if (this._entitySet == null)
				{
					this.InitializeUnderlyingTypes(entitySetAndBaseTypeForType);
				}
			}
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x0017A790 File Offset: 0x00178990
		public virtual void TryInitialize()
		{
			if (this._entitySet == null)
			{
				EntitySetTypePair entitySetTypePair = base.InternalContext.TryGetEntitySetAndBaseTypeForType(typeof(TEntity));
				if (entitySetTypePair != null)
				{
					this.InitializeUnderlyingTypes(entitySetTypePair);
				}
			}
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x0017A7C8 File Offset: 0x001789C8
		private void InitializeUnderlyingTypes(EntitySetTypePair pair)
		{
			this._entitySet = pair.EntitySet;
			this._baseType = pair.BaseType;
			this._entitySetName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				this._entitySet.EntityContainer.Name,
				this._entitySet.Name
			});
			this._quotedEntitySetName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this._entitySet.EntityContainer.Name),
				DbHelpers.QuoteIdentifier(this._entitySet.Name)
			});
			base.InitializeQuery(this.CreateObjectQuery(false, null, null));
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0017A88C File Offset: 0x00178A8C
		private ObjectQuery<TEntity> CreateObjectQuery(bool asNoTracking, bool? streaming = null, IDbExecutionStrategy executionStrategy = null)
		{
			ObjectQuery<TEntity> objectQuery = this.InternalContext.ObjectContext.CreateQuery<TEntity>(this._quotedEntitySetName, new ObjectParameter[0]);
			if (this._baseType != typeof(TEntity))
			{
				objectQuery = objectQuery.OfType<TEntity>();
			}
			if (asNoTracking)
			{
				objectQuery.MergeOption = MergeOption.NoTracking;
			}
			if (streaming != null)
			{
				objectQuery.Streaming = streaming.Value;
			}
			objectQuery.ExecutionStrategy = executionStrategy;
			return objectQuery;
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x0017A8FC File Offset: 0x00178AFC
		public override string ToString()
		{
			this.Initialize();
			return base.ToString();
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0017A90A File Offset: 0x00178B0A
		public override InternalContext InternalContext
		{
			get
			{
				this.Initialize();
				return base.InternalContext;
			}
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x0017A918 File Offset: 0x00178B18
		public override IInternalQuery<TEntity> Include(string path)
		{
			this.Initialize();
			return base.Include(path);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x0017A928 File Offset: 0x00178B28
		public override IInternalQuery<TEntity> AsNoTracking()
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(true, null, null));
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x0017A957 File Offset: 0x00178B57
		public override IInternalQuery<TEntity> AsStreaming()
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(false, new bool?(true), null));
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x0017A978 File Offset: 0x00178B78
		public override IInternalQuery<TEntity> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			this.Initialize();
			return new InternalQuery<TEntity>(this.InternalContext, this.CreateObjectQuery(false, new bool?(false), executionStrategy));
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x0017A9E0 File Offset: 0x00178BE0
		public IEnumerator ExecuteSqlQuery(string sql, bool asNoTracking, bool? streaming, object[] parameters)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.Initialize();
			MergeOption mergeOption = asNoTracking ? MergeOption.NoTracking : MergeOption.AppendOnly;
			return new LazyEnumerator<TEntity>(() => this.InternalContext.ObjectContext.ExecuteStoreQuery<TEntity>(sql, this.EntitySetName, new ExecutionOptions(mergeOption, streaming), parameters));
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x0017AA94 File Offset: 0x00178C94
		public IDbAsyncEnumerator ExecuteSqlQueryAsync(string sql, bool asNoTracking, bool? streaming, object[] parameters)
		{
			this.InternalContext.ObjectContext.AsyncMonitor.EnsureNotEntered();
			this.Initialize();
			MergeOption mergeOption = asNoTracking ? MergeOption.NoTracking : MergeOption.AppendOnly;
			return new LazyAsyncEnumerator<TEntity>((CancellationToken cancellationToken) => this.InternalContext.ObjectContext.ExecuteStoreQueryAsync<TEntity>(sql, this.EntitySetName, new ExecutionOptions(mergeOption, streaming), cancellationToken, parameters));
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005800 RID: 22528 RVA: 0x0017AAFD File Offset: 0x00178CFD
		public override Expression Expression
		{
			get
			{
				this.Initialize();
				return base.Expression;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0017AB0B File Offset: 0x00178D0B
		public override ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				this.Initialize();
				return base.ObjectQueryProvider;
			}
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x0017AB19 File Offset: 0x00178D19
		public override IEnumerator<TEntity> GetEnumerator()
		{
			this.Initialize();
			return base.GetEnumerator();
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x0017AB27 File Offset: 0x00178D27
		public override IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
		{
			this.Initialize();
			return base.GetAsyncEnumerator();
		}

		// Token: 0x0400234C RID: 9036
		private DbLocalView<TEntity> _localView;

		// Token: 0x0400234D RID: 9037
		private EntitySet _entitySet;

		// Token: 0x0400234E RID: 9038
		private string _entitySetName;

		// Token: 0x0400234F RID: 9039
		private string _quotedEntitySetName;

		// Token: 0x04002350 RID: 9040
		private Type _baseType;
	}
}
