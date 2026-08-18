using System;

namespace System.Net
{
	// Token: 0x020004F6 RID: 1270
	[Flags]
	internal enum ContextFlags
	{
		// Token: 0x040026E3 RID: 9955
		Zero = 0,
		// Token: 0x040026E4 RID: 9956
		Delegate = 1,
		// Token: 0x040026E5 RID: 9957
		MutualAuth = 2,
		// Token: 0x040026E6 RID: 9958
		ReplayDetect = 4,
		// Token: 0x040026E7 RID: 9959
		SequenceDetect = 8,
		// Token: 0x040026E8 RID: 9960
		Confidentiality = 16,
		// Token: 0x040026E9 RID: 9961
		UseSessionKey = 32,
		// Token: 0x040026EA RID: 9962
		AllocateMemory = 256,
		// Token: 0x040026EB RID: 9963
		Connection = 2048,
		// Token: 0x040026EC RID: 9964
		InitExtendedError = 16384,
		// Token: 0x040026ED RID: 9965
		AcceptExtendedError = 32768,
		// Token: 0x040026EE RID: 9966
		InitStream = 32768,
		// Token: 0x040026EF RID: 9967
		AcceptStream = 65536,
		// Token: 0x040026F0 RID: 9968
		InitIntegrity = 65536,
		// Token: 0x040026F1 RID: 9969
		AcceptIntegrity = 131072,
		// Token: 0x040026F2 RID: 9970
		InitManualCredValidation = 524288,
		// Token: 0x040026F3 RID: 9971
		InitUseSuppliedCreds = 128,
		// Token: 0x040026F4 RID: 9972
		InitIdentify = 131072,
		// Token: 0x040026F5 RID: 9973
		AcceptIdentify = 524288,
		// Token: 0x040026F6 RID: 9974
		ProxyBindings = 67108864,
		// Token: 0x040026F7 RID: 9975
		AllowMissingBindings = 268435456
	}
}
