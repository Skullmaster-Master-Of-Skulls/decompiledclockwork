using System;
using System.ComponentModel;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems
{
	// Token: 0x02000510 RID: 1296
	public abstract class TwoValueSeriesItem : SingleValueSeriesItem
	{
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000982BE File Offset: 0x000964BE
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x000982D5 File Offset: 0x000964D5
		[DefaultValue(null)]
		public decimal? X
		{
			get
			{
				return (decimal?)base.ViewState["X"];
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}
	}
}
