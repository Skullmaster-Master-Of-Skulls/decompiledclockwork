using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E5 RID: 6117
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StylePlotArea : LayoutStyle
	{
		// Token: 0x17004801 RID: 18433
		// (get) Token: 0x0600EDED RID: 60909 RVA: 0x00363744 File Offset: 0x00361944
		// (set) Token: 0x0600EDEE RID: 60910 RVA: 0x0036374C File Offset: 0x0036194C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal ChartPlotArea PlotArea
		{
			get
			{
				return this.stylePlotAreaParent;
			}
			set
			{
				this.stylePlotAreaParent = value;
			}
		}

		// Token: 0x17004802 RID: 18434
		// (get) Token: 0x0600EDEF RID: 60911 RVA: 0x00363755 File Offset: 0x00361955
		// (set) Token: 0x0600EDF0 RID: 60912 RVA: 0x0036375D File Offset: 0x0036195D
		[TypeConverter(typeof(CornersConverter))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		public Corners Corners
		{
			get
			{
				return this.stylePlotAreaCorners;
			}
			set
			{
				this.stylePlotAreaCorners = value;
			}
		}

		// Token: 0x17004803 RID: 18435
		// (get) Token: 0x0600EDF1 RID: 60913 RVA: 0x00363766 File Offset: 0x00361966
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public FillStylePlotArea FillStyle
		{
			get
			{
				return this.stylePlotAreaFillStyle;
			}
		}

		// Token: 0x17004804 RID: 18436
		// (get) Token: 0x0600EDF2 RID: 60914 RVA: 0x0036376E File Offset: 0x0036196E
		[DefaultValue("Rectangle")]
		[Browsable(false)]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public string Figure
		{
			get
			{
				return "Rectangle";
			}
		}

		// Token: 0x17004805 RID: 18437
		// (get) Token: 0x0600EDF3 RID: 60915 RVA: 0x00363775 File Offset: 0x00361975
		// (set) Token: 0x0600EDF4 RID: 60916 RVA: 0x00363795 File Offset: 0x00361995
		[SkinnableProperty]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor(typeof(ChartPaletteEditor), typeof(UITypeEditor))]
		public string SeriesPalette
		{
			get
			{
				return (string)(base.ViewState["SeriesPalette"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) == 0)
				{
					base.ViewState["SeriesPalette"] = "";
					return;
				}
				base.ViewState["SeriesPalette"] = value;
			}
		}

		// Token: 0x17004806 RID: 18438
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name <= StyleProperties.Corners)
				{
					if (name == StyleProperties.Dimensions)
					{
						return this.dimensions;
					}
					switch (name)
					{
					case StyleProperties.Figure:
						return this.Figure;
					case StyleProperties.FillStyle:
						return this.stylePlotAreaFillStyle;
					case StyleProperties.Corners:
						return this.stylePlotAreaCorners;
					}
				}
				else
				{
					if (name == StyleProperties.Position)
					{
						return this.position;
					}
					if (name == StyleProperties.SeriesPalette)
					{
						return this.SeriesPalette;
					}
				}
				return base[name];
			}
		}

		// Token: 0x0600EDF6 RID: 60918 RVA: 0x00363836 File Offset: 0x00361A36
		public StylePlotArea() : base(new DimensionsPlotArea())
		{
			this.stylePlotAreaFillStyle = new FillStylePlotArea();
			this.stylePlotAreaCorners = new Corners();
			this.styleBorder = new StyleChartBorder();
			this.autoLayoutMargins = new ChartMarginsPlotArea();
		}

		// Token: 0x0600EDF7 RID: 60919 RVA: 0x0036386F File Offset: 0x00361A6F
		public StylePlotArea(Dimensions dimensions, FillStylePlotArea fillStyle, Position position, string palette, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, position, dimensions)
		{
			this.stylePlotAreaFillStyle = fillStyle;
			this.SeriesPalette = palette;
			this.stylePlotAreaCorners = corners;
			this.autoLayoutMargins = new ChartMarginsPlotArea();
		}

		// Token: 0x0600EDF8 RID: 60920 RVA: 0x003638A1 File Offset: 0x00361AA1
		internal override void Reset()
		{
			base.Reset();
			this.SeriesPalette = "";
			this.stylePlotAreaFillStyle = new FillStylePlotArea();
			this.stylePlotAreaCorners = new Corners();
			this.styleBorder = new StyleChartBorder();
			this.dimensions = new DimensionsPlotArea();
		}

		// Token: 0x0600EDF9 RID: 60921 RVA: 0x003638E0 File Offset: 0x00361AE0
		internal void SetAutoLayoutDefaults()
		{
			this.SaveDimensions();
			this.Dimensions.Margins.Top = DefaultValues.AUTO_MARGIN_PLOTAREA_TOP.Clone();
			this.Dimensions.Margins.Right = DefaultValues.AUTO_MARGIN_PLOTAREA_RIGHT.Clone();
			this.Dimensions.Margins.Bottom = DefaultValues.AUTO_MARGIN_PLOTAREA_BOTTOM.Clone();
			this.Dimensions.Margins.Left = DefaultValues.AUTO_MARGIN_PLOTAREA_LEFT.Clone();
		}

		// Token: 0x0600EDFA RID: 60922 RVA: 0x0036395B File Offset: 0x00361B5B
		internal void SaveDimensions()
		{
			this.Dimensions.Copy = this.Dimensions;
		}

		// Token: 0x0600EDFB RID: 60923 RVA: 0x0036396E File Offset: 0x00361B6E
		internal void RestoreDimensions()
		{
			this.Dimensions.SetDimensions(this.Dimensions.Copy);
		}

		// Token: 0x0600EDFC RID: 60924 RVA: 0x00363986 File Offset: 0x00361B86
		internal void RestoreDimensions(bool marginsOnly)
		{
			this.autoLayoutMargins = (ChartMargins)this.Dimensions.Margins.Clone();
			this.Dimensions.Margins.CopyFrom(this.Dimensions.Copy.Margins);
		}

		// Token: 0x0600EDFD RID: 60925 RVA: 0x003639C3 File Offset: 0x00361BC3
		internal void RestoreAutoLayoutMargins()
		{
			this.Dimensions.Margins.CopyFrom(this.autoLayoutMargins);
		}

		// Token: 0x0600EDFE RID: 60926 RVA: 0x003639DC File Offset: 0x00361BDC
		public override object Clone()
		{
			StylePlotArea stylePlotArea = (StylePlotArea)base.MemberwiseClone();
			stylePlotArea.stylePlotAreaCorners.CopyFrom(this.stylePlotAreaCorners);
			stylePlotArea.stylePlotAreaFillStyle = (FillStylePlotArea)this.stylePlotAreaFillStyle.Clone();
			stylePlotArea.position = (Position)this.position.Clone();
			stylePlotArea.dimensions = (Dimensions)this.dimensions.Clone();
			stylePlotArea.styleBorder = (StyleBorder)this.styleBorder.Clone();
			stylePlotArea.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			stylePlotArea.styleContainerObject = null;
			return stylePlotArea;
		}

		// Token: 0x0600EDFF RID: 60927 RVA: 0x00363A7C File Offset: 0x00361C7C
		protected override void Dispose(bool disposing)
		{
			if (this.stylePlotAreaFillStyle != null)
			{
				this.stylePlotAreaFillStyle.Dispose();
				this.stylePlotAreaFillStyle = null;
			}
			if (this.autoLayoutMargins != null)
			{
				this.autoLayoutMargins.Dispose();
				this.autoLayoutMargins = null;
			}
			if (this.stylePlotAreaCorners != null)
			{
				this.stylePlotAreaCorners.Dispose();
				this.stylePlotAreaCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EE00 RID: 60928 RVA: 0x00363AE4 File Offset: 0x00361CE4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.stylePlotAreaCorners).TrackViewState();
			((IChartingStateManager)this.stylePlotAreaFillStyle).TrackViewState();
			((IChartingStateManager)this.autoLayoutMargins).TrackViewState();
		}

		// Token: 0x0600EE01 RID: 60929 RVA: 0x00363B10 File Offset: 0x00361D10
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.stylePlotAreaCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.stylePlotAreaFillStyle).LoadViewState(array[2]);
				((IChartingStateManager)this.autoLayoutMargins).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600EE02 RID: 60930 RVA: 0x00363B5C File Offset: 0x00361D5C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.stylePlotAreaCorners).SaveViewState(),
				((IChartingStateManager)this.stylePlotAreaFillStyle).SaveViewState(),
				((IChartingStateManager)this.autoLayoutMargins).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044BF RID: 17599
		private ChartPlotArea stylePlotAreaParent;

		// Token: 0x040044C0 RID: 17600
		private Corners stylePlotAreaCorners;

		// Token: 0x040044C1 RID: 17601
		internal FillStylePlotArea stylePlotAreaFillStyle;

		// Token: 0x040044C2 RID: 17602
		private ChartMargins autoLayoutMargins;
	}
}
