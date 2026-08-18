using System;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000032 RID: 50
	internal class ImmutableSortedSetBuilderDebuggerProxy<T>
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00009004 File Offset: 0x00007204
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000901E File Offset: 0x0000721E
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

		// Token: 0x0400003C RID: 60
		private readonly ImmutableSortedSet<T>.Builder _set;

		// Token: 0x0400003D RID: 61
		private T[] _contents;
	}
}
