using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9B RID: 7067
	public interface IGroup
	{
		// Token: 0x1700537B RID: 21371
		// (get) Token: 0x060111A8 RID: 70056
		object Key { get; }

		// Token: 0x1700537C RID: 21372
		// (get) Token: 0x060111A9 RID: 70057
		IEnumerable Items { get; }

		// Token: 0x1700537D RID: 21373
		// (get) Token: 0x060111AA RID: 70058
		bool HasSubgroups { get; }

		// Token: 0x1700537E RID: 21374
		// (get) Token: 0x060111AB RID: 70059
		int ItemCount { get; }

		// Token: 0x1700537F RID: 21375
		// (get) Token: 0x060111AC RID: 70060
		ReadOnlyCollection<IGroup> Subgroups { get; }
	}
}
