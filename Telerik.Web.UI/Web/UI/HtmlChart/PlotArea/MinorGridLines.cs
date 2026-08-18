using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B98 RID: 2968
	public class MinorGridLines : GridLinesBase
	{
		// Token: 0x06007025 RID: 28709 RVA: 0x001A2F7C File Offset: 0x001A117C
		public MinorGridLines(string key, StateBag OwnerStateBag) : base("minor" + key, OwnerStateBag)
		{
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x001A2F90 File Offset: 0x001A1190
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("minorGridLines: {").Append(base.Serialize()).Append("}");
			return stringBuilder.ToString();
		}
	}
}
