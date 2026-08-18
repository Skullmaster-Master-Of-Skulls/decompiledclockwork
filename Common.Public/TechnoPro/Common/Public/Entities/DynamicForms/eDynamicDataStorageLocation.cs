using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000364 RID: 868
	[Flags]
	[Serializable]
	public enum eDynamicDataStorageLocation
	{
		// Token: 0x040015CF RID: 5583
		Unknown = 0,
		// Token: 0x040015D0 RID: 5584
		MainInfo = 1,
		// Token: 0x040015D1 RID: 5585
		OtherInfo = 2,
		// Token: 0x040015D2 RID: 5586
		DateTimeInfo = 4,
		// Token: 0x040015D3 RID: 5587
		ImageInfo = 8
	}
}
