using System;

namespace MailBee.Mime
{
	// Token: 0x02000536 RID: 1334
	[Flags]
	public enum HtmlToPlainConvertOptions
	{
		// Token: 0x04001E3E RID: 7742
		None = 0,
		// Token: 0x04001E3F RID: 7743
		AddImgAltText = 1,
		// Token: 0x04001E40 RID: 7744
		WriteImageIfNoAlt = 2,
		// Token: 0x04001E41 RID: 7745
		AddUriForImg = 4,
		// Token: 0x04001E42 RID: 7746
		AddUriForAHRef = 8
	}
}
