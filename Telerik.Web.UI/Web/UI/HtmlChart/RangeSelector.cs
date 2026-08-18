using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000517 RID: 1303
	public class RangeSelector : ObjectWithState
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x00098861 File Offset: 0x00096A61
		public RangeSelector(StateBag OwnerStateBag) : base("crs", OwnerStateBag)
		{
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x0009886F File Offset: 0x00096A6F
		// (set) Token: 0x06002EA1 RID: 11937 RVA: 0x00098886 File Offset: 0x00096A86
		public DateTime? From
		{
			get
			{
				return (DateTime?)base.ViewState["From"];
			}
			set
			{
				base.ViewState["From"] = value;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x0009889E File Offset: 0x00096A9E
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000988B5 File Offset: 0x00096AB5
		public DateTime? To
		{
			get
			{
				return (DateTime?)base.ViewState["To"];
			}
			set
			{
				base.ViewState["To"] = value;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000988D0 File Offset: 0x00096AD0
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select:{");
			if (this.From != null)
			{
				stringBuilder.AppendFormat("from: {0},", HtmlChartHelper.GetSerializedValueField(HtmlChartHelper.ToStringInvariant(this.From), true));
			}
			if (this.To != null)
			{
				stringBuilder.AppendFormat("to: {0},", HtmlChartHelper.GetSerializedValueField(HtmlChartHelper.ToStringInvariant(this.To), true));
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
