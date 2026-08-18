using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017CE RID: 6094
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleChartDataTable : LayoutStyle
	{
		// Token: 0x170047C4 RID: 18372
		// (get) Token: 0x0600ED1A RID: 60698 RVA: 0x00361800 File Offset: 0x0035FA00
		// (set) Token: 0x0600ED1B RID: 60699 RVA: 0x00361821 File Offset: 0x0035FA21
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
				if (this.styleChartDataTableParent != null && !base.Visible)
				{
					this.styleChartDataTableParent.PlotArea.Appearance.dimensions = new DimensionsPlotArea();
				}
			}
		}

		// Token: 0x170047C5 RID: 18373
		// (get) Token: 0x0600ED1C RID: 60700 RVA: 0x00361854 File Offset: 0x0035FA54
		// (set) Token: 0x0600ED1D RID: 60701 RVA: 0x00361876 File Offset: 0x0035FA76
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(18)]
		public int CellWidth
		{
			get
			{
				return (int)(base.ViewState["CellWidth"] ?? 18);
			}
			set
			{
				base.ViewState["CellWidth"] = value;
			}
		}

		// Token: 0x170047C6 RID: 18374
		// (get) Token: 0x0600ED1E RID: 60702 RVA: 0x0036188E File Offset: 0x0035FA8E
		// (set) Token: 0x0600ED1F RID: 60703 RVA: 0x003618B0 File Offset: 0x0035FAB0
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[DefaultValue(14)]
		public int CellHeight
		{
			get
			{
				return (int)(base.ViewState["CellHeight"] ?? 14);
			}
			set
			{
				base.ViewState["CellHeight"] = value;
			}
		}

		// Token: 0x170047C7 RID: 18375
		// (get) Token: 0x0600ED20 RID: 60704 RVA: 0x003618C8 File Offset: 0x0035FAC8
		// (set) Token: 0x0600ED21 RID: 60705 RVA: 0x003618EC File Offset: 0x0035FAEC
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(TableRenderType.PlotAreaRelative)]
		public TableRenderType RenderType
		{
			get
			{
				return (TableRenderType)(base.ViewState["RenderType"] ?? TableRenderType.PlotAreaRelative);
			}
			set
			{
				base.ViewState["RenderType"] = value;
				if (this.position.Auto)
				{
					this.position = new Position(this.position.AlignedPosition);
				}
				if (value == TableRenderType.TableFixedSize)
				{
					this.dimensions.AutoSize = false;
				}
			}
		}

		// Token: 0x170047C8 RID: 18376
		// (get) Token: 0x0600ED22 RID: 60706 RVA: 0x00361942 File Offset: 0x0035FB42
		// (set) Token: 0x0600ED23 RID: 60707 RVA: 0x00361963 File Offset: 0x0035FB63
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[DefaultValue(true)]
		public bool DrawHorizontalLines
		{
			get
			{
				return (bool)(base.ViewState["DrawHorizontalLines"] ?? true);
			}
			set
			{
				base.ViewState["DrawHorizontalLines"] = value;
			}
		}

		// Token: 0x170047C9 RID: 18377
		// (get) Token: 0x0600ED24 RID: 60708 RVA: 0x0036197B File Offset: 0x0035FB7B
		// (set) Token: 0x0600ED25 RID: 60709 RVA: 0x0036199C File Offset: 0x0035FB9C
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[DefaultValue(true)]
		public bool DrawVerticalLines
		{
			get
			{
				return (bool)(base.ViewState["DrawVerticalLines"] ?? true);
			}
			set
			{
				base.ViewState["DrawVerticalLines"] = value;
			}
		}

		// Token: 0x170047CA RID: 18378
		// (get) Token: 0x0600ED26 RID: 60710 RVA: 0x003619B4 File Offset: 0x0035FBB4
		// (set) Token: 0x0600ED27 RID: 60711 RVA: 0x003619D5 File Offset: 0x0035FBD5
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool DrawLines
		{
			get
			{
				return (bool)(base.ViewState["DrawLines"] ?? true);
			}
			set
			{
				base.ViewState["DrawLines"] = value;
				this.DrawVerticalLines = value;
				this.DrawHorizontalLines = value;
			}
		}

		// Token: 0x170047CB RID: 18379
		// (get) Token: 0x0600ED28 RID: 60712 RVA: 0x003619FB File Offset: 0x0035FBFB
		// (set) Token: 0x0600ED29 RID: 60713 RVA: 0x00361A1C File Offset: 0x0035FC1C
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[DefaultValue(ContentVerticalAlign.Middle)]
		public ContentVerticalAlign TextVerticalAlign
		{
			get
			{
				return (ContentVerticalAlign)(base.ViewState["TextVerticalAlign"] ?? ContentVerticalAlign.Middle);
			}
			set
			{
				base.ViewState["TextVerticalAlign"] = value;
			}
		}

		// Token: 0x170047CC RID: 18380
		// (get) Token: 0x0600ED2A RID: 60714 RVA: 0x00361A34 File Offset: 0x0035FC34
		// (set) Token: 0x0600ED2B RID: 60715 RVA: 0x00361A55 File Offset: 0x0035FC55
		[SkinnableProperty]
		[DefaultValue(ContentHorizontalAlign.Center)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public ContentHorizontalAlign TextHorizontalAlign
		{
			get
			{
				return (ContentHorizontalAlign)(base.ViewState["TextHorizontalAlign"] ?? ContentHorizontalAlign.Center);
			}
			set
			{
				base.ViewState["TextHorizontalAlign"] = value;
			}
		}

		// Token: 0x170047CD RID: 18381
		// (get) Token: 0x0600ED2C RID: 60716 RVA: 0x00361A6D File Offset: 0x0035FC6D
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		[DefaultValue("Rectangle")]
		public string Figure
		{
			get
			{
				return "Rectangle";
			}
		}

		// Token: 0x170047CE RID: 18382
		// (get) Token: 0x0600ED2D RID: 60717 RVA: 0x00361A74 File Offset: 0x0035FC74
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FillStyle FillStyle
		{
			get
			{
				return this.styleChartDataTableFillStyle;
			}
		}

		// Token: 0x170047CF RID: 18383
		// (get) Token: 0x0600ED2E RID: 60718 RVA: 0x00361A7C File Offset: 0x0035FC7C
		// (set) Token: 0x0600ED2F RID: 60719 RVA: 0x00361A9D File Offset: 0x0035FC9D
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue(AutoTextWrap.Auto)]
		public AutoTextWrap AutoTextWrap
		{
			get
			{
				return (AutoTextWrap)(base.ViewState["AutoTextWrap"] ?? AutoTextWrap.Auto);
			}
			set
			{
				base.ViewState["AutoTextWrap"] = value;
			}
		}

		// Token: 0x170047D0 RID: 18384
		// (get) Token: 0x0600ED30 RID: 60720 RVA: 0x00361AB5 File Offset: 0x0035FCB5
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TextProperties TextProperties
		{
			get
			{
				return this.styleChartDataTableTextProperties;
			}
		}

		// Token: 0x170047D1 RID: 18385
		internal override object this[StyleProperties name]
		{
			get
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
					return this.styleChartDataTableFillStyle;
				case StyleProperties.Corners:
					return this.styleChartDataTableCorners;
				default:
					switch (name)
					{
					case StyleProperties.Position:
						return this.position;
					case StyleProperties.TextProperties:
						return this.styleChartDataTableTextProperties;
					default:
						return base[name];
					}
					break;
				}
			}
		}

		// Token: 0x0600ED32 RID: 60722 RVA: 0x00361B2C File Offset: 0x0035FD2C
		public StyleChartDataTable() : this(null)
		{
		}

		// Token: 0x0600ED33 RID: 60723 RVA: 0x00361B35 File Offset: 0x0035FD35
		public StyleChartDataTable(ChartDataTable parent) : this(null, new FillStyle(), null, new TextProperties(), new StyleDataTableBorder(), null, true)
		{
			this.styleChartDataTableParent = parent;
		}

		// Token: 0x0600ED34 RID: 60724 RVA: 0x00361B57 File Offset: 0x0035FD57
		public StyleChartDataTable(Dimensions dimensions, FillStyle fillStyle, Position position, TextProperties textProperties, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, position, dimensions)
		{
			this.styleChartDataTableFillStyle = fillStyle;
			this.styleChartDataTableTextProperties = textProperties;
		}

		// Token: 0x0600ED35 RID: 60725 RVA: 0x00361B78 File Offset: 0x0035FD78
		internal override void Reset()
		{
			base.Reset();
			this.styleChartDataTableFillStyle = new FillStyle();
			this.styleChartDataTableTextProperties = new TextProperties();
			this.styleChartDataTableCorners = new Corners();
			this.styleBorder = new StyleDataTableBorder();
			this.CellWidth = 18;
			this.CellHeight = 14;
			this.RenderType = TableRenderType.PlotAreaRelative;
			this.DrawLines = true;
			this.TextVerticalAlign = ContentVerticalAlign.Middle;
			this.TextHorizontalAlign = ContentHorizontalAlign.Center;
			this.Visible = false;
		}

		// Token: 0x0600ED36 RID: 60726 RVA: 0x00361BEA File Offset: 0x0035FDEA
		internal void SaveDimensions()
		{
			this.Dimensions.Copy = this.Dimensions;
		}

		// Token: 0x0600ED37 RID: 60727 RVA: 0x00361BFD File Offset: 0x0035FDFD
		internal void SetAutoLayoutDefaults()
		{
			this.SaveDimensions();
			this.Position.Copy = this.Position;
			this.Position.SetPositionForAutoLayout();
			this.Dimensions.Margins.Reset(DefaultValues.AUTO_MARGIN_DATATABLE);
		}

		// Token: 0x0600ED38 RID: 60728 RVA: 0x00361C36 File Offset: 0x0035FE36
		internal void RestoreDimensions()
		{
			this.Dimensions.SetDimensions(this.Dimensions.Copy);
		}

		// Token: 0x0600ED39 RID: 60729 RVA: 0x00361C4E File Offset: 0x0035FE4E
		internal void RestoreInitialValues()
		{
			this.Position.AlignedPosition = this.Position.Copy.AlignedPosition;
			this.Dimensions.Margins.CopyFrom(this.Dimensions.Copy.Margins);
		}

		// Token: 0x0600ED3A RID: 60730 RVA: 0x00361C8C File Offset: 0x0035FE8C
		public override object Clone()
		{
			StyleChartDataTable styleChartDataTable = (StyleChartDataTable)base.MemberwiseClone();
			styleChartDataTable.ViewState = base.CloneState();
			styleChartDataTable.styleChartDataTableCorners.CopyFrom(this.styleChartDataTableCorners);
			styleChartDataTable.styleChartDataTableFillStyle = (FillStyle)this.styleChartDataTableFillStyle.Clone();
			styleChartDataTable.position = (Position)this.position.Clone();
			styleChartDataTable.dimensions = (Dimensions)this.dimensions.Clone();
			styleChartDataTable.styleBorder = (StyleDataTableBorder)this.styleBorder.Clone();
			styleChartDataTable.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleChartDataTable.styleChartDataTableTextProperties = (TextProperties)this.styleChartDataTableTextProperties.Clone();
			return styleChartDataTable;
		}

		// Token: 0x0600ED3B RID: 60731 RVA: 0x00361D47 File Offset: 0x0035FF47
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleChartDataTableFillStyle).TrackViewState();
			((IChartingStateManager)this.styleChartDataTableTextProperties).TrackViewState();
		}

		// Token: 0x0600ED3C RID: 60732 RVA: 0x00361D68 File Offset: 0x0035FF68
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleChartDataTableFillStyle).LoadViewState(array[1]);
				((IChartingStateManager)this.styleChartDataTableTextProperties).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600ED3D RID: 60733 RVA: 0x00361DA4 File Offset: 0x0035FFA4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleChartDataTableFillStyle).SaveViewState(),
				((IChartingStateManager)this.styleChartDataTableTextProperties).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044A6 RID: 17574
		private FillStyle styleChartDataTableFillStyle;

		// Token: 0x040044A7 RID: 17575
		private TextProperties styleChartDataTableTextProperties;

		// Token: 0x040044A8 RID: 17576
		private Corners styleChartDataTableCorners;

		// Token: 0x040044A9 RID: 17577
		private ChartDataTable styleChartDataTableParent;
	}
}
