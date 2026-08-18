using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BD RID: 957
	internal sealed class System_QueueDebugView<T>
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x000A95D8 File Offset: 0x000A77D8
		public System_QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this.queue = queue;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000A95F5 File Offset: 0x000A77F5
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.queue.ToArray();
			}
		}

		// Token: 0x04002002 RID: 8194
		private Queue<T> queue;
	}
}
