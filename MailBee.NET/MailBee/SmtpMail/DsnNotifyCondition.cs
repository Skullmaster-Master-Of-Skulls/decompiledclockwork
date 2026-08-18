using System;

namespace MailBee.SmtpMail
{
	// Token: 0x02000136 RID: 310
	[Flags]
	public enum DsnNotifyCondition
	{
		// Token: 0x040007C5 RID: 1989
		Default = 0,
		// Token: 0x040007C6 RID: 1990
		Failure = 1,
		// Token: 0x040007C7 RID: 1991
		Delay = 2,
		// Token: 0x040007C8 RID: 1992
		Success = 4,
		// Token: 0x040007C9 RID: 1993
		Always = 7,
		// Token: 0x040007CA RID: 1994
		Never = 8
	}
}
