using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200059F RID: 1439
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class ObjectContext : IDisposable, IObjectContextAdapter
	{
		// Token: 0x0600385F RID: 14431 RVA: 0x0010A44C File Offset: 0x0010864C
		public ObjectContext(EntityConnection connection) : this(connection, true, null, null, null)
		{
			this._contextOwnsConnection = false;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x0010A460 File Offset: 0x00108660
		public ObjectContext(EntityConnection connection, bool contextOwnsConnection) : this(connection, true, null, null, null)
		{
			this._contextOwnsConnection = contextOwnsConnection;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x0010A474 File Offset: 0x00108674
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		public ObjectContext(string connectionString) : this(ObjectContext.CreateEntityConnection(connectionString), false, null, null, null)
		{
			this._contextOwnsConnection = true;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x0010A48D File Offset: 0x0010868D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Class is internal and methods are made virtual for testing purposes only. They cannot be overrided by user.")]
		protected ObjectContext(string connectionString, string defaultContainerName) : this(connectionString)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x0010A4AC File Offset: 0x001086AC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Class is internal and methods are made virtual for testing purposes only. They cannot be overrided by user.")]
		protected ObjectContext(EntityConnection connection, string defaultContainerName) : this(connection)
		{
			this.DefaultContainerName = defaultContainerName;
			if (!string.IsNullOrEmpty(defaultContainerName))
			{
				this._disallowSettingDefaultContainerName = true;
			}
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x0010A4CC File Offset: 0x001086CC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		internal ObjectContext(EntityConnection connection, bool isConnectionConstructor, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory, Translator translator = null, ColumnMapFactory columnMapFactory = null)
		{
			this._options = new ObjectContextOptions();
			this._asyncMonitor = new ThrowingMonitor();
			base..ctor();
			Check.NotNull<EntityConnection>(connection, "connection");
			this._interceptionContext = new DbInterceptionContext().WithObjectContext(this);
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
			this._translator = (translator ?? new Translator());
			this._columnMapFactory = (columnMapFactory ?? new ColumnMapFactory());
			this._adapter = new EntityAdapter(this);
			this._connection = connection;
			this._connection.AssociateContext(this);
			this._connection.StateChange += this.ConnectionStateChange;
			this._entityWrapperFactory = new EntityWrapperFactory();
			string connectionString = connection.ConnectionString;
			if (connectionString == null || connectionString.Trim().Length == 0)
			{
				throw isConnectionConstructor ? new ArgumentException(Strings.ObjectContext_InvalidConnection, "connection", null) : new ArgumentException(Strings.ObjectContext_InvalidConnectionString, "connectionString", null);
			}
			try
			{
				this._workspace = this.RetrieveMetadataWorkspaceFromConnection();
			}
			catch (InvalidOperationException innerException)
			{
				throw isConnectionConstructor ? new ArgumentException(Strings.ObjectContext_InvalidConnection, "connection", innerException) : new ArgumentException(Strings.ObjectContext_InvalidConnectionString, "connectionString", innerException);
			}
			string value = ConfigurationManager.AppSettings["EntityFramework_UseLegacyPreserveChangesBehavior"];
			bool useLegacyPreserveChangesBehavior = false;
			if (bool.TryParse(value, out useLegacyPreserveChangesBehavior))
			{
				this.ContextOptions.UseLegacyPreserveChangesBehavior = useLegacyPreserveChangesBehavior;
			}
			this.InitializeMappingViewCacheFactory(null);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x0010A638 File Offset: 0x00108838
		internal ObjectContext(ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null, Translator translator = null, ColumnMapFactory columnMapFactory = null, IEntityAdapter adapter = null)
		{
			this._options = new ObjectContextOptions();
			this._asyncMonitor = new ThrowingMonitor();
			base..ctor();
			this._interceptionContext = new DbInterceptionContext().WithObjectContext(this);
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
			this._translator = (translator ?? new Translator());
			this._columnMapFactory = (columnMapFactory ?? new ColumnMapFactory());
			this._adapter = (adapter ?? new EntityAdapter(this));
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x0010A6B5 File Offset: 0x001088B5
		public virtual DbConnection Connection
		{
			get
			{
				if (this._connection == null)
				{
					throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
				}
				return this._connection;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x0010A6D4 File Offset: 0x001088D4
		// (set) Token: 0x06003868 RID: 14440 RVA: 0x0010A6FC File Offset: 0x001088FC
		public virtual string DefaultContainerName
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
				throw new InvalidOperationException(Strings.ObjectContext_CannotSetDefaultContainerName);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x0010A71D File Offset: 0x0010891D
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._workspace;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x0010A725 File Offset: 0x00108925
		public virtual ObjectStateManager ObjectStateManager
		{
			get
			{
				if (this._objectStateManager == null)
				{
					this._objectStateManager = new ObjectStateManager(this._workspace);
				}
				return this._objectStateManager;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (set) Token: 0x0600386B RID: 14443 RVA: 0x0010A746 File Offset: 0x00108946
		internal bool ContextOwnsConnection
		{
			set
			{
				this._contextOwnsConnection = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x0010A74F File Offset: 0x0010894F
		internal ClrPerspective Perspective
		{
			get
			{
				if (this._perspective == null)
				{
					this._perspective = new ClrPerspective(this.MetadataWorkspace);
				}
				return this._perspective;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x0010A770 File Offset: 0x00108970
		// (set) Token: 0x0600386E RID: 14446 RVA: 0x0010A778 File Offset: 0x00108978
		public virtual int? CommandTimeout
		{
			get
			{
				return this._queryTimeout;
			}
			set
			{
				if (value != null && value < 0)
				{
					throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout, "value");
				}
				this._queryTimeout = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x0010A7BD File Offset: 0x001089BD
		protected internal virtual IQueryProvider QueryProvider
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

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x0010A7D9 File Offset: 0x001089D9
		// (set) Token: 0x06003871 RID: 14449 RVA: 0x0010A7E1 File Offset: 0x001089E1
		internal bool InMaterialization { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x0010A7EA File Offset: 0x001089EA
		internal ThrowingMonitor AsyncMonitor
		{
			get
			{
				return this._asyncMonitor;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x0010A7F2 File Offset: 0x001089F2
		public virtual ObjectContextOptions ContextOptions
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x0010A7FA File Offset: 0x001089FA
		// (set) Token: 0x06003875 RID: 14453 RVA: 0x0010A802 File Offset: 0x00108A02
		internal CollectionColumnMap ColumnMapBuilder { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x0010A80B File Offset: 0x00108A0B
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x0010A813 File Offset: 0x00108A13
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		ObjectContext IObjectContextAdapter.ObjectContext
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x0010A816 File Offset: 0x00108A16
		public TransactionHandler TransactionHandler
		{
			get
			{
				this.EnsureTransactionHandlerRegistered();
				return this._transactionHandler;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x0010A824 File Offset: 0x00108A24
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x0010A82C File Offset: 0x00108A2C
		public DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
			internal set
			{
				this._interceptionContext = value;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600387B RID: 14459 RVA: 0x0010A835 File Offset: 0x00108A35
		// (remove) Token: 0x0600387C RID: 14460 RVA: 0x0010A84E File Offset: 0x00108A4E
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

		// Token: 0x0600387D RID: 14461 RVA: 0x0010A867 File Offset: 0x00108A67
		private void OnSavingChanges()
		{
			if (this._onSavingChanges != null)
			{
				this._onSavingChanges(this, new EventArgs());
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600387E RID: 14462 RVA: 0x0010A882 File Offset: 0x00108A82
		// (remove) Token: 0x0600387F RID: 14463 RVA: 0x0010A89B File Offset: 0x00108A9B
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

		// Token: 0x06003880 RID: 14464 RVA: 0x0010A8B4 File Offset: 0x00108AB4
		internal void OnObjectMaterialized(object entity)
		{
			if (this._onObjectMaterialized != null)
			{
				this._onObjectMaterialized(this, new ObjectMaterializedEventArgs(entity));
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x0010A8D0 File Offset: 0x00108AD0
		internal bool OnMaterializedHasHandlers
		{
			get
			{
				return this._onObjectMaterialized != null && this._onObjectMaterialized.GetInvocationList().Length != 0;
			}
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x0010A8F0 File Offset: 0x00108AF0
		public virtual void AcceptAllChanges()
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

		// Token: 0x06003883 RID: 14467 RVA: 0x0010A99C File Offset: 0x00108B9C
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
				throw new InvalidOperationException(Strings.ObjectContext_EntitySetNameOrEntityKeyRequired);
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
			if (existingEntry != null && (!doAttach || !existingEntry.IsKeyEntry))
			{
				if (!object.ReferenceEquals(existingEntry.Entity, wrappedEntity.Entity))
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedEntity.IdentityType.FullName));
				}
				EntityState entityState = doAttach ? EntityState.Unchanged : EntityState.Added;
				if (existingEntry.State != entityState)
				{
					throw doAttach ? new InvalidOperationException(Strings.ObjectContext_EntityAlreadyExistsInObjectStateManager) : new InvalidOperationException(Strings.ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(existingEntry.State));
				}
				isNoOperation = true;
			}
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x0010AADC File Offset: 0x00108CDC
		public virtual void AddObject(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
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
			System.Data.Entity.Core.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
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

		// Token: 0x06003885 RID: 14469 RVA: 0x0010ABD4 File Offset: 0x00108DD4
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
			this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
			wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x0010AC58 File Offset: 0x00108E58
		public virtual void LoadProperty(object entity, string navigationProperty)
		{
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, false).Load();
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x0010AC84 File Offset: 0x00108E84
		public virtual void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
		{
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, false).Load(mergeOption);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x0010ACB4 File Offset: 0x00108EB4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector)
		{
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out throwArgumentException);
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException).Load();
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x0010ACF0 File Offset: 0x00108EF0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
		{
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out throwArgumentException);
			IEntityWrapper entityWrapper = this.WrapEntityAndCheckContext(entity, "property");
			entityWrapper.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException).Load(mergeOption);
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0010AD2C File Offset: 0x00108F2C
		private IEntityWrapper WrapEntityAndCheckContext(object entity, string refType)
		{
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(entity, this);
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

		// Token: 0x0600388B RID: 14475 RVA: 0x0010AD74 File Offset: 0x00108F74
		internal static string ParsePropertySelectorExpression<TEntity>(Expression<Func<TEntity, object>> selector, out bool removedConvert)
		{
			Check.NotNull<Expression<Func<TEntity, object>>>(selector, "selector");
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

		// Token: 0x0600388C RID: 14476 RVA: 0x0010AE07 File Offset: 0x00109007
		[Obsolete("Use ApplyCurrentValues instead")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public virtual void ApplyPropertyChanges(string entitySetName, object changed)
		{
			Check.NotNull<object>(changed, "changed");
			Check.NotEmpty(entitySetName, "entitySetName");
			this.ApplyCurrentValues<object>(entitySetName, changed);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0010AE2C File Offset: 0x0010902C
		public virtual TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
		{
			Check.NotNull<TEntity>(currentEntity, "currentEntity");
			Check.NotEmpty(entitySetName, "entitySetName");
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(currentEntity, this);
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
				throw new InvalidOperationException(Strings.ObjectStateManager_EntityNotTracked);
			}
			entityEntry.ApplyCurrentValuesInternal(entityWrapper);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0010AEEC File Offset: 0x001090EC
		public virtual TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
		{
			Check.NotNull<TEntity>(originalEntity, "originalEntity");
			Check.NotEmpty(entitySetName, "entitySetName");
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(originalEntity, this);
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
				throw new InvalidOperationException(Strings.ObjectContext_EntityNotTrackedOrHasTempKey);
			}
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged && entityEntry.State != EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(entityEntry.State.ToString()));
			}
			if (entityEntry.WrappedEntity.IdentityType != entityWrapper.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(entityEntry.Entity.GetType().FullName, originalEntity.GetType().FullName));
			}
			entityEntry.CompareKeyProperties(originalEntity);
			entityEntry.UpdateOriginalValues(entityWrapper.Entity);
			return (TEntity)((object)entityEntry.Entity);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0010B038 File Offset: 0x00109238
		public virtual void AttachTo(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityEntry entityEntry;
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out entityEntry);
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
			System.Data.Entity.Core.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
			transactionManager.BeginAttachTracking();
			try
			{
				this.ObjectStateManager.TransactionManager.OriginalMergeOption = new MergeOption?(entityWrapper.MergeOption);
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				bool flag2 = true;
				try
				{
					this.AttachSingleObject(entityWrapper, entitySet);
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

		// Token: 0x06003890 RID: 14480 RVA: 0x0010B120 File Offset: 0x00109320
		public virtual void Attach(IEntityWithKey entity)
		{
			Check.NotNull<IEntityWithKey>(entity, "entity");
			if (entity.EntityKey == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotAttachEntityWithoutKey);
			}
			this.AttachTo(null, entity);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0010B14C File Offset: 0x0010934C
		internal void AttachSingleObject(IEntityWrapper wrappedEntity, EntitySet entitySet)
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
				throw new InvalidOperationException(Strings.ObjectContext_CannotAttachEntityWithTemporaryKey);
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
				entityEntry = this.ObjectStateManager.AttachEntry(entityKey, wrappedEntity, entitySet);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			if (entityEntry.IsKeyEntry)
			{
				this.ObjectStateManager.PromoteKeyEntryInitialization(this, entityEntry, wrappedEntity, false);
				this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
				wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
				this.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, false, false, true);
				this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry, false);
				relationshipManager.CheckReferentialConstraintProperties(entityEntry);
				return;
			}
			throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedEntity.IdentityType.FullName));
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x0010B293 File Offset: 0x00109493
		private void VerifyContextForAddOrAttach(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.Context != this && !wrappedEntity.Context.ObjectStateManager.IsDisposed && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.Entity_EntityCantHaveMultipleChangeTrackers);
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x0010B2CC File Offset: 0x001094CC
		public virtual EntityKey CreateEntityKey(string entitySetName, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			Check.NotEmpty(entitySetName, "entitySetName");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(EntityUtil.GetEntityIdentityType(entity.GetType()), null);
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			return this.ObjectStateManager.CreateEntityKey(entitySetFromName, entity);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x0010B320 File Offset: 0x00109520
		internal EntitySet GetEntitySetFromName(string entitySetName)
		{
			string entitySetName2;
			string entityContainerName;
			ObjectContext.GetEntitySetName(entitySetName, "entitySetName", this, out entitySetName2, out entityContainerName);
			return this.GetEntitySet(entitySetName2, entityContainerName);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x0010B348 File Offset: 0x00109548
		private void AddRefreshKey(object entityLike, Dictionary<EntityKey, EntityEntry> entities, Dictionary<EntitySet, List<EntityKey>> currentKeys)
		{
			if (entityLike == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementIsNull(entities.Count));
			}
			IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(entityLike, this);
			EntityKey entityKey = entityWrapper.EntityKey;
			this.RefreshCheck(entities, entityKey);
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			List<EntityKey> list = null;
			if (!currentKeys.TryGetValue(entitySet, out list))
			{
				list = new List<EntityKey>();
				currentKeys.Add(entitySet, list);
			}
			list.Add(entityKey);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x0010B3BC File Offset: 0x001095BC
		public virtual ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
		{
			EntitySet entitySetForType = this.GetEntitySetForType(typeof(TEntity), "TEntity");
			return new ObjectSet<TEntity>(entitySetForType, this);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x0010B3E8 File Offset: 0x001095E8
		public virtual ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
		{
			EntitySet entitySetForNameAndType = this.GetEntitySetForNameAndType(entitySetName, typeof(TEntity), "TEntity");
			return new ObjectSet<TEntity>(entitySetForNameAndType, this);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x0010B414 File Offset: 0x00109614
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
								throw new ArgumentException(Strings.ObjectContext_MultipleEntitySetsFoundInAllContainers(entityCLRType.FullName), exceptionParameterName);
							}
							entitySet = entitySetFromContainer;
						}
					}
					goto IL_78;
				}
			}
			entitySet = this.GetEntitySetFromContainer(defaultContainer, entityCLRType, exceptionParameterName);
			IL_78:
			if (entitySet == null)
			{
				throw new ArgumentException(Strings.ObjectContext_NoEntitySetFoundForType(entityCLRType.FullName), exceptionParameterName);
			}
			return entitySet;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0010B4C0 File Offset: 0x001096C0
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
						throw new ArgumentException(Strings.ObjectContext_MultipleEntitySetsFoundInSingleContainer(entityCLRType.FullName, container.Name), exceptionParameterName);
					}
					entitySet = (EntitySet)entitySetBase;
				}
			}
			return entitySet;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x0010B554 File Offset: 0x00109754
		private EntitySet GetEntitySetForNameAndType(string entitySetName, Type entityCLRType, string exceptionParameterName)
		{
			EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
			EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
			if (entitySetFromName.ElementType != edmType)
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidObjectSetTypeForEntitySet(entityCLRType.FullName, entitySetFromName.ElementType.FullName, entitySetName), exceptionParameterName);
			}
			return entitySetFromName;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0010B5AC File Offset: 0x001097AC
		internal virtual void EnsureConnection(bool shouldMonitorTransactions)
		{
			if (shouldMonitorTransactions)
			{
				this.EnsureTransactionHandlerRegistered();
			}
			if (this.Connection.State == ConnectionState.Broken)
			{
				this.Connection.Close();
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
			try
			{
				Transaction transaction = Transaction.Current;
				this.EnsureContextIsEnlistedInCurrentTransaction<bool>(transaction, delegate
				{
					this.Connection.Open();
					return true;
				}, false);
				this._lastTransaction = transaction;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x0010BA54 File Offset: 0x00109C54
		internal virtual async Task EnsureConnectionAsync(bool shouldMonitorTransactions, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (shouldMonitorTransactions)
			{
				this.EnsureTransactionHandlerRegistered();
			}
			if (this.Connection.State == ConnectionState.Broken)
			{
				this.Connection.Close();
			}
			if (this.Connection.State == ConnectionState.Closed)
			{
				await this.Connection.OpenAsync(cancellationToken).WithCurrentCulture();
				this._openedConnection = true;
			}
			if (this._openedConnection)
			{
				this._connectionRequestCount++;
			}
			try
			{
				Transaction currentTransaction = Transaction.Current;
				await this.EnsureContextIsEnlistedInCurrentTransaction<Task<bool>>(currentTransaction, async delegate
				{
					await this.Connection.OpenAsync(cancellationToken).WithCurrentCulture();
					return true;
				}, Task.FromResult<bool>(false)).WithCurrentCulture<bool>();
				this._lastTransaction = currentTransaction;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x0010BAB8 File Offset: 0x00109CB8
		private void EnsureTransactionHandlerRegistered()
		{
			if (this._transactionHandler == null)
			{
				if (!this.InterceptionContext.DbContexts.Any((DbContext dbc) => dbc is TransactionContext))
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					string name = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderFactory).Name;
					Func<TransactionHandler> service = DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(name, this.Connection.DataSource));
					if (service != null)
					{
						this._transactionHandler = service();
						this._transactionHandler.Initialize(this);
					}
				}
			}
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x0010BB60 File Offset: 0x00109D60
		private T EnsureContextIsEnlistedInCurrentTransaction<T>(Transaction currentTransaction, Func<T> openConnection, T defaultValue)
		{
			if (this.Connection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(Strings.BadConnectionWrapping);
			}
			bool flag = (null != currentTransaction && !currentTransaction.Equals(this._lastTransaction)) || (null != this._lastTransaction && !this._lastTransaction.Equals(currentTransaction));
			if (flag)
			{
				if (!this._openedConnection)
				{
					if (currentTransaction != null)
					{
						this.Connection.EnlistTransaction(currentTransaction);
					}
				}
				else if (this._connectionRequestCount > 1)
				{
					if (!(null == this._lastTransaction))
					{
						this.Connection.Close();
						return openConnection();
					}
					this.Connection.EnlistTransaction(currentTransaction);
				}
			}
			return defaultValue;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x0010BC1B File Offset: 0x00109E1B
		private void ConnectionStateChange(object sender, StateChangeEventArgs e)
		{
			if (e.CurrentState == ConnectionState.Closed)
			{
				this._connectionRequestCount = 0;
				this._openedConnection = false;
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x0010BC34 File Offset: 0x00109E34
		internal virtual void ReleaseConnection()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
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

		// Token: 0x060038A1 RID: 14497 RVA: 0x0010BC90 File Offset: 0x00109E90
		public virtual ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
		{
			Check.NotNull<string>(queryString, "queryString");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
			ObjectQuery<T> objectQuery = new ObjectQuery<T>(queryString, this, MergeOption.AppendOnly);
			foreach (ObjectParameter item in parameters)
			{
				objectQuery.Parameters.Add(item);
			}
			return objectQuery;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x0010BCFC File Offset: 0x00109EFC
		private static EntityConnection CreateEntityConnection(string connectionString)
		{
			Check.NotEmpty(connectionString, "connectionString");
			return new EntityConnection(connectionString);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x0010BD1D File Offset: 0x00109F1D
		private MetadataWorkspace RetrieveMetadataWorkspaceFromConnection()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
			}
			return this._connection.GetMetadataWorkspace();
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0010BD3E File Offset: 0x00109F3E
		public virtual void DeleteObject(object entity)
		{
			this.DeleteObject(entity, null);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x0010BD48 File Offset: 0x00109F48
		internal void DeleteObject(object entity, EntitySet expectedEntitySet)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || !object.ReferenceEquals(entityEntry.Entity, entity))
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotDeleteEntityNotInObjectStateManager);
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw new InvalidOperationException(Strings.ObjectContext_EntityNotInObjectSet_Delete(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name));
				}
			}
			entityEntry.Delete();
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0010BDBF File Offset: 0x00109FBF
		public virtual void Detach(object entity)
		{
			this.Detach(entity, null);
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x0010BDCC File Offset: 0x00109FCC
		internal void Detach(object entity, EntitySet expectedEntitySet)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
			if (entityEntry == null || !object.ReferenceEquals(entityEntry.Entity, entity) || entityEntry.Entity == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_CannotDetachEntityNotInObjectStateManager);
			}
			if (expectedEntitySet != null)
			{
				EntitySetBase entitySet = entityEntry.EntitySet;
				if (entitySet != expectedEntitySet)
				{
					throw new InvalidOperationException(Strings.ObjectContext_EntityNotInObjectSet_Detach(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name));
				}
			}
			entityEntry.Detach();
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x0010BE4C File Offset: 0x0010A04C
		~ObjectContext()
		{
			this.Dispose(false);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x0010BE7C File Offset: 0x0010A07C
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x0010BE8C File Offset: 0x0010A08C
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (this._transactionHandler != null)
				{
					this._transactionHandler.Dispose();
				}
				if (disposing)
				{
					if (this._connection != null)
					{
						this._connection.StateChange -= this.ConnectionStateChange;
						if (this._contextOwnsConnection)
						{
							this._connection.Dispose();
						}
					}
					this._connection = null;
					if (this._objectStateManager != null)
					{
						this._objectStateManager.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x0010BF0A File Offset: 0x0010A10A
		internal bool IsDisposed
		{
			get
			{
				return this._disposed;
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x0010BF14 File Offset: 0x0010A114
		internal EntitySet GetEntitySet(string entitySetName, string entityContainerName)
		{
			EntityContainer entityContainer = null;
			if (string.IsNullOrEmpty(entityContainerName))
			{
				entityContainer = this.Perspective.GetDefaultContainer();
			}
			else if (!this.MetadataWorkspace.TryGetEntityContainer(entityContainerName, DataSpace.CSpace, out entityContainer))
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityContainerNotFoundForName(entityContainerName));
			}
			EntitySet result = null;
			if (!entityContainer.TryGetEntitySetByName(entitySetName, false, out result))
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntitySetNotFoundForName(TypeHelpers.GetFullName(entityContainer.Name, entitySetName)));
			}
			return result;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x0010BF7C File Offset: 0x0010A17C
		private static void GetEntitySetName(string qualifiedName, string parameterName, ObjectContext context, out string entityset, out string container)
		{
			entityset = null;
			container = null;
			Check.NotEmpty(qualifiedName, parameterName);
			string[] array = qualifiedName.Split(new char[]
			{
				'.'
			});
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
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
					throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
				}
			}
			if (entityset == null || entityset.Length == 0)
			{
				throw new ArgumentException(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
			}
			if (context != null && string.IsNullOrEmpty(container) && context.Perspective.GetDefaultContainer() == null)
			{
				throw new ArgumentException(Strings.ObjectContext_ContainerQualifiedEntitySetNameRequired, parameterName);
			}
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x0010C030 File Offset: 0x0010A230
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private void ValidateEntitySet(EntitySet entitySet, Type entityType)
		{
			TypeUsage typeUsage = this.GetTypeUsage(entityType);
			if (!entitySet.ElementType.IsAssignableFrom(typeUsage.EdmType))
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidEntitySetOnEntity(entitySet.Name, entityType), "entity");
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x0010C070 File Offset: 0x0010A270
		internal TypeUsage GetTypeUsage(Type entityCLRType)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityCLRType, Assembly.GetCallingAssembly());
			TypeUsage typeUsage = null;
			if (!this.Perspective.TryGetType(entityCLRType, out typeUsage) || !TypeSemantics.IsEntityType(typeUsage))
			{
				throw new InvalidOperationException(Strings.ObjectContext_NoMappingForEntityType(entityCLRType.FullName));
			}
			return typeUsage;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x0010C0BC File Offset: 0x0010A2BC
		public virtual object GetObjectByKey(EntityKey key)
		{
			Check.NotNull<EntityKey>(key, "key");
			EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
			this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
			object result;
			if (!this.TryGetObjectByKey(key, out result))
			{
				throw new ObjectNotFoundException(Strings.ObjectContext_ObjectNotFound);
			}
			return result;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x0010C10F File Offset: 0x0010A30F
		public virtual void Refresh(RefreshMode refreshMode, IEnumerable collection)
		{
			Check.NotNull<IEnumerable>(collection, "collection");
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			this.RefreshEntities(refreshMode, collection);
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x0010C12C File Offset: 0x0010A32C
		public virtual void Refresh(RefreshMode refreshMode, object entity)
		{
			Check.NotNull<object>(entity, "entity");
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			this.RefreshEntities(refreshMode, new object[]
			{
				entity
			});
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x0010C15E File Offset: 0x0010A35E
		public Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection)
		{
			return this.RefreshAsync(refreshMode, collection, CancellationToken.None);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x0010C16D File Offset: 0x0010A36D
		public virtual Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken)
		{
			Check.NotNull<IEnumerable>(collection, "collection");
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			return this.RefreshEntitiesAsync(refreshMode, collection, cancellationToken);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x0010C19C File Offset: 0x0010A39C
		public Task RefreshAsync(RefreshMode refreshMode, object entity)
		{
			return this.RefreshAsync(refreshMode, entity, CancellationToken.None);
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x0010C1AC File Offset: 0x0010A3AC
		public virtual Task RefreshAsync(RefreshMode refreshMode, object entity, CancellationToken cancellationToken)
		{
			Check.NotNull<object>(entity, "entity");
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			EntityUtil.CheckArgumentRefreshMode(refreshMode);
			return this.RefreshEntitiesAsync(refreshMode, new object[]
			{
				entity
			}, cancellationToken);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x0010C1F4 File Offset: 0x0010A3F4
		private void RefreshCheck(Dictionary<EntityKey, EntityEntry> entities, EntityKey key)
		{
			EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(key);
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementNotInObjectStateManager(entities.Count));
			}
			if (EntityState.Added == entityEntry.State)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementInAddedState(entities.Count));
			}
			try
			{
				entities.Add(key, entityEntry);
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException(Strings.ObjectContext_NthElementIsDuplicate(entities.Count));
			}
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x0010C278 File Offset: 0x0010A478
		private void RefreshEntities(RefreshMode refreshMode, IEnumerable collection)
		{
			this.AsyncMonitor.EnsureNotEntered();
			bool flag = false;
			try
			{
				Dictionary<EntityKey, EntityEntry> dictionary = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
				Dictionary<EntitySet, List<EntityKey>> dictionary2 = new Dictionary<EntitySet, List<EntityKey>>();
				foreach (object entityLike in collection)
				{
					this.AddRefreshKey(entityLike, dictionary, dictionary2);
				}
				if (dictionary2.Count > 0)
				{
					this.EnsureConnection(false);
					flag = true;
					foreach (EntitySet entitySet in dictionary2.Keys)
					{
						List<EntityKey> list = dictionary2[entitySet];
						for (int i = 0; i < list.Count; i = this.BatchRefreshEntitiesByKey(refreshMode, dictionary, entitySet, list, i))
						{
						}
					}
				}
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
						goto IL_207;
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
						throw new InvalidOperationException(Strings.ObjectContext_ClientEntityRemovedFromStore(stringBuilder.ToString()));
					}
				}
				IL_207:;
			}
			finally
			{
				if (flag)
				{
					this.ReleaseConnection();
				}
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x0010C570 File Offset: 0x0010A770
		private int BatchRefreshEntitiesByKey(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
		{
			Tuple<ObjectQueryExecutionPlan, int> queryPlanAndNextPosition = this.PrepareRefreshQuery(refreshMode, targetSet, targetKeys, startFrom);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			ObjectResult<object> results = executionStrategy.Execute<ObjectResult<object>>(() => this.ExecuteInTransaction<ObjectResult<object>>(() => queryPlanAndNextPosition.Item1.Execute<object>(this, null), executionStrategy, false, true));
			this.ProcessRefreshedEntities(trackedEntities, results);
			return queryPlanAndNextPosition.Item2;
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0010CB00 File Offset: 0x0010AD00
		private async Task RefreshEntitiesAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken)
		{
			this.AsyncMonitor.Enter();
			bool openedConnection = false;
			try
			{
				Dictionary<EntityKey, EntityEntry> entities = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
				Dictionary<EntitySet, List<EntityKey>> refreshKeys = new Dictionary<EntitySet, List<EntityKey>>();
				foreach (object entityLike in collection)
				{
					this.AddRefreshKey(entityLike, entities, refreshKeys);
				}
				if (refreshKeys.Count > 0)
				{
					await this.EnsureConnectionAsync(false, cancellationToken).WithCurrentCulture();
					openedConnection = true;
					foreach (EntitySet targetSet in refreshKeys.Keys)
					{
						List<EntityKey> setKeys = refreshKeys[targetSet];
						for (int refreshedCount = 0; refreshedCount < setKeys.Count; refreshedCount = await this.BatchRefreshEntitiesByKeyAsync(refreshMode, entities, targetSet, setKeys, refreshedCount, cancellationToken).WithCurrentCulture<int>())
						{
						}
					}
				}
				if (RefreshMode.StoreWins == refreshMode)
				{
					using (Dictionary<EntityKey, EntityEntry>.Enumerator enumerator3 = entities.GetEnumerator())
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
						goto IL_3FD;
					}
				}
				if (RefreshMode.ClientWins == refreshMode && 0 < entities.Count)
				{
					string value = string.Empty;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair2 in entities)
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
						throw new InvalidOperationException(Strings.ObjectContext_ClientEntityRemovedFromStore(stringBuilder.ToString()));
					}
				}
				IL_3FD:;
			}
			finally
			{
				if (openedConnection)
				{
					this.ReleaseConnection();
				}
				this.AsyncMonitor.Exit();
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0010CD6C File Offset: 0x0010AF6C
		private async Task<int> BatchRefreshEntitiesByKeyAsync(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom, CancellationToken cancellationToken)
		{
			Tuple<ObjectQueryExecutionPlan, int> queryPlanAndNextPosition = this.PrepareRefreshQuery(refreshMode, targetSet, targetKeys, startFrom);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			ObjectResult<object> results = await executionStrategy.ExecuteAsync<ObjectResult<object>>(() => this.ExecuteInTransactionAsync<ObjectResult<object>>(() => queryPlanAndNextPosition.Item1.ExecuteAsync<object>(this, null, cancellationToken), executionStrategy, false, true, cancellationToken), cancellationToken).WithCurrentCulture<ObjectResult<object>>();
			this.ProcessRefreshedEntities(trackedEntities, results);
			return queryPlanAndNextPosition.Item2;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x0010CDE8 File Offset: 0x0010AFE8
		internal virtual Tuple<ObjectQueryExecutionPlan, int> PrepareRefreshQuery(RefreshMode refreshMode, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
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
			DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(this.MetadataWorkspace, DataSpace.CSpace, query, true);
			MergeOption mergeOption = (RefreshMode.StoreWins == refreshMode) ? MergeOption.OverwriteChanges : MergeOption.PreserveChanges;
			ObjectQueryExecutionPlan item = this._objectQueryExecutionPlanFactory.Prepare(this, tree, typeof(object), mergeOption, false, null, null, DbExpressionBuilder.AliasGenerator);
			return new Tuple<ObjectQueryExecutionPlan, int>(item, startFrom);
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x0010CED4 File Offset: 0x0010B0D4
		private void ProcessRefreshedEntities(Dictionary<EntityKey, EntityEntry> trackedEntities, ObjectResult<object> results)
		{
			foreach (object entity in results)
			{
				EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
				if (entityEntry != null && entityEntry.State == EntityState.Modified)
				{
					entityEntry.SetModifiedAll();
				}
				IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(entity, this);
				EntityKey entityKey = entityWrapper.EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				if (!trackedEntities.Remove(entityKey))
				{
					throw new InvalidOperationException(Strings.ObjectContext_StoreEntityNotPresentInClient);
				}
			}
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0010CF6C File Offset: 0x0010B16C
		private static int RefreshEntitiesSize(IEnumerable collection)
		{
			ICollection collection2 = collection as ICollection;
			if (collection2 == null)
			{
				return 0;
			}
			return collection2.Count;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x0010CF8B File Offset: 0x0010B18B
		public virtual int SaveChanges()
		{
			return this.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x0010CF94 File Offset: 0x0010B194
		public virtual Task<int> SaveChangesAsync()
		{
			return this.SaveChangesAsync(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave, CancellationToken.None);
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x0010CFA2 File Offset: 0x0010B1A2
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return this.SaveChangesAsync(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave, cancellationToken);
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x0010CFAC File Offset: 0x0010B1AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Use SaveChanges(SaveOptions options) instead.")]
		public virtual int SaveChanges(bool acceptChangesDuringSave)
		{
			return this.SaveChanges(acceptChangesDuringSave ? (SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave) : SaveOptions.DetectChangesBeforeSave);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x0010CFBB File Offset: 0x0010B1BB
		public virtual int SaveChanges(SaveOptions options)
		{
			return this.SaveChangesInternal(options, false);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0010CFFC File Offset: 0x0010B1FC
		internal int SaveChangesInternal(SaveOptions options, bool executeInExistingTransaction)
		{
			this.AsyncMonitor.EnsureNotEntered();
			this.PrepareToSaveChanges(options);
			int result = 0;
			if (this.ObjectStateManager.HasChanges())
			{
				if (executeInExistingTransaction)
				{
					result = this.SaveChangesToStore(options, null, false);
				}
				else
				{
					IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
					result = executionStrategy.Execute<int>(() => this.SaveChangesToStore(options, executionStrategy, true));
				}
			}
			return result;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x0010D094 File Offset: 0x0010B294
		public virtual Task<int> SaveChangesAsync(SaveOptions options)
		{
			return this.SaveChangesAsync(options, CancellationToken.None);
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x0010D0A2 File Offset: 0x0010B2A2
		public virtual Task<int> SaveChangesAsync(SaveOptions options, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			return this.SaveChangesInternalAsync(options, false, cancellationToken);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x0010D3D4 File Offset: 0x0010B5D4
		internal async Task<int> SaveChangesInternalAsync(SaveOptions options, bool executeInExistingTransaction, CancellationToken cancellationToken)
		{
			this.AsyncMonitor.Enter();
			int result;
			try
			{
				this.PrepareToSaveChanges(options);
				int entriesAffected = 0;
				if (this.ObjectStateManager.HasChanges())
				{
					if (executeInExistingTransaction)
					{
						entriesAffected = await this.SaveChangesToStoreAsync(options, null, false, cancellationToken).WithCurrentCulture<int>();
					}
					else
					{
						IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
						entriesAffected = await executionStrategy.ExecuteAsync<int>(() => this.SaveChangesToStoreAsync(options, executionStrategy, true, cancellationToken), cancellationToken).WithCurrentCulture<int>();
					}
				}
				result = entriesAffected;
			}
			finally
			{
				this.AsyncMonitor.Exit();
			}
			return result;
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x0010D434 File Offset: 0x0010B634
		private void PrepareToSaveChanges(SaveOptions options)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, Strings.ObjectContext_ObjectDisposed);
			}
			this.OnSavingChanges();
			if ((SaveOptions.DetectChangesBeforeSave & options) != SaveOptions.None)
			{
				this.ObjectStateManager.DetectChanges();
			}
			if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0010D490 File Offset: 0x0010B690
		private int SaveChangesToStore(SaveOptions options, IDbExecutionStrategy executionStrategy, bool startLocalTransaction)
		{
			this._adapter.AcceptChangesDuringUpdate = false;
			this._adapter.Connection = this.Connection;
			this._adapter.CommandTimeout = this.CommandTimeout;
			int result = this.ExecuteInTransaction<int>(() => this._adapter.Update(), executionStrategy, startLocalTransaction, true);
			if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
			{
				try
				{
					this.AcceptAllChanges();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Strings.ObjectContext_AcceptAllChangesFailure(ex.Message), ex);
				}
			}
			return result;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x0010D714 File Offset: 0x0010B914
		private async Task<int> SaveChangesToStoreAsync(SaveOptions options, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, CancellationToken cancellationToken)
		{
			this._adapter.AcceptChangesDuringUpdate = false;
			this._adapter.Connection = this.Connection;
			this._adapter.CommandTimeout = this.CommandTimeout;
			int entriesAffected = await this.ExecuteInTransactionAsync<int>(() => this._adapter.UpdateAsync(cancellationToken), executionStrategy, startLocalTransaction, true, cancellationToken).WithCurrentCulture<int>();
			if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
			{
				try
				{
					this.AcceptAllChanges();
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Strings.ObjectContext_AcceptAllChangesFailure(ex.Message), ex);
				}
			}
			return entriesAffected;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x0010D77C File Offset: 0x0010B97C
		internal virtual T ExecuteInTransaction<T>(Func<T> func, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, bool releaseConnectionOnSuccess)
		{
			this.EnsureConnection(startLocalTransaction);
			bool flag = false;
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			if (entityConnection.CurrentTransaction == null && !entityConnection.EnlistedInUserTransaction && this._lastTransaction == null)
			{
				flag = startLocalTransaction;
			}
			else if (executionStrategy != null && executionStrategy.RetriesOnFailure)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(executionStrategy.GetType().Name));
			}
			DbTransaction dbTransaction = null;
			T result;
			try
			{
				if (flag)
				{
					dbTransaction = entityConnection.BeginTransaction();
				}
				T t = func();
				if (dbTransaction != null)
				{
					dbTransaction.Commit();
				}
				if (releaseConnectionOnSuccess)
				{
					this.ReleaseConnection();
				}
				result = t;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
			finally
			{
				if (dbTransaction != null)
				{
					dbTransaction.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x0010DAFC File Offset: 0x0010BCFC
		internal virtual async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> func, IDbExecutionStrategy executionStrategy, bool startLocalTransaction, bool releaseConnectionOnSuccess, CancellationToken cancellationToken)
		{
			await this.EnsureConnectionAsync(startLocalTransaction, cancellationToken).WithCurrentCulture();
			bool needLocalTransaction = false;
			EntityConnection connection = (EntityConnection)this.Connection;
			if (connection.CurrentTransaction == null && !connection.EnlistedInUserTransaction && this._lastTransaction == null)
			{
				needLocalTransaction = startLocalTransaction;
			}
			else if (executionStrategy.RetriesOnFailure)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(executionStrategy.GetType().Name));
			}
			DbTransaction localTransaction = null;
			T result2;
			try
			{
				if (needLocalTransaction)
				{
					localTransaction = connection.BeginTransaction();
				}
				T result = await func().WithCurrentCulture<T>();
				if (localTransaction != null)
				{
					localTransaction.Commit();
				}
				if (releaseConnectionOnSuccess)
				{
					this.ReleaseConnection();
				}
				result2 = result;
			}
			catch (Exception)
			{
				this.ReleaseConnection();
				throw;
			}
			finally
			{
				if (localTransaction != null)
				{
					localTransaction.Dispose();
				}
			}
			return result2;
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x0010DB6C File Offset: 0x0010BD6C
		public virtual void DetectChanges()
		{
			this.ObjectStateManager.DetectChanges();
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x0010DB7C File Offset: 0x0010BD7C
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		public virtual bool TryGetObjectByKey(EntityKey key, out object value)
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

		// Token: 0x060038CF RID: 14543 RVA: 0x0010DD54 File Offset: 0x0010BF54
		public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return this.ExecuteFunction<TElement>(functionName, MergeOption.AppendOnly, parameters);
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x0010DD6B File Offset: 0x0010BF6B
		public virtual ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			return this.ExecuteFunction<TElement>(functionName, new ExecutionOptions(mergeOption), parameters);
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0010DE10 File Offset: 0x0010C010
		public virtual ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, ExecutionOptions executionOptions, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			this.AsyncMonitor.EnsureNotEntered();
			EdmFunction functionImport;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out functionImport, parameters);
			int num = Math.Max(1, functionImport.ReturnParameters.Count);
			EdmType[] expectedEdmTypes = new EdmType[num];
			expectedEdmTypes[0] = MetadataHelper.GetAndCheckFunctionImportReturnType<TElement>(functionImport, 0, this.MetadataWorkspace);
			for (int i = 1; i < num; i++)
			{
				if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, i, out expectedEdmTypes[i]))
				{
					throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(functionImport);
				}
			}
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			if (executionOptions.UserSpecifiedStreaming == null)
			{
				executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
			}
			bool startLocalTransaction = !executionOptions.UserSpecifiedStreaming.Value && this._options.EnsureTransactionsForFunctionsAndCommands;
			return executionStrategy.Execute<ObjectResult<TElement>>(() => this.ExecuteInTransaction<ObjectResult<TElement>>(() => this.CreateFunctionObjectResult<TElement>(entityCommand, functionImport.EntitySets, expectedEdmTypes, executionOptions), executionStrategy, startLocalTransaction, !executionOptions.UserSpecifiedStreaming.Value));
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x0010E004 File Offset: 0x0010C204
		public virtual int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
		{
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			Check.NotEmpty(functionName, "functionName");
			this.AsyncMonitor.EnsureNotEntered();
			EdmFunction edmFunction;
			EntityCommand entityCommand = this.CreateEntityCommandForFunctionImport(functionName, out edmFunction, parameters);
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			return executionStrategy.Execute<int>(() => this.ExecuteInTransaction<int>(() => ObjectContext.ExecuteFunctionCommand(entityCommand), executionStrategy, this._options.EnsureTransactionsForFunctionsAndCommands, true));
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x0010E080 File Offset: 0x0010C280
		private static int ExecuteFunctionCommand(EntityCommand entityCommand)
		{
			entityCommand.Prepare();
			int result;
			try
			{
				result = entityCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableEntityExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x0010E0C4 File Offset: 0x0010C2C4
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		private EntityCommand CreateEntityCommandForFunctionImport(string functionName, out EdmFunction functionImport, params ObjectParameter[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] == null)
				{
					throw new InvalidOperationException(Strings.ObjectContext_ExecuteFunctionCalledWithNullParameter(i));
				}
			}
			string str;
			string str2;
			functionImport = MetadataHelper.GetFunctionImport(functionName, this.DefaultContainerName, this.MetadataWorkspace, out str, out str2);
			EntityConnection connection = (EntityConnection)this.Connection;
			EntityCommand entityCommand = new EntityCommand(this.InterceptionContext);
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

		// Token: 0x060038D5 RID: 14549 RVA: 0x0010E184 File Offset: 0x0010C384
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reader disposed by the returned ObjectResult")]
		private ObjectResult<TElement> CreateFunctionObjectResult<TElement>(EntityCommand entityCommand, ReadOnlyCollection<EntitySet> entitySets, EdmType[] edmTypes, ExecutionOptions executionOptions)
		{
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			DbDataReader dbDataReader = null;
			try
			{
				dbDataReader = commandDefinition.ExecuteStoreCommands(entityCommand, executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableEntityExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandExecutionFailed, ex);
				}
				throw;
			}
			ShaperFactory<TElement> shaperFactory = null;
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedDataReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					shaperFactory = this._translator.TranslateColumnMap<TElement>(commandDefinition.CreateColumnMap(dbDataReader, 0), this.MetadataWorkspace, null, executionOptions.MergeOption, false, false);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					dbDataReader = bufferedDataReader;
				}
				catch (Exception)
				{
					if (bufferedDataReader != null)
					{
						bufferedDataReader.Dispose();
					}
					throw;
				}
			}
			return this.MaterializedDataRecord<TElement>(entityCommand, dbDataReader, 0, entitySets, edmTypes, shaperFactory, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x0010E2F0 File Offset: 0x0010C4F0
		internal ObjectResult<TElement> MaterializedDataRecord<TElement>(EntityCommand entityCommand, DbDataReader storeReader, int resultSetIndex, ReadOnlyCollection<EntitySet> entitySets, EdmType[] edmTypes, ShaperFactory<TElement> shaperFactory, MergeOption mergeOption, bool streaming)
		{
			EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
			ObjectResult<TElement> result;
			try
			{
				bool flag = edmTypes.Length <= resultSetIndex + 1;
				EntitySet singleEntitySet = (entitySets.Count > resultSetIndex) ? entitySets[resultSetIndex] : null;
				if (shaperFactory == null)
				{
					shaperFactory = this._translator.TranslateColumnMap<TElement>(commandDefinition.CreateColumnMap(storeReader, resultSetIndex), this.MetadataWorkspace, null, mergeOption, streaming, false);
				}
				Shaper<TElement> shaper = shaperFactory.Create(storeReader, this, this.MetadataWorkspace, mergeOption, flag, streaming);
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
					nextResultGenerator = new NextResultGenerator(this, entityCommand, edmTypes, entitySets, mergeOption, streaming, resultSetIndex + 1);
				}
				result = new ObjectResult<TElement>(shaper, singleEntitySet, TypeUsage.Create(edmTypes[resultSetIndex]), true, streaming, nextResultGenerator, action, null);
			}
			catch
			{
				this.ReleaseConnection();
				storeReader.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x0010E42C File Offset: 0x0010C62C
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
						if (!this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage))
						{
							this.MetadataWorkspace.ImplicitLoadAssemblyForType(objectParameter.MappableType, null);
							this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage);
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

		// Token: 0x060038D8 RID: 14552 RVA: 0x0010E568 File Offset: 0x0010C768
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

		// Token: 0x060038D9 RID: 14553 RVA: 0x0010E61C File Offset: 0x0010C81C
		public virtual void CreateProxyTypes(IEnumerable<Type> types)
		{
			ObjectItemCollection ospaceItems = (ObjectItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			EntityProxyFactory.TryCreateProxyTypes(from entityType in types.Select(delegate(Type type)
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(type, null);
				EntityType result;
				ospaceItems.TryGetItem<EntityType>(type.FullNameWithNesting(), out result);
				return result;
			})
			where entityType != null
			select entityType, this.MetadataWorkspace);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x0010E68C File Offset: 0x0010C88C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static IEnumerable<Type> GetKnownProxyTypes()
		{
			return EntityProxyFactory.GetKnownProxyTypes();
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x0010E693 File Offset: 0x0010C893
		public static Type GetObjectType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			if (!EntityProxyFactory.IsProxyType(type))
			{
				return type;
			}
			return type.BaseType();
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x0010E6B4 File Offset: 0x0010C8B4
		public virtual T CreateObject<T>() where T : class
		{
			T t = default(T);
			Type typeFromHandle = typeof(T);
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeFromHandle, null);
			ClrEntityType item = this.MetadataWorkspace.GetItem<ClrEntityType>(typeFromHandle.FullNameWithNesting(), DataSpace.OSpace);
			EntityProxyTypeInfo proxyType;
			if (this.ContextOptions.ProxyCreationEnabled && (proxyType = EntityProxyFactory.GetProxyType(item, this.MetadataWorkspace)) != null)
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
			t = (DelegateFactory.GetConstructorDelegateForType(item)() as T);
			return t;
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x0010E7B0 File Offset: 0x0010C9B0
		public virtual int ExecuteStoreCommand(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommand(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, parameters);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x0010E820 File Offset: 0x0010CA20
		public virtual int ExecuteStoreCommand(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
		{
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			this.AsyncMonitor.EnsureNotEntered();
			return executionStrategy.Execute<int>(() => this.ExecuteInTransaction<int>(() => this.CreateStoreCommand(commandText, parameters).ExecuteNonQuery(), executionStrategy, transactionalBehavior != TransactionalBehavior.DoNotEnsureTransaction, true));
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x0010E888 File Offset: 0x0010CA88
		public Task<int> ExecuteStoreCommandAsync(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0010E8A8 File Offset: 0x0010CAA8
		public Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(transactionalBehavior, commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
		public virtual Task<int> ExecuteStoreCommandAsync(string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			return this.ExecuteStoreCommandAsync(this._options.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, commandText, cancellationToken, parameters);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x0010E8D4 File Offset: 0x0010CAD4
		public virtual Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AsyncMonitor.EnsureNotEntered();
			return this.ExecuteStoreCommandInternalAsync(transactionalBehavior, commandText, cancellationToken, parameters);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x0010EB4C File Offset: 0x0010CD4C
		private async Task<int> ExecuteStoreCommandInternalAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			this.AsyncMonitor.Enter();
			int result;
			try
			{
				result = await executionStrategy.ExecuteAsync<int>(() => this.ExecuteInTransactionAsync<int>(() => this.CreateStoreCommand(commandText, parameters).ExecuteNonQueryAsync(cancellationToken), executionStrategy, transactionalBehavior != TransactionalBehavior.DoNotEnsureTransaction, true, cancellationToken), cancellationToken).WithCurrentCulture<int>();
			}
			finally
			{
				this.AsyncMonitor.Exit();
			}
			return result;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x0010EBB3 File Offset: 0x0010CDB3
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreQueryReliably<TElement>(commandText, null, ExecutionOptions.Default, parameters);
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x0010EBC3 File Offset: 0x0010CDC3
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryReliably<TElement>(commandText, null, executionOptions, parameters);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x0010EBCF File Offset: 0x0010CDCF
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			return this.ExecuteStoreQueryReliably<TElement>(commandText, entitySetName, new ExecutionOptions(mergeOption), parameters);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x0010EBED File Offset: 0x0010CDED
		public virtual ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			return this.ExecuteStoreQueryReliably<TElement>(commandText, entitySetName, executionOptions, parameters);
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x0010EC78 File Offset: 0x0010CE78
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Buffer disposed by the returned ObjectResult")]
		private ObjectResult<TElement> ExecuteStoreQueryReliably<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			if (executionOptions.UserSpecifiedStreaming == null)
			{
				executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
			}
			return executionStrategy.Execute<ObjectResult<TElement>>(() => this.ExecuteInTransaction<ObjectResult<TElement>>(() => this.ExecuteStoreQueryInternal<TElement>(commandText, entitySetName, executionOptions, parameters), executionStrategy, false, !executionOptions.UserSpecifiedStreaming.Value));
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x0010ED90 File Offset: 0x0010CF90
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by ObjectResult")]
		private ObjectResult<TElement> ExecuteStoreQueryInternal<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			DbDataReader dbDataReader = null;
			DbCommand dbCommand = null;
			EntitySet entitySet;
			TypeUsage edmType;
			ShaperFactory<TElement> shaperFactory;
			try
			{
				dbCommand = this.CreateStoreCommand(commandText, parameters);
				dbDataReader = dbCommand.ExecuteReader(executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
				shaperFactory = this.InternalTranslate<TElement>(dbDataReader, entitySetName, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value, out entitySet, out edmType);
			}
			catch
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				throw;
			}
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedDataReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					dbDataReader = bufferedDataReader;
				}
				catch
				{
					if (bufferedDataReader != null)
					{
						bufferedDataReader.Dispose();
					}
					throw;
				}
			}
			return this.ShapeResult<TElement>(dbDataReader, executionOptions.MergeOption, true, executionOptions.UserSpecifiedStreaming.Value, shaperFactory, entitySet, edmType, dbCommand);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0010EEB4 File Offset: 0x0010D0B4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, CancellationToken.None, parameters);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x0010EEC4 File Offset: 0x0010D0C4
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, CancellationToken cancellationToken, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, null, ExecutionOptions.Default, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x0010EEFE File Offset: 0x0010D0FE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, executionOptions, CancellationToken.None, parameters);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x0010EF10 File Offset: 0x0010D110
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, null, executionOptions, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x0010EF85 File Offset: 0x0010D185
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
		{
			return this.ExecuteStoreQueryAsync<TElement>(commandText, entitySetName, executionOptions, CancellationToken.None, parameters);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x0010EF98 File Offset: 0x0010D198
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this.Connection, this.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && executionOptions.UserSpecifiedStreaming != null && executionOptions.UserSpecifiedStreaming.Value)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			return this.ExecuteStoreQueryReliablyAsync<TElement>(commandText, entitySetName, executionOptions, cancellationToken, executionStrategy, parameters);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x0010F324 File Offset: 0x0010D524
		private async Task<ObjectResult<TElement>> ExecuteStoreQueryReliablyAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, IDbExecutionStrategy executionStrategy, params object[] parameters)
		{
			if (executionOptions.MergeOption != MergeOption.NoTracking)
			{
				this.AsyncMonitor.Enter();
			}
			ObjectResult<TElement> result;
			try
			{
				this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
				if (executionOptions.UserSpecifiedStreaming == null)
				{
					executionOptions = new ExecutionOptions(executionOptions.MergeOption, !executionStrategy.RetriesOnFailure);
				}
				result = await executionStrategy.ExecuteAsync<ObjectResult<TElement>>(() => this.ExecuteInTransactionAsync<ObjectResult<TElement>>(() => this.ExecuteStoreQueryInternalAsync<TElement>(commandText, entitySetName, executionOptions, cancellationToken, parameters), executionStrategy, false, !executionOptions.UserSpecifiedStreaming.Value, cancellationToken), cancellationToken).WithCurrentCulture<ObjectResult<TElement>>();
			}
			finally
			{
				if (executionOptions.MergeOption != MergeOption.NoTracking)
				{
					this.AsyncMonitor.Exit();
				}
			}
			return result;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x0010F728 File Offset: 0x0010D928
		private async Task<ObjectResult<TElement>> ExecuteStoreQueryInternalAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, params object[] parameters)
		{
			DbDataReader reader = null;
			DbCommand command = null;
			EntitySet entitySet;
			TypeUsage edmType;
			ShaperFactory<TElement> shaperFactory;
			try
			{
				command = this.CreateStoreCommand(commandText, parameters);
				reader = await command.ExecuteReaderAsync(executionOptions.UserSpecifiedStreaming.Value ? CommandBehavior.Default : CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
				shaperFactory = this.InternalTranslate<TElement>(reader, entitySetName, executionOptions.MergeOption, executionOptions.UserSpecifiedStreaming.Value, out entitySet, out edmType);
			}
			catch
			{
				if (reader != null)
				{
					reader.Dispose();
				}
				if (command != null)
				{
					command.Dispose();
				}
				throw;
			}
			if (!executionOptions.UserSpecifiedStreaming.Value)
			{
				BufferedDataReader bufferedReader = null;
				try
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices providerServices = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedReader = new BufferedDataReader(reader);
					await bufferedReader.InitializeAsync(storeItemCollection.ProviderManifestToken, providerServices, shaperFactory.ColumnTypes, shaperFactory.NullableColumns, cancellationToken).WithCurrentCulture();
					reader = bufferedReader;
				}
				catch
				{
					if (bufferedReader != null)
					{
						bufferedReader.Dispose();
					}
					throw;
				}
			}
			return this.ShapeResult<TElement>(reader, executionOptions.MergeOption, true, executionOptions.UserSpecifiedStreaming.Value, shaperFactory, entitySet, edmType, command);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x0010F798 File Offset: 0x0010D998
		public virtual ObjectResult<TElement> Translate<TElement>(DbDataReader reader)
		{
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TElement), Assembly.GetCallingAssembly());
			EntitySet entitySet;
			TypeUsage edmType;
			ShaperFactory<TElement> shaperFactory = this.InternalTranslate<TElement>(reader, null, MergeOption.AppendOnly, false, out entitySet, out edmType);
			return this.ShapeResult<TElement>(reader, MergeOption.AppendOnly, false, false, shaperFactory, entitySet, edmType, null);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x0010F7DC File Offset: 0x0010D9DC
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Generic parameters are required for strong-typing of the return type.")]
		public virtual ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption)
		{
			Check.NotEmpty(entitySetName, "entitySetName");
			this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TEntity), Assembly.GetCallingAssembly());
			EntitySet entitySet;
			TypeUsage edmType;
			ShaperFactory<TEntity> shaperFactory = this.InternalTranslate<TEntity>(reader, entitySetName, mergeOption, false, out entitySet, out edmType);
			return this.ShapeResult<TEntity>(reader, mergeOption, false, false, shaperFactory, entitySet, edmType, null);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x0010F82C File Offset: 0x0010DA2C
		private ShaperFactory<TElement> InternalTranslate<TElement>(DbDataReader reader, string entitySetName, MergeOption mergeOption, bool streaming, out EntitySet entitySet, out TypeUsage edmType)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			entitySet = null;
			if (!string.IsNullOrEmpty(entitySetName))
			{
				entitySet = this.GetEntitySetFromName(entitySetName);
			}
			Type type = Nullable.GetUnderlyingType(typeof(TElement)) ?? typeof(TElement);
			EdmType edmType2;
			CollectionColumnMap collectionColumnMap;
			if (this.MetadataWorkspace.TryDetermineCSpaceModelType<TElement>(out edmType2) || (type.IsEnum() && this.MetadataWorkspace.TryDetermineCSpaceModelType(type.GetEnumUnderlyingType(), out edmType2)))
			{
				if (entitySet != null && !entitySet.ElementType.IsAssignableFrom(edmType2))
				{
					throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetForStoreQuery(entitySet.EntityContainer.Name, entitySet.Name, typeof(TElement)));
				}
				collectionColumnMap = this._columnMapFactory.CreateColumnMapFromReaderAndType(reader, edmType2, entitySet, null);
			}
			else
			{
				collectionColumnMap = this._columnMapFactory.CreateColumnMapFromReaderAndClrType(reader, typeof(TElement), this.MetadataWorkspace);
			}
			edmType = collectionColumnMap.Type;
			return this._translator.TranslateColumnMap<TElement>(collectionColumnMap, this.MetadataWorkspace, null, mergeOption, streaming, false);
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x0010F930 File Offset: 0x0010DB30
		private ObjectResult<TElement> ShapeResult<TElement>(DbDataReader reader, MergeOption mergeOption, bool readerOwned, bool streaming, ShaperFactory<TElement> shaperFactory, EntitySet entitySet, TypeUsage edmType, DbCommand command = null)
		{
			Shaper<TElement> shaper = shaperFactory.Create(reader, this, this.MetadataWorkspace, mergeOption, readerOwned, streaming);
			return new ObjectResult<TElement>(shaper, entitySet, MetadataHelper.GetElementType(edmType), readerOwned, streaming, command);
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x0010F97C File Offset: 0x0010DB7C
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		private DbCommand CreateStoreCommand(string commandText, params object[] parameters)
		{
			DbCommand dbCommand = ((EntityConnection)this.Connection).StoreConnection.CreateCommand();
			dbCommand.CommandText = commandText;
			if (this.CommandTimeout != null)
			{
				dbCommand.CommandTimeout = this.CommandTimeout.Value;
			}
			EntityTransaction currentTransaction = ((EntityConnection)this.Connection).CurrentTransaction;
			if (currentTransaction != null)
			{
				dbCommand.Transaction = currentTransaction.StoreTransaction;
			}
			if (parameters != null && parameters.Length > 0)
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
						throw new InvalidOperationException(Strings.ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues);
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
					dbCommand.CommandText = string.Format(CultureInfo.InvariantCulture, dbCommand.CommandText, array3);
				}
				dbCommand.Parameters.AddRange(array);
			}
			return new InterceptableDbCommand(dbCommand, this.InterceptionContext, null);
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x0010FB40 File Offset: 0x0010DD40
		public virtual void CreateDatabase()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			providerServices.CreateDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x0010FB84 File Offset: 0x0010DD84
		public virtual void DeleteDatabase()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			providerServices.DeleteDatabase(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x0010FBC8 File Offset: 0x0010DDC8
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool DatabaseExists()
		{
			DbConnection storeConnection = ((EntityConnection)this.Connection).StoreConnection;
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			bool result;
			try
			{
				result = providerServices.DatabaseExists(storeConnection, this.CommandTimeout, this.GetStoreItemCollection());
			}
			catch (Exception)
			{
				if (this.Connection.State == ConnectionState.Open)
				{
					result = true;
				}
				else
				{
					try
					{
						this.Connection.Open();
						result = true;
					}
					catch (EntityException)
					{
						result = false;
					}
					finally
					{
						this.Connection.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x0010FC6C File Offset: 0x0010DE6C
		private StoreItemCollection GetStoreItemCollection()
		{
			EntityConnection entityConnection = (EntityConnection)this.Connection;
			return (StoreItemCollection)entityConnection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0010FC98 File Offset: 0x0010DE98
		public virtual string CreateDatabaseScript()
		{
			DbProviderServices providerServices = this.GetStoreItemCollection().ProviderFactory.GetProviderServices();
			string providerManifestToken = this.GetStoreItemCollection().ProviderManifestToken;
			return providerServices.CreateDatabaseScript(providerManifestToken, this.GetStoreItemCollection());
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x0010FD74 File Offset: 0x0010DF74
		internal void InitializeMappingViewCacheFactory(DbContext owner = null)
		{
			StorageMappingItemCollection itemCollection = (StorageMappingItemCollection)this.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
			if (itemCollection == null)
			{
				return;
			}
			Type key = (owner != null) ? owner.GetType() : base.GetType();
			ObjectContext._contextTypesWithViewCacheInitialized.GetOrAdd(key, delegate(Type t)
			{
				IEnumerable<DbMappingViewCacheTypeAttribute> source = from a in t.Assembly().GetCustomAttributes<DbMappingViewCacheTypeAttribute>()
				where a.ContextType == t
				select a;
				int num = source.Count<DbMappingViewCacheTypeAttribute>();
				if (num > 1)
				{
					throw new InvalidOperationException(Strings.DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType(t));
				}
				if (num == 1)
				{
					itemCollection.MappingViewCacheFactory = new DefaultDbMappingViewCacheFactory(source.First<DbMappingViewCacheTypeAttribute>().CacheType);
				}
				return true;
			});
		}

		// Token: 0x040015B0 RID: 5552
		private const string UseLegacyPreserveChangesBehavior = "EntityFramework_UseLegacyPreserveChangesBehavior";

		// Token: 0x040015B1 RID: 5553
		private bool _disposed;

		// Token: 0x040015B2 RID: 5554
		private readonly IEntityAdapter _adapter;

		// Token: 0x040015B3 RID: 5555
		private EntityConnection _connection;

		// Token: 0x040015B4 RID: 5556
		private readonly MetadataWorkspace _workspace;

		// Token: 0x040015B5 RID: 5557
		private ObjectStateManager _objectStateManager;

		// Token: 0x040015B6 RID: 5558
		private ClrPerspective _perspective;

		// Token: 0x040015B7 RID: 5559
		private bool _contextOwnsConnection;

		// Token: 0x040015B8 RID: 5560
		private bool _openedConnection;

		// Token: 0x040015B9 RID: 5561
		private int _connectionRequestCount;

		// Token: 0x040015BA RID: 5562
		private int? _queryTimeout;

		// Token: 0x040015BB RID: 5563
		private Transaction _lastTransaction;

		// Token: 0x040015BC RID: 5564
		private readonly bool _disallowSettingDefaultContainerName;

		// Token: 0x040015BD RID: 5565
		private EventHandler _onSavingChanges;

		// Token: 0x040015BE RID: 5566
		private ObjectMaterializedEventHandler _onObjectMaterialized;

		// Token: 0x040015BF RID: 5567
		private ObjectQueryProvider _queryProvider;

		// Token: 0x040015C0 RID: 5568
		private readonly EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x040015C1 RID: 5569
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;

		// Token: 0x040015C2 RID: 5570
		private readonly Translator _translator;

		// Token: 0x040015C3 RID: 5571
		private readonly ColumnMapFactory _columnMapFactory;

		// Token: 0x040015C4 RID: 5572
		private readonly ObjectContextOptions _options;

		// Token: 0x040015C5 RID: 5573
		private readonly ThrowingMonitor _asyncMonitor;

		// Token: 0x040015C6 RID: 5574
		private DbInterceptionContext _interceptionContext;

		// Token: 0x040015C7 RID: 5575
		private static readonly ConcurrentDictionary<Type, bool> _contextTypesWithViewCacheInitialized = new ConcurrentDictionary<Type, bool>();

		// Token: 0x040015C8 RID: 5576
		private TransactionHandler _transactionHandler;

		// Token: 0x020005A0 RID: 1440
		private class ParameterBinder
		{
			// Token: 0x06003904 RID: 14596 RVA: 0x0010FDDD File Offset: 0x0010DFDD
			internal ParameterBinder(EntityParameter entityParameter, ObjectParameter objectParameter)
			{
				this._entityParameter = entityParameter;
				this._objectParameter = objectParameter;
			}

			// Token: 0x06003905 RID: 14597 RVA: 0x0010FDF4 File Offset: 0x0010DFF4
			internal void OnDataReaderClosingHandler(object sender, EventArgs args)
			{
				if (this._entityParameter.Value != DBNull.Value && this._objectParameter.MappableType.IsEnum())
				{
					this._objectParameter.Value = Enum.ToObject(this._objectParameter.MappableType, this._entityParameter.Value);
					return;
				}
				this._objectParameter.Value = this._entityParameter.Value;
			}

			// Token: 0x040015CF RID: 5583
			private readonly EntityParameter _entityParameter;

			// Token: 0x040015D0 RID: 5584
			private readonly ObjectParameter _objectParameter;
		}
	}
}
