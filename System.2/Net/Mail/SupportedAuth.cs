using System;

namespace System.Net.Mail
{
	// Token: 0x02000296 RID: 662
	internal enum SupportedAuth
	{
		// Token: 0x0400188D RID: 6285
		None,
		// Token: 0x0400188E RID: 6286
		Login,
		// Token: 0x0400188F RID: 6287
		NTLM,
		// Token: 0x04001890 RID: 6288
		GSSAPI = 4,
		// Token: 0x04001891 RID: 6289
		WDigest = 8
	}
}
