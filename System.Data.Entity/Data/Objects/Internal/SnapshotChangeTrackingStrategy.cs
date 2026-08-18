using System;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016F RID: 367
	internal sealed class SnapshotChangeTrackingStrategy : IChangeTrackingStrategy
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0005C404 File Offset: 0x0005A604
		public static SnapshotChangeTrackingStrategy Instance
		{
			get
			{
				return SnapshotChangeTrackingStrategy._instance;
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00002050 File Offset: 0x00000250
		private SnapshotChangeTrackingStrategy()
		{
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0005C40B File Offset: 0x0005A60B
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null)
			{
				entry.TakeSnapshot(false);
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0005C418 File Offset: 0x0005A618
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			if (target == entry.Entity)
			{
				((IEntityChangeTracker)entry).EntityMemberChanging(member.CLayerName);
				member.SetValue(target, value);
				((IEntityChangeTracker)entry).EntityMemberChanged(member.CLayerName);
				if (member.IsComplex)
				{
					entry.UpdateComplexObjectSnapshot(member, target, ordinal, value);
					return;
				}
			}
			else
			{
				member.SetValue(target, value);
				if (entry.State != EntityState.Added)
				{
					entry.DetectChangesInProperties(true);
				}
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0005C480 File Offset: 0x0005A680
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			entry.DetectChangesInProperties(false);
		}

		// Token: 0x04000B37 RID: 2871
		private static SnapshotChangeTrackingStrategy _instance = new SnapshotChangeTrackingStrategy();
	}
}
