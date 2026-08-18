using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000038 RID: 56
	internal class KeysCollectionAccessor<TKey, TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00009436 File Offset: 0x00007636
		internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary, dictionary.Keys)
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009445 File Offset: 0x00007645
		public override bool Contains(TKey item)
		{
			return base.Dictionary.ContainsKey(item);
		}
	}
}
