using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B97 RID: 2967
	public class GridLinesBase : ObjectWithState
	{
		// Token: 0x0600701B RID: 28699 RVA: 0x001A2DA2 File Offset: 0x001A0FA2
		public GridLinesBase(string key, StateBag OwnerStateBag) : base("gl" + key, OwnerStateBag)
		{
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x0600701C RID: 28700 RVA: 0x001A2DB6 File Offset: 0x001A0FB6
		// (set) Token: 0x0600701D RID: 28701 RVA: 0x001A2DCD File Offset: 0x001A0FCD
		[DefaultValue(null)]
		public bool? Visible
		{
			get
			{
				return (bool?)base.ViewState["Visible"];
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x0600701E RID: 28702 RVA: 0x001A2DE5 File Offset: 0x001A0FE5
		// (set) Token: 0x0600701F RID: 28703 RVA: 0x001A2E0A File Offset: 0x001A100A
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x06007020 RID: 28704 RVA: 0x001A2E22 File Offset: 0x001A1022
		// (set) Token: 0x06007021 RID: 28705 RVA: 0x001A2E48 File Offset: 0x001A1048
		[DefaultValue(typeof(Unit), "1")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? new Unit(1));
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x06007022 RID: 28706 RVA: 0x001A2E60 File Offset: 0x001A1060
		// (set) Token: 0x06007023 RID: 28707 RVA: 0x001A2E6E File Offset: 0x001A106E
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

		// Token: 0x06007024 RID: 28708 RVA: 0x001A2E88 File Offset: 0x001A1088
		internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("visible: {0}", (this.Visible == true) ? "true" : "false");
			if (this.Color != Color.Empty)
			{
				stringBuilder.Append(", color: '").Append(HtmlChartHelper.ColorToHex(this.Color)).Append("'");
			}
			if (this.Width.Value != 1.0)
			{
				stringBuilder.Append(", width: ").Append(this.Width.Value);
			}
			if (this.DashType != DashType.Solid)
			{
				string arg = HtmlChartHelper.StringToLowerCamelCase(this.DashType.ToString());
				stringBuilder.Append(", dashType: ").AppendFormat("'{0}'", arg);
			}
			return stringBuilder.ToString();
		}
	}
}
