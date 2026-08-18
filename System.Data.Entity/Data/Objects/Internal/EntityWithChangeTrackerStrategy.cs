using System;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000166 RID: 358
	internal sealed class EntityWithChangeTrackerStrategy : IChangeTrackingStrategy
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x0005BB43 File Offset: 0x00059D43
		public EntityWithChangeTrackerStrategy(IEntityWithChangeTracker entity)
		{
			this._entity = entity;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0005BB52 File Offset: 0x00059D52
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			this._entity.SetChangeTracker(changeTracker);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0005BB60 File Offset: 0x00059D60
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null && entry.RequiresComplexChangeTracking)
			{
				entry.TakeSnapshot(true);
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0005BB74 File Offset: 0x00059D74
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			member.SetValue(target, value);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0005BB80 File Offset: 0x00059D80
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			bool flag = entry.WrappedEntity.IdentityType != this._entity.GetType();
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			if (flag)
			{
				entry.DetectChangesInProperties(true);
			}
		}

		// Token: 0x04000B2E RID: 2862
		private IEntityWithChangeTracker _entity;
	}
}
