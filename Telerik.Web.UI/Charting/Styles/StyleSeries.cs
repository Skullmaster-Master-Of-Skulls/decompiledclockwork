using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E6 RID: 6118
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleSeries : Style
	{
		// Token: 0x0600EE03 RID: 60931 RVA: 0x00363BB8 File Offset: 0x00361DB8
		internal override void Reset()
		{
			base.Reset();
			this.styleSeriesPointDimentions = new DimensionsPointMarker();
			this.styleSeriesCorners = new Corners();
			this.styleSeriesFillStyle = new FillStyleSeries();
			this.styleSeriesLabelAppearance = new StyleSeriesItemLabel();
			this.styleSeriesTextAppearance = new StyleSeriesItemTextBlock();
			this.styleSeriesLineSeriesAppearance = new StyleLineSeries();
			this.styleSeriesLineSeriesAppearance.lineStyleContainerObject = this.styleSeriesParent;
			this.styleSeriesEmptyValue = new EmptyValue();
			this.styleBorder = new StyleSeriesBorder();
			this.styleSeriesPointAppearance = new StyleMarkerSeriesPoint();
			this.LegendDisplayMode = ChartSeriesLegendDisplayMode.SeriesName;
			this.BubbleSize = 20;
			this.CenterXOffset = 0;
			this.CenterYOffset = 0;
			this.StartAngle = 0.0;
			this.DiameterScale = 0.75;
			this.ExplodePercent = 20;
			this.ShowLabels = true;
			this.ShowLabelConnectors = false;
			this.BarWidthPercent = 0m;
		}

		// Token: 0x17004807 RID: 18439
		// (get) Token: 0x0600EE04 RID: 60932 RVA: 0x00363C9C File Offset: 0x00361E9C
		// (set) Token: 0x0600EE05 RID: 60933 RVA: 0x00363CC3 File Offset: 0x00361EC3
		[DefaultValue(typeof(decimal), "75")]
		[Bindable(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[Description("Sets the width percent of the bars.")]
		[NotifyParentProperty(true)]
		public decimal BarWidthPercent
		{
			get
			{
				return (decimal)(base.ViewState["BarWidthPercent"] ?? 75m);
			}
			set
			{
				base.ViewState["BarWidthPercent"] = value;
			}
		}

		// Token: 0x17004808 RID: 18440
		// (get) Token: 0x0600EE06 RID: 60934 RVA: 0x00363CDB File Offset: 0x00361EDB
		// (set) Token: 0x0600EE07 RID: 60935 RVA: 0x00363CE3 File Offset: 0x00361EE3
		[TypeConverter(typeof(CornersConverter))]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public Corners Corners
		{
			get
			{
				return this.styleSeriesCorners;
			}
			set
			{
				this.styleSeriesCorners = value;
			}
		}

		// Token: 0x17004809 RID: 18441
		// (get) Token: 0x0600EE08 RID: 60936 RVA: 0x00363CEC File Offset: 0x00361EEC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FillStyleSeries FillStyle
		{
			get
			{
				return this.styleSeriesFillStyle;
			}
		}

		// Token: 0x1700480A RID: 18442
		// (get) Token: 0x0600EE09 RID: 60937 RVA: 0x00363CF4 File Offset: 0x00361EF4
		// (set) Token: 0x0600EE0A RID: 60938 RVA: 0x00363D14 File Offset: 0x00361F14
		[NotifyParentProperty(true)]
		[DefaultValue("Ellipse")]
		[Browsable(true)]
		[SkinnableProperty]
		[Category("Point")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		public string PointShape
		{
			get
			{
				return (string)(base.ViewState["PointShape"] ?? "Ellipse");
			}
			set
			{
				base.ViewState["PointShape"] = value;
			}
		}

		// Token: 0x1700480B RID: 18443
		// (get) Token: 0x0600EE0B RID: 60939 RVA: 0x00363D27 File Offset: 0x00361F27
		// (set) Token: 0x0600EE0C RID: 60940 RVA: 0x00363D2F File Offset: 0x00361F2F
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Point")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public DimensionsPointMarker PointDimentions
		{
			get
			{
				return this.styleSeriesPointDimentions;
			}
			set
			{
				this.styleSeriesPointDimentions = value;
			}
		}

		// Token: 0x1700480C RID: 18444
		// (get) Token: 0x0600EE0D RID: 60941 RVA: 0x00363D38 File Offset: 0x00361F38
		// (set) Token: 0x0600EE0E RID: 60942 RVA: 0x00363D5D File Offset: 0x00361F5D
		[SkinnableProperty]
		[DefaultValue(0f)]
		[NotifyParentProperty(true)]
		public virtual float PointRotationAngle
		{
			get
			{
				return (float)(base.ViewState["PointRotationAngle"] ?? 0f);
			}
			set
			{
				base.ViewState["PointRotationAngle"] = value;
			}
		}

		// Token: 0x1700480D RID: 18445
		// (get) Token: 0x0600EE0F RID: 60943 RVA: 0x00363D75 File Offset: 0x00361F75
		// (set) Token: 0x0600EE10 RID: 60944 RVA: 0x00363D96 File Offset: 0x00361F96
		[Bindable(true)]
		[Category("Legend")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ChartSeriesLegendDisplayMode), "SeriesName")]
		[SkinnableProperty]
		public ChartSeriesLegendDisplayMode LegendDisplayMode
		{
			get
			{
				return (ChartSeriesLegendDisplayMode)(base.ViewState["LegendDisplayMode"] ?? ChartSeriesLegendDisplayMode.SeriesName);
			}
			set
			{
				base.ViewState["LegendDisplayMode"] = value;
			}
		}

		// Token: 0x1700480E RID: 18446
		// (get) Token: 0x0600EE11 RID: 60945 RVA: 0x00363DAE File Offset: 0x00361FAE
		// (set) Token: 0x0600EE12 RID: 60946 RVA: 0x00363DCF File Offset: 0x00361FCF
		[Description("Indicates whether to show the labels of the data items.")]
		[Category("Labels")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowLabels
		{
			get
			{
				return (bool)(base.ViewState["ShowLabels"] ?? true);
			}
			set
			{
				base.ViewState["ShowLabels"] = value;
			}
		}

		// Token: 0x1700480F RID: 18447
		// (get) Token: 0x0600EE13 RID: 60947 RVA: 0x00363DE7 File Offset: 0x00361FE7
		// (set) Token: 0x0600EE14 RID: 60948 RVA: 0x00363E08 File Offset: 0x00362008
		[SkinnableProperty]
		[Category("Labels")]
		[Description("Specifies whether the line connecting item's top and item's label should be drawn")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public bool ShowLabelConnectors
		{
			get
			{
				return (bool)(base.ViewState["ShowLabelConnectors"] ?? false);
			}
			set
			{
				base.ViewState["ShowLabelConnectors"] = value;
			}
		}

		// Token: 0x17004810 RID: 18448
		// (get) Token: 0x0600EE15 RID: 60949 RVA: 0x00363E20 File Offset: 0x00362020
		// (set) Token: 0x0600EE16 RID: 60950 RVA: 0x00363E49 File Offset: 0x00362049
		[Category("Pie")]
		[SkinnableProperty]
		[Description("Specifies the angle at which a pie chart is started to be drawn.")]
		[DefaultValue(0.0)]
		[NotifyParentProperty(true)]
		public double StartAngle
		{
			get
			{
				return (double)(base.ViewState["StartAngle"] ?? 0.0);
			}
			set
			{
				base.ViewState["StartAngle"] = value;
			}
		}

		// Token: 0x17004811 RID: 18449
		// (get) Token: 0x0600EE17 RID: 60951 RVA: 0x00363E61 File Offset: 0x00362061
		// (set) Token: 0x0600EE18 RID: 60952 RVA: 0x00363E8A File Offset: 0x0036208A
		[SkinnableProperty]
		[Category("Pie")]
		[Description("Specifies the diameter of the pie chart in correspondence to the chart's plot area.")]
		[DefaultValue(0.75)]
		[NotifyParentProperty(true)]
		public double DiameterScale
		{
			get
			{
				return (double)(base.ViewState["DiameterScale"] ?? 0.75);
			}
			set
			{
				base.ViewState["DiameterScale"] = value;
			}
		}

		// Token: 0x17004812 RID: 18450
		// (get) Token: 0x0600EE19 RID: 60953 RVA: 0x00363EA2 File Offset: 0x003620A2
		// (set) Token: 0x0600EE1A RID: 60954 RVA: 0x00363EC4 File Offset: 0x003620C4
		[DefaultValue(20)]
		[SkinnableProperty]
		[Category("Pie")]
		[Description("Specifies the explode percent of a pie slice.")]
		[NotifyParentProperty(true)]
		public int ExplodePercent
		{
			get
			{
				return (int)(base.ViewState["ExplodePercent"] ?? 20);
			}
			set
			{
				base.ViewState["ExplodePercent"] = value;
			}
		}

		// Token: 0x17004813 RID: 18451
		// (get) Token: 0x0600EE1B RID: 60955 RVA: 0x00363EDC File Offset: 0x003620DC
		// (set) Token: 0x0600EE1C RID: 60956 RVA: 0x00363EFD File Offset: 0x003620FD
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Category("Pie")]
		[Description("Sets the X offset of the pie center.")]
		[Browsable(true)]
		[DefaultValue(0)]
		public int CenterXOffset
		{
			get
			{
				return (int)(base.ViewState["CenterXOffset"] ?? 0);
			}
			set
			{
				base.ViewState["CenterXOffset"] = value;
			}
		}

		// Token: 0x17004814 RID: 18452
		// (get) Token: 0x0600EE1D RID: 60957 RVA: 0x00363F15 File Offset: 0x00362115
		// (set) Token: 0x0600EE1E RID: 60958 RVA: 0x00363F36 File Offset: 0x00362136
		[Category("Pie")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Description("Sets the Y offset of the pie center.")]
		[Browsable(true)]
		[DefaultValue(0)]
		public int CenterYOffset
		{
			get
			{
				return (int)(base.ViewState["CenterYOffset"] ?? 0);
			}
			set
			{
				base.ViewState["CenterYOffset"] = value;
			}
		}

		// Token: 0x17004815 RID: 18453
		// (get) Token: 0x0600EE1F RID: 60959 RVA: 0x00363F4E File Offset: 0x0036214E
		// (set) Token: 0x0600EE20 RID: 60960 RVA: 0x00363F70 File Offset: 0x00362170
		[Category("Bubble")]
		[SkinnableProperty]
		[DefaultValue(20)]
		[NotifyParentProperty(true)]
		public int BubbleSize
		{
			get
			{
				return (int)(base.ViewState["BubbleSize"] ?? 20);
			}
			set
			{
				base.ViewState["BubbleSize"] = value;
			}
		}

		// Token: 0x17004816 RID: 18454
		// (get) Token: 0x0600EE21 RID: 60961 RVA: 0x00363F88 File Offset: 0x00362188
		[Description("Specifies common settings for the series items labels")]
		[SkinnableProperty]
		[Category("Labels")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StyleSeriesItemLabel LabelAppearance
		{
			get
			{
				return this.styleSeriesLabelAppearance;
			}
		}

		// Token: 0x17004817 RID: 18455
		// (get) Token: 0x0600EE22 RID: 60962 RVA: 0x00363F90 File Offset: 0x00362190
		[SkinnableProperty]
		[Category("Line series")]
		[Description("Line, Spline, Bezier series line style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public StyleLineSeries LineSeriesAppearance
		{
			get
			{
				this.styleSeriesLineSeriesAppearance.lineStyleContainerObject = this.styleSeriesParent;
				return this.styleSeriesLineSeriesAppearance;
			}
		}

		// Token: 0x17004818 RID: 18456
		// (get) Token: 0x0600EE23 RID: 60963 RVA: 0x00363FA9 File Offset: 0x003621A9
		[NotifyParentProperty(true)]
		[Category("Point marks")]
		[Description("Specifies the shape of the item's point mark")]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public StyleMarkerSeriesPoint PointMark
		{
			get
			{
				return this.styleSeriesPointAppearance;
			}
		}

		// Token: 0x17004819 RID: 18457
		// (get) Token: 0x0600EE24 RID: 60964 RVA: 0x00363FB1 File Offset: 0x003621B1
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Labels")]
		[Description("Specifies common text settings for the series items labels")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleSeriesItemTextBlock TextAppearance
		{
			get
			{
				return this.styleSeriesTextAppearance;
			}
		}

		// Token: 0x1700481A RID: 18458
		// (get) Token: 0x0600EE25 RID: 60965 RVA: 0x00363FB9 File Offset: 0x003621B9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("Specifies the empty item's appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		public EmptyValue EmptyValue
		{
			get
			{
				return this.styleSeriesEmptyValue;
			}
		}

		// Token: 0x1700481B RID: 18459
		// (get) Token: 0x0600EE26 RID: 60966 RVA: 0x00363FC1 File Offset: 0x003621C1
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		public override StyleBorder Border
		{
			get
			{
				return this.styleBorder;
			}
		}

		// Token: 0x1700481C RID: 18460
		// (get) Token: 0x0600EE27 RID: 60967 RVA: 0x00363FC9 File Offset: 0x003621C9
		// (set) Token: 0x0600EE28 RID: 60968 RVA: 0x00363FD1 File Offset: 0x003621D1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600EE29 RID: 60969 RVA: 0x00363FDC File Offset: 0x003621DC
		public StyleSeries(ChartSeries series)
		{
			this.styleSeriesParent = series;
			this.styleSeriesPointDimentions = new DimensionsPointMarker();
			this.styleSeriesCorners = new Corners(series);
			this.styleSeriesFillStyle = new FillStyleSeries(series);
			this.styleSeriesLabelAppearance = new StyleSeriesItemLabel(series);
			this.styleBorder = new StyleSeriesBorder(series);
			this.styleSeriesPointAppearance = new StyleMarkerSeriesPoint(series, "PointMarker");
			this.styleSeriesPointAppearance.styleChart = this.styleChart;
			this.styleSeriesTextAppearance = new StyleSeriesItemTextBlock(series);
			this.styleSeriesLineSeriesAppearance = new StyleLineSeries();
			this.styleSeriesLineSeriesAppearance.lineStyleContainerObject = series;
			this.styleSeriesEmptyValue = new EmptyValue();
			this.styleSeriesEmptyValue.PointMark.styleContainerObject = series;
		}

		// Token: 0x0600EE2A RID: 60970 RVA: 0x00364094 File Offset: 0x00362294
		public StyleSeries()
		{
			this.styleSeriesCorners = new Corners();
			this.styleSeriesPointDimentions = new DimensionsPointMarker();
			this.styleSeriesFillStyle = new FillStyleSeries();
			this.styleSeriesLabelAppearance = new StyleSeriesItemLabel();
			this.styleBorder = new StyleSeriesBorder();
			this.styleSeriesPointAppearance = new StyleMarkerSeriesPoint();
			this.styleSeriesTextAppearance = new StyleSeriesItemTextBlock();
			this.styleSeriesLineSeriesAppearance = new StyleLineSeries();
			this.styleSeriesLineSeriesAppearance.lineStyleContainerObject = this.styleSeriesParent;
			this.styleSeriesEmptyValue = new EmptyValue();
		}

		// Token: 0x0600EE2B RID: 60971 RVA: 0x0036411B File Offset: 0x0036231B
		public StyleSeries(FillStyleSeries fillStyle, StyleSeriesItemLabel styleSeriesLabel, StyleMarkerSeriesPoint stylePointMarker, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle)
		{
			this.styleSeriesFillStyle = fillStyle;
			this.styleSeriesPointDimentions = new DimensionsPointMarker();
			this.styleSeriesLabelAppearance = styleSeriesLabel;
			this.styleSeriesPointAppearance = stylePointMarker;
			this.styleSeriesCorners = corners;
		}

		// Token: 0x0600EE2C RID: 60972 RVA: 0x00364154 File Offset: 0x00362354
		public override object Clone()
		{
			StyleSeries styleSeries = (StyleSeries)base.MemberwiseClone();
			styleSeries.ViewState = base.CloneState();
			styleSeries.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleSeries.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleSeries.styleSeriesCorners = (Corners)this.styleSeriesCorners.Clone();
			styleSeries.styleSeriesCorners.cornersContainerObject = styleSeries;
			styleSeries.styleSeriesLineSeriesAppearance = (StyleLineSeries)this.styleSeriesLineSeriesAppearance.Clone();
			styleSeries.styleSeriesPointAppearance = (StyleMarkerSeriesPoint)this.styleSeriesPointAppearance.Clone();
			styleSeries.styleSeriesTextAppearance = (StyleSeriesItemTextBlock)this.styleSeriesTextAppearance.Clone();
			styleSeries.styleSeriesLabelAppearance = (StyleSeriesItemLabel)this.styleSeriesLabelAppearance.Clone();
			styleSeries.styleSeriesFillStyle = (FillStyleSeries)this.styleSeriesFillStyle.Clone();
			styleSeries.styleSeriesEmptyValue = (EmptyValue)this.styleSeriesEmptyValue.Clone();
			styleSeries.styleSeriesPointDimentions = (DimensionsPointMarker)this.styleSeriesPointDimentions.Clone();
			styleSeries.styleContainerObject = null;
			return styleSeries;
		}

		// Token: 0x0600EE2D RID: 60973 RVA: 0x0036426C File Offset: 0x0036246C
		protected override void Dispose(bool disposing)
		{
			if (this.styleSeriesEmptyValue != null)
			{
				this.styleSeriesEmptyValue.Dispose();
				this.styleSeriesEmptyValue = null;
			}
			if (this.styleSeriesFillStyle != null)
			{
				this.styleSeriesFillStyle.Dispose();
				this.styleSeriesFillStyle = null;
			}
			if (this.styleSeriesLabelAppearance != null)
			{
				this.styleSeriesLabelAppearance.Dispose();
				this.styleSeriesLabelAppearance = null;
			}
			if (this.styleSeriesLineSeriesAppearance != null)
			{
				this.styleSeriesLineSeriesAppearance.Dispose();
				this.styleSeriesLineSeriesAppearance = null;
			}
			if (this.styleSeriesPointAppearance != null)
			{
				this.styleSeriesPointAppearance.Dispose();
				this.styleSeriesPointAppearance = null;
			}
			if (this.styleSeriesPointDimentions != null)
			{
				this.styleSeriesPointDimentions.Dispose();
				this.styleSeriesPointDimentions = null;
			}
			if (this.styleSeriesTextAppearance != null)
			{
				this.styleSeriesTextAppearance.Dispose();
				this.styleSeriesTextAppearance = null;
			}
			if (this.styleSeriesCorners != null)
			{
				this.styleSeriesCorners.Dispose();
				this.styleSeriesCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EE2E RID: 60974 RVA: 0x00364350 File Offset: 0x00362550
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleSeriesCorners).TrackViewState();
			((IChartingStateManager)this.styleSeriesEmptyValue).TrackViewState();
			((IChartingStateManager)this.styleSeriesFillStyle).TrackViewState();
			((IChartingStateManager)this.styleSeriesLabelAppearance).TrackViewState();
			((IChartingStateManager)this.styleSeriesLineSeriesAppearance).TrackViewState();
			((IChartingStateManager)this.styleSeriesPointAppearance).TrackViewState();
			((IChartingStateManager)this.styleSeriesTextAppearance).TrackViewState();
			((IChartingStateManager)this.styleSeriesPointDimentions).TrackViewState();
		}

		// Token: 0x0600EE2F RID: 60975 RVA: 0x003643BC File Offset: 0x003625BC
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleSeriesCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.styleSeriesEmptyValue).LoadViewState(array[2]);
				((IChartingStateManager)this.styleSeriesFillStyle).LoadViewState(array[3]);
				((IChartingStateManager)this.styleSeriesLabelAppearance).LoadViewState(array[4]);
				((IChartingStateManager)this.styleSeriesLineSeriesAppearance).LoadViewState(array[5]);
				((IChartingStateManager)this.styleSeriesPointAppearance).LoadViewState(array[6]);
				((IChartingStateManager)this.styleSeriesTextAppearance).LoadViewState(array[7]);
				((IChartingStateManager)this.styleSeriesPointDimentions).LoadViewState(array[8]);
			}
		}

		// Token: 0x0600EE30 RID: 60976 RVA: 0x0036444C File Offset: 0x0036264C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleSeriesCorners).SaveViewState(),
				((IChartingStateManager)this.styleSeriesEmptyValue).SaveViewState(),
				((IChartingStateManager)this.styleSeriesFillStyle).SaveViewState(),
				((IChartingStateManager)this.styleSeriesLabelAppearance).SaveViewState(),
				((IChartingStateManager)this.styleSeriesLineSeriesAppearance).SaveViewState(),
				((IChartingStateManager)this.styleSeriesPointAppearance).SaveViewState(),
				((IChartingStateManager)this.styleSeriesTextAppearance).SaveViewState(),
				((IChartingStateManager)this.styleSeriesPointDimentions).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044C3 RID: 17603
		private const int DEFAULT_BUBBLE_SIZE = 20;

		// Token: 0x040044C4 RID: 17604
		private const ChartSeriesLegendDisplayMode DEFAULT_DISPLAY_MODE = ChartSeriesLegendDisplayMode.SeriesName;

		// Token: 0x040044C5 RID: 17605
		private Corners styleSeriesCorners;

		// Token: 0x040044C6 RID: 17606
		private FillStyleSeries styleSeriesFillStyle;

		// Token: 0x040044C7 RID: 17607
		private StyleSeriesItemLabel styleSeriesLabelAppearance;

		// Token: 0x040044C8 RID: 17608
		private StyleSeriesItemTextBlock styleSeriesTextAppearance;

		// Token: 0x040044C9 RID: 17609
		private StyleMarkerSeriesPoint styleSeriesPointAppearance;

		// Token: 0x040044CA RID: 17610
		private StyleLineSeries styleSeriesLineSeriesAppearance;

		// Token: 0x040044CB RID: 17611
		private EmptyValue styleSeriesEmptyValue;

		// Token: 0x040044CC RID: 17612
		internal ChartSeries styleSeriesParent;

		// Token: 0x040044CD RID: 17613
		internal DimensionsPointMarker styleSeriesPointDimentions;
	}
}
