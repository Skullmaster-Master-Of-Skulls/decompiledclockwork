using System;
using System.Collections.Generic;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200002F RID: 47
	internal class ImmutableSortedDictionaryDebuggerProxy<TKey, TValue>
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00008520 File Offset: 0x00006720
		public ImmutableSortedDictionaryDebuggerProxy(ImmutableSortedDictionary<TKey, TValue> map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
			this._map = map;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000853A File Offset: 0x0000673A
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<TKey, TValue>[] Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = this._map.ToArray(this._map.Count);
				}
				return this._contents;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly ImmutableSortedDictionary<TKey, TValue> _map;

		// Token: 0x04000037 RID: 55
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
