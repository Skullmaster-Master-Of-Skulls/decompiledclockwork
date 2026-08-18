using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000211 RID: 529
	[Flags]
	[Serializable]
	public enum eReportSource
	{
		// Token: 0x04000E33 RID: 3635
		Unknown = 0,
		// Token: 0x04000E34 RID: 3636
		TechnoPro = 1,
		// Token: 0x04000E35 RID: 3637
		Client = 2,
		// Token: 0x04000E36 RID: 3638
		All = 3
	}
}
