using System;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000034 RID: 52
	[Flags]
	public enum BalancingModes
	{
		// Token: 0x0400008A RID: 138
		None = 0,
		// Token: 0x0400008B RID: 139
		BacktrackOnFailure = 1,
		// Token: 0x0400008C RID: 140
		NoErrorOnFailure = 2,
		// Token: 0x0400008D RID: 141
		AllowCommentsAndTemplates = 4,
		// Token: 0x0400008E RID: 142
		AllowEmbeddedTransitions = 8
	}
}
