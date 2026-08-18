using System;
using Telerik.Charting;

namespace Telerik.Web.UI
{
	// Token: 0x02001803 RID: 6147
	internal class MapAreaBuilder : MapAreaBuilderBase
	{
		// Token: 0x0600EF09 RID: 61193 RVA: 0x0036671E File Offset: 0x0036491E
		public MapAreaBuilder(RadChart radChart)
		{
			this.radChartLocal = radChart;
		}

		// Token: 0x0600EF0A RID: 61194 RVA: 0x0036672D File Offset: 0x0036492D
		public override string GenerateImageMap()
		{
			return base.GenerateImageMap(this.radChartLocal.Chart);
		}

		// Token: 0x0600EF0B RID: 61195 RVA: 0x00366740 File Offset: 0x00364940
		protected override string GetPostBackEventReference(string arguments)
		{
			return this.radChartLocal.Page.ClientScript.GetPostBackEventReference(this.radChartLocal, arguments);
		}

		// Token: 0x0600EF0C RID: 61196 RVA: 0x0036675E File Offset: 0x0036495E
		protected override bool HasChartClickEvent()
		{
			return this.radChartLocal.HasClickEvent();
		}

		// Token: 0x040044EF RID: 17647
		private RadChart radChartLocal;
	}
}
