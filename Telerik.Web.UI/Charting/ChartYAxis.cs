using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200172C RID: 5932
	public class ChartYAxis : ChartAxis
	{
		// Token: 0x0600E6BC RID: 59068 RVA: 0x00337246 File Offset: 0x00335446
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartYAxisScaleBreak).TrackViewState();
		}

		// Token: 0x0600E6BD RID: 59069 RVA: 0x0033725C File Offset: 0x0033545C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartYAxisScaleBreak).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600E6BE RID: 59070 RVA: 0x0033728C File Offset: 0x0033548C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartYAxisScaleBreak).SaveViewState()
			}.ToArray();
		}

		// Token: 0x1700463D RID: 17981
		// (get) Token: 0x0600E6BF RID: 59071 RVA: 0x003372C4 File Offset: 0x003354C4
		// (set) Token: 0x0600E6C0 RID: 59072 RVA: 0x003372E5 File Offset: 0x003354E5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool IsLogarithmic
		{
			get
			{
				return (bool)(base.ViewState["IsLogarithmic"] ?? false);
			}
			set
			{
				base.ViewState["IsLogarithmic"] = value;
			}
		}

		// Token: 0x1700463E RID: 17982
		// (get) Token: 0x0600E6C1 RID: 59073 RVA: 0x003372FD File Offset: 0x003354FD
		// (set) Token: 0x0600E6C2 RID: 59074 RVA: 0x00337326 File Offset: 0x00335526
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0.0)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Range")]
		[Description("Specifies the minimal value of the axis.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public override double MinValue
		{
			get
			{
				return (double)(base.ViewState["MinValue"] ?? 0.0);
			}
			set
			{
				if (!base.AutoScale && !this.IsLogarithmic)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x1700463F RID: 17983
		// (get) Token: 0x0600E6C3 RID: 59075 RVA: 0x00337359 File Offset: 0x00335559
		// (set) Token: 0x0600E6C4 RID: 59076 RVA: 0x00337382 File Offset: 0x00335582
		[DefaultValue(7.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Specifies the maximal value of the axis.")]
		[Category("Range")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public override double MaxValue
		{
			get
			{
				return (double)(base.ViewState["MaxValue"] ?? 7.0);
			}
			set
			{
				if (!base.AutoScale && !this.IsLogarithmic)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x17004640 RID: 17984
		// (get) Token: 0x0600E6C5 RID: 59077 RVA: 0x003373B5 File Offset: 0x003355B5
		// (set) Token: 0x0600E6C6 RID: 59078 RVA: 0x003373DE File Offset: 0x003355DE
		[PersistenceMode(PersistenceMode.Attribute)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(1.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Range")]
		[Description("Specifies the step at which axis values are calculated.")]
		public override double Step
		{
			get
			{
				return (double)(base.ViewState["Step"] ?? 1.0);
			}
			set
			{
				if (!base.AutoScale && !this.IsLogarithmic)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["Step"] = value;
			}
		}

		// Token: 0x17004641 RID: 17985
		// (get) Token: 0x0600E6C7 RID: 59079 RVA: 0x00337411 File Offset: 0x00335611
		// (set) Token: 0x0600E6C8 RID: 59080 RVA: 0x0033743C File Offset: 0x0033563C
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(10.0)]
		[Browsable(true)]
		public double LogarithmBase
		{
			get
			{
				return (double)(base.ViewState["LogarithmBase"] ?? 10.0);
			}
			set
			{
				if (value < 2.0)
				{
					base.ViewState["LogarithmBase"] = 2.0;
					return;
				}
				base.ViewState["LogarithmBase"] = value;
			}
		}

		// Token: 0x17004642 RID: 17986
		// (get) Token: 0x0600E6C9 RID: 59081 RVA: 0x0033748A File Offset: 0x0033568A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AxisSegmentCollection Segments
		{
			get
			{
				return this.chartYAxisScaleBreak.scaleBreakSegments;
			}
		}

		// Token: 0x17004643 RID: 17987
		// (get) Token: 0x0600E6CA RID: 59082 RVA: 0x00337497 File Offset: 0x00335697
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public ScaleBreak ScaleBreaks
		{
			get
			{
				return this.chartYAxisScaleBreak;
			}
		}

		// Token: 0x17004644 RID: 17988
		// (get) Token: 0x0600E6CB RID: 59083 RVA: 0x0033749F File Offset: 0x0033569F
		// (set) Token: 0x0600E6CC RID: 59084 RVA: 0x003374C0 File Offset: 0x003356C0
		[DefaultValue(ChartYAxisType.Primary)]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17004645 RID: 17989
		// (get) Token: 0x0600E6CD RID: 59085 RVA: 0x003374D8 File Offset: 0x003356D8
		// (set) Token: 0x0600E6CE RID: 59086 RVA: 0x003374F9 File Offset: 0x003356F9
		[Browsable(true)]
		[DefaultValue(ChartYAxisMode.Normal)]
		[NotifyParentProperty(true)]
		public ChartYAxisMode AxisMode
		{
			get
			{
				return (ChartYAxisMode)(base.ViewState["AxisMode"] ?? ChartYAxisMode.Normal);
			}
			set
			{
				base.ViewState["AxisMode"] = value;
			}
		}

		// Token: 0x17004646 RID: 17990
		// (get) Token: 0x0600E6CF RID: 59087 RVA: 0x00337514 File Offset: 0x00335714
		internal override float ItemsBound
		{
			get
			{
				if (this.YAxisType == ChartYAxisType.Primary)
				{
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						return Math.Max(0f, base.StartPoint.X - (float)base.TicksLength - base.Items.GetWidth());
					}
					return Math.Max(0f, base.StartPoint.Y + (float)base.TicksLength + base.Items.GetHeight());
				}
				else
				{
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						return Math.Max(0f, base.StartPoint.X + (float)base.TicksLength + base.Items.GetWidth());
					}
					return Math.Max(0f, base.StartPoint.Y - (float)base.TicksLength - base.Items.GetHeight());
				}
			}
		}

		// Token: 0x0600E6D0 RID: 59088 RVA: 0x003375FC File Offset: 0x003357FC
		public ChartYAxis(ChartPlotArea parent, ChartYAxisType type) : base(parent)
		{
			this.YAxisType = type;
			this.chartAxisType = ((type == ChartYAxisType.Primary) ? ChartAxisType.YAxis : ChartAxisType.YAxis2);
			this.chartAxisAppearance = new StyleAxisY(this);
			this.chartYAxisScaleBreak = new ScaleBreak(this);
			this.chartAxisLabel = new AxisYLabel(this, base.Chart);
			this.chartAxisLabel.Appearance.RotationAngle = 270f;
			this.chartAxisLabel.Appearance.styleChart = (this.chartAxisLabel.Marker.Appearance.styleChart = base.Chart);
		}

		// Token: 0x17004647 RID: 17991
		// (get) Token: 0x0600E6D1 RID: 59089 RVA: 0x00337691 File Offset: 0x00335891
		// (set) Token: 0x0600E6D2 RID: 59090 RVA: 0x00337699 File Offset: 0x00335899
		internal PointF[] MajorPoints
		{
			get
			{
				return this.chartYAxisMajorPoints;
			}
			set
			{
				this.chartYAxisMajorPoints = value;
			}
		}

		// Token: 0x17004648 RID: 17992
		// (get) Token: 0x0600E6D3 RID: 59091 RVA: 0x003376A2 File Offset: 0x003358A2
		// (set) Token: 0x0600E6D4 RID: 59092 RVA: 0x003376AA File Offset: 0x003358AA
		internal PointF[] MinorPoints
		{
			get
			{
				return this.chartYAxisMinorPoints;
			}
			set
			{
				this.chartYAxisMinorPoints = value;
			}
		}

		// Token: 0x0600E6D5 RID: 59093 RVA: 0x003376B4 File Offset: 0x003358B4
		internal override void CalculateGridsAndTicks()
		{
			if (this != null)
			{
				bool flag = base.IsVisible();
				bool flag2 = base.Appearance.MajorGridLines.ShouldRender(flag);
				bool flag3 = base.Appearance.MinorGridLines.ShouldRender(flag);
				if (!flag && !flag2 && !flag3)
				{
					return;
				}
				bool flag4 = this.YAxisType == ChartYAxisType.Primary;
				int count = base.Items.Count;
				if (count < 1)
				{
					return;
				}
				int num = base.Appearance.MajorTick.Length;
				int num2 = base.Appearance.MinorTick.Length;
				float num3;
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					num3 = base.StartPoint.X;
				}
				else
				{
					num3 = base.StartPoint.Y;
					num = -num;
					num2 = -num2;
				}
				this.MajorPoints = new PointF[count];
				this.MinorPoints = new PointF[(count - 1) * base.Appearance.MinorTick.MinorTickCount + count];
				float num8;
				float num9;
				float num11;
				float num13;
				if (this.ScaleBreaks.Enabled && this.Segments.Count >= 2)
				{
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					for (int i = this.Segments.Count - 1; i >= 0; i--)
					{
						int num7 = num6;
						AxisSegment axisSegment = this.Segments[i];
						if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							num8 = axisSegment.StartPoint.Y;
							num9 = axisSegment.EndPoint.Y;
						}
						else
						{
							num8 = axisSegment.StartPoint.X;
							num9 = axisSegment.EndPoint.X;
						}
						double num10 = Math.Abs(axisSegment.MaxValue - axisSegment.MinValue);
						int num12;
						if (axisSegment.Step < num10)
						{
							num11 = (float)((double)(num8 - num9) / (num10 / axisSegment.Step));
							num12 = (int)Math.Round(num10 / axisSegment.Step) + 1;
						}
						else
						{
							num11 = num8 - num9;
							num12 = 2;
						}
						if (axisSegment.axisSegmentPercent <= 15)
						{
							num11 = num8 - num9;
							num12 = 2;
						}
						num13 = num11 / (float)(base.Appearance.MinorTick.MinorTickCount + 1);
						for (int j = 0; j < num12; j++)
						{
							try
							{
								int num14 = 1;
								if ((num6 - num7) % base.LabelStep == 0 || base.Items[num6].Visible)
								{
									float y = (float)Math.Round((double)(num8 - num11 * (float)j));
									this.MajorPoints[num4] = new PointF(num3 + (float)(flag4 ? 0 : num), y);
									PointF[] array = this.MajorPoints;
									if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
									{
										RenderEngine.ChangePlaces(ref array[num4]);
									}
									num4++;
								}
								else
								{
									num14 = 0;
								}
								if (base.Appearance.MinorTick.Visible && j < num12 - 1)
								{
									for (int k = num14; k <= base.Appearance.MinorTick.MinorTickCount; k++)
									{
										float y = (float)Math.Round((double)(num8 - num11 * (float)j - num13 * (float)k));
										this.MinorPoints[num5] = new PointF(num3 + (float)(flag4 ? 0 : num2), y);
										PointF[] array = this.MinorPoints;
										if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
										{
											RenderEngine.ChangePlaces(ref array[num5]);
										}
										num5++;
									}
								}
								num6++;
							}
							catch
							{
							}
						}
					}
					return;
				}
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					num8 = base.StartPoint.Y;
					num9 = base.EndPoint.Y;
				}
				else
				{
					num8 = base.StartPoint.X;
					num9 = base.EndPoint.X;
				}
				if (count > 1)
				{
					num11 = (num8 - num9) / (float)(count - 1);
				}
				else
				{
					num11 = num8 - num9;
				}
				num13 = num11 / (float)(base.Appearance.MinorTick.MinorTickCount + 1);
				int num15 = 0;
				for (int l = 0; l < count; l++)
				{
					int num16 = 1;
					if (l % base.LabelStep == 0)
					{
						float y = (float)Math.Round((double)(num8 - num11 * (float)l));
						this.MajorPoints[l] = new PointF(num3 + (float)(flag4 ? 0 : num), y);
						PointF[] array = this.MajorPoints;
						if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
						{
							RenderEngine.ChangePlaces(ref array[l]);
						}
					}
					else
					{
						num16 = 0;
					}
					if (l < count)
					{
						int num17 = (l == count - 1) ? 0 : base.Appearance.MinorTick.MinorTickCount;
						for (int m = num16; m <= num17; m++)
						{
							float y = (float)Math.Round((double)(num8 - num11 * (float)l - num13 * (float)m));
							this.MinorPoints[num15] = new PointF(num3 + (float)(flag4 ? 0 : num2), y);
							PointF[] array = this.MinorPoints;
							if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
							{
								RenderEngine.ChangePlaces(ref array[num15]);
							}
							num15++;
						}
					}
				}
			}
		}

		// Token: 0x0600E6D6 RID: 59094 RVA: 0x00337C00 File Offset: 0x00335E00
		internal override void Initialize(double min, double max)
		{
			if (this.ScaleBreaks.Enabled && base.AutoScale && !this.IsLogarithmic)
			{
				this.CreateSegments();
			}
			else if (base.AutoScale)
			{
				this.Segments.Clear();
			}
			else
			{
				this.InitializeSegments();
			}
			if (this.IsLogarithmic)
			{
				double num = 0.0;
				double num2 = 1.0;
				if (!base.AutoScale)
				{
					num = ((this.MinValue > 0.0) ? this.MinValue : 0.0);
					num2 = ((this.Step != 0.0) ? Math.Abs(this.Step) : 1.0);
				}
				this.MinValue = (this.chartAxisMinAxisValue = num);
				this.chartAxisItems.Clear();
				double num3;
				do
				{
					num3 = Math.Pow(this.LogarithmBase, num);
					num += num2;
					ChartAxisItem chartAxisItem = new ChartAxisItem();
					chartAxisItem.Value = (decimal)num3;
					chartAxisItem.TextBlock.Text = base.FormatLabel(num3);
					chartAxisItem.Appearance.styleChart = (chartAxisItem.Marker.Appearance.styleChart = base.Chart);
					this.chartAxisItems.Add(chartAxisItem);
				}
				while ((!base.AutoScale && num - num2 < this.MaxValue) || num3 < max || base.Items.Count < 2);
				this.MaxValue = (this.chartAxisMaxAxisValue = num - num2);
				this.Step = num2;
			}
			else
			{
				base.Initialize(min, max);
			}
			this.DisableCachedValues();
		}

		// Token: 0x0600E6D7 RID: 59095 RVA: 0x00337D98 File Offset: 0x00335F98
		private void InitializeSegments()
		{
			if (!this.ScaleBreaks.Enabled || this.Segments.Count <= 1)
			{
				return;
			}
			this.Segments.Sort();
			ChartSeriesCollection chartSeriesCollection = base.Parent.SeriesCollection(this.YAxisType);
			ChartSeriesCollection chartSeriesCollection2 = new ChartSeriesCollection(base.Chart);
			foreach (ChartSeries chartSeries in chartSeriesCollection)
			{
				ChartSeries chartSeries2 = chartSeries.CloneSeries();
				chartSeries2.PrepareSeriesByXValues();
				chartSeriesCollection2.Add(chartSeries2);
			}
			if (chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedBar) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedBar100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedArea) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedArea100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSplineArea) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSplineArea100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.Pie) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.CandleStick) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedLine) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSpline) > 0)
			{
				this.Segments.Clear();
				throw new ChartException("Series types StackedBar, StackedBar100, StackedArea, StackedArea100, StackedSplineArea, StackedSplineArea100, CandleStick and Pie do not support ScaleBreaks.");
			}
			ChartSeriesItemsCollection chartSeriesItemsCollection = new ChartSeriesItemsCollection();
			foreach (ChartSeries chartSeries3 in chartSeriesCollection2)
			{
				chartSeriesItemsCollection.AddRange(chartSeries3.Items);
			}
			chartSeriesItemsCollection.Sort();
			chartSeriesItemsCollection.Filter(base.VisibleValues);
			if (!this.Segments.IsHaveZero)
			{
				base.IsZeroBased = false;
			}
		}

		// Token: 0x0600E6D8 RID: 59096 RVA: 0x00337F2C File Offset: 0x0033612C
		private void CreateSegments()
		{
			if (!base.IsVisible())
			{
				this.Segments.Clear();
				return;
			}
			if (!this.ScaleBreaks.Enabled)
			{
				this.Segments.Clear();
				return;
			}
			ChartSeriesCollection chartSeriesCollection = base.Parent.SeriesCollection(this.YAxisType);
			ChartSeriesCollection chartSeriesCollection2 = new ChartSeriesCollection(base.Chart);
			foreach (ChartSeries chartSeries in chartSeriesCollection)
			{
				ChartSeries chartSeries2 = chartSeries.CloneSeries();
				chartSeries2.PrepareSeriesByXValues();
				chartSeriesCollection2.Add(chartSeries2);
			}
			if (chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedBar) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedBar100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedArea) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedArea100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSplineArea) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSplineArea100) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.Pie) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.CandleStick) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedLine) > 0 || chartSeriesCollection2.GetSeriesCount(ChartSeriesType.StackedSpline) > 0)
			{
				this.Segments.Clear();
				return;
			}
			ChartSeriesItemsCollection chartSeriesItemsCollection = new ChartSeriesItemsCollection();
			foreach (ChartSeries chartSeries3 in chartSeriesCollection2)
			{
				chartSeriesItemsCollection.AddRange(chartSeries3.Items);
			}
			chartSeriesItemsCollection.Sort();
			chartSeriesItemsCollection.Filter(base.VisibleValues);
			if (chartSeriesItemsCollection.Count <= 1)
			{
				this.Segments.Clear();
				return;
			}
			double num = (double)(this.ScaleBreaks.ValueTolerance + 1) / 100.0;
			ChartSeriesItem chartSeriesItem = chartSeriesItemsCollection[0];
			double val = chartSeriesItem.YValue;
			int num2 = 1;
			AxisSegmentCollection axisSegmentCollection = new AxisSegmentCollection();
			int num3 = 1;
			while (num3 <= chartSeriesItemsCollection.Count && num3 >= -1)
			{
				ChartSeriesItem chartSeriesItem2;
				if ((num3 == -1 && num2 < 0) || (num3 == chartSeriesItemsCollection.Count && num2 > 0))
				{
					num = ((chartSeriesItem.YValue < 0.0) ? num : (1.0 - num));
					num += (double)((chartSeriesItem.YValue < 0.0) ? 1 : 0);
					chartSeriesItem2 = new ChartSeriesItem(chartSeriesItem.YValue * num);
				}
				else
				{
					chartSeriesItem2 = chartSeriesItemsCollection[num3];
					if (chartSeriesItem.YValue > 0.0 && chartSeriesItem2.YValue > 0.0 && num2 < 0)
					{
						break;
					}
				}
				if (chartSeriesItem.YValue < 0.0 && chartSeriesItem2.YValue < 0.0 && num2 > 0)
				{
					num2 = -1;
					chartSeriesItem = chartSeriesItemsCollection[chartSeriesItemsCollection.Count - 1];
					val = chartSeriesItem.YValue;
					num3 = chartSeriesItemsCollection.Count - 2;
					chartSeriesItem2 = chartSeriesItemsCollection[num3];
				}
				double num4 = Math.Abs(chartSeriesItem.YValue - chartSeriesItem2.YValue);
				double num5 = num4 * 100.0 / Math.Abs(chartSeriesItem.YValue);
				if (num5 >= (double)this.ScaleBreaks.ValueTolerance)
				{
					AxisSegment axisSegment = new AxisSegment();
					double num6 = Math.Max(val, chartSeriesItem.YValue);
					double num7 = Math.Min(val, chartSeriesItem.YValue);
					axisSegment.MaxValue = ((num6 >= 0.0) ? (num6 * 1.1) : (num6 * 0.8));
					axisSegment.MinValue = ((num7 >= 0.0) ? (num7 * 0.8) : (num7 * 1.1));
					axisSegment.axisSegmentItemsCount = chartSeriesItemsCollection.ItemsInRange(axisSegment.MinValue, axisSegment.MaxValue);
					axisSegment.SetRange(chartSeriesItemsCollection, true);
					ChartSeriesItem chartSeriesItem3 = new ChartSeriesItem();
					chartSeriesItem3.YValue = ((num2 > 0) ? axisSegment.MinValue : axisSegment.MaxValue);
					chartSeriesItem3 = ((num2 > 0) ? chartSeriesItemsCollection.GetItemWithMaxYValue(chartSeriesItem3) : chartSeriesItemsCollection.GetItemWithMinYValue(chartSeriesItem3));
					if (chartSeriesItem3 != null)
					{
						val = chartSeriesItem3.YValue;
					}
					else
					{
						val = (double.IsNaN(this.chartAxisMinAxisValue) ? 0.0 : this.chartAxisMinAxisValue);
					}
					axisSegment.axisSegmentVisibleValues = base.VisibleValues;
					axisSegmentCollection.CheckedAdd(axisSegment);
					if (axisSegmentCollection.Test(chartSeriesItemsCollection))
					{
						break;
					}
				}
				chartSeriesItem = chartSeriesItem2;
				if (chartSeriesItem2.YValue < 0.0 && num2 > 0 && num3 == chartSeriesItemsCollection.Count - 1)
				{
					num2 = -1;
					chartSeriesItem = chartSeriesItemsCollection[chartSeriesItemsCollection.Count - 1];
					val = chartSeriesItem.YValue;
					num3 = chartSeriesItemsCollection.Count - 2;
				}
				num3 += num2;
			}
			if (axisSegmentCollection.Count <= 1)
			{
				this.Segments.Clear();
				return;
			}
			axisSegmentCollection = ChartYAxis.OptimizeSegments(axisSegmentCollection, chartSeriesItemsCollection);
			while (axisSegmentCollection.Count > this.ScaleBreaks.MaxCount + 1)
			{
				SegmentsCombinePriority[] array = new SegmentsCombinePriority[axisSegmentCollection.Count - 1];
				for (int i = 0; i < axisSegmentCollection.Count - 1; i++)
				{
					array[i].priority = axisSegmentCollection[i].MinValue - axisSegmentCollection[i + 1].MaxValue;
					array[i].first = axisSegmentCollection[i];
					array[i].second = axisSegmentCollection[i + 1];
				}
				int num8 = 0;
				for (int j = 1; j < array.Length; j++)
				{
					if (!double.IsNaN(array[j].priority) && array[j].priority < array[num8].priority)
					{
						num8 = j;
					}
				}
				array[num8].priority = double.NaN;
				AxisSegment first = array[num8].first;
				AxisSegment second = array[num8].second;
				first.MaxValue = Math.Max(first.MaxValue, second.MaxValue);
				first.MinValue = Math.Min(first.MinValue, second.MinValue);
				first.axisSegmentItemsCount = chartSeriesItemsCollection.ItemsInRange(first.MinValue, first.MaxValue);
				first.SetRange(chartSeriesItemsCollection, true);
				axisSegmentCollection.Remove(second);
			}
			this.Segments.Clear();
			foreach (AxisSegment segment in axisSegmentCollection)
			{
				if (this.Segments.Count < this.ScaleBreaks.MaxCount + 1)
				{
					this.Segments.CheckedAdd(segment);
				}
			}
			if (this.Segments.Count == 1)
			{
				this.Segments.Clear();
			}
			if (this.Segments.Count > 0)
			{
				if (!this.Segments.IsHaveZero)
				{
					double num9 = double.MaxValue;
					AxisSegment axisSegment2 = null;
					foreach (AxisSegment axisSegment3 in this.Segments)
					{
						double num10 = double.MaxValue;
						if (axisSegment3.MinValue > 0.0 && axisSegment3.MaxValue > 0.0)
						{
							num10 = axisSegment3.MinValue;
						}
						if (axisSegment3.MaxValue < 0.0 && axisSegment3.MinValue < 0.0)
						{
							num10 = -axisSegment3.MaxValue;
						}
						if (num10 < num9)
						{
							num9 = num10;
							axisSegment2 = axisSegment3;
						}
					}
					if (axisSegment2 != null)
					{
						if (axisSegment2.MinValue > 0.0 && axisSegment2.MaxValue > 0.0)
						{
							axisSegment2.MinValue = (this.Segments.IsHaveNegative ? (-num9) : 0.0);
						}
						if (axisSegment2.MaxValue < 0.0 && axisSegment2.MinValue < 0.0)
						{
							axisSegment2.MaxValue = (this.Segments.IsHavePositive ? num9 : 0.0);
						}
						axisSegment2.axisSegmentItemsCount = chartSeriesItemsCollection.ItemsInRange(axisSegment2.MinValue, axisSegment2.MaxValue);
						axisSegment2.SetRange(chartSeriesItemsCollection, axisSegment2.MinValue == 0.0);
					}
				}
				this.chartYAxisScaleBreak.scaleBreakSegments = ChartYAxis.OptimizeSegments(this.Segments, chartSeriesItemsCollection);
				if (this.Segments.Count <= 1)
				{
					this.Segments.Clear();
					return;
				}
				this.Segments.Sort();
			}
		}

		// Token: 0x0600E6D9 RID: 59097 RVA: 0x003387F8 File Offset: 0x003369F8
		private static AxisSegmentCollection OptimizeSegments(AxisSegmentCollection LocalSegments, ChartSeriesItemsCollection items)
		{
			for (int i = 0; i < LocalSegments.Count; i++)
			{
				AxisSegment axisSegment = LocalSegments[i];
				for (int j = 0; j < LocalSegments.Count; j++)
				{
					AxisSegment axisSegment2 = LocalSegments[j];
					if (axisSegment2 != axisSegment && axisSegment.IsIntersection(axisSegment2))
					{
						axisSegment.MaxValue = Math.Max(axisSegment.MaxValue, axisSegment2.MaxValue);
						axisSegment.MinValue = Math.Min(axisSegment.MinValue, axisSegment2.MinValue);
						axisSegment.axisSegmentItemsCount = items.ItemsInRange(axisSegment.MinValue, axisSegment.MaxValue);
						axisSegment.SetRange(items, true);
						axisSegment2.MaxValue = 0.0;
						axisSegment2.MinValue = 0.0;
						axisSegment2.axisSegmentItemsCount = 0;
						i = 0;
					}
				}
			}
			AxisSegmentCollection axisSegmentCollection = new AxisSegmentCollection();
			foreach (AxisSegment axisSegment3 in LocalSegments)
			{
				if (axisSegment3.axisSegmentItemsCount != 0)
				{
					axisSegmentCollection.CheckedAdd(axisSegment3);
				}
			}
			axisSegmentCollection.Sort();
			return axisSegmentCollection;
		}

		// Token: 0x0600E6DA RID: 59098 RVA: 0x00338924 File Offset: 0x00336B24
		internal void CalculateSegmentsPosition()
		{
			if (this.Segments.Count <= 1 || !this.ScaleBreaks.Enabled)
			{
				return;
			}
			ChartSeriesCollection chartSeriesCollection = base.Parent.SeriesCollection(this.YAxisType);
			ChartSeriesItemsCollection chartSeriesItemsCollection = new ChartSeriesItemsCollection();
			foreach (ChartSeries chartSeries in chartSeriesCollection)
			{
				chartSeriesItemsCollection.AddRange(chartSeries.Items);
			}
			chartSeriesItemsCollection.Filter(base.VisibleValues);
			int num = (int)((base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical) ? base.Parent.Appearance.Dimensions.Height.PixelValue : base.Parent.Appearance.Dimensions.Width.PixelValue);
			num -= (this.Segments.Count - 1) * this.ScaleBreaks.Width;
			float num2 = (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical) ? base.StartPoint.Y : base.StartPoint.X;
			for (int i = this.Segments.Count - 1; i >= 0; i--)
			{
				AxisSegment axisSegment = this.Segments[i];
				double num3 = (double)chartSeriesItemsCollection.ItemsInRange(axisSegment.MinValue, axisSegment.MaxValue);
				double num4 = num3 / (double)chartSeriesItemsCollection.Count;
				axisSegment.axisSegmentPercent = (int)(num4 * 100.0);
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					float x = base.StartPoint.X;
					float y = num2;
					axisSegment.StartPoint = new PointF(x, y);
					if (i == this.Segments.Count - 1)
					{
						axisSegment.StartPoint = base.StartPoint;
					}
					num2 -= (float)((double)num * num4);
					y = num2;
					axisSegment.EndPoint = new PointF(x, y);
					num2 -= (float)this.ScaleBreaks.Width;
					if (i == 0)
					{
						axisSegment.EndPoint = base.EndPoint;
					}
					if (this.YAxisType == ChartYAxisType.Primary)
					{
						axisSegment.Rectangle = new RectangleF(axisSegment.EndPoint, new SizeF(base.Parent.Appearance.Dimensions.Width.PixelValue, Math.Abs(axisSegment.EndPoint.Y - axisSegment.StartPoint.Y)));
					}
					else
					{
						axisSegment.Rectangle = new RectangleF(new PointF(axisSegment.EndPoint.X - base.Parent.Appearance.Dimensions.Width.PixelValue, axisSegment.EndPoint.Y), new SizeF(base.Parent.Appearance.Dimensions.Width.PixelValue, Math.Abs(axisSegment.EndPoint.Y - axisSegment.StartPoint.Y)));
					}
				}
				else
				{
					float y2 = base.StartPoint.Y;
					float x2 = num2;
					axisSegment.StartPoint = new PointF(x2, y2);
					if (i == this.Segments.Count - 1)
					{
						axisSegment.StartPoint = base.StartPoint;
					}
					num2 += (float)((double)num * num4);
					x2 = num2;
					axisSegment.EndPoint = new PointF(x2, y2);
					num2 += (float)this.ScaleBreaks.Width;
					if (i == 0)
					{
						axisSegment.EndPoint = base.EndPoint;
					}
					if (this.YAxisType == ChartYAxisType.Primary)
					{
						axisSegment.Rectangle = new RectangleF(new PointF(axisSegment.StartPoint.X, axisSegment.StartPoint.Y - base.Parent.Appearance.Dimensions.Height.PixelValue), new SizeF(Math.Abs(axisSegment.EndPoint.X - axisSegment.StartPoint.X), base.Parent.Appearance.Dimensions.Height.PixelValue));
					}
					else
					{
						axisSegment.Rectangle = new RectangleF(axisSegment.StartPoint, new SizeF(Math.Abs(axisSegment.EndPoint.X - axisSegment.StartPoint.X), base.Parent.Appearance.Dimensions.Height.PixelValue));
					}
				}
			}
			base.Items.Clear();
			for (int j = this.Segments.Count - 1; j >= 0; j--)
			{
				this.Segments[j].GetAxisItems(this);
			}
		}

		// Token: 0x0600E6DB RID: 59099 RVA: 0x00338DF4 File Offset: 0x00336FF4
		internal override float GetCoordinate(double val)
		{
			float num = this.PixelsPerValue;
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				num = -num;
			}
			if (this.IsLogarithmic)
			{
				val = Math.Log(val, this.LogarithmBase);
				val = ((val > 0.0) ? val : 0.0);
				return base.GetCoordinate(val, num, true);
			}
			if (!this.ScaleBreaks.Enabled || this.Segments.Count < 2)
			{
				return base.GetCoordinate(val, num, true);
			}
			AxisSegment axisSegment = this.Segments.Search(val, true);
			if (axisSegment != null)
			{
				float? num2;
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					num2 = axisSegment.GetY(val);
				}
				else
				{
					num2 = axisSegment.GetX(val);
				}
				if (num2 != null)
				{
					return num2.Value;
				}
			}
			return base.GetCoordinate(val, num, true);
		}

		// Token: 0x0600E6DC RID: 59100 RVA: 0x00338EC4 File Offset: 0x003370C4
		internal override float GetZeroCoordinate()
		{
			if (float.IsNaN(this.zeroCoord))
			{
				if (this.Segments.Count > 1 && this.ScaleBreaks.Enabled)
				{
					this.zeroCoord = this.GetCoordinate(this.GetZeroValue());
				}
				else
				{
					this.zeroCoord = base.GetZeroCoordinate();
				}
				this.zeroCoord = (float)Math.Round((double)this.zeroCoord);
			}
			return this.zeroCoord;
		}

		// Token: 0x0600E6DD RID: 59101 RVA: 0x00338F34 File Offset: 0x00337134
		protected internal override float GetAxisStartCoord()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return this.PlotRect.Bottom;
			}
			return this.PlotRect.Left;
		}

		// Token: 0x0600E6DE RID: 59102 RVA: 0x00338F6C File Offset: 0x0033716C
		protected internal override float GetAxisEndCoord()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return this.PlotRect.Top;
			}
			return this.PlotRect.Right;
		}

		// Token: 0x0600E6DF RID: 59103 RVA: 0x00338FA4 File Offset: 0x003371A4
		internal override double GetZeroValue()
		{
			if (this.Segments.Count <= 1 || !this.ScaleBreaks.Enabled)
			{
				return base.GetZeroValue();
			}
			if (this.Segments.IsHaveZero)
			{
				return 0.0;
			}
			return this.Segments.NearZeroValue;
		}

		// Token: 0x17004649 RID: 17993
		// (get) Token: 0x0600E6E0 RID: 59104 RVA: 0x00338FF5 File Offset: 0x003371F5
		internal override ChartAxisType AxisType
		{
			get
			{
				return this.chartAxisType;
			}
		}

		// Token: 0x0600E6E1 RID: 59105 RVA: 0x00339000 File Offset: 0x00337200
		internal override RectangleF GetClientRectangle(PointF startPoint, PointF endPoint)
		{
			RectangleF result = default(RectangleF);
			float num = 0f;
			float num2 = 0f;
			if (base.Items.Count > 0)
			{
				num = this.GetFirstItemHalfDimension();
				num2 = this.GetLastItemHalfDimension();
			}
			if (this.YAxisType == ChartYAxisType.Primary)
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					result.X = 0f;
					result.Y = endPoint.Y - num;
					result.Width = startPoint.X - result.X + base.Appearance.Width;
					result.Height = startPoint.Y - result.Y + num2;
				}
				else
				{
					result.X = startPoint.X - num;
					result.Y = startPoint.Y;
					result.Width = endPoint.X - result.X + num2;
					result.Height = (float)((int)Math.Round((double)base.Parent.Appearance.Dimensions.Margins.Bottom.PixelValue));
				}
			}
			else if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				result.X = endPoint.X;
				result.Y = endPoint.Y - num;
				result.Width = (float)((int)Math.Round((double)base.Parent.Appearance.Dimensions.Margins.Right.PixelValue));
				result.Height = startPoint.Y - result.Y + num2;
			}
			else
			{
				result.X = startPoint.X - num + base.Appearance.Width;
				result.Y = startPoint.Y - (float)((int)Math.Round((double)base.Parent.Appearance.Position.Y));
				result.Width = endPoint.X - result.X + num2;
				result.Height = endPoint.Y - result.Y + base.Appearance.Width;
			}
			return result;
		}

		// Token: 0x0600E6E2 RID: 59106 RVA: 0x00339212 File Offset: 0x00337412
		internal override RectangleF GetClientRectangle()
		{
			return this.GetClientRectangle(this.chartAxisPointStart, this.chartAxisPointEnd);
		}

		// Token: 0x0600E6E3 RID: 59107 RVA: 0x00339228 File Offset: 0x00337428
		internal override float GetFirstItemHalfDimension()
		{
			Unit unit = null;
			int count = base.Items.Count;
			if (count > 0)
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					unit = base.Items[count - 1].Appearance.Dimensions.Height;
				}
				else
				{
					unit = base.Items[0].Appearance.Dimensions.Width;
				}
			}
			if (!(unit != null))
			{
				return 0f;
			}
			return (float)((int)Math.Round((double)(unit.PixelValue / 2f)));
		}

		// Token: 0x0600E6E4 RID: 59108 RVA: 0x003392B4 File Offset: 0x003374B4
		internal override float GetLastItemHalfDimension()
		{
			Unit unit = null;
			int count = base.Items.Count;
			if (count > 0)
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					unit = base.Items[0].Appearance.Dimensions.Height;
				}
				else
				{
					unit = base.Items[count - 1].Appearance.Dimensions.Width;
				}
			}
			if (!(unit != null))
			{
				return 0f;
			}
			return (float)((int)Math.Round((double)(unit.PixelValue / 2f)));
		}

		// Token: 0x0600E6E5 RID: 59109 RVA: 0x0033933F File Offset: 0x0033753F
		internal float GetMaxItemBound()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return base.Items.GetWidth();
			}
			return base.Items.GetHeight();
		}

		// Token: 0x0600E6E6 RID: 59110 RVA: 0x00339368 File Offset: 0x00337568
		internal override void InitializeItems()
		{
			if (!base.IsParentVisible)
			{
				return;
			}
			ChartSeriesCollection yusedSeriesCollection = base.Parent.SeriesCollection().GetYUsedSeriesCollection();
			ChartSeriesCollection clonedXUsedSeriesCollection = yusedSeriesCollection.GetFilteredSeriesByYAxis(this.YAxisType).GetClonedXUsedSeriesCollection();
			ChartValueLimits valueLimits = clonedXUsedSeriesCollection.GetValueLimits();
			this.Initialize(valueLimits.MinYValue, valueLimits.MaxYValue);
		}

		// Token: 0x0600E6E7 RID: 59111 RVA: 0x003393BC File Offset: 0x003375BC
		internal override void CalculateLayout(RenderEngine renderEngine)
		{
			if (!base.IsParentVisible)
			{
				return;
			}
			Position position = RenderEngine.LocalToGlobal(base.Parent);
			bool flag = base.IsVisible();
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				if (this.YAxisType == ChartYAxisType.Primary)
				{
					base.StartPoint = new PointF((float)Math.Round((double)position.X), (float)Math.Round((double)(base.Parent.Appearance.Dimensions.Height.PixelValue + position.Y)));
					base.EndPoint = new PointF(base.StartPoint.X, (float)Math.Round((double)position.Y));
				}
				else if (this.YAxisType == ChartYAxisType.Secondary)
				{
					base.StartPoint = new PointF((float)Math.Round((double)(base.Parent.Appearance.Dimensions.Width.PixelValue + position.X)), (float)Math.Round((double)(base.Parent.Appearance.Dimensions.Height.PixelValue + position.Y)));
					base.EndPoint = new PointF(base.StartPoint.X, (float)Math.Round((double)position.Y));
				}
			}
			else if (this.YAxisType == ChartYAxisType.Primary)
			{
				base.StartPoint = new PointF((float)Math.Round((double)position.X), (float)Math.Round((double)(base.Parent.Appearance.Dimensions.Height.PixelValue + position.Y)));
				base.EndPoint = new PointF((float)Math.Round((double)(base.Parent.Appearance.Dimensions.Width.PixelValue + position.X)), base.StartPoint.Y);
			}
			else if (this.YAxisType == ChartYAxisType.Secondary)
			{
				base.StartPoint = new PointF((float)Math.Round((double)position.X), (float)Math.Round((double)position.Y));
				base.EndPoint = new PointF((float)Math.Round((double)(base.Parent.Appearance.Dimensions.Width.PixelValue + position.X)), base.StartPoint.Y);
			}
			if (flag)
			{
				this.CalculateSegmentsPosition();
				this.CreateSegmentsRenderingRegions(renderEngine);
				this.CalculateAxisItemsLayout(renderEngine, renderEngine.getAxisItemBoundOnly, null);
				if (base.Parent.Chart.AutoLayoutWrapper)
				{
					this.CalculateAxisItemsLayout(renderEngine, renderEngine.getAxisItemBoundOnly, new float?(this.GetMaxItemBound()));
				}
			}
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				if (base.AxisLabel.Appearance.RotationAngle == 270f)
				{
					base.AxisLabel.Appearance.RotationAngle = 0f;
				}
			}
			else if (base.AxisLabel.Appearance.RotationAngle == 0f)
			{
				base.AxisLabel.Appearance.RotationAngle = 270f;
			}
			if (base.AxisLabel.Appearance.Visible && base.AxisLabel.IsVisible())
			{
				base.CalculateAxisLabel(renderEngine);
			}
		}

		// Token: 0x0600E6E8 RID: 59112 RVA: 0x003396D8 File Offset: 0x003378D8
		private void CalculateAxisItemsLayout(RenderEngine renderEngine, bool getItemBoundOnly, float? maxBound)
		{
			float num;
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				num = base.StartPoint.X;
			}
			else
			{
				num = base.StartPoint.Y;
			}
			int count = base.Items.Count;
			if (count > 0)
			{
				ChartAxisItem chartAxisItem = new ChartAxisItem();
				int ticksLength = base.TicksLength;
				float num2 = 0f;
				if (maxBound == null)
				{
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						if (this.YAxisType == ChartYAxisType.Primary)
						{
							num2 += base.Parent.Appearance.Position.X - (float)ticksLength;
						}
						else
						{
							num2 += base.Chart.Appearance.Dimensions.Width.PixelValue - base.Parent.Appearance.Position.X - base.Parent.Appearance.Dimensions.Width.PixelValue + (float)ticksLength;
						}
					}
					else if (this.YAxisType == ChartYAxisType.Primary)
					{
						num2 += base.Chart.Appearance.Dimensions.Height.PixelValue - base.Parent.Appearance.Position.Y - base.Parent.Appearance.Dimensions.Height.PixelValue - (float)ticksLength;
						if (base.Parent.DataTable.IsVisible && base.Parent.DataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative)
						{
							num2 -= base.Parent.DataTable.Appearance.Dimensions.Height.PixelValue + base.Parent.DataTable.Appearance.Dimensions.Margins.Top.PixelValue + base.Parent.DataTable.Appearance.Border.Width * (float)base.Parent.DataTable.SizesH.Length;
						}
					}
					else
					{
						num2 += base.Parent.Appearance.Position.Y - (float)ticksLength;
					}
				}
				else
				{
					num2 = maxBound.Value;
				}
				int num3 = 0;
				for (int i = 0; i < count; i++)
				{
					ChartAxisItem chartAxisItem2 = base.Items[i];
					if (num3 % base.LabelStep != 0 && chartAxisItem2.chartAxisItemType == ChartAxisItemType.Normal)
					{
						chartAxisItem2.Visible = false;
						num3++;
					}
					else
					{
						num3 = 1;
						if (this.CheckAxisItemVisibility(chartAxisItem2))
						{
							float coordinate = this.GetCoordinate((double)chartAxisItem2.Value);
							if (string.IsNullOrEmpty(chartAxisItem2.TextBlock.Text))
							{
								chartAxisItem2.TextBlock.Text = base.FormatLabel(Convert.ToDouble(chartAxisItem2.Value));
							}
							chartAxisItem2.Container = this.chartAxisParent.Chart;
							float rotationAngle = chartAxisItem2.Appearance.RotationAngle;
							if (chartAxisItem2.Appearance.RotationAngle == chartAxisItem.Appearance.RotationAngle)
							{
								float rotationAngle2 = base.Appearance.LabelAppearance.RotationAngle;
							}
							chartAxisItem2.Appearance.Position.Copy = chartAxisItem2.Appearance.Position;
							Dimensions dimensions;
							if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								dimensions = new Dimensions(num2, this.GetPixelStep(chartAxisItem2.Value));
								if (this.YAxisType == ChartYAxisType.Secondary && maxBound == null)
								{
									dimensions.Width.Value -= (float)(ticksLength * 2);
								}
							}
							else
							{
								dimensions = new Dimensions(this.GetPixelStep(chartAxisItem2.Value), num2);
							}
							if (chartAxisItem2.Appearance.Position.AlignedPosition == chartAxisItem.Appearance.Position.AlignedPosition)
							{
								chartAxisItem2.Appearance.Position.AlignedPosition = base.Appearance.LabelAppearance.Position.AlignedPosition;
							}
							if (chartAxisItem2.Appearance.Position.Auto == chartAxisItem.Appearance.Position.Auto)
							{
								chartAxisItem2.Appearance.Position.Auto = base.Appearance.LabelAppearance.Position.Auto;
							}
							if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								if (chartAxisItem2.Appearance.Position.X == chartAxisItem.Appearance.Position.X)
								{
									chartAxisItem2.Appearance.Position.X = base.Appearance.LabelAppearance.Position.X;
								}
								if (chartAxisItem2.Appearance.Position.AlignedPosition == AlignedPositions.None)
								{
									if (this.YAxisType == ChartYAxisType.Primary)
									{
										chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Right;
									}
									else
									{
										chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Left;
									}
								}
								chartAxisItem2.TextBlock.textBlockWrapContext = new WrapContext(dimensions.Width.PixelValue, dimensions.Height.PixelValue, WrapType.FixedHeight);
								if (this.shouldOptimizeMaxLength)
								{
									((TextBlockAxisItem)chartAxisItem2.TextBlock).DefineMaxLengthAuto(renderEngine);
								}
							}
							else
							{
								if (chartAxisItem2.Appearance.Position.Y == chartAxisItem.Appearance.Position.Y)
								{
									chartAxisItem2.Appearance.Position.Y = 0f;
								}
								if (chartAxisItem2.Appearance.Position.AlignedPosition == AlignedPositions.None)
								{
									if (this.YAxisType == ChartYAxisType.Primary)
									{
										chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Top;
									}
									else
									{
										chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Bottom;
									}
								}
								chartAxisItem2.TextBlock.textBlockWrapContext = new WrapContext(dimensions.Width.PixelValue, dimensions.Height.PixelValue, WrapType.FixedWidth);
							}
							AutoTextWrap autoTextWrap = chartAxisItem2.TextBlock.Appearance.AutoTextWrap;
							if (chartAxisItem2.TextBlock.Appearance.AutoTextWrap == chartAxisItem.TextBlock.Appearance.AutoTextWrap)
							{
								chartAxisItem2.TextBlock.Appearance.AutoTextWrap = base.Appearance.TextAppearance.AutoTextWrap;
							}
							if (chartAxisItem2.Appearance.Dimensions.AutoSize && base.Appearance.LabelAppearance.Dimensions.AutoSize)
							{
								SizeF sizeF = chartAxisItem2.Measure(renderEngine, chartAxisItem);
								chartAxisItem2.Appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
							}
							chartAxisItem2.CalculatePosition(dimensions);
							if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
							{
								if (this.YAxisType == ChartYAxisType.Secondary && chartAxisItem2.Appearance.Position.Auto)
								{
									chartAxisItem2.Appearance.Position.X += num + (float)ticksLength;
								}
								if (chartAxisItem2.Appearance.Position.Auto)
								{
									chartAxisItem2.Appearance.Position.Y = coordinate - chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 2f;
								}
								if (maxBound != null && this.YAxisType == ChartYAxisType.Primary && chartAxisItem2.Appearance.Position.Auto)
								{
									chartAxisItem2.Appearance.Position.X += base.Parent.Appearance.Dimensions.Margins.Left.PixelValue - maxBound.Value - (float)ticksLength;
								}
							}
							else
							{
								if (chartAxisItem2.Appearance.Position.Auto)
								{
									chartAxisItem2.Appearance.Position.Y += ((this.YAxisType == ChartYAxisType.Primary) ? (num + (float)ticksLength) : chartAxisItem2.Appearance.Dimensions.Margins.Bottom.PixelValue);
									chartAxisItem2.Appearance.Position.X = coordinate - chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f;
								}
								if (maxBound != null && this.YAxisType == ChartYAxisType.Secondary && chartAxisItem2.Appearance.Position.Auto)
								{
									chartAxisItem2.Appearance.Position.Y += base.Parent.Appearance.Dimensions.Margins.Top.PixelValue - maxBound.Value;
								}
							}
							chartAxisItem2.TextBlock.Appearance.Position.Copy = chartAxisItem2.TextBlock.Appearance.Position;
							if (chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition == chartAxisItem.TextBlock.Appearance.Position.AlignedPosition)
							{
								chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition = base.Appearance.TextAppearance.Position.AlignedPosition;
							}
							if (base.Parent.Chart.ShouldApplyTextWrapping(chartAxisItem2.TextBlock.Appearance.AutoTextWrap))
							{
								chartAxisItem2.CorrectTextBlockAlignedPosition(this.YAxisType == ChartYAxisType.Primary && base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical);
							}
							chartAxisItem2.TextBlock.CalculatePosition(renderEngine);
							chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition = chartAxisItem2.TextBlock.Appearance.Position.Copy.AlignedPosition;
							chartAxisItem2.TextBlock.Appearance.AutoTextWrap = autoTextWrap;
							chartAxisItem2.Appearance.Position.AlignedPosition = chartAxisItem2.Appearance.Position.Copy.AlignedPosition;
							chartAxisItem2.Appearance.Position.Auto = chartAxisItem2.Appearance.Position.Copy.Auto;
						}
					}
				}
			}
		}

		// Token: 0x0600E6E9 RID: 59113 RVA: 0x0033A09C File Offset: 0x0033829C
		internal float GetPixelStep(decimal itemValue)
		{
			float value = 0f;
			if (this.ScaleBreaks.Enabled && this.Segments.Count >= 2)
			{
				AxisSegment axisSegment = this.Segments.Search((double)itemValue, true);
				if (axisSegment != null)
				{
					float num;
					float num2;
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						num = axisSegment.StartPoint.Y;
						num2 = axisSegment.EndPoint.Y;
					}
					else
					{
						num = axisSegment.StartPoint.X;
						num2 = axisSegment.EndPoint.X;
					}
					double num3 = Math.Abs(axisSegment.MaxValue - axisSegment.MinValue);
					if (axisSegment.Step < num3)
					{
						value = (float)((double)(num - num2) / (num3 / axisSegment.Step));
					}
					else
					{
						value = num - num2;
					}
					if (axisSegment.axisSegmentPercent <= 15)
					{
						value = num - num2;
					}
				}
			}
			else
			{
				float num;
				float num2;
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					num = base.StartPoint.Y;
					num2 = base.EndPoint.Y;
				}
				else
				{
					num = base.StartPoint.X;
					num2 = base.EndPoint.X;
				}
				if (base.Items.Count > 1)
				{
					value = (num - num2) / (float)(base.Items.Count - 1);
				}
				else
				{
					value = num - num2;
				}
			}
			return Math.Abs(value);
		}

		// Token: 0x0600E6EA RID: 59114 RVA: 0x0033A1FC File Offset: 0x003383FC
		private void CreateSegmentsRenderingRegions(RenderEngine renderEngine)
		{
			if (this.Segments.Count <= 1 || !this.ScaleBreaks.Enabled || !base.IsVisible())
			{
				return;
			}
			try
			{
				if (base.Parent.PlotRegionCommon == null)
				{
					base.Parent.PlotRegionCommon = new Region(renderEngine.GetRenderRegion(base.Parent).GetRegionData());
				}
				if (base.Parent.PlotRegionYAxisPrimary == null)
				{
					base.Parent.PlotRegionYAxisPrimary = new Region(renderEngine.GetRenderRegion(base.Parent).GetRegionData());
				}
				if (base.Parent.PlotRegionYAxisSecondary == null)
				{
					base.Parent.PlotRegionYAxisSecondary = new Region(renderEngine.GetRenderRegion(base.Parent).GetRegionData());
				}
				if (this.Segments.Count > 0)
				{
					bool flag = base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical;
					double length = (double)(flag ? base.Parent.Appearance.Dimensions.Width.PixelValue : base.Parent.Appearance.Dimensions.Height.PixelValue);
					GraphicsPath linePath = this.ScaleBreaks.CreateScaleBreakLine(length, flag);
					GraphicsPath graphicsPath = new GraphicsPath();
					for (int i = this.Segments.Count - 1; i >= 0; i--)
					{
						AxisSegment axisSegment = this.Segments[i];
						bool startLine = i != 0;
						bool endLine = i != this.Segments.Count - 1;
						graphicsPath.AddPath(axisSegment.GetPath(linePath, startLine, endLine, flag), true);
					}
					base.Parent.PlotRegionCommon.Intersect(graphicsPath);
					if (this.YAxisType == ChartYAxisType.Primary)
					{
						base.Parent.PlotRegionYAxisPrimary.Intersect(graphicsPath);
					}
					else
					{
						base.Parent.PlotRegionYAxisSecondary.Intersect(graphicsPath);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04004259 RID: 16985
		private ScaleBreak chartYAxisScaleBreak;

		// Token: 0x0400425A RID: 16986
		private ChartAxisType chartAxisType;

		// Token: 0x0400425B RID: 16987
		private PointF[] chartYAxisMajorPoints;

		// Token: 0x0400425C RID: 16988
		private PointF[] chartYAxisMinorPoints;
	}
}
