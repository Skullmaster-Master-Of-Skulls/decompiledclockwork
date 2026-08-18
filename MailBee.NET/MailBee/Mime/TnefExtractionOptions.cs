using System;

namespace MailBee.Mime
{
	// Token: 0x0200052F RID: 1327
	[Flags]
	public enum TnefExtractionOptions
	{
		// Token: 0x04001E21 RID: 7713
		None = 0,
		// Token: 0x04001E22 RID: 7714
		ExtractAttachments = 1,
		// Token: 0x04001E23 RID: 7715
		ExtractRtfBody = 2
	}
}
