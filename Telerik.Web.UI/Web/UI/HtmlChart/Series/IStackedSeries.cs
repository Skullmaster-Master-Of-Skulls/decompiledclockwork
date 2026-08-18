using System;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.Series
{
	// Token: 0x020003F6 RID: 1014
	public interface IStackedSeries
	{
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002542 RID: 9538
		// (set) Token: 0x06002543 RID: 9539
		bool? Stacked { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002544 RID: 9540
		// (set) Token: 0x06002545 RID: 9541
		HtmlChartStackType StackType { get; set; }
	}
}
