using System;
using System.Text;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x02000501 RID: 1281
	public class PolarAreaSeries : PolarLineSeries
	{
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000966AC File Offset: 0x000948AC
		public PolarAreaSeries()
		{
			this.sType = SeriesType.PolarArea;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000966BC File Offset: 0x000948BC
		protected override void SerializeLine(StringBuilder sb)
		{
			sb.AppendFormat(",line:{{{0}}},", base.LineAppearance.Serialize());
		}
	}
}
