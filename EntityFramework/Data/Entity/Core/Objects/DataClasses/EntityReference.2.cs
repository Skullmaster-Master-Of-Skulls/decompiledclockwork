using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000544 RID: 1348
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[DataContract]
	[Serializable]
	public class EntityReference<TEntity> : EntityReference where TEntity : class
	{
		// Token: 0x06003401 RID: 13313 RVA: 0x000F5498 File Offset: 0x000F3698
		public EntityReference()
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000F54AB File Offset: 0x000F36AB
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000F54C1 File Offset: 0x000F36C1
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000F54D9 File Offset: 0x000F36D9
		[SoapIgnore]
		[XmlIgnore]
		public TEntity Value
		{
			get
			{
				base.DeferredLoad();
				return (TEntity)((object)this.ReferenceValue.Entity);
			}
			set
			{
				this.ReferenceValue = this.EntityWrapperFactory.WrapEntityUsingContext(value, this.ObjectContext);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000F54F8 File Offset: 0x000F36F8
		internal override IEntityWrapper CachedValue
		{
			get
			{
				return this._wrappedCachedValue;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000F5500 File Offset: 0x000F3700
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x000F5510 File Offset: 0x000F3710
		internal override IEntityWrapper ReferenceValue
		{
			get
			{
				this.CheckOwnerNull();
				return this._wrappedCachedValue;
			}
			set
			{
				this.CheckOwnerNull();
				if (value.Entity != null && value.Entity == this._wrappedCachedValue.Entity)
				{
					return;
				}
				if (value.Entity != null)
				{
					base.ValidateOwnerWithRIConstraints(value, (value == NullEntityWrapper.NullWrapper) ? null : value.EntityKey, true);
					ObjectContext objectContext = this.ObjectContext ?? value.Context;
					if (objectContext != null)
					{
						objectContext.ObjectStateManager.TransactionManager.EntityBeingReparented = base.GetDependentEndOfReferentialConstraint(value.Entity);
					}
					try
					{
						base.Add(value, false);
						return;
					}
					finally
					{
						if (objectContext != null)
						{
							objectContext.ObjectStateManager.TransactionManager.EntityBeingReparented = null;
						}
					}
				}
				if (base.UsingNoTracking)
				{
					if (this._wrappedCachedValue.Entity != null)
					{
						RelatedEnd otherEndOfRelationship = base.GetOtherEndOfRelationship(this._wrappedCachedValue);
						otherEndOfRelationship.OnRelatedEndClear();
					}
					this._isLoaded = false;
				}
				else if (this.ObjectContext != null && this.ObjectContext.ContextOptions.UseConsistentNullReferenceBehavior)
				{
					base.AttemptToNullFKsOnRefOrKeySetToNull();
				}
				this.ClearCollectionOrRef(null, null, false);
			}
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000F561C File Offset: 0x000F381C
		public override void Load(MergeOption mergeOption)
		{
			this.CheckOwnerNull();
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityReference", out flag);
			this._suppressEvents = true;
			try
			{
				IList<TEntity> refreshedValue = null;
				if (flag)
				{
					ObjectResult<TEntity> source = objectQuery.Execute(objectQuery.MergeOption);
					refreshedValue = source.ToList<TEntity>();
				}
				this.HandleRefreshedValue(mergeOption, refreshedValue);
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000F58D4 File Offset: 0x000F3AD4
		public override async Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			this.CheckOwnerNull();
			cancellationToken.ThrowIfCancellationRequested();
			bool hasResults;
			ObjectQuery<TEntity> sourceQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityReference", out hasResults);
			this._suppressEvents = true;
			try
			{
				IList<TEntity> refreshedValue = null;
				if (hasResults)
				{
					ObjectResult<TEntity> objectResult = await sourceQuery.ExecuteAsync(sourceQuery.MergeOption, cancellationToken).WithCurrentCulture<ObjectResult<TEntity>>();
					refreshedValue = await objectResult.ToListAsync(cancellationToken).WithCurrentCulture<List<TEntity>>();
				}
				this.HandleRefreshedValue(mergeOption, refreshedValue);
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000F592C File Offset: 0x000F3B2C
		private void HandleRefreshedValue(MergeOption mergeOption, IList<TEntity> refreshedValue)
		{
			if (refreshedValue == null || !refreshedValue.Any<TEntity>())
			{
				if (!((AssociationType)this.RelationMetadata).IsForeignKey && this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
				{
					throw Error.EntityReference_LessThanExpectedRelatedEntitiesFound();
				}
				if (mergeOption == MergeOption.OverwriteChanges || mergeOption == MergeOption.PreserveChanges)
				{
					EntityKey entityKey = this.WrappedOwner.EntityKey;
					if (entityKey == null)
					{
						throw Error.EntityKey_UnexpectedNull();
					}
					this.ObjectContext.ObjectStateManager.RemoveRelationships(mergeOption, (AssociationSet)this.RelationshipSet, entityKey, (AssociationEndMember)this.FromEndMember);
				}
				this._isLoaded = true;
				return;
			}
			else
			{
				if (refreshedValue.Count<TEntity>() == 1)
				{
					this.Merge<TEntity>(refreshedValue, mergeOption, true);
					return;
				}
				throw Error.EntityReference_MoreThanExpectedRelatedEntitiesFound();
			}
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000F59D0 File Offset: 0x000F3BD0
		internal override IEnumerable GetInternalEnumerable()
		{
			this.CheckOwnerNull();
			if (this.ReferenceValue.Entity != null)
			{
				return new object[]
				{
					this.ReferenceValue.Entity
				};
			}
			return Enumerable.Empty<object>();
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000F5A0C File Offset: 0x000F3C0C
		internal override IEnumerable<IEntityWrapper> GetWrappedEntities()
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				return new IEntityWrapper[]
				{
					this._wrappedCachedValue
				};
			}
			return new IEntityWrapper[0];
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000F5A40 File Offset: 0x000F3C40
		public void Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.CheckOwnerNull();
			base.Attach(new IEntityWrapper[]
			{
				this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext)
			}, false);
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000F5A88 File Offset: 0x000F3C88
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(this._wrappedCachedValue.Entity, this.WrappedOwner.Context);
				if (entityWrapper != this._wrappedCachedValue)
				{
					this._wrappedCachedValue = entityWrapper;
				}
				base.IncludeEntity(this._wrappedCachedValue, addRelationshipAsUnchanged, doAttach);
				return;
			}
			if (base.DetachedEntityKey != null)
			{
				this.IncludeEntityKey(doAttach);
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000F5AF8 File Offset: 0x000F3CF8
		private void IncludeEntityKey(bool doAttach)
		{
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			bool flag = false;
			bool flag2 = false;
			EntityEntry entityEntry = objectStateManager.FindEntityEntry(base.DetachedEntityKey);
			if (entityEntry == null)
			{
				flag2 = true;
				flag = true;
			}
			else if (entityEntry.IsKeyEntry)
			{
				if (this.FromEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					foreach (RelationshipEntry relationshipEntry in this.ObjectContext.ObjectStateManager.FindRelationshipsByKey(base.DetachedEntityKey))
					{
						if (relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)this.RelationshipSet, (AssociationEndMember)this.ToEndMember, base.DetachedEntityKey) && relationshipEntry.State != EntityState.Deleted)
						{
							throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
						}
					}
				}
				flag = true;
			}
			else
			{
				IEntityWrapper wrappedEntity = entityEntry.WrappedEntity;
				if (entityEntry.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
				}
				RelatedEnd relatedEndInternal = wrappedEntity.RelationshipManager.GetRelatedEndInternal(base.RelationshipName, base.RelationshipNavigation.From);
				if (this.FromEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many && !relatedEndInternal.IsEmpty())
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
				}
				base.Add(wrappedEntity, true, doAttach, false, true, true);
				objectStateManager.TransactionManager.PopulatedEntityReferences.Add(this);
			}
			if (flag && !base.IsForeignKey)
			{
				if (flag2)
				{
					EntitySet entitySet = base.DetachedEntityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace);
					objectStateManager.AddKeyEntry(base.DetachedEntityKey, entitySet);
				}
				EntityKey entityKey = this.WrappedOwner.EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				RelationshipWrapper wrapper = new RelationshipWrapper((AssociationSet)this.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, base.DetachedEntityKey);
				objectStateManager.AddNewRelation(wrapper, doAttach ? EntityState.Unchanged : EntityState.Added);
			}
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000F5CE8 File Offset: 0x000F3EE8
		internal override void Exclude()
		{
			if (this._wrappedCachedValue.Entity == null)
			{
				if (base.DetachedEntityKey != null)
				{
					this.ExcludeEntityKey();
				}
				return;
			}
			TransactionManager transactionManager = this.ObjectContext.ObjectStateManager.TransactionManager;
			bool flag = transactionManager.PopulatedEntityReferences.Contains(this);
			bool flag2 = transactionManager.AlignedEntityReferences.Contains(this);
			if ((transactionManager.ProcessedEntities != null && transactionManager.ProcessedEntities.Contains(this._wrappedCachedValue)) || (!flag && !flag2))
			{
				base.ExcludeEntity(this._wrappedCachedValue);
				return;
			}
			RelationshipEntry relationshipEntry = base.IsForeignKey ? null : base.FindRelationshipEntryInObjectStateManager(this._wrappedCachedValue);
			base.Remove(this._wrappedCachedValue, flag, false, false, false, true);
			if (relationshipEntry != null && relationshipEntry.State != EntityState.Detached)
			{
				relationshipEntry.AcceptChanges();
			}
			if (flag)
			{
				transactionManager.PopulatedEntityReferences.Remove(this);
				return;
			}
			transactionManager.AlignedEntityReferences.Remove(this);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000F5DCC File Offset: 0x000F3FCC
		private void ExcludeEntityKey()
		{
			EntityKey entityKey = this.WrappedOwner.EntityKey;
			RelationshipEntry relationshipEntry = this.ObjectContext.ObjectStateManager.FindRelationship(this.RelationshipSet, new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.From, entityKey), new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.To, base.DetachedEntityKey));
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
				if (relationshipEntry.State != EntityState.Detached)
				{
					relationshipEntry.AcceptChanges();
				}
			}
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000F5E3C File Offset: 0x000F403C
		internal override void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			if (wrappedEntity == null)
			{
				wrappedEntity = NullEntityWrapper.NullWrapper;
			}
			if (this._wrappedCachedValue.Entity != null)
			{
				if (wrappedEntity.Entity == this._wrappedCachedValue.Entity && navigation.Equals(base.RelationshipNavigation))
				{
					base.Remove(this._wrappedCachedValue, false, false, false, false, false);
				}
				else
				{
					base.Remove(this._wrappedCachedValue, true, doCascadeDelete, false, true, false);
				}
			}
			else if (this.WrappedOwner.Entity != null && this.WrappedOwner.Context != null && !base.UsingNoTracking)
			{
				EntityEntry entityEntry = this.WrappedOwner.Context.ObjectStateManager.GetEntityEntry(this.WrappedOwner.Entity);
				entityEntry.DeleteRelationshipsThatReferenceKeys(this.RelationshipSet, this.ToEndMember);
			}
			if (this.WrappedOwner.Entity != null)
			{
				base.DetachedEntityKey = null;
			}
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000F5F10 File Offset: 0x000F4110
		internal override void ClearWrappedValues()
		{
			this._cachedValue = default(TEntity);
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000F5F29 File Offset: 0x000F4129
		internal override bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists)
		{
			if (!relationshipAlreadyExists && this.ContainsEntity(wrappedEntity))
			{
				return false;
			}
			this.VerifyType(wrappedEntity);
			return true;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000F5F41 File Offset: 0x000F4141
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000F5F51 File Offset: 0x000F4151
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_InvalidContainedType_Reference(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName));
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000F5F86 File Offset: 0x000F4186
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			this.CheckOwnerNull();
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000F5F8E File Offset: 0x000F418E
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			this.CheckOwnerNull();
			return false;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000F5F97 File Offset: 0x000F4197
		internal override bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
			this._cachedValue = default(TEntity);
			if (resetIsLoaded)
			{
				this._isLoaded = false;
			}
			if (this.ObjectContext != null && base.IsForeignKey && !preserveForeignKey)
			{
				base.NullAllForeignKeys();
			}
			return true;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000F5FD4 File Offset: 0x000F41D4
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.RemoveNavigationPropertyValue(this, wrappedEntity.Entity);
			}
			return true;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000F5FF8 File Offset: 0x000F41F8
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndMember)
					{
						if (visited.Contains(this._wrappedCachedValue))
						{
							throw new InvalidOperationException(Strings.RelationshipManager_CircularRelationshipsWithReferentialConstraints);
						}
						visited.Add(this._wrappedCachedValue);
						Dictionary<string, KeyValuePair<object, IntBox>> dictionary;
						this._wrappedCachedValue.RelationshipManager.RetrieveReferentialConstraintProperties(out dictionary, visited, true);
						for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
						{
							EntityEntry.AddOrIncreaseCounter(referentialConstraint, properties, referentialConstraint.ToProperties[i].Name, dictionary[referentialConstraint.FromProperties[i].Name].Key);
						}
					}
				}
			}
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000F6100 File Offset: 0x000F4300
		internal override bool IsEmpty()
		{
			return this._wrappedCachedValue.Entity == null;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000F6110 File Offset: 0x000F4310
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
			if (applyConstraints && !this.IsEmpty())
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000F613E File Offset: 0x000F433E
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000F6147 File Offset: 0x000F4347
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			return this._wrappedCachedValue.Entity != null && this._wrappedCachedValue.Entity == wrappedEntity.Entity;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000F616C File Offset: 0x000F436C
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			this.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000F618D File Offset: 0x000F438D
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000F6198 File Offset: 0x000F4398
		internal void InitializeWithValue(RelatedEnd relatedEnd)
		{
			EntityReference<TEntity> entityReference = relatedEnd as EntityReference<TEntity>;
			if (entityReference != null && entityReference._wrappedCachedValue.Entity != null)
			{
				this._wrappedCachedValue = entityReference._wrappedCachedValue;
				this._cachedValue = (TEntity)((object)this._wrappedCachedValue.Entity);
			}
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000F61E0 File Offset: 0x000F43E0
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			if (!base.TargetAccessor.HasProperty)
			{
				return false;
			}
			object navigationPropertyValue = this.WrappedOwner.GetNavigationPropertyValue(this);
			return object.ReferenceEquals(navigationPropertyValue, wrapper.Entity);
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000F6218 File Offset: 0x000F4418
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
			if (base.TargetAccessor.HasProperty)
			{
				object navigationPropertyValue = this.WrappedOwner.GetNavigationPropertyValue(this);
				if (!object.ReferenceEquals(null, navigationPropertyValue) && !object.ReferenceEquals(navigationPropertyValue, wrapper.Entity))
				{
					throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
				}
			}
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000F6277 File Offset: 0x000F4477
		[EditorBrowsable(EditorBrowsableState.Never)]
		[OnDeserialized]
		[Browsable(false)]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		public void OnRefDeserialized(StreamingContext context)
		{
			this._wrappedCachedValue = this.EntityWrapperFactory.WrapEntityUsingContext(this._cachedValue, this.ObjectContext);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000F629B File Offset: 0x000F449B
		[OnSerializing]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(this.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityReference"));
			}
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000F62C0 File Offset: 0x000F44C0
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (wrappedEntity != this._wrappedCachedValue)
			{
				TransactionManager transactionManager = (this.ObjectContext != null) ? this.ObjectContext.ObjectStateManager.TransactionManager : null;
				if (applyConstraints && this._wrappedCachedValue.Entity != null && (transactionManager == null || transactionManager.ProcessedEntities == null || transactionManager.ProcessedEntities.Contains(this._wrappedCachedValue)))
				{
					throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName));
				}
				if (transactionManager != null && wrappedEntity.Entity != null)
				{
					transactionManager.BeginRelatedEndAdd();
				}
				try
				{
					this.ClearCollectionOrRef(null, null, false);
					this._wrappedCachedValue = wrappedEntity;
					this._cachedValue = (TEntity)((object)wrappedEntity.Entity);
				}
				finally
				{
					if (transactionManager != null && transactionManager.IsRelatedEndAdd)
					{
						transactionManager.EndRelatedEndAdd();
					}
				}
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000F639C File Offset: 0x000F459C
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.SetNavigationPropertyValue(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x04001397 RID: 5015
		private TEntity _cachedValue;

		// Token: 0x04001398 RID: 5016
		[NonSerialized]
		private IEntityWrapper _wrappedCachedValue;
	}
}
