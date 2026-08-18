using System;

namespace MailBee.Mime
{
	// Token: 0x02000540 RID: 1344
	[Flags]
	public enum HeaderEncodingOptions
	{
		// Token: 0x04001E7A RID: 7802
		None = 0,
		// Token: 0x04001E7B RID: 7803
		ForceEncoding = 1,
		// Token: 0x04001E7C RID: 7804
		Base64 = 2,
		// Token: 0x04001E7D RID: 7805
		IgnoreAttachments = 4
	}
}
