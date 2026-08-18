using System;

namespace System.Collections.Generic
{
	// Token: 0x0200003F RID: 63
	internal interface IHashKeyCollection<in TKey>
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600037F RID: 895
		IEqualityComparer<TKey> KeyComparer { get; }
	}
}
