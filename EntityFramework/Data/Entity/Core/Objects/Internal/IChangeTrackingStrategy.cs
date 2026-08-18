using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000580 RID: 1408
	internal interface IChangeTrackingStrategy
	{
		// Token: 0x0600370D RID: 14093
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x0600370E RID: 14094
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x0600370F RID: 14095
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x06003710 RID: 14096
		void UpdateCurrentValueRecord(object value, EntityEntry entry);
	}
}
