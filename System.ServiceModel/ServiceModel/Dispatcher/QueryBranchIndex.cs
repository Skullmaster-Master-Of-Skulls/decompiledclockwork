using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048A RID: 1162
	internal abstract class QueryBranchIndex
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002CF8 RID: 11512
		internal abstract int Count { get; }

		// Token: 0x17000ACC RID: 2764
		internal abstract QueryBranch this[object key]
		{
			get;
			set;
		}

		// Token: 0x06002CFB RID: 11515
		internal abstract void CollectXPathFilters(ICollection<MessageFilter> filters);

		// Token: 0x06002CFC RID: 11516
		internal abstract void Match(int valIndex, ref Value val, QueryBranchResultSet results);

		// Token: 0x06002CFD RID: 11517
		internal abstract void Remove(object key);

		// Token: 0x06002CFE RID: 11518
		internal abstract void Trim();
	}
}
