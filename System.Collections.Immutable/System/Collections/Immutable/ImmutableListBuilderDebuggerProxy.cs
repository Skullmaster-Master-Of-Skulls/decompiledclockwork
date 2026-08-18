using System;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000027 RID: 39
	internal class ImmutableListBuilderDebuggerProxy<T>
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00007848 File Offset: 0x00005A48
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00007862 File Offset: 0x00005A62
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

		// Token: 0x04000025 RID: 37
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04000026 RID: 38
		private T[] _cachedContents;
	}
}
