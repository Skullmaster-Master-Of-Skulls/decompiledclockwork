using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003CD RID: 973
	internal class SortedSetDebugView<T>
	{
		// Token: 0x06002546 RID: 9542 RVA: 0x000ADBFC File Offset: 0x000ABDFC
		public SortedSetDebugView(SortedSet<T> set)
		{
			if (set == null)
			{
				throw new ArgumentNullException("set");
			}
			this.set = set;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000ADC19 File Offset: 0x000ABE19
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.set.ToArray();
			}
		}

		// Token: 0x0400204E RID: 8270
		private SortedSet<T> set;
	}
}
