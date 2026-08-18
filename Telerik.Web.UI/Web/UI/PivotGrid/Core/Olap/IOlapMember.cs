using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF5 RID: 3317
	internal interface IOlapMember : IOlapElement
	{
		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x06007BDD RID: 31709
		string HierarchyName { get; }

		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x06007BDE RID: 31710
		int LevelNumber { get; }

		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x06007BDF RID: 31711
		string LevelName { get; }

		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x06007BE0 RID: 31712
		IList<string> SortKeys { get; }
	}
}
