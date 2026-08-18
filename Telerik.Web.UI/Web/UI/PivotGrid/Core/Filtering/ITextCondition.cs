using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C8 RID: 1736
	internal interface ITextCondition
	{
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06003E3D RID: 15933
		// (set) Token: 0x06003E3E RID: 15934
		string Pattern { get; set; }

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06003E3F RID: 15935
		// (set) Token: 0x06003E40 RID: 15936
		TextComparison Comparison { get; set; }

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06003E41 RID: 15937
		// (set) Token: 0x06003E42 RID: 15938
		bool IgnoreCase { get; set; }
	}
}
