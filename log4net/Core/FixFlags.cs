using System;

namespace log4net.Core
{
	// Token: 0x0200006F RID: 111
	[Flags]
	public enum FixFlags
	{
		// Token: 0x040001A8 RID: 424
		[Obsolete("Replaced by composite Properties")]
		Mdc = 1,
		// Token: 0x040001A9 RID: 425
		Ndc = 2,
		// Token: 0x040001AA RID: 426
		Message = 4,
		// Token: 0x040001AB RID: 427
		ThreadName = 8,
		// Token: 0x040001AC RID: 428
		LocationInfo = 16,
		// Token: 0x040001AD RID: 429
		UserName = 32,
		// Token: 0x040001AE RID: 430
		Domain = 64,
		// Token: 0x040001AF RID: 431
		Identity = 128,
		// Token: 0x040001B0 RID: 432
		Exception = 256,
		// Token: 0x040001B1 RID: 433
		Properties = 512,
		// Token: 0x040001B2 RID: 434
		None = 0,
		// Token: 0x040001B3 RID: 435
		All = 268435455,
		// Token: 0x040001B4 RID: 436
		Partial = 844
	}
}
