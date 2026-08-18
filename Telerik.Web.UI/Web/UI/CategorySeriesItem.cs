using System;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000513 RID: 1299
	public class CategorySeriesItem : SingleValueSeriesItem
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x000984F4 File Offset: 0x000966F4
		public CategorySeriesItem()
		{
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000984FC File Offset: 0x000966FC
		public CategorySeriesItem(decimal? y) : base(y)
		{
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x00098505 File Offset: 0x00096705
		public CategorySeriesItem(decimal? y, Color backgroundColor) : base(y, backgroundColor)
		{
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x00098510 File Offset: 0x00096710
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
