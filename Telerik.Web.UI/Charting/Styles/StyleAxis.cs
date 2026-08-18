using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017C8 RID: 6088
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleAxis : LineStyle, IDisposable
	{
		// Token: 0x0600ECDA RID: 60634 RVA: 0x00360D80 File Offset: 0x0035EF80
		internal override void Reset()
		{
			base.Reset();
			this.ValueFormat = ChartValueFormat.None;
			this.CustomFormat = string.Empty;
			this.Color = DefaultValues.DEFAULT_AXIS_COLOR;
			this.Width = 1f;
			this.Visible = ChartAxisVisibility.Auto;
			this.styleAxisOrientation = Orientation.Undefined;
			this.styleAxisLabelAppearance = new StyleLabel();
			this.styleAxisTextAppearance = new StyleAxisItemText();
			this.styleAxisMajorTick = new StyleTickMajor();
			this.styleAxisMinorTick = new StyleTickMinor();
			this.styleAxisMinorGridLines = new StyleGridLineHidden();
			this.styleAxisMajorGridLines = new StyleGridLine();
		}

		// Token: 0x170047A9 RID: 18345
		// (get) Token: 0x0600ECDB RID: 60635 RVA: 0x00360E0B File Offset: 0x0035F00B
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StyleGridLine MajorGridLines
		{
			get
			{
				return this.styleAxisMajorGridLines;
			}
		}

		// Token: 0x170047AA RID: 18346
		// (get) Token: 0x0600ECDC RID: 60636 RVA: 0x00360E13 File Offset: 0x0035F013
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual StyleGridLine MinorGridLines
		{
			get
			{
				return this.styleAxisMinorGridLines;
			}
		}

		// Token: 0x170047AB RID: 18347
		// (get) Token: 0x0600ECDD RID: 60637 RVA: 0x00360E1B File Offset: 0x0035F01B
		// (set) Token: 0x0600ECDE RID: 60638 RVA: 0x00360E23 File Offset: 0x0035F023
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Orientation), "Undefined")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal virtual Orientation Orientation
		{
			get
			{
				return this.styleAxisOrientation;
			}
			set
			{
				this.styleAxisOrientation = value;
			}
		}

		// Token: 0x170047AC RID: 18348
		// (get) Token: 0x0600ECDF RID: 60639 RVA: 0x00360E2C File Offset: 0x0035F02C
		// (set) Token: 0x0600ECE0 RID: 60640 RVA: 0x00360E51 File Offset: 0x0035F051
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "Black")]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_AXIS_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x170047AD RID: 18349
		// (get) Token: 0x0600ECE1 RID: 60641 RVA: 0x00360E5A File Offset: 0x0035F05A
		// (set) Token: 0x0600ECE2 RID: 60642 RVA: 0x00360E7B File Offset: 0x0035F07B
		[DefaultValue(typeof(ChartAxisVisibility), "Auto")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public new ChartAxisVisibility Visible
		{
			get
			{
				return (ChartAxisVisibility)(base.ViewState["AxisVisibility"] ?? ChartAxisVisibility.Auto);
			}
			set
			{
				base.ViewState["AxisVisibility"] = value;
			}
		}

		// Token: 0x170047AE RID: 18350
		// (get) Token: 0x0600ECE3 RID: 60643 RVA: 0x00360E93 File Offset: 0x0035F093
		// (set) Token: 0x0600ECE4 RID: 60644 RVA: 0x00360EB4 File Offset: 0x0035F0B4
		[SkinnableProperty]
		[Description("Specifies a predefined numerical format string.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ChartValueFormat), "None")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public ChartValueFormat ValueFormat
		{
			get
			{
				return (ChartValueFormat)(base.ViewState["ValueFormat"] ?? ChartValueFormat.None);
			}
			set
			{
				base.ViewState["ValueFormat"] = value;
			}
		}

		// Token: 0x170047AF RID: 18351
		// (get) Token: 0x0600ECE5 RID: 60645 RVA: 0x00360ECC File Offset: 0x0035F0CC
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public virtual StyleLabel LabelAppearance
		{
			get
			{
				return this.styleAxisLabelAppearance;
			}
		}

		// Token: 0x170047B0 RID: 18352
		// (get) Token: 0x0600ECE6 RID: 60646 RVA: 0x00360ED4 File Offset: 0x0035F0D4
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual StyleTickMinor MinorTick
		{
			get
			{
				return this.styleAxisMinorTick;
			}
		}

		// Token: 0x170047B1 RID: 18353
		// (get) Token: 0x0600ECE7 RID: 60647 RVA: 0x00360EDC File Offset: 0x0035F0DC
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public StyleTickMajor MajorTick
		{
			get
			{
				return this.styleAxisMajorTick;
			}
		}

		// Token: 0x170047B2 RID: 18354
		// (get) Token: 0x0600ECE8 RID: 60648 RVA: 0x00360EE4 File Offset: 0x0035F0E4
		// (set) Token: 0x0600ECE9 RID: 60649 RVA: 0x00360F04 File Offset: 0x0035F104
		[Description("Specifies a custom numerical format string.")]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string CustomFormat
		{
			get
			{
				return (string)(base.ViewState["CustomFormat"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CustomFormat"] = value;
			}
		}

		// Token: 0x170047B3 RID: 18355
		// (get) Token: 0x0600ECEA RID: 60650 RVA: 0x00360F17 File Offset: 0x0035F117
		// (set) Token: 0x0600ECEB RID: 60651 RVA: 0x00360F3C File Offset: 0x0035F13C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue(1f)]
		[Description("Specifies the width of the axis.")]
		[SkinnableProperty]
		public override float Width
		{
			get
			{
				return (float)(base.ViewState["Width"] ?? 1f);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170047B4 RID: 18356
		// (get) Token: 0x0600ECEC RID: 60652 RVA: 0x00360F45 File Offset: 0x0035F145
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public StyleAxisItemText TextAppearance
		{
			get
			{
				return this.styleAxisTextAppearance;
			}
		}

		// Token: 0x170047B5 RID: 18357
		internal override object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.TextAppearance:
					return this.styleAxisTextAppearance;
				case StyleProperties.CustomFormat:
					return this.CustomFormat;
				case StyleProperties.MajorTick:
					return this.styleAxisMajorTick;
				case StyleProperties.MinorTick:
					return this.styleAxisMinorTick;
				case StyleProperties.LabelAppearance:
					return this.styleAxisLabelAppearance;
				case StyleProperties.ValueFormat:
					return this.ValueFormat;
				case StyleProperties.Orientation:
					return this.styleAxisOrientation;
				case StyleProperties.MinorGridLines:
					return this.styleAxisMinorGridLines;
				case StyleProperties.MajorGridLines:
					return this.styleAxisMajorGridLines;
				}
				return base[name];
			}
		}

		// Token: 0x0600ECEE RID: 60654 RVA: 0x00360FE4 File Offset: 0x0035F1E4
		public StyleAxis(ChartAxis axis) : this()
		{
			this.styleAxisLabelAppearance.styleChart = axis.Chart;
			Style style = this.styleAxisTextAppearance;
			this.styleAxisTextAppearance.TextProperties.textPropertiesContainerObject = axis;
			style.styleContainerObject = axis;
		}

		// Token: 0x0600ECEF RID: 60655 RVA: 0x00361028 File Offset: 0x0035F228
		public StyleAxis()
		{
			this.styleAxisLabelAppearance = new StyleLabel();
			this.styleAxisTextAppearance = new StyleAxisItemText();
			this.styleAxisMajorTick = new StyleTickMajor();
			this.styleAxisMinorTick = new StyleTickMinor();
			this.styleAxisMinorGridLines = new StyleGridLineHidden();
			this.styleAxisMajorGridLines = new StyleGridLine();
		}

		// Token: 0x0600ECF0 RID: 60656 RVA: 0x0036107D File Offset: 0x0035F27D
		public StyleAxis(Orientation orientation) : this()
		{
			this.styleAxisOrientation = orientation;
		}

		// Token: 0x0600ECF1 RID: 60657 RVA: 0x0036108C File Offset: 0x0035F28C
		public StyleAxis(Orientation orientation, ChartAxis axis) : this(axis)
		{
			this.styleAxisOrientation = orientation;
		}

		// Token: 0x0600ECF2 RID: 60658 RVA: 0x0036109C File Offset: 0x0035F29C
		public StyleAxis(Orientation orientation, ChartAxisVisibility visibility) : this(orientation)
		{
			this.Visible = visibility;
		}

		// Token: 0x0600ECF3 RID: 60659 RVA: 0x003610AC File Offset: 0x0035F2AC
		public StyleAxis(Orientation orientation, ChartAxisVisibility visibility, ChartAxis axis) : this(orientation, axis)
		{
			this.Visible = visibility;
		}

		// Token: 0x0600ECF4 RID: 60660 RVA: 0x003610BD File Offset: 0x0035F2BD
		public StyleAxis(Orientation orientation, ChartAxisVisibility visibility, LineStyle lineStyle) : base(lineStyle.Color, lineStyle.Width, lineStyle.PenStyle)
		{
			this.styleAxisOrientation = orientation;
			this.Visible = visibility;
		}

		// Token: 0x0600ECF5 RID: 60661 RVA: 0x003610E8 File Offset: 0x0035F2E8
		protected override void Dispose(bool disposing)
		{
			if (this.styleAxisLabelAppearance != null)
			{
				this.styleAxisLabelAppearance.Dispose();
				this.styleAxisLabelAppearance = null;
			}
			if (this.styleAxisMajorGridLines != null)
			{
				this.styleAxisMajorGridLines.Dispose();
				this.styleAxisMajorGridLines = null;
			}
			if (this.styleAxisMajorTick != null)
			{
				this.styleAxisMajorTick.Dispose();
				this.styleAxisMajorTick = null;
			}
			if (this.styleAxisMinorGridLines != null)
			{
				this.styleAxisMinorGridLines.Dispose();
				this.styleAxisMinorGridLines = null;
			}
			if (this.styleAxisMinorTick != null)
			{
				this.styleAxisMinorTick.Dispose();
				this.styleAxisMinorTick = null;
			}
			if (this.styleAxisTextAppearance != null)
			{
				this.styleAxisTextAppearance.Dispose();
				this.styleAxisTextAppearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600ECF6 RID: 60662 RVA: 0x00361198 File Offset: 0x0035F398
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleAxisLabelAppearance).TrackViewState();
			((IChartingStateManager)this.styleAxisMajorGridLines).TrackViewState();
			((IChartingStateManager)this.styleAxisMajorTick).TrackViewState();
			((IChartingStateManager)this.styleAxisMinorGridLines).TrackViewState();
			((IChartingStateManager)this.styleAxisMinorTick).TrackViewState();
			((IChartingStateManager)this.styleAxisTextAppearance).TrackViewState();
		}

		// Token: 0x0600ECF7 RID: 60663 RVA: 0x003611F0 File Offset: 0x0035F3F0
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleAxisLabelAppearance).LoadViewState(array[1]);
				((IChartingStateManager)this.styleAxisMajorGridLines).LoadViewState(array[2]);
				((IChartingStateManager)this.styleAxisMajorTick).LoadViewState(array[3]);
				((IChartingStateManager)this.styleAxisMinorGridLines).LoadViewState(array[4]);
				((IChartingStateManager)this.styleAxisMinorTick).LoadViewState(array[5]);
				((IChartingStateManager)this.styleAxisTextAppearance).LoadViewState(array[6]);
			}
		}

		// Token: 0x0600ECF8 RID: 60664 RVA: 0x00361264 File Offset: 0x0035F464
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleAxisLabelAppearance).SaveViewState(),
				((IChartingStateManager)this.styleAxisMajorGridLines).SaveViewState(),
				((IChartingStateManager)this.styleAxisMajorTick).SaveViewState(),
				((IChartingStateManager)this.styleAxisMinorGridLines).SaveViewState(),
				((IChartingStateManager)this.styleAxisMinorTick).SaveViewState(),
				((IChartingStateManager)this.styleAxisTextAppearance).SaveViewState()
			}.ToArray();
		}

		// Token: 0x04004491 RID: 17553
		protected Orientation styleAxisOrientation;

		// Token: 0x04004492 RID: 17554
		protected StyleLabel styleAxisLabelAppearance;

		// Token: 0x04004493 RID: 17555
		protected StyleAxisItemText styleAxisTextAppearance;

		// Token: 0x04004494 RID: 17556
		protected StyleTickMinor styleAxisMinorTick;

		// Token: 0x04004495 RID: 17557
		protected StyleTickMajor styleAxisMajorTick;

		// Token: 0x04004496 RID: 17558
		protected StyleGridLine styleAxisMajorGridLines;

		// Token: 0x04004497 RID: 17559
		protected StyleGridLine styleAxisMinorGridLines;
	}
}
