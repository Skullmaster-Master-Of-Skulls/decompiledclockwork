using System;

namespace System.Net.Cache
{
	// Token: 0x0200056E RID: 1390
	public enum HttpCacheAgeControl
	{
		// Token: 0x0400292C RID: 10540
		None,
		// Token: 0x0400292D RID: 10541
		MinFresh,
		// Token: 0x0400292E RID: 10542
		MaxAge,
		// Token: 0x0400292F RID: 10543
		MaxStale = 4,
		// Token: 0x04002930 RID: 10544
		MaxAgeAndMinFresh = 3,
		// Token: 0x04002931 RID: 10545
		MaxAgeAndMaxStale = 6
	}
}
