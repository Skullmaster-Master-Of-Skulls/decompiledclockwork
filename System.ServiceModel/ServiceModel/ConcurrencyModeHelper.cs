using System;

namespace System.ServiceModel
{
	// Token: 0x020000EC RID: 236
	internal static class ConcurrencyModeHelper
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001773C File Offset: 0x0001593C
		public static bool IsDefined(ConcurrencyMode x)
		{
			return x == ConcurrencyMode.Single || x == ConcurrencyMode.Reentrant || x == ConcurrencyMode.Multiple;
		}
	}
}
