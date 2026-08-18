using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000B8F RID: 2959
	public class DonutSeries : PieSeriesBase
	{
		// Token: 0x06006FCC RID: 28620 RVA: 0x001A2053 File Offset: 0x001A0253
		public DonutSeries()
		{
			this.sType = SeriesType.Donut;
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x06006FCD RID: 28621 RVA: 0x001A2062 File Offset: 0x001A0262
		// (set) Token: 0x06006FCE RID: 28622 RVA: 0x001A2084 File Offset: 0x001A0284
		[DefaultValue(50)]
		public int HoleSize
		{
			get
			{
				return (int)(base.ViewState["HoleSize"] ?? 50);
			}
			set
			{
				base.ViewState["HoleSize"] = value;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x06006FCF RID: 28623 RVA: 0x001A209C File Offset: 0x001A029C
		[Description("Series labels visual settings")]
		[Category("Appearance")]
		[DefaultValue("LabelsAppearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DonutSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new DonutSeriesLabelsAppearance("dla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x001A20C2 File Offset: 0x001A02C2
		protected override void SerializeCommonProperties(StringBuilder sb)
		{
			base.SerializeCommonProperties(sb);
			if (this.HoleSize != 50)
			{
				sb.Append(", holeSize: ").Append(this.HoleSize);
			}
		}

		// Token: 0x06006FD1 RID: 28625 RVA: 0x001A20EC File Offset: 0x001A02EC
		internal override void SerializeLabels(StringBuilder sb)
		{
			string text = this.LabelsAppearance.Serialize();
			if (text != string.Empty)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append(",").Append(text);
			}
		}

		// Token: 0x04001E0F RID: 7695
		private DonutSeriesLabelsAppearance _labelsAppearance;
	}
}
