using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004D1 RID: 1233
	public class ChartYAxis : AxisBase
	{
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000937E0 File Offset: 0x000919E0
		// (set) Token: 0x06002CDA RID: 11482 RVA: 0x00093801 File Offset: 0x00091A01
		[DefaultValue(HtmlChartValueAxisType.Numeric)]
		public HtmlChartValueAxisType Type
		{
			get
			{
				return (HtmlChartValueAxisType)(base.ViewState["Type"] ?? HtmlChartValueAxisType.Numeric);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x00093819 File Offset: 0x00091A19
		// (set) Token: 0x06002CDC RID: 11484 RVA: 0x00093830 File Offset: 0x00091A30
		[DefaultValue(null)]
		public virtual bool? NarrowRange
		{
			get
			{
				return (bool?)base.ViewState["NarrowRange"];
			}
			set
			{
				base.ViewState["NarrowRange"] = value;
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00093848 File Offset: 0x00091A48
		internal override string Serialize()
		{
			if (base.PlotType == PlotType.Pie || base.PlotType == PlotType.Funnel)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(base.Serialize());
			stringBuilder.Append(base.SerializeAxisScaling());
			this.SerializeAxisType(stringBuilder);
			if (this.NarrowRange != null)
			{
				stringBuilder.Append(",narrowRange:" + this.NarrowRange.ToString().ToLower());
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000938EC File Offset: 0x00091AEC
		protected override void SerializeAxisType(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			if (this.Type != HtmlChartValueAxisType.Numeric)
			{
				sb.AppendFormat(",type:'{0}'", this.Type.ToString().ToLower());
			}
		}
	}
}
