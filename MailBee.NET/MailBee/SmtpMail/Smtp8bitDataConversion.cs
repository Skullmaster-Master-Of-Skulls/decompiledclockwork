using System;

namespace MailBee.SmtpMail
{
	// Token: 0x02000164 RID: 356
	public enum Smtp8bitDataConversion
	{
		// Token: 0x04000894 RID: 2196
		DoNothing,
		// Token: 0x04000895 RID: 2197
		ConvertAndWarn,
		// Token: 0x04000896 RID: 2198
		ConvertAndForget,
		// Token: 0x04000897 RID: 2199
		WarnOnly,
		// Token: 0x04000898 RID: 2200
		ThrowException
	}
}
