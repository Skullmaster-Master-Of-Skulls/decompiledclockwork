using System;
using System.Diagnostics;
using System.Linq;

namespace System.Collections.Immutable
{
	// Token: 0x0200002B RID: 43
	internal class ImmutableQueueDebuggerProxy<T>
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00007B36 File Offset: 0x00005D36
		public ImmutableQueueDebuggerProxy(ImmutableQueue<T> queue)
		{
			this._queue = queue;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007B45 File Offset: 0x00005D45
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = this._queue.ToArray<T>();
				}
				return this._contents;
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly ImmutableQueue<T> _queue;

		// Token: 0x0400002E RID: 46
		private T[] _contents;
	}
}
