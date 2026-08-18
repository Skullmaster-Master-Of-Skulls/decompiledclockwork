using System;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000168 RID: 360
	internal interface IChangeTrackingStrategy
	{
		// Token: 0x06001AC9 RID: 6857
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06001ACA RID: 6858
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x06001ACB RID: 6859
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x06001ACC RID: 6860
		void UpdateCurrentValueRecord(object value, EntityEntry entry);
	}
}
