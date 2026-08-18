using System;

namespace System.ServiceModel
{
	// Token: 0x020000E4 RID: 228
	internal static class SessionModeHelper
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00016683 File Offset: 0x00014883
		public static bool IsDefined(SessionMode sessionMode)
		{
			return sessionMode == SessionMode.NotAllowed || sessionMode == SessionMode.Allowed || sessionMode == SessionMode.Required;
		}
	}
}
