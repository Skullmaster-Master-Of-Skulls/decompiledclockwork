using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000584 RID: 1412
	internal abstract class EntityWrapper<TEntity> : BaseEntityWrapper<TEntity> where TEntity : class
	{
		// Token: 0x0600371D RID: 14109 RVA: 0x00105AA4 File Offset: 0x00103CA4
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, relationshipManager, overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x00105B08 File Offset: 0x00103D08
		protected EntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntityKey key, EntitySet set, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, relationshipManager, set, context, mergeOption, identityType, overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._entity = entity;
			this._propertyStrategy = propertyStrategy(entity);
			this._changeTrackingStrategy = changeTrackingStrategy(entity);
			this._keyStrategy = keyStrategy(entity);
			this._keyStrategy.SetEntityKey(key);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x00105B81 File Offset: 0x00103D81
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._changeTrackingStrategy.SetChangeTracker(changeTracker);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x00105B8F File Offset: 0x00103D8F
		public override void TakeSnapshot(EntityEntry entry)
		{
			this._changeTrackingStrategy.TakeSnapshot(entry);
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x00105B9D File Offset: 0x00103D9D
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x00105BAA File Offset: 0x00103DAA
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

		// Token: 0x06003723 RID: 14115 RVA: 0x00105BB8 File Offset: 0x00103DB8
		public override EntityKey GetEntityKeyFromEntity()
		{
			return this._keyStrategy.GetEntityKeyFromEntity();
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x00105BC5 File Offset: 0x00103DC5
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.CollectionAdd(relatedEnd, value);
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x00105BDC File Offset: 0x00103DDC
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return this._propertyStrategy != null && this._propertyStrategy.CollectionRemove(relatedEnd, value);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x00105BF8 File Offset: 0x00103DF8
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy != null && this._propertyStrategy.GetNavigationPropertyValue(relatedEnd) == null)
			{
				object value = this._propertyStrategy.CollectionCreate(relatedEnd);
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, value);
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x00105C37 File Offset: 0x00103E37
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			if (this._propertyStrategy == null)
			{
				return null;
			}
			return this._propertyStrategy.GetNavigationPropertyValue(relatedEnd);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x00105C4F File Offset: 0x00103E4F
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, value);
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x00105C68 File Offset: 0x00103E68
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (this._propertyStrategy != null)
			{
				object navigationPropertyValue = this._propertyStrategy.GetNavigationPropertyValue(relatedEnd);
				if (object.ReferenceEquals(navigationPropertyValue, value))
				{
					this._propertyStrategy.SetNavigationPropertyValue(relatedEnd, null);
				}
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x00105CA0 File Offset: 0x00103EA0
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x00105CAD File Offset: 0x00103EAD
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x00105CB5 File Offset: 0x00103EB5
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			this._changeTrackingStrategy.SetCurrentValue(entry, member, ordinal, target, value);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x00105CC9 File Offset: 0x00103EC9
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			this._changeTrackingStrategy.UpdateCurrentValueRecord(value, entry);
		}

		// Token: 0x04001531 RID: 5425
		private readonly TEntity _entity;

		// Token: 0x04001532 RID: 5426
		private readonly IPropertyAccessorStrategy _propertyStrategy;

		// Token: 0x04001533 RID: 5427
		private readonly IChangeTrackingStrategy _changeTrackingStrategy;

		// Token: 0x04001534 RID: 5428
		private readonly IEntityKeyStrategy _keyStrategy;
	}
}
