using System;

namespace System.Web.Mvc
{
	// Token: 0x020001AC RID: 428
	[Flags]
	public enum HttpVerbs
	{
		// Token: 0x04000332 RID: 818
		Get = 1,
		// Token: 0x04000333 RID: 819
		Post = 2,
		// Token: 0x04000334 RID: 820
		Put = 4,
		// Token: 0x04000335 RID: 821
		Delete = 8,
		// Token: 0x04000336 RID: 822
		Head = 16,
		// Token: 0x04000337 RID: 823
		Patch = 32,
		// Token: 0x04000338 RID: 824
		Options = 64
	}
}
