using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003DD RID: 989
	[Flags]
	[__DynamicallyInvokable]
	public enum ADVF
	{
		// Token: 0x0400208A RID: 8330
		[__DynamicallyInvokable]
		ADVF_NODATA = 1,
		// Token: 0x0400208B RID: 8331
		[__DynamicallyInvokable]
		ADVF_PRIMEFIRST = 2,
		// Token: 0x0400208C RID: 8332
		[__DynamicallyInvokable]
		ADVF_ONLYONCE = 4,
		// Token: 0x0400208D RID: 8333
		[__DynamicallyInvokable]
		ADVF_DATAONSTOP = 64,
		// Token: 0x0400208E RID: 8334
		[__DynamicallyInvokable]
		ADVFCACHE_NOHANDLER = 8,
		// Token: 0x0400208F RID: 8335
		[__DynamicallyInvokable]
		ADVFCACHE_FORCEBUILTIN = 16,
		// Token: 0x04002090 RID: 8336
		[__DynamicallyInvokable]
		ADVFCACHE_ONSAVE = 32
	}
}
