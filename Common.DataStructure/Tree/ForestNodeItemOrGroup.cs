using System;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000011 RID: 17
	public interface ForestNodeItemOrGroup<I, G> where I : class, ForestNodeItem where G : class, ForestNodeGroup
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107
		// (set) Token: 0x0600006C RID: 108
		I Item { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109
		// (set) Token: 0x0600006E RID: 110
		G Group { get; set; }
	}
}
