using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000515 RID: 1301
	public class PieSeriesItem : SingleValueSeriesItem
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x000986D7 File Offset: 0x000968D7
		public PieSeriesItem()
		{
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000986DF File Offset: 0x000968DF
		public PieSeriesItem(decimal? y) : base(y)
		{
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000986E8 File Offset: 0x000968E8
		public PieSeriesItem(decimal? y, Color backgroundColor) : base(y, backgroundColor)
		{
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000986F2 File Offset: 0x000968F2
		public PieSeriesItem(decimal? y, Color backgroundColor, string name) : this(y, backgroundColor)
		{
			this.Name = name;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00098703 File Offset: 0x00096903
		public PieSeriesItem(decimal? y, Color backgroundColor, string name, bool exploded) : this(y, backgroundColor, name)
		{
			this.Exploded = exploded;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x00098716 File Offset: 0x00096916
		public PieSeriesItem(decimal? y, Color backgroundColor, string name, bool exploded, bool visible) : this(y, backgroundColor, name)
		{
			this.Exploded = exploded;
			this.Visible = visible;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x00098731 File Offset: 0x00096931
		public PieSeriesItem(decimal? y, Color backgroundColor, string name, bool exploded, bool visible, bool visibleInLegend) : this(y, backgroundColor, name)
		{
			this.Exploded = exploded;
			this.Visible = visible;
			this.VisibleInLegend = visibleInLegend;
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x00098754 File Offset: 0x00096954
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x00098774 File Offset: 0x00096974
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x00098787 File Offset: 0x00096987
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x000987A8 File Offset: 0x000969A8
		[DefaultValue(false)]
		public bool Exploded
		{
			get
			{
				return (bool)(base.ViewState["Exploded"] ?? false);
			}
			set
			{
				base.ViewState["Exploded"] = value;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000987C0 File Offset: 0x000969C0
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x000987E1 File Offset: 0x000969E1
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x000987F9 File Offset: 0x000969F9
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x0009881A File Offset: 0x00096A1A
		[DefaultValue(true)]
		public bool VisibleInLegend
		{
			get
			{
				return (bool)(base.ViewState["VisibleInLegend"] ?? true);
			}
			set
			{
				base.ViewState["VisibleInLegend"] = value;
			}
		}
	}
}
