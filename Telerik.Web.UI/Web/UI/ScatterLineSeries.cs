using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI
{
	// Token: 0x02000B92 RID: 2962
	public class ScatterLineSeries : ScatterSeriesBase
	{
		// Token: 0x06006FE2 RID: 28642 RVA: 0x001A2693 File Offset: 0x001A0893
		public ScatterLineSeries()
		{
			this.sType = SeriesType.ScatterLine;
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06006FE3 RID: 28643 RVA: 0x001A26A2 File Offset: 0x001A08A2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Series line visual settings")]
		[DefaultValue("LineAppearance")]
		public LineAppearance LineAppearance
		{
			get
			{
				if (this._lineAppearance == null)
				{
					this._lineAppearance = new LineAppearance("lineAppearance", base.ViewState);
				}
				return this._lineAppearance;
			}
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x001A26C8 File Offset: 0x001A08C8
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(base.Serialize());
			this.SerializeSeriesSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x001A2714 File Offset: 0x001A0914
		internal override void SerializeSeriesSpecificProperties(StringBuilder sb)
		{
			this.SerializeLine(sb);
		}

		// Token: 0x06006FE6 RID: 28646 RVA: 0x001A271D File Offset: 0x001A091D
		private void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",{0}", this.LineAppearance.Serialize());
		}

		// Token: 0x04001E12 RID: 7698
		private LineAppearance _lineAppearance;
	}
}
