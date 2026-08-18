using System;

namespace MailBee
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum AuthenticationMethods
	{
		// Token: 0x04000066 RID: 102
		None = 0,
		// Token: 0x04000067 RID: 103
		Regular = 1,
		// Token: 0x04000068 RID: 104
		Apop = 2,
		// Token: 0x04000069 RID: 105
		SaslUserDefined = 4,
		// Token: 0x0400006A RID: 106
		SaslLogin = 8,
		// Token: 0x0400006B RID: 107
		SaslPlain = 16,
		// Token: 0x0400006C RID: 108
		SaslCramMD5 = 32,
		// Token: 0x0400006D RID: 109
		SaslDigestMD5 = 64,
		// Token: 0x0400006E RID: 110
		SaslNtlm = 128,
		// Token: 0x0400006F RID: 111
		SaslMsn = 256,
		// Token: 0x04000070 RID: 112
		SaslGssApi = 512,
		// Token: 0x04000071 RID: 113
		SaslOAuth = 1024,
		// Token: 0x04000072 RID: 114
		SaslOAuth2 = 2048,
		// Token: 0x04000073 RID: 115
		Auto = 4095
	}
}
