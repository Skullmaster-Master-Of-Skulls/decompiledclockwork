using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BE RID: 958
	internal sealed class System_StackDebugView<T>
	{
		// Token: 0x0600241E RID: 9246 RVA: 0x000A9602 File Offset: 0x000A7802
		public System_StackDebugView(Stack<T> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			this.stack = stack;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000A961F File Offset: 0x000A781F
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.stack.ToArray();
			}
		}

		// Token: 0x04002003 RID: 8195
		private Stack<T> stack;
	}
}
