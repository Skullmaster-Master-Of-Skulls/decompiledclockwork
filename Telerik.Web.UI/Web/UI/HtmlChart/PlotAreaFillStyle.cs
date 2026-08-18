using System;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B87 RID: 2951
	public class PlotAreaFillStyle : FillStyleBase
	{
		// Token: 0x06006F7A RID: 28538 RVA: 0x001A096A File Offset: 0x0019EB6A
		public PlotAreaFillStyle(string prefix, StateBag OwnerStateBag) : base("pafs" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x06006F7B RID: 28539 RVA: 0x001A0980 File Offset: 0x0019EB80
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.BackgroundColor != Color.Empty)
			{
				stringBuilder.AppendFormat("background:{0},", HtmlChartHelper.SerializeColor(base.BackgroundColor));
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
