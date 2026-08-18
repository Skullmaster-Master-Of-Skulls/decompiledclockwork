using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020003EE RID: 1006
	public class WaterfallSeriesItem : SingleValueSeriesItem
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x0007B2B9 File Offset: 0x000794B9
		public WaterfallSeriesItem()
		{
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0007B2C1 File Offset: 0x000794C1
		public WaterfallSeriesItem(decimal? y) : base(y)
		{
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0007B2CA File Offset: 0x000794CA
		public WaterfallSeriesItem(decimal? y, Color backgroundColor) : base(y, backgroundColor)
		{
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0007B2D4 File Offset: 0x000794D4
		public WaterfallSeriesItem(decimal? y, Color backgroundColor, SummaryType summary) : base(y, backgroundColor)
		{
			this.Summary = summary;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0007B2E5 File Offset: 0x000794E5
		public WaterfallSeriesItem(SummaryType summary)
		{
			this.Summary = summary;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0007B2F4 File Offset: 0x000794F4
		public WaterfallSeriesItem(SummaryType summary, Color backgroundColor) : base(null, backgroundColor)
		{
			this.Summary = summary;
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x0007B318 File Offset: 0x00079518
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x0007B339 File Offset: 0x00079539
		[DefaultValue(SummaryType.Default)]
		public SummaryType Summary
		{
			get
			{
				return (SummaryType)(base.ViewState["Summary"] ?? SummaryType.Default);
			}
			set
			{
				base.ViewState["Summary"] = value;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0007B354 File Offset: 0x00079554
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			this.SerializeItemSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0007B388 File Offset: 0x00079588
		private void SerializeItemSpecificProperties(StringBuilder sb)
		{
			if (this.Summary != SummaryType.Default)
			{
				sb.AppendFormat("type:'{0}',", HtmlChartHelper.StringToLowerCamelCase(Enum.GetName(typeof(SummaryType), this.Summary)));
			}
		}
	}
}
