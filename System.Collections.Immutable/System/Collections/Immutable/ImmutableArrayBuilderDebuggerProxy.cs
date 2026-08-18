using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x0200001B RID: 27
	internal sealed class ImmutableArrayBuilderDebuggerProxy<T>
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00004A6B File Offset: 0x00002C6B
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			this._builder = builder;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004A7A File Offset: 0x00002C7A
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x04000012 RID: 18
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
