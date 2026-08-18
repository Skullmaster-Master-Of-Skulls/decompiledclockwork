using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000039 RID: 57
	internal class ValuesCollectionAccessor<TKey, TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00009453 File Offset: 0x00007653
		internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary, dictionary.Values)
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00009464 File Offset: 0x00007664
		public override bool Contains(TValue item)
		{
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = base.Dictionary as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.ContainsValue(item);
			}
			IImmutableDictionaryInternal<TKey, TValue> immutableDictionaryInternal = base.Dictionary as IImmutableDictionaryInternal<TKey, TValue>;
			if (immutableDictionaryInternal != null)
			{
				return immutableDictionaryInternal.ContainsValue(item);
			}
			throw new NotSupportedException();
		}
	}
}
