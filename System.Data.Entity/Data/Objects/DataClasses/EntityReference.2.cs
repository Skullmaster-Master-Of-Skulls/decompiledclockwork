using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018F RID: 399
	[DataContract]
	[Serializable]
	public sealed class EntityReference<TEntity> : EntityReference where TEntity : class
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x0006151C File Offset: 0x0005F71C
		public EntityReference()
		{
			this._wrappedCachedValue = EntityWrapperFactory.NullWrapper;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0006152F File Offset: 0x0005F72F
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
			this._wrappedCachedValue = EntityWrapperFactory.NullWrapper;
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00061545 File Offset: 0x0005F745
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0006155D File Offset: 0x0005F75D
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
				this.ReferenceValue = EntityWrapperFactory.WrapEntityUsingContext(value, base.ObjectContext);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00061576 File Offset: 0x0005F776
		internal override IEntityWrapper CachedValue
		{
			get
			{
				return this._wrappedCachedValue;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0006157E File Offset: 0x0005F77E
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0006158C File Offset: 0x0005F78C
		internal override IEntityWrapper ReferenceValue
		{
			get
			{
				base.CheckOwnerNull();
				return this._wrappedCachedValue;
			}
			set
			{
				base.CheckOwnerNull();
				if (value.Entity != null && value.Entity == this._wrappedCachedValue.Entity)
				{
					return;
				}
				if (value.Entity != null)
				{
					base.ValidateOwnerWithRIConstraints(value, (value == EntityWrapperFactory.NullWrapper) ? null : value.EntityKey, true);
					ObjectContext objectContext = base.ObjectContext ?? value.Context;
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
				else if (base.ObjectContext != null && base.ObjectContext.ContextOptions.UseConsistentNullReferenceBehavior)
				{
					base.AttemptToNullFKsOnRefOrKeySetToNull();
				}
				this.ClearCollectionOrRef(null, null, false);
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00061698 File Offset: 0x0005F898
		public override void Load(MergeOption mergeOption)
		{
			base.CheckOwnerNull();
			bool flag;
			ObjectQuery<TEntity> query = base.ValidateLoad<TEntity>(mergeOption, "EntityReference", out flag);
			this._suppressEvents = true;
			try
			{
				List<TEntity> list = null;
				if (flag)
				{
					list = new List<TEntity>(RelatedEnd.GetResults<TEntity>(query));
				}
				if (list == null || list.Count == 0)
				{
					if (!((AssociationType)base.RelationMetadata).IsForeignKey && base.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
					{
						throw EntityUtil.LessThanExpectedRelatedEntitiesFound();
					}
					if (mergeOption == MergeOption.OverwriteChanges || mergeOption == MergeOption.PreserveChanges)
					{
						EntityKey entityKey = base.WrappedOwner.EntityKey;
						EntityUtil.CheckEntityKeyNull(entityKey);
						ObjectStateManager.RemoveRelationships(base.ObjectContext, mergeOption, (AssociationSet)base.RelationshipSet, entityKey, (AssociationEndMember)base.FromEndProperty);
					}
					this._isLoaded = true;
				}
				else
				{
					if (list.Count != 1)
					{
						throw EntityUtil.MoreThanExpectedRelatedEntitiesFound();
					}
					base.Merge<TEntity>(list, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00061784 File Offset: 0x0005F984
		internal override IEnumerable GetInternalEnumerable()
		{
			if (this.ReferenceValue.Entity != null)
			{
				yield return (TEntity)((object)this.ReferenceValue.Entity);
			}
			yield break;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00061794 File Offset: 0x0005F994
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

		// Token: 0x06001C9D RID: 7325 RVA: 0x000617B9 File Offset: 0x0005F9B9
		public void Attach(TEntity entity)
		{
			base.CheckOwnerNull();
			EntityUtil.CheckArgumentNull<TEntity>(entity, "entity");
			base.Attach(new IEntityWrapper[]
			{
				EntityWrapperFactory.WrapEntityUsingContext(entity, base.ObjectContext)
			}, false);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000617F0 File Offset: 0x0005F9F0
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(this._wrappedCachedValue.Entity, base.WrappedOwner.Context);
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

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006185C File Offset: 0x0005FA5C
		private void IncludeEntityKey(bool doAttach)
		{
			ObjectStateManager objectStateManager = base.ObjectContext.ObjectStateManager;
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
				if (base.FromEndProperty.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					foreach (RelationshipEntry relationshipEntry in base.ObjectContext.ObjectStateManager.FindRelationshipsByKey(base.DetachedEntityKey))
					{
						if (relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)base.RelationshipSet, (AssociationEndMember)base.ToEndMember, base.DetachedEntityKey) && relationshipEntry.State != EntityState.Deleted)
						{
							throw EntityUtil.EntityConflictsWithKeyEntry();
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
					throw EntityUtil.UnableToAddRelationshipWithDeletedEntity();
				}
				RelatedEnd relatedEndInternal = wrappedEntity.RelationshipManager.GetRelatedEndInternal(base.RelationshipName, base.RelationshipNavigation.From);
				if (base.FromEndProperty.RelationshipMultiplicity != RelationshipMultiplicity.Many && !relatedEndInternal.IsEmpty())
				{
					throw EntityUtil.EntityConflictsWithKeyEntry();
				}
				base.Add(wrappedEntity, true, doAttach, false, true, true);
				objectStateManager.TransactionManager.PopulatedEntityReferences.Add(this);
			}
			if (flag && !base.IsForeignKey)
			{
				if (flag2)
				{
					EntitySet entitySet = base.DetachedEntityKey.GetEntitySet(base.ObjectContext.MetadataWorkspace);
					objectStateManager.AddKeyEntry(base.DetachedEntityKey, entitySet);
				}
				EntityKey entityKey = base.WrappedOwner.EntityKey;
				EntityUtil.CheckEntityKeyNull(entityKey);
				RelationshipWrapper wrapper = new RelationshipWrapper((AssociationSet)base.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, base.DetachedEntityKey);
				objectStateManager.AddNewRelation(wrapper, doAttach ? EntityState.Unchanged : EntityState.Added);
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00061A38 File Offset: 0x0005FC38
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
			TransactionManager transactionManager = base.ObjectContext.ObjectStateManager.TransactionManager;
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

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00061B1C File Offset: 0x0005FD1C
		private void ExcludeEntityKey()
		{
			EntityKey entityKey = base.WrappedOwner.EntityKey;
			RelationshipEntry relationshipEntry = base.ObjectContext.ObjectStateManager.FindRelationship(base.RelationshipSet, new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.From, entityKey), new KeyValuePair<string, EntityKey>(base.RelationshipNavigation.To, base.DetachedEntityKey));
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
				if (relationshipEntry.State != EntityState.Detached)
				{
					relationshipEntry.AcceptChanges();
				}
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00061B8C File Offset: 0x0005FD8C
		internal override void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			if (wrappedEntity == null)
			{
				wrappedEntity = EntityWrapperFactory.NullWrapper;
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
			else if (base.WrappedOwner.Entity != null && base.WrappedOwner.Context != null && !base.UsingNoTracking)
			{
				EntityEntry entityEntry = base.WrappedOwner.Context.ObjectStateManager.GetEntityEntry(base.WrappedOwner.Entity);
				entityEntry.DeleteRelationshipsThatReferenceKeys(base.RelationshipSet, base.ToEndMember);
			}
			if (base.WrappedOwner.Entity != null)
			{
				base.DetachedEntityKey = null;
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00061C60 File Offset: 0x0005FE60
		internal override void ClearWrappedValues()
		{
			this._cachedValue = default(TEntity);
			this._wrappedCachedValue = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006044C File Offset: 0x0005E64C
		internal override bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists)
		{
			if (!relationshipAlreadyExists && this.ContainsEntity(wrappedEntity))
			{
				return false;
			}
			this.VerifyType(wrappedEntity);
			return true;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00061C79 File Offset: 0x0005FE79
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00061C89 File Offset: 0x0005FE89
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw EntityUtil.InvalidContainedTypeReference(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName);
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00061CB9 File Offset: 0x0005FEB9
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			base.CheckOwnerNull();
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00061CC1 File Offset: 0x0005FEC1
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			base.CheckOwnerNull();
			return false;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00061CCA File Offset: 0x0005FECA
		internal override bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			this._wrappedCachedValue = EntityWrapperFactory.NullWrapper;
			this._cachedValue = default(TEntity);
			if (resetIsLoaded)
			{
				this._isLoaded = false;
			}
			if (base.ObjectContext != null && base.IsForeignKey && !preserveForeignKey)
			{
				base.NullAllForeignKeys();
			}
			return true;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00061D07 File Offset: 0x0005FF07
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				base.WrappedOwner.RemoveNavigationPropertyValue(this, (TEntity)((object)wrappedEntity.Entity));
			}
			return true;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00061D34 File Offset: 0x0005FF34
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
			if (this._wrappedCachedValue.Entity != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)base.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == base.FromEndProperty)
					{
						if (visited.Contains(this._wrappedCachedValue))
						{
							throw EntityUtil.CircularRelationshipsWithReferentialConstraints();
						}
						visited.Add(this._wrappedCachedValue);
						Dictionary<string, KeyValuePair<object, IntBox>> dictionary;
						this._wrappedCachedValue.RelationshipManager.RetrieveReferentialConstraintProperties(out dictionary, visited, true);
						for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
						{
							EntityEntry.AddOrIncreaseCounter(properties, referentialConstraint.ToProperties[i].Name, dictionary[referentialConstraint.FromProperties[i].Name].Key);
						}
					}
				}
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00061E34 File Offset: 0x00060034
		internal override bool IsEmpty()
		{
			return this._wrappedCachedValue.Entity == null;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00061E44 File Offset: 0x00060044
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
			if (applyConstraints && !this.IsEmpty())
			{
				throw EntityUtil.CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName);
			}
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006051A File Offset: 0x0005E71A
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00061E6D File Offset: 0x0006006D
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			return this._wrappedCachedValue.Entity != null && this._wrappedCachedValue.Entity == wrappedEntity.Entity;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00061E94 File Offset: 0x00060094
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			base.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00061EB5 File Offset: 0x000600B5
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00061EC0 File Offset: 0x000600C0
		internal void InitializeWithValue(RelatedEnd relatedEnd)
		{
			EntityReference<TEntity> entityReference = relatedEnd as EntityReference<TEntity>;
			if (entityReference != null && entityReference._wrappedCachedValue.Entity != null)
			{
				this._wrappedCachedValue = entityReference._wrappedCachedValue;
				this._cachedValue = (TEntity)((object)this._wrappedCachedValue.Entity);
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00061F08 File Offset: 0x00060108
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			if (!base.TargetAccessor.HasProperty)
			{
				return false;
			}
			object navigationPropertyValue = base.WrappedOwner.GetNavigationPropertyValue(this);
			return object.Equals(navigationPropertyValue, wrapper.Entity);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00061F40 File Offset: 0x00060140
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
			if (base.TargetAccessor.HasProperty)
			{
				object navigationPropertyValue = base.WrappedOwner.GetNavigationPropertyValue(this);
				if (navigationPropertyValue != null && !object.Equals(navigationPropertyValue, wrapper.Entity))
				{
					throw EntityUtil.CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName);
				}
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00061F94 File Offset: 0x00060194
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnRefDeserialized(StreamingContext context)
		{
			this._wrappedCachedValue = EntityWrapperFactory.WrapEntityUsingContext(this._cachedValue, base.ObjectContext);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00061FB2 File Offset: 0x000601B2
		[OnSerializing]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(base.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityReference"));
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00061FD8 File Offset: 0x000601D8
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (wrappedEntity != this._wrappedCachedValue)
			{
				TransactionManager transactionManager = (base.ObjectContext != null) ? base.ObjectContext.ObjectStateManager.TransactionManager : null;
				if (applyConstraints && this._wrappedCachedValue.Entity != null && (transactionManager == null || transactionManager.ProcessedEntities == null || transactionManager.ProcessedEntities.Contains(this._wrappedCachedValue)))
				{
					throw EntityUtil.CannotAddMoreThanOneEntityToEntityReference(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName);
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

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000620B0 File Offset: 0x000602B0
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				base.WrappedOwner.SetNavigationPropertyValue(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x04000BB3 RID: 2995
		private TEntity _cachedValue;

		// Token: 0x04000BB4 RID: 2996
		[NonSerialized]
		private IEntityWrapper _wrappedCachedValue;
	}
}
