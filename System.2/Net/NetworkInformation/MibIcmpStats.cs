using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C7 RID: 711
	internal struct MibIcmpStats
	{
		// Token: 0x040019F0 RID: 6640
		internal uint messages;

		// Token: 0x040019F1 RID: 6641
		internal uint errors;

		// Token: 0x040019F2 RID: 6642
		internal uint destinationUnreachables;

		// Token: 0x040019F3 RID: 6643
		internal uint timeExceeds;

		// Token: 0x040019F4 RID: 6644
		internal uint parameterProblems;

		// Token: 0x040019F5 RID: 6645
		internal uint sourceQuenches;

		// Token: 0x040019F6 RID: 6646
		internal uint redirects;

		// Token: 0x040019F7 RID: 6647
		internal uint echoRequests;

		// Token: 0x040019F8 RID: 6648
		internal uint echoReplies;

		// Token: 0x040019F9 RID: 6649
		internal uint timestampRequests;

		// Token: 0x040019FA RID: 6650
		internal uint timestampReplies;

		// Token: 0x040019FB RID: 6651
		internal uint addressMaskRequests;

		// Token: 0x040019FC RID: 6652
		internal uint addressMaskReplies;
	}
}
