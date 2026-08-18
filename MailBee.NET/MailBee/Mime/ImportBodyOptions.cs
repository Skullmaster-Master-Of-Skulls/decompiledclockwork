using System;

namespace MailBee.Mime
{
	// Token: 0x02000545 RID: 1349
	[Flags]
	public enum ImportBodyOptions
	{
		// Token: 0x04001E8F RID: 7823
		None = 0,
		// Token: 0x04001E90 RID: 7824
		Append = 1,
		// Token: 0x04001E91 RID: 7825
		PathIsUri = 2,
		// Token: 0x04001E92 RID: 7826
		ImportRelatedFiles = 4,
		// Token: 0x04001E93 RID: 7827
		ImportRelatedFilesFromUris = 8,
		// Token: 0x04001E94 RID: 7828
		PreferCharsetFromMetaTag = 16
	}
}
