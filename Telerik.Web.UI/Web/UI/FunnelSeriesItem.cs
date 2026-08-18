using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000514 RID: 1300
	public class FunnelSeriesItem : SingleValueSeriesItem
	{
		// Token: 0x06002E7D RID: 11901 RVA: 0x0009853D File Offset: 0x0009673D
		public FunnelSeriesItem()
		{
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00098545 File Offset: 0x00096745
		public FunnelSeriesItem(decimal? y) : base(y)
		{
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0009854E File Offset: 0x0009674E
		public FunnelSeriesItem(decimal? y, Color backgroundColor) : base(y, backgroundColor)
		{
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x00098558 File Offset: 0x00096758
		public FunnelSeriesItem(decimal? y, string name, Color backgroundColor) : base(y, backgroundColor)
		{
			this.Name = name;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x00098569 File Offset: 0x00096769
		public FunnelSeriesItem(decimal? y, string name, bool visible, Color backgroundColor) : base(y, backgroundColor)
		{
			this.Name = name;
			this.Visible = visible;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00098582 File Offset: 0x00096782
		public FunnelSeriesItem(decimal? y, string name, bool visible, Color backgroundColor, bool visibleInLegend) : base(y, backgroundColor)
		{
			this.Name = name;
			this.Visible = visible;
			this.VisibleInLegend = visibleInLegend;
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000985A3 File Offset: 0x000967A3
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000985C3 File Offset: 0x000967C3
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

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000985D6 File Offset: 0x000967D6
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000985F7 File Offset: 0x000967F7
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

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x0009860F File Offset: 0x0009680F
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x00098630 File Offset: 0x00096830
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

		// Token: 0x06002E89 RID: 11913 RVA: 0x00098648 File Offset: 0x00096848
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			this.SerializeName(stringBuilder);
			this.SerializeVisible(stringBuilder);
			this.SerializeVisibleInLegend(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0009868A File Offset: 0x0009688A
		private void SerializeName(StringBuilder sb)
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				sb.AppendFormat("category:'{0}',", this.Name);
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000986AB File Offset: 0x000968AB
		private void SerializeVisible(StringBuilder sb)
		{
			if (!this.Visible)
			{
				sb.Append("visible: false,");
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000986C1 File Offset: 0x000968C1
		private void SerializeVisibleInLegend(StringBuilder sb)
		{
			if (!this.VisibleInLegend)
			{
				sb.Append("visibleInLegend: false,");
			}
		}
	}
}
