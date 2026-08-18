using System;

namespace System.Windows.Forms
{
	// Token: 0x02000363 RID: 867
	public enum SecurityIDType
	{
		// Token: 0x040021C9 RID: 8649
		User = 1,
		// Token: 0x040021CA RID: 8650
		Group,
		// Token: 0x040021CB RID: 8651
		Domain,
		// Token: 0x040021CC RID: 8652
		Alias,
		// Token: 0x040021CD RID: 8653
		WellKnownGroup,
		// Token: 0x040021CE RID: 8654
		DeletedAccount,
		// Token: 0x040021CF RID: 8655
		Invalid,
		// Token: 0x040021D0 RID: 8656
		Unknown,
		// Token: 0x040021D1 RID: 8657
		Computer
	}
}
