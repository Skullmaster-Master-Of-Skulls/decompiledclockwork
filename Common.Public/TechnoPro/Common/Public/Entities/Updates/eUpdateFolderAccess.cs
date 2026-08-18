using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000151 RID: 337
	[Flags]
	public enum eUpdateFolderAccess
	{
		// Token: 0x04000653 RID: 1619
		None = 0,
		// Token: 0x04000654 RID: 1620
		Public = 1,
		// Token: 0x04000655 RID: 1621
		Private = 2,
		// Token: 0x04000656 RID: 1622
		Computer = 4,
		// Token: 0x04000657 RID: 1623
		All = 7
	}
}
