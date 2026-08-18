using System;

namespace MailBee.Outlook
{
	// Token: 0x02000597 RID: 1431
	[Serializable]
	internal class MarkUnsupportedException : HPSFException
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x000E2556 File Offset: 0x000E1556
		public MarkUnsupportedException()
		{
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000E255E File Offset: 0x000E155E
		public MarkUnsupportedException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000E2567 File Offset: 0x000E1567
		public MarkUnsupportedException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000E2570 File Offset: 0x000E1570
		public MarkUnsupportedException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
