using System;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006DA RID: 1754
	internal class ArrayWithSize
	{
		// Token: 0x06003F1F RID: 16159 RVA: 0x000D83D2 File Offset: 0x000D73D2
		internal ArrayWithSize(IDynamicMessageSink[] sinks, int count)
		{
			this.Sinks = sinks;
			this.Count = count;
		}

		// Token: 0x04002009 RID: 8201
		internal IDynamicMessageSink[] Sinks;

		// Token: 0x0400200A RID: 8202
		internal int Count;
	}
}
