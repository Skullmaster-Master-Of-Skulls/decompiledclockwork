using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001974 RID: 6516
	public interface IRadListViewSingleValueExpression
	{
		// Token: 0x17004C38 RID: 19512
		// (get) Token: 0x0600FC5D RID: 64605
		// (set) Token: 0x0600FC5E RID: 64606
		object CurrentValue { get; set; }

		// Token: 0x17004C39 RID: 19513
		// (get) Token: 0x0600FC5F RID: 64607
		Type ItemType { get; }
	}
}
