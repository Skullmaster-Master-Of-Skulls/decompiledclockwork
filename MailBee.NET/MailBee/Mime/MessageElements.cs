using System;

namespace MailBee.Mime
{
	// Token: 0x0200053E RID: 1342
	[Flags]
	public enum MessageElements
	{
		// Token: 0x04001E6A RID: 7786
		None = 0,
		// Token: 0x04001E6B RID: 7787
		Recipients = 1,
		// Token: 0x04001E6C RID: 7788
		Attachments = 2,
		// Token: 0x04001E6D RID: 7789
		CustomHeaders = 4,
		// Token: 0x04001E6E RID: 7790
		RouteHeaders = 8,
		// Token: 0x04001E6F RID: 7791
		RawBody = 16
	}
}
