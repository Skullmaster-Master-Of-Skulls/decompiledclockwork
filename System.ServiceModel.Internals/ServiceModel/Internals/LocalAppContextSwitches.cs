using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Internals
{
	// Token: 0x02000055 RID: 85
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001170C File Offset: 0x0000F90C
		public static bool IncludeNullExceptionMessageInETWTrace
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.ServiceModel.Internals.IncludeNullExceptionMessageInETWTrace", ref LocalAppContextSwitches.includeNullExceptionMessageInETWTrace);
			}
		}

		// Token: 0x040001C3 RID: 451
		private static int includeNullExceptionMessageInETWTrace;
	}
}
