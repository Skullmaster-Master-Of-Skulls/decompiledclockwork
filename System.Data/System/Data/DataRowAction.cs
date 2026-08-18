using System;

namespace System.Data
{
	// Token: 0x02000083 RID: 131
	[Flags]
	public enum DataRowAction
	{
		// Token: 0x04000758 RID: 1880
		Nothing = 0,
		// Token: 0x04000759 RID: 1881
		Delete = 1,
		// Token: 0x0400075A RID: 1882
		Change = 2,
		// Token: 0x0400075B RID: 1883
		Rollback = 4,
		// Token: 0x0400075C RID: 1884
		Commit = 8,
		// Token: 0x0400075D RID: 1885
		Add = 16,
		// Token: 0x0400075E RID: 1886
		ChangeOriginal = 32,
		// Token: 0x0400075F RID: 1887
		ChangeCurrentAndOriginal = 64
	}
}
