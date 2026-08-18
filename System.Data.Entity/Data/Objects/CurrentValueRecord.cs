using System;

namespace System.Data.Objects
{
	// Token: 0x02000132 RID: 306
	public abstract class CurrentValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x0004AF3A File Offset: 0x0004913A
		internal CurrentValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004AF45 File Offset: 0x00049145
		internal CurrentValueRecord(ObjectStateEntry cacheEntry) : base(cacheEntry)
		{
		}
	}
}
