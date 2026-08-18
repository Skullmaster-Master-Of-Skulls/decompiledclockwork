using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000179 RID: 377
	internal abstract class EntityWrapper<TEntity> : BaseEntityWrapper<TEntity>
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x0005EDE0 File Offset: 0x0005CFE0
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, relationshipManager)
		{
			if (relationshipManager == null)
			{
				throw EntityUtil.UnexpectedNullRelationshipManager();
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0005EE40 File Offset: 0x0005D040
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntityKey key, EntitySet set, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, relationshipManager, set, context, mergeOption, identityType)
		{
			if (relationshipManager == null)
			{
				throw EntityUtil.UnexpectedNullRelationshipManager();
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
			this._keyStrategy.SetEntityKey(key);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0005EEB2 File Offset: 0x0005D0B2
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._changeTrackingStrategy.SetChangeTracker(changeTracker);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0005EEC0 File Offset: 0x0005D0C0
		public override void TakeSnapshot(EntityEntry entry)
		{
			this._changeTrackingStrategy.TakeSnapshot(entry);
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0005EECE File Offset: 0x0005D0CE
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x0005EEDB File Offset: 0x0005D0DB
		public override EntityKey EntityKey
		{
			get
			{
				return this._keyStrategy.GetEntityKey();
			}
			set
			{
				this._keyStrategy.SetEntityKey(value);
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0005EEE9 File Offset: 0x0005D0E9
		public override EntityKey GetEntityKeyFromEntity()
		{
			return this._keyStrategy.GetEntityKeyFromEntity();
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0005EEF6 File Offset: 0x0005D0F6
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.CollectionAdd(relatedEnd, value);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0005EF0D File Offset: 0x0005D10D
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return this._propertyStrategy != null && this._propertyStrategy.CollectionRemove(relatedEnd, value);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0005EF28 File Offset: 0x0005D128
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy != null && this._propertyStrategy.GetNavigationPropertyValue(relatedEnd) == null)
			{
				object value = this._propertyStrategy.CollectionCreate(relatedEnd);
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, value);
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0005EF67 File Offset: 0x0005D167
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy == null)
			{
				return null;
			}
			return this._propertyStrategy.GetNavigationPropertyValue(relatedEnd);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0005EF7F File Offset: 0x0005D17F
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, value);
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0005EF98 File Offset: 0x0005D198
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				object navigationPropertyValue = this._propertyStrategy.GetNavigationPropertyValue(relatedEnd);
				if (navigationPropertyValue == value)
				{
					this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, null);
				}
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0005EFCB File Offset: 0x0005D1CB
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			this._changeTrackingStrategy.SetCurrentValue(entry, member, ordinal, target, value);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			this._changeTrackingStrategy.UpdateCurrentValueRecord(value, entry);
		}

		// Token: 0x04000B77 RID: 2935
		private readonly TEntity _entity;

		// Token: 0x04000B78 RID: 2936
		private IPropertyAccessorStrategy _propertyStrategy;

		// Token: 0x04000B79 RID: 2937
		private IChangeTrackingStrategy _changeTrackingStrategy;

		// Token: 0x04000B7A RID: 2938
		private IEntityKeyStrategy _keyStrategy;
	}
}
