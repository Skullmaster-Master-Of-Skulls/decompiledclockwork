using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000096 RID: 150
	internal class HashSetDebugView<T>
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000B9E5 File Offset: 0x00009BE5
		public HashSetDebugView(HashSet<T> set)
		{
			if (set == null)
			{
				throw new ArgumentNullException("set");
			}
			this.set = set;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000BA02 File Offset: 0x00009C02
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.set.ToArray();
			}
		}

		// Token: 0x040004DB RID: 1243
		private HashSet<T> set;
	}
}
