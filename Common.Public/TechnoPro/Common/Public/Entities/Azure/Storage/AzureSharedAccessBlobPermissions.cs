using System;

namespace TechnoPro.Common.Public.Entities.Azure.Storage
{
	// Token: 0x02000478 RID: 1144
	[Flags]
	[Serializable]
	public enum AzureSharedAccessBlobPermissions
	{
		// Token: 0x04001A05 RID: 6661
		None = 0,
		// Token: 0x04001A06 RID: 6662
		Read = 1,
		// Token: 0x04001A07 RID: 6663
		Write = 2,
		// Token: 0x04001A08 RID: 6664
		Delete = 4,
		// Token: 0x04001A09 RID: 6665
		List = 8,
		// Token: 0x04001A0A RID: 6666
		Add = 16,
		// Token: 0x04001A0B RID: 6667
		Create = 32
	}
}
