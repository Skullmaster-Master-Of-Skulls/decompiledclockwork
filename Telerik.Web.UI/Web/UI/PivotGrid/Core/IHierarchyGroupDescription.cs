using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006D3 RID: 1747
	internal interface IHierarchyGroupDescription
	{
		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06003EA8 RID: 16040
		IEnumerable<IGroupDescription> Levels { get; }

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06003EA9 RID: 16041
		int LevelsCount { get; }

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06003EAA RID: 16042
		bool IgnoreChildren { get; }
	}
}
