using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000007 RID: 7
	public interface IRenameable
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62
		string OriginalName { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63
		bool WasRenamed { get; }
	}
}
