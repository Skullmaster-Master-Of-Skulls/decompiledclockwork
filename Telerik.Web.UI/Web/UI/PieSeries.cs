using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.PlotArea.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000B90 RID: 2960
	public class PieSeries : PieSeriesBase
	{
		// Token: 0x06006FD2 RID: 28626 RVA: 0x001A212B File Offset: 0x001A032B
		public PieSeries()
		{
			this.sType = SeriesType.Pie;
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06006FD3 RID: 28627 RVA: 0x001A213A File Offset: 0x001A033A
		[DefaultValue("LabelsAppearance")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series labels visual settings")]
		public PieSeriesLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new PieSeriesLabelsAppearance("pla", base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x001A2160 File Offset: 0x001A0360
		internal override void SerializeLabels(StringBuilder sb)
		{
			string text = this.LabelsAppearance.Serialize();
			if (text != string.Empty)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append(",").Append(text);
			}
		}

		// Token: 0x04001E10 RID: 7696
		private PieSeriesLabelsAppearance _labelsAppearance;
	}
}
