using System;

namespace System.Collections.Generic
{
	// Token: 0x02000040 RID: 64
	internal interface ISortKeyCollection<in TKey>
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000380 RID: 896
		IComparer<TKey> KeyComparer { get; }
	}
}
