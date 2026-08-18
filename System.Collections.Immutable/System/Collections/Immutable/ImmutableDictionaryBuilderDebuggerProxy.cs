using System;
using System.Collections.Generic;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200001E RID: 30
	internal class ImmutableDictionaryBuilderDebuggerProxy<TKey, TValue>
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00005720 File Offset: 0x00003920
		public ImmutableDictionaryBuilderDebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000573A File Offset: 0x0000393A
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

		// Token: 0x04000018 RID: 24
		private readonly ImmutableDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000019 RID: 25
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
