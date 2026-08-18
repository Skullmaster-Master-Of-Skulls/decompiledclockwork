using System;

namespace System.Web.Configuration
{
	// Token: 0x02000746 RID: 1862
	internal enum RpcAuthent
	{
		// Token: 0x04002F90 RID: 12176
		None,
		// Token: 0x04002F91 RID: 12177
		DcePrivate,
		// Token: 0x04002F92 RID: 12178
		DcePublic,
		// Token: 0x04002F93 RID: 12179
		DecPublic = 4,
		// Token: 0x04002F94 RID: 12180
		GssNegotiate = 9,
		// Token: 0x04002F95 RID: 12181
		WinNT,
		// Token: 0x04002F96 RID: 12182
		GssSchannel = 14,
		// Token: 0x04002F97 RID: 12183
		GssKerberos = 16,
		// Token: 0x04002F98 RID: 12184
		DPA,
		// Token: 0x04002F99 RID: 12185
		MSN,
		// Token: 0x04002F9A RID: 12186
		Digest = 21,
		// Token: 0x04002F9B RID: 12187
		MQ = 100,
		// Token: 0x04002F9C RID: 12188
		Default = -1
	}
}
