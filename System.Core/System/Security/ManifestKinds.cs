using System;

namespace System.Security
{
	// Token: 0x020000DA RID: 218
	[Flags]
	public enum ManifestKinds
	{
		// Token: 0x040005C9 RID: 1481
		None = 0,
		// Token: 0x040005CA RID: 1482
		Deployment = 1,
		// Token: 0x040005CB RID: 1483
		Application = 2,
		// Token: 0x040005CC RID: 1484
		ApplicationAndDeployment = 3
	}
}
