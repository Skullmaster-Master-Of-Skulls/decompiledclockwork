using System;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200003B RID: 59
	internal class SecureObjectPool
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000094B0 File Offset: 0x000076B0
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref SecureObjectPool.s_poolUserIdCounter);
			}
			while (num == -1);
			return num;
		}

		// Token: 0x04000048 RID: 72
		private static int s_poolUserIdCounter;

		// Token: 0x04000049 RID: 73
		internal const int UnassignedId = -1;
	}
}
