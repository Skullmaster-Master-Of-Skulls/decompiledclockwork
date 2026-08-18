using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B99 RID: 2969
	public class MajorGridLines : GridLinesBase
	{
		// Token: 0x06007027 RID: 28711 RVA: 0x001A2FC3 File Offset: 0x001A11C3
		public MajorGridLines(string key, StateBag OwnerStateBag) : base("major" + key, OwnerStateBag)
		{
		}

		// Token: 0x06007028 RID: 28712 RVA: 0x001A2FD8 File Offset: 0x001A11D8
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("majorGridLines: {").Append(base.Serialize()).Append("}");
			return stringBuilder.ToString();
		}
	}
}
