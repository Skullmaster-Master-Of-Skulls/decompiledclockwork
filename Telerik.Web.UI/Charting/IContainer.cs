using System;
using System.Collections.Generic;

namespace Telerik.Charting
{
	// Token: 0x020016E0 RID: 5856
	public interface IContainer
	{
		// Token: 0x1700455A RID: 17754
		// (get) Token: 0x0600E313 RID: 58131
		List<IOrdering> OrderList { get; }

		// Token: 0x1700455B RID: 17755
		// (get) Token: 0x0600E314 RID: 58132
		int NextPosition { get; }

		// Token: 0x0600E315 RID: 58133
		int GetOrder(IOrdering element);

		// Token: 0x0600E316 RID: 58134
		void Add(IOrdering element);

		// Token: 0x0600E317 RID: 58135
		void Insert(int order, IOrdering element);

		// Token: 0x0600E318 RID: 58136
		void Remove(IOrdering element);

		// Token: 0x0600E319 RID: 58137
		void RemoveAt(int index);

		// Token: 0x0600E31A RID: 58138
		void ReIndex();
	}
}
