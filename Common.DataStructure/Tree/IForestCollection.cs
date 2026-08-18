using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x0200000E RID: 14
	public interface IForestCollection<I, G> where I : class, ForestNodeItem where G : class, ForestNodeGroup
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		IList<I> Items { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		IList<G> Groups { get; set; }
	}
}
