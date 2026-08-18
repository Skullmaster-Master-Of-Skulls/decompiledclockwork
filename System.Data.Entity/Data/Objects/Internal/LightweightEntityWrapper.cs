using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016B RID: 363
	internal sealed class LightweightEntityWrapper<TEntity> : BaseEntityWrapper<TEntity> where TEntity : IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0005BBEA File Offset: 0x00059DEA
		internal LightweightEntityWrapper(TEntity entity) : base(entity, entity.RelationshipManager)
		{
			this._entity = entity;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0005BC07 File Offset: 0x00059E07
		internal LightweightEntityWrapper(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType) : base(entity, entity.RelationshipManager, entitySet, context, mergeOption, identityType)
		{
			this._entity = entity;
			this._entity.EntityKey = key;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0005BC40 File Offset: 0x00059E40
		public override void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			TEntity entity = this._entity;
			entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0005BC64 File Offset: 0x00059E64
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x0005BC88 File Offset: 0x00059E88
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0005BCAC File Offset: 0x00059EAC
		public override EntityKey GetEntityKeyFromEntity()
		{
			TEntity entity = this._entity;
			return entity.EntityKey;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00006174 File Offset: 0x00004374
		public override object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0005BCCD File Offset: 0x00059ECD
		public override object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0005BCDA File Offset: 0x00059EDA
		public override TEntity TypedEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0005BB74 File Offset: 0x00059D74
		public override void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0005BCE2 File Offset: 0x00059EE2
		public override void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000B30 RID: 2864
		private readonly TEntity _entity;
	}
}
