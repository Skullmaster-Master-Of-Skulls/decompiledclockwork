using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E2 RID: 994
	public class LineAppearance : ObjectWithState
	{
		// Token: 0x0600245E RID: 9310 RVA: 0x00078D1A File Offset: 0x00076F1A
		public LineAppearance(string prefix, StateBag OwnerStateBag) : base("lineAppearance" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00078D2E File Offset: 0x00076F2E
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00078D53 File Offset: 0x00076F53
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Get/Set the line width of the series.")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00078D6B File Offset: 0x00076F6B
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x00078D8C File Offset: 0x00076F8C
		[DefaultValue(LineStyle.Normal)]
		public virtual LineStyle LineStyle
		{
			get
			{
				return (LineStyle)(base.ViewState["LineStyle"] ?? LineStyle.Normal);
			}
			set
			{
				base.ViewState["LineStyle"] = value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x00078DA4 File Offset: 0x00076FA4
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x00078DB2 File Offset: 0x00076FB2
		[DefaultValue(DashType.Solid)]
		public DashType DashType
		{
			get
			{
				return base.GetViewStateValue<DashType>("DashType", DashType.Solid);
			}
			set
			{
				base.ViewState["DashType"] = value;
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00078DCC File Offset: 0x00076FCC
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.SerializeWidth(stringBuilder);
			this.SerializeDashType(stringBuilder);
			this.SerializeLineStyle(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00078E04 File Offset: 0x00077004
		private void SerializeWidth(StringBuilder sb)
		{
			if (this.Width != Unit.Empty)
			{
				sb.AppendFormat("width:{0},", this.Width.Value);
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00078E44 File Offset: 0x00077044
		private void SerializeDashType(StringBuilder sb)
		{
			if (this.DashType != DashType.Solid)
			{
				string arg = HtmlChartHelper.StringToLowerCamelCase(this.DashType.ToString());
				sb.AppendFormat("dashType:'{0}',", arg);
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00078E7D File Offset: 0x0007707D
		internal virtual void SerializeLineStyle(StringBuilder sb)
		{
			if (this.LineStyle != LineStyle.Normal)
			{
				sb.AppendFormat("style:'{0}'", this.LineStyle.ToString().ToLower());
			}
		}
	}
}
