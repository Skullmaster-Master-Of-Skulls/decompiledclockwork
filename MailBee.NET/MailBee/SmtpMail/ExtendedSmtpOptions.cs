using System;

namespace MailBee.SmtpMail
{
	// Token: 0x02000163 RID: 355
	[Flags]
	public enum ExtendedSmtpOptions
	{
		// Token: 0x0400088E RID: 2190
		Default = 0,
		// Token: 0x0400088F RID: 2191
		NoChunking = 1,
		// Token: 0x04000890 RID: 2192
		NoDsn = 2,
		// Token: 0x04000891 RID: 2193
		NoSize = 4,
		// Token: 0x04000892 RID: 2194
		ClassicSmtpMode = 8
	}
}
