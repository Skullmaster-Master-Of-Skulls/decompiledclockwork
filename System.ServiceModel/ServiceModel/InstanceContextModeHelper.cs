using System;

namespace System.ServiceModel
{
	// Token: 0x020000FA RID: 250
	internal static class InstanceContextModeHelper
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0001846C File Offset: 0x0001666C
		public static bool IsDefined(InstanceContextMode x)
		{
			return x == InstanceContextMode.PerCall || x == InstanceContextMode.PerSession || x == InstanceContextMode.Single;
		}
	}
}
