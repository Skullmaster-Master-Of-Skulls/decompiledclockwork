using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B84 RID: 2948
	public class LegendAppearance : LegendLabelsAppearance
	{
		// Token: 0x06006F63 RID: 28515 RVA: 0x001A0540 File Offset: 0x0019E740
		public LegendAppearance(StateBag OwnerStateBag) : base("la", OwnerStateBag)
		{
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06006F64 RID: 28516 RVA: 0x001A054E File Offset: 0x0019E74E
		// (set) Token: 0x06006F65 RID: 28517 RVA: 0x001A056F File Offset: 0x0019E76F
		[DefaultValue(ChartLegendPosition.Right)]
		public ChartLegendPosition Position
		{
			get
			{
				return (ChartLegendPosition)(base.ViewState["Position"] ?? ChartLegendPosition.Right);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x06006F66 RID: 28518 RVA: 0x001A0587 File Offset: 0x0019E787
		// (set) Token: 0x06006F67 RID: 28519 RVA: 0x001A05A8 File Offset: 0x0019E7A8
		[DefaultValue(ChartLegendAlign.Center)]
		public ChartLegendAlign Align
		{
			get
			{
				return (ChartLegendAlign)(base.ViewState["Align"] ?? ChartLegendAlign.Center);
			}
			set
			{
				base.ViewState["Align"] = value;
			}
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x001A05C0 File Offset: 0x0019E7C0
		// (set) Token: 0x06006F69 RID: 28521 RVA: 0x001A05E1 File Offset: 0x0019E7E1
		[DefaultValue(ChartLegendOrientation.Auto)]
		public ChartLegendOrientation Orientation
		{
			get
			{
				return (ChartLegendOrientation)(base.ViewState["Orientation"] ?? ChartLegendOrientation.Auto);
			}
			set
			{
				base.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x06006F6A RID: 28522 RVA: 0x001A05F9 File Offset: 0x0019E7F9
		// (set) Token: 0x06006F6B RID: 28523 RVA: 0x001A061A File Offset: 0x0019E81A
		[TypeConverter(typeof(UnitConverter))]
		[Description("Get/Set the width of the legend.")]
		[DefaultValue(0)]
		public int Width
		{
			get
			{
				return (int)(base.ViewState["Width"] ?? 0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06006F6C RID: 28524 RVA: 0x001A0632 File Offset: 0x0019E832
		// (set) Token: 0x06006F6D RID: 28525 RVA: 0x001A0653 File Offset: 0x0019E853
		[DefaultValue(0)]
		[Description("Get/Set the width of the legend.")]
		[TypeConverter(typeof(UnitConverter))]
		public int Height
		{
			get
			{
				return (int)(base.ViewState["Height"] ?? 0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06006F6E RID: 28526 RVA: 0x001A066B File Offset: 0x0019E86B
		// (set) Token: 0x06006F6F RID: 28527 RVA: 0x001A068C File Offset: 0x0019E88C
		[DefaultValue(0)]
		[Description("Get/Set the width of the legend.")]
		[TypeConverter(typeof(UnitConverter))]
		public int OffsetX
		{
			get
			{
				return (int)(base.ViewState["OffsetX"] ?? 0);
			}
			set
			{
				base.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06006F70 RID: 28528 RVA: 0x001A06A4 File Offset: 0x0019E8A4
		// (set) Token: 0x06006F71 RID: 28529 RVA: 0x001A06C5 File Offset: 0x0019E8C5
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(0)]
		[Description("Get/Set the width of the legend.")]
		public int OffsetY
		{
			get
			{
				return (int)(base.ViewState["OffsetY"] ?? 0);
			}
			set
			{
				base.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x001A06E0 File Offset: 0x0019E8E0
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			if (this.Position != ChartLegendPosition.Right)
			{
				stringBuilder.Append(", position: '").Append(this.Position.ToString().ToLower()).Append("'");
			}
			if (this.Align != ChartLegendAlign.Center)
			{
				stringBuilder.Append(", align: '").Append(this.Align.ToString().ToLower()).Append("'");
			}
			if (this.Orientation != ChartLegendOrientation.Auto)
			{
				stringBuilder.Append(", orientation: '").Append(this.Orientation.ToString().ToLower()).Append("'");
			}
			if (this.Width != 0)
			{
				stringBuilder.Append(string.Format(", width: {0}", this.Width));
			}
			if (this.Height != 0)
			{
				stringBuilder.Append(string.Format(", height: {0}", this.Height));
			}
			if (this.OffsetX != 0)
			{
				stringBuilder.Append(string.Format(", offsetX: {0}", this.OffsetX));
			}
			if (this.OffsetY != 0)
			{
				stringBuilder.Append(string.Format(", offsetY: {0}", this.OffsetY));
			}
			return stringBuilder.ToString();
		}
	}
}
