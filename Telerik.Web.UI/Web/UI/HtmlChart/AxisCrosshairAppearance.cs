using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000050 RID: 80
	public class AxisCrosshairAppearance : StateManager, IDefaultCheck
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00006A6B File Offset: 0x00004C6B
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00006A90 File Offset: 0x00004C90
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00006AA8 File Offset: 0x00004CA8
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00006AB6 File Offset: 0x00004CB6
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00006ACE File Offset: 0x00004CCE
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00006AF8 File Offset: 0x00004CF8
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				double num = 0.0;
				if (value > 0.0)
				{
					num = Math.Min(value, 1.0);
				}
				base.ViewState["Opacity"] = num;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00006B41 File Offset: 0x00004D41
		[DefaultValue("TooltipsAppearance")]
		[Description("Tooltips visual settings")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SeriesTooltipsAppearance TooltipsAppearance
		{
			get
			{
				if (this._tooltipsAppearance == null)
				{
					this._tooltipsAppearance = new SeriesTooltipsAppearance("sta", base.ViewState);
					this._tooltipsAppearance.Visible = new bool?(false);
				}
				return this._tooltipsAppearance;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00006B78 File Offset: 0x00004D78
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00006B99 File Offset: 0x00004D99
		[DefaultValue(false)]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00006BB1 File Offset: 0x00004DB1
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00006BD6 File Offset: 0x00004DD6
		[TypeConverter(typeof(UnitConverter))]
		[Description("Get/Set the line width of the crosshair.")]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public bool IsDefault
		{
			get
			{
				return this.Color == Color.Empty && this.DashType == DashType.Solid && this.Width == Unit.Empty && this.Opacity == 1.0 && this.TooltipsAppearance.Visible != true && !this.Visible;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006C6C File Offset: 0x00004E6C
		internal virtual string Serialize()
		{
			if (this.IsDefault)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Color != Color.Empty)
			{
				stringBuilder.AppendFormat("color: '{0}',", HtmlChartHelper.ColorToHex(this.Color));
			}
			if (this.DashType != DashType.Solid)
			{
				stringBuilder.AppendFormat("dashType: '{0}',", HtmlChartHelper.StringToLowerCamelCase(this.DashType.ToString()));
			}
			if (this.Opacity != 1.0)
			{
				stringBuilder.AppendFormat("opacity: {0},", this.Opacity);
			}
			if (this.Opacity != 1.0)
			{
				stringBuilder.AppendFormat("opacity: {0},", this.Opacity);
			}
			if (this.Visible)
			{
				stringBuilder.Append("visible: true,");
			}
			if (this.Width != Unit.Empty)
			{
				stringBuilder.AppendFormat("width: {0},", this.Width.Value);
			}
			if (this.TooltipsAppearance.Visible == true)
			{
				stringBuilder.Append(this.TooltipsAppearance.Serialize());
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return string.Format("crosshair: {{{0}}}", stringBuilder.ToString());
		}

		// Token: 0x04000053 RID: 83
		private SeriesTooltipsAppearance _tooltipsAppearance;
	}
}
