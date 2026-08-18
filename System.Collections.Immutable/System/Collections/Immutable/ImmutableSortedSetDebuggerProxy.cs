using System;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000033 RID: 51
	internal class ImmutableSortedSetDebuggerProxy<T>
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000904A File Offset: 0x0000724A
		public ImmutableSortedSetDebuggerProxy(ImmutableSortedSet<T> set)
		{
			Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
			this._set = set;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009064 File Offset: 0x00007264
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = this._set.ToArray(this._set.Count);
				}
				return this._contents;
			}
		}

		// Token: 0x0400003E RID: 62
		private readonly ImmutableSortedSet<T> _set;

		// Token: 0x0400003F RID: 63
		private T[] _contents;
	}
}
