using System;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020004F9 RID: 1273
	public class PolarSeriesItem : SeriesItemBase
	{
		// Token: 0x06002D61 RID: 11617 RVA: 0x00094E97 File Offset: 0x00093097
		public PolarSeriesItem()
		{
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00094E9F File Offset: 0x0009309F
		public PolarSeriesItem(decimal? angle, decimal? radius)
		{
			this.Angle = angle;
			this.Radius = radius;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x00094EB5 File Offset: 0x000930B5
		public PolarSeriesItem(decimal? angle, decimal? radius, Color backgroundColor) : this(angle, radius)
		{
			base.BackgroundColor = backgroundColor;
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x00094EC6 File Offset: 0x000930C6
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x00094EE2 File Offset: 0x000930E2
		public decimal? Angle
		{
			get
			{
				return (decimal?)(base.ViewState["Angle"] ?? null);
			}
			set
			{
				base.ViewState["Angle"] = value;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x00094EFA File Offset: 0x000930FA
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x00094F16 File Offset: 0x00093116
		public decimal? Radius
		{
			get
			{
				return (decimal?)(base.ViewState["Radius"] ?? null);
			}
			set
			{
				base.ViewState["Radius"] = value;
			}
		}
	}
}
