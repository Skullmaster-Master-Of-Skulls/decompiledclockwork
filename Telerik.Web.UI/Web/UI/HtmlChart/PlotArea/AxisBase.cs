using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004CF RID: 1231
	public class AxisBase : StateManager
	{
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x00092697 File Offset: 0x00090897
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x000926B8 File Offset: 0x000908B8
		[DefaultValue(PlotType.Categorial)]
		internal PlotType PlotType
		{
			get
			{
				return (PlotType)(base.ViewState["PlotType"] ?? PlotType.Categorial);
			}
			set
			{
				base.ViewState["PlotType"] = value;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000926D0 File Offset: 0x000908D0
		// (set) Token: 0x06002C96 RID: 11414 RVA: 0x000926F0 File Offset: 0x000908F0
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return ((string)base.ViewState["Name"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x00092703 File Offset: 0x00090903
		// (set) Token: 0x06002C98 RID: 11416 RVA: 0x0009271A File Offset: 0x0009091A
		[DefaultValue(null)]
		public decimal? MinValue
		{
			get
			{
				return (decimal?)base.ViewState["MinValue"];
			}
			set
			{
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00092732 File Offset: 0x00090932
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x00092749 File Offset: 0x00090949
		[DefaultValue(null)]
		public decimal? MaxValue
		{
			get
			{
				return (decimal?)base.ViewState["MaxValue"];
			}
			set
			{
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x00092761 File Offset: 0x00090961
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x00092778 File Offset: 0x00090978
		[DefaultValue(null)]
		public decimal? Step
		{
			get
			{
				return (decimal?)base.ViewState["Step"];
			}
			set
			{
				base.ViewState["Step"] = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x00092790 File Offset: 0x00090990
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x000927A7 File Offset: 0x000909A7
		[DefaultValue(null)]
		public decimal? AxisCrossingValue
		{
			get
			{
				return (decimal?)base.ViewState["AxisCrossingValue"];
			}
			set
			{
				base.ViewState["AxisCrossingValue"] = value;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000927BF File Offset: 0x000909BF
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x000927E4 File Offset: 0x000909E4
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

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000927FC File Offset: 0x000909FC
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x00092822 File Offset: 0x00090A22
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

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x0009283A File Offset: 0x00090A3A
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x00092848 File Offset: 0x00090A48
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

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x00092860 File Offset: 0x00090A60
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x00092877 File Offset: 0x00090A77
		[DefaultValue(false)]
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

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x0009288F File Offset: 0x00090A8F
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000928B0 File Offset: 0x00090AB0
		[DefaultValue(false)]
		public bool Reversed
		{
			get
			{
				return (bool)(base.ViewState["Reversed"] ?? false);
			}
			set
			{
				base.ViewState["Reversed"] = value;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000928C8 File Offset: 0x00090AC8
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000928EE File Offset: 0x00090AEE
		[DefaultValue(typeof(Unit), "3")]
		public Unit MinorTickSize
		{
			get
			{
				return (Unit)(base.ViewState["MinorTickSize"] ?? new Unit(3));
			}
			set
			{
				base.ViewState["MinorTickSize"] = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x00092906 File Offset: 0x00090B06
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x00092927 File Offset: 0x00090B27
		[DefaultValue(typeof(TickType), "1")]
		public TickType MinorTickType
		{
			get
			{
				return (TickType)(base.ViewState["MinorTickType"] ?? TickType.None);
			}
			set
			{
				base.ViewState["MinorTickType"] = value;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x0009293F File Offset: 0x00090B3F
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x00092965 File Offset: 0x00090B65
		[DefaultValue(typeof(Unit), "4")]
		public Unit MajorTickSize
		{
			get
			{
				return (Unit)(base.ViewState["MajorTickSize"] ?? new Unit(4));
			}
			set
			{
				base.ViewState["MajorTickSize"] = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x0009297D File Offset: 0x00090B7D
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x0009299E File Offset: 0x00090B9E
		[DefaultValue(typeof(TickType), "0")]
		public TickType MajorTickType
		{
			get
			{
				return (TickType)(base.ViewState["MajorTickType"] ?? TickType.Outside);
			}
			set
			{
				base.ViewState["MajorTickType"] = value;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000929B6 File Offset: 0x00090BB6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Axis title visual settings")]
		public AxisTitleAppearance TitleAppearance
		{
			get
			{
				if (this._titleAppearance == null)
				{
					this._titleAppearance = new AxisTitleAppearance(this._prefix, base.ViewState);
				}
				return this._titleAppearance;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000929DD File Offset: 0x00090BDD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Axis labels visual settings")]
		[Category("Appearance")]
		public AxisLabelsAppearance LabelsAppearance
		{
			get
			{
				if (this._labelsAppearance == null)
				{
					this._labelsAppearance = new AxisLabelsAppearance(this._prefix, base.ViewState);
				}
				return this._labelsAppearance;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x00092A04 File Offset: 0x00090C04
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Minor grid lines settings")]
		public MinorGridLines MinorGridLines
		{
			get
			{
				if (this._minorGridLines == null)
				{
					this._minorGridLines = new MinorGridLines(this._prefix, base.ViewState);
				}
				return this._minorGridLines;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x00092A2B File Offset: 0x00090C2B
		[Category("Appearance")]
		[Description("Major grid lines settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MajorGridLines MajorGridLines
		{
			get
			{
				if (this._majorGridLines == null)
				{
					this._majorGridLines = new MajorGridLines(this._prefix, base.ViewState);
				}
				return this._majorGridLines;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x00092A52 File Offset: 0x00090C52
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public PlotBandsCollection PlotBands
		{
			get
			{
				if (this._plotBands == null)
				{
					this._plotBands = new PlotBandsCollection();
				}
				return this._plotBands;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x00092A6D File Offset: 0x00090C6D
		[Category("Appearance")]
		[Description("Axis crosshair visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AxisCrosshairAppearance CrosshairAppearance
		{
			get
			{
				if (this._crosshairAppearance == null)
				{
					this._crosshairAppearance = new AxisCrosshairAppearance();
				}
				return this._crosshairAppearance;
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x00092A88 File Offset: 0x00090C88
		internal virtual string Serialize()
		{
			if (this.PlotType == PlotType.Pie || this.PlotType == PlotType.Funnel)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Visible == true)
			{
				stringBuilder.Append("visible: true,");
			}
			else
			{
				stringBuilder.Append("visible: false,");
			}
			if (this.Color != Color.Empty)
			{
				stringBuilder.Append("color: '").Append(HtmlChartHelper.ColorToHex(this.Color)).Append("',");
			}
			string value = string.Empty;
			if (this.DashType != DashType.Solid)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				string arg = HtmlChartHelper.StringToLowerCamelCase(this.DashType.ToString());
				stringBuilder2.Append(",dashType: ").AppendFormat("'{0}'", arg);
				value = stringBuilder2.ToString();
			}
			stringBuilder.Append(" line: { width: ").Append(this.Width.Value).Append(value).Append("}");
			if (!string.IsNullOrEmpty(this.Name))
			{
				stringBuilder.AppendFormat(",name:'{0}'", this.Name);
			}
			if (this.Reversed)
			{
				stringBuilder.Append(",").Append("reverse: ").Append(this.Reversed.ToString().ToLower());
			}
			if (this.MinorTickSize.Value != 3.0)
			{
				stringBuilder.Append(",").Append("minorTickSize: ").Append(this.MinorTickSize.Value.ToString().ToLower());
			}
			if (this.MinorTickType != TickType.None)
			{
				stringBuilder.Append(", minorTickType: '").Append(this.MinorTickType.ToString().ToLower()).Append("'");
			}
			if (this.MajorTickSize.Value != 4.0)
			{
				stringBuilder.Append(",").Append("majorTickSize: ").Append(this.MajorTickSize.Value.ToString().ToLower());
			}
			if (this.MajorTickType != TickType.Outside)
			{
				stringBuilder.Append(", majorTickType: '").Append(this.MajorTickType.ToString().ToLower()).Append("'");
			}
			stringBuilder.Append(",").Append(this.TitleAppearance.Serialize());
			stringBuilder.Append(",").Append(this.LabelsAppearance.Serialize());
			stringBuilder.Append(",").Append(this.MinorGridLines.Serialize());
			stringBuilder.Append(",").Append(this.MajorGridLines.Serialize());
			if (!this.CrosshairAppearance.IsDefault)
			{
				stringBuilder.Append(",").Append(this.CrosshairAppearance.Serialize());
			}
			if (this.AxisCrossingValue != null && this.PlotType != PlotType.Radar && this.PlotType != PlotType.Polar)
			{
				stringBuilder.Append(", axisCrossingValue: ").Append(HtmlChartHelper.ToStringInvariant(this.AxisCrossingValue));
			}
			HtmlChartHelper.AddComma(stringBuilder);
			if (this.PlotBands.Count > 0)
			{
				this.SerializePlotBands(stringBuilder);
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00092E08 File Offset: 0x00091008
		protected virtual string SerializeAxisScaling()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.MinValue != null)
			{
				stringBuilder.Append(", min: ").Append(HtmlChartHelper.ToStringInvariant(this.MinValue));
			}
			if (this.MaxValue != null)
			{
				stringBuilder.Append(", max: ").Append(HtmlChartHelper.ToStringInvariant(this.MaxValue));
			}
			if (this.Step != null)
			{
				stringBuilder.Append(", majorUnit: ").Append(HtmlChartHelper.ToStringInvariant(this.Step));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00092EA8 File Offset: 0x000910A8
		private void SerializePlotBands(StringBuilder sb)
		{
			sb.Append("plotBands:[");
			foreach (object obj in this.PlotBands)
			{
				PlotBand plotBand = (PlotBand)obj;
				sb.Append(plotBand.Serialize());
				HtmlChartHelper.AddComma(sb);
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Append("]");
			HtmlChartHelper.AddComma(sb);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00092F34 File Offset: 0x00091134
		protected virtual void SerializeAxisType(StringBuilder sb)
		{
		}

		// Token: 0x04000B80 RID: 2944
		private readonly string _prefix = "ab";

		// Token: 0x04000B81 RID: 2945
		internal bool IsDataBound;

		// Token: 0x04000B82 RID: 2946
		private AxisTitleAppearance _titleAppearance;

		// Token: 0x04000B83 RID: 2947
		private AxisLabelsAppearance _labelsAppearance;

		// Token: 0x04000B84 RID: 2948
		private MinorGridLines _minorGridLines;

		// Token: 0x04000B85 RID: 2949
		private MajorGridLines _majorGridLines;

		// Token: 0x04000B86 RID: 2950
		private PlotBandsCollection _plotBands;

		// Token: 0x04000B87 RID: 2951
		private AxisCrosshairAppearance _crosshairAppearance;
	}
}
