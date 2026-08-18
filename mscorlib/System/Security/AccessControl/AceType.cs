using System;

namespace System.Security.AccessControl
{
	// Token: 0x020008F8 RID: 2296
	public enum AceType : byte
	{
		// Token: 0x04002B14 RID: 11028
		AccessAllowed,
		// Token: 0x04002B15 RID: 11029
		AccessDenied,
		// Token: 0x04002B16 RID: 11030
		SystemAudit,
		// Token: 0x04002B17 RID: 11031
		SystemAlarm,
		// Token: 0x04002B18 RID: 11032
		AccessAllowedCompound,
		// Token: 0x04002B19 RID: 11033
		AccessAllowedObject,
		// Token: 0x04002B1A RID: 11034
		AccessDeniedObject,
		// Token: 0x04002B1B RID: 11035
		SystemAuditObject,
		// Token: 0x04002B1C RID: 11036
		SystemAlarmObject,
		// Token: 0x04002B1D RID: 11037
		AccessAllowedCallback,
		// Token: 0x04002B1E RID: 11038
		AccessDeniedCallback,
		// Token: 0x04002B1F RID: 11039
		AccessAllowedCallbackObject,
		// Token: 0x04002B20 RID: 11040
		AccessDeniedCallbackObject,
		// Token: 0x04002B21 RID: 11041
		SystemAuditCallback,
		// Token: 0x04002B22 RID: 11042
		SystemAlarmCallback,
		// Token: 0x04002B23 RID: 11043
		SystemAuditCallbackObject,
		// Token: 0x04002B24 RID: 11044
		SystemAlarmCallbackObject,
		// Token: 0x04002B25 RID: 11045
		MaxDefinedAceType = 16
	}
}
