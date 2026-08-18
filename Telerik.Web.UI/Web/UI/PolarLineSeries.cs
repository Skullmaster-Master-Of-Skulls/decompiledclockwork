using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000500 RID: 1280
	public class PolarLineSeries : PolarSeriesBase
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x0009660A File Offset: 0x0009480A
		public PolarLineSeries()
		{
			this.sType = SeriesType.PolarLine;
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0009661A File Offset: 0x0009481A
		[Description("Series line visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[DefaultValue("LineAppearance")]
		public LineAppearance LineAppearance
		{
			get
			{
				if (this._lineAppearance == null)
				{
					this._lineAppearance = new LineAppearance("la", base.ViewState);
				}
				return this._lineAppearance;
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x00096640 File Offset: 0x00094840
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(base.Serialize());
			this.SerializeLine(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0009668C File Offset: 0x0009488C
		protected virtual void SerializeLine(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.AppendFormat(",{0}", this.LineAppearance.Serialize());
		}

		// Token: 0x04000C3D RID: 3133
		private LineAppearance _lineAppearance;
	}
}
