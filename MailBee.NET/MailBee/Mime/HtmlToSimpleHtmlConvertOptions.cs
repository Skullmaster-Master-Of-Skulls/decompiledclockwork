using System;

namespace MailBee.Mime
{
	// Token: 0x02000533 RID: 1331
	[Flags]
	public enum HtmlToSimpleHtmlConvertOptions
	{
		// Token: 0x04001E33 RID: 7731
		None = 0,
		// Token: 0x04001E34 RID: 7732
		AddImgAltText = 1,
		// Token: 0x04001E35 RID: 7733
		WriteImageIfNoAlt = 2,
		// Token: 0x04001E36 RID: 7734
		MakeLinkForImg = 4
	}
}
