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

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200053C RID: 1340
	[Serializable]
	public class EntityCollection<TEntity> : RelatedEnd, ICollection<TEntity>, IEnumerable<!0>, IEnumerable, IListSource where TEntity : class
	{
		// Token: 0x0600338E RID: 13198 RVA: 0x000F3359 File Offset: 0x000F1559
		public EntityCollection()
		{
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000F3361 File Offset: 0x000F1561
		internal EntityCollection(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06003390 RID: 13200 RVA: 0x000F336C File Offset: 0x000F156C
		// (remove) Token: 0x06003391 RID: 13201 RVA: 0x000F3385 File Offset: 0x000F1585
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

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000F339E File Offset: 0x000F159E
		private Dictionary<TEntity, IEntityWrapper> WrappedRelatedEntities
		{
			get
			{
				if (this._wrappedRelatedEntities == null)
				{
					this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
				}
				return this._wrappedRelatedEntities;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000F33BE File Offset: 0x000F15BE
		public int Count
		{
			get
			{
				base.DeferredLoad();
				return this.CountInternal;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000F33CC File Offset: 0x000F15CC
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000F33E3 File Offset: 0x000F15E3
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000F33E6 File Offset: 0x000F15E6
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000F33E9 File Offset: 0x000F15E9
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

		// Token: 0x06003398 RID: 13208 RVA: 0x000F342C File Offset: 0x000F162C
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			EntityType entityType = null;
			if (this.WrappedOwner.Entity != null && this.RelationshipSet != null)
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.ToEndMember.Name].EntitySet;
				EntityType entityType2 = (EntityType)((RefType)this.ToEndMember.TypeUsage.EdmType).ElementType;
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

		// Token: 0x06003399 RID: 13209 RVA: 0x000F34B5 File Offset: 0x000F16B5
		public override void Load(MergeOption mergeOption)
		{
			this.CheckOwnerNull();
			this.Load(null, mergeOption);
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000F34C5 File Offset: 0x000F16C5
		public override Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			this.CheckOwnerNull();
			cancellationToken.ThrowIfCancellationRequested();
			return this.LoadAsync(null, mergeOption, cancellationToken);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000F34E0 File Offset: 0x000F16E0
		public void Attach(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.CheckOwnerNull();
			IList<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (TEntity tentity in entities)
			{
				list.Add(this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext));
			}
			base.Attach(list, true);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000F3560 File Offset: 0x000F1760
		public void Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			base.Attach(new IEntityWrapper[]
			{
				this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext)
			}, false);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000F35A4 File Offset: 0x000F17A4
		internal virtual void Load(List<IEntityWrapper> collection, MergeOption mergeOption)
		{
			bool flag;
			ObjectQuery<TEntity> objectQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityCollection", out flag);
			this._suppressEvents = true;
			try
			{
				if (collection == null)
				{
					IEnumerable<TEntity> collection2;
					if (flag)
					{
						collection2 = objectQuery.Execute(objectQuery.MergeOption);
					}
					else
					{
						collection2 = Enumerable.Empty<TEntity>();
					}
					this.Merge<TEntity>(collection2, mergeOption, true);
				}
				else
				{
					this.Merge<TEntity>(collection, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000F3888 File Offset: 0x000F1A88
		internal virtual async Task LoadAsync(List<IEntityWrapper> collection, MergeOption mergeOption, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			bool hasResults;
			ObjectQuery<TEntity> sourceQuery = this.ValidateLoad<TEntity>(mergeOption, "EntityCollection", out hasResults);
			this._suppressEvents = true;
			try
			{
				if (collection == null)
				{
					IEnumerable<TEntity> refreshedValues;
					if (hasResults)
					{
						ObjectResult<TEntity> queryResult = await sourceQuery.ExecuteAsync(sourceQuery.MergeOption, cancellationToken).WithCurrentCulture<ObjectResult<TEntity>>();
						refreshedValues = await queryResult.ToListAsync(cancellationToken).WithCurrentCulture<List<TEntity>>();
					}
					else
					{
						refreshedValues = Enumerable.Empty<TEntity>();
					}
					this.Merge<TEntity>(refreshedValues, mergeOption, true);
				}
				else
				{
					this.Merge<TEntity>(collection, mergeOption, true);
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000F38E6 File Offset: 0x000F1AE6
		public void Add(TEntity item)
		{
			Check.NotNull<TEntity>(item, "item");
			base.Add(this.EntityWrapperFactory.WrapEntityUsingContext(item, this.ObjectContext));
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000F3911 File Offset: 0x000F1B11
		internal override void DisconnectedAdd(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddEntity);
			}
			this.VerifyType(wrappedEntity);
			base.AddToCache(wrappedEntity, false);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedEntity.Entity);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000F394C File Offset: 0x000F1B4C
		internal override bool DisconnectedRemove(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity.Context != null && wrappedEntity.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelatedEnd_UnableToRemoveEntity);
			}
			bool result = base.RemoveFromCache(wrappedEntity, false, false);
			this.OnAssociationChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
			return result;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000F398D File Offset: 0x000F1B8D
		public bool Remove(TEntity item)
		{
			Check.NotNull<TEntity>(item, "item");
			base.DeferredLoad();
			return this.RemoveInternal(item);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000F39A8 File Offset: 0x000F1BA8
		internal bool RemoveInternal(TEntity entity)
		{
			return base.Remove(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext), false);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000F39C8 File Offset: 0x000F1BC8
		internal override void Include(bool addRelationshipAsUnchanged, bool doAttach)
		{
			if (this._wrappedRelatedEntities != null && this.ObjectContext != null)
			{
				List<IEntityWrapper> list = new List<IEntityWrapper>(this._wrappedRelatedEntities.Values);
				foreach (IEntityWrapper entityWrapper in list)
				{
					IEntityWrapper entityWrapper2 = this.EntityWrapperFactory.WrapEntityUsingContext(entityWrapper.Entity, this.WrappedOwner.Context);
					if (entityWrapper2 != entityWrapper)
					{
						this._wrappedRelatedEntities[(TEntity)((object)entityWrapper2.Entity)] = entityWrapper2;
					}
					base.IncludeEntity(entityWrapper2, addRelationshipAsUnchanged, doAttach);
				}
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000F3A74 File Offset: 0x000F1C74
		internal override void Exclude()
		{
			if (this._wrappedRelatedEntities != null && this.ObjectContext != null)
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
				TransactionManager transactionManager = this.ObjectContext.ObjectStateManager.TransactionManager;
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

		// Token: 0x060033A6 RID: 13222 RVA: 0x000F3BC0 File Offset: 0x000F1DC0
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

		// Token: 0x060033A7 RID: 13223 RVA: 0x000F3C58 File Offset: 0x000F1E58
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

		// Token: 0x060033A8 RID: 13224 RVA: 0x000F3C80 File Offset: 0x000F1E80
		internal override bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists)
		{
			if (!relationshipAlreadyExists && this.ContainsEntity(wrappedEntity))
			{
				return false;
			}
			this.VerifyType(wrappedEntity);
			return true;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000F3C98 File Offset: 0x000F1E98
		internal override bool CanSetEntityType(IEntityWrapper wrappedEntity)
		{
			return wrappedEntity.Entity is TEntity;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000F3CA8 File Offset: 0x000F1EA8
		internal override void VerifyType(IEntityWrapper wrappedEntity)
		{
			if (!this.CanSetEntityType(wrappedEntity))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_InvalidContainedType_Collection(wrappedEntity.Entity.GetType().FullName, typeof(TEntity).FullName));
			}
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000F3CDD File Offset: 0x000F1EDD
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

		// Token: 0x060033AC RID: 13228 RVA: 0x000F3D0C File Offset: 0x000F1F0C
		internal override bool RemoveFromObjectCache(IEntityWrapper wrappedEntity)
		{
			return base.TargetAccessor.HasProperty && this.WrappedOwner.CollectionRemove(this, wrappedEntity.Entity);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000F3D2F File Offset: 0x000F1F2F
		internal override void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited)
		{
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000F3D31 File Offset: 0x000F1F31
		internal override bool IsEmpty()
		{
			return this._wrappedRelatedEntities == null || this._wrappedRelatedEntities.Count == 0;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000F3D4B File Offset: 0x000F1F4B
		internal override void VerifyMultiplicityConstraintsForAdd(bool applyConstraints)
		{
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000F3D4D File Offset: 0x000F1F4D
		internal override void OnRelatedEndClear()
		{
			this._isLoaded = false;
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000F3D58 File Offset: 0x000F1F58
		internal override bool ContainsEntity(IEntityWrapper wrappedEntity)
		{
			TEntity key = wrappedEntity.Entity as TEntity;
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey(key);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000F3D8C File Offset: 0x000F1F8C
		public new IEnumerator<TEntity> GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000F3DA9 File Offset: 0x000F1FA9
		IEnumerator IEnumerable.GetEnumerator()
		{
			base.DeferredLoad();
			return this.WrappedRelatedEntities.Keys.GetEnumerator();
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000F3DC6 File Offset: 0x000F1FC6
		internal override IEnumerable GetInternalEnumerable()
		{
			return this.WrappedRelatedEntities.Keys;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000F3DD3 File Offset: 0x000F1FD3
		internal override IEnumerable<IEntityWrapper> GetWrappedEntities()
		{
			return this.WrappedRelatedEntities.Values;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000F3DE0 File Offset: 0x000F1FE0
		public void Clear()
		{
			base.DeferredLoad();
			if (this.WrappedOwner.Entity != null)
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

		// Token: 0x060033B7 RID: 13239 RVA: 0x000F3ECC File Offset: 0x000F20CC
		public bool Contains(TEntity item)
		{
			base.DeferredLoad();
			return this._wrappedRelatedEntities != null && this._wrappedRelatedEntities.ContainsKey(item);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000F3EEA File Offset: 0x000F20EA
		public void CopyTo(TEntity[] array, int arrayIndex)
		{
			base.DeferredLoad();
			this.WrappedRelatedEntities.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000F3F04 File Offset: 0x000F2104
		internal virtual void BulkDeleteAll(List<object> list)
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

		// Token: 0x060033BA RID: 13242 RVA: 0x000F3FAC File Offset: 0x000F21AC
		internal override bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper)
		{
			if (!base.TargetAccessor.HasProperty)
			{
				return false;
			}
			bool state = base.DisableLazyLoading();
			try
			{
				object navigationPropertyValue = this.WrappedOwner.GetNavigationPropertyValue(this);
				if (navigationPropertyValue != null)
				{
					IEnumerable<TEntity> enumerable = navigationPropertyValue as IEnumerable<TEntity>;
					if (enumerable == null)
					{
						throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(base.TargetAccessor.PropertyName, this.WrappedOwner.Entity.GetType().FullName));
					}
					HashSet<TEntity> hashSet = navigationPropertyValue as HashSet<TEntity>;
					if (!wrapper.OverridesEqualsOrGetHashCode || (hashSet != null && hashSet.Comparer is ObjectReferenceEqualityComparer))
					{
						return enumerable.Contains((TEntity)((object)wrapper.Entity));
					}
					return enumerable.Any((TEntity o) => object.ReferenceEquals(o, wrapper.Entity));
				}
			}
			finally
			{
				base.ResetLazyLoading(state);
			}
			return false;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000F40A8 File Offset: 0x000F22A8
		internal override void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper)
		{
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000F40AC File Offset: 0x000F22AC
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[OnSerializing]
		[Browsable(false)]
		public void OnSerializing(StreamingContext context)
		{
			if (!(this.WrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("EntityCollection"));
			}
			this._relatedEntities = ((this._wrappedRelatedEntities == null) ? null : new HashSet<TEntity>(this._wrappedRelatedEntities.Keys, ObjectReferenceEqualityComparer.Default));
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000F4104 File Offset: 0x000F2304
		[Browsable(false)]
		[OnDeserialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		public void OnCollectionDeserialized(StreamingContext context)
		{
			if (this._relatedEntities != null)
			{
				this._relatedEntities.OnDeserialization(null);
				this._wrappedRelatedEntities = new Dictionary<TEntity, IEntityWrapper>(ObjectReferenceEqualityComparer.Default);
				foreach (TEntity tentity in this._relatedEntities)
				{
					this._wrappedRelatedEntities.Add(tentity, this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext));
				}
			}
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000F4198 File Offset: 0x000F2398
		public ObjectQuery<TEntity> CreateSourceQuery()
		{
			this.CheckOwnerNull();
			bool flag;
			return base.CreateSourceQuery<TEntity>(base.DefaultMergeOption, out flag);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000F41B9 File Offset: 0x000F23B9
		internal override IEnumerable CreateSourceQueryInternal()
		{
			return this.CreateSourceQuery();
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000F41C1 File Offset: 0x000F23C1
		internal override void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.WrappedRelatedEntities[(TEntity)((object)wrappedEntity.Entity)] = wrappedEntity;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000F41DA File Offset: 0x000F23DA
		internal override void AddToObjectCache(IEntityWrapper wrappedEntity)
		{
			if (base.TargetAccessor.HasProperty)
			{
				this.WrappedOwner.CollectionAdd(this, wrappedEntity.Entity);
			}
		}

		// Token: 0x0400138D RID: 5005
		private HashSet<TEntity> _relatedEntities;

		// Token: 0x0400138E RID: 5006
		[NonSerialized]
		private CollectionChangeEventHandler _onAssociationChangedforObjectView;

		// Token: 0x0400138F RID: 5007
		[NonSerialized]
		private Dictionary<TEntity, IEntityWrapper> _wrappedRelatedEntities;
	}
}
