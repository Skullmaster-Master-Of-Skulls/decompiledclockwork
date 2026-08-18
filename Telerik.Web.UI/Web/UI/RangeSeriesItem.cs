using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x020003EF RID: 1007
	public class RangeSeriesItem : SeriesItemBase
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x0007B3BD File Offset: 0x000795BD
		public RangeSeriesItem()
		{
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0007B3C5 File Offset: 0x000795C5
		public RangeSeriesItem(decimal? from, decimal? to)
		{
			this.From = from;
			this.To = to;
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x0007B3DB File Offset: 0x000795DB
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x0007B3F2 File Offset: 0x000795F2
		[DefaultValue(null)]
		public decimal? From
		{
			get
			{
				return (decimal?)base.ViewState["From"];
			}
			set
			{
				base.ViewState["From"] = value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0007B40A File Offset: 0x0007960A
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x0007B421 File Offset: 0x00079621
		[DefaultValue(null)]
		public decimal? To
		{
			get
			{
				return (decimal?)base.ViewState["To"];
			}
			set
			{
				base.ViewState["To"] = value;
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0007B43C File Offset: 0x0007963C
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			this.SerializeItemSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0007B470 File Offset: 0x00079670
		private void SerializeItemSpecificProperties(StringBuilder sb)
		{
			sb.AppendFormat("from:{0},", HtmlChartHelper.ToStringInvariant(this.From));
			sb.AppendFormat("to:{0},", HtmlChartHelper.ToStringInvariant(this.To));
		}
	}
}
