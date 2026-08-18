using System;

namespace System.Net.Cache
{
	// Token: 0x02000314 RID: 788
	public enum HttpCacheAgeControl
	{
		// Token: 0x04001B6B RID: 7019
		None,
		// Token: 0x04001B6C RID: 7020
		MinFresh,
		// Token: 0x04001B6D RID: 7021
		MaxAge,
		// Token: 0x04001B6E RID: 7022
		MaxStale = 4,
		// Token: 0x04001B6F RID: 7023
		MaxAgeAndMinFresh = 3,
		// Token: 0x04001B70 RID: 7024
		MaxAgeAndMaxStale = 6
	}
}
