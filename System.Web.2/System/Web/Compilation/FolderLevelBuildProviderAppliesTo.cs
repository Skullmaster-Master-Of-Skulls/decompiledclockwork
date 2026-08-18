using System;

namespace System.Web.Compilation
{
	// Token: 0x0200083E RID: 2110
	[Flags]
	public enum FolderLevelBuildProviderAppliesTo
	{
		// Token: 0x040033E9 RID: 13289
		None = 0,
		// Token: 0x040033EA RID: 13290
		Code = 1,
		// Token: 0x040033EB RID: 13291
		WebReferences = 2,
		// Token: 0x040033EC RID: 13292
		LocalResources = 4,
		// Token: 0x040033ED RID: 13293
		GlobalResources = 8
	}
}
