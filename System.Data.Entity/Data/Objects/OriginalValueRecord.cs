using System;

namespace System.Data.Objects
{
	// Token: 0x02000133 RID: 307
	public abstract class OriginalValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x06001658 RID: 5720 RVA: 0x0004AF3A File Offset: 0x0004913A
		internal OriginalValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
		}
	}
}
