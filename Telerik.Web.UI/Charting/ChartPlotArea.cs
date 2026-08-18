using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001733 RID: 5939
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class ChartPlotArea : LayoutElement, IContainer, IDisposable
	{
		// Token: 0x0600E71D RID: 59165 RVA: 0x0033B0E4 File Offset: 0x003392E4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartPlotAreaMarkedZones).TrackViewState();
			((IChartingStateManager)this.chartPlotAreaXAxis).TrackViewState();
			((IChartingStateManager)this.chartPlotAreaYAxis).TrackViewState();
			((IChartingStateManager)this.chartPlotAreaYAxis2).TrackViewState();
			((IChartingStateManager)this.chartPlotAreaEmptySeriesMessage).TrackViewState();
			((IChartingStateManager)this.chartPlotAreaChartDataTable).TrackViewState();
		}

		// Token: 0x0600E71E RID: 59166 RVA: 0x0033B13C File Offset: 0x0033933C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartPlotAreaMarkedZones).LoadViewState(array[1]);
				((IChartingStateManager)this.chartPlotAreaXAxis).LoadViewState(array[2]);
				((IChartingStateManager)this.chartPlotAreaYAxis).LoadViewState(array[3]);
				((IChartingStateManager)this.chartPlotAreaYAxis2).LoadViewState(array[4]);
				((IChartingStateManager)this.chartPlotAreaEmptySeriesMessage).LoadViewState(array[5]);
				((IChartingStateManager)this.chartPlotAreaChartDataTable).LoadViewState(array[6]);
			}
		}

		// Token: 0x0600E71F RID: 59167 RVA: 0x0033B1B0 File Offset: 0x003393B0
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			base.ViewState["OrderIndex"] = base.GetOrder();
			arrayList.Add(base.SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaMarkedZones).SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaXAxis).SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaYAxis).SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaYAxis2).SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaEmptySeriesMessage).SaveViewState());
			arrayList.Add(((IChartingStateManager)this.chartPlotAreaChartDataTable).SaveViewState());
			return arrayList.ToArray();
		}

		// Token: 0x17004656 RID: 18006
		// (get) Token: 0x0600E720 RID: 59168 RVA: 0x0033B25D File Offset: 0x0033945D
		// (set) Token: 0x0600E721 RID: 59169 RVA: 0x0033B265 File Offset: 0x00339465
		internal List<SeriesItemLabel> SeriesLabels
		{
			get
			{
				return this.chartPlotAreaSeriesLabels;
			}
			set
			{
				this.chartPlotAreaSeriesLabels = value;
			}
		}

		// Token: 0x17004657 RID: 18007
		// (get) Token: 0x0600E722 RID: 59170 RVA: 0x0033B26E File Offset: 0x0033946E
		// (set) Token: 0x0600E723 RID: 59171 RVA: 0x0033B276 File Offset: 0x00339476
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal Region PlotRegionCommon
		{
			get
			{
				return this.chartPlotAreaRegionCommon;
			}
			set
			{
				this.chartPlotAreaRegionCommon = value;
			}
		}

		// Token: 0x17004658 RID: 18008
		// (get) Token: 0x0600E724 RID: 59172 RVA: 0x0033B27F File Offset: 0x0033947F
		// (set) Token: 0x0600E725 RID: 59173 RVA: 0x0033B287 File Offset: 0x00339487
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal Region PlotRegionYAxisPrimary
		{
			get
			{
				return this.chartPlotAreaRegionYAxisPrimary;
			}
			set
			{
				this.chartPlotAreaRegionYAxisPrimary = value;
			}
		}

		// Token: 0x17004659 RID: 18009
		// (get) Token: 0x0600E726 RID: 59174 RVA: 0x0033B290 File Offset: 0x00339490
		// (set) Token: 0x0600E727 RID: 59175 RVA: 0x0033B298 File Offset: 0x00339498
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal Region PlotRegionYAxisSecondary
		{
			get
			{
				return this.chartPlotAreaRegionYAxisSecondary;
			}
			set
			{
				this.chartPlotAreaRegionYAxisSecondary = value;
			}
		}

		// Token: 0x1700465A RID: 18010
		// (get) Token: 0x0600E728 RID: 59176 RVA: 0x0033B2A1 File Offset: 0x003394A1
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Description("Marked zones collection.")]
		[Editor(typeof(MarkedZonesCollectionEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartMarkedZonesCollection MarkedZones
		{
			get
			{
				return this.chartPlotAreaMarkedZones;
			}
		}

		// Token: 0x1700465B RID: 18011
		// (get) Token: 0x0600E729 RID: 59177 RVA: 0x0033B2A9 File Offset: 0x003394A9
		// (set) Token: 0x0600E72A RID: 59178 RVA: 0x0033B2B6 File Offset: 0x003394B6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				return this.appearance.Visible;
			}
			set
			{
				this.appearance.Visible = value;
			}
		}

		// Token: 0x1700465C RID: 18012
		// (get) Token: 0x0600E72B RID: 59179 RVA: 0x0033B2C4 File Offset: 0x003394C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		public ChartDataTable DataTable
		{
			get
			{
				return this.chartPlotAreaChartDataTable;
			}
		}

		// Token: 0x1700465D RID: 18013
		// (get) Token: 0x0600E72C RID: 59180 RVA: 0x0033B2CC File Offset: 0x003394CC
		// (set) Token: 0x0600E72D RID: 59181 RVA: 0x0033B301 File Offset: 0x00339501
		[NotifyParentProperty(true)]
		[Browsable(false)]
		internal ChartSeriesOrientation SeriesOrientation
		{
			get
			{
				if (this.Chart != null)
				{
					return this.Chart.SeriesOrientation;
				}
				return (ChartSeriesOrientation)(base.ViewState["SeriesOrientation"] ?? ChartSeriesOrientation.Vertical);
			}
			set
			{
				if (this.Chart != null)
				{
					this.Chart.SeriesOrientation = value;
					return;
				}
				base.ViewState["SeriesOrientation"] = value;
				this.UpdateAxisOrientation();
			}
		}

		// Token: 0x0600E72E RID: 59182 RVA: 0x0033B334 File Offset: 0x00339534
		internal bool ShouldSerializeSeriesOrientation()
		{
			return false;
		}

		// Token: 0x1700465E RID: 18014
		// (get) Token: 0x0600E72F RID: 59183 RVA: 0x0033B337 File Offset: 0x00339537
		// (set) Token: 0x0600E730 RID: 59184 RVA: 0x0033B358 File Offset: 0x00339558
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		internal bool IntelligentLabelsEnabled
		{
			get
			{
				return (bool)(base.ViewState["IntelligentLabelsEnabled"] ?? false);
			}
			set
			{
				base.ViewState["IntelligentLabelsEnabled"] = value;
			}
		}

		// Token: 0x0600E731 RID: 59185 RVA: 0x0033B370 File Offset: 0x00339570
		internal bool ShouldSerializeIntelligentLabelsEnabled()
		{
			return false;
		}

		// Token: 0x1700465F RID: 18015
		// (get) Token: 0x0600E732 RID: 59186 RVA: 0x0033B373 File Offset: 0x00339573
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Description("Empty series message")]
		public EmptySeriesMessage EmptySeriesMessage
		{
			get
			{
				return this.chartPlotAreaEmptySeriesMessage;
			}
		}

		// Token: 0x17004660 RID: 18016
		// (get) Token: 0x0600E733 RID: 59187 RVA: 0x0033B37B File Offset: 0x0033957B
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartXAxis XAxis
		{
			get
			{
				return this.chartPlotAreaXAxis;
			}
		}

		// Token: 0x17004661 RID: 18017
		// (get) Token: 0x0600E734 RID: 59188 RVA: 0x0033B383 File Offset: 0x00339583
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartYAxis YAxis
		{
			get
			{
				return this.chartPlotAreaYAxis;
			}
		}

		// Token: 0x17004662 RID: 18018
		// (get) Token: 0x0600E735 RID: 59189 RVA: 0x0033B38B File Offset: 0x0033958B
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ChartYAxis YAxis2
		{
			get
			{
				return this.chartPlotAreaYAxis2;
			}
		}

		// Token: 0x17004663 RID: 18019
		// (get) Token: 0x0600E736 RID: 59190 RVA: 0x0033B393 File Offset: 0x00339593
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public Chart Parent
		{
			get
			{
				return this.chartPlotAreaParent;
			}
		}

		// Token: 0x17004664 RID: 18020
		// (get) Token: 0x0600E737 RID: 59191 RVA: 0x0033B39B File Offset: 0x0033959B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public StylePlotArea Appearance
		{
			get
			{
				return (StylePlotArea)this.appearance;
			}
		}

		// Token: 0x17004665 RID: 18021
		// (get) Token: 0x0600E738 RID: 59192 RVA: 0x0033B3A8 File Offset: 0x003395A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Chart Chart
		{
			get
			{
				return this.chartPlotAreaParent;
			}
		}

		// Token: 0x17004666 RID: 18022
		// (get) Token: 0x0600E739 RID: 59193 RVA: 0x0033B3B0 File Offset: 0x003395B0
		// (set) Token: 0x0600E73A RID: 59194 RVA: 0x0033B3D1 File Offset: 0x003395D1
		internal PopularCollection PopularValues
		{
			get
			{
				if (this.popVals == null)
				{
					this.popVals = PopularCollection.GetPopularValues(this.Chart);
				}
				return this.popVals;
			}
			set
			{
				this.popVals = value;
			}
		}

		// Token: 0x0600E73B RID: 59195 RVA: 0x0033B3DA File Offset: 0x003395DA
		public ChartPlotArea() : this(null)
		{
		}

		// Token: 0x0600E73C RID: 59196 RVA: 0x0033B3E3 File Offset: 0x003395E3
		public ChartPlotArea(Chart parent) : base(new StylePlotArea(), parent)
		{
			this.chartPlotAreaParent = parent;
			this.Init();
			this.chartPlotAreaChartDataTable.Container = parent;
			this.InitOrderList();
		}

		// Token: 0x0600E73D RID: 59197 RVA: 0x0033B410 File Offset: 0x00339610
		private void Init()
		{
			((StylePlotArea)this.appearance).PlotArea = this;
			this.chartPlotAreaOrderList = new List<IOrdering>();
			this.chartPlotAreaXAxis = new ChartXAxis(this, this);
			this.chartPlotAreaYAxis = new ChartYAxis(this, ChartYAxisType.Primary);
			this.chartPlotAreaYAxis2 = new ChartYAxis(this, ChartYAxisType.Secondary);
			this.chartPlotAreaEmptySeriesMessage = new EmptySeriesMessage(this, this);
			this.chartPlotAreaChartDataTable = new ChartDataTable(this);
			this.chartPlotAreaMarkedZones = new ChartMarkedZonesCollection(this);
			this.chartPlotAreaSeriesLabels = new List<SeriesItemLabel>();
		}

		// Token: 0x0600E73E RID: 59198 RVA: 0x0033B490 File Offset: 0x00339690
		private void InitOrderList()
		{
			this.chartPlotAreaOrderList.Add(this.chartPlotAreaXAxis);
			this.chartPlotAreaOrderList.Add(this.chartPlotAreaYAxis);
			this.chartPlotAreaOrderList.Add(this.chartPlotAreaYAxis2);
			this.chartPlotAreaOrderList.Add(this.chartPlotAreaEmptySeriesMessage);
			this.chartPlotAreaOrderList.Add(this.chartPlotAreaChartDataTable);
		}

		// Token: 0x0600E73F RID: 59199 RVA: 0x0033B4F4 File Offset: 0x003396F4
		internal void UpdateAxisOrientation()
		{
			switch (this.Chart.SeriesOrientation)
			{
			case ChartSeriesOrientation.Vertical:
				this.chartPlotAreaXAxis.Appearance.Orientation = Orientation.Horizontal;
				this.chartPlotAreaYAxis.Appearance.Orientation = (this.chartPlotAreaYAxis2.Appearance.Orientation = Orientation.Vertical);
				return;
			case ChartSeriesOrientation.Horizontal:
				this.chartPlotAreaXAxis.Appearance.Orientation = Orientation.Vertical;
				this.chartPlotAreaYAxis.Appearance.Orientation = (this.chartPlotAreaYAxis2.Appearance.Orientation = Orientation.Horizontal);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600E740 RID: 59200 RVA: 0x0033B588 File Offset: 0x00339788
		public ChartSeriesCollection SeriesCollection()
		{
			Chart chart = this.Chart;
			if (chart != null)
			{
				return chart.Series;
			}
			return null;
		}

		// Token: 0x0600E741 RID: 59201 RVA: 0x0033B5A8 File Offset: 0x003397A8
		internal ChartSeriesCollection SeriesCollection(ChartYAxisType chartYAxisType)
		{
			Chart chart = this.Chart;
			if (chart != null)
			{
				ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(chart);
				foreach (ChartSeries chartSeries in chart.Series)
				{
					if (chartSeries.PlotArea.Equals(this) && chartSeries.YAxisType == chartYAxisType)
					{
						chartSeriesCollection.Add(chartSeries);
					}
				}
				return chartSeriesCollection;
			}
			return null;
		}

		// Token: 0x0600E742 RID: 59202 RVA: 0x0033B620 File Offset: 0x00339820
		internal void InitializeAxes()
		{
			this.XAxis.InitializeItems();
			this.YAxis.InitializeItems();
			this.YAxis2.InitializeItems();
		}

		// Token: 0x0600E743 RID: 59203 RVA: 0x0033B644 File Offset: 0x00339844
		internal void CreateRectanglesInSeriesLabel()
		{
			foreach (SeriesItemLabel seriesItemLabel in this.SeriesLabels)
			{
				seriesItemLabel.seriesItemLabelRectangle = new RectangleF(seriesItemLabel.Appearance.Position.X, seriesItemLabel.Appearance.Position.Y, seriesItemLabel.Appearance.Dimensions.Width.PixelValue, seriesItemLabel.Appearance.Dimensions.Height.PixelValue);
			}
		}

		// Token: 0x0600E744 RID: 59204 RVA: 0x0033B6E8 File Offset: 0x003398E8
		internal void ClearAutoPropertiesForAxisItems()
		{
			this.XAxis.ClearAutoPropertiesForAxisItems();
			this.YAxis.ClearAutoPropertiesForAxisItems();
			this.YAxis2.ClearAutoPropertiesForAxisItems();
		}

		// Token: 0x0600E745 RID: 59205 RVA: 0x0033B70B File Offset: 0x0033990B
		internal float GetBarStart(ChartSeries series)
		{
			return this.GetBarStart(series, false);
		}

		// Token: 0x0600E746 RID: 59206 RVA: 0x0033B718 File Offset: 0x00339918
		internal float GetBarStart(ChartSeries series, bool displacementOnly)
		{
			float num = 1f - (float)(this.Chart.Appearance.BarOverlapPercent / 100m);
			ChartSeriesCollection seriesCollection = this.SeriesCollection().GetSeriesCollection(new ChartSeriesType[]
			{
				ChartSeriesType.Gantt,
				ChartSeriesType.Bar,
				ChartSeriesType.CandleStick,
				ChartSeriesType.StackedBar,
				ChartSeriesType.StackedBar100
			});
			float num2 = 0f;
			float num3 = 0f;
			bool flag = true;
			int num4 = 1;
			bool flag2 = false;
			bool flag3 = false;
			foreach (ChartSeries chartSeries in seriesCollection)
			{
				if (chartSeries.Type == ChartSeriesType.StackedBar)
				{
					if (flag2)
					{
						continue;
					}
					flag2 = true;
				}
				if (chartSeries.Type == ChartSeriesType.StackedBar100)
				{
					if (flag3)
					{
						continue;
					}
					flag3 = true;
				}
				float num5 = (num4 != seriesCollection.BarSeriesCount) ? (this.GetBarWidth(chartSeries) * num) : this.GetBarWidth(chartSeries);
				num2 += num5;
				if (series == chartSeries)
				{
					flag = false;
				}
				if (flag)
				{
					num3 += num5;
				}
				num4++;
			}
			float result = this.XAxis.GetStartCoordinate() - num2 / 2f + num3;
			if (displacementOnly)
			{
				return num3;
			}
			return result;
		}

		// Token: 0x0600E747 RID: 59207 RVA: 0x0033B850 File Offset: 0x00339A50
		internal void Reset()
		{
			this.ResetRegions();
			this.XAxis.TickPoints = null;
			this.XAxis.TickPointsTypes = null;
			this.XAxis.GridPoints = null;
			this.XAxis.GridPointsTypes = null;
			this.YAxis.MajorPoints = null;
			this.YAxis.MinorPoints = null;
			this.YAxis2.MajorPoints = null;
			this.YAxis2.MinorPoints = null;
			this.SeriesLabels.Clear();
		}

		// Token: 0x0600E748 RID: 59208 RVA: 0x0033B8CE File Offset: 0x00339ACE
		internal void ResetRegions()
		{
			this.PlotRegionCommon = null;
			this.PlotRegionYAxisPrimary = null;
			this.PlotRegionYAxisSecondary = null;
		}

		// Token: 0x0600E749 RID: 59209 RVA: 0x0033B8E5 File Offset: 0x00339AE5
		internal void PrepareForScale()
		{
			this.Appearance.Corners.Reset();
		}

		// Token: 0x0600E74A RID: 59210 RVA: 0x0033B8F8 File Offset: 0x00339AF8
		internal void PrepareForScale(float xScale, float yScale)
		{
			this.XAxis.chartAxisZoom = xScale;
			if (xScale > 1f)
			{
				this.XAxis.IsZeroBased = false;
			}
			ChartAxis yaxis = this.YAxis;
			this.YAxis2.chartAxisZoom = yScale;
			yaxis.chartAxisZoom = yScale;
			this.Appearance.SaveDimensions();
			this.PrepareForScale();
		}

		// Token: 0x0600E74B RID: 59211 RVA: 0x0033B950 File Offset: 0x00339B50
		internal void RestoreAfterScale()
		{
			this.Appearance.RestoreDimensions();
			this.XAxis.chartAxisZoom = (this.YAxis.chartAxisZoom = (this.YAxis2.chartAxisZoom = 1f));
		}

		// Token: 0x0600E74C RID: 59212 RVA: 0x0033B994 File Offset: 0x00339B94
		internal float GetBarWidth()
		{
			Chart chart = this.Chart;
			if (chart == null)
			{
				return float.NaN;
			}
			int barSeriesCount = chart.Series.BarSeriesCount;
			float num = (float)(chart.Appearance.BarWidthPercent / 100m);
			float num2 = (float)(chart.Appearance.BarOverlapPercent / 100m);
			if (barSeriesCount <= 0)
			{
				return float.NaN;
			}
			return num * this.chartPlotAreaXAxis.GetPixelStep() / ((1f - num2) * (float)(barSeriesCount - 1) + 1f);
		}

		// Token: 0x0600E74D RID: 59213 RVA: 0x0033BA24 File Offset: 0x00339C24
		internal float GetBarWidth(ChartSeries series)
		{
			Chart chart = this.Chart;
			if (chart == null)
			{
				return float.NaN;
			}
			int barSeriesCount = chart.Series.BarSeriesCount;
			float num = (float)(series.Appearance.BarWidthPercent / 100m);
			float num2 = (float)(chart.Appearance.BarOverlapPercent / 100m);
			if (barSeriesCount <= 0)
			{
				return float.NaN;
			}
			return num * this.chartPlotAreaXAxis.GetPixelStep() / ((1f - num2) * (float)(barSeriesCount - 1) + 1f);
		}

		// Token: 0x0600E74E RID: 59214 RVA: 0x0033BAB4 File Offset: 0x00339CB4
		internal void AlignAxisByZeros()
		{
			float zeroCoordinate;
			float zeroCoordinate2;
			switch (this.Parent.SeriesOrientation)
			{
			case ChartSeriesOrientation.Vertical:
				break;
			default:
				zeroCoordinate = this.YAxis.GetZeroCoordinate();
				this.XAxis.StartPoint = new PointF(zeroCoordinate, this.XAxis.StartPoint.Y);
				this.XAxis.EndPoint = new PointF(zeroCoordinate, this.XAxis.EndPoint.Y);
				zeroCoordinate2 = this.XAxis.GetZeroCoordinate();
				this.YAxis.StartPoint = new PointF(this.YAxis.StartPoint.X, zeroCoordinate2);
				this.YAxis.EndPoint = new PointF(this.YAxis.EndPoint.X, zeroCoordinate2);
				using (IEnumerator<AxisSegment> enumerator = this.YAxis.Segments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AxisSegment axisSegment = enumerator.Current;
						axisSegment.StartPoint = new PointF(axisSegment.StartPoint.X, zeroCoordinate2);
						axisSegment.EndPoint = new PointF(axisSegment.EndPoint.X, zeroCoordinate2);
					}
					return;
				}
				break;
			}
			zeroCoordinate2 = this.YAxis.GetZeroCoordinate();
			this.XAxis.StartPoint = new PointF(this.XAxis.StartPoint.X, zeroCoordinate2);
			this.XAxis.EndPoint = new PointF(this.XAxis.EndPoint.X, zeroCoordinate2);
			zeroCoordinate = this.XAxis.GetZeroCoordinate();
			this.YAxis.StartPoint = new PointF(zeroCoordinate, this.YAxis.StartPoint.Y);
			this.YAxis.EndPoint = new PointF(zeroCoordinate, this.YAxis.EndPoint.Y);
			foreach (AxisSegment axisSegment2 in this.YAxis.Segments)
			{
				axisSegment2.StartPoint = new PointF(zeroCoordinate, axisSegment2.StartPoint.Y);
				axisSegment2.EndPoint = new PointF(zeroCoordinate, axisSegment2.EndPoint.Y);
			}
		}

		// Token: 0x0600E74F RID: 59215 RVA: 0x0033BD28 File Offset: 0x00339F28
		internal override void CalculatePosition(RenderEngine renderEngine)
		{
			if (!this.Visible)
			{
				return;
			}
			if (this.DataTable.IsVisible)
			{
				this.CalculateChartDataTablePlotAreaRelative(renderEngine, this.Chart.Appearance.Dimensions.Width.PixelValue, this.Chart.Appearance.Dimensions.Height.PixelValue);
			}
			this.Reset();
			if (this.Appearance.Dimensions.AutoSize)
			{
				this.Appearance.Position.ResetGlobal();
				this.Appearance.Position.X = this.Appearance.Dimensions.Margins.Left.PixelValue;
				this.Appearance.Position.Y = this.Appearance.Dimensions.Margins.Top.PixelValue;
				this.Appearance.Dimensions.SetDimensions(this.Chart.Appearance.Dimensions.Width.PixelValue - this.Appearance.Dimensions.Margins.Left.PixelValue - this.Appearance.Dimensions.Margins.Right.PixelValue, this.Chart.Appearance.Dimensions.Height.PixelValue - this.Appearance.Dimensions.Margins.Top.PixelValue - this.Appearance.Dimensions.Margins.Bottom.PixelValue);
			}
			else if (this.appearance.Position.AlignedPosition == AlignedPositions.None)
			{
				this.appearance.Position.X = this.appearance.dimensions.Margins.Left.PixelValue;
				this.appearance.Position.Y = this.appearance.dimensions.Margins.Top.PixelValue;
			}
			else
			{
				base.CalculatePosition(renderEngine);
			}
			if (this.DataTable.IsVisible)
			{
				this.DataTable.Measure(renderEngine);
			}
			if (this.EmptySeriesMessage.TextBlock.textBlockWrapContext == null)
			{
				this.EmptySeriesMessage.TextBlock.textBlockWrapContext = new WrapContext(this.Appearance.Dimensions, WrapType.FixedProportion);
			}
		}

		// Token: 0x0600E750 RID: 59216 RVA: 0x0033BF74 File Offset: 0x0033A174
		private void CalculateChartDataTablePlotAreaRelative(RenderEngine renderEngine, float containerWidth, float containerHeight)
		{
			this.DataTable.Measure(renderEngine);
			if (this.DataTable.Data.Length > 0 && this.DataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative)
			{
				if (this.Appearance.Dimensions.AutoSize)
				{
					this.Appearance.Dimensions.SetDimensions(containerWidth, 1f);
					Style.SetPixelValues(this, containerWidth, containerHeight);
				}
				if (this.Appearance.Position.Auto)
				{
					this.Appearance.Position.X = (this.Appearance.Position.Y = 0f);
				}
				float num = this.DataTable.Appearance.Dimensions.Height.PixelValue + this.DataTable.Appearance.Dimensions.Margins.Bottom.PixelValue + this.DataTable.Appearance.Border.Width + (float)Math.Ceiling((double)(this.Appearance.Border.Width / 2f));
				if (this.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					this.XAxis.LayoutMode = ChartAxisLayoutMode.Between;
					this.XAxis.AxisLabel.Appearance.Position.AlignedPosition = AlignedPositions.Bottom;
					this.YAxis.AxisLabel.Appearance.Position.AlignedPosition = AlignedPositions.Center;
					this.XAxis.CalculateLayout(renderEngine);
					num += this.XAxis.GetHeight() + (float)this.XAxis.TicksLength;
				}
				else
				{
					renderEngine.getAxisItemBoundOnly = true;
					this.YAxis.AxisLabel.Appearance.Position.AlignedPosition = AlignedPositions.Top;
					this.XAxis.AxisLabel.Appearance.Position.AlignedPosition = AlignedPositions.Center;
					this.YAxis.CalculateLayout(renderEngine);
					this.PlotRegionCommon = null;
					this.PlotRegionYAxisPrimary = null;
					this.PlotRegionYAxisSecondary = null;
					num += this.DataTable.Appearance.Dimensions.Margins.Top.PixelValue;
					num += this.YAxis.GetHeight() + (float)this.YAxis.TicksLength;
				}
				float num2 = this.DataTable.SizesW[0] + this.DataTable.Appearance.Dimensions.Margins.Left.PixelValue + this.DataTable.Appearance.Border.Width * 2f;
				if (this.Appearance.Dimensions.AutoSize)
				{
					if (this.Appearance.Dimensions.Margins.Left.PixelValue < num2)
					{
						this.Appearance.Dimensions.Margins.Left = Unit.Pixel(num2);
					}
					if (this.Appearance.Dimensions.Margins.Bottom.PixelValue < num)
					{
						this.Appearance.Dimensions.Margins.Bottom = Unit.Pixel(num);
					}
				}
			}
		}

		// Token: 0x17004667 RID: 18023
		// (get) Token: 0x0600E751 RID: 59217 RVA: 0x0033C277 File Offset: 0x0033A477
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<IOrdering> OrderList
		{
			get
			{
				return this.chartPlotAreaOrderList;
			}
		}

		// Token: 0x17004668 RID: 18024
		// (get) Token: 0x0600E752 RID: 59218 RVA: 0x0033C280 File Offset: 0x0033A480
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public int NextPosition
		{
			get
			{
				IOrdering item = null;
				foreach (IOrdering ordering in this.chartPlotAreaOrderList)
				{
					item = ordering;
				}
				return this.chartPlotAreaOrderList.IndexOf(item) + 1;
			}
		}

		// Token: 0x0600E753 RID: 59219 RVA: 0x0033C2E0 File Offset: 0x0033A4E0
		public int GetOrder(IOrdering element)
		{
			return this.chartPlotAreaOrderList.IndexOf(element);
		}

		// Token: 0x0600E754 RID: 59220 RVA: 0x0033C2EE File Offset: 0x0033A4EE
		public void Add(IOrdering element)
		{
			element.Container = this;
			this.chartPlotAreaOrderList.Add(element);
		}

		// Token: 0x0600E755 RID: 59221 RVA: 0x0033C303 File Offset: 0x0033A503
		public void Insert(int order, IOrdering element)
		{
			element.Container = this;
			this.chartPlotAreaOrderList.Insert(order, element);
		}

		// Token: 0x0600E756 RID: 59222 RVA: 0x0033C319 File Offset: 0x0033A519
		public void Remove(IOrdering element)
		{
			this.chartPlotAreaOrderList.Remove(element);
		}

		// Token: 0x0600E757 RID: 59223 RVA: 0x0033C328 File Offset: 0x0033A528
		public void RemoveAt(int index)
		{
			this.chartPlotAreaOrderList.RemoveAt(index);
		}

		// Token: 0x0600E758 RID: 59224 RVA: 0x0033C338 File Offset: 0x0033A538
		public void ReIndex()
		{
			List<IOrdering> list = new List<IOrdering>();
			int num = 0;
			foreach (IOrdering ordering in this.chartPlotAreaOrderList)
			{
				if (ordering != null)
				{
					list.Insert(num++, ordering);
				}
			}
			this.chartPlotAreaOrderList = list;
		}

		// Token: 0x0600E759 RID: 59225 RVA: 0x0033C3A4 File Offset: 0x0033A5A4
		protected override void Dispose(bool disposing)
		{
			if (this.chartPlotAreaChartDataTable != null)
			{
				this.chartPlotAreaChartDataTable.Dispose();
				this.chartPlotAreaChartDataTable = null;
			}
			if (this.chartPlotAreaEmptySeriesMessage != null)
			{
				this.chartPlotAreaEmptySeriesMessage.Dispose();
				this.chartPlotAreaEmptySeriesMessage = null;
			}
			if (this.chartPlotAreaRegionCommon != null)
			{
				this.chartPlotAreaRegionCommon.Dispose();
				this.chartPlotAreaRegionCommon = null;
			}
			if (this.chartPlotAreaRegionYAxisPrimary != null)
			{
				this.chartPlotAreaRegionYAxisPrimary.Dispose();
				this.chartPlotAreaRegionYAxisPrimary = null;
			}
			if (this.chartPlotAreaRegionYAxisSecondary != null)
			{
				this.chartPlotAreaRegionYAxisSecondary.Dispose();
				this.chartPlotAreaRegionYAxisSecondary = null;
			}
			if (this.chartPlotAreaXAxis != null)
			{
				this.chartPlotAreaXAxis.Dispose();
				this.chartPlotAreaXAxis = null;
			}
			if (this.chartPlotAreaYAxis != null)
			{
				this.chartPlotAreaYAxis.Dispose();
				this.chartPlotAreaYAxis = null;
			}
			if (this.chartPlotAreaYAxis2 != null)
			{
				this.chartPlotAreaYAxis2.Dispose();
				this.chartPlotAreaYAxis2 = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400426E RID: 17006
		private ChartMarkedZonesCollection chartPlotAreaMarkedZones;

		// Token: 0x0400426F RID: 17007
		private ChartXAxis chartPlotAreaXAxis;

		// Token: 0x04004270 RID: 17008
		private ChartYAxis chartPlotAreaYAxis;

		// Token: 0x04004271 RID: 17009
		private ChartYAxis chartPlotAreaYAxis2;

		// Token: 0x04004272 RID: 17010
		private Chart chartPlotAreaParent;

		// Token: 0x04004273 RID: 17011
		private EmptySeriesMessage chartPlotAreaEmptySeriesMessage;

		// Token: 0x04004274 RID: 17012
		private Region chartPlotAreaRegionCommon;

		// Token: 0x04004275 RID: 17013
		private Region chartPlotAreaRegionYAxisPrimary;

		// Token: 0x04004276 RID: 17014
		private Region chartPlotAreaRegionYAxisSecondary;

		// Token: 0x04004277 RID: 17015
		private List<SeriesItemLabel> chartPlotAreaSeriesLabels;

		// Token: 0x04004278 RID: 17016
		private PopularCollection popVals;

		// Token: 0x04004279 RID: 17017
		private ChartDataTable chartPlotAreaChartDataTable;

		// Token: 0x0400427A RID: 17018
		private List<IOrdering> chartPlotAreaOrderList;
	}
}
