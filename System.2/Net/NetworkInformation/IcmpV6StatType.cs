using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F3 RID: 755
	internal enum IcmpV6StatType
	{
		// Token: 0x04001AA2 RID: 6818
		DestinationUnreachable = 1,
		// Token: 0x04001AA3 RID: 6819
		PacketTooBig,
		// Token: 0x04001AA4 RID: 6820
		TimeExceeded,
		// Token: 0x04001AA5 RID: 6821
		ParameterProblem,
		// Token: 0x04001AA6 RID: 6822
		EchoRequest = 128,
		// Token: 0x04001AA7 RID: 6823
		EchoReply,
		// Token: 0x04001AA8 RID: 6824
		MembershipQuery,
		// Token: 0x04001AA9 RID: 6825
		MembershipReport,
		// Token: 0x04001AAA RID: 6826
		MembershipReduction,
		// Token: 0x04001AAB RID: 6827
		RouterSolicit,
		// Token: 0x04001AAC RID: 6828
		RouterAdvertisement,
		// Token: 0x04001AAD RID: 6829
		NeighborSolict,
		// Token: 0x04001AAE RID: 6830
		NeighborAdvertisement,
		// Token: 0x04001AAF RID: 6831
		Redirect
	}
}
