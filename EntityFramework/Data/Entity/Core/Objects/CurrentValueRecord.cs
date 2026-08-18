using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200052D RID: 1325
	public abstract class CurrentValueRecord : DbUpdatableDataRecord
	{
		// Token: 0x06003272 RID: 12914 RVA: 0x000F0271 File Offset: 0x000EE471
		internal CurrentValueRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000F027C File Offset: 0x000EE47C
		internal CurrentValueRecord(ObjectStateEntry cacheEntry) : base(cacheEntry)
		{
		}
	}
}
