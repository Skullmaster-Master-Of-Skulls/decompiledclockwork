using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x020004F8 RID: 1272
	public class Outlier : StateManager
	{
		// Token: 0x06002D5C RID: 11612 RVA: 0x00094E18 File Offset: 0x00093018
		public Outlier()
		{
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x00094E20 File Offset: 0x00093020
		public Outlier(decimal? value)
		{
			this.Value = value;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x00094E2F File Offset: 0x0009302F
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x00094E46 File Offset: 0x00093046
		[DefaultValue(null)]
		public decimal? Value
		{
			get
			{
				return (decimal?)base.ViewState["Value"];
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00094E60 File Offset: 0x00093060
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0},", HtmlChartHelper.ToStringInvariant(this.Value));
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
