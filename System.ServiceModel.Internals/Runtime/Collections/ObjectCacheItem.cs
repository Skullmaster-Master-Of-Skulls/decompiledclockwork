using System;

namespace System.Runtime.Collections
{
	// Token: 0x02000051 RID: 81
	internal abstract class ObjectCacheItem<T> where T : class
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600032C RID: 812
		public abstract T Value { get; }

		// Token: 0x0600032D RID: 813
		public abstract bool TryAddReference();

		// Token: 0x0600032E RID: 814
		public abstract void ReleaseReference();
	}
}
