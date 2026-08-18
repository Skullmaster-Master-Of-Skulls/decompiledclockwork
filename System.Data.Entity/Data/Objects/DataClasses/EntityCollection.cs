using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public sealed class EntityCollection<TEntity> : RelatedEnd, ICollection<TEntity>, IEnumerable<TEntity>, IEnumerable, IListSource where TEntity : class
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x0005FE50 File Offset: 0x0005E050
		public EntityCollection()
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0005FE58 File Offset: 0x0005E058
		internal EntityCollection(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001C4C RID: 7244 RVA: 0x0005FE63 File Offset: 0x0005E063
		// (remove) Token: 0x06001C4D RID: 7245 RVA: 0x0005FE7C File Offset: 0x0005E07C
		internal override event CollectionChangeEventHandler AssociationChangedForObjectView
		{
			add
			{
				this._onAssociationChangedforObjectView = (CollectionChangeEventHandler)Delegate.Combine(this._onAssociationChangedforObjectView, value);
			}
			remove
			{
				this._onAssociationChangedforObjectView = (CollectionChangeEventHandler)Delegate.Remove(this._onAssociationChangedforObjectView, value);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0005FE95 File Offset: 0x0005E095
		private Dictionary<TEntity, IEntityWrapper> WrappedRelatedEntities
		{
			get
			{
				if (this._wrappedRelatedEntities == null)
				{
					this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>();
				}
				return this._wrappedRelatedEntities;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0005FEB0 File Offset: 0x0005E0B0
		public int Count
		{
			get
			{
				base.DeferredLoad();
				return this.CountInternal;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0005FEBE File Offset: 0x0005E0BE
		internal int CountInternal
		{
			get
			{
				if (this._wrappedRelatedEntities == null)
				{
					return 0;
				}
				return this._wrappedRelatedEntities.Count;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0005FED5 File Offset: 0x0005E0D5
		internal override void OnAssociationChanged(CollectionChangeAction collectionChangeAction, object entity)
		{
			if (!this._suppressEvents)
			{
				if (this._onAssociationChangedforObjectView != null)
				{
					this._onAssociationChangedforObjectView(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
				}
				if (this._onAssociationChanged != null)
				{
					this._onAssociationChanged(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
				}
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0005FF18 File Offset: 0x0005E118
		IList IListSource.GetList()
		{
			EntityType entityType = null;
			if (base.WrappedOwner.Entity != null && base.RelationshipSet != null)
			{
				EntitySet entitySet = ((AssociationSet)base.RelationshipSet).AssociationSetEnds[base.ToEndMember.Name].EntitySet;
				EntityType entityType2 = (EntityType)((RefType)((AssociationEndMember)base.ToEndMember).TypeUsage.EdmType).ElementType;
				EntityType elementType = entitySet.ElementType;
				if (entityType2.IsAssignableFrom(elementType))
				{
					entityType = elementType;
				}
				else
				{
					entityType = entityType2;
				}
			}
			return ObjectViewFactory.CreateViewForEntityCollection<TEntity>(entityType, this);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0005FFA6 File Offset: 0x0005E1A6
		public override void Load(MergeOption mergeOption)
		{
			base.CheckOwnerNull();
			this.Load(null, mergeOption);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0005FFB8 File Offset: 0x0005E1B8
		public void Attach(IEnumerable<TEntity> entities)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<TEntity>>(entities, "entities");
			base.CheckOwnerNull();
			IList<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (TEntity tentity in entities)
			{
				list.Add(EntityWrapperFactory.WrapEntityUsingContext(tentity, base.ObjectContext));
			}
			base.Attach(list, true);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00060030 File Offset: 0x0005E230
		public void Attach(TEntity entity)
		{
			EntityUtil.CheckArgumentNull<TEntity>(entity, "entity");
			base.Attach(new IEntityWrapper[]
			{
				EntityWrapperFactory.WrapEntityUsingContext(entity, base.ObjectContext)
			}, false);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00060060 File Offset: 0x0005E260
		internal void Load(List<IEntityWrapper> collection, MergeOption mergeOption)
		{
			bool flag;
			ObjectQuery<TEntity> query = base.ValidateLoad<TEntity>(mergeOption, "EntityCollection", out flag);
			this._suppressEvents = true;
			try
			{
				if (collection == null)
				{
					base.Merge<TEntity>(flag ? RelatedEnd.GetResults<TEntity>(query) : Enumerable.Empty<TEntity>(), mergeOption, true);
				}
				else
				{
					base.Merge<TEntity>(collection, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000600CC File Offset: 0x0005E2CC
		public void Add(TEntity entity)
		{
			EntityUtil.CheckArgumentNull<TEntity>(entity, "entity");
			base.Add(EntityWrapperFactory.WrapEntityUsingContext(entity, base.ObjectContext));
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000600F1 File Offset: 0x0005E2F1
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw EntityUtil.UnableToAddToDisconnectedRelatedEnd();
			}
			this.VerifyType(wrappedEntity);
			base.AddToCache(wrappedEntity, false);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedEntity.Entity);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00060128 File Offset: 0x0005E328
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw EntityUtil.UnableToRemoveFromDisconnectedRelatedEnd();
			}
			bool result = base.RemoveFromCache(wrappedEntity, false, false);
			this.OnAssociationChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
			return result;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00060164 File Offset: 0x0005E364
		public bool Remove(TEntity entity)
		{
			EntityUtil.CheckArgumentNull<TEntity>(entity, "entity");
			base.DeferredLoad();
			return this.RemoveInternal(entity);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006017F File Offset: 0x0005E37F
		internal bool RemoveInternal(TEntity entity)
		{
			return base.Remove(EntityWrapperFactory.WrapEntityUsingContext(entity, base.ObjectContext), false);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006019C File Offset: 0x0005E39C
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedRelatedEntities != null && base.ObjectContext != null)
			{
				List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
				foreach (IEntityWrapper entityWrapper in list)
				{
					IEntityWrapper entityWrapper2 = EntityWrapperFactory.WrapEntityUsingContext(entityWrapper.Entity, base.WrappedOwner.Context);
					if (entityWrapper2 != entityWrapper)
					{
						this._wrappedRelatedEntities[(TEntity)((object)entityWrapper2.Entity)] = entityWrapper2;
					}
					base.IncludeEntity(entityWrapper2, addRelationshipAsUnchanged, doAttach);
				}
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00060240 File Offset: 0x0005E440
		internal override void Exclude()
		{
			if (this._wrappedRelatedEntities != null && base.ObjectContext != null)
			{
				if (!base.IsForeignKey)
				{
					using (Dictionary<TEntity, IEntityWrapper>.ValueCollection.Enumerator enumerator = this._wrappedRelatedEntities.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IEntityWrapper wrappedEntity = enumerator.Current;
							base.ExcludeEntity(wrappedEntity);
						}
						return;
					}
				}
				TransactionManager transactionManager = base.ObjectContext.ObjectStateManager.TransactionManager;
				List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
				foreach (IEntityWrapper wrappedEntity2 in list)
				{
					EntityReference entityReference = base.GetOtherEndOfRelationship(wrappedEntity2) as EntityReference;
					bool flag = transactionManager.PopulatedEntityReferences.Contains(entityReference);
					bool flag2 = transactionManager.AlignedEntityReferences.Contains(entityReference);
					if (flag || flag2)
					{
						entityReference.Remove(entityReference.CachedValue, flag, false, false, false, true);
						if (flag)
						{
							transactionManager.PopulatedEntityReferences.Remove(entityReference);
						}
						else
						{
							transactionManager.AlignedEntityReferences.Remove(entityReference);
						}
					}
					else
					{
						base.ExcludeEntity(wrappedEntity2);
					}
				}
			}
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0006038C File Offset: 0x0005E58C
		internal override void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			if (this._wrappedRelatedEntities != null)
			{
				List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
				foreach (IEntityWrapper entityWrapper in list)
				{
					if (wrappedEntity.Entity == entityWrapper.Entity && navigation.Equals(base.RelationshipNavigation))
					{
						base.Remove(entityWrapper, false, false, false, false, false);
					}
					else
					{
						base.Remove(entityWrapper, true, doCascadeDelete, false, false, false);
					}
				}
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00060424 File Offset: 0x0005E624
		internal override void ClearWrappedValues()
		{
			if (this._wrappedRelatedEntities != null)
			{
				this._wrappedRelatedEntities.Clear();
			}
			if (this._relatedEntities != null)
			{
				this._relatedEntities.Clear();
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0006044C File Offset: 0x0005E64C
		internal override bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists)
		{
			if (!relationshipAlreadyExists && this.ContainsEntity(wrappedEntity))
			{
				return false;
			}
			this.VerifyType(wrappedEntity);
			return true;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00060464 File Offset: 0x0005E664
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00060474 File Offset: 0x0005E674
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw EntityUtil.InvalidContainedTypeCollection(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName);
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000604A4 File Offset: 0x0005E6A4
		internal override bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			if (this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.Remove((TEntity)((object)wrappedEntity.Entity)))
			{
				if (resetIsLoaded)
				{
					this._isLoaded = false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000604D3 File Offset: 0x0005E6D3
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			return base.TargetAccessor.HasProperty && base.WrappedOwner.CollectionRemove(this, (TEntity)((object)wrappedEntity.Entity));
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00060500 File Offset: 0x0005E700
		internal override bool IsEmpty()
		{
			return this._wrappedRelatedEntities == null || this._wrappedRelatedEntities.Count == 0;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0006051A File Offset: 0x0005E71A
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00060524 File Offset: 0x0005E724
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			TEntity key = wrappedEntity.Entity as TEntity;
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey(key);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00060558 File Offset: 0x0005E758
		public new IEnumerator<TEntity> GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00060558 File Offset: 0x0005E758
		IEnumerator IEnumerable.GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00060575 File Offset: 0x0005E775
		internal override IEnumerable GetInternalEnumerable()
		{
			return this.WrappedRelatedEntities.Keys;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00060582 File Offset: 0x0005E782
		internal override IEnumerable<IEntityWrapper> GetWrappedEntities()
		{
			return this.WrappedRelatedEntities.Values;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00060590 File Offset: 0x0005E790
		public void Clear()
		{
			base.DeferredLoad();
			if (base.WrappedOwner.Entity != null)
			{
				bool flag = this.CountInternal > 0;
				if (this._wrappedRelatedEntities != null)
				{
					List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
					try
					{
						this._suppressEvents = true;
						foreach (IEntityWrapper wrappedEntity in list)
						{
							base.Remove(wrappedEntity, false);
							if (base.UsingNoTracking)
							{
								RelatedEnd otherEndOfRelationship = base.GetOtherEndOfRelationship(wrappedEntity);
								otherEndOfRelationship.OnRelatedEndClear();
							}
						}
					}
					finally
					{
						this._suppressEvents = false;
					}
					if (base.UsingNoTracking)
					{
						this._isLoaded = false;
					}
				}
				if (flag)
				{
					this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
					return;
				}
			}
			else if (this._wrappedRelatedEntities != null)
			{
				this._wrappedRelatedEntities.Clear();
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0006067C File Offset: 0x0005E87C
		public bool Contains(TEntity entity)
		{
			base.DeferredLoad();
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey(entity);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0006069A File Offset: 0x0005E89A
		public void CopyTo(TEntity[] array, int arrayIndex)
		{
			base.DeferredLoad();
			this.WrappedRelatedEntities.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000606B4 File Offset: 0x0005E8B4
		internal override void BulkDeleteAll(List<object> list)
		{
			if (list.Count > 0)
			{
				this._suppressEvents = true;
				try
				{
					foreach (object obj in list)
					{
						this.RemoveInternal(obj as TEntity);
					}
				}
				finally
				{
					this._suppressEvents = false;
				}
				this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006073C File Offset: 0x0005E93C
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			if (!base.TargetAccessor.HasProperty)
			{
				return false;
			}
			object navigationPropertyValue = base.WrappedOwner.GetNavigationPropertyValue(this);
			if (navigationPropertyValue != null)
			{
				if (!(navigationPropertyValue is IEnumerable))
				{
					throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(base.TargetAccessor.PropertyName, base.WrappedOwner.Entity.GetType().FullName));
				}
				foreach (object objA in (navigationPropertyValue as IEnumerable))
				{
					if (object.Equals(objA, wrapper.Entity))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000607F4 File Offset: 0x0005E9F4
		[OnSerializing]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(base.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityCollection"));
			}
			this._relatedEntities = ((this._wrappedRelatedEntities == null) ? null : new HashSet<TEntity>(this._wrappedRelatedEntities.Keys));
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00060844 File Offset: 0x0005EA44
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnCollectionDeserialized(StreamingContext context)
		{
			if (this._relatedEntities != null)
			{
				this._relatedEntities.OnDeserialization(null);
				this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>();
				foreach (TEntity tentity in this._relatedEntities)
				{
					this._wrappedRelatedEntities.Add(tentity, EntityWrapperFactory.WrapEntityUsingContext(tentity, base.ObjectContext));
				}
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000608CC File Offset: 0x0005EACC
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			base.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000608ED File Offset: 0x0005EAED
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000608F5 File Offset: 0x0005EAF5
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.WrappedRelatedEntities[(TEntity)((object)wrappedEntity.Entity)] = wrappedEntity;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0006090E File Offset: 0x0005EB0E
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				base.WrappedOwner.CollectionAdd(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x04000BAE RID: 2990
		private HashSet<TEntity> _relatedEntities;

		// Token: 0x04000BAF RID: 2991
		[NonSerialized]
		private CollectionChangeEventHandler _onAssociationChangedforObjectView;

		// Token: 0x04000BB0 RID: 2992
		[NonSerialized]
		private Dictionary<TEntity, IEntityWrapper> _wrappedRelatedEntities;
	}
}
