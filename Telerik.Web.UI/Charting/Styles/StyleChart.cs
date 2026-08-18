using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017CD RID: 6093
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleChart : LayoutStyle
	{
		// Token: 0x170047BA RID: 18362
		// (get) Token: 0x0600ED03 RID: 60675 RVA: 0x0036138B File Offset: 0x0035F58B
		// (set) Token: 0x0600ED04 RID: 60676 RVA: 0x003613B4 File Offset: 0x0035F5B4
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue(typeof(decimal), "75")]
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
				if (this.styleChart != null)
				{
					foreach (ChartSeries chartSeries in base.Chart.Series)
					{
						chartSeries.Appearance.BarWidthPercent = value;
					}
				}
			}
		}

		// Token: 0x170047BB RID: 18363
		// (get) Token: 0x0600ED05 RID: 60677 RVA: 0x0036142C File Offset: 0x0035F62C
		// (set) Token: 0x0600ED06 RID: 60678 RVA: 0x00361452 File Offset: 0x0035F652
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(decimal), "0")]
		[Description("Sets the overlap percent of the bars.")]
		[Bindable(true)]
		public decimal BarOverlapPercent
		{
			get
			{
				return (decimal)(base.ViewState["BarOverlapPercent"] ?? 0m);
			}
			set
			{
				base.ViewState["BarOverlapPercent"] = value;
			}
		}

		// Token: 0x170047BC RID: 18364
		// (get) Token: 0x0600ED07 RID: 60679 RVA: 0x0036146A File Offset: 0x0035F66A
		// (set) Token: 0x0600ED08 RID: 60680 RVA: 0x0036148B File Offset: 0x0035F68B
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(TextQuality), "AntiAliasGridFit")]
		public TextQuality TextQuality
		{
			get
			{
				return (TextQuality)(base.ViewState["TextQuality"] ?? TextQuality.AntiAliasGridFit);
			}
			set
			{
				base.ViewState["TextQuality"] = value;
			}
		}

		// Token: 0x170047BD RID: 18365
		// (get) Token: 0x0600ED09 RID: 60681 RVA: 0x003614A3 File Offset: 0x0035F6A3
		// (set) Token: 0x0600ED0A RID: 60682 RVA: 0x003614C4 File Offset: 0x0035F6C4
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ImageQuality), "HighQuality")]
		public ImageQuality ImageQuality
		{
			get
			{
				return (ImageQuality)(base.ViewState["ImageQuality"] ?? ImageQuality.HighQuality);
			}
			set
			{
				base.ViewState["ImageQuality"] = value;
			}
		}

		// Token: 0x170047BE RID: 18366
		// (get) Token: 0x0600ED0B RID: 60683 RVA: 0x003614DC File Offset: 0x0035F6DC
		// (set) Token: 0x0600ED0C RID: 60684 RVA: 0x003614E4 File Offset: 0x0035F6E4
		[Description("Specifies the types of the rectangle corners.")]
		[TypeConverter(typeof(CornersConverter))]
		[Browsable(true)]
		[Bindable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		public Corners Corners
		{
			get
			{
				return this.styleChartCorners;
			}
			set
			{
				this.styleChartCorners = value;
			}
		}

		// Token: 0x170047BF RID: 18367
		// (get) Token: 0x0600ED0D RID: 60685 RVA: 0x003614ED File Offset: 0x0035F6ED
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FillStyleChart FillStyle
		{
			get
			{
				return this.styleChartFillStyle;
			}
		}

		// Token: 0x170047C0 RID: 18368
		// (get) Token: 0x0600ED0E RID: 60686 RVA: 0x003614F5 File Offset: 0x0035F6F5
		[DefaultValue("Rectangle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		internal string Figure
		{
			get
			{
				return "Rectangle";
			}
		}

		// Token: 0x170047C1 RID: 18369
		// (get) Token: 0x0600ED0F RID: 60687 RVA: 0x003614FC File Offset: 0x0035F6FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170047C2 RID: 18370
		// (get) Token: 0x0600ED10 RID: 60688 RVA: 0x003614FF File Offset: 0x0035F6FF
		[Obsolete("Is not applicable")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Position Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170047C3 RID: 18371
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.Dimensions)
				{
					return this.Dimensions;
				}
				switch (name)
				{
				case StyleProperties.Figure:
					return this.Figure;
				case StyleProperties.FillStyle:
					return this.FillStyle;
				case StyleProperties.Corners:
					return this.Corners;
				default:
					switch (name)
					{
					case StyleProperties.BarWidthPercent:
						return this.BarWidthPercent;
					case StyleProperties.BarOverlapPercent:
						return this.BarOverlapPercent;
					case StyleProperties.TextQuality:
						return this.TextQuality;
					case StyleProperties.ImageQuality:
						return this.ImageQuality;
					default:
						return base[name];
					}
					break;
				}
			}
		}

		// Token: 0x0600ED12 RID: 60690 RVA: 0x0036159E File Offset: 0x0035F79E
		public StyleChart(Chart chart) : base(new StyleChartBorder(), true, new ShadowStyleChart(chart), null, new DimensionsChart())
		{
			this.styleChart = chart;
			this.styleChartFillStyle = new FillStyleChart();
			this.styleChartCorners = new Corners();
		}

		// Token: 0x0600ED13 RID: 60691 RVA: 0x003615D5 File Offset: 0x0035F7D5
		public StyleChart(DimensionsChart dimensions, FillStyleChart fillStyle, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, null, dimensions)
		{
			this.styleChartFillStyle = fillStyle;
			this.styleChartCorners = corners;
		}

		// Token: 0x0600ED14 RID: 60692 RVA: 0x003615F4 File Offset: 0x0035F7F4
		internal override void Reset()
		{
			base.Reset();
			this.dimensions = new DimensionsChart();
			this.styleChartFillStyle = new FillStyleChart();
			this.styleChartCorners = new Corners();
			this.styleBorder = new StyleChartBorder();
			this.styleShadow = new ShadowStyleChart(this.styleChart);
			this.TextQuality = TextQuality.AntiAliasGridFit;
			this.ImageQuality = ImageQuality.HighQuality;
			this.BarWidthPercent = 75m;
			this.BarOverlapPercent = 0m;
		}

		// Token: 0x0600ED15 RID: 60693 RVA: 0x0036166C File Offset: 0x0035F86C
		public override object Clone()
		{
			StyleChart styleChart = (StyleChart)base.MemberwiseClone();
			styleChart.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleChart.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleChart.dimensions = (DimensionsChart)this.Dimensions.Clone();
			styleChart.styleChartCorners.CopyFrom(this.styleChartCorners);
			styleChart.styleChartFillStyle = (FillStyleChart)this.styleChartFillStyle.Clone();
			return styleChart;
		}

		// Token: 0x0600ED16 RID: 60694 RVA: 0x003616EF File Offset: 0x0035F8EF
		protected override void Dispose(bool disposing)
		{
			if (this.styleChartFillStyle != null)
			{
				this.styleChartFillStyle.Dispose();
				this.styleChartFillStyle = null;
			}
			if (this.styleChartCorners != null)
			{
				this.styleChartCorners.Dispose();
				this.styleChartCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600ED17 RID: 60695 RVA: 0x0036172C File Offset: 0x0035F92C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleChartCorners).TrackViewState();
			((IChartingStateManager)this.dimensions).TrackViewState();
			((IChartingStateManager)this.styleChartFillStyle).TrackViewState();
		}

		// Token: 0x0600ED18 RID: 60696 RVA: 0x00361758 File Offset: 0x0035F958
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleChartCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.dimensions).LoadViewState(array[2]);
				((IChartingStateManager)this.styleChartFillStyle).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600ED19 RID: 60697 RVA: 0x003617A4 File Offset: 0x0035F9A4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleChartCorners).SaveViewState(),
				((IChartingStateManager)this.dimensions).SaveViewState(),
				((IChartingStateManager)this.styleChartFillStyle).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044A4 RID: 17572
		private Corners styleChartCorners;

		// Token: 0x040044A5 RID: 17573
		private FillStyleChart styleChartFillStyle;
	}
}
