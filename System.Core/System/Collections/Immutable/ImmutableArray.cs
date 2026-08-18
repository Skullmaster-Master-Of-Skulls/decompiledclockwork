using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000092 RID: 146
	internal static class ImmutableArray
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public static ImmutableArray<T>.Builder CreateBuilder<T>(int capacity)
		{
			return new ImmutableArray<T>.Builder(capacity);
		}
	}
}
