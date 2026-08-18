using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001729 RID: 5929
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class ChartXAxis : ChartAxis
	{
		// Token: 0x17004633 RID: 17971
		// (get) Token: 0x0600E696 RID: 59030 RVA: 0x00335573 File Offset: 0x00333773
		// (set) Token: 0x0600E697 RID: 59031 RVA: 0x00335594 File Offset: 0x00333794
		[DefaultValue("")]
		[Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("The data source column used as axis items labels source")]
		public string DataLabelsColumn
		{
			get
			{
				return (string)(base.ViewState["DataLabelsColumn"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) == 0 || string.IsNullOrEmpty(value))
				{
					base.AutoScale = true;
					base.ViewState.Remove("DataLabelsColumn");
				}
				else
				{
					base.AutoScale = false;
					base.ViewState["DataLabelsColumn"] = value;
				}
				if (base.Chart.DesignTime)
				{
					try
					{
						if (base.Chart.DataManager.DataSource != null)
						{
							base.Chart.DataManager.IsDataBindCalled = false;
							base.Chart.DataManager.UseAutoBind = false;
							base.Chart.DataManager.DataBind();
						}
					}
					finally
					{
						base.Chart.DataManager.UseAutoBind = true;
					}
				}
			}
		}

		// Token: 0x17004634 RID: 17972
		// (get) Token: 0x0600E698 RID: 59032 RVA: 0x00335660 File Offset: 0x00333860
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDataBound
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataLabelsColumn);
			}
		}

		// Token: 0x17004635 RID: 17973
		// (get) Token: 0x0600E699 RID: 59033 RVA: 0x00335670 File Offset: 0x00333870
		// (set) Token: 0x0600E69A RID: 59034 RVA: 0x00335691 File Offset: 0x00333891
		[DefaultValue(ChartAxisLayoutMode.Between)]
		[Description("Specifies the layout style of the axis.")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public ChartAxisLayoutMode LayoutMode
		{
			get
			{
				return (ChartAxisLayoutMode)(base.ViewState["LayoutMode"] ?? ChartAxisLayoutMode.Between);
			}
			set
			{
				base.ViewState["LayoutMode"] = value;
			}
		}

		// Token: 0x17004636 RID: 17974
		// (get) Token: 0x0600E69B RID: 59035 RVA: 0x003356A9 File Offset: 0x003338A9
		// (set) Token: 0x0600E69C RID: 59036 RVA: 0x003356CA File Offset: 0x003338CA
		[DefaultValue(true)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public bool AutoShrink
		{
			get
			{
				return (bool)(base.ViewState["AutoShrink"] ?? true);
			}
			set
			{
				base.ViewState["AutoShrink"] = value;
			}
		}

		// Token: 0x17004637 RID: 17975
		// (get) Token: 0x0600E69D RID: 59037 RVA: 0x003356E4 File Offset: 0x003338E4
		internal override float ItemsBound
		{
			get
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					return Math.Max(0f, base.StartPoint.Y + (float)base.TicksLength + base.Items.GetHeight());
				}
				return Math.Max(0f, base.StartPoint.X - (float)base.TicksLength - base.Items.GetWidth());
			}
		}

		// Token: 0x0600E69E RID: 59038 RVA: 0x00335759 File Offset: 0x00333959
		public ChartXAxis(ChartPlotArea parent) : this(parent, parent)
		{
		}

		// Token: 0x0600E69F RID: 59039 RVA: 0x00335764 File Offset: 0x00333964
		public ChartXAxis(ChartPlotArea parent, IContainer container) : base(parent, container)
		{
			this.chartAxisAppearance = new StyleAxisX(this);
			this.chartAxisLabel = new AxisLabel(this, base.Chart);
			this.chartAxisLabel.Appearance.RotationAngle = 0f;
			this.chartAxisLabel.Appearance.styleChart = (this.chartAxisLabel.Marker.Appearance.styleChart = base.Chart);
			this.chartXAxisOrderingMode = BarOrderingMode.Classic;
		}

		// Token: 0x17004638 RID: 17976
		// (get) Token: 0x0600E6A0 RID: 59040 RVA: 0x003357E8 File Offset: 0x003339E8
		// (set) Token: 0x0600E6A1 RID: 59041 RVA: 0x003357F0 File Offset: 0x003339F0
		internal BarOrderingMode OrderingMode
		{
			get
			{
				return this.chartXAxisOrderingMode;
			}
			set
			{
				this.chartXAxisOrderingMode = value;
			}
		}

		// Token: 0x0600E6A2 RID: 59042 RVA: 0x003357FC File Offset: 0x003339FC
		protected internal float GetPixelStep()
		{
			if (float.IsNaN(this.pixelStep))
			{
				int num = base.Items.Count;
				if (num > 0)
				{
					float num2;
					if (this.chartAxisAppearance.Orientation == Orientation.Vertical)
					{
						num2 = this.chartAxisPointStart.Y - this.chartAxisPointEnd.Y;
					}
					else
					{
						num2 = this.chartAxisPointStart.X - this.chartAxisPointEnd.X;
					}
					if (this.LayoutMode == ChartAxisLayoutMode.Inside)
					{
						num++;
					}
					else if (this.LayoutMode == ChartAxisLayoutMode.Normal && num > 1)
					{
						num--;
					}
					this.pixelStep = Math.Abs(num2 / (float)num);
				}
				else
				{
					this.pixelStep = 1f;
				}
			}
			return this.pixelStep;
		}

		// Token: 0x0600E6A3 RID: 59043 RVA: 0x003358AC File Offset: 0x00333AAC
		internal override float GetCoordinate(double val)
		{
			float num = this.PixelsPerValue;
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num = -num;
			}
			float coordinate = base.GetCoordinate(val, num, false);
			if (this.chartAxisMinAxisValue < 0.0 && this.chartAxisMaxAxisValue > 0.0)
			{
				return coordinate;
			}
			float num2 = this.GetPixelStep();
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				num2 = -num2;
			}
			if (this.chartAxisMinAxisValue < 0.0 && this.chartAxisMaxAxisValue <= 0.0 && this.LayoutMode == ChartAxisLayoutMode.Between)
			{
				return coordinate - num2 / 2f;
			}
			switch (this.LayoutMode)
			{
			case ChartAxisLayoutMode.Inside:
				if (val > 0.0 || (this.chartAxisMinAxisValue == 0.0 && val == 0.0))
				{
					return coordinate + num2;
				}
				return coordinate - num2;
			case ChartAxisLayoutMode.Between:
				return coordinate + num2 / 2f;
			}
			return coordinate;
		}

		// Token: 0x17004639 RID: 17977
		// (get) Token: 0x0600E6A4 RID: 59044 RVA: 0x003359A4 File Offset: 0x00333BA4
		internal override float PixelsPerValue
		{
			get
			{
				if (float.IsNaN(this.pixelsPerValue))
				{
					float num = this.chartAxisPointStart.X - this.chartAxisPointEnd.X;
					float num2 = this.chartAxisPointStart.Y - this.chartAxisPointEnd.Y;
					double num3 = Math.Abs(this.chartAxisMaxAxisValue - this.chartAxisMinAxisValue);
					if (num3 == 0.0)
					{
						num3 = 1.0;
					}
					double num4 = Math.Sqrt((double)(num * num + num2 * num2));
					switch (this.LayoutMode)
					{
					case ChartAxisLayoutMode.Inside:
						this.pixelsPerValue = (float)((num4 - (double)(this.GetPixelStep() * 2f)) / num3);
						goto IL_CC;
					case ChartAxisLayoutMode.Between:
						this.pixelsPerValue = (float)((num4 - (double)this.GetPixelStep()) / num3);
						goto IL_CC;
					}
					this.pixelsPerValue = (float)(num4 / num3);
				}
				IL_CC:
				return this.pixelsPerValue;
			}
		}

		// Token: 0x0600E6A5 RID: 59045 RVA: 0x00335A84 File Offset: 0x00333C84
		internal override float GetZeroCoordinate()
		{
			if (float.IsNaN(this.zeroCoord))
			{
				if (double.IsInfinity(this.chartAxisMinAxisValue) || double.IsNaN(this.chartAxisMinAxisValue))
				{
					this.zeroCoord = this.GetAxisStartCoord();
				}
				else if (this.chartAxisMinAxisValue >= 0.0)
				{
					this.zeroCoord = this.GetAxisStartCoord();
				}
				else if (this.chartAxisMaxAxisValue <= 0.0)
				{
					this.zeroCoord = this.GetAxisEndCoord();
				}
				else
				{
					float num = this.GetPixelStep();
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						this.zeroCoord = this.GetAxisStartCoord() + Math.Abs((float)this.chartAxisMinAxisValue) * this.PixelsPerValue;
					}
					else
					{
						num = -num;
						this.zeroCoord = this.GetAxisStartCoord() - Math.Abs((float)this.chartAxisMinAxisValue) * this.PixelsPerValue;
					}
					switch (this.LayoutMode)
					{
					case ChartAxisLayoutMode.Inside:
						this.zeroCoord += num;
						break;
					case ChartAxisLayoutMode.Between:
						this.zeroCoord += num / 2f;
						break;
					}
				}
				this.zeroCoord = (float)Math.Round((double)this.zeroCoord);
			}
			return this.zeroCoord;
		}

		// Token: 0x0600E6A6 RID: 59046 RVA: 0x00335BC0 File Offset: 0x00333DC0
		protected internal override float GetAxisStartCoord()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return (float)Math.Round((double)this.PlotRect.Left);
			}
			return (float)Math.Round((double)this.PlotRect.Bottom);
		}

		// Token: 0x0600E6A7 RID: 59047 RVA: 0x00335C08 File Offset: 0x00333E08
		protected internal override float GetAxisEndCoord()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return (float)Math.Round((double)this.PlotRect.Right);
			}
			return (float)Math.Round((double)this.PlotRect.Top);
		}

		// Token: 0x0600E6A8 RID: 59048 RVA: 0x00335C4D File Offset: 0x00333E4D
		internal int GetFreePositions()
		{
			return this.chartAxisItems.Count - 2;
		}

		// Token: 0x0600E6A9 RID: 59049 RVA: 0x00335C5C File Offset: 0x00333E5C
		internal int GetMarksCount()
		{
			ChartAxisLayoutMode layoutMode = this.LayoutMode;
			if (layoutMode == ChartAxisLayoutMode.Between)
			{
				return base.Items.Count - 1;
			}
			return base.Items.Count;
		}

		// Token: 0x0600E6AA RID: 59050 RVA: 0x00335C90 File Offset: 0x00333E90
		internal float GetStartCoordinate()
		{
			float num = this.GetPixelStep();
			float num2;
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				num2 = base.StartPoint.X;
			}
			else
			{
				num = -num;
				num2 = base.StartPoint.Y;
			}
			switch (this.LayoutMode)
			{
			case ChartAxisLayoutMode.Normal:
				return num2;
			case ChartAxisLayoutMode.Inside:
				return num2 + num;
			case ChartAxisLayoutMode.Between:
				return num2 + num / 2f;
			default:
				return 0f;
			}
		}

		// Token: 0x0600E6AB RID: 59051 RVA: 0x00335D06 File Offset: 0x00333F06
		protected override void DisableCachedValues()
		{
			base.DisableCachedValues();
			this.pixelStep = float.NaN;
		}

		// Token: 0x1700463A RID: 17978
		// (get) Token: 0x0600E6AC RID: 59052 RVA: 0x00335D19 File Offset: 0x00333F19
		internal override ChartAxisType AxisType
		{
			get
			{
				return ChartAxisType.XAxis;
			}
		}

		// Token: 0x1700463B RID: 17979
		// (get) Token: 0x0600E6AD RID: 59053 RVA: 0x00335D1C File Offset: 0x00333F1C
		internal override bool IsMinorTickVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700463C RID: 17980
		// (get) Token: 0x0600E6AE RID: 59054 RVA: 0x00335D20 File Offset: 0x00333F20
		internal override bool IsMajorTickVisible
		{
			get
			{
				return (base.Chart.SeriesOrientation != ChartSeriesOrientation.Vertical || !base.Parent.DataTable.Visible || base.Parent.DataTable.Appearance.RenderType != TableRenderType.PlotAreaRelative) && base.IsMajorTickVisible;
			}
		}

		// Token: 0x0600E6AF RID: 59055 RVA: 0x00335D6C File Offset: 0x00333F6C
		internal override RectangleF GetClientRectangle(PointF startPoint, PointF endPoint)
		{
			RectangleF result = default(RectangleF);
			float num = 0f;
			float num2 = 0f;
			if (this.LayoutMode == ChartAxisLayoutMode.Normal && base.Items.Count > 0)
			{
				num = this.GetFirstItemHalfDimension();
				num2 = this.GetLastItemHalfDimension();
			}
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				result.X = startPoint.X - num;
				result.Y = startPoint.Y;
				result.Width = endPoint.X - result.X + num2;
				result.Height = (float)((int)Math.Round((double)base.Parent.Appearance.Dimensions.Margins.Bottom.PixelValue));
			}
			else
			{
				result.X = 0f;
				result.Y = endPoint.Y - num;
				result.Width = startPoint.X + base.Appearance.Width;
				result.Height = startPoint.Y - result.Y + num2;
			}
			return result;
		}

		// Token: 0x0600E6B0 RID: 59056 RVA: 0x00335E73 File Offset: 0x00334073
		internal override RectangleF GetClientRectangle()
		{
			return this.GetClientRectangle(this.chartAxisPointStart, this.chartAxisPointEnd);
		}

		// Token: 0x0600E6B1 RID: 59057 RVA: 0x00335E88 File Offset: 0x00334088
		internal override float GetFirstItemHalfDimension()
		{
			Unit unit = null;
			int count = base.Items.Count;
			if (count > 0)
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					unit = base.Items[0].Appearance.Dimensions.Width;
				}
				else
				{
					unit = base.Items[count - 1].Appearance.Dimensions.Height;
				}
			}
			if (!(unit != null))
			{
				return 0f;
			}
			return (float)((int)Math.Round((double)(unit.PixelValue / 2f)));
		}

		// Token: 0x0600E6B2 RID: 59058 RVA: 0x00335F14 File Offset: 0x00334114
		internal override float GetLastItemHalfDimension()
		{
			Unit unit = null;
			int count = base.Items.Count;
			if (count > 0)
			{
				if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					unit = base.Items[count - 1].Appearance.Dimensions.Width;
				}
				else
				{
					unit = base.Items[0].Appearance.Dimensions.Height;
				}
			}
			if (!(unit != null))
			{
				return 0f;
			}
			return (float)((int)Math.Round((double)(unit.PixelValue / 2f)));
		}

		// Token: 0x0600E6B3 RID: 59059 RVA: 0x00335F9F File Offset: 0x0033419F
		internal float GetMaxItemBound()
		{
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return base.Items.GetHeight();
			}
			return base.Items.GetWidth();
		}

		// Token: 0x0600E6B4 RID: 59060 RVA: 0x00335FC8 File Offset: 0x003341C8
		internal override void InitializeItems()
		{
			if (!base.IsParentVisible)
			{
				return;
			}
			ChartSeriesCollection xusedSeriesCollection = base.Parent.SeriesCollection().GetXUsedSeriesCollection();
			ChartValueLimits valueLimits = xusedSeriesCollection.GetValueLimits();
			this.Initialize(valueLimits.MinXValue, valueLimits.MaxXValue);
		}

		// Token: 0x0600E6B5 RID: 59061 RVA: 0x00336008 File Offset: 0x00334208
		internal override void CalculateLayout(RenderEngine renderEngine)
		{
			if (!base.IsParentVisible)
			{
				return;
			}
			this.CalculateAxisItemsLayout(renderEngine, null);
			if (base.Parent.Chart.AutoLayoutWrapper)
			{
				this.CalculateAxisItemsLayout(renderEngine, new float?(this.GetMaxItemBound()));
			}
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
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
			if (base.AxisLabel.IsVisible())
			{
				base.CalculateAxisLabel(renderEngine);
			}
		}

		// Token: 0x0600E6B6 RID: 59062 RVA: 0x003360D0 File Offset: 0x003342D0
		internal override bool CheckAxisItemVisibility(ChartAxisItem item)
		{
			return (base.Chart.SeriesOrientation != ChartSeriesOrientation.Vertical || !base.Parent.DataTable.Visible || base.Parent.DataTable.Appearance.RenderType != TableRenderType.PlotAreaRelative) && base.CheckAxisItemVisibility(item);
		}

		// Token: 0x0600E6B7 RID: 59063 RVA: 0x00336120 File Offset: 0x00334320
		private void CalculateAxisItemsLayout(RenderEngine renderEngine, float? maxBound)
		{
			int ticksLength = base.TicksLength;
			Position position = RenderEngine.LocalToGlobal(base.Parent);
			base.StartPoint = new PointF((float)Math.Round((double)position.X), (float)Math.Round((double)(base.Parent.Appearance.Dimensions.Height.PixelValue + position.Y)));
			float num;
			float num2;
			if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				base.EndPoint = new PointF((float)Math.Round((double)(base.Parent.Appearance.Dimensions.Width.PixelValue + position.X)), (float)Math.Round((double)(base.Parent.Appearance.Dimensions.Height.PixelValue + position.Y)));
				num = this.GetPixelStep();
				num2 = base.StartPoint.X;
			}
			else
			{
				base.Appearance.Orientation = Orientation.Vertical;
				base.EndPoint = new PointF((float)Math.Round((double)position.X), (float)Math.Round((double)position.Y));
				num = -this.GetPixelStep();
				num2 = base.StartPoint.Y;
			}
			if (this.LayoutMode == ChartAxisLayoutMode.Inside)
			{
				num2 += num;
			}
			ChartAxisItem chartAxisItem = new ChartAxisItem();
			int count = base.Items.Count;
			for (int i = 0; i < count; i++)
			{
				ChartAxisItem chartAxisItem2 = base.Items[i];
				if (i % base.LabelStep != 0)
				{
					chartAxisItem2.Visible = false;
				}
				else if (this.CheckAxisItemVisibility(chartAxisItem2))
				{
					chartAxisItem2.TextBlock.textBlockWrappedText = string.Empty;
					if (string.IsNullOrEmpty(chartAxisItem2.TextBlock.Text))
					{
						chartAxisItem2.TextBlock.Text = base.FormatLabel(Convert.ToDouble(chartAxisItem2.Value));
					}
					chartAxisItem2.Container = base.Chart;
					Position position2 = (Position)chartAxisItem2.Appearance.Position.Clone();
					if (chartAxisItem2.Appearance.Position.AlignedPosition == chartAxisItem.Appearance.Position.AlignedPosition)
					{
						chartAxisItem2.Appearance.Position.AlignedPosition = base.Appearance.LabelAppearance.Position.AlignedPosition;
					}
					if (chartAxisItem2.Appearance.Position.Auto == chartAxisItem.Appearance.Position.Auto)
					{
						chartAxisItem2.Appearance.Position.Auto = base.Appearance.LabelAppearance.Position.Auto;
					}
					Dimensions dimensions;
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						float height = (maxBound != null) ? (maxBound.Value + (float)ticksLength) : (base.Chart.Appearance.Dimensions.Height.PixelValue - base.Parent.Appearance.Position.Y - base.Parent.Appearance.Dimensions.Height.PixelValue - (float)ticksLength);
						dimensions = new Dimensions(this.GetPixelStep(), height);
						if (chartAxisItem2.Appearance.Position.Y == chartAxisItem.Appearance.Position.Y)
						{
							chartAxisItem2.Appearance.Position.Y = base.Appearance.LabelAppearance.Position.Y;
						}
						if (chartAxisItem2.Appearance.Position.AlignedPosition == AlignedPositions.None)
						{
							chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Top;
						}
						chartAxisItem2.TextBlock.textBlockWrapContext = new WrapContext(dimensions.Width.PixelValue, dimensions.Height.PixelValue, WrapType.FixedWidth);
					}
					else
					{
						float width = (maxBound != null) ? maxBound.Value : (base.Parent.Appearance.Position.X - (float)ticksLength - base.Chart.Appearance.Border.Width);
						dimensions = new Dimensions(width, this.GetPixelStep());
						if (chartAxisItem2.Appearance.Position.X == chartAxisItem.Appearance.Position.X)
						{
							chartAxisItem2.Appearance.Position.X = base.Appearance.LabelAppearance.Position.X;
						}
						if (chartAxisItem2.Appearance.Position.AlignedPosition == AlignedPositions.None)
						{
							chartAxisItem2.Appearance.Position.AlignedPosition = AlignedPositions.Right;
						}
						chartAxisItem2.TextBlock.textBlockWrapContext = new WrapContext(dimensions.Width.PixelValue, dimensions.Height.PixelValue, WrapType.FixedHeight);
						if (this.shouldOptimizeMaxLength)
						{
							((TextBlockAxisItem)chartAxisItem2.TextBlock).DefineMaxLengthAuto(renderEngine);
						}
					}
					AutoTextWrap autoTextWrap = chartAxisItem2.TextBlock.Appearance.AutoTextWrap;
					if (chartAxisItem2.TextBlock.Appearance.AutoTextWrap == chartAxisItem.TextBlock.Appearance.AutoTextWrap)
					{
						chartAxisItem2.TextBlock.Appearance.AutoTextWrap = base.Appearance.TextAppearance.AutoTextWrap;
					}
					bool flag = true;
					SizeF sizeF = new SizeF(chartAxisItem2.Appearance.Dimensions.Width.PixelValue, chartAxisItem2.Appearance.Dimensions.Height.PixelValue);
					if (!base.Appearance.LabelAppearance.Dimensions.AutoSize && chartAxisItem2.Appearance.Dimensions.AutoSize)
					{
						chartAxisItem2.Appearance.Dimensions.SetDimensions(base.Appearance.LabelAppearance.Dimensions.Width.PixelValue, base.Appearance.LabelAppearance.Dimensions.Height.PixelValue);
					}
					else if (chartAxisItem2.Appearance.Dimensions.AutoSize)
					{
						SizeF sizeF2 = chartAxisItem2.Measure(renderEngine, chartAxisItem);
						chartAxisItem2.Appearance.Dimensions.SetDimensions(sizeF2.Width, sizeF2.Height);
						flag = false;
					}
					chartAxisItem2.CalculatePosition(dimensions);
					chartAxisItem2.Appearance.Position.AlignedPosition = position2.AlignedPosition;
					chartAxisItem2.Appearance.Position.Auto = position2.Auto;
					if (chartAxisItem2.Appearance.Position.Auto)
					{
						if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
						{
							switch (this.LayoutMode)
							{
							case ChartAxisLayoutMode.Normal:
								chartAxisItem2.Appearance.Position.X = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f));
								break;
							case ChartAxisLayoutMode.Inside:
								chartAxisItem2.Appearance.Position.X = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f));
								break;
							case ChartAxisLayoutMode.Between:
								chartAxisItem2.Appearance.Position.X = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Width.PixelValue / 2f)) + num / 2f;
								break;
							}
							chartAxisItem2.Appearance.Position.Y += base.StartPoint.Y + (float)ticksLength;
							if (!chartAxisItem2.Appearance.Dimensions.Margins.Equals(chartAxisItem.Appearance.Dimensions.Margins))
							{
								chartAxisItem2.Appearance.Position.Y += chartAxisItem2.Appearance.Dimensions.Margins.Top.PixelValue - chartAxisItem2.Appearance.Dimensions.Margins.Bottom.PixelValue;
							}
							else
							{
								chartAxisItem2.Appearance.Position.Y += base.Appearance.LabelAppearance.Dimensions.Margins.Top.PixelValue - base.Appearance.LabelAppearance.Dimensions.Margins.Bottom.PixelValue;
							}
						}
						else
						{
							switch (this.LayoutMode)
							{
							case ChartAxisLayoutMode.Normal:
								chartAxisItem2.Appearance.Position.Y = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 2f));
								break;
							case ChartAxisLayoutMode.Inside:
								chartAxisItem2.Appearance.Position.Y = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 2f));
								break;
							case ChartAxisLayoutMode.Between:
								chartAxisItem2.Appearance.Position.Y = num2 - (float)Math.Floor((double)(chartAxisItem2.Appearance.Dimensions.Height.PixelValue / 2f)) + num / 2f;
								break;
							}
							if (!chartAxisItem2.Appearance.Dimensions.Margins.Equals(chartAxisItem.Appearance.Dimensions.Margins))
							{
								chartAxisItem2.Appearance.Position.X += chartAxisItem2.Appearance.Dimensions.Margins.Left.PixelValue - chartAxisItem2.Appearance.Dimensions.Margins.Right.PixelValue;
							}
							else
							{
								chartAxisItem2.Appearance.Position.X += base.Appearance.LabelAppearance.Dimensions.Margins.Left.PixelValue - base.Appearance.LabelAppearance.Dimensions.Margins.Right.PixelValue;
							}
							if (maxBound != null)
							{
								chartAxisItem2.Appearance.Position.X += base.Parent.Appearance.Dimensions.Margins.Left.PixelValue - maxBound.Value - (float)ticksLength;
							}
						}
					}
					if (flag)
					{
						chartAxisItem2.Appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
					}
					chartAxisItem2.TextBlock.Appearance.Position.Copy = chartAxisItem2.TextBlock.Appearance.Position;
					if (chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition == chartAxisItem.TextBlock.Appearance.Position.AlignedPosition)
					{
						chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition = base.Appearance.TextAppearance.Position.AlignedPosition;
					}
					if (base.Parent.Chart.ShouldApplyTextWrapping(chartAxisItem2.TextBlock.Appearance.AutoTextWrap))
					{
						chartAxisItem2.CorrectTextBlockAlignedPosition(base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal);
					}
					chartAxisItem2.TextBlock.CalculatePosition(renderEngine);
					chartAxisItem2.TextBlock.Appearance.Position.AlignedPosition = chartAxisItem2.TextBlock.Appearance.Position.Copy.AlignedPosition;
					chartAxisItem2.TextBlock.Appearance.AutoTextWrap = autoTextWrap;
				}
				num2 += num;
			}
		}

		// Token: 0x0600E6B8 RID: 59064 RVA: 0x00336C68 File Offset: 0x00334E68
		internal override void CalculateGridsAndTicks()
		{
			bool flag = base.Appearance.MajorGridLines.ShouldRender(base.IsVisible());
			bool flag2 = base.Appearance.MajorTick.IsVisible() && this.IsMajorTickVisible;
			if (base.IsVisible())
			{
				int count = base.Items.Count;
				int num = 0;
				switch (this.LayoutMode)
				{
				case ChartAxisLayoutMode.Normal:
					num = count;
					if (count > 1)
					{
						num--;
					}
					break;
				case ChartAxisLayoutMode.Inside:
					num = count + 1;
					break;
				case ChartAxisLayoutMode.Between:
					num = count;
					break;
				}
				if (num > 0)
				{
					float num2 = base.GetDistance(base.StartPoint, base.EndPoint) / (float)num;
					int length = base.Appearance.MajorTick.Length;
					int num3 = 0;
					int num4 = 0;
					int num6;
					if (this.LayoutMode == ChartAxisLayoutMode.Inside)
					{
						int num5 = ((num - 1) % base.LabelStep == 0) ? 0 : 1;
						num6 = ((num - 1) / base.LabelStep + num5) * 2;
						if (flag2)
						{
							this.TickPoints = new PointF[num6];
							this.TickPointsTypes = new byte[num6];
						}
					}
					else
					{
						int num7 = (num % base.LabelStep == 0) ? 1 : 0;
						num6 = (num / base.LabelStep - num7) * 2;
						if (flag2)
						{
							this.TickPoints = new PointF[num6 + 4];
							this.TickPointsTypes = new byte[num6 + 4];
						}
					}
					if (flag)
					{
						this.GridPoints = new PointF[num6];
						this.GridPointsTypes = new byte[num6];
					}
					if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						float y = this.PlotRect.Top;
						float num8 = this.PlotRect.Bottom;
						int num9 = -1;
						if (this.LayoutMode != ChartAxisLayoutMode.Inside && flag2)
						{
							this.TickPoints[num4++] = new PointF(base.StartPoint.X, num8);
							this.TickPoints[num4] = new PointF(base.StartPoint.X, num8 + (float)length);
							this.TickPointsTypes[num4++] = 1;
							num9++;
						}
						for (int i = 1; i < num; i++)
						{
							float num10;
							float x = num10 = (float)Math.Round((double)(base.StartPoint.X + (float)i * num2));
							PointF pointF = new PointF(num10, y);
							PointF pointF2 = new PointF(x, num8);
							PointF pointF3 = new PointF(num10, num8);
							PointF pointF4 = new PointF(num10, num8 + (float)length);
							num9++;
							if (flag2 && num9 % base.LabelStep == 0)
							{
								this.TickPoints[num4++] = pointF3;
								this.TickPoints[num4] = pointF4;
								this.TickPointsTypes[num4++] = 1;
							}
							if (flag && num9 % base.LabelStep == 0)
							{
								this.GridPoints[num3++] = pointF;
								this.GridPoints[num3] = pointF2;
								this.GridPointsTypes[num3++] = 1;
							}
						}
						if (this.LayoutMode != ChartAxisLayoutMode.Inside && flag2)
						{
							this.TickPoints[num4++] = new PointF(base.EndPoint.X, num8);
							this.TickPoints[num4] = new PointF(base.EndPoint.X, num8 + (float)length);
							this.TickPointsTypes[num4++] = 1;
							return;
						}
					}
					else if (base.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
					{
						float num10 = this.PlotRect.Left;
						float x = this.PlotRect.Right;
						int num11 = -1;
						if (this.LayoutMode != ChartAxisLayoutMode.Inside && flag2)
						{
							this.TickPoints[num4++] = new PointF(num10 - (float)length, base.StartPoint.Y);
							this.TickPoints[num4] = new PointF(num10, base.StartPoint.Y);
							this.TickPointsTypes[num4++] = 1;
							num11++;
						}
						for (int j = 1; j < num; j++)
						{
							float y;
							float num8 = y = (float)Math.Round((double)(base.StartPoint.Y - (float)j * num2));
							PointF pointF5 = new PointF(num10, y);
							PointF pointF6 = new PointF(x, num8);
							PointF pointF7 = new PointF(num10, num8);
							PointF pointF8 = new PointF(num10 - (float)length, num8);
							num11++;
							if (flag2 && num11 % base.LabelStep == 0)
							{
								this.TickPoints[num4++] = pointF7;
								this.TickPoints[num4] = pointF8;
								this.TickPointsTypes[num4++] = 1;
							}
							if (flag && num11 % base.LabelStep == 0)
							{
								this.GridPoints[num3++] = pointF5;
								this.GridPoints[num3] = pointF6;
								this.GridPointsTypes[num3++] = 1;
							}
						}
						if (this.LayoutMode != ChartAxisLayoutMode.Inside && flag2)
						{
							this.TickPoints[num4++] = new PointF(num10 - (float)length, base.EndPoint.Y);
							this.TickPoints[num4] = new PointF(num10, base.EndPoint.Y);
							this.TickPointsTypes[num4++] = 1;
						}
					}
				}
			}
		}

		// Token: 0x0600E6B9 RID: 59065 RVA: 0x00337228 File Offset: 0x00335428
		public new void AddItem(string label)
		{
			base.AddItem(label);
		}

		// Token: 0x0600E6BA RID: 59066 RVA: 0x00337232 File Offset: 0x00335432
		public new void AddItem(string label, Color color)
		{
			base.AddItem(label, color);
		}

		// Token: 0x0600E6BB RID: 59067 RVA: 0x0033723D File Offset: 0x0033543D
		public void ClearDataBoundState()
		{
			base.AutoScale = true;
		}

		// Token: 0x0400424D RID: 16973
		private BarOrderingMode chartXAxisOrderingMode = BarOrderingMode.Classic;

		// Token: 0x0400424E RID: 16974
		protected float pixelStep;

		// Token: 0x0400424F RID: 16975
		internal PointF[] TickPoints;

		// Token: 0x04004250 RID: 16976
		internal PointF[] GridPoints;

		// Token: 0x04004251 RID: 16977
		internal byte[] TickPointsTypes;

		// Token: 0x04004252 RID: 16978
		internal byte[] GridPointsTypes;
	}
}
