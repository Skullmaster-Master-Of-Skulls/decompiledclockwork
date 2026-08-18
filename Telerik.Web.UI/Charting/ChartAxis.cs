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
	// Token: 0x02001727 RID: 5927
	[ParseChildren(true)]
	[PersistChildren(false)]
	public abstract class ChartAxis : RenderedObject
	{
		// Token: 0x0600E62C RID: 58924 RVA: 0x00332F00 File Offset: 0x00331100
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartAxisAppearance).TrackViewState();
			((IChartingStateManager)this.chartAxisLabel).TrackViewState();
			((IChartingStateManager)this.chartAxisItems).TrackViewState();
		}

		// Token: 0x0600E62D RID: 58925 RVA: 0x00332F2C File Offset: 0x0033112C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartAxisAppearance).LoadViewState(array[1]);
				((IChartingStateManager)this.chartAxisLabel).LoadViewState(array[2]);
				((IChartingStateManager)this.chartAxisItems).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600E62E RID: 58926 RVA: 0x00332F78 File Offset: 0x00331178
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartAxisAppearance).SaveViewState(),
				((IChartingStateManager)this.chartAxisLabel).SaveViewState(),
				((IChartingStateManager)this.chartAxisItems).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E62F RID: 58927 RVA: 0x00332FD4 File Offset: 0x003311D4
		protected float GetDistance(PointF point1, PointF point2)
		{
			return (float)Math.Sqrt((double)((point2.X - point1.X) * (point2.X - point1.X) + (point2.Y - point1.Y) * (point2.Y - point1.Y)));
		}

		// Token: 0x0600E630 RID: 58928
		internal abstract void CalculateGridsAndTicks();

		// Token: 0x0600E631 RID: 58929 RVA: 0x00333028 File Offset: 0x00331228
		internal void ClearAutoPropertiesForAxisItems()
		{
			if (this.AutoScale)
			{
				this.Items.Clear();
				return;
			}
			foreach (ChartAxisItem chartAxisItem in this.Items)
			{
				if (string.Compare(this.FormatLabel(Convert.ToDouble(chartAxisItem.Value)), chartAxisItem.TextBlock.Text, true) == 0)
				{
					chartAxisItem.TextBlock.Text = string.Empty;
				}
			}
		}

		// Token: 0x0600E632 RID: 58930 RVA: 0x003330B8 File Offset: 0x003312B8
		internal void CorrectAxisLabelPosition(Position position)
		{
			ChartSeriesOrientation seriesOrientation = this.Chart.SeriesOrientation;
			if (!this.Chart.AutoLayoutWrapper)
			{
				if (position.AlignedPosition == AlignedPositions.None)
				{
					position.AlignedPosition = AlignedPositions.Center;
					return;
				}
			}
			else
			{
				if ((this.AxisType == ChartAxisType.XAxis && seriesOrientation == ChartSeriesOrientation.Horizontal) || (this.AxisType == ChartAxisType.YAxis && seriesOrientation == ChartSeriesOrientation.Vertical))
				{
					AlignedPositions alignedPosition = position.AlignedPosition;
					if (alignedPosition <= AlignedPositions.Left)
					{
						switch (alignedPosition)
						{
						case AlignedPositions.None:
							break;
						case AlignedPositions.TopLeft:
						case AlignedPositions.Top:
							position.AlignedPosition = AlignedPositions.TopRight;
							return;
						default:
							if (alignedPosition != AlignedPositions.Left)
							{
								return;
							}
							break;
						}
					}
					else if (alignedPosition != AlignedPositions.Center)
					{
						if (alignedPosition != AlignedPositions.BottomLeft && alignedPosition != AlignedPositions.Bottom)
						{
							return;
						}
						position.AlignedPosition = AlignedPositions.BottomRight;
						return;
					}
					position.AlignedPosition = AlignedPositions.Right;
					return;
				}
				if ((this.AxisType == ChartAxisType.YAxis && seriesOrientation == ChartSeriesOrientation.Horizontal) || (this.AxisType == ChartAxisType.XAxis && seriesOrientation == ChartSeriesOrientation.Vertical))
				{
					AlignedPositions alignedPosition2 = position.AlignedPosition;
					if (alignedPosition2 > AlignedPositions.Center)
					{
						if (alignedPosition2 <= AlignedPositions.BottomLeft)
						{
							if (alignedPosition2 != AlignedPositions.Right)
							{
								if (alignedPosition2 != AlignedPositions.BottomLeft)
								{
									return;
								}
								goto IL_106;
							}
						}
						else
						{
							if (alignedPosition2 == AlignedPositions.Bottom)
							{
								goto IL_F6;
							}
							if (alignedPosition2 != AlignedPositions.BottomRight)
							{
								return;
							}
						}
						position.AlignedPosition = AlignedPositions.TopRight;
						return;
					}
					if (alignedPosition2 != AlignedPositions.None)
					{
						if (alignedPosition2 == AlignedPositions.Left)
						{
							goto IL_106;
						}
						if (alignedPosition2 != AlignedPositions.Center)
						{
							return;
						}
					}
					IL_F6:
					position.AlignedPosition = AlignedPositions.Top;
					return;
					IL_106:
					position.AlignedPosition = AlignedPositions.TopLeft;
					return;
				}
				if (seriesOrientation == ChartSeriesOrientation.Vertical)
				{
					AlignedPositions alignedPosition3 = position.AlignedPosition;
					if (alignedPosition3 <= AlignedPositions.Center)
					{
						switch (alignedPosition3)
						{
						case AlignedPositions.None:
							break;
						case AlignedPositions.TopLeft:
						case (AlignedPositions)3:
							return;
						case AlignedPositions.Top:
						case AlignedPositions.TopRight:
							position.AlignedPosition = AlignedPositions.TopLeft;
							return;
						default:
							if (alignedPosition3 != AlignedPositions.Center)
							{
								return;
							}
							break;
						}
					}
					else if (alignedPosition3 != AlignedPositions.Right)
					{
						if (alignedPosition3 != AlignedPositions.Bottom && alignedPosition3 != AlignedPositions.BottomRight)
						{
							return;
						}
						position.AlignedPosition = AlignedPositions.BottomLeft;
						return;
					}
					position.AlignedPosition = AlignedPositions.Left;
					return;
				}
				AlignedPositions alignedPosition4 = position.AlignedPosition;
				if (alignedPosition4 <= AlignedPositions.Left)
				{
					switch (alignedPosition4)
					{
					case AlignedPositions.None:
					case AlignedPositions.Top:
						goto IL_1AD;
					case AlignedPositions.TopLeft:
						break;
					case (AlignedPositions)3:
						return;
					case AlignedPositions.TopRight:
						goto IL_1C5;
					default:
						if (alignedPosition4 != AlignedPositions.Left)
						{
							return;
						}
						break;
					}
					position.AlignedPosition = AlignedPositions.BottomLeft;
					return;
				}
				if (alignedPosition4 != AlignedPositions.Center)
				{
					if (alignedPosition4 != AlignedPositions.Right)
					{
						return;
					}
					goto IL_1C5;
				}
				IL_1AD:
				position.AlignedPosition = AlignedPositions.Bottom;
				return;
				IL_1C5:
				position.AlignedPosition = AlignedPositions.BottomRight;
			}
		}

		// Token: 0x0600E633 RID: 58931 RVA: 0x00333298 File Offset: 0x00331498
		internal void CorrectAxisItemPosition(Position position)
		{
			ChartSeriesOrientation seriesOrientation = this.Chart.SeriesOrientation;
			if (this.Chart.AutoLayoutWrapper && position.AlignedPosition == AlignedPositions.None)
			{
				if ((this.AxisType == ChartAxisType.XAxis && seriesOrientation == ChartSeriesOrientation.Horizontal) || (this.AxisType == ChartAxisType.YAxis && seriesOrientation == ChartSeriesOrientation.Vertical))
				{
					position.AlignedPosition = AlignedPositions.Right;
					return;
				}
				if ((this.AxisType == ChartAxisType.YAxis && seriesOrientation == ChartSeriesOrientation.Horizontal) || (this.AxisType == ChartAxisType.XAxis && seriesOrientation == ChartSeriesOrientation.Vertical))
				{
					position.AlignedPosition = AlignedPositions.Top;
					return;
				}
				if (seriesOrientation == ChartSeriesOrientation.Vertical)
				{
					position.AlignedPosition = AlignedPositions.Left;
					return;
				}
				position.AlignedPosition = AlignedPositions.Bottom;
			}
		}

		// Token: 0x17004619 RID: 17945
		// (get) Token: 0x0600E634 RID: 58932 RVA: 0x00333320 File Offset: 0x00331520
		internal int TicksLength
		{
			get
			{
				int num = 0;
				if (this.IsVisible())
				{
					if (this.IsMinorTickVisible)
					{
						num = Math.Max(this.Appearance.MinorTick.Length, num);
					}
					if (this.IsMajorTickVisible)
					{
						num = Math.Max(this.Appearance.MajorTick.Length, num);
					}
				}
				return num;
			}
		}

		// Token: 0x1700461A RID: 17946
		// (get) Token: 0x0600E635 RID: 58933 RVA: 0x00333376 File Offset: 0x00331576
		internal virtual bool IsMajorTickVisible
		{
			get
			{
				return this.Appearance.MajorTick.Visible;
			}
		}

		// Token: 0x1700461B RID: 17947
		// (get) Token: 0x0600E636 RID: 58934 RVA: 0x00333388 File Offset: 0x00331588
		internal virtual bool IsMinorTickVisible
		{
			get
			{
				return this.Appearance.MinorTick.Visible;
			}
		}

		// Token: 0x1700461C RID: 17948
		// (get) Token: 0x0600E637 RID: 58935 RVA: 0x0033339A File Offset: 0x0033159A
		internal bool IsTickVisible
		{
			get
			{
				return this.IsMajorTickVisible || this.IsMinorTickVisible;
			}
		}

		// Token: 0x0600E638 RID: 58936 RVA: 0x003333AC File Offset: 0x003315AC
		internal float GetWidth()
		{
			if (!this.IsVisible())
			{
				return 0f;
			}
			float num = this.Items.GetWidth();
			if (this.AxisLabel.IsVisible())
			{
				num += Style.GetRealBounds(this.AxisLabel.Appearance.Dimensions, new float?(this.AxisLabel.Appearance.RotationAngle)).Width + this.AxisLabel.Appearance.Dimensions.Margins.Left.PixelValue + this.AxisLabel.Appearance.Dimensions.Margins.Right.PixelValue;
			}
			return num;
		}

		// Token: 0x0600E639 RID: 58937 RVA: 0x00333458 File Offset: 0x00331658
		internal float GetHeight()
		{
			if (!this.IsVisible())
			{
				return 0f;
			}
			float num = this.Items.GetHeight();
			if (this.AxisLabel.IsVisible())
			{
				num += Style.GetRealBounds(this.AxisLabel.Appearance.Dimensions, new float?(this.AxisLabel.Appearance.RotationAngle)).Height + this.AxisLabel.Appearance.Dimensions.Margins.Top.PixelValue + this.AxisLabel.Appearance.Dimensions.Margins.Bottom.PixelValue;
			}
			return num;
		}

		// Token: 0x1700461D RID: 17949
		// (get) Token: 0x0600E63A RID: 58938 RVA: 0x00333502 File Offset: 0x00331702
		// (set) Token: 0x0600E63B RID: 58939 RVA: 0x0033350A File Offset: 0x0033170A
		internal PointF StartPoint
		{
			get
			{
				return this.chartAxisPointStart;
			}
			set
			{
				this.DisableCachedValues();
				this.chartAxisPointStart = value;
			}
		}

		// Token: 0x1700461E RID: 17950
		// (get) Token: 0x0600E63C RID: 58940 RVA: 0x00333519 File Offset: 0x00331719
		// (set) Token: 0x0600E63D RID: 58941 RVA: 0x00333521 File Offset: 0x00331721
		internal PointF EndPoint
		{
			get
			{
				return this.chartAxisPointEnd;
			}
			set
			{
				this.DisableCachedValues();
				this.chartAxisPointEnd = value;
			}
		}

		// Token: 0x1700461F RID: 17951
		// (get) Token: 0x0600E63E RID: 58942
		internal abstract float ItemsBound { get; }

		// Token: 0x17004620 RID: 17952
		// (get) Token: 0x0600E63F RID: 58943 RVA: 0x00333530 File Offset: 0x00331730
		internal virtual float PixelsPerValue
		{
			get
			{
				if (float.IsNaN(this.pixelsPerValue))
				{
					float num = this.chartAxisPointStart.X - this.chartAxisPointEnd.X;
					float num2 = this.chartAxisPointStart.Y - this.chartAxisPointEnd.Y;
					float num3 = (float)Math.Abs(this.chartAxisMaxAxisValue - this.chartAxisMinAxisValue);
					if (num3 != 0f)
					{
						this.pixelsPerValue = (float)Math.Sqrt((double)(num * num + num2 * num2)) / num3;
					}
					else
					{
						this.pixelsPerValue = 0f;
					}
				}
				return this.pixelsPerValue;
			}
		}

		// Token: 0x17004621 RID: 17953
		// (get) Token: 0x0600E640 RID: 58944 RVA: 0x003335BF File Offset: 0x003317BF
		internal Chart Chart
		{
			get
			{
				return this.Parent.Parent;
			}
		}

		// Token: 0x17004622 RID: 17954
		// (get) Token: 0x0600E641 RID: 58945 RVA: 0x003335CC File Offset: 0x003317CC
		internal virtual RectangleF PlotRect
		{
			get
			{
				return new RectangleF(this.Parent.Appearance.Position.X, this.Parent.Appearance.Position.Y, this.Parent.Appearance.Dimensions.Width.PixelValue, this.Parent.Appearance.Dimensions.Height.PixelValue);
			}
		}

		// Token: 0x17004623 RID: 17955
		// (get) Token: 0x0600E642 RID: 58946
		internal abstract ChartAxisType AxisType { get; }

		// Token: 0x0600E643 RID: 58947 RVA: 0x0033363C File Offset: 0x0033183C
		internal string FormatLabel(double val)
		{
			switch (this.chartAxisAppearance.ValueFormat)
			{
			case ChartValueFormat.None:
				if (string.IsNullOrEmpty(this.chartAxisAppearance.CustomFormat))
				{
					return Convert.ToString(val);
				}
				return val.ToString(this.chartAxisAppearance.CustomFormat);
			case ChartValueFormat.Currency:
				return val.ToString("C");
			case ChartValueFormat.Scientific:
				return val.ToString("E");
			case ChartValueFormat.General:
				return val.ToString("G");
			case ChartValueFormat.Number:
				return val.ToString("N");
			case ChartValueFormat.Percent:
				return val.ToString("P");
			case ChartValueFormat.ShortDate:
				if (!string.IsNullOrEmpty(this.chartAxisAppearance.CustomFormat))
				{
					return DateTime.FromOADate(val).ToString(this.chartAxisAppearance.CustomFormat);
				}
				return DateTime.FromOADate(val).ToShortDateString();
			case ChartValueFormat.ShortTime:
				if (!string.IsNullOrEmpty(this.chartAxisAppearance.CustomFormat))
				{
					return DateTime.FromOADate(val).ToString(this.chartAxisAppearance.CustomFormat);
				}
				return DateTime.FromOADate(val).ToShortTimeString();
			case ChartValueFormat.LongDate:
				if (!string.IsNullOrEmpty(this.chartAxisAppearance.CustomFormat))
				{
					return DateTime.FromOADate(val).ToString(this.chartAxisAppearance.CustomFormat);
				}
				return DateTime.FromOADate(val).ToLongDateString();
			case ChartValueFormat.LongTime:
				if (!string.IsNullOrEmpty(this.chartAxisAppearance.CustomFormat))
				{
					return DateTime.FromOADate(val).ToString(this.chartAxisAppearance.CustomFormat);
				}
				return DateTime.FromOADate(val).ToLongTimeString();
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600E644 RID: 58948
		internal abstract float GetCoordinate(double val);

		// Token: 0x0600E645 RID: 58949 RVA: 0x003337EC File Offset: 0x003319EC
		internal float GetCoordinate(double val, float pixelsPerVal, bool roundCoord)
		{
			if (double.IsNaN(this.chartAxisMinAxisValue))
			{
				return this.GetAxisStartCoord();
			}
			double num;
			if (this.chartAxisRealIsZeroBased || (this.chartAxisMinAxisValue < 0.0 && this.chartAxisMaxAxisValue >= 0.0))
			{
				num = (double)this.GetZeroCoordinate() + val * (double)pixelsPerVal;
			}
			else
			{
				double num2 = (this.chartAxisMinAxisValue >= 0.0) ? this.chartAxisMinAxisValue : this.chartAxisMaxAxisValue;
				num = (double)this.GetZeroCoordinate() + (val - num2) * (double)pixelsPerVal;
			}
			if (!roundCoord)
			{
				return (float)num;
			}
			if (val == 0.0 || Math.Abs(val) >= 1.0)
			{
				return (float)Math.Round(num);
			}
			if (val <= 0.0)
			{
				return (float)Math.Floor(num);
			}
			return (float)Math.Ceiling(num);
		}

		// Token: 0x0600E646 RID: 58950 RVA: 0x003338BB File Offset: 0x00331ABB
		internal virtual double GetZeroValue()
		{
			if (this.chartAxisMinAxisValue >= 0.0)
			{
				return this.chartAxisMinAxisValue;
			}
			if (this.chartAxisMaxAxisValue <= 0.0)
			{
				return this.chartAxisMaxAxisValue;
			}
			return 0.0;
		}

		// Token: 0x0600E647 RID: 58951 RVA: 0x003338F8 File Offset: 0x00331AF8
		internal virtual float GetZeroCoordinate()
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
					float num = (float)this.chartAxisMinAxisValue;
					if (this.chartAxisParent.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
					{
						num = -num;
					}
					this.zeroCoord = (float)Math.Round((double)this.GetAxisStartCoord()) + num * this.PixelsPerValue;
				}
			}
			return this.zeroCoord;
		}

		// Token: 0x0600E648 RID: 58952
		protected internal abstract float GetAxisStartCoord();

		// Token: 0x0600E649 RID: 58953
		protected internal abstract float GetAxisEndCoord();

		// Token: 0x0600E64A RID: 58954 RVA: 0x003339BC File Offset: 0x00331BBC
		internal void SaveLabelPosition()
		{
			this.AxisLabel.Appearance.Position.Copy = this.AxisLabel.Appearance.Position;
			this.Appearance.LabelAppearance.Position.Copy = this.Appearance.LabelAppearance.Position;
		}

		// Token: 0x0600E64B RID: 58955 RVA: 0x00333A14 File Offset: 0x00331C14
		internal void RestoreLabelPosition()
		{
			this.AxisLabel.Appearance.Position.AlignedPosition = this.AxisLabel.Appearance.Position.Copy.AlignedPosition;
			this.Appearance.LabelAppearance.Position.AlignedPosition = this.Appearance.LabelAppearance.Position.Copy.AlignedPosition;
		}

		// Token: 0x0600E64C RID: 58956 RVA: 0x00333A80 File Offset: 0x00331C80
		internal void SetRange()
		{
			try
			{
				if (this.Items.Count > 0)
				{
					foreach (ChartAxisItem chartAxisItem in this.Items)
					{
						chartAxisItem.Parent = this.Items;
					}
					this.SetMinValue(Convert.ToDouble(this.Items[0].Value));
					this.SetMaxValue(Convert.ToDouble(this.Items[this.Items.Count - 1].Value));
					this.chartAxisMinAxisValue = this.MinValue;
					this.chartAxisMaxAxisValue = this.MaxValue;
					this.DisableCachedValues();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600E64D RID: 58957 RVA: 0x00333B58 File Offset: 0x00331D58
		internal void CheckRange(double minValue, double maxValue, double step)
		{
			if (step == 0.0)
			{
				throw new ChartException(string.Format("{0}. Axis range step cannot be 0.", this.AxisType.ToString()));
			}
			if (minValue > maxValue)
			{
				throw new ChartException(string.Format("{0}. Min axis range value cannot be greater than max axis range value.", this.AxisType.ToString()));
			}
			if (step < 0.0)
			{
				throw new ChartException(string.Format("{0}. Axis range step cannot be negative.", this.AxisType.ToString()));
			}
		}

		// Token: 0x0600E64E RID: 58958 RVA: 0x00333BE1 File Offset: 0x00331DE1
		protected virtual void DisableCachedValues()
		{
			this.ResetCachedValues();
		}

		// Token: 0x0600E64F RID: 58959 RVA: 0x00333BE9 File Offset: 0x00331DE9
		private void ResetCachedValues()
		{
			this.pixelsPerValue = float.NaN;
			this.zeroCoord = float.NaN;
		}

		// Token: 0x0600E650 RID: 58960 RVA: 0x00333C04 File Offset: 0x00331E04
		internal float GetItemsBound(ChartAxisItem item, float rotationAngle)
		{
			float num = Math.Max(item.Appearance.Dimensions.Width.PixelValue, item.Appearance.Dimensions.Height.PixelValue);
			float num2 = Math.Min(item.Appearance.Dimensions.Width.PixelValue, item.Appearance.Dimensions.Height.PixelValue);
			return Math.Abs(num * (float)Math.Sin((double)rotationAngle * 3.141592653589793 / 180.0)) + Math.Abs(num2 * (float)Math.Cos((double)rotationAngle * 3.141592653589793 / 180.0));
		}

		// Token: 0x0600E651 RID: 58961
		internal abstract RectangleF GetClientRectangle(PointF startPoint, PointF endPoint);

		// Token: 0x0600E652 RID: 58962
		internal abstract RectangleF GetClientRectangle();

		// Token: 0x0600E653 RID: 58963
		internal abstract float GetFirstItemHalfDimension();

		// Token: 0x0600E654 RID: 58964
		internal abstract float GetLastItemHalfDimension();

		// Token: 0x0600E655 RID: 58965
		internal abstract void CalculateLayout(RenderEngine renderEngine);

		// Token: 0x0600E656 RID: 58966
		internal abstract void InitializeItems();

		// Token: 0x0600E657 RID: 58967 RVA: 0x00333CB8 File Offset: 0x00331EB8
		internal void CalculateAxisLabel(RenderEngine renderEngine)
		{
			ChartXAxis chartXAxis = this as ChartXAxis;
			Dimensions dimensions = this.chartAxisParent.Appearance.Dimensions;
			if (renderEngine.chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				if (chartXAxis != null)
				{
					this.chartAxisLabel.TextBlock.textBlockWrapContext = new WrapContext(renderEngine.chart.Appearance.Dimensions.Width.PixelValue, renderEngine.chart.Appearance.Dimensions.Height.PixelValue, WrapType.FixedWidth);
				}
				else
				{
					this.chartAxisLabel.TextBlock.textBlockWrapContext = new WrapContext(renderEngine.chart.Appearance.Dimensions.Height.PixelValue, renderEngine.chart.Appearance.Dimensions.Width.PixelValue, WrapType.FixedWidth);
				}
			}
			else if (chartXAxis != null)
			{
				this.chartAxisLabel.TextBlock.textBlockWrapContext = new WrapContext(renderEngine.chart.Appearance.Dimensions.Height.PixelValue, renderEngine.chart.Appearance.Dimensions.Width.PixelValue, WrapType.FixedWidth);
			}
			else
			{
				this.chartAxisLabel.TextBlock.textBlockWrapContext = new WrapContext(renderEngine.chart.Appearance.Dimensions.Width.PixelValue, renderEngine.chart.Appearance.Dimensions.Height.PixelValue, WrapType.FixedWidth);
			}
			if (this.chartAxisLabel.TextBlock.Appearance.Dimensions.AutoSize)
			{
				SizeF sizeF = this.chartAxisLabel.TextBlock.Measure(renderEngine);
				this.chartAxisLabel.TextBlock.Appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
			}
			if (this.chartAxisLabel.Appearance.Dimensions.AutoSize)
			{
				SizeF sizeF = this.chartAxisLabel.Measure(renderEngine);
				this.chartAxisLabel.Appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
			}
			else
			{
				this.chartAxisLabel.Measure(renderEngine);
			}
			if (this.chartAxisLabel.Appearance.Position.Auto)
			{
				Dimensions dimensions2 = new Dimensions(new ChartMargins(0f), new ChartPaddings(0f));
				AlignedPositions alignedPosition = this.chartAxisLabel.Appearance.Position.AlignedPosition;
				if (this.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					if (this.AxisType != ChartAxisType.XAxis)
					{
						if (this.chartAxisParent.Chart.AutoLayoutWrapper)
						{
							dimensions2.Width = Unit.Pixel(Style.GetRealBounds(this.chartAxisLabel.Appearance.Dimensions, new float?(this.chartAxisLabel.Appearance.RotationAngle)).Width + this.chartAxisLabel.Appearance.Dimensions.Margins.Left.PixelValue + this.chartAxisLabel.Appearance.Dimensions.Margins.Right.PixelValue);
						}
						else if (this.AxisType == ChartAxisType.YAxis)
						{
							dimensions2.Width = Unit.Pixel(this.ItemsBound);
						}
						else
						{
							dimensions2.Width = Unit.Pixel(this.chartAxisParent.Chart.Appearance.Dimensions.Width.PixelValue - this.ItemsBound);
						}
						dimensions2.Height = dimensions.Height;
					}
					else
					{
						dimensions2.Width = dimensions.Width;
						if (this.chartAxisParent.Chart.AutoLayoutWrapper)
						{
							dimensions2.Height = Unit.Pixel(Style.GetRealBounds(this.chartAxisLabel.Appearance.Dimensions, new float?(this.chartAxisLabel.Appearance.RotationAngle)).Height + this.chartAxisLabel.Appearance.Dimensions.Margins.Top.PixelValue + this.chartAxisLabel.Appearance.Dimensions.Margins.Bottom.PixelValue);
						}
						else
						{
							dimensions2.Height = Unit.Pixel(this.chartAxisParent.Chart.Appearance.Dimensions.Height.PixelValue - this.ItemsBound);
						}
					}
				}
				else if (this.AxisType != ChartAxisType.XAxis)
				{
					if (this.chartAxisParent.Chart.AutoLayoutWrapper)
					{
						dimensions2.Height = Unit.Pixel(Style.GetRealBounds(this.chartAxisLabel.Appearance.Dimensions, new float?(this.chartAxisLabel.Appearance.RotationAngle)).Height + this.chartAxisLabel.Appearance.Dimensions.Margins.Top.PixelValue + this.chartAxisLabel.Appearance.Dimensions.Margins.Bottom.PixelValue);
					}
					else if (this.AxisType == ChartAxisType.YAxis)
					{
						dimensions2.Height = Unit.Pixel(this.chartAxisParent.Chart.Appearance.Dimensions.Height.PixelValue - this.ItemsBound);
					}
					else
					{
						dimensions2.Height = Unit.Pixel(this.ItemsBound);
					}
					dimensions2.Width = dimensions.Width;
				}
				else
				{
					if (this.chartAxisParent.Chart.AutoLayoutWrapper)
					{
						dimensions2.Width = Unit.Pixel(Style.GetRealBounds(this.chartAxisLabel.Appearance.Dimensions, new float?(this.chartAxisLabel.Appearance.RotationAngle)).Width + this.chartAxisLabel.Appearance.Dimensions.Margins.Left.PixelValue + this.chartAxisLabel.Appearance.Dimensions.Margins.Right.PixelValue);
					}
					else
					{
						dimensions2.Width = Unit.Pixel(this.ItemsBound);
					}
					dimensions2.Height = dimensions.Height;
				}
				this.CorrectAxisLabelPosition(this.chartAxisLabel.Appearance.Position);
				this.chartAxisLabel.CalculatePosition(dimensions2);
				if (!this.chartAxisParent.Chart.AutoLayoutWrapper)
				{
					this.chartAxisLabel.Appearance.Position.AlignedPosition = alignedPosition;
				}
				if (this.Chart.SeriesOrientation == ChartSeriesOrientation.Vertical)
				{
					if (this.AxisType != ChartAxisType.XAxis)
					{
						if (this.chartAxisParent.Chart.AutoLayoutWrapper)
						{
							if (this.AxisType == ChartAxisType.YAxis)
							{
								this.chartAxisLabel.Appearance.Position.X = this.StartPoint.X - this.Items.GetWidth() - (float)this.TicksLength - this.chartAxisLabel.Appearance.Dimensions.Width.PixelValue - this.chartAxisLabel.Appearance.Dimensions.Margins.Right.PixelValue;
							}
							else
							{
								this.chartAxisLabel.Appearance.Position.X = this.StartPoint.X + this.Items.GetWidth() + (float)this.TicksLength + this.chartAxisLabel.Appearance.Dimensions.Margins.Left.PixelValue;
							}
						}
						else if (this.AxisType == ChartAxisType.YAxis2)
						{
							this.chartAxisLabel.Appearance.Position.X += this.ItemsBound;
						}
						this.chartAxisLabel.Appearance.Position.Y += dimensions.Margins.Top.PixelValue;
						return;
					}
					if (this.chartAxisParent.Chart.AutoLayoutWrapper)
					{
						this.chartAxisLabel.Appearance.Position.Y = this.StartPoint.Y + this.Items.GetHeight() + (float)this.TicksLength;
						if (this.Parent.DataTable.Appearance.RenderType == TableRenderType.PlotAreaRelative && this.Parent.DataTable.Visible)
						{
							this.chartAxisLabel.Appearance.Position.Y += this.Parent.DataTable.Appearance.Dimensions.Height.PixelValue + this.Parent.DataTable.Appearance.Dimensions.Margins.Bottom.PixelValue;
						}
					}
					else
					{
						this.chartAxisLabel.Appearance.Position.Y += this.ItemsBound;
					}
					this.chartAxisLabel.Appearance.Position.X += dimensions.Margins.Left.PixelValue;
					return;
				}
				else if (this.AxisType != ChartAxisType.XAxis)
				{
					this.chartAxisLabel.Appearance.Position.X += dimensions.Margins.Left.PixelValue;
					if (this.chartAxisParent.Chart.AutoLayoutWrapper)
					{
						if (this.AxisType == ChartAxisType.YAxis)
						{
							this.chartAxisLabel.Appearance.Position.Y = this.StartPoint.Y + this.Items.GetHeight() + (float)this.TicksLength;
							return;
						}
						this.chartAxisLabel.Appearance.Position.Y = this.StartPoint.Y - this.Items.GetHeight() - (float)this.TicksLength - this.chartAxisLabel.Appearance.Dimensions.Height.PixelValue;
						return;
					}
					else if (this.AxisType == ChartAxisType.YAxis)
					{
						this.chartAxisLabel.Appearance.Position.Y += this.ItemsBound;
						return;
					}
				}
				else
				{
					if (this.chartAxisParent.Chart.AutoLayoutWrapper)
					{
						this.chartAxisLabel.Appearance.Position.X = this.StartPoint.X - this.Items.GetWidth() - (float)this.TicksLength - this.chartAxisLabel.Appearance.Dimensions.Width.PixelValue;
					}
					this.chartAxisLabel.Appearance.Position.Y += dimensions.Margins.Top.PixelValue;
				}
			}
		}

		// Token: 0x0600E658 RID: 58968 RVA: 0x00334740 File Offset: 0x00332940
		internal virtual bool CheckAxisItemVisibility(ChartAxisItem item)
		{
			if (this.Parent.Parent.OnlyPieSeries())
			{
				return false;
			}
			if (this.AutoScale)
			{
				return this.Appearance.LabelAppearance.Visible;
			}
			return item.Visible && this.Appearance.LabelAppearance.Visible;
		}

		// Token: 0x0600E659 RID: 58969 RVA: 0x00334794 File Offset: 0x00332994
		internal bool IsVisible()
		{
			switch (this.Appearance.Visible)
			{
			default:
			{
				if (this.Chart.OnlyPieSeries())
				{
					return false;
				}
				bool flag = true;
				if (this.AxisType != ChartAxisType.XAxis)
				{
					ChartSeriesCollection filteredSeriesByYAxis = this.Chart.Series.GetFilteredSeriesByYAxis((this as ChartYAxis).YAxisType);
					flag &= (filteredSeriesByYAxis.Count > 0);
				}
				return flag;
			}
			case ChartAxisVisibility.True:
				return true;
			case ChartAxisVisibility.False:
				return false;
			}
		}

		// Token: 0x17004624 RID: 17956
		// (get) Token: 0x0600E65A RID: 58970 RVA: 0x00334805 File Offset: 0x00332A05
		internal bool IsParentVisible
		{
			get
			{
				return this.Parent.Visible && !this.Parent.EmptySeriesMessage.IsVisible();
			}
		}

		// Token: 0x17004625 RID: 17957
		// (get) Token: 0x0600E65B RID: 58971 RVA: 0x00334829 File Offset: 0x00332A29
		// (set) Token: 0x0600E65C RID: 58972 RVA: 0x00334836 File Offset: 0x00332A36
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(ChartAxisVisibility), "Auto")]
		public ChartAxisVisibility Visible
		{
			get
			{
				return this.chartAxisAppearance.Visible;
			}
			set
			{
				this.chartAxisAppearance.Visible = value;
			}
		}

		// Token: 0x17004626 RID: 17958
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(false)]
		public ChartAxisItem this[int index]
		{
			get
			{
				return this.chartAxisItems[index];
			}
		}

		// Token: 0x17004627 RID: 17959
		// (get) Token: 0x0600E65E RID: 58974 RVA: 0x00334852 File Offset: 0x00332A52
		// (set) Token: 0x0600E65F RID: 58975 RVA: 0x00334873 File Offset: 0x00332A73
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[Description("Enables or disables automatic axis scaling.")]
		[DefaultValue(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoScale
		{
			get
			{
				return (bool)(base.ViewState["AutoScale"] ?? true);
			}
			set
			{
				base.ViewState["AutoScale"] = value;
			}
		}

		// Token: 0x17004628 RID: 17960
		// (get) Token: 0x0600E660 RID: 58976 RVA: 0x0033488B File Offset: 0x00332A8B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		public StyleAxis Appearance
		{
			get
			{
				return this.chartAxisAppearance;
			}
		}

		// Token: 0x17004629 RID: 17961
		// (get) Token: 0x0600E661 RID: 58977 RVA: 0x00334893 File Offset: 0x00332A93
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		public ChartLabel AxisLabel
		{
			get
			{
				return this.chartAxisLabel;
			}
		}

		// Token: 0x1700462A RID: 17962
		// (get) Token: 0x0600E662 RID: 58978 RVA: 0x0033489B File Offset: 0x00332A9B
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ChartPlotArea Parent
		{
			get
			{
				return this.chartAxisParent;
			}
		}

		// Token: 0x1700462B RID: 17963
		// (get) Token: 0x0600E663 RID: 58979 RVA: 0x003348A3 File Offset: 0x00332AA3
		// (set) Token: 0x0600E664 RID: 58980 RVA: 0x003348CC File Offset: 0x00332ACC
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(0.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Range")]
		[Description("Specifies the minimal value of the axis.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public virtual double MinValue
		{
			get
			{
				return (double)(base.ViewState["MinValue"] ?? 0.0);
			}
			set
			{
				if (!this.AutoScale)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x0600E665 RID: 58981 RVA: 0x003348F7 File Offset: 0x00332AF7
		protected void SetMinValue(double minValue)
		{
			base.ViewState["MinValue"] = minValue;
		}

		// Token: 0x0600E666 RID: 58982 RVA: 0x0033490F File Offset: 0x00332B0F
		protected bool ShouldSerializeMinValue()
		{
			return !this.AutoScale;
		}

		// Token: 0x0600E667 RID: 58983 RVA: 0x0033491A File Offset: 0x00332B1A
		protected void ResetMinValue()
		{
			this.MaxValue = 0.0;
		}

		// Token: 0x1700462C RID: 17964
		// (get) Token: 0x0600E668 RID: 58984 RVA: 0x0033492B File Offset: 0x00332B2B
		// (set) Token: 0x0600E669 RID: 58985 RVA: 0x00334954 File Offset: 0x00332B54
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Description("Specifies the maximal value of the axis.")]
		[DefaultValue(7.0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Range")]
		public virtual double MaxValue
		{
			get
			{
				return (double)(base.ViewState["MaxValue"] ?? 7.0);
			}
			set
			{
				if (!this.AutoScale)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x0600E66A RID: 58986 RVA: 0x0033497F File Offset: 0x00332B7F
		protected bool ShouldSerializeMaxValue()
		{
			return !this.AutoScale;
		}

		// Token: 0x0600E66B RID: 58987 RVA: 0x0033498A File Offset: 0x00332B8A
		protected void ResetMaxValue()
		{
			this.MaxValue = 7.0;
		}

		// Token: 0x0600E66C RID: 58988 RVA: 0x0033499B File Offset: 0x00332B9B
		protected void SetMaxValue(double maxValue)
		{
			base.ViewState["MaxValue"] = maxValue;
		}

		// Token: 0x1700462D RID: 17965
		// (get) Token: 0x0600E66D RID: 58989 RVA: 0x003349B3 File Offset: 0x00332BB3
		// (set) Token: 0x0600E66E RID: 58990 RVA: 0x003349DC File Offset: 0x00332BDC
		[PersistenceMode(PersistenceMode.Attribute)]
		[Browsable(true)]
		[Category("Range")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		[DefaultValue(1.0)]
		[Description("Specifies the step at which axis values are calculated.")]
		public virtual double Step
		{
			get
			{
				return (double)(base.ViewState["Step"] ?? 1.0);
			}
			set
			{
				if (!this.AutoScale)
				{
					this.chartAxisItems.Clear();
				}
				base.ViewState["Step"] = value;
			}
		}

		// Token: 0x0600E66F RID: 58991 RVA: 0x00334A07 File Offset: 0x00332C07
		protected bool ShouldSerializeStep()
		{
			return !this.AutoScale;
		}

		// Token: 0x0600E670 RID: 58992 RVA: 0x00334A12 File Offset: 0x00332C12
		protected void ResetStep()
		{
			this.Step = 1.0;
		}

		// Token: 0x1700462E RID: 17966
		// (get) Token: 0x0600E671 RID: 58993 RVA: 0x00334A23 File Offset: 0x00332C23
		// (set) Token: 0x0600E672 RID: 58994 RVA: 0x00334A44 File Offset: 0x00332C44
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public bool IsZeroBased
		{
			get
			{
				return (bool)(base.ViewState["IsZeroBased"] ?? true);
			}
			set
			{
				StateBag viewState = base.ViewState;
				string key = "IsZeroBased";
				this.chartAxisRealIsZeroBased = value;
				viewState[key] = value;
			}
		}

		// Token: 0x1700462F RID: 17967
		// (get) Token: 0x0600E673 RID: 58995 RVA: 0x00334A70 File Offset: 0x00332C70
		// (set) Token: 0x0600E674 RID: 58996 RVA: 0x00334A91 File Offset: 0x00332C91
		[DefaultValue(8)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		public int MaxItemsCount
		{
			get
			{
				return (int)(base.ViewState["MaxItemsCount"] ?? 8);
			}
			set
			{
				base.ViewState["MaxItemsCount"] = value;
			}
		}

		// Token: 0x17004630 RID: 17968
		// (get) Token: 0x0600E675 RID: 58997 RVA: 0x00334AA9 File Offset: 0x00332CA9
		// (set) Token: 0x0600E676 RID: 58998 RVA: 0x00334ACA File Offset: 0x00332CCA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Determines the type of shown values")]
		[DefaultValue(ChartAxisVisibleValues.All)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public ChartAxisVisibleValues VisibleValues
		{
			get
			{
				return (ChartAxisVisibleValues)(base.ViewState["VisibleValues"] ?? ChartAxisVisibleValues.All);
			}
			set
			{
				base.ViewState["VisibleValues"] = value;
			}
		}

		// Token: 0x17004631 RID: 17969
		// (get) Token: 0x0600E677 RID: 58999 RVA: 0x00334AE2 File Offset: 0x00332CE2
		// (set) Token: 0x0600E678 RID: 59000 RVA: 0x00334B03 File Offset: 0x00332D03
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[DefaultValue(1)]
		public int LabelStep
		{
			get
			{
				return (int)(base.ViewState["LabelStep"] ?? 1);
			}
			set
			{
				if (value < 1)
				{
					throw new ChartException("Axis LabelStep should be more than 0");
				}
				base.ViewState["LabelStep"] = value;
			}
		}

		// Token: 0x17004632 RID: 17970
		// (get) Token: 0x0600E679 RID: 59001 RVA: 0x00334B2A File Offset: 0x00332D2A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Axis Items Collection")]
		[Editor(typeof(ChartAxisItemsCollectionEditor), typeof(UITypeEditor))]
		[Category("Axis Items")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public ChartAxisItemsCollection Items
		{
			get
			{
				return this.chartAxisItems;
			}
		}

		// Token: 0x0600E67A RID: 59002 RVA: 0x00334B32 File Offset: 0x00332D32
		public ChartAxis(ChartPlotArea parent) : this(parent, parent)
		{
			this.shouldOptimizeMaxLength = false;
		}

		// Token: 0x0600E67B RID: 59003 RVA: 0x00334B44 File Offset: 0x00332D44
		public ChartAxis(ChartPlotArea parent, IContainer container) : base(container)
		{
			this.chartAxisParent = parent;
			this.VisibleValues = ChartAxisVisibleValues.All;
			this.chartAxisMaxAxisValue = double.NaN;
			this.chartAxisMinAxisValue = double.NaN;
			this.chartAxisMaxItemValue = double.NaN;
			this.chartAxisMinItemValue = double.NaN;
			this.chartAxisItems = new ChartAxisItemsCollection(this);
			this.chartAxisZoom = 1f;
			this.ResetCachedValues();
		}

		// Token: 0x0600E67C RID: 59004 RVA: 0x00334BC0 File Offset: 0x00332DC0
		internal virtual void Initialize(double min, double max)
		{
			this.chartAxisMinItemValue = min;
			this.chartAxisMaxItemValue = max;
			if (double.IsInfinity(min) || double.IsNaN(min))
			{
				this.chartAxisRealIsZeroBased = false;
			}
			if (this.AutoScale)
			{
				this.chartAxisItems.Clear();
				this.AutoCalcAxisExtents();
				return;
			}
			if (this.chartAxisItems.Count == 0)
			{
				this.AddRange(this.MinValue, this.MaxValue, this.Step);
				return;
			}
			this.SetRange();
		}

		// Token: 0x0600E67D RID: 59005 RVA: 0x00334C38 File Offset: 0x00332E38
		protected void AutoCalcAxisExtents()
		{
			ChartYAxis chartYAxis = this as ChartYAxis;
			if (!this.chartAxisMinItemValue.Equals(double.PositiveInfinity) || !this.chartAxisMaxItemValue.Equals(double.NegativeInfinity))
			{
				double num = this.chartAxisMinItemValue;
				double num2 = this.chartAxisMaxItemValue;
				double num3 = num;
				double num4 = num2;
				this.chartAxisOnlyPositiveValues = false;
				this.chartAxisOnlyNegativeValues = false;
				if (num3 > 0.0)
				{
					this.chartAxisOnlyPositiveValues = true;
				}
				if (num4 < 0.0)
				{
					this.chartAxisOnlyNegativeValues = true;
				}
				if (this.IsZeroBased)
				{
					if (this.chartAxisOnlyPositiveValues)
					{
						num3 = 0.0;
					}
					else if (this.chartAxisOnlyNegativeValues)
					{
						num4 = 0.0;
					}
				}
				switch (this.VisibleValues)
				{
				case ChartAxisVisibleValues.Positive:
					if (!this.chartAxisOnlyPositiveValues)
					{
						num3 = 0.0;
					}
					break;
				case ChartAxisVisibleValues.Negative:
					if (!this.chartAxisOnlyNegativeValues)
					{
						num4 = 0.0;
					}
					break;
				}
				bool flag = false;
				if (chartYAxis != null && chartYAxis.AxisMode == ChartYAxisMode.Extended)
				{
					this.MaxItemsCount -= 2;
					flag = true;
				}
				if (this.MaxItemsCount < 2)
				{
					this.MaxItemsCount = 2;
				}
				double step = this.CalculateStep(ref num3, ref num4);
				this.AddRange(num3, num4, step);
				if (flag)
				{
					this.MaxItemsCount += 2;
				}
				return;
			}
			ChartSeriesCollection chartSeriesCollection = this.Chart.Series;
			if (this.AxisType == ChartAxisType.XAxis)
			{
				chartSeriesCollection = chartSeriesCollection.GetXUsedSeriesCollection();
				if (chartSeriesCollection.Count == 0)
				{
					chartSeriesCollection = this.Chart.Series;
				}
			}
			else
			{
				chartSeriesCollection = chartSeriesCollection.GetYUsedSeriesCollection().GetFilteredSeriesByYAxis(chartYAxis.YAxisType);
			}
			int maxItemsCount = chartSeriesCollection.GetMaxItemsCount();
			if (maxItemsCount < 1)
			{
				this.AddRange(0.0, 7.0, 1.0);
				return;
			}
			this.AddRange(1.0, (double)maxItemsCount, 1.0);
		}

		// Token: 0x0600E67E RID: 59006 RVA: 0x00334E24 File Offset: 0x00333024
		internal virtual double CalculateStep(ref double minValue, ref double maxValue)
		{
			double num = minValue;
			double num2 = maxValue;
			if (minValue >= maxValue)
			{
				this.SetPositiveOrNegative(ref minValue, ref maxValue);
			}
			double calculatedStep = (maxValue - minValue) / (double)this.MaxItemsCount;
			double num3 = this.NormalizeStep(calculatedStep);
			double result = num3;
			if (this.chartAxisZoom != 1f)
			{
				double calculatedStep2 = (maxValue - minValue) / (double)((float)this.MaxItemsCount * this.chartAxisZoom);
				result = this.NormalizeStep(calculatedStep2);
			}
			if (num < num2)
			{
				this.AdjustingMinMax(ref minValue, ref maxValue, num3);
			}
			return result;
		}

		// Token: 0x0600E67F RID: 59007 RVA: 0x00334E9C File Offset: 0x0033309C
		private double NormalizeStep(double calculatedStep)
		{
			if (Math.Round(calculatedStep, 13) == 0.0)
			{
				calculatedStep = 1E-13;
			}
			double num = calculatedStep;
			int num2 = 0;
			if (calculatedStep >= 1.0)
			{
				while (num > 9.0)
				{
					num /= 10.0;
					num2++;
				}
			}
			else
			{
				while (Math.Round(num, 13) <= 1.0)
				{
					num2++;
					num *= 10.0;
				}
				if (num2 > 14)
				{
					throw new ChartException(string.Format("Impossible to show correct data, when difference between minimal and maximal values of series items less than 1E-{0}.", 14));
				}
			}
			int num3 = (int)Math.Round(num);
			if (num3 > 2 && num3 <= 5)
			{
				num3 = 5;
			}
			else if (num3 > 5 && num3 <= 9)
			{
				num3 = 10;
			}
			double num4 = (double)num3;
			if (calculatedStep >= 1.0)
			{
				while (num2-- > 0)
				{
					num4 *= 10.0;
				}
			}
			else
			{
				while (num2-- > 0)
				{
					num4 /= 10.0;
				}
			}
			return Math.Round(num4, 13);
		}

		// Token: 0x0600E680 RID: 59008 RVA: 0x00334F9C File Offset: 0x0033319C
		internal void AdjustingMinMax(ref double minValue, ref double maxValue, double dValue)
		{
			double num = this.chartAxisMaxItemValue;
			minValue = Math.Floor(minValue / dValue) * dValue;
			maxValue = minValue;
			if (this.IsZeroBased)
			{
				if (this.chartAxisOnlyPositiveValues)
				{
					minValue = 0.0;
					while (maxValue < num)
					{
						maxValue += dValue;
					}
				}
				if (this.chartAxisOnlyNegativeValues)
				{
					maxValue = 0.0;
				}
				else
				{
					while (maxValue < num)
					{
						maxValue += dValue;
					}
				}
			}
			else
			{
				while (maxValue < num)
				{
					maxValue += dValue;
				}
			}
			maxValue = Math.Round(maxValue, 13);
			ChartYAxis chartYAxis = this as ChartYAxis;
			if (chartYAxis != null)
			{
				switch (chartYAxis.AxisMode)
				{
				case ChartYAxisMode.Extended:
					if (maxValue != 0.0)
					{
						maxValue += dValue;
					}
					if (minValue != 0.0)
					{
						minValue -= dValue;
					}
					break;
				}
			}
			switch (this.VisibleValues)
			{
			case ChartAxisVisibleValues.All:
				break;
			case ChartAxisVisibleValues.Positive:
				if (minValue < 0.0)
				{
					minValue = 0.0;
				}
				break;
			case ChartAxisVisibleValues.Negative:
				if (maxValue > 0.0)
				{
					maxValue = 0.0;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600E681 RID: 59009 RVA: 0x003350B4 File Offset: 0x003332B4
		internal void SetPositiveOrNegative(ref double minValue, ref double maxValue)
		{
			if (minValue >= maxValue)
			{
				switch (this.VisibleValues)
				{
				case ChartAxisVisibleValues.All:
					minValue = -50.0;
					maxValue = 50.0;
					return;
				case ChartAxisVisibleValues.Positive:
					minValue = 0.0;
					if (maxValue == this.chartAxisMinItemValue || this.chartAxisOnlyNegativeValues || minValue == maxValue)
					{
						maxValue = 100.0;
						return;
					}
					maxValue = -this.chartAxisMinItemValue;
					break;
				case ChartAxisVisibleValues.Negative:
					maxValue = 0.0;
					if (minValue == this.chartAxisMaxItemValue || this.chartAxisOnlyPositiveValues || minValue == maxValue)
					{
						minValue = -100.0;
						return;
					}
					minValue = -this.chartAxisMaxItemValue;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600E682 RID: 59010 RVA: 0x0033516C File Offset: 0x0033336C
		public void AddItem(ChartAxisItem item, params ChartAxisItem[] items)
		{
			this.chartAxisItems.Add(item);
			foreach (ChartAxisItem item2 in items)
			{
				this.chartAxisItems.Add(item2);
			}
		}

		// Token: 0x0600E683 RID: 59011 RVA: 0x003351A8 File Offset: 0x003333A8
		public void AddItem(ChartAxisItemsCollection items)
		{
			foreach (ChartAxisItem item in items)
			{
				this.chartAxisItems.Add(item);
			}
		}

		// Token: 0x0600E684 RID: 59012 RVA: 0x003351F8 File Offset: 0x003333F8
		public void AddItem(ChartAxisItem[] items)
		{
			foreach (ChartAxisItem item in items)
			{
				this.chartAxisItems.Add(item);
			}
		}

		// Token: 0x0600E685 RID: 59013 RVA: 0x00335228 File Offset: 0x00333428
		public void AddItem(List<ChartAxisItem> items)
		{
			foreach (ChartAxisItem item in items)
			{
				this.chartAxisItems.Add(item);
			}
		}

		// Token: 0x0600E686 RID: 59014 RVA: 0x0033527C File Offset: 0x0033347C
		public ChartAxisItem GetItem(int index)
		{
			return this.chartAxisItems[index];
		}

		// Token: 0x0600E687 RID: 59015 RVA: 0x0033528A File Offset: 0x0033348A
		public void RemoveAllItems()
		{
			this.chartAxisItems.Clear();
		}

		// Token: 0x0600E688 RID: 59016 RVA: 0x00335298 File Offset: 0x00333498
		public void RemoveItem(ChartAxisItem item, params ChartAxisItem[] items)
		{
			this.chartAxisItems.Remove(item);
			foreach (ChartAxisItem item2 in items)
			{
				this.chartAxisItems.Remove(item2);
			}
		}

		// Token: 0x0600E689 RID: 59017 RVA: 0x003352D4 File Offset: 0x003334D4
		public void RemoveItem(int index, params int[] indexes)
		{
			this.chartAxisItems.RemoveAt(index);
			foreach (int index2 in indexes)
			{
				this.chartAxisItems.RemoveAt(index2);
			}
		}

		// Token: 0x0600E68A RID: 59018 RVA: 0x0033530D File Offset: 0x0033350D
		public void RemoveItem(int itemIndex)
		{
			this.chartAxisItems.DeleteItem(itemIndex);
		}

		// Token: 0x0600E68B RID: 59019 RVA: 0x0033531B File Offset: 0x0033351B
		public void RemoveLastItem()
		{
			this.chartAxisItems.DeleteItem(this.chartAxisItems.Count - 1);
		}

		// Token: 0x0600E68C RID: 59020 RVA: 0x00335335 File Offset: 0x00333535
		public void Clear()
		{
			this.chartAxisItems.Clear();
			this.chartAxisOnlyNegativeValues = false;
			this.chartAxisOnlyPositiveValues = false;
		}

		// Token: 0x0600E68D RID: 59021 RVA: 0x00335350 File Offset: 0x00333550
		public void AddRange(double minValue, double maxValue, double step)
		{
			this.CheckRange(minValue, maxValue, step);
			this.Clear();
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.Step = step;
			double num = minValue;
			this.chartAxisOnlyNegativeValues = true;
			this.chartAxisOnlyPositiveValues = true;
			double num2 = Math.Round(maxValue - num, 13) / step;
			double num3 = Math.Round(num2, 13);
			double num4 = num2 - num3;
			int num5;
			if (num4 == 0.0 || num4 > Math.Pow(10.0, -13.0))
			{
				num5 = (int)Math.Ceiling(num2);
			}
			else
			{
				num5 = (int)num3;
			}
			for (int i = 0; i <= num5; i++)
			{
				if (num >= 0.0)
				{
					if (this.chartAxisOnlyNegativeValues)
					{
						this.chartAxisOnlyNegativeValues = false;
					}
				}
				else if (this.chartAxisOnlyPositiveValues)
				{
					this.chartAxisOnlyPositiveValues = false;
				}
				this.AddItem(this.FormatLabel(num), num);
				num = Math.Round(num + step, 13);
			}
			this.chartAxisMinAxisValue = this.MinValue;
			this.chartAxisMaxAxisValue = this.MaxValue;
			this.DisableCachedValues();
		}

		// Token: 0x0600E68E RID: 59022 RVA: 0x00335462 File Offset: 0x00333662
		protected ChartAxisItem AddItem(string label, Color color)
		{
			return this.AddItem(label, color, true);
		}

		// Token: 0x0600E68F RID: 59023 RVA: 0x00335470 File Offset: 0x00333670
		protected ChartAxisItem AddItem(string label, Color color, bool visible)
		{
			ChartAxisItem chartAxisItem = new ChartAxisItem(label, color, visible, null);
			chartAxisItem.Appearance.styleChart = (chartAxisItem.Marker.Appearance.styleChart = this.Chart);
			this.chartAxisItems.Add(chartAxisItem);
			return chartAxisItem;
		}

		// Token: 0x0600E690 RID: 59024 RVA: 0x003354B8 File Offset: 0x003336B8
		internal ChartAxisItem AddItem(string label)
		{
			return this.AddItem(label, Color.Empty);
		}

		// Token: 0x0600E691 RID: 59025 RVA: 0x003354C8 File Offset: 0x003336C8
		internal ChartAxisItem AddItem(string label, double value)
		{
			ChartAxisItem chartAxisItem = this.AddItem(label);
			chartAxisItem.Value = (decimal)value;
			return chartAxisItem;
		}

		// Token: 0x0600E692 RID: 59026 RVA: 0x003354EB File Offset: 0x003336EB
		public void SetItemLabel(int itemIndex, string newLabelText)
		{
			this.chartAxisItems[itemIndex].TextBlock.Text = newLabelText;
		}

		// Token: 0x0600E693 RID: 59027 RVA: 0x00335504 File Offset: 0x00333704
		public void SetItemLabel(int itemIndex, ChartAxisItem newLabel)
		{
			this.chartAxisItems[itemIndex] = newLabel;
		}

		// Token: 0x0600E694 RID: 59028 RVA: 0x00335513 File Offset: 0x00333713
		public void SetItemColor(int itemIndex, Color newColor)
		{
			this.chartAxisItems[itemIndex].TextBlock.Appearance.TextProperties.Color = newColor;
		}

		// Token: 0x0600E695 RID: 59029 RVA: 0x00335536 File Offset: 0x00333736
		protected override void Dispose(bool disposing)
		{
			if (this.chartAxisLabel != null)
			{
				this.chartAxisLabel.Dispose();
				this.chartAxisLabel = null;
			}
			if (this.chartAxisAppearance != null)
			{
				this.chartAxisAppearance.Dispose();
				this.chartAxisAppearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04004239 RID: 16953
		protected StyleAxis chartAxisAppearance;

		// Token: 0x0400423A RID: 16954
		protected ChartLabel chartAxisLabel;

		// Token: 0x0400423B RID: 16955
		protected ChartAxisItemsCollection chartAxisItems;

		// Token: 0x0400423C RID: 16956
		protected ChartPlotArea chartAxisParent;

		// Token: 0x0400423D RID: 16957
		internal bool chartAxisOnlyNegativeValues;

		// Token: 0x0400423E RID: 16958
		internal bool chartAxisOnlyPositiveValues;

		// Token: 0x0400423F RID: 16959
		protected bool chartAxisRealIsZeroBased;

		// Token: 0x04004240 RID: 16960
		protected double chartAxisMinItemValue;

		// Token: 0x04004241 RID: 16961
		protected double chartAxisMaxItemValue;

		// Token: 0x04004242 RID: 16962
		protected double chartAxisMinAxisValue;

		// Token: 0x04004243 RID: 16963
		protected double chartAxisMaxAxisValue;

		// Token: 0x04004244 RID: 16964
		protected PointF chartAxisPointStart;

		// Token: 0x04004245 RID: 16965
		protected PointF chartAxisPointEnd;

		// Token: 0x04004246 RID: 16966
		internal bool shouldOptimizeMaxLength;

		// Token: 0x04004247 RID: 16967
		internal float chartAxisZoom;

		// Token: 0x04004248 RID: 16968
		protected float pixelsPerValue;

		// Token: 0x04004249 RID: 16969
		protected float zeroCoord;
	}
}
