using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000178 RID: 376
	internal sealed class EntityWrapperWithRelationships<TEntity> : EntityWrapper<TEntity> where TEntity : IEntityWithRelationships
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x0005ED94 File Offset: 0x0005CF94
		internal EntityWrapperWithRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, entity.RelationshipManager, key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy)
		{
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0005EDC3 File Offset: 0x0005CFC3
		internal EntityWrapperWithRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, entity.RelationshipManager, propertyStrategy, changeTrackingStrategy, keyStrategy)
		{
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}
	}
}
