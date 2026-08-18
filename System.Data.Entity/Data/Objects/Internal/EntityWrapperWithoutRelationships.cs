using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000177 RID: 375
	internal sealed class EntityWrapperWithoutRelationships<TEntity> : EntityWrapper<TEntity>
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x0005ED50 File Offset: 0x0005CF50
		internal EntityWrapperWithoutRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, RelationshipManager.Create(), key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy)
		{
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0005ED77 File Offset: 0x0005CF77
		internal EntityWrapperWithoutRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy) : base(entity, RelationshipManager.Create(), propertyStrategy, changeTrackingStrategy, keyStrategy)
		{
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0005ED89 File Offset: 0x0005CF89
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
			entry.TakeSnapshotOfRelationships();
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return true;
			}
		}
	}
}
