using System;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Series;

namespace Telerik.Web.UI
{
	// Token: 0x02000502 RID: 1282
	public class PolarScatterSeries : PolarSeriesBase
	{
		// Token: 0x06002DDB RID: 11739 RVA: 0x000966D5 File Offset: 0x000948D5
		public PolarScatterSeries()
		{
			this.sType = SeriesType.PolarScatter;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000966E5 File Offset: 0x000948E5
		internal override string Serialize()
		{
			return string.Format("{{{0}}}", base.Serialize());
		}
	}
}
