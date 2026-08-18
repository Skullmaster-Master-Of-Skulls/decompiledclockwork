using System;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000028 RID: 40
	internal class ImmutableListDebuggerProxy<T>
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000788E File Offset: 0x00005A8E
		public ImmutableListDebuggerProxy(ImmutableList<T> list)
		{
			Requires.NotNull<ImmutableList<T>>(list, "list");
			this._list = list;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000078A8 File Offset: 0x00005AA8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._cachedContents == null)
				{
					this._cachedContents = this._list.ToArray(this._list.Count);
				}
				return this._cachedContents;
			}
		}

		// Token: 0x04000027 RID: 39
		private readonly ImmutableList<T> _list;

		// Token: 0x04000028 RID: 40
		private T[] _cachedContents;
	}
}
