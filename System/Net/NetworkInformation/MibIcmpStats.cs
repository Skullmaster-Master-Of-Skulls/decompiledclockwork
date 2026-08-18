using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000603 RID: 1539
	internal struct MibIcmpStats
	{
		// Token: 0x04002D92 RID: 11666
		internal uint messages;

		// Token: 0x04002D93 RID: 11667
		internal uint errors;

		// Token: 0x04002D94 RID: 11668
		internal uint destinationUnreachables;

		// Token: 0x04002D95 RID: 11669
		internal uint timeExceeds;

		// Token: 0x04002D96 RID: 11670
		internal uint parameterProblems;

		// Token: 0x04002D97 RID: 11671
		internal uint sourceQuenches;

		// Token: 0x04002D98 RID: 11672
		internal uint redirects;

		// Token: 0x04002D99 RID: 11673
		internal uint echoRequests;

		// Token: 0x04002D9A RID: 11674
		internal uint echoReplies;

		// Token: 0x04002D9B RID: 11675
		internal uint timestampRequests;

		// Token: 0x04002D9C RID: 11676
		internal uint timestampReplies;

		// Token: 0x04002D9D RID: 11677
		internal uint addressMaskRequests;

		// Token: 0x04002D9E RID: 11678
		internal uint addressMaskReplies;
	}
}
