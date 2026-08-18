using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000581 RID: 1409
	internal sealed class EntityWithChangeTrackerStrategy : IChangeTrackingStrategy
	{
		// Token: 0x06003711 RID: 14097 RVA: 0x001059EC File Offset: 0x00103BEC
		public EntityWithChangeTrackerStrategy(IEntityWithChangeTracker entity)
		{
			this._entity = entity;
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x001059FB File Offset: 0x00103BFB
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x00105A09 File Offset: 0x00103C09
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null && entry.RequiresComplexChangeTracking)
			{
				entry.TakeSnapshot(true);
			}
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x00105A1D File Offset: 0x00103C1D
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x00105A2C File Offset: 0x00103C2C
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			bool flag = entry.WrappedEntity.IdentityType != this._entity.GetType();
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			if (flag)
			{
				entry.DetectChangesInProperties(true);
			}
		}

		// Token: 0x0400152F RID: 5423
		private readonly IEntityWithChangeTracker _entity;
	}
}
