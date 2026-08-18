using System;

namespace System.Security.AccessControl
{
	// Token: 0x020008F9 RID: 2297
	[Flags]
	public enum AceFlags : byte
	{
		// Token: 0x04002B27 RID: 11047
		None = 0,
		// Token: 0x04002B28 RID: 11048
		ObjectInherit = 1,
		// Token: 0x04002B29 RID: 11049
		ContainerInherit = 2,
		// Token: 0x04002B2A RID: 11050
		NoPropagateInherit = 4,
		// Token: 0x04002B2B RID: 11051
		InheritOnly = 8,
		// Token: 0x04002B2C RID: 11052
		Inherited = 16,
		// Token: 0x04002B2D RID: 11053
		SuccessfulAccess = 64,
		// Token: 0x04002B2E RID: 11054
		FailedAccess = 128,
		// Token: 0x04002B2F RID: 11055
		InheritanceFlags = 15,
		// Token: 0x04002B30 RID: 11056
		AuditFlags = 192
	}
}
