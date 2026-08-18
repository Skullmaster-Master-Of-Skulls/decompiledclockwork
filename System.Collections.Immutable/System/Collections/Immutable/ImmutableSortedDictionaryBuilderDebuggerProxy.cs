using System;
using System.Collections.Generic;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200002E RID: 46
	internal class ImmutableSortedDictionaryBuilderDebuggerProxy<TKey, TValue>
	{
		// Token: 0x060002EB RID: 747 RVA: 0x000084DA File Offset: 0x000066DA
		public ImmutableSortedDictionaryBuilderDebuggerProxy(ImmutableSortedDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000084F4 File Offset: 0x000066F4
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

		// Token: 0x04000034 RID: 52
		private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000035 RID: 53
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
