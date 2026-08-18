using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200173F RID: 5951
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("YValue")]
	[Description("A series data item.")]
	public class ChartSeriesItem : RenderedObject, IActiveRegion, ICloneable
	{
		// Token: 0x170046A7 RID: 18087
		// (get) Token: 0x0600E838 RID: 59448 RVA: 0x00340EB4 File Offset: 0x0033F0B4
		// (set) Token: 0x0600E839 RID: 59449 RVA: 0x00340EC1 File Offset: 0x0033F0C1
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Visible
		{
			get
			{
				return this.chartSeriesItemAppearance.Visible;
			}
			set
			{
				this.chartSeriesItemAppearance.Visible = value;
			}
		}

		// Token: 0x170046A8 RID: 18088
		// (get) Token: 0x0600E83A RID: 59450 RVA: 0x00340ECF File Offset: 0x0033F0CF
		// (set) Token: 0x0600E83B RID: 59451 RVA: 0x00340ED7 File Offset: 0x0033F0D7
		[Browsable(false)]
		internal double RelativeValue
		{
			get
			{
				return this.chartSeriesItemRelativeValue;
			}
			set
			{
				this.chartSeriesItemRelativeValue = value;
			}
		}

		// Token: 0x170046A9 RID: 18089
		// (get) Token: 0x0600E83C RID: 59452 RVA: 0x00340EE0 File Offset: 0x0033F0E0
		// (set) Token: 0x0600E83D RID: 59453 RVA: 0x00340EE8 File Offset: 0x0033F0E8
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		public ActiveRegion ActiveRegion
		{
			get
			{
				return this.chartSeriesItemActiveRegion;
			}
			set
			{
				this.chartSeriesItemActiveRegion = value;
			}
		}

		// Token: 0x170046AA RID: 18090
		// (get) Token: 0x0600E83E RID: 59454 RVA: 0x00340EF1 File Offset: 0x0033F0F1
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleSeriesItem Appearance
		{
			get
			{
				return this.chartSeriesItemAppearance;
			}
		}

		// Token: 0x170046AB RID: 18091
		// (get) Token: 0x0600E83F RID: 59455 RVA: 0x00340EF9 File Offset: 0x0033F0F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public SeriesItemLabel Label
		{
			get
			{
				return this.chartSeriesItemLabel;
			}
		}

		// Token: 0x170046AC RID: 18092
		// (get) Token: 0x0600E840 RID: 59456 RVA: 0x00340F01 File Offset: 0x0033F101
		// (set) Token: 0x0600E841 RID: 59457 RVA: 0x00340F0C File Offset: 0x0033F10C
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ChartSeries Parent
		{
			get
			{
				return this.chartSeriesItemParent;
			}
			set
			{
				this.chartSeriesItemParent = value;
				if (this.chartSeriesItemParent != null && this.chartSeriesItemParent.Chart != null)
				{
					this.chartSeriesItemAppearance.styleChart = this.chartSeriesItemParent.Chart;
					this.chartSeriesItemLabel.Appearance.styleChart = this.chartSeriesItemParent.Chart;
					this.chartSeriesItemLabel.Marker.Appearance.styleChart = this.chartSeriesItemParent.Chart;
				}
			}
		}

		// Token: 0x170046AD RID: 18093
		// (get) Token: 0x0600E842 RID: 59458 RVA: 0x00340F86 File Offset: 0x0033F186
		// (set) Token: 0x0600E843 RID: 59459 RVA: 0x00340FA7 File Offset: 0x0033F1A7
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Category("Basic value set")]
		[DefaultValue(false)]
		public bool Empty
		{
			get
			{
				return (bool)(base.ViewState["Empty"] ?? false);
			}
			set
			{
				base.ViewState["Empty"] = value;
			}
		}

		// Token: 0x170046AE RID: 18094
		// (get) Token: 0x0600E844 RID: 59460 RVA: 0x00340FBF File Offset: 0x0033F1BF
		// (set) Token: 0x0600E845 RID: 59461 RVA: 0x00340FE8 File Offset: 0x0033F1E8
		[TypeConverter(typeof(DoubleConverter2))]
		[DefaultValue(double.NaN)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Category("Basic value set")]
		[NotifyParentProperty(true)]
		public double XValue
		{
			get
			{
				return (double)(base.ViewState["XValue"] ?? double.NaN);
			}
			set
			{
				base.ViewState["XValue"] = value;
			}
		}

		// Token: 0x170046AF RID: 18095
		// (get) Token: 0x0600E846 RID: 59462 RVA: 0x00341000 File Offset: 0x0033F200
		// (set) Token: 0x0600E847 RID: 59463 RVA: 0x00341029 File Offset: 0x0033F229
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Category("Basic value set")]
		[TypeConverter(typeof(DoubleConverter2))]
		[DefaultValue(typeof(double), "NaN")]
		[Browsable(true)]
		public double XValue2
		{
			get
			{
				return (double)(base.ViewState["XValue2"] ?? double.NaN);
			}
			set
			{
				base.ViewState["XValue2"] = value;
			}
		}

		// Token: 0x170046B0 RID: 18096
		// (get) Token: 0x0600E848 RID: 59464 RVA: 0x00341041 File Offset: 0x0033F241
		// (set) Token: 0x0600E849 RID: 59465 RVA: 0x0034106A File Offset: 0x0033F26A
		[DefaultValue(typeof(double), "0")]
		[Description("Y value of the data series item.")]
		[Category("Basic value set")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(DoubleConverter2))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Bindable(true)]
		public double YValue
		{
			get
			{
				return (double)(base.ViewState["YValue"] ?? 0.0);
			}
			set
			{
				base.ViewState["YValue"] = value;
			}
		}

		// Token: 0x170046B1 RID: 18097
		// (get) Token: 0x0600E84A RID: 59466 RVA: 0x00341082 File Offset: 0x0033F282
		// (set) Token: 0x0600E84B RID: 59467 RVA: 0x003410AB File Offset: 0x0033F2AB
		[Category("Basic value set")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(DoubleConverter2))]
		[DefaultValue(typeof(double), "NaN")]
		public double YValue2
		{
			get
			{
				return (double)(base.ViewState["YValue2"] ?? double.NaN);
			}
			set
			{
				base.ViewState["YValue2"] = value;
			}
		}

		// Token: 0x170046B2 RID: 18098
		// (get) Token: 0x0600E84C RID: 59468 RVA: 0x003410C3 File Offset: 0x0033F2C3
		// (set) Token: 0x0600E84D RID: 59469 RVA: 0x003410EC File Offset: 0x0033F2EC
		[DefaultValue(typeof(double), "NaN")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[NotifyParentProperty(true)]
		[Category("Basic value set")]
		[TypeConverter(typeof(DoubleConverter2))]
		public double YValue3
		{
			get
			{
				return (double)(base.ViewState["YValue3"] ?? double.NaN);
			}
			set
			{
				base.ViewState["YValue3"] = value;
			}
		}

		// Token: 0x170046B3 RID: 18099
		// (get) Token: 0x0600E84E RID: 59470 RVA: 0x00341104 File Offset: 0x0033F304
		// (set) Token: 0x0600E84F RID: 59471 RVA: 0x0034112D File Offset: 0x0033F32D
		[Category("Basic value set")]
		[DefaultValue(typeof(double), "NaN")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(DoubleConverter2))]
		public double YValue4
		{
			get
			{
				return (double)(base.ViewState["YValue4"] ?? double.NaN);
			}
			set
			{
				base.ViewState["YValue4"] = value;
			}
		}

		// Token: 0x170046B4 RID: 18100
		internal double this[string valueTypeName]
		{
			get
			{
				switch (valueTypeName)
				{
				case "XValue":
					return this.XValue;
				case "XValue2":
					return this.XValue2;
				case "YValue2":
					return this.YValue2;
				case "YValue3":
					return this.YValue3;
				case "YValue4":
					return this.YValue4;
				}
				return this.YValue;
			}
		}

		// Token: 0x170046B5 RID: 18101
		// (get) Token: 0x0600E851 RID: 59473 RVA: 0x00341215 File Offset: 0x0033F415
		// (set) Token: 0x0600E852 RID: 59474 RVA: 0x00341235 File Offset: 0x0033F435
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170046B6 RID: 18102
		// (get) Token: 0x0600E853 RID: 59475 RVA: 0x00341248 File Offset: 0x0033F448
		[Browsable(true)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public StyleMarkerSeriesPoint PointAppearance
		{
			get
			{
				return this.chartSeriesItemPointAppearance;
			}
		}

		// Token: 0x170046B7 RID: 18103
		// (get) Token: 0x0600E854 RID: 59476 RVA: 0x00341250 File Offset: 0x0033F450
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.Parent.Items.IndexOf(this);
			}
		}

		// Token: 0x0600E855 RID: 59477 RVA: 0x00341264 File Offset: 0x0033F464
		public ChartSeriesItem(double x, double y, StyleSeriesItem style) : this(x, y)
		{
			this.chartSeriesItemLabel.appearance = (StyleSeriesItemLabel)style.Clone();
			this.chartSeriesItemLabel.Appearance.styleChart = this.Parent.Chart;
			this.chartSeriesItemLabel.TextBlock.Text = y.ToString();
			this.haveRealXValue = true;
		}

		// Token: 0x0600E856 RID: 59478 RVA: 0x003412C8 File Offset: 0x0033F4C8
		public ChartSeriesItem(double x, double y) : this(y)
		{
			this.XValue = x;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E857 RID: 59479 RVA: 0x003412DF File Offset: 0x0033F4DF
		public ChartSeriesItem(double x, double y, double x2, double y2) : this(x, y)
		{
			this.XValue2 = x2;
			this.YValue2 = y2;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E858 RID: 59480 RVA: 0x003412FF File Offset: 0x0033F4FF
		public ChartSeriesItem(double x, double y, double x2, double y2, double y3, double y4) : this(x, y, x2, y2)
		{
			this.YValue3 = y3;
			this.YValue4 = y4;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E859 RID: 59481 RVA: 0x00341324 File Offset: 0x0033F524
		public ChartSeriesItem() : base(null)
		{
			this.chartSeriesItemActiveRegion = new ActiveRegion(this);
			this.chartSeriesItemAppearance = new StyleSeriesItem();
			this.chartSeriesItemLabel = new SeriesItemLabel();
			this.chartSeriesItemPointAppearance = new StyleMarkerSeriesPoint();
			this.haveRealXValue = true;
			this.isAutoGenerateText = false;
		}

		// Token: 0x0600E85A RID: 59482 RVA: 0x00341373 File Offset: 0x0033F573
		public ChartSeriesItem(ChartSeries parent) : this()
		{
			this.chartSeriesItemParent = parent;
			this.chartSeriesItemLabel.Appearance.styleChart = parent.Chart;
			this.chartSeriesItemLabel = new SeriesItemLabel();
			this.haveRealXValue = true;
		}

		// Token: 0x0600E85B RID: 59483 RVA: 0x003413AA File Offset: 0x0033F5AA
		public ChartSeriesItem(bool isEmpty) : this()
		{
			this.Empty = isEmpty;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E85C RID: 59484 RVA: 0x003413C0 File Offset: 0x0033F5C0
		public ChartSeriesItem(double value) : this()
		{
			this.YValue = value;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E85D RID: 59485 RVA: 0x003413D6 File Offset: 0x0033F5D6
		public ChartSeriesItem(double value, string labelText) : this(value)
		{
			this.chartSeriesItemLabel.TextBlock.Text = labelText;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E85E RID: 59486 RVA: 0x003413F7 File Offset: 0x0033F5F7
		public ChartSeriesItem(double value, string label, Color color) : this(value, label)
		{
			this.Appearance.FillStyle.MainColor = color;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E85F RID: 59487 RVA: 0x00341419 File Offset: 0x0033F619
		public ChartSeriesItem(double value, string label, Color color, bool exploded) : this(value, label, color)
		{
			this.Appearance.Exploded = exploded;
			this.haveRealXValue = true;
		}

		// Token: 0x0600E860 RID: 59488 RVA: 0x00341438 File Offset: 0x0033F638
		internal void DefineLabelText()
		{
			this.isAutoGenerateText = string.IsNullOrEmpty(this.Label.TextBlock.Text);
			this.Label.TextBlock.Text = this.Parent.GetItemLabel(this);
		}

		// Token: 0x0600E861 RID: 59489 RVA: 0x00341471 File Offset: 0x0033F671
		internal void ClearAutoGeneratedLabelText()
		{
			if (this.isAutoGenerateText)
			{
				this.Label.TextBlock.Text = string.Empty;
			}
		}

		// Token: 0x0600E862 RID: 59490 RVA: 0x00341490 File Offset: 0x0033F690
		internal float GetXValue()
		{
			if (double.IsNaN(this.XValue))
			{
				return 0f;
			}
			return (float)this.XValue;
		}

		// Token: 0x0600E863 RID: 59491 RVA: 0x003414AC File Offset: 0x0033F6AC
		internal SeriesItemLabel AddLabel(string text, RectangleF rect, RenderEngine engine)
		{
			ChartPlotArea plotArea = this.Parent.Chart.PlotArea;
			if (!this.chartSeriesItemLabel.IsVisible(this.Parent))
			{
				return null;
			}
			SeriesItemLabel seriesItemLabel = new SeriesItemLabel();
			ChartSeriesType type = this.Parent.Type;
			SeriesItemLabel seriesItemLabel2 = new SeriesItemLabel();
			seriesItemLabel2.TextBlock.Text = text;
			seriesItemLabel2.appearance = (StyleSeriesItemLabel)this.Label.Appearance.Clone();
			seriesItemLabel2.Appearance.styleContainerObject = null;
			seriesItemLabel2.TextBlock.appearance = (StyleTextBlock)this.Label.TextBlock.Appearance.Clone();
			seriesItemLabel2.Marker.CopyFrom(this.Label.Marker);
			seriesItemLabel2.Marker.Parent = seriesItemLabel2;
			seriesItemLabel2.Marker.Container = seriesItemLabel2;
			seriesItemLabel2.ActiveRegion = this.Label.ActiveRegion;
			seriesItemLabel2.Container = (this.Label.Container = plotArea);
			StyleSeriesItemLabel appearance = seriesItemLabel2.Appearance;
			if (appearance.Border.Color.Equals(seriesItemLabel.Appearance.Border.Color))
			{
				appearance.styleBorder.Color = this.Parent.Appearance.LabelAppearance.Border.Color;
			}
			if (appearance.Border.PenStyle.Equals(seriesItemLabel.Appearance.Border.PenStyle))
			{
				appearance.styleBorder.PenStyle = this.Parent.Appearance.LabelAppearance.Border.PenStyle;
			}
			if (appearance.Border.Width == seriesItemLabel.Appearance.Border.Width)
			{
				appearance.styleBorder.Width = this.Parent.Appearance.LabelAppearance.Border.Width;
			}
			if (appearance.Border.Visible == seriesItemLabel.Appearance.Border.Visible)
			{
				appearance.styleBorder.Visible = this.Parent.Appearance.LabelAppearance.Border.Visible;
			}
			if (appearance.Corners.Equals(seriesItemLabel.Appearance.Corners))
			{
				appearance.Corners.CopyFrom(this.Parent.Appearance.LabelAppearance.Corners);
			}
			if (appearance.Dimensions.Equals(seriesItemLabel.Appearance.Dimensions))
			{
				appearance.dimensions = (Dimensions)this.Parent.Appearance.LabelAppearance.Dimensions.Clone();
			}
			if (appearance.Figure == seriesItemLabel.Appearance.Figure)
			{
				appearance.Figure = this.Parent.Appearance.LabelAppearance.Figure;
			}
			if (appearance.FillStyle.Equals(seriesItemLabel.Appearance.FillStyle))
			{
				appearance.styleLabelFillStyle = (FillStyle)this.Parent.Appearance.LabelAppearance.FillStyle.Clone();
			}
			if (appearance.Position.AlignedPosition.Equals(seriesItemLabel.Appearance.Position.AlignedPosition))
			{
				appearance.position.AlignedPosition = this.Parent.Appearance.LabelAppearance.Position.AlignedPosition;
			}
			if (appearance.Position.Auto == seriesItemLabel.Appearance.Position.Auto)
			{
				appearance.position.Auto = this.Parent.Appearance.LabelAppearance.Position.Auto;
			}
			if (appearance.Position.X == seriesItemLabel.Appearance.Position.X)
			{
				appearance.position.X = this.Parent.Appearance.LabelAppearance.Position.X;
			}
			if (appearance.Position.Y == seriesItemLabel.Appearance.Position.Y)
			{
				appearance.position.Y = this.Parent.Appearance.LabelAppearance.Position.Y;
			}
			if (appearance.Visible == seriesItemLabel.Appearance.Visible)
			{
				appearance.Visible = this.Parent.Appearance.LabelAppearance.Visible;
			}
			if (appearance.RotationAngle == seriesItemLabel.Appearance.RotationAngle)
			{
				appearance.RotationAngle = this.Parent.Appearance.LabelAppearance.RotationAngle;
			}
			if (appearance.LabelLocation == seriesItemLabel.Appearance.LabelLocation)
			{
				appearance.LabelLocation = this.Parent.Appearance.LabelAppearance.LabelLocation;
			}
			if (appearance.Distance == seriesItemLabel.Appearance.Distance)
			{
				appearance.Distance = this.Parent.Appearance.LabelAppearance.Distance;
			}
			if (appearance.Shadow.Equals(seriesItemLabel.Appearance.Shadow))
			{
				appearance.styleShadow = (ShadowStyle)this.Parent.Appearance.LabelAppearance.Shadow.Clone();
			}
			if (appearance.LabelConnectorStyle.Color.Equals(seriesItemLabel.Appearance.LabelConnectorStyle.Color))
			{
				appearance.LabelConnectorStyle.Color = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.Color;
			}
			if (appearance.LabelConnectorStyle.StartCap.Equals(seriesItemLabel.Appearance.LabelConnectorStyle.StartCap))
			{
				appearance.LabelConnectorStyle.StartCap = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.StartCap;
			}
			if (appearance.LabelConnectorStyle.EndCap.Equals(seriesItemLabel.Appearance.LabelConnectorStyle.EndCap))
			{
				appearance.LabelConnectorStyle.EndCap = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.EndCap;
			}
			if (appearance.LabelConnectorStyle.PenStyle.Equals(seriesItemLabel.Appearance.LabelConnectorStyle.PenStyle))
			{
				appearance.LabelConnectorStyle.PenStyle = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.PenStyle;
			}
			if (appearance.LabelConnectorStyle.Width == seriesItemLabel.Appearance.LabelConnectorStyle.Width)
			{
				appearance.styleSeriesItemLabelLabelConnectorStyle.Width = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.Width;
			}
			if (appearance.LabelConnectorStyle.Visible == seriesItemLabel.Appearance.LabelConnectorStyle.Visible)
			{
				appearance.LabelConnectorStyle.Visible = this.Parent.Appearance.LabelAppearance.LabelConnectorStyle.Visible;
			}
			if (seriesItemLabel2.TextBlock.Appearance.Border.Color.Equals(seriesItemLabel.TextBlock.Appearance.Border.Color))
			{
				seriesItemLabel2.TextBlock.Appearance.styleBorder.Color = this.Parent.Appearance.TextAppearance.Border.Color;
			}
			if (seriesItemLabel2.TextBlock.Appearance.Border.PenStyle.Equals(seriesItemLabel.TextBlock.Appearance.Border.PenStyle))
			{
				seriesItemLabel2.TextBlock.Appearance.styleBorder.PenStyle = this.Parent.Appearance.TextAppearance.Border.PenStyle;
			}
			if (seriesItemLabel2.TextBlock.Appearance.Border.Width == seriesItemLabel.TextBlock.Appearance.Border.Width)
			{
				seriesItemLabel2.TextBlock.Appearance.styleBorder.Width = this.Parent.Appearance.TextAppearance.Border.Width;
			}
			if (seriesItemLabel2.TextBlock.Appearance.Border.Visible == seriesItemLabel.TextBlock.Appearance.Border.Visible)
			{
				seriesItemLabel2.TextBlock.Appearance.styleBorder.Visible = this.Parent.Appearance.TextAppearance.Border.Visible;
			}
			if (seriesItemLabel2.TextBlock.Appearance.Corners.Equals(seriesItemLabel.TextBlock.Appearance.Corners))
			{
				seriesItemLabel2.TextBlock.Appearance.Corners.CopyFrom(this.Parent.Appearance.TextAppearance.Corners);
			}
			if (seriesItemLabel2.TextBlock.Appearance.Dimensions.Equals(seriesItemLabel.TextBlock.Appearance.Dimensions))
			{
				seriesItemLabel2.TextBlock.Appearance.dimensions = (Dimensions)this.Parent.Appearance.TextAppearance.Dimensions.Clone();
			}
			if (seriesItemLabel2.TextBlock.Appearance.FillStyle.Equals(seriesItemLabel.TextBlock.Appearance.FillStyle))
			{
				seriesItemLabel2.TextBlock.Appearance.styleTextBlockFillStyle = (FillStyle)this.Parent.Appearance.TextAppearance.FillStyle.Clone();
			}
			if (seriesItemLabel2.TextBlock.Appearance.Position.Equals(seriesItemLabel.TextBlock.Appearance.Position))
			{
				seriesItemLabel2.TextBlock.Appearance.position = (Position)this.Parent.Appearance.TextAppearance.Position.Clone();
			}
			if (seriesItemLabel2.TextBlock.Visible == seriesItemLabel.TextBlock.Visible)
			{
				seriesItemLabel2.TextBlock.Visible = this.Parent.Appearance.TextAppearance.Visible;
			}
			if (seriesItemLabel2.TextBlock.Appearance.MaxLength == seriesItemLabel.TextBlock.Appearance.MaxLength)
			{
				seriesItemLabel2.TextBlock.Appearance.MaxLength = this.Parent.Appearance.TextAppearance.MaxLength;
			}
			TextPropertiesSeriesItem textPropertiesSeriesItem = new TextPropertiesSeriesItem();
			if (seriesItemLabel2.TextBlock.Appearance.TextProperties.Color.Equals(textPropertiesSeriesItem.Color))
			{
				seriesItemLabel2.TextBlock.Appearance.TextProperties.Color = this.Parent.Appearance.TextAppearance.TextProperties.Color;
			}
			if (seriesItemLabel2.TextBlock.Appearance.TextProperties.Font.Equals(textPropertiesSeriesItem.Font))
			{
				seriesItemLabel2.TextBlock.Appearance.TextProperties.Font = this.Parent.Appearance.TextAppearance.TextProperties.Font;
			}
			seriesItemLabel2.CalculateLayout(new PointF(rect.Left + rect.Width / 2f, rect.Location.Y), new PointF(rect.Left + rect.Width / 2f, rect.Top + rect.Height / 2f), this.Parent.Appearance.ShowLabelConnectors, engine);
			bool flag = appearance.LabelLocation == StyleSeriesItemLabel.ItemLabelLocation.Auto;
			if (flag)
			{
				this.SetLabelAutoPosition(ref seriesItemLabel2, plotArea.Chart.SeriesOrientation);
			}
			switch (type)
			{
			case ChartSeriesType.Bar:
			case ChartSeriesType.StackedBar:
			case ChartSeriesType.StackedBar100:
			case ChartSeriesType.Gantt:
			case ChartSeriesType.Bubble:
			case ChartSeriesType.Point:
			case ChartSeriesType.CandleStick:
				switch (appearance.LabelLocation)
				{
				case StyleSeriesItemLabel.ItemLabelLocation.Inside:
					seriesItemLabel2.SetInsideCoordinates(rect);
					break;
				case StyleSeriesItemLabel.ItemLabelLocation.Outside:
					seriesItemLabel2.SetOutsideCoordinates(rect, flag);
					break;
				}
				break;
			case ChartSeriesType.Line:
			case ChartSeriesType.Area:
			case ChartSeriesType.StackedArea:
			case ChartSeriesType.StackedArea100:
			case ChartSeriesType.Bezier:
			case ChartSeriesType.Spline:
			case ChartSeriesType.SplineArea:
			case ChartSeriesType.StackedSplineArea:
			case ChartSeriesType.StackedSplineArea100:
			case ChartSeriesType.StackedLine:
			case ChartSeriesType.StackedSpline:
				seriesItemLabel2.SetOutsideCoordinates(rect, flag);
				break;
			}
			seriesItemLabel2.Adjust(plotArea);
			if (this.IsVisible(rect))
			{
				plotArea.SeriesLabels.Add(seriesItemLabel2);
			}
			return seriesItemLabel2;
		}

		// Token: 0x0600E864 RID: 59492 RVA: 0x00342124 File Offset: 0x00340324
		private void SetLabelAutoPosition(ref SeriesItemLabel label, ChartSeriesOrientation chartSeriesOrientation)
		{
			double num = this.YValue;
			ChartSeriesType type = this.Parent.Type;
			if (type == ChartSeriesType.Pie)
			{
				num = Math.Abs(num);
			}
			label.Appearance.LabelLocation = StyleSeriesItemLabel.ItemLabelLocation.Outside;
			switch (chartSeriesOrientation)
			{
			case ChartSeriesOrientation.Vertical:
				if (num < 0.0)
				{
					label.Appearance.Position.AlignedPosition = AlignedPositions.Bottom;
					return;
				}
				label.Appearance.Position.AlignedPosition = AlignedPositions.Top;
				return;
			case ChartSeriesOrientation.Horizontal:
				if (num < 0.0)
				{
					label.Appearance.Position.AlignedPosition = AlignedPositions.Left;
					return;
				}
				label.Appearance.Position.AlignedPosition = AlignedPositions.Right;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600E865 RID: 59493 RVA: 0x003421D8 File Offset: 0x003403D8
		private bool IsVisible(RectangleF rect)
		{
			ChartPlotArea plotArea = this.Parent.Chart.PlotArea;
			return ((double)(rect.X + rect.Width) > Math.Round((double)plotArea.Appearance.Position.X) && (double)rect.X < Math.Round((double)(plotArea.Appearance.Position.X + plotArea.Appearance.Dimensions.Width.PixelValue)) && (double)(rect.Y + rect.Height) > Math.Round((double)plotArea.Appearance.Position.Y) && (double)rect.Y <= Math.Round((double)(plotArea.Appearance.Position.Y + plotArea.Appearance.Dimensions.Height.PixelValue))) || (this.YValue == 0.0 && ((double)rect.X == Math.Round((double)plotArea.Appearance.Position.X) || (double)rect.Y == Math.Round((double)plotArea.Appearance.Position.Y) || (double)rect.X == Math.Round((double)(plotArea.Appearance.Position.X + plotArea.Appearance.Dimensions.Width.PixelValue)) || (double)rect.Y == Math.Round((double)(plotArea.Appearance.Position.Y + plotArea.Appearance.Dimensions.Height.PixelValue))));
		}

		// Token: 0x0600E866 RID: 59494 RVA: 0x00342381 File Offset: 0x00340581
		public override string ToString()
		{
			return "Item";
		}

		// Token: 0x0600E867 RID: 59495 RVA: 0x00342388 File Offset: 0x00340588
		protected override void Dispose(bool disposing)
		{
			if (this.chartSeriesItemActiveRegion != null)
			{
				this.chartSeriesItemActiveRegion.Dispose();
				this.chartSeriesItemActiveRegion = null;
			}
			if (this.chartSeriesItemAppearance != null)
			{
				this.chartSeriesItemAppearance.Dispose();
				this.chartSeriesItemAppearance = null;
			}
			if (this.chartSeriesItemLabel != null)
			{
				this.chartSeriesItemLabel.Dispose();
				this.chartSeriesItemLabel = null;
			}
			if (this.chartSeriesItemPointAppearance != null)
			{
				this.chartSeriesItemPointAppearance.Dispose();
				this.chartSeriesItemPointAppearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600E868 RID: 59496 RVA: 0x00342404 File Offset: 0x00340604
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartSeriesItemActiveRegion).TrackViewState();
			((IChartingStateManager)this.chartSeriesItemAppearance).TrackViewState();
			((IChartingStateManager)this.chartSeriesItemLabel).TrackViewState();
			((IChartingStateManager)this.chartSeriesItemPointAppearance).TrackViewState();
		}

		// Token: 0x0600E869 RID: 59497 RVA: 0x00342438 File Offset: 0x00340638
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartSeriesItemActiveRegion).LoadViewState(array[1]);
				((IChartingStateManager)this.chartSeriesItemAppearance).LoadViewState(array[2]);
				((IChartingStateManager)this.chartSeriesItemLabel).LoadViewState(array[3]);
				((IChartingStateManager)this.chartSeriesItemPointAppearance).LoadViewState(array[4]);
			}
		}

		// Token: 0x0600E86A RID: 59498 RVA: 0x00342490 File Offset: 0x00340690
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartSeriesItemActiveRegion).SaveViewState(),
				((IChartingStateManager)this.chartSeriesItemAppearance).SaveViewState(),
				((IChartingStateManager)this.chartSeriesItemLabel).SaveViewState(),
				((IChartingStateManager)this.chartSeriesItemPointAppearance).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E86B RID: 59499 RVA: 0x00342500 File Offset: 0x00340700
		public object Clone()
		{
			ChartSeriesItem chartSeriesItem = (ChartSeriesItem)base.MemberwiseClone();
			chartSeriesItem.ViewState = base.CloneState();
			chartSeriesItem.chartSeriesItemAppearance = (StyleSeriesItem)this.Appearance.Clone();
			chartSeriesItem.chartSeriesItemLabel = (SeriesItemLabel)this.chartSeriesItemLabel.Clone();
			chartSeriesItem.Parent = this.Parent;
			chartSeriesItem.chartSeriesItemPointAppearance = (StyleMarkerSeriesPoint)this.PointAppearance.Clone();
			chartSeriesItem.chartSeriesItemPointAppearance.styleChart = this.chartSeriesItemPointAppearance.styleChart;
			return chartSeriesItem;
		}

		// Token: 0x04004298 RID: 17048
		internal StyleSeriesItem chartSeriesItemAppearance;

		// Token: 0x04004299 RID: 17049
		internal StyleMarkerSeriesPoint chartSeriesItemPointAppearance;

		// Token: 0x0400429A RID: 17050
		internal SeriesItemLabel chartSeriesItemLabel;

		// Token: 0x0400429B RID: 17051
		private ChartSeries chartSeriesItemParent;

		// Token: 0x0400429C RID: 17052
		private double chartSeriesItemRelativeValue;

		// Token: 0x0400429D RID: 17053
		internal ActiveRegion chartSeriesItemActiveRegion;

		// Token: 0x0400429E RID: 17054
		internal bool haveRealXValue;

		// Token: 0x0400429F RID: 17055
		private bool isAutoGenerateText;
	}
}
