using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000511 RID: 1297
	public class BubbleSeriesItem : TwoValueSeriesItem
	{
		// Token: 0x06002E63 RID: 11875 RVA: 0x000982F5 File Offset: 0x000964F5
		public BubbleSeriesItem()
		{
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000982FD File Offset: 0x000964FD
		public BubbleSeriesItem(decimal? x, decimal? y, decimal? size)
		{
			base.X = x;
			base.Y = y;
			this.Size = size;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0009831A File Offset: 0x0009651A
		public BubbleSeriesItem(decimal? x, decimal? y, decimal? size, string tooltip) : this(x, y, size)
		{
			this.Tooltip = tooltip;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0009832D File Offset: 0x0009652D
		public BubbleSeriesItem(decimal? x, decimal? y, decimal? size, string tooltip, Color backgroundColor) : this(x, y, size, tooltip)
		{
			base.BackgroundColor = backgroundColor;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x00098342 File Offset: 0x00096542
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x00098359 File Offset: 0x00096559
		[DefaultValue(null)]
		public decimal? Size
		{
			get
			{
				return (decimal?)base.ViewState["Size"];
			}
			set
			{
				base.ViewState["Size"] = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x00098371 File Offset: 0x00096571
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x00098391 File Offset: 0x00096591
		[DefaultValue("")]
		public string Tooltip
		{
			get
			{
				return (string)(base.ViewState["Tooltip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Tooltip"] = value;
			}
		}
	}
}
