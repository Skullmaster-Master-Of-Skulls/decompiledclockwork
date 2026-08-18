using System;

namespace System.ServiceModel
{
	// Token: 0x020000A8 RID: 168
	internal static class MsmqAuthenticationModeHelper
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00011323 File Offset: 0x0000F523
		public static bool IsDefined(MsmqAuthenticationMode mode)
		{
			return mode >= MsmqAuthenticationMode.None && mode <= MsmqAuthenticationMode.Certificate;
		}
	}
}
