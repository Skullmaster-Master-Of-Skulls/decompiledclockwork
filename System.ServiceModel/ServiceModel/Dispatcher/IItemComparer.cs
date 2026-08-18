using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000509 RID: 1289
	internal interface IItemComparer<K, V>
	{
		// Token: 0x060030CB RID: 12491
		int Compare(K key, V value);
	}
}
