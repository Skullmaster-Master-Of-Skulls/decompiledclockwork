using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E7 RID: 6119
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleSeriesItem : Style
	{
		// Token: 0x0600EE31 RID: 60977 RVA: 0x00364502 File Offset: 0x00362702
		public StyleSeriesItem(ChartSeries series) : base(series)
		{
			this.styleSeriesItemFillStyle = new FillStyleSeries(series);
			this.styleBorder = new StyleSeriesBorder(series);
			this.styleSeriesItemPointDimentions = new DimensionsPointMarker();
		}

		// Token: 0x0600EE32 RID: 60978 RVA: 0x0036452E File Offset: 0x0036272E
		public StyleSeriesItem()
		{
			this.styleSeriesItemFillStyle = new FillStyleSeries();
			this.styleSeriesItemCorners = new Corners();
			this.styleBorder = new StyleSeriesBorder();
			this.styleSeriesItemPointDimentions = new DimensionsPointMarker();
		}

		// Token: 0x1700481D RID: 18461
		// (get) Token: 0x0600EE33 RID: 60979 RVA: 0x00364562 File Offset: 0x00362762
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SkinnableProperty]
		public override ShadowStyle Shadow
		{
			get
			{
				return base.Shadow;
			}
		}

		// Token: 0x1700481E RID: 18462
		// (get) Token: 0x0600EE34 RID: 60980 RVA: 0x0036456A File Offset: 0x0036276A
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FillStyleSeries FillStyle
		{
			get
			{
				return this.styleSeriesItemFillStyle;
			}
		}

		// Token: 0x1700481F RID: 18463
		// (get) Token: 0x0600EE35 RID: 60981 RVA: 0x00364572 File Offset: 0x00362772
		// (set) Token: 0x0600EE36 RID: 60982 RVA: 0x00364593 File Offset: 0x00362793
		[SkinnableProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool Exploded
		{
			get
			{
				return (bool)(base.ViewState["Exploded"] ?? false);
			}
			set
			{
				base.ViewState["Exploded"] = value;
			}
		}

		// Token: 0x17004820 RID: 18464
		// (get) Token: 0x0600EE37 RID: 60983 RVA: 0x003645AB File Offset: 0x003627AB
		// (set) Token: 0x0600EE38 RID: 60984 RVA: 0x003645B3 File Offset: 0x003627B3
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(CornersConverter))]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		public Corners Corners
		{
			get
			{
				return this.styleSeriesItemCorners;
			}
			set
			{
				this.styleSeriesItemCorners = value;
			}
		}

		// Token: 0x17004821 RID: 18465
		// (get) Token: 0x0600EE39 RID: 60985 RVA: 0x003645BC File Offset: 0x003627BC
		// (set) Token: 0x0600EE3A RID: 60986 RVA: 0x003645DC File Offset: 0x003627DC
		[SkinnableProperty]
		[DefaultValue("Ellipse")]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Point")]
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

		// Token: 0x17004822 RID: 18466
		// (get) Token: 0x0600EE3B RID: 60987 RVA: 0x003645EF File Offset: 0x003627EF
		// (set) Token: 0x0600EE3C RID: 60988 RVA: 0x00364614 File Offset: 0x00362814
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(0f)]
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

		// Token: 0x17004823 RID: 18467
		// (get) Token: 0x0600EE3D RID: 60989 RVA: 0x0036462C File Offset: 0x0036282C
		// (set) Token: 0x0600EE3E RID: 60990 RVA: 0x00364634 File Offset: 0x00362834
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Point")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DimensionsPointMarker PointDimentions
		{
			get
			{
				return this.styleSeriesItemPointDimentions;
			}
			set
			{
				this.styleSeriesItemPointDimentions = value;
			}
		}

		// Token: 0x0600EE3F RID: 60991 RVA: 0x0036463D File Offset: 0x0036283D
		internal override void Reset()
		{
			base.Reset();
			this.styleSeriesItemPointDimentions = new DimensionsPointMarker();
			this.styleSeriesItemFillStyle = new FillStyleSeries();
			this.styleSeriesItemCorners = new Corners();
			this.styleBorder = new StyleSeriesBorder();
			this.Exploded = false;
		}

		// Token: 0x0600EE40 RID: 60992 RVA: 0x00364678 File Offset: 0x00362878
		public override object Clone()
		{
			StyleSeriesItem styleSeriesItem = new StyleSeriesItem();
			styleSeriesItem.ViewState = base.CloneState();
			styleSeriesItem.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleSeriesItem.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleSeriesItem.styleSeriesItemFillStyle = (FillStyleSeries)this.styleSeriesItemFillStyle.Clone();
			styleSeriesItem.styleSeriesItemCorners.CopyFrom(this.styleSeriesItemCorners);
			styleSeriesItem.styleSeriesItemPointDimentions = (DimensionsPointMarker)this.styleSeriesItemPointDimentions.Clone();
			styleSeriesItem.styleContainerObject = null;
			return base.Clone();
		}

		// Token: 0x0600EE41 RID: 60993 RVA: 0x00364710 File Offset: 0x00362910
		protected override void Dispose(bool disposing)
		{
			if (this.styleSeriesItemFillStyle != null)
			{
				this.styleSeriesItemFillStyle.Dispose();
				this.styleSeriesItemFillStyle = null;
			}
			if (this.styleSeriesItemPointDimentions != null)
			{
				this.styleSeriesItemPointDimentions.Dispose();
				this.styleSeriesItemPointDimentions = null;
			}
			if (this.styleSeriesItemCorners != null)
			{
				this.styleSeriesItemCorners.Dispose();
				this.styleSeriesItemCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EE42 RID: 60994 RVA: 0x00364772 File Offset: 0x00362972
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleSeriesItemCorners).TrackViewState();
			((IChartingStateManager)this.styleSeriesItemFillStyle).TrackViewState();
			((IChartingStateManager)this.styleSeriesItemPointDimentions).TrackViewState();
		}

		// Token: 0x0600EE43 RID: 60995 RVA: 0x0036479C File Offset: 0x0036299C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleSeriesItemCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.styleSeriesItemFillStyle).LoadViewState(array[2]);
				((IChartingStateManager)this.styleSeriesItemPointDimentions).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600EE44 RID: 60996 RVA: 0x003647E8 File Offset: 0x003629E8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleSeriesItemCorners).SaveViewState(),
				((IChartingStateManager)this.styleSeriesItemFillStyle).SaveViewState(),
				((IChartingStateManager)this.styleSeriesItemPointDimentions).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044CE RID: 17614
		private FillStyleSeries styleSeriesItemFillStyle;

		// Token: 0x040044CF RID: 17615
		private Corners styleSeriesItemCorners;

		// Token: 0x040044D0 RID: 17616
		internal DimensionsPointMarker styleSeriesItemPointDimentions;
	}
}
