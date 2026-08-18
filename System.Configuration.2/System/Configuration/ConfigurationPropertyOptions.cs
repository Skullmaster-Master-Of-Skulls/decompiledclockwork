using System;

namespace System.Configuration
{
	// Token: 0x02000035 RID: 53
	[Flags]
	public enum ConfigurationPropertyOptions
	{
		// Token: 0x040001FA RID: 506
		None = 0,
		// Token: 0x040001FB RID: 507
		IsDefaultCollection = 1,
		// Token: 0x040001FC RID: 508
		IsRequired = 2,
		// Token: 0x040001FD RID: 509
		IsKey = 4,
		// Token: 0x040001FE RID: 510
		IsTypeStringTransformationRequired = 8,
		// Token: 0x040001FF RID: 511
		IsAssemblyStringTransformationRequired = 16,
		// Token: 0x04000200 RID: 512
		IsVersionCheckRequired = 32
	}
}
