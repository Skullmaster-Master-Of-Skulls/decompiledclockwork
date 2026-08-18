using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D36 RID: 3382
	public interface IGroup
	{
		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x06007DB3 RID: 32179
		object Name { get; }

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x06007DB4 RID: 32180
		IReadOnlyList<IGroup> Groups { get; }

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06007DB5 RID: 32181
		bool HasGroups { get; }

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x06007DB6 RID: 32182
		IGroup Parent { get; }

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x06007DB7 RID: 32183
		GroupType Type { get; }

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x06007DB8 RID: 32184
		int Level { get; }
	}
}
