using System;

namespace System.Web.UI
{
	// Token: 0x020002D0 RID: 720
	[Flags]
	internal enum OutputCacheParameter
	{
		// Token: 0x04001B1B RID: 6939
		CacheProfile = 1,
		// Token: 0x04001B1C RID: 6940
		Duration = 2,
		// Token: 0x04001B1D RID: 6941
		Enabled = 4,
		// Token: 0x04001B1E RID: 6942
		Location = 8,
		// Token: 0x04001B1F RID: 6943
		NoStore = 16,
		// Token: 0x04001B20 RID: 6944
		SqlDependency = 32,
		// Token: 0x04001B21 RID: 6945
		VaryByControl = 64,
		// Token: 0x04001B22 RID: 6946
		VaryByCustom = 128,
		// Token: 0x04001B23 RID: 6947
		VaryByHeader = 256,
		// Token: 0x04001B24 RID: 6948
		VaryByParam = 512,
		// Token: 0x04001B25 RID: 6949
		VaryByContentEncoding = 1024
	}
}
