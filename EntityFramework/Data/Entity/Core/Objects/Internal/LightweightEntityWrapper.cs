using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058D RID: 1421
	internal sealed class LightweightEntityWrapper<TEntity> : BaseEntityWrapper<TEntity> where TEntity : class, IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
	{
		// Token: 0x06003776 RID: 14198 RVA: 0x00107A6F File Offset: 0x00105C6F
		internal LightweightEntityWrapper(TEntity entity, bool overridesEquals) : base(entity, entity.RelationshipManager, overridesEquals)
		{
			this._entity = entity;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x00107A8D File Offset: 0x00105C8D
		internal LightweightEntityWrapper(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, bool overridesEquals) : base(entity, entity.RelationshipManager, entitySet, context, mergeOption, identityType, overridesEquals)
		{
			this._entity = entity;
			this._entity.EntityKey = key;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x00107AC8 File Offset: 0x00105CC8
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			TEntity entity = this._entity;
			entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x00107AEA File Offset: 0x00105CEA
		public override void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x00107AEC File Offset: 0x00105CEC
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x00107AF0 File Offset: 0x00105CF0
		// (set) Token: 0x0600377C RID: 14204 RVA: 0x00107B14 File Offset: 0x00105D14
		public override EntityKey EntityKey
		{
			get
			{
				TEntity entity = this._entity;
				return entity.EntityKey;
			}
			set
			{
				TEntity entity = this._entity;
				entity.EntityKey = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x00107B36 File Offset: 0x00105D36
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x00107B3C File Offset: 0x00105D3C
		public override EntityKey GetEntityKeyFromEntity()
		{
			TEntity entity = this._entity;
			return entity.EntityKey;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x00107B5D File Offset: 0x00105D5D
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x00107B5F File Offset: 0x00105D5F
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x00107B62 File Offset: 0x00105D62
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x00107B64 File Offset: 0x00105D64
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x00107B66 File Offset: 0x00105D66
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x00107B68 File Offset: 0x00105D68
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x00107B6B File Offset: 0x00105D6B
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x00107B78 File Offset: 0x00105D78
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x00107B80 File Offset: 0x00105D80
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x00107B8C File Offset: 0x00105D8C
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x00107B9B File Offset: 0x00105D9B
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400155D RID: 5469
		private readonly TEntity _entity;
	}
}
