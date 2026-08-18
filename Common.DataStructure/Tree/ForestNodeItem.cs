using System;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x0200000F RID: 15
	public interface ForestNodeItem
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		int Id { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		int ParentId { get; set; }
	}
}
