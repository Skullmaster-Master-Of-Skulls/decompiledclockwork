using System;

namespace MailBee.Mime
{
	// Token: 0x02000530 RID: 1328
	[Flags]
	public enum NewAttachmentOptions
	{
		// Token: 0x04001E25 RID: 7717
		None = 0,
		// Token: 0x04001E26 RID: 7718
		ReplaceIfExists = 1,
		// Token: 0x04001E27 RID: 7719
		Inline = 2,
		// Token: 0x04001E28 RID: 7720
		NoDefaultHeaders = 4,
		// Token: 0x04001E29 RID: 7721
		PathIsUri = 8,
		// Token: 0x04001E2A RID: 7722
		NoContentDispositionForInline = 16
	}
}
