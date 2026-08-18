using System;

namespace System.ServiceModel.Description
{
	// Token: 0x02000440 RID: 1088
	internal static class ListenUriModeHelper
	{
		// Token: 0x06002A79 RID: 10873 RVA: 0x000A422C File Offset: 0x000A242C
		public static bool IsDefined(ListenUriMode mode)
		{
			return mode == ListenUriMode.Explicit || mode == ListenUriMode.Unique;
		}
	}
}
