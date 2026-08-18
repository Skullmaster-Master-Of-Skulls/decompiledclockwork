using System;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000023 RID: 35
	internal class ImmutableHashSetDebuggerProxy<T>
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000672F File Offset: 0x0000492F
		public ImmutableHashSetDebuggerProxy(ImmutableHashSet<T> set)
		{
			Requires.NotNull<ImmutableHashSet<T>>(set, "set");
			this._set = set;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006749 File Offset: 0x00004949
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

		// Token: 0x04000021 RID: 33
		private readonly ImmutableHashSet<T> _set;

		// Token: 0x04000022 RID: 34
		private T[] _contents;
	}
}
