using System;
using System.Collections.Generic;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200001F RID: 31
	internal class ImmutableDictionaryDebuggerProxy<TKey, TValue>
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00005766 File Offset: 0x00003966
		public ImmutableDictionaryDebuggerProxy(ImmutableDictionary<TKey, TValue> map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
			this._map = map;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005780 File Offset: 0x00003980
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

		// Token: 0x0400001A RID: 26
		private readonly ImmutableDictionary<TKey, TValue> _map;

		// Token: 0x0400001B RID: 27
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
