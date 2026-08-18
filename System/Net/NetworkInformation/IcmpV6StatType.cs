using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062C RID: 1580
	internal enum IcmpV6StatType
	{
		// Token: 0x04002E3F RID: 11839
		DestinationUnreachable = 1,
		// Token: 0x04002E40 RID: 11840
		PacketTooBig,
		// Token: 0x04002E41 RID: 11841
		TimeExceeded,
		// Token: 0x04002E42 RID: 11842
		ParameterProblem,
		// Token: 0x04002E43 RID: 11843
		EchoRequest = 128,
		// Token: 0x04002E44 RID: 11844
		EchoReply,
		// Token: 0x04002E45 RID: 11845
		MembershipQuery,
		// Token: 0x04002E46 RID: 11846
		MembershipReport,
		// Token: 0x04002E47 RID: 11847
		MembershipReduction,
		// Token: 0x04002E48 RID: 11848
		RouterSolicit,
		// Token: 0x04002E49 RID: 11849
		RouterAdvertisement,
		// Token: 0x04002E4A RID: 11850
		NeighborSolict,
		// Token: 0x04002E4B RID: 11851
		NeighborAdvertisement,
		// Token: 0x04002E4C RID: 11852
		Redirect
	}
}
