using System;

namespace System.Web.Hosting
{
	// Token: 0x02000797 RID: 1943
	internal struct HTTP_COOKED_URL
	{
		// Token: 0x040030CA RID: 12490
		internal readonly ushort FullUrlLength;

		// Token: 0x040030CB RID: 12491
		internal readonly ushort HostLength;

		// Token: 0x040030CC RID: 12492
		internal readonly ushort AbsPathLength;

		// Token: 0x040030CD RID: 12493
		internal readonly ushort QueryStringLength;

		// Token: 0x040030CE RID: 12494
		internal unsafe readonly char* pFullUrl;

		// Token: 0x040030CF RID: 12495
		internal unsafe readonly char* pHost;

		// Token: 0x040030D0 RID: 12496
		internal unsafe readonly char* pAbsPath;

		// Token: 0x040030D1 RID: 12497
		internal unsafe readonly char* pQueryString;
	}
}
