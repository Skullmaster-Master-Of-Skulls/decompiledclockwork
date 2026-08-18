using System;

namespace System.Net
{
	// Token: 0x020001CE RID: 462
	[Flags]
	internal enum ContextFlags
	{
		// Token: 0x040014A8 RID: 5288
		Zero = 0,
		// Token: 0x040014A9 RID: 5289
		Delegate = 1,
		// Token: 0x040014AA RID: 5290
		MutualAuth = 2,
		// Token: 0x040014AB RID: 5291
		ReplayDetect = 4,
		// Token: 0x040014AC RID: 5292
		SequenceDetect = 8,
		// Token: 0x040014AD RID: 5293
		Confidentiality = 16,
		// Token: 0x040014AE RID: 5294
		UseSessionKey = 32,
		// Token: 0x040014AF RID: 5295
		AllocateMemory = 256,
		// Token: 0x040014B0 RID: 5296
		Connection = 2048,
		// Token: 0x040014B1 RID: 5297
		InitExtendedError = 16384,
		// Token: 0x040014B2 RID: 5298
		AcceptExtendedError = 32768,
		// Token: 0x040014B3 RID: 5299
		InitStream = 32768,
		// Token: 0x040014B4 RID: 5300
		AcceptStream = 65536,
		// Token: 0x040014B5 RID: 5301
		InitIntegrity = 65536,
		// Token: 0x040014B6 RID: 5302
		AcceptIntegrity = 131072,
		// Token: 0x040014B7 RID: 5303
		InitManualCredValidation = 524288,
		// Token: 0x040014B8 RID: 5304
		InitUseSuppliedCreds = 128,
		// Token: 0x040014B9 RID: 5305
		InitIdentify = 131072,
		// Token: 0x040014BA RID: 5306
		AcceptIdentify = 524288,
		// Token: 0x040014BB RID: 5307
		ProxyBindings = 67108864,
		// Token: 0x040014BC RID: 5308
		AllowMissingBindings = 268435456,
		// Token: 0x040014BD RID: 5309
		UnverifiedTargetName = 536870912
	}
}
