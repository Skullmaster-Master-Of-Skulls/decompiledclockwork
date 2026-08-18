using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AB RID: 1451
	public abstract class OriginalValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x060039A8 RID: 14760 RVA: 0x00111745 File Offset: 0x0010F945
		internal OriginalValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
		}
	}
}
