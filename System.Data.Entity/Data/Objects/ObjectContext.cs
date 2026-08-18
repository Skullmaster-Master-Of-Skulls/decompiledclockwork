using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Internal.Materialization;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace System.Data.Objects
{
	// Token: 0x02000144 RID: 324
	public class ObjectContext : IDisposable
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
		public ObjectContext(EntityConnection connection) : this(EntityUtil.CheckArgumentNull<EntityConnection>(connection, "connection"), true)
		{
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004CAC8 File Offset: 0x0004ACC8
		public ObjectContext(string connectionString) : this(ObjectContext.CreateEntityConnection(connectionString), false)
		{
			this._createdConnection = true;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0004CADE File Offset: 0x0004ACDE
		protected ObjectContext(string connectionString, string defaultContainerName) : this(connectionString)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0004CAFD File Offset: 0x0004ACFD
		protected ObjectContext(EntityConnection connection, string defaultContainerName) : this(connection)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0004CB1C File Offset: 0x0004AD1C
		private ObjectContext(EntityConnection connection, bool isConnectionConstructor)
		{
			this._connection = connection;
			this._connection.StateChange += this.ConnectionStateChange;
			string connectionString = connection.ConnectionString;
			if (connectionString == null || connectionString.Trim().Length == 0)
			{
				throw EntityUtil.InvalidConnection(isConnectionConstructor, null);
			}
			try
			{
				this._workspace = this.RetrieveMetadataWorkspaceFromConnection();
			}
			catch (InvalidOperationException innerException)
			{
				throw EntityUtil.InvalidConnection(isConnectionConstructor, innerException);
			}
			if (this._workspace != null)
			{
				if (!this._workspace.IsItemCollectionAlreadyRegistered(DataSpace.OSpace))
				{
					ObjectItemCollection collection = new ObjectItemCollection();
					this._workspace.RegisterItemCollection(collection);
				}
				this._workspace.GetItemCollection(DataSpace.OCSpace);
			}
			string value = ConfigurationManager.AppSettings[this.s_UseLegacyPreserveChangesBehavior];
			bool useLegacyPreserveChangesBehavior = false;
			if (bool.TryParse(value, out useLegacyPreserveChangesBehavior))
			{
				this.ContextOptions.UseLegacyPreserveChangesBehavior = useLegacyPreserveChangesBehavior;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0004CC0C File Offset: 0x0004AE0C
		public DbConnection Connection
		{
			get
			{
				if (this._connection == null)
				{
					throw EntityUtil.ObjectContextDisposed();
				}
				return this._connection;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0004CC24 File Offset: 0x0004AE24
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0004CC4C File Offset: 0x0004AE4C
		public string DefaultContainerName
		{
			get
			{
				EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
				if (defaultContainer == null)
				{
					return string.Empty;
				}
				return defaultContainer.Name;
			}
			set
			{
				if (!this._disallowSettingDefaultContainerName)
				{
					this.Perspective.SetDefaultContainer(value);
					return;
				}
				throw EntityUtil.CannotSetDefaultContainerName();
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0004CC68 File Offset: 0x0004AE68
		[CLSCompliant(false)]
		public MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._workspace;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0004CC70 File Offset: 0x0004AE70
		public ObjectStateManager ObjectStateManager
		{
			get
			{
				if (this._cache == null)
				{
					this._cache = new ObjectStateManager(this._workspace);
				}
				return this._cache;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0004CC91 File Offset: 0x0004AE91
		internal ClrPerspective Perspective
		{
			get
			{
				if (this._perspective == null)
				{
					this._perspective = new ClrPerspective(this._workspace);
				}
				return this._perspective;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0004CCB2 File Offset: 0x0004AEB2
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0004CCBC File Offset: 0x0004AEBC
		public int? CommandTimeout
		{
			get
			{
				return this._queryTimeout;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						throw EntityUtil.InvalidCommandTimeout("value");
					}
				}
				this._queryTimeout = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0004CCFC File Offset: 0x0004AEFC
		protected internal IQueryProvider QueryProvider
		{
			get
			{
				if (this._queryProvider == null)
				{
					this._queryProvider = new ObjectQueryProvider(this);
				}
				return this._queryProvider;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0004CD18 File Offset: 0x0004AF18
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0004CD20 File Offset: 0x0004AF20
		internal bool InMaterialization { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0004CD29 File Offset: 0x0004AF29
		public ObjectContextOptions ContextOptions
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600172F RID: 5935 RVA: 0x0004CD31 File Offset: 0x0004AF31
		// (remove) Token: 0x06001730 RID: 5936 RVA: 0x0004CD4A File Offset: 0x0004AF4A
		public event EventHandler SavingChanges
		{
			add
			{
				this._onSavingChanges = (EventHandler)Delegate.Combine(this._onSavingChanges, value);
			}
			remove
			{
				this._onSavingChanges = (EventHandler)Delegate.Remove(this._onSavingChanges, value);
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004CD63 File Offset: 0x0004AF63
		private void OnSavingChanges()
		{
			if (this._onSavingChanges != null)
			{
				this._onSavingChanges(this, new EventArgs());
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001732 RID: 5938 RVA: 0x0004CD7E File Offset: 0x0004AF7E
		// (remove) Token: 0x06001733 RID: 5939 RVA: 0x0004CD97 File Offset: 0x0004AF97
		public event ObjectMaterializedEventHandler ObjectMaterialized
		{
			add
			{
				this._onObjectMaterialized = (ObjectMaterializedEventHandler)Delegate.Combine(this._onObjectMaterialized, value);
			}
			remove
			{
				this._onObjectMaterialized = (ObjectMaterializedEventHandler)Delegate.Remove(this._onObjectMaterialized, value);
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004CDB0 File Offset: 0x0004AFB0
		internal void OnObjectMaterialized(object entity)
		{
			if (this._onObjectMaterialized != null)
			{
				this._onObjectMaterialized(this, new ObjectMaterializedEventArgs(entity));
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0004CDCC File Offset: 0x0004AFCC
		internal bool OnMaterializedHasHandlers
		{
			get
			{
				return this._onObjectMaterialized != null && this._onObjectMaterialized.GetInvocationList().Length != 0;
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0004CDE8 File Offset: 0x0004AFE8
		public void AcceptAllChanges()
		{
			if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			foreach (ObjectStateEntry objectStateEntry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted))
			{
				objectStateEntry.AcceptChanges();
			}
			foreach (ObjectStateEntry objectStateEntry2 in this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
			{
				objectStateEntry2.AcceptChanges();
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0004CE94 File Offset: 0x0004B094
		private void VerifyRootForAdd(bool doAttach, string entitySetName, IEntityWrapper wrappedEntity, EntityEntry existingEntry, out EntitySet entitySet, out bool isNoOperation)
		{
			isNoOperation = false;
			EntitySet entitySet2 = null;
			if (doAttach)
			{
				if (!string.IsNullOrEmpty(entitySetName))
				{
					entitySet2 = this.GetEntitySetFromName(entitySetName);
				}
			}
			else
			{
				entitySet2 = this.GetEntitySetFromName(entitySetName);
			}
			EntitySet entitySet3 = null;
			EntityKey entityKey = (existingEntry != null) ? existingEntry.EntityKey : wrappedEntity.GetEntityKeyFromEntity();
			if (entityKey != null)
			{
				entitySet3 = entityKey.GetEntitySet(this.MetadataWorkspace);
				if (entitySet2 != null)
				{
					EntityUtil.ValidateEntitySetInKey(entityKey, entitySet2, "entitySetName");
				}
				entityKey.ValidateEntityKey(this._workspace, entitySet3);
			}
			entitySet = (entitySet3 ?? entitySet2);
			if (entitySet == null)
			{
				throw EntityUtil.EntitySetNameOrEntityKeyRequired();
			}
			this.ValidateEntitySet(entitySet, wrappedEntity.IdentityType);
			if (doAttach && existingEntry == null)
			{
				if (entityKey == null)
				{
					entityKey = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
				}
				existingEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			}
			if (existingEntry == null || (doAttach && existingEntry.IsKeyEntry))
			{
				return;
			}
			if (existingEntry.Entity != wrappedEntity.Entity)
			{
				throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
			}
			EntityState entityState = doAttach ? EntityState.Unchanged : EntityState.Added;
			if (existingEntry.State != entityState)
			{
				throw doAttach ? EntityUtil.EntityAlreadyExistsInObjectStateManager() : EntityUtil.ObjectStateManagerDoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(existingEntry.State);
			}
			isNoOperation = true;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0004CFAC File Offset: 0x0004B1AC
		public void AddObject(string entitySetName, object entity)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
			if (entityEntry == null)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			}
			EntitySet entitySet;
			bool flag;
			this.VerifyRootForAdd(false, entitySetName, entityWrapper, entityEntry, out entitySet, out flag);
			if (flag)
			{
				return;
			}
			System.Data.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
			transactionManager.BeginAddTracking();
			try
			{
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				bool flag2 = true;
				try
				{
					this.AddSingleObject(entitySet, entityWrapper, "entity");
					flag2 = false;
				}
				finally
				{
					if (flag2 && entityWrapper.Context == this)
					{
						EntityEntry entityEntry2 = this.ObjectStateManager.FindEntityEntry(entityWrapper.Entity);
						if (entityEntry2 != null && entityEntry2.EntityKey.IsTemporary)
						{
							relationshipManager.NodeVisited = true;
							RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
							RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
						}
					}
				}
				relationshipManager.AddRelatedEntitiesToObjectStateManager(false);
			}
			finally
			{
				transactionManager.EndAddTracking();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0004D09C File Offset: 0x0004B29C
		internal void AddSingleObject(EntitySet entitySet, IEntityWrapper wrappedEntity, string argumentName)
		{
			EntityKey entityKeyFromEntity = wrappedEntity.GetEntityKeyFromEntity();
			if (entityKeyFromEntity != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKeyFromEntity, entitySet);
				entityKeyFromEntity.ValidateEntityKey(this._workspace, entitySet);
			}
			this.VerifyContextForAddOrAttach(wrappedEntity);
			wrappedEntity.Context = this;
			EntityEntry entityEntry = this.ObjectStateManager.AddEntry(wrappedEntity, null, entitySet, argumentName, true);
			this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
			wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
			entityEntry.FixupFKValuesFromNonAddedReferences();
			this._cache.FixupReferencesByForeignKeys(entityEntry, false);
			wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004D120 File Offset: 0x0004B320
		public void LoadProperty(object entity, string navigationProperty)
		{
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, false).Load();
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0004D14C File Offset: 0x0004B34C
		public void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
		{
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, false).Load(mergeOption);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0004D17C File Offset: 0x0004B37C
		public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector)
		{
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out throwArgumentException);
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException).Load();
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0004D1B8 File Offset: 0x0004B3B8
		public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
		{
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out throwArgumentException);
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException).Load(mergeOption);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0004D1F4 File Offset: 0x0004B3F4
		private IEntityWrapper WrapEntityAndCheckContext(object entity, string refType)
		{
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(entity, this);
			if (entityWrapper.Context == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotExplicitlyLoadDetachedRelationships(refType));
			}
			if (entityWrapper.Context != this)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotLoadReferencesUsingDifferentContext(refType));
			}
			return entityWrapper;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0004D234 File Offset: 0x0004B434
		internal static string ParsePropertySelectorExpression<TEntity>(Expression<Func<TEntity, object>> selector, out bool removedConvert)
		{
			EntityUtil.CheckArgumentNull<Expression<Func<TEntity, object>>>(selector, "selector");
			removedConvert = false;
			Expression expression = selector.Body;
			while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
			{
				removedConvert = true;
				expression = ((UnaryExpression)expression).Operand;
			}
			MemberExpression memberExpression = expression as MemberExpression;
			if (memberExpression == null || !memberExpression.Member.DeclaringType.IsAssignableFrom(typeof(TEntity)) || memberExpression.Expression.NodeType != ExpressionType.Parameter)
			{
				throw new ArgumentException(Strings.ObjectContext_SelectorExpressionMustBeMemberAccess);
			}
			return memberExpression.Member.Name;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0004D2C7 File Offset: 0x0004B4C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Use ApplyCurrentValues instead")]
		public void ApplyPropertyChanges(string entitySetName, object changed)
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			EntityUtil.CheckArgumentNull<object>(changed, "changed");
			this.ApplyCurrentValues<object>(entitySetName, changed);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			EntityUtil.CheckArgumentNull<TEntity>(currentEntity, "currentEntity");
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(currentEntity, this);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EntityKey entityKey = entityWrapper.EntityKey;
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
				entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, currentEntity);
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null || entityEntry.IsKeyEntry)
			{
				throw EntityUtil.EntityNotTracked();
			}
			entityEntry.ApplyCurrentValuesInternal(entityWrapper);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			EntityUtil.CheckArgumentNull<TEntity>(originalEntity, "originalEntity");
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(originalEntity, this);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EntityKey entityKey = entityWrapper.EntityKey;
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
				entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, originalEntity);
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null || entityEntry.IsKeyEntry)
			{
				throw EntityUtil.EntityNotTrackedOrHasTempKey();
			}
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged && entityEntry.State != EntityState.Deleted)
			{
				throw EntityUtil.EntityMustBeUnchangedOrModifiedOrDeleted(entityEntry.State);
			}
			if (entityEntry.WrappedEntity.IdentityType != entityWrapper.IdentityType)
			{
				throw EntityUtil.EntitiesHaveDifferentType(entityEntry.Entity.GetType().FullName, originalEntity.GetType().FullName);
			}
			entityEntry.CompareKeyProperties(originalEntity);
			entityEntry.UpdateOriginalValues(entityWrapper.Entity);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0004D4CC File Offset: 0x0004B6CC
		public void AttachTo(string entitySetName, object entity)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
			if (entityEntry == null)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, null);
			}
			EntitySet entitySet;
			bool flag;
			this.VerifyRootForAdd(true, entitySetName, entityWrapper, entityEntry, out entitySet, out flag);
			if (flag)
			{
				return;
			}
			System.Data.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
			transactionManager.BeginAttachTracking();
			try
			{
				this.ObjectStateManager.TransactionManager.OriginalMergeOption = new MergeOption?(entityWrapper.MergeOption);
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				bool flag2 = true;
				try
				{
					this.AttachSingleObject(entityWrapper, entitySet, "entity");
					flag2 = false;
				}
				finally
				{
					if (flag2 && entityWrapper.Context == this)
					{
						relationshipManager.NodeVisited = true;
						RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
						RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
					}
				}
				relationshipManager.AddRelatedEntitiesToObjectStateManager(true);
			}
			finally
			{
				transactionManager.EndAttachTracking();
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0004D5B4 File Offset: 0x0004B7B4
		public void Attach(IEntityWithKey entity)
		{
			EntityUtil.CheckArgumentNull<IEntityWithKey>(entity, "entity");
			if (entity.EntityKey == null)
			{
				throw EntityUtil.CannotAttachEntityWithoutKey();
			}
			this.AttachTo(null, entity);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		internal void AttachSingleObject(IEntityWrapper wrappedEntity, EntitySet entitySet, string argumentName)
		{
			RelationshipManager relationshipManager = wrappedEntity.RelationshipManager;
			EntityKey entityKey = wrappedEntity.GetEntityKeyFromEntity();
			if (entityKey != null)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, entitySet);
				entityKey.ValidateEntityKey(this._workspace, entitySet);
			}
			else
			{
				entityKey = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
			}
			if (entityKey.IsTemporary)
			{
				throw EntityUtil.CannotAttachEntityWithTemporaryKey();
			}
			if (wrappedEntity.EntityKey != entityKey)
			{
				wrappedEntity.EntityKey = entityKey;
			}
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
			if (entityEntry == null)
			{
				this.VerifyContextForAddOrAttach(wrappedEntity);
				wrappedEntity.Context = this;
				entityEntry = this.ObjectStateManager.AttachEntry(entityKey, wrappedEntity, entitySet, argumentName);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			if (entityEntry.IsKeyEntry)
			{
				this.ObjectStateManager.PromoteKeyEntryInitialization(this, entityEntry, wrappedEntity, null, false);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				this.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, null, false, false, true, "Attach");
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0004D712 File Offset: 0x0004B912
		private void VerifyContextForAddOrAttach(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.Context != this && !wrappedEntity.Context.ObjectStateManager.IsDisposed && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw EntityUtil.EntityCantHaveMultipleChangeTrackers();
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0004D748 File Offset: 0x0004B948
		public EntityKey CreateEntityKey(string entitySetName, object entity)
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(EntityUtil.GetEntityIdentityType(entity.GetType()), null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			return this.ObjectStateManager.CreateEntityKey(entitySetFromName, entity);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0004D798 File Offset: 0x0004B998
		internal EntitySet GetEntitySetFromName(string entitySetName)
		{
			string entitySetName2;
			string entityContainerName;
			ObjectContext.GetEntitySetName(entitySetName, "entitySetName", this, out entitySetName2, out entityContainerName);
			return this.GetEntitySet(entitySetName2, entityContainerName);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0004D7C0 File Offset: 0x0004B9C0
		private void AddRefreshKey(object entityLike, Dictionary<EntityKey, EntityEntry> entities, Dictionary<EntitySet, List<EntityKey>> currentKeys)
		{
			if (entityLike == null)
			{
				throw EntityUtil.NthElementIsNull(entities.Count);
			}
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(entityLike, this);
			EntityKey entityKey = entityWrapper.EntityKey;
			this.RefreshCheck(entities, entityLike, entityKey);
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			List<EntityKey> list = null;
			if (!currentKeys.TryGetValue(entitySet, out list))
			{
				list = new List<EntityKey>();
				currentKeys.Add(entitySet, list);
			}
			list.Add(entityKey);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0004D824 File Offset: 0x0004BA24
		public ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
		{
			EntitySet entitySetForType = this.GetEntitySetForType(typeof(TEntity), "TEntity");
			return new ObjectSet<TEntity>(entitySetForType, this);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0004D850 File Offset: 0x0004BA50
		private EntitySet GetEntitySetForType(Type entityCLRType, string exceptionParameterName)
		{
			EntitySet entitySet = null;
			EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
			if (defaultContainer == null)
			{
				ReadOnlyCollection<EntityContainer> items = this.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace);
				using (IEnumerator<EntityContainer> enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EntityContainer container = enumerator.Current;
						EntitySet entitySetFromContainer = this.GetEntitySetFromContainer(container, entityCLRType, exceptionParameterName);
						if (entitySetFromContainer != null)
						{
							if (entitySet != null)
							{
								throw EntityUtil.MultipleEntitySetsFoundInAllContainers(entityCLRType.FullName, exceptionParameterName);
							}
							entitySet = entitySetFromContainer;
						}
					}
					goto IL_70;
				}
			}
			entitySet = this.GetEntitySetFromContainer(defaultContainer, entityCLRType, exceptionParameterName);
			IL_70:
			if (entitySet == null)
			{
				throw EntityUtil.NoEntitySetFoundForType(entityCLRType.FullName, exceptionParameterName);
			}
			return entitySet;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		private EntitySet GetEntitySetFromContainer(EntityContainer container, Type entityCLRType, string exceptionParameterName)
		{
			EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
			EntitySet entitySet = null;
			foreach (EntitySetBase entitySetBase in container.BaseEntitySets)
			{
				if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet && entitySetBase.ElementType == edmType)
				{
					if (entitySet != null)
					{
						throw EntityUtil.MultipleEntitySetsFoundInSingleContainer(entityCLRType.FullName, container.Name, exceptionParameterName);
					}
					entitySet = (EntitySet)entitySetBase;
				}
			}
			return entitySet;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0004D97C File Offset: 0x0004BB7C
		public ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
		{
			EntitySet entitySetForNameAndType = this.GetEntitySetForNameAndType(entitySetName, typeof(TEntity), "TEntity");
			return new ObjectSet<TEntity>(entitySetForNameAndType, this);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
		private EntitySet GetEntitySetForNameAndType(string entitySetName, Type entityCLRType, string exceptionParameterName)
		{
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
			if (entitySetFromName.ElementType != edmType)
			{
				throw EntityUtil.InvalidEntityTypeForObjectSet(entityCLRType.FullName, entitySetFromName.ElementType.FullName, entitySetName, exceptionParameterName);
			}
			return entitySetFromName;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		internal void EnsureConnection()
		{
			if (this._connection == null)
			{
				throw EntityUtil.ObjectContextDisposed();
			}
			if (this.Connection.State == ConnectionState.Closed)
			{
				this.Connection.Open();
				this._openedConnection = true;
			}
			if (this._openedConnection)
			{
				this._connectionRequestCount++;
			}
			if (this._connection.State == ConnectionState.Closed || this._connection.State == ConnectionState.Broken)
			{
				string error = Strings.EntityClient_ExecutingOnClosedConnection((this._connection.State == ConnectionState.Closed) ? Strings.EntityClient_ConnectionStateClosed : Strings.EntityClient_ConnectionStateBroken);
				throw EntityUtil.InvalidOperation(error);
			}
			try
			{
				this.EnsureMetadata();
				Transaction transaction = Transaction.Current;
				bool flag = (null != transaction && !transaction.Equals(this._lastTransaction)) || (null != this._lastTransaction && !this._lastTransaction.Equals(transaction));
				if (flag)
				{
					if (!this._openedConnection)
					{
						if (transaction != null)
						{
							this._connection.EnlistTransaction(transaction);
						}
					}
					else if (this._connectionRequestCount > 1)
					{
						if (null == this._lastTransaction)
						{
							this._connection.EnlistTransaction(transaction);
						}
						else
						{
							this._connection.Close();
							this._connection.Open();
							this._openedConnection = true;
							this._connectionRequestCount++;
						}
					}
				}
				this._lastTransaction = transaction;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0004DB60 File Offset: 0x0004BD60
		private void ConnectionStateChange(object sender, StateChangeEventArgs e)
		{
			if (e.CurrentState == ConnectionState.Closed)
			{
				this._connectionRequestCount = 0;
				this._openedConnection = false;
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0004DB78 File Offset: 0x0004BD78
		internal void ReleaseConnection()
		{
			if (this._connection == null)
			{
				throw EntityUtil.ObjectContextDisposed();
			}
			if (this._openedConnection)
			{
				if (this._connectionRequestCount > 0)
				{
					this._connectionRequestCount--;
				}
				if (this._connectionRequestCount == 0)
				{
					this.Connection.Close();
					this._openedConnection = false;
				}
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0004DBCC File Offset: 0x0004BDCC
		internal void EnsureMetadata()
		{
			if (!this.MetadataWorkspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
			{
				if (this._connection == null)
				{
					throw EntityUtil.ObjectContextDisposed();
				}
				MetadataWorkspace metadataWorkspace = this._connection.GetMetadataWorkspace();
				ItemCollection itemCollection = metadataWorkspace.GetItemCollection(DataSpace.CSpace);
				ItemCollection itemCollection2 = this.MetadataWorkspace.GetItemCollection(DataSpace.CSpace);
				if (itemCollection != itemCollection2)
				{
					throw EntityUtil.ContextMetadataHasChanged();
				}
				this.MetadataWorkspace.RegisterItemCollection(metadataWorkspace.GetItemCollection(DataSpace.SSpace));
				this.MetadataWorkspace.RegisterItemCollection(metadataWorkspace.GetItemCollection(DataSpace.CSSpace));
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0004DC44 File Offset: 0x0004BE44
		public ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(queryString, "queryString");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
			ObjectQuery<T> objectQuery = new ObjectQuery<T>(queryString, this, MergeOption.AppendOnly);
			foreach (ObjectParameter parameter in parameters)
			{
				objectQuery.Parameters.Add(parameter);
			}
			return objectQuery;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0004DCB0 File Offset: 0x0004BEB0
		private static EntityConnection CreateEntityConnection(string connectionString)
		{
			EntityUtil.CheckStringArgument(connectionString, "connectionString");
			return new EntityConnection(connectionString);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0004DCD0 File Offset: 0x0004BED0
		private MetadataWorkspace RetrieveMetadataWorkspaceFromConnection()
		{
			if (this._connection == null)
			{
				throw EntityUtil.ObjectContextDisposed();
			}
			MetadataWorkspace metadataWorkspace = this._connection.GetMetadataWorkspace(false);
			return metadataWorkspace.ShallowCopy();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0004DD00 File Offset: 0x0004BF00
		public void DeleteObject(object entity)
		{
			this.DeleteObject(entity, null);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		internal void DeleteObject(object entity, EntitySet expectedEntitySet)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || entityEntry.Entity != entity)
			{
				throw EntityUtil.CannotDeleteEntityNotInObjectStateManager();
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw EntityUtil.EntityNotInObjectSet_Delete(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name);
				}
			}
			entityEntry.Delete();
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0004DD80 File Offset: 0x0004BF80
		public void Detach(object entity)
		{
			this.Detach(entity, null);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0004DD8C File Offset: 0x0004BF8C
		internal void Detach(object entity, EntitySet expectedEntitySet)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || entityEntry.Entity != entity || entityEntry.Entity == null)
			{
				throw EntityUtil.CannotDetachEntityNotInObjectStateManager();
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw EntityUtil.EntityNotInObjectSet_Detach(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name);
				}
			}
			entityEntry.Detach();
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0004DE08 File Offset: 0x0004C008
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0004DE18 File Offset: 0x0004C018
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._connection != null)
				{
					this._connection.StateChange -= this.ConnectionStateChange;
					if (this._createdConnection)
					{
						this._connection.Dispose();
					}
				}
				this._connection = null;
				this._adapter = null;
				if (this._cache != null)
				{
					this._cache.Dispose();
				}
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0004DE7C File Offset: 0x0004C07C
		internal EntitySet GetEntitySet(string entitySetName, string entityContainerName)
		{
			EntityContainer entityContainer = null;
			if (string.IsNullOrEmpty(entityContainerName))
			{
				entityContainer = this.Perspective.GetDefaultContainer();
			}
			else if (!this.MetadataWorkspace.TryGetEntityContainer(entityContainerName, DataSpace.CSpace, out entityContainer))
			{
				throw EntityUtil.EntityContainterNotFoundForName(entityContainerName);
			}
			EntitySet result = null;
			if (!entityContainer.TryGetEntitySetByName(entitySetName, false, out result))
			{
				throw EntityUtil.EntitySetNotFoundForName(TypeHelpers.GetFullName(entityContainer.Name, entitySetName));
			}
			return result;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0004DEDC File Offset: 0x0004C0DC
		private static void GetEntitySetName(string qualifiedName, string parameterName, ObjectContext context, out string entityset, out string container)
		{
			entityset = null;
			container = null;
			EntityUtil.CheckStringArgument(qualifiedName, parameterName);
			string[] array = qualifiedName.Split(new char[]
			{
				'.'
			});
			if (array.Length > 2)
			{
				throw EntityUtil.QualfiedEntitySetName(parameterName);
			}
			if (array.Length == 1)
			{
				entityset = array[0];
			}
			else
			{
				container = array[0];
				entityset = array[1];
				if (container == null || container.Length == 0)
				{
					throw EntityUtil.QualfiedEntitySetName(parameterName);
				}
			}
			if (entityset == null || entityset.Length == 0)
			{
				throw EntityUtil.QualfiedEntitySetName(parameterName);
			}
			if (context != null && string.IsNullOrEmpty(container) && context.Perspective.GetDefaultContainer() == null)
			{
				throw EntityUtil.ContainerQualifiedEntitySetNameRequired(parameterName);
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0004DF7C File Offset: 0x0004C17C
		private void ValidateEntitySet(EntitySet entitySet, Type entityType)
		{
			TypeUsage typeUsage = this.GetTypeUsage(entityType);
			if (!entitySet.ElementType.IsAssignableFrom(typeUsage.EdmType))
			{
				throw EntityUtil.InvalidEntitySetOnEntity(entitySet.Name, entityType, "entity");
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0004DFB8 File Offset: 0x0004C1B8
		internal TypeUsage GetTypeUsage(Type entityCLRType)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityCLRType, Assembly.GetCallingAssembly());
			TypeUsage typeUsage = null;
			if (!this.Perspective.TryGetType(entityCLRType, out typeUsage) || !TypeSemantics.IsEntityType(typeUsage))
			{
				throw EntityUtil.InvalidEntityType(entityCLRType);
			}
			return typeUsage;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0004DFF8 File Offset: 0x0004C1F8
		public object GetObjectByKey(EntityKey key)
		{
			EntityUtil.CheckArgumentNull<EntityKey>(key, "key");
			EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
			this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
			object result;
			if (!this.TryGetObjectByKey(key, out result))
			{
				throw EntityUtil.ObjectNotFound();
			}
			return result;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0004E048 File Offset: 0x0004C248
		public void Refresh(RefreshMode refreshMode, IEnumerable collection)
		{
			try
			{
				EntityUtil.CheckArgumentRefreshMode(refreshMode);
				EntityUtil.CheckArgumentNull<IEnumerable>(collection, "collection");
				this.RefreshEntities(refreshMode, collection);
			}
			finally
			{
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0004E084 File Offset: 0x0004C284
		public void Refresh(RefreshMode refreshMode, object entity)
		{
			try
			{
				EntityUtil.CheckArgumentRefreshMode(refreshMode);
				EntityUtil.CheckArgumentNull<object>(entity, "entity");
				this.RefreshEntities(refreshMode, new object[]
				{
					entity
				});
			}
			finally
			{
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
		private void RefreshCheck(Dictionary<EntityKey, EntityEntry> entities, object entity, EntityKey key)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(key);
			if (entityEntry == null)
			{
				throw EntityUtil.NthElementNotInObjectStateManager(entities.Count);
			}
			if (EntityState.Added == entityEntry.State)
			{
				throw EntityUtil.NthElementInAddedState(entities.Count);
			}
			try
			{
				entities.Add(key, entityEntry);
			}
			catch (ArgumentException)
			{
				throw EntityUtil.NthElementIsDuplicate(entities.Count);
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0004E130 File Offset: 0x0004C330
		private void RefreshEntities(RefreshMode refreshMode, IEnumerable collection)
		{
			bool flag = false;
			try
			{
				Dictionary<EntityKey, EntityEntry> dictionary = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
				Dictionary<EntitySet, List<EntityKey>> dictionary2 = new Dictionary<EntitySet, List<EntityKey>>();
				foreach (object entityLike in collection)
				{
					this.AddRefreshKey(entityLike, dictionary, dictionary2);
				}
				collection = null;
				if (dictionary2.Count > 0)
				{
					this.EnsureConnection();
					flag = true;
					foreach (EntitySet entitySet in dictionary2.Keys)
					{
						List<EntityKey> list = dictionary2[entitySet];
						for (int i = 0; i < list.Count; i = this.BatchRefreshEntitiesByKey(refreshMode, dictionary, entitySet, list, i))
						{
						}
					}
				}
				dictionary2 = null;
				if (RefreshMode.StoreWins == refreshMode)
				{
					using (Dictionary<EntityKey, EntityEntry>.Enumerator enumerator3 = dictionary.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<EntityKey, EntityEntry> keyValuePair = enumerator3.Current;
							if (EntityState.Detached != keyValuePair.Value.State)
							{
								this.ObjectStateManager.TransactionManager.BeginDetaching();
								try
								{
									keyValuePair.Value.Delete();
								}
								finally
								{
									this.ObjectStateManager.TransactionManager.EndDetaching();
								}
								keyValuePair.Value.AcceptChanges();
							}
						}
						return;
					}
				}
				if (RefreshMode.ClientWins == refreshMode && 0 < dictionary.Count)
				{
					string value = string.Empty;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair2 in dictionary)
					{
						if (keyValuePair2.Value.State == EntityState.Deleted)
						{
							keyValuePair2.Value.AcceptChanges();
						}
						else
						{
							stringBuilder.Append(value).Append(Environment.NewLine);
							stringBuilder.Append('\'').Append(keyValuePair2.Key.ConcatKeyValue()).Append('\'');
							value = ",";
						}
					}
					if (stringBuilder.Length > 0)
					{
						throw EntityUtil.ClientEntityRemovedFromStore(stringBuilder.ToString());
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.ReleaseConnection();
				}
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0004E3D8 File Offset: 0x0004C5D8
		private int BatchRefreshEntitiesByKey(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
		{
			DbExpressionBinding dbExpressionBinding = targetSet.Scan().BindAs("EntitySet");
			DbExpression refKey = dbExpressionBinding.Variable.GetEntityRef().GetRefKey();
			int num = Math.Min(250, targetKeys.Count - startFrom);
			DbExpression[] array = new DbExpression[num];
			for (int i = 0; i < num; i++)
			{
				KeyValuePair<string, DbExpression>[] keyValueExpressions = targetKeys[startFrom++].GetKeyValueExpressions(targetSet);
				DbExpression right = DbExpressionBuilder.NewRow(keyValueExpressions);
				array[i] = refKey.Equal(right);
			}
			DbExpression predicate = Helpers.BuildBalancedTreeInPlace<DbExpression>(array, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
			DbExpression query = dbExpressionBinding.Filter(predicate);
			DbQueryCommandTree query2 = DbQueryCommandTree.FromValidExpression(this.MetadataWorkspace, DataSpace.CSpace, query);
			MergeOption mergeOption = (RefreshMode.StoreWins == refreshMode) ? MergeOption.OverwriteChanges : MergeOption.PreserveChanges;
			this.EnsureConnection();
			try
			{
				ObjectResult<object> objectResult = ObjectQueryExecutionPlan.ExecuteCommandTree<object>(this, query2, mergeOption);
				foreach (object entity in objectResult)
				{
					EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
					if (entityEntry != null && EntityState.Modified == entityEntry.State)
					{
						entityEntry.SetModifiedAll();
					}
					IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(entity, this);
					EntityKey entityKey = entityWrapper.EntityKey;
					EntityUtil.CheckEntityKeyNull(entityKey);
					if (!trackedEntities.Remove(entityKey))
					{
						throw EntityUtil.StoreEntityNotPresentInClient();
					}
				}
			}
			catch
			{
				this.ReleaseConnection();
				throw;
			}
			return startFrom;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0004E54C File Offset: 0x0004C74C
		private static int RefreshEntitiesSize(IEnumerable collection)
		{
			ICollection collection2 = collection as ICollection;
			if (collection2 == null)
			{
				return 0;
			}
			return collection2.Count;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0004E56B File Offset: 0x0004C76B
		public int SaveChanges()
		{
			return this.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0004E574 File Offset: 0x0004C774
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Use SaveChanges(SaveOptions options) instead.")]
		public int SaveChanges(bool acceptChangesDuringSave)
		{
			return this.SaveChanges(acceptChangesDuringSave ? (SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave) : SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0004E584 File Offset: 0x0004C784
		public virtual int SaveChanges(SaveOptions options)
		{
			this.OnSavingChanges();
			if ((SaveOptions.DetectChangesBeforeSave & options) != SaveOptions.None)
			{
				this.ObjectStateManager.DetectChanges();
			}
			if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			bool flag = false;
			int num = this.ObjectStateManager.GetObjectStateEntriesCount(EntityState.Added | EntityState.Deleted | EntityState.Modified);
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			if (0 < num)
			{
				if (this._adapter == null)
				{
					IServiceProvider serviceProvider = DbProviderFactories.GetFactory(entityConnection) as IServiceProvider;
					if (serviceProvider != null)
					{
						this._adapter = (serviceProvider.GetService(typeof(IEntityAdapter)) as IEntityAdapter);
					}
					if (this._adapter == null)
					{
						throw EntityUtil.InvalidDataAdapter();
					}
				}
				this._adapter.AcceptChangesDuringUpdate = false;
				this._adapter.Connection = entityConnection;
				this._adapter.CommandTimeout = this.CommandTimeout;
				try
				{
					this.EnsureConnection();
					flag = true;
					bool flag2 = false;
					if (entityConnection.CurrentTransaction == null && !entityConnection.EnlistedInUserTransaction)
					{
						flag2 = (null == this._lastTransaction);
					}
					DbTransaction dbTransaction = null;
					try
					{
						if (flag2)
						{
							dbTransaction = entityConnection.BeginTransaction();
						}
						num = this._adapter.Update(this.ObjectStateManager);
						if (dbTransaction != null)
						{
							dbTransaction.Commit();
						}
					}
					finally
					{
						if (dbTransaction != null)
						{
							dbTransaction.Dispose();
						}
					}
				}
				finally
				{
					if (flag)
					{
						this.ReleaseConnection();
					}
				}
				if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
				{
					try
					{
						this.AcceptAllChanges();
					}
					catch (Exception e)
					{
						if (EntityUtil.IsCatchableExceptionType(e))
						{
							throw EntityUtil.AcceptAllChangesFailure(e);
						}
						throw;
					}
				}
			}
			return num;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0004E704 File Offset: 0x0004C904
		public void DetectChanges()
		{
			this.ObjectStateManager.DetectChanges();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0004E714 File Offset: 0x0004C914
		public bool TryGetObjectByKey(EntityKey key, out object value)
		{
			EntityEntry entityEntry;
			this.ObjectStateManager.TryGetEntityEntry(key, out entityEntry);
			if (entityEntry != null && !entityEntry.IsKeyEntry)
			{
				value = entityEntry.Entity;
				return value != null;
			}
			if (key.IsTemporary)
			{
				value = null;
				return false;
			}
			EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
			key.ValidateEntityKey(this._workspace, entitySet, true, "key");
			this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT VALUE X FROM {0}.{1} AS X WHERE ", EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name), EntityUtil.QuoteIdentifier(entitySet.Name));
			EntityKeyMember[] entityKeyValues = key.EntityKeyValues;
			ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
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
				stringBuilder.AppendFormat("X.{0} = @{1}", EntityUtil.QuoteIdentifier(entityKeyValues[i].Key), text);
				array[i] = new ObjectParameter(text, entityKeyValues[i].Value);
				EdmMember edmMember = null;
				if (keyMembers.TryGetValue(entityKeyValues[i].Key, true, out edmMember))
				{
					array[i].TypeUsage = edmMember.TypeUsage;
				}
			}
			object obj = null;
			ObjectResult<object> objectResult = this.CreateQuery<object>(stringBuilder.ToString(), array).Execute(MergeOption.AppendOnly);
			foreach (object obj2 in objectResult)
			{
				obj = obj2;
			}
			value = obj;
			return value != null;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0004E8E4 File Offset: 0x0004CAE4
		public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
		{
			return this.ExecuteFunction<TElement>(functionName, MergeOption.AppendOnly, parameters);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0004E8F0 File Offset: 0x0004CAF0
		public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckStringArgument(functionName, "function");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			EdmFunction edmFunction;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out edmFunction, parameters);
			int num = Math.Max(1, edmFunction.ReturnParameters.Count);
			EdmType[] array = new EdmType[num];
			array[0] = MetadataHelper.GetAndCheckFunctionImportReturnType<TElement>(edmFunction, 0, this.MetadataWorkspace);
			for (int i = 1; i < num; i++)
			{
				if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(edmFunction, i, out array[i]))
				{
					throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(edmFunction);
				}
			}
			return this.CreateFunctionObjectResult<TElement>(entityCommand, edmFunction.EntitySets, array, mergeOption);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0004E980 File Offset: 0x0004CB80
		public int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckStringArgument(functionName, "function");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			EdmFunction edmFunction;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out edmFunction, parameters);
			this.EnsureConnection();
			entityCommand.Prepare();
			int result;
			try
			{
				result = entityCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableEntityExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			finally
			{
				this.ReleaseConnection();
			}
			return result;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0004EA00 File Offset: 0x0004CC00
		private EntityCommand CreateEntityCommandForFunctionImport(string functionName, out EdmFunction functionImport, params ObjectParameter[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] == null)
				{
					throw EntityUtil.InvalidOperation(Strings.ObjectContext_ExecuteFunctionCalledWithNullParameter(i));
				}
			}
			string str;
			string str2;
			functionImport = MetadataHelper.GetFunctionImport(functionName, this.DefaultContainerName, this.MetadataWorkspace, out str, out str2);
			EntityConnection connection = (EntityConnection)this.Connection;
			EntityCommand entityCommand = new EntityCommand();
			entityCommand.CommandType = CommandType.StoredProcedure;
			entityCommand.CommandText = str + "." + str2;
			entityCommand.Connection = connection;
			if (this.CommandTimeout != null)
			{
				entityCommand.CommandTimeout = this.CommandTimeout.Value;
			}
			this.PopulateFunctionImportEntityCommandParameters(parameters, functionImport, entityCommand);
			return entityCommand;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0004EAB8 File Offset: 0x0004CCB8
		private ObjectResult<TElement> CreateFunctionObjectResult<TElement>(EntityCommand entityCommand, ReadOnlyMetadataCollection<EntitySet> entitySets, EdmType[] edmTypes, MergeOption mergeOption)
		{
			this.EnsureConnection();
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			DbDataReader storeReader;
			try
			{
				storeReader = commandDefinition.ExecuteStoreCommands(entityCommand, CommandBehavior.Default);
			}
			catch (Exception ex)
			{
				this.ReleaseConnection();
				if (EntityUtil.IsCatchableEntityExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			return this.MaterializedDataRecord<TElement>(entityCommand, storeReader, 0, entitySets, edmTypes, mergeOption);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0004EB18 File Offset: 0x0004CD18
		internal ObjectResult<TElement> MaterializedDataRecord<TElement>(EntityCommand entityCommand, DbDataReader storeReader, int resultSetIndex, ReadOnlyMetadataCollection<EntitySet> entitySets, EdmType[] edmTypes, MergeOption mergeOption)
		{
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			ObjectResult<TElement> result;
			try
			{
				bool flag = edmTypes.Length <= resultSetIndex + 1;
				EdmType edmType = edmTypes[resultSetIndex];
				EntitySet singleEntitySet = (entitySets.Count > resultSetIndex) ? entitySets[resultSetIndex] : null;
				QueryCacheManager queryCacheManager = this.Perspective.MetadataWorkspace.GetQueryCacheManager();
				ShaperFactory<TElement> shaperFactory = Translator.TranslateColumnMap<TElement>(queryCacheManager, commandDefinition.CreateColumnMap(storeReader, resultSetIndex), this.MetadataWorkspace, null, mergeOption, false);
				Shaper<TElement> shaper = shaperFactory.Create(storeReader, this, this.MetadataWorkspace, mergeOption, flag);
				bool onReaderDisposeHasRun = false;
				Action<object, EventArgs> action = delegate(object sender, EventArgs e)
				{
					if (!onReaderDisposeHasRun)
					{
						onReaderDisposeHasRun = true;
						CommandHelper.ConsumeReader(storeReader);
						entityCommand.NotifyDataReaderClosing();
					}
				};
				NextResultGenerator nextResultGenerator;
				if (flag)
				{
					shaper.OnDone += action.Invoke;
					nextResultGenerator = null;
				}
				else
				{
					nextResultGenerator = new NextResultGenerator(this, entityCommand, edmTypes, entitySets, mergeOption, resultSetIndex + 1);
				}
				result = new ObjectResult<TElement>(shaper, singleEntitySet, TypeUsage.Create(edmTypes[resultSetIndex]), true, nextResultGenerator, action);
			}
			catch
			{
				this.ReleaseConnection();
				storeReader.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0004EC44 File Offset: 0x0004CE44
		private void PopulateFunctionImportEntityCommandParameters(ObjectParameter[] parameters, EdmFunction functionImport, EntityCommand command)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				ObjectParameter objectParameter = parameters[i];
				EntityParameter entityParameter = new EntityParameter();
				FunctionParameter functionParameter = ObjectContext.FindParameterMetadata(functionImport, parameters, i);
				if (functionParameter != null)
				{
					entityParameter.Direction = MetadataHelper.ParameterModeToParameterDirection(functionParameter.Mode);
					entityParameter.ParameterName = functionParameter.Name;
				}
				else
				{
					entityParameter.ParameterName = objectParameter.Name;
				}
				entityParameter.Value = (objectParameter.Value ?? DBNull.Value);
				if (DBNull.Value == entityParameter.Value || entityParameter.Direction != ParameterDirection.Input)
				{
					TypeUsage typeUsage;
					if (functionParameter != null)
					{
						typeUsage = functionParameter.TypeUsage;
					}
					else if (objectParameter.TypeUsage == null)
					{
						if (!this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullName, false, out typeUsage))
						{
							this.MetadataWorkspace.ImplicitLoadAssemblyForType(objectParameter.MappableType, null);
							this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullName, false, out typeUsage);
						}
					}
					else
					{
						typeUsage = objectParameter.TypeUsage;
					}
					EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, typeUsage, entityParameter.Direction != ParameterDirection.Input);
				}
				if (entityParameter.Direction != ParameterDirection.Input)
				{
					ObjectContext.ParameterBinder @object = new ObjectContext.ParameterBinder(entityParameter, objectParameter);
					command.OnDataReaderClosing += @object.OnDataReaderClosingHandler;
				}
				command.Parameters.Add(entityParameter);
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0004ED80 File Offset: 0x0004CF80
		private static FunctionParameter FindParameterMetadata(EdmFunction functionImport, ObjectParameter[] parameters, int ordinal)
		{
			string name = parameters[ordinal].Name;
			FunctionParameter result;
			if (!functionImport.Parameters.TryGetValue(name, false, out result))
			{
				int num = 0;
				int num2 = 0;
				while (num2 < parameters.Length && num < 2)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(parameters[num2].Name, name))
					{
						num++;
					}
					num2++;
				}
				if (num == 1)
				{
					functionImport.Parameters.TryGetValue(name, true, out result);
				}
			}
			return result;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0004EDEC File Offset: 0x0004CFEC
		public void CreateProxyTypes(IEnumerable<Type> types)
		{
			ObjectItemCollection ospaceItems = (ObjectItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			EntityProxyFactory.TryCreateProxyTypes(from entityType in types.Select(delegate(Type type)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(type, null);
				EntityType result;
				ospaceItems.TryGetItem<EntityType>(type.FullName, out result);
				return result;
			})
			where entityType != null
			select entityType);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0004EE58 File Offset: 0x0004D058
		public static IEnumerable<Type> GetKnownProxyTypes()
		{
			return EntityProxyFactory.GetKnownProxyTypes();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0004EE5F File Offset: 0x0004D05F
		public static Type GetObjectType(Type type)
		{
			EntityUtil.CheckArgumentNull<Type>(type, "type");
			if (!EntityProxyFactory.IsProxyType(type))
			{
				return type;
			}
			return type.BaseType;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004EE80 File Offset: 0x0004D080
		public T CreateObject<T>() where T : class
		{
			T t = default(T);
			Type typeFromHandle = typeof(T);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeFromHandle, null);
			ClrEntityType item = this.MetadataWorkspace.GetItem<ClrEntityType>(typeFromHandle.FullName, DataSpace.OSpace);
			EntityProxyTypeInfo proxyType;
			if (this.ContextOptions.ProxyCreationEnabled && (proxyType = EntityProxyFactory.GetProxyType(item)) != null)
			{
				t = (T)((object)proxyType.CreateProxyObject());
				IEntityWrapper entityWrapper = EntityWrapperFactory.CreateNewWrapper(t, null);
				entityWrapper.InitializingProxyRelatedEnds = true;
				try
				{
					entityWrapper.AttachContext(this, null, MergeOption.NoTracking);
					proxyType.SetEntityWrapper(entityWrapper);
					if (proxyType.InitializeEntityCollections != null)
					{
						proxyType.InitializeEntityCollections.Invoke(null, new object[]
						{
							entityWrapper
						});
					}
					return t;
				}
				finally
				{
					entityWrapper.InitializingProxyRelatedEnds = false;
					entityWrapper.DetachContext();
				}
			}
			Func<object> func = LightweightCodeGenerator.GetConstructorDelegateForType(item) as Func<object>;
			t = (func() as T);
			return t;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0004EF78 File Offset: 0x0004D178
		public int ExecuteStoreCommand(string commandText, params object[] parameters)
		{
			this.EnsureConnection();
			int result;
			try
			{
				DbCommand dbCommand = this.CreateStoreCommand(commandText, parameters);
				result = dbCommand.ExecuteNonQuery();
			}
			finally
			{
				this.ReleaseConnection();
			}
			return result;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0004EFB8 File Offset: 0x0004D1B8
		public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreQueryInternal<TElement>(commandText, null, MergeOption.AppendOnly, parameters);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
		public ObjectResult<TEntity> ExecuteStoreQuery<TEntity>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			return this.ExecuteStoreQueryInternal<TEntity>(commandText, entitySetName, mergeOption, parameters);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0004EFDC File Offset: 0x0004D1DC
		private ObjectResult<TElement> ExecuteStoreQueryInternal<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			this.EnsureConnection();
			DbDataReader dbDataReader = null;
			try
			{
				DbCommand dbCommand = this.CreateStoreCommand(commandText, parameters);
				dbDataReader = dbCommand.ExecuteReader();
			}
			catch
			{
				this.ReleaseConnection();
				throw;
			}
			ObjectResult<TElement> result;
			try
			{
				result = this.InternalTranslate<TElement>(dbDataReader, entitySetName, mergeOption, true);
			}
			catch
			{
				dbDataReader.Dispose();
				this.ReleaseConnection();
				throw;
			}
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0004F064 File Offset: 0x0004D264
		public ObjectResult<TElement> Translate<TElement>(DbDataReader reader)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			return this.InternalTranslate<TElement>(reader, null, MergeOption.AppendOnly, false);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0004F08A File Offset: 0x0004D28A
		public ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption)
		{
			EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TEntity), Assembly.GetCallingAssembly());
			return this.InternalTranslate<TEntity>(reader, entitySetName, mergeOption, false);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0004F0BC File Offset: 0x0004D2BC
		private ObjectResult<TElement> InternalTranslate<TElement>(DbDataReader reader, string entitySetName, MergeOption mergeOption, bool readerOwned)
		{
			EntityUtil.CheckArgumentNull<DbDataReader>(reader, "reader");
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			EntitySet entitySet = null;
			if (!string.IsNullOrEmpty(entitySetName))
			{
				entitySet = this.GetEntitySetFromName(entitySetName);
			}
			this.EnsureMetadata();
			Type type = Nullable.GetUnderlyingType(typeof(TElement)) ?? typeof(TElement);
			EdmType edmType;
			CollectionColumnMap collectionColumnMap;
			if (MetadataHelper.TryDetermineCSpaceModelType<TElement>(this.MetadataWorkspace, out edmType) || (type.IsEnum && MetadataHelper.TryDetermineCSpaceModelType(type.GetEnumUnderlyingType(), this.MetadataWorkspace, out edmType)))
			{
				if (entitySet != null && !entitySet.ElementType.IsAssignableFrom(edmType))
				{
					throw EntityUtil.InvalidOperation(Strings.ObjectContext_InvalidEntitySetForStoreQuery(entitySet.EntityContainer.Name, entitySet.Name, typeof(TElement)));
				}
				collectionColumnMap = ColumnMapFactory.CreateColumnMapFromReaderAndType(reader, edmType, entitySet, null);
			}
			else
			{
				collectionColumnMap = ColumnMapFactory.CreateColumnMapFromReaderAndClrType(reader, typeof(TElement), this.MetadataWorkspace);
			}
			QueryCacheManager queryCacheManager = this.MetadataWorkspace.GetQueryCacheManager();
			ShaperFactory<TElement> shaperFactory = Translator.TranslateColumnMap<TElement>(queryCacheManager, collectionColumnMap, this.MetadataWorkspace, null, mergeOption, false);
			Shaper<TElement> shaper = shaperFactory.Create(reader, this, this.MetadataWorkspace, mergeOption, readerOwned);
			return new ObjectResult<TElement>(shaper, entitySet, MetadataHelper.GetElementType(collectionColumnMap.Type), readerOwned);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
		private DbCommand CreateStoreCommand(string commandText, params object[] parameters)
		{
			DbCommand dbCommand = this._connection.StoreConnection.CreateCommand();
			dbCommand.CommandText = commandText;
			if (this.CommandTimeout != null)
			{
				dbCommand.CommandTimeout = this.CommandTimeout.Value;
			}
			EntityTransaction currentTransaction = this._connection.CurrentTransaction;
			if (currentTransaction != null)
			{
				dbCommand.Transaction = currentTransaction.StoreTransaction;
			}
			if (parameters != null && parameters.Length != 0)
			{
				DbParameter[] array = new DbParameter[parameters.Length];
				if (parameters.All((object p) => p is DbParameter))
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						array[i] = (DbParameter)parameters[i];
					}
				}
				else
				{
					if (parameters.Any((object p) => p is DbParameter))
					{
						throw EntityUtil.InvalidOperation(Strings.ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues);
					}
					string[] array2 = new string[parameters.Length];
					string[] array3 = new string[parameters.Length];
					for (int j = 0; j < parameters.Length; j++)
					{
						array2[j] = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[]
						{
							j
						});
						array[j] = dbCommand.CreateParameter();
						array[j].ParameterName = array2[j];
						array[j].Value = (parameters[j] ?? DBNull.Value);
						array3[j] = "@" + array2[j];
					}
					DbCommand dbCommand2 = dbCommand;
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string commandText2 = dbCommand.CommandText;
					object[] args = array3;
					dbCommand2.CommandText = string.Format(invariantCulture, commandText2, args);
				}
				dbCommand.Parameters.AddRange(array);
			}
			return dbCommand;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0004F394 File Offset: 0x0004D594
		public void CreateDatabase()
		{
			DbConnection storeConnection = this._connection.StoreConnection;
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory);
			providerServices.CreateDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		public void DeleteDatabase()
		{
			DbConnection storeConnection = this._connection.StoreConnection;
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory);
			providerServices.DeleteDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0004F414 File Offset: 0x0004D614
		public bool DatabaseExists()
		{
			DbConnection storeConnection = this._connection.StoreConnection;
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory);
			return providerServices.DatabaseExists(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004F454 File Offset: 0x0004D654
		public string CreateDatabaseScript()
		{
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory);
			string storeProviderManifestToken = this.GetStoreItemCollection().StoreProviderManifestToken;
			return providerServices.CreateDatabaseScript(storeProviderManifestToken, this.GetStoreItemCollection());
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0004F48C File Offset: 0x0004D68C
		private StoreItemCollection GetStoreItemCollection()
		{
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			return (StoreItemCollection)entityConnection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0004F4B6 File Offset: 0x0004D6B6
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0004F4BE File Offset: 0x0004D6BE
		internal CollectionColumnMap ColumnMapBuilder { get; set; }

		// Token: 0x04000A7C RID: 2684
		private IEntityAdapter _adapter;

		// Token: 0x04000A7D RID: 2685
		private EntityConnection _connection;

		// Token: 0x04000A7E RID: 2686
		private readonly MetadataWorkspace _workspace;

		// Token: 0x04000A7F RID: 2687
		private ObjectStateManager _cache;

		// Token: 0x04000A80 RID: 2688
		private ClrPerspective _perspective;

		// Token: 0x04000A81 RID: 2689
		private readonly bool _createdConnection;

		// Token: 0x04000A82 RID: 2690
		private bool _openedConnection;

		// Token: 0x04000A83 RID: 2691
		private int _connectionRequestCount;

		// Token: 0x04000A84 RID: 2692
		private int? _queryTimeout;

		// Token: 0x04000A85 RID: 2693
		private Transaction _lastTransaction;

		// Token: 0x04000A86 RID: 2694
		private bool _disallowSettingDefaultContainerName;

		// Token: 0x04000A87 RID: 2695
		private EventHandler _onSavingChanges;

		// Token: 0x04000A88 RID: 2696
		private ObjectMaterializedEventHandler _onObjectMaterialized;

		// Token: 0x04000A89 RID: 2697
		private ObjectQueryProvider _queryProvider;

		// Token: 0x04000A8A RID: 2698
		private readonly ObjectContextOptions _options = new ObjectContextOptions();

		// Token: 0x04000A8B RID: 2699
		private readonly string s_UseLegacyPreserveChangesBehavior = "EntityFramework_UseLegacyPreserveChangesBehavior";

		// Token: 0x020004A1 RID: 1185
		private class ParameterBinder
		{
			// Token: 0x06003C29 RID: 15401 RVA: 0x000E28B5 File Offset: 0x000E0AB5
			internal ParameterBinder(EntityParameter entityParameter, ObjectParameter objectParameter)
			{
				this._entityParameter = entityParameter;
				this._objectParameter = objectParameter;
			}

			// Token: 0x06003C2A RID: 15402 RVA: 0x000E28CC File Offset: 0x000E0ACC
			internal void OnDataReaderClosingHandler(object sender, EventArgs args)
			{
				if (this._entityParameter.Value != DBNull.Value && this._objectParameter.MappableType.IsEnum)
				{
					this._objectParameter.Value = Enum.ToObject(this._objectParameter.MappableType, this._entityParameter.Value);
					return;
				}
				this._objectParameter.Value = this._entityParameter.Value;
			}

			// Token: 0x04001A2D RID: 6701
			private readonly EntityParameter _entityParameter;

			// Token: 0x04001A2E RID: 6702
			private readonly ObjectParameter _objectParameter;
		}
	}
}
