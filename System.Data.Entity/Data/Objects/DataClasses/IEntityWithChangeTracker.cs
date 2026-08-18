using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000180 RID: 384
	public interface IEntityWithChangeTracker
	{
		// Token: 0x06001C12 RID: 7186
		void SetChangeTracker(IEntityChangeTracker changeTracker);
	}
}
