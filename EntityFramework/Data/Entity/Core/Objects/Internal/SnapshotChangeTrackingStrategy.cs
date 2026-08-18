using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000599 RID: 1433
	internal sealed class SnapshotChangeTrackingStrategy : IChangeTrackingStrategy
	{
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x00109E02 File Offset: 0x00108002
		public static SnapshotChangeTrackingStrategy Instance
		{
			get
			{
				return SnapshotChangeTrackingStrategy._instance;
			}
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x00109E09 File Offset: 0x00108009
		private SnapshotChangeTrackingStrategy()
		{
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x00109E11 File Offset: 0x00108011
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x00109E13 File Offset: 0x00108013
		public void TakeSnapshot(EntityEntry entry)
		{
			if (entry != null)
			{
				entry.TakeSnapshot(false);
			}
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00109E20 File Offset: 0x00108020
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
			if (object.ReferenceEquals(target, entry.Entity))
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

		// Token: 0x060037FD RID: 14333 RVA: 0x00109E8D File Offset: 0x0010808D
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
			entry.UpdateRecordWithoutSetModified(value, entry.CurrentValues);
			entry.DetectChangesInProperties(false);
		}

		// Token: 0x04001588 RID: 5512
		private static readonly SnapshotChangeTrackingStrategy _instance = new SnapshotChangeTrackingStrategy();
	}
}
