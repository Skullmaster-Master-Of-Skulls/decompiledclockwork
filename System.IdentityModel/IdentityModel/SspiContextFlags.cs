using System;

namespace System.IdentityModel
{
	// Token: 0x02000086 RID: 134
	[Flags]
	internal enum SspiContextFlags
	{
		// Token: 0x040003CA RID: 970
		Zero = 0,
		// Token: 0x040003CB RID: 971
		Delegate = 1,
		// Token: 0x040003CC RID: 972
		MutualAuth = 2,
		// Token: 0x040003CD RID: 973
		ReplayDetect = 4,
		// Token: 0x040003CE RID: 974
		SequenceDetect = 8,
		// Token: 0x040003CF RID: 975
		Confidentiality = 16,
		// Token: 0x040003D0 RID: 976
		UseSessionKey = 32,
		// Token: 0x040003D1 RID: 977
		AllocateMemory = 256,
		// Token: 0x040003D2 RID: 978
		InitStream = 32768,
		// Token: 0x040003D3 RID: 979
		AcceptStream = 65536,
		// Token: 0x040003D4 RID: 980
		InitExtendedError = 16384,
		// Token: 0x040003D5 RID: 981
		AcceptExtendedError = 32768,
		// Token: 0x040003D6 RID: 982
		InitIdentify = 131072,
		// Token: 0x040003D7 RID: 983
		AcceptIdentify = 524288,
		// Token: 0x040003D8 RID: 984
		InitManualCredValidation = 524288,
		// Token: 0x040003D9 RID: 985
		InitAnonymous = 262144,
		// Token: 0x040003DA RID: 986
		AcceptAnonymous = 1048576,
		// Token: 0x040003DB RID: 987
		ChannelBindingProxyBindings = 67108864,
		// Token: 0x040003DC RID: 988
		ChannelBindingAllowMissingBindings = 268435456
	}
}
