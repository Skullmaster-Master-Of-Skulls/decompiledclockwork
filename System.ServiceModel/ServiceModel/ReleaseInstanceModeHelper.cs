using System;

namespace System.ServiceModel
{
	// Token: 0x020000FF RID: 255
	internal static class ReleaseInstanceModeHelper
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x0001847B File Offset: 0x0001667B
		public static bool IsDefined(ReleaseInstanceMode x)
		{
			return x == ReleaseInstanceMode.None || x == ReleaseInstanceMode.BeforeCall || x == ReleaseInstanceMode.AfterCall || x == ReleaseInstanceMode.BeforeAndAfterCall;
		}
	}
}
