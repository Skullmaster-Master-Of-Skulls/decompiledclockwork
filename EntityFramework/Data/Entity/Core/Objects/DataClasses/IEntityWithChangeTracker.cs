using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200053E RID: 1342
	public interface IEntityWithChangeTracker
	{
		// Token: 0x060033C4 RID: 13252
		void SetChangeTracker(IEntityChangeTracker changeTracker);
	}
}
