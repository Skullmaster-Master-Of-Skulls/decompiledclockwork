using System;

namespace System.IdentityModel
{
	// Token: 0x0200008A RID: 138
	internal enum BufferType
	{
		// Token: 0x040003EC RID: 1004
		Empty,
		// Token: 0x040003ED RID: 1005
		Data,
		// Token: 0x040003EE RID: 1006
		Token,
		// Token: 0x040003EF RID: 1007
		Parameters,
		// Token: 0x040003F0 RID: 1008
		Missing,
		// Token: 0x040003F1 RID: 1009
		Extra,
		// Token: 0x040003F2 RID: 1010
		Trailer,
		// Token: 0x040003F3 RID: 1011
		Header,
		// Token: 0x040003F4 RID: 1012
		Padding = 9,
		// Token: 0x040003F5 RID: 1013
		Stream,
		// Token: 0x040003F6 RID: 1014
		ChannelBindings = 14
	}
}
