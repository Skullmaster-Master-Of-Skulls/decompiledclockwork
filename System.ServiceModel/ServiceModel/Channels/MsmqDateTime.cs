using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000903 RID: 2307
	internal static class MsmqDateTime
	{
		// Token: 0x060057F1 RID: 22513 RVA: 0x00143610 File Offset: 0x00141810
		public static DateTime ToDateTime(int seconds)
		{
			return new DateTime(1970, 1, 1).AddSeconds((double)seconds);
		}
	}
}
