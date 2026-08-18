using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200173A RID: 5946
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("Items")]
	public class ChartSeries : RenderedObject, ICloneable
	{
		// Token: 0x1700467A RID: 18042
		// (get) Token: 0x0600E794 RID: 59284 RVA: 0x0033CD34 File Offset: 0x0033AF34
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsActiveRegionSet
		{
			get
			{
				return this.chartSeriesIsActiveRegionSet;
			}
		}

		// Token: 0x1700467B RID: 18043
		// (get) Token: 0x0600E795 RID: 59285 RVA: 0x0033CD3C File Offset: 0x0033AF3C
		// (set) Token: 0x0600E796 RID: 59286 RVA: 0x0033CD5C File Offset: 0x0033AF5C
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("ActiveRegion")]
		public string ActiveRegionAttributes
		{
			get
			{
				return (string)(base.ViewState["ActiveRegionAttributes"] ?? "");
			}
			set
			{
				base.ViewState["ActiveRegionAttributes"] = value;
				this.chartSeriesIsActiveRegionSet = true;
				this.ResetActiveRegionForItems();
			}
		}

		// Token: 0x1700467C RID: 18044
		// (get) Token: 0x0600E797 RID: 59287 RVA: 0x0033CD7C File Offset: 0x0033AF7C
		// (set) Token: 0x0600E798 RID: 59288 RVA: 0x0033CD9C File Offset: 0x0033AF9C
		[Category("ActiveRegion")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public string ActiveRegionToolTip
		{
			get
			{
				return (string)(base.ViewState["ActiveRegionToolTip"] ?? "");
			}
			set
			{
				base.ViewState["ActiveRegionToolTip"] = value;
				this.chartSeriesIsActiveRegionSet = true;
				this.ResetActiveRegionForItems();
			}
		}

		// Token: 0x1700467D RID: 18045
		// (get) Token: 0x0600E799 RID: 59289 RVA: 0x0033CDBC File Offset: 0x0033AFBC
		// (set) Token: 0x0600E79A RID: 59290 RVA: 0x0033CDDC File Offset: 0x0033AFDC
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("ActiveRegion")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string ActiveRegionUrl
		{
			get
			{
				return (string)(base.ViewState["ActiveRegionUrl"] ?? "");
			}
			set
			{
				base.ViewState["ActiveRegionUrl"] = value;
				this.chartSeriesIsActiveRegionSet = true;
				this.ResetActiveRegionForItems();
			}
		}

		// Token: 0x1700467E RID: 18046
		// (get) Token: 0x0600E79B RID: 59291 RVA: 0x0033CDFC File Offset: 0x0033AFFC
		// (set) Token: 0x0600E79C RID: 59292 RVA: 0x0033CE09 File Offset: 0x0033B009
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Visible
		{
			get
			{
				return this.chartSeriesAppearance.Visible;
			}
			set
			{
				this.chartSeriesAppearance.Visible = value;
			}
		}

		// Token: 0x1700467F RID: 18047
		// (get) Token: 0x0600E79D RID: 59293 RVA: 0x0033CE17 File Offset: 0x0033B017
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		public StyleSeries Appearance
		{
			get
			{
				return this.chartSeriesAppearance;
			}
		}

		// Token: 0x17004680 RID: 18048
		// (get) Token: 0x0600E79E RID: 59294 RVA: 0x0033CE1F File Offset: 0x0033B01F
		// (set) Token: 0x0600E79F RID: 59295 RVA: 0x0033CE40 File Offset: 0x0033B040
		[Category("Series")]
		[DefaultValue(ChartSeriesType.Bar)]
		[NotifyParentProperty(true)]
		[Description("Specifies the type of the data series.")]
		public ChartSeriesType Type
		{
			get
			{
				return (ChartSeriesType)(base.ViewState["Type"] ?? ChartSeriesType.Bar);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17004681 RID: 18049
		// (get) Token: 0x0600E7A0 RID: 59296 RVA: 0x0033CE58 File Offset: 0x0033B058
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public ChartPlotArea PlotArea
		{
			get
			{
				return this.chartSeriesPlotArea;
			}
		}

		// Token: 0x17004682 RID: 18050
		// (get) Token: 0x0600E7A1 RID: 59297 RVA: 0x0033CE60 File Offset: 0x0033B060
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ChartSeriesCollection Parent
		{
			get
			{
				return this.chartSeriesParent;
			}
		}

		// Token: 0x0600E7A2 RID: 59298 RVA: 0x0033CE68 File Offset: 0x0033B068
		internal void SetParent(ChartSeriesCollection parent)
		{
			this.chartSeriesParent = parent;
		}

		// Token: 0x17004683 RID: 18051
		// (get) Token: 0x0600E7A3 RID: 59299 RVA: 0x0033CE71 File Offset: 0x0033B071
		internal Chart Chart
		{
			get
			{
				if (this.chartSeriesParent != null)
				{
					return this.chartSeriesParent.Parent;
				}
				return null;
			}
		}

		// Token: 0x17004684 RID: 18052
		// (get) Token: 0x0600E7A4 RID: 59300 RVA: 0x0033CE88 File Offset: 0x0033B088
		// (set) Token: 0x0600E7A5 RID: 59301 RVA: 0x0033CEA8 File Offset: 0x0033B0A8
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue("")]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series X-value")]
		[Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public string DataXColumn
		{
			get
			{
				return (string)(base.ViewState["DataXColumn"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataXColumn"] = value;
				}
			}
		}

		// Token: 0x17004685 RID: 18053
		// (get) Token: 0x0600E7A6 RID: 59302 RVA: 0x0033CEC9 File Offset: 0x0033B0C9
		// (set) Token: 0x0600E7A7 RID: 59303 RVA: 0x0033CEE9 File Offset: 0x0033B0E9
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series X2-value")]
		[Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Data")]
		public string DataXColumn2
		{
			get
			{
				return (string)(base.ViewState["DataXColumn2"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataXColumn2"] = value;
				}
			}
		}

		// Token: 0x17004686 RID: 18054
		// (get) Token: 0x0600E7A8 RID: 59304 RVA: 0x0033CF0A File Offset: 0x0033B10A
		// (set) Token: 0x0600E7A9 RID: 59305 RVA: 0x0033CF2A File Offset: 0x0033B12A
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Data")]
		[DefaultValue("")]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series Y-value")]
		[Editor(typeof(NumericDataColumnEditor), typeof(UITypeEditor))]
		public string DataYColumn
		{
			get
			{
				return (string)(base.ViewState["DataYColumn"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataYColumn"] = value;
				}
			}
		}

		// Token: 0x17004687 RID: 18055
		// (get) Token: 0x0600E7AA RID: 59306 RVA: 0x0033CF4B File Offset: 0x0033B14B
		// (set) Token: 0x0600E7AB RID: 59307 RVA: 0x0033CF6B File Offset: 0x0033B16B
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Data")]
		[DefaultValue("")]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series Y2-value")]
		[Editor(typeof(NumericDataColumnEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public string DataYColumn2
		{
			get
			{
				return (string)(base.ViewState["DataYColumn2"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataYColumn2"] = value;
				}
			}
		}

		// Token: 0x17004688 RID: 18056
		// (get) Token: 0x0600E7AC RID: 59308 RVA: 0x0033CF8C File Offset: 0x0033B18C
		// (set) Token: 0x0600E7AD RID: 59309 RVA: 0x0033CFAC File Offset: 0x0033B1AC
		[Editor(typeof(NumericDataColumnEditor), typeof(UITypeEditor))]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series Y3-value")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Data")]
		[DefaultValue("")]
		public string DataYColumn3
		{
			get
			{
				return (string)(base.ViewState["DataYColumn3"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataYColumn3"] = value;
				}
			}
		}

		// Token: 0x17004689 RID: 18057
		// (get) Token: 0x0600E7AE RID: 59310 RVA: 0x0033CFCD File Offset: 0x0033B1CD
		// (set) Token: 0x0600E7AF RID: 59311 RVA: 0x0033CFED File Offset: 0x0033B1ED
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Data")]
		[DefaultValue("")]
		[Description("Name of the DataSource column (member) that is used to data-bind to the series Y4-value")]
		[Editor(typeof(NumericDataColumnEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public string DataYColumn4
		{
			get
			{
				return (string)(base.ViewState["DataYColumn4"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataYColumn4"] = value;
				}
			}
		}

		// Token: 0x1700468A RID: 18058
		// (get) Token: 0x0600E7B0 RID: 59312 RVA: 0x0033D00E File Offset: 0x0033B20E
		// (set) Token: 0x0600E7B1 RID: 59313 RVA: 0x0033D02E File Offset: 0x0033B22E
		[Description("Name of the DataSource column (member) that will be used as ChartSeries names source when Y-values are taken from one column for a several chart ChartSeries")]
		[Category("Data")]
		[DefaultValue("")]
		[Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public string DataLabelsColumn
		{
			get
			{
				return (string)(base.ViewState["DataLabelsColumn"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) != 0)
				{
					base.ViewState["DataLabelsColumn"] = value;
				}
			}
		}

		// Token: 0x1700468B RID: 18059
		// (get) Token: 0x0600E7B2 RID: 59314 RVA: 0x0033D04F File Offset: 0x0033B24F
		[Category("Data")]
		[Browsable(false)]
		[DefaultValue(false)]
		public bool IsDataBound
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataLabelsColumn) || !string.IsNullOrEmpty(this.DataYColumn) || !string.IsNullOrEmpty(this.DataXColumn);
			}
		}

		// Token: 0x1700468C RID: 18060
		// (get) Token: 0x0600E7B3 RID: 59315 RVA: 0x0033D07B File Offset: 0x0033B27B
		// (set) Token: 0x0600E7B4 RID: 59316 RVA: 0x0033D09B File Offset: 0x0033B29B
		[DefaultValue("Series xx")]
		[Category("Series")]
		[Description("Specifies the series name.")]
		[NotifyParentProperty(true)]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "Series xx");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x1700468D RID: 18061
		// (get) Token: 0x0600E7B5 RID: 59317 RVA: 0x0033D0AE File Offset: 0x0033B2AE
		// (set) Token: 0x0600E7B6 RID: 59318 RVA: 0x0033D0CE File Offset: 0x0033B2CE
		[DefaultValue("#Y")]
		[Description("Specifies the default label for series items.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Category("Labels")]
		public string DefaultLabelValue
		{
			get
			{
				return (string)(base.ViewState["DefaultLabelValue"] ?? "#Y");
			}
			set
			{
				base.ViewState["DefaultLabelValue"] = value;
			}
		}

		// Token: 0x1700468E RID: 18062
		// (get) Token: 0x0600E7B7 RID: 59319 RVA: 0x0033D0E1 File Offset: 0x0033B2E1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public int Index
		{
			get
			{
				return this.chartSeriesParent.IndexOf(this);
			}
		}

		// Token: 0x1700468F RID: 18063
		// (get) Token: 0x0600E7B8 RID: 59320 RVA: 0x0033D0EF File Offset: 0x0033B2EF
		// (set) Token: 0x0600E7B9 RID: 59321 RVA: 0x0033D110 File Offset: 0x0033B310
		[Category("Series")]
		[DefaultValue(ChartYAxisType.Primary)]
		[NotifyParentProperty(true)]
		public ChartYAxisType YAxisType
		{
			get
			{
				return (ChartYAxisType)(base.ViewState["YAxisType"] ?? ChartYAxisType.Primary);
			}
			set
			{
				base.ViewState["YAxisType"] = value;
			}
		}

		// Token: 0x17004690 RID: 18064
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ChartSeriesItem this[int itemIndex]
		{
			get
			{
				return this.chartSeriesItems[itemIndex];
			}
			set
			{
				this.chartSeriesItems[itemIndex] = value;
			}
		}

		// Token: 0x17004691 RID: 18065
		// (get) Token: 0x0600E7BC RID: 59324 RVA: 0x0033D145 File Offset: 0x0033B345
		[Editor(typeof(SeriesItemsCollectionEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Data items collection.")]
		[NotifyParentProperty(true)]
		[Category("Series")]
		public ChartSeriesItemsCollection Items
		{
			get
			{
				return this.chartSeriesItems;
			}
		}

		// Token: 0x0600E7BD RID: 59325 RVA: 0x0033D14D File Offset: 0x0033B34D
		public ChartSeries() : base(null)
		{
			this.chartSeriesItems = new ChartSeriesItemsCollection(this);
			this.chartSeriesAppearance = new StyleSeries(this);
		}

		// Token: 0x0600E7BE RID: 59326 RVA: 0x0033D16E File Offset: 0x0033B36E
		public ChartSeries(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x0600E7BF RID: 59327 RVA: 0x0033D17D File Offset: 0x0033B37D
		public ChartSeries(string name, ChartSeriesType type) : this(name)
		{
			this.Type = type;
		}

		// Token: 0x0600E7C0 RID: 59328 RVA: 0x0033D190 File Offset: 0x0033B390
		public ChartSeries(string name, ChartSeriesType type, ChartSeriesCollection parent) : this(name, type)
		{
			this.Name = name;
			this.Type = type;
			this.chartSeriesParent = parent;
			this.chartSeriesPlotArea = parent.Parent.PlotArea;
			this.chartSeriesAppearance.PointMark.Chart = parent.Parent;
			this.chartSeriesAppearance.LabelAppearance.Chart = parent.Parent;
			this.chartSeriesAppearance.EmptyValue.PointMark.Chart = parent.Parent;
		}

		// Token: 0x0600E7C1 RID: 59329 RVA: 0x0033D212 File Offset: 0x0033B412
		public ChartSeries(string seriesName, ChartSeriesType chartSeriesType, ChartSeriesCollection parent, ChartYAxisType yAxisType, StyleSeries style) : this()
		{
			this.Name = seriesName;
			this.Type = chartSeriesType;
			this.chartSeriesParent = parent;
			this.YAxisType = yAxisType;
			this.chartSeriesAppearance = style;
		}

		// Token: 0x0600E7C2 RID: 59330 RVA: 0x0033D240 File Offset: 0x0033B440
		public ChartSeries(string seriesName, ChartSeriesType chartSeriesType, ChartSeriesCollection parent, ChartYAxisType yAxisType, StyleSeries style, string dataYColumn, string dataXColumn, string dataYColumn2, string dataXColumn2, string dataYColumn3, string dataYColumn4, string dataLabelsColumn) : this(seriesName, chartSeriesType, parent, yAxisType, style)
		{
			this.DataYColumn = dataYColumn;
			this.DataXColumn = dataXColumn;
			this.DataYColumn2 = dataYColumn2;
			this.DataXColumn2 = dataXColumn2;
			this.DataYColumn3 = dataYColumn3;
			this.DataYColumn4 = dataYColumn4;
			this.DataLabelsColumn = dataLabelsColumn;
		}

		// Token: 0x0600E7C3 RID: 59331 RVA: 0x0033D292 File Offset: 0x0033B492
		public ChartSeries(ChartSeriesCollection parent) : this()
		{
			this.chartSeriesParent = parent;
		}

		// Token: 0x0600E7C4 RID: 59332 RVA: 0x0033D2A4 File Offset: 0x0033B4A4
		private void ResetActiveRegionForItems()
		{
			ActiveRegion activeRegion = new ActiveRegion();
			int count = this.chartSeriesItems.Count;
			for (int i = 0; i < count; i++)
			{
				ChartSeriesItem chartSeriesItem = this.chartSeriesItems[i];
				chartSeriesItem.chartSeriesItemActiveRegion.Attributes = activeRegion.Attributes;
				chartSeriesItem.chartSeriesItemActiveRegion.Tooltip = activeRegion.Tooltip;
				chartSeriesItem.chartSeriesItemActiveRegion.Url = activeRegion.Url;
			}
		}

		// Token: 0x0600E7C5 RID: 59333 RVA: 0x0033D310 File Offset: 0x0033B510
		internal int FindItemIndex(ChartSeriesItem chartItem)
		{
			int count = this.chartSeriesItems.Count;
			for (int i = 0; i < count; i++)
			{
				ChartSeriesItem chartSeriesItem = this.chartSeriesItems[i];
				if (chartSeriesItem.Equals(chartItem))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x17004692 RID: 18066
		// (get) Token: 0x0600E7C6 RID: 59334 RVA: 0x0033D34E File Offset: 0x0033B54E
		// (set) Token: 0x0600E7C7 RID: 59335 RVA: 0x0033D36E File Offset: 0x0033B56E
		internal string LegendFormattedText
		{
			get
			{
				return (string)(base.ViewState["LegendFormattedText"] ?? "");
			}
			set
			{
				base.ViewState["LegendFormattedText"] = value;
			}
		}

		// Token: 0x17004693 RID: 18067
		// (get) Token: 0x0600E7C8 RID: 59336 RVA: 0x0033D384 File Offset: 0x0033B584
		internal bool IsScalable
		{
			get
			{
				return this.Type == ChartSeriesType.Area | this.Type == ChartSeriesType.Bubble | this.Type == ChartSeriesType.Line | this.Type == ChartSeriesType.Point | this.Type == ChartSeriesType.Spline | this.Type == ChartSeriesType.SplineArea | this.Type == ChartSeriesType.Bezier | this.Type == ChartSeriesType.StackedArea | this.Type == ChartSeriesType.StackedArea100 | this.Type == ChartSeriesType.StackedSplineArea | this.Type == ChartSeriesType.StackedSplineArea100 | this.Type == ChartSeriesType.StackedLine | this.Type == ChartSeriesType.StackedSpline | this.Type == ChartSeriesType.Bar | this.Type == ChartSeriesType.StackedBar | this.Type == ChartSeriesType.StackedBar100;
			}
		}

		// Token: 0x17004694 RID: 18068
		// (get) Token: 0x0600E7C9 RID: 59337 RVA: 0x0033D43C File Offset: 0x0033B63C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsXDependent
		{
			get
			{
				int count = this.chartSeriesItems.Count;
				for (int i = 0; i < count; i++)
				{
					if (!this.chartSeriesItems[i].XValue.Equals(double.NaN))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17004695 RID: 18069
		// (get) Token: 0x0600E7CA RID: 59338 RVA: 0x0033D488 File Offset: 0x0033B688
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsXDependentSeriesType
		{
			get
			{
				return this.Type == ChartSeriesType.Area | this.Type == ChartSeriesType.Bubble | this.Type == ChartSeriesType.Gantt | this.Type == ChartSeriesType.Line | this.Type == ChartSeriesType.Point | this.Type == ChartSeriesType.Spline | this.Type == ChartSeriesType.SplineArea | this.Type == ChartSeriesType.Bar | this.Type == ChartSeriesType.StackedBar | this.Type == ChartSeriesType.CandleStick | this.Type == ChartSeriesType.StackedBar100;
			}
		}

		// Token: 0x17004696 RID: 18070
		// (get) Token: 0x0600E7CB RID: 59339 RVA: 0x0033D508 File Offset: 0x0033B708
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal bool IsNormalStacked
		{
			get
			{
				return this.Type == ChartSeriesType.StackedArea || this.Type == ChartSeriesType.StackedSplineArea || this.Type == ChartSeriesType.StackedBar || this.Type == ChartSeriesType.StackedLine || this.Type == ChartSeriesType.StackedSpline;
			}
		}

		// Token: 0x17004697 RID: 18071
		// (get) Token: 0x0600E7CC RID: 59340 RVA: 0x0033D545 File Offset: 0x0033B745
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal bool IsStacked100
		{
			get
			{
				return this.Type == ChartSeriesType.StackedArea100 || this.Type == ChartSeriesType.StackedSplineArea100 || this.Type == ChartSeriesType.StackedBar100;
			}
		}

		// Token: 0x17004698 RID: 18072
		// (get) Token: 0x0600E7CD RID: 59341 RVA: 0x0033D56A File Offset: 0x0033B76A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal bool IsStacked
		{
			get
			{
				return this.IsNormalStacked || this.IsStacked100;
			}
		}

		// Token: 0x17004699 RID: 18073
		// (get) Token: 0x0600E7CE RID: 59342 RVA: 0x0033D57F File Offset: 0x0033B77F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsLine
		{
			get
			{
				return this.Type == ChartSeriesType.Line || this.Type == ChartSeriesType.Spline || this.Type == ChartSeriesType.Bezier;
			}
		}

		// Token: 0x1700469A RID: 18074
		// (get) Token: 0x0600E7CF RID: 59343 RVA: 0x0033D5A0 File Offset: 0x0033B7A0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsSplineArea
		{
			get
			{
				return this.Type == ChartSeriesType.SplineArea || this.Type == ChartSeriesType.StackedSplineArea || this.Type == ChartSeriesType.StackedSplineArea100;
			}
		}

		// Token: 0x1700469B RID: 18075
		// (get) Token: 0x0600E7D0 RID: 59344 RVA: 0x0033D5C2 File Offset: 0x0033B7C2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal bool IsArea
		{
			get
			{
				return this.Type == ChartSeriesType.Area || this.Type == ChartSeriesType.StackedArea || this.Type == ChartSeriesType.StackedArea100;
			}
		}

		// Token: 0x1700469C RID: 18076
		// (get) Token: 0x0600E7D1 RID: 59345 RVA: 0x0033D5E1 File Offset: 0x0033B7E1
		internal bool IsStackedLine
		{
			get
			{
				return this.Type == ChartSeriesType.StackedLine || this.Type == ChartSeriesType.StackedSpline;
			}
		}

		// Token: 0x1700469D RID: 18077
		// (get) Token: 0x0600E7D2 RID: 59346 RVA: 0x0033D5F9 File Offset: 0x0033B7F9
		internal bool IsStackedNormalArea
		{
			get
			{
				return this.Type == ChartSeriesType.StackedArea || this.Type == ChartSeriesType.StackedArea100;
			}
		}

		// Token: 0x1700469E RID: 18078
		// (get) Token: 0x0600E7D3 RID: 59347 RVA: 0x0033D60F File Offset: 0x0033B80F
		internal bool IsStackedSplineArea
		{
			get
			{
				return this.Type == ChartSeriesType.StackedSplineArea || this.Type == ChartSeriesType.StackedSplineArea100;
			}
		}

		// Token: 0x1700469F RID: 18079
		// (get) Token: 0x0600E7D4 RID: 59348 RVA: 0x0033D627 File Offset: 0x0033B827
		internal bool IsStackedArea
		{
			get
			{
				return this.IsStackedNormalArea || this.IsStackedSplineArea;
			}
		}

		// Token: 0x170046A0 RID: 18080
		// (get) Token: 0x0600E7D5 RID: 59349 RVA: 0x0033D63C File Offset: 0x0033B83C
		internal bool IsHasEmptyValues
		{
			get
			{
				foreach (ChartSeriesItem chartSeriesItem in this.Items)
				{
					if (chartSeriesItem.Empty)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600E7D6 RID: 59350 RVA: 0x0033D694 File Offset: 0x0033B894
		internal void SetFormattedLegendItemText()
		{
			string text = this.Name;
			if (!string.IsNullOrEmpty(this.Chart.DataGroupColumn))
			{
				string text2 = this.Chart.Legend.Appearance.GroupNameFormat;
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.Replace("#VALUE", text);
					ICommonDataHelper currentDataHelper = this.Chart.DataManager.CurrentDataHelper;
					if (currentDataHelper != null && currentDataHelper.ColumnNameSupported)
					{
						string columnName = currentDataHelper.GetColumnName(currentDataHelper.GetColumnIndex(this.Chart.DataGroupColumn));
						if (!string.IsNullOrEmpty(columnName))
						{
							text = text2.Replace("#NAME", columnName);
						}
					}
					else
					{
						text = text2.Replace("#NAME", string.Empty);
					}
				}
			}
			this.LegendFormattedText = text.Trim();
		}

		// Token: 0x0600E7D7 RID: 59351 RVA: 0x0033D750 File Offset: 0x0033B950
		internal double GetEmptyPointYValue(ChartSeriesItem item, int itemIndex)
		{
			return this.GetEmptyPointYValue(item, itemIndex, "YValue");
		}

		// Token: 0x0600E7D8 RID: 59352 RVA: 0x0033D760 File Offset: 0x0033B960
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal double GetEmptyPointYValue(ChartSeriesItem item, int itemIndex, string valueTypeName)
		{
			if (!item.Empty)
			{
				return 0.0;
			}
			switch (this.Appearance.EmptyValue.Mode)
			{
			case EmtyValuesMode.Zero:
				return 0.0;
			}
			double num = double.NaN;
			double num2 = double.NaN;
			for (int i = itemIndex - 1; i >= 0; i--)
			{
				if (!this[i].Empty)
				{
					num = this[i][valueTypeName];
					break;
				}
			}
			int count = this.Items.Count;
			for (int j = itemIndex + 1; j < count; j++)
			{
				if (!this[j].Empty)
				{
					num2 = this.Items[j][valueTypeName];
					break;
				}
			}
			if (num.Equals(double.NaN) && !num2.Equals(double.NaN))
			{
				return num2;
			}
			if (!num.Equals(double.NaN) && num2.Equals(double.NaN))
			{
				return num;
			}
			if (num.Equals(double.NaN) && num2.Equals(double.NaN))
			{
				return 0.0;
			}
			return (num2 + num) / 2.0;
		}

		// Token: 0x0600E7D9 RID: 59353 RVA: 0x0033D8BC File Offset: 0x0033BABC
		internal bool CheckBezierSeriesForItemsCount(ref string mess)
		{
			int count = this.Items.Count;
			if (count == 0 | (count - 1) % 3 != 0)
			{
				mess = string.Format(mess + "{0}: {1} items;\n", this.Name, count);
				return false;
			}
			return true;
		}

		// Token: 0x0600E7DA RID: 59354 RVA: 0x0033D90C File Offset: 0x0033BB0C
		internal void AddLabelsForPieSeries(PointF[] points, string[] text, double[] angles, PointF pieCenter, float pieRadius, RenderEngine renderEngine)
		{
			int num = points.Length;
			for (int i = 0; i < num; i++)
			{
				ChartSeriesItem chartSeriesItem = this.Items[i];
				if (!chartSeriesItem.Empty)
				{
					SeriesItemLabel seriesItemLabel = chartSeriesItem.AddLabel(text[i], RectangleF.Empty, renderEngine);
					if (seriesItemLabel != null)
					{
						PointF connectionPoint = points[i];
						double num2 = angles[i];
						PointF pointF = SeriesItemLabel.AdjustLabelConnectionPointForPie(num2, connectionPoint);
						seriesItemLabel.CalculateLayout(pointF, pointF, this.Appearance.ShowLabelConnectors, renderEngine);
						seriesItemLabel.Appearance.Position.X -= seriesItemLabel.Appearance.Dimensions.Width.PixelValue / 2f;
						seriesItemLabel.Appearance.Position.Y -= seriesItemLabel.Appearance.Dimensions.Height.PixelValue / 2f;
						PointF[] array = new PointF[]
						{
							new PointF(seriesItemLabel.Appearance.Position.X, seriesItemLabel.Appearance.Position.Y),
							new PointF(seriesItemLabel.Appearance.Position.X + seriesItemLabel.Appearance.Dimensions.Width.PixelValue, seriesItemLabel.Appearance.Position.Y),
							new PointF(seriesItemLabel.Appearance.Position.X, seriesItemLabel.Appearance.Position.Y + seriesItemLabel.Appearance.Dimensions.Height.PixelValue),
							new PointF(seriesItemLabel.Appearance.Position.X + seriesItemLabel.Appearance.Dimensions.Width.PixelValue, seriesItemLabel.Appearance.Position.Y + seriesItemLabel.Appearance.Dimensions.Height.PixelValue)
						};
						double[] array2 = new double[]
						{
							Math.Sqrt(Math.Pow((double)(pieCenter.X - array[0].X), 2.0) + Math.Pow((double)(pieCenter.Y - array[0].Y), 2.0)),
							Math.Sqrt(Math.Pow((double)(pieCenter.X - array[1].X), 2.0) + Math.Pow((double)(pieCenter.Y - array[1].Y), 2.0)),
							Math.Sqrt(Math.Pow((double)(pieCenter.X - array[2].X), 2.0) + Math.Pow((double)(pieCenter.Y - array[2].Y), 2.0)),
							Math.Sqrt(Math.Pow((double)(pieCenter.X - array[3].X), 2.0) + Math.Pow((double)(pieCenter.Y - array[3].Y), 2.0))
						};
						double num3 = array2[0];
						for (int j = 1; j < 4; j++)
						{
							if (num3 > array2[j])
							{
								num3 = array2[j];
							}
						}
						bool showLabelConnectors = this.Appearance.ShowLabelConnectors;
						double num4 = (num3 < (double)pieRadius) ? ((double)pieRadius - num3) : 0.0;
						if (!showLabelConnectors)
						{
							num4 += (double)(seriesItemLabel.Appearance.Distance + 1);
						}
						float num5 = (float)(num4 * Math.Cos(num2));
						float num6 = (float)(num4 * Math.Sin(num2));
						seriesItemLabel.Appearance.Position.Y += num6;
						if (showLabelConnectors && seriesItemLabel.Appearance.LabelLocation != StyleSeriesItemLabel.ItemLabelLocation.Inside)
						{
							seriesItemLabel.ConnectionMidPoint = new PointF(seriesItemLabel.Appearance.Position.X + num5, seriesItemLabel.Appearance.Position.Y);
							float num7 = pieRadius + (float)seriesItemLabel.Appearance.Distance;
							if (num2 > 1.57075 && num2 < 4.71225)
							{
								num7 = -num7 - seriesItemLabel.Appearance.Dimensions.Width.PixelValue;
							}
							seriesItemLabel.Appearance.Position.X = pieCenter.X + num7;
						}
						else
						{
							seriesItemLabel.Appearance.Position.X += num5;
						}
						if (seriesItemLabel.Appearance.LabelLocation == StyleSeriesItemLabel.ItemLabelLocation.Inside)
						{
							seriesItemLabel.ConnectionPoint = PointF.Empty;
							seriesItemLabel.Appearance.Position.X -= (float)((double)(pieRadius / 2f) * Math.Cos(num2));
							seriesItemLabel.Appearance.Position.Y -= (float)((double)(pieRadius / 2f) * Math.Sin(num2));
						}
						seriesItemLabel.Adjust(this.Chart.PlotArea);
						this.Chart.PlotArea.SeriesLabels.Add(seriesItemLabel);
					}
				}
			}
		}

		// Token: 0x0600E7DB RID: 59355 RVA: 0x0033DE58 File Offset: 0x0033C058
		internal void PrepareSeriesByXValues()
		{
			if (this.IsXDependent)
			{
				int count = this.chartSeriesItems.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.chartSeriesItems[i].XValue.Equals(double.NaN))
					{
						this.chartSeriesItems[i].XValue = 0.0;
					}
				}
			}
		}

		// Token: 0x0600E7DC RID: 59356 RVA: 0x0033DEC4 File Offset: 0x0033C0C4
		internal double Sum()
		{
			double num = 0.0;
			int count = this.chartSeriesItems.Count;
			for (int i = 0; i < count; i++)
			{
				ChartSeriesItem chartSeriesItem = this.chartSeriesItems[i];
				num += (chartSeriesItem.Empty ? 0.0 : Math.Abs(chartSeriesItem.YValue));
			}
			return num;
		}

		// Token: 0x0600E7DD RID: 59357 RVA: 0x0033DF24 File Offset: 0x0033C124
		internal static string GetCustomFormat(ref string s, string expression)
		{
			int num = s.IndexOf(expression);
			if (num > -1)
			{
				num += expression.Length;
				int num2 = s.IndexOf('}', num);
				string result = s.Substring(num, num2 - num);
				s = s.Remove(num - 1, num2 - num + 2);
				return result;
			}
			return string.Empty;
		}

		// Token: 0x0600E7DE RID: 59358 RVA: 0x0033DF78 File Offset: 0x0033C178
		internal double GetSumForStacked(ChartSeriesItem item)
		{
			if (!this.IsXDependent)
			{
				return this.Parent.GetSumForStacked(this.FindItemIndex(item));
			}
			Dictionary<double, double> sumsForStacked = this.Parent.GetSumsForStacked(this.Type);
			double key = 0.0;
			if (!double.IsNaN(item.XValue))
			{
				key = item.XValue;
			}
			if (sumsForStacked.ContainsKey(key))
			{
				return sumsForStacked[key];
			}
			return 0.0;
		}

		// Token: 0x0600E7DF RID: 59359 RVA: 0x0033DFEC File Offset: 0x0033C1EC
		internal static void ReplaceString(ref string s, string expression, double val, string defaultFormat)
		{
			while (s.Contains(expression))
			{
				string format = defaultFormat;
				if (expression.EndsWith("{"))
				{
					string customFormat = ChartSeries.GetCustomFormat(ref s, expression);
					if (!string.IsNullOrEmpty(customFormat))
					{
						format = customFormat;
					}
				}
				s = s.Replace(expression.TrimEnd(new char[]
				{
					'{'
				}), val.ToString(format));
			}
		}

		// Token: 0x0600E7E0 RID: 59360 RVA: 0x0033E04C File Offset: 0x0033C24C
		internal string FormatValues(string s, ChartSeriesItem item)
		{
			double num = 0.0;
			double num2 = item.YValue;
			if (this.Type == ChartSeriesType.Pie)
			{
				num2 = Math.Abs(num2);
			}
			ChartSeries.ReplaceString(ref s, "#Y2{", item.YValue2, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y3{", item.YValue3, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y4{", item.YValue4, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y2", item.YValue2, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y3", item.YValue3, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y4", item.YValue4, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y{", num2, string.Empty);
			ChartSeries.ReplaceString(ref s, "#Y", num2, string.Empty);
			if (s.Contains("#SUM"))
			{
				num = this.Sum();
				ChartSeries.ReplaceString(ref s, "#SUM{", num, string.Empty);
				ChartSeries.ReplaceString(ref s, "#SUM", num, string.Empty);
			}
			if (this.IsStacked)
			{
				num = this.GetSumForStacked(item);
				ChartSeries.ReplaceString(ref s, "#STSUM{", num, string.Empty);
				ChartSeries.ReplaceString(ref s, "#STSUM", num, string.Empty);
			}
			ChartSeries.ReplaceString(ref s, "#X{", item.XValue, string.Empty);
			ChartSeries.ReplaceString(ref s, "#X", item.XValue, string.Empty);
			s = s.Replace("#SERIES", item.Parent.Name);
			s = s.Replace("#ITEM", item.Name);
			if (this.IsStacked100)
			{
				ChartSeries.ReplaceString(ref s, "#%{", item.RelativeValue, string.Empty);
				ChartSeries.ReplaceString(ref s, "#%", item.RelativeValue, "P0");
			}
			else
			{
				if (num == 0.0)
				{
					num = this.Sum();
				}
				ChartSeries.ReplaceString(ref s, "#%{", item.YValue / num, string.Empty);
				ChartSeries.ReplaceString(ref s, "#%", item.YValue / num, "P0");
			}
			return s;
		}

		// Token: 0x0600E7E1 RID: 59361 RVA: 0x0033E26C File Offset: 0x0033C46C
		internal string GetItemLabel(ChartSeriesItem item)
		{
			string s;
			if (string.IsNullOrEmpty(item.Label.TextBlock.Text))
			{
				s = item.Parent.DefaultLabelValue;
			}
			else
			{
				s = item.Label.TextBlock.Text;
			}
			if (this.chartSeriesAppearance.ShowLabels)
			{
				return this.FormatValues(s, item);
			}
			return string.Empty;
		}

		// Token: 0x0600E7E2 RID: 59362 RVA: 0x0033E2CC File Offset: 0x0033C4CC
		internal float GetBarWidthRatio()
		{
			bool flag = true;
			if (this.Parent.Count != 1)
			{
				foreach (ChartSeries chartSeries in this.Parent)
				{
					flag = (flag && chartSeries.Appearance.BarWidthPercent == this.Parent.Parent.Appearance.BarWidthPercent);
				}
			}
			return (float)((flag ? this.Appearance.BarWidthPercent : this.Parent.Parent.Appearance.BarWidthPercent) / 100m);
		}

		// Token: 0x0600E7E3 RID: 59363 RVA: 0x0033E388 File Offset: 0x0033C588
		public void Clear()
		{
			this.Items.Clear();
		}

		// Token: 0x0600E7E4 RID: 59364 RVA: 0x0033E398 File Offset: 0x0033C598
		public void RemoveItem(ChartSeriesItem seriesItem, params ChartSeriesItem[] seriesItems)
		{
			this.chartSeriesItems.Remove(seriesItem);
			foreach (ChartSeriesItem item in seriesItems)
			{
				this.chartSeriesItems.Remove(item);
			}
		}

		// Token: 0x0600E7E5 RID: 59365 RVA: 0x0033E3D4 File Offset: 0x0033C5D4
		public void RemoveItem(int index, params int[] indexes)
		{
			this.chartSeriesItems.RemoveAt(index);
			foreach (int index2 in indexes)
			{
				this.chartSeriesItems.RemoveAt(index2);
			}
		}

		// Token: 0x0600E7E6 RID: 59366 RVA: 0x0033E410 File Offset: 0x0033C610
		public void AddItem(ChartSeriesItem seriesItem, params ChartSeriesItem[] seriesItems)
		{
			seriesItem.Parent = this;
			this.chartSeriesItems.Add(seriesItem);
			foreach (ChartSeriesItem chartSeriesItem in seriesItems)
			{
				chartSeriesItem.Parent = this;
				this.chartSeriesItems.Add(chartSeriesItem);
			}
		}

		// Token: 0x0600E7E7 RID: 59367 RVA: 0x0033E458 File Offset: 0x0033C658
		public void AddItem(ChartSeriesItemsCollection seriesItems)
		{
			foreach (ChartSeriesItem chartSeriesItem in seriesItems)
			{
				chartSeriesItem.Parent = this;
				this.chartSeriesItems.Add(chartSeriesItem);
			}
		}

		// Token: 0x0600E7E8 RID: 59368 RVA: 0x0033E4AC File Offset: 0x0033C6AC
		public void AddItem(ChartSeriesItem[] seriesItems)
		{
			foreach (ChartSeriesItem chartSeriesItem in seriesItems)
			{
				chartSeriesItem.Parent = this;
				this.chartSeriesItems.Add(chartSeriesItem);
			}
		}

		// Token: 0x0600E7E9 RID: 59369 RVA: 0x0033E4E0 File Offset: 0x0033C6E0
		public void AddItem(List<ChartSeriesItem> seriesItems)
		{
			foreach (ChartSeriesItem chartSeriesItem in seriesItems)
			{
				chartSeriesItem.Parent = this;
				this.chartSeriesItems.Add(chartSeriesItem);
			}
		}

		// Token: 0x0600E7EA RID: 59370 RVA: 0x0033E53C File Offset: 0x0033C73C
		public void AddItem(double value)
		{
			this.chartSeriesItems.Add(new ChartSeriesItem(value));
		}

		// Token: 0x0600E7EB RID: 59371 RVA: 0x0033E54F File Offset: 0x0033C74F
		public void AddItem(double value, string label)
		{
			this.chartSeriesItems.Add(new ChartSeriesItem(value, label));
		}

		// Token: 0x0600E7EC RID: 59372 RVA: 0x0033E563 File Offset: 0x0033C763
		public void AddItem(double value, string label, Color color)
		{
			this.chartSeriesItems.Add(new ChartSeriesItem(value, label, color));
		}

		// Token: 0x0600E7ED RID: 59373 RVA: 0x0033E578 File Offset: 0x0033C778
		public void AddItem(double value, string label, Color color, bool exploded)
		{
			this.chartSeriesItems.Add(new ChartSeriesItem(value, label, color, exploded));
		}

		// Token: 0x0600E7EE RID: 59374 RVA: 0x0033E58F File Offset: 0x0033C78F
		public void SetItemColor(int itemIndex, Color newColor)
		{
			if (itemIndex < this.chartSeriesItems.Count && itemIndex >= 0)
			{
				this[itemIndex].Appearance.FillStyle.MainColor = newColor;
			}
		}

		// Token: 0x0600E7EF RID: 59375 RVA: 0x0033E5BA File Offset: 0x0033C7BA
		public void SetItemValue(int itemIndex, double newValue)
		{
			if (itemIndex < this.chartSeriesItems.Count && itemIndex >= 0)
			{
				this[itemIndex].YValue = newValue;
			}
		}

		// Token: 0x0600E7F0 RID: 59376 RVA: 0x0033E5DB File Offset: 0x0033C7DB
		public void SetItemLabel(int itemIndex, string newLabel)
		{
			if (itemIndex < this.chartSeriesItems.Count)
			{
				this[itemIndex].Label.TextBlock.Text = newLabel;
			}
		}

		// Token: 0x0600E7F1 RID: 59377 RVA: 0x0033E602 File Offset: 0x0033C802
		public void SetItemExplode(int itemIndex, bool exploded)
		{
			if (itemIndex < this.chartSeriesItems.Count)
			{
				this[itemIndex].Appearance.Exploded = exploded;
			}
		}

		// Token: 0x0600E7F2 RID: 59378 RVA: 0x0033E624 File Offset: 0x0033C824
		public void SetValues(params double[] values)
		{
			int num = this.chartSeriesItems.Count;
			int num2 = num - values.Length;
			while (num2++ < 0)
			{
				ChartSeriesItem item = new ChartSeriesItem(0.0);
				this.chartSeriesItems.Add(item);
			}
			if (num2 > 0)
			{
				num = values.Length;
			}
			for (int i = 0; i < num; i++)
			{
				this[i].YValue = values[i];
			}
		}

		// Token: 0x0600E7F3 RID: 59379 RVA: 0x0033E68C File Offset: 0x0033C88C
		public void SetColors(params Color[] colors)
		{
			int num = Math.Min(this.chartSeriesItems.Count, colors.Length);
			for (int i = 0; i < num; i++)
			{
				this[i].Appearance.FillStyle.MainColor = colors[i];
			}
		}

		// Token: 0x0600E7F4 RID: 59380 RVA: 0x0033E6DC File Offset: 0x0033C8DC
		public void SetLabels(params string[] labels)
		{
			int num = Math.Min(this.chartSeriesItems.Count, labels.Length);
			for (int i = 0; i < num; i++)
			{
				this.chartSeriesItems[i].Name = labels[i];
			}
		}

		// Token: 0x0600E7F5 RID: 59381 RVA: 0x0033E720 File Offset: 0x0033C920
		public void SetExplodes(params bool[] explodes)
		{
			int num = Math.Min(this.chartSeriesItems.Count, explodes.Length);
			for (int i = 0; i < num; i++)
			{
				this[i].Appearance.Exploded = explodes[i];
			}
		}

		// Token: 0x0600E7F6 RID: 59382 RVA: 0x0033E761 File Offset: 0x0033C961
		public void SetItems(params ChartSeriesItem[] seriesItems)
		{
			this.chartSeriesItems.Clear();
			this.AddItem(seriesItems);
		}

		// Token: 0x0600E7F7 RID: 59383 RVA: 0x0033E775 File Offset: 0x0033C975
		public void RemoveItem(int itemIndex)
		{
			if (itemIndex >= 0 && itemIndex < this.chartSeriesItems.Count)
			{
				this.chartSeriesItems.RemoveAt(itemIndex);
			}
		}

		// Token: 0x0600E7F8 RID: 59384 RVA: 0x0033E798 File Offset: 0x0033C998
		public void ClearDataBoundState()
		{
			this.DataLabelsColumn = "";
			this.DataXColumn = "";
			this.DataXColumn2 = "";
			this.DataYColumn = "";
			this.DataYColumn2 = "";
			this.DataYColumn3 = "";
			this.DataYColumn4 = "";
			this.LegendFormattedText = string.Empty;
		}

		// Token: 0x0600E7F9 RID: 59385 RVA: 0x0033E7FD File Offset: 0x0033C9FD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600E7FA RID: 59386 RVA: 0x0033E805 File Offset: 0x0033CA05
		protected internal void CopyFrom(ChartSeries originalSeries)
		{
			base.ViewState = originalSeries.CloneState();
			this.chartSeriesParent = originalSeries.Parent;
			this.chartSeriesPlotArea = originalSeries.PlotArea;
			this.chartSeriesAppearance = originalSeries.Appearance;
		}

		// Token: 0x0600E7FB RID: 59387 RVA: 0x0033E838 File Offset: 0x0033CA38
		protected internal void CopyItems(ChartSeries originalSeries)
		{
			this.Clear();
			foreach (ChartSeriesItem chartSeriesItem in originalSeries.Items)
			{
				ChartSeriesItem chartSeriesItem2 = (ChartSeriesItem)chartSeriesItem.Clone();
				chartSeriesItem2.Parent = this;
				this.Items.Add(chartSeriesItem2);
			}
		}

		// Token: 0x0600E7FC RID: 59388 RVA: 0x0033E8A4 File Offset: 0x0033CAA4
		public ChartSeries CloneSeries()
		{
			ChartSeries chartSeries = (ChartSeries)this.Clone();
			chartSeries.chartSeriesAppearance.styleChart = this.chartSeriesAppearance.styleChart;
			chartSeries.CopyItems(this);
			return chartSeries;
		}

		// Token: 0x0600E7FD RID: 59389 RVA: 0x0033E8DC File Offset: 0x0033CADC
		public object Clone()
		{
			return new ChartSeries
			{
				ViewState = base.CloneState(),
				chartSeriesIsActiveRegionSet = this.chartSeriesIsActiveRegionSet,
				chartSeriesItems = new ChartSeriesItemsCollection(),
				chartSeriesAppearance = (StyleSeries)this.chartSeriesAppearance.Clone(),
				chartSeriesPlotArea = this.chartSeriesPlotArea,
				chartSeriesParent = this.chartSeriesParent
			};
		}

		// Token: 0x0600E7FE RID: 59390 RVA: 0x0033E941 File Offset: 0x0033CB41
		protected override void Dispose(bool disposing)
		{
			if (this.chartSeriesAppearance != null)
			{
				this.chartSeriesAppearance.Dispose();
				this.chartSeriesAppearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600E7FF RID: 59391 RVA: 0x0033E964 File Offset: 0x0033CB64
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartSeriesItems).TrackViewState();
			((IChartingStateManager)this.chartSeriesAppearance).TrackViewState();
		}

		// Token: 0x0600E800 RID: 59392 RVA: 0x0033E984 File Offset: 0x0033CB84
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartSeriesItems).LoadViewState(array[1]);
				((IChartingStateManager)this.chartSeriesAppearance).LoadViewState(array[2]);
			}
			if (!string.IsNullOrEmpty(this.ActiveRegionAttributes) || !string.IsNullOrEmpty(this.ActiveRegionToolTip) || !string.IsNullOrEmpty(this.ActiveRegionUrl))
			{
				this.chartSeriesIsActiveRegionSet = true;
			}
		}

		// Token: 0x0600E801 RID: 59393 RVA: 0x0033E9F0 File Offset: 0x0033CBF0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartSeriesItems).SaveViewState(),
				((IChartingStateManager)this.chartSeriesAppearance).SaveViewState()
			}.ToArray();
		}

		// Token: 0x04004287 RID: 17031
		private StyleSeries chartSeriesAppearance;

		// Token: 0x04004288 RID: 17032
		private ChartSeriesItemsCollection chartSeriesItems;

		// Token: 0x04004289 RID: 17033
		internal ChartPlotArea chartSeriesPlotArea;

		// Token: 0x0400428A RID: 17034
		private ChartSeriesCollection chartSeriesParent;

		// Token: 0x0400428B RID: 17035
		private bool chartSeriesIsActiveRegionSet;
	}
}
