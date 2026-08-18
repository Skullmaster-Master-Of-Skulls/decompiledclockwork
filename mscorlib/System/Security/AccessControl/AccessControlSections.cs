using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200091D RID: 2333
	[Flags]
	public enum AccessControlSections
	{
		// Token: 0x04002BBB RID: 11195
		None = 0,
		// Token: 0x04002BBC RID: 11196
		Audit = 1,
		// Token: 0x04002BBD RID: 11197
		Access = 2,
		// Token: 0x04002BBE RID: 11198
		Owner = 4,
		// Token: 0x04002BBF RID: 11199
		Group = 8,
		// Token: 0x04002BC0 RID: 11200
		All = 15
	}
}
