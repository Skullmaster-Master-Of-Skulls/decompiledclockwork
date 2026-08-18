using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016EA RID: 5866
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class ChartDataTable : LayoutElement
	{
		// Token: 0x17004582 RID: 17794
		// (get) Token: 0x0600E3AD RID: 58285 RVA: 0x003277AB File Offset: 0x003259AB
		internal float[] SizesW
		{
			get
			{
				return this.dataTableSizesW;
			}
		}

		// Token: 0x17004583 RID: 17795
		// (get) Token: 0x0600E3AE RID: 58286 RVA: 0x003277B3 File Offset: 0x003259B3
		internal float[] SizesH
		{
			get
			{
				return this.dataTableSizesH;
			}
		}

		// Token: 0x17004584 RID: 17796
		// (get) Token: 0x0600E3AF RID: 58287 RVA: 0x003277BB File Offset: 0x003259BB
		internal ChartPlotArea PlotArea
		{
			get
			{
				return this.dataTablePlotArea;
			}
		}

		// Token: 0x17004585 RID: 17797
		// (get) Token: 0x0600E3B0 RID: 58288 RVA: 0x003277C3 File Offset: 0x003259C3
		internal string[][] Data
		{
			get
			{
				return this.dataTableData;
			}
		}

		// Token: 0x17004586 RID: 17798
		// (get) Token: 0x0600E3B1 RID: 58289 RVA: 0x003277CB File Offset: 0x003259CB
		// (set) Token: 0x0600E3B2 RID: 58290 RVA: 0x003277D8 File Offset: 0x003259D8
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Visible
		{
			get
			{
				return this.Appearance.Visible;
			}
			set
			{
				this.Appearance.Visible = value;
			}
		}

		// Token: 0x17004587 RID: 17799
		// (get) Token: 0x0600E3B3 RID: 58291 RVA: 0x003277E6 File Offset: 0x003259E6
		internal bool IsVisible
		{
			get
			{
				return this.Visible && this.dataTableShouldCalculate;
			}
		}

		// Token: 0x17004588 RID: 17800
		// (get) Token: 0x0600E3B4 RID: 58292 RVA: 0x003277F8 File Offset: 0x003259F8
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StyleChartDataTable Appearance
		{
			get
			{
				return (StyleChartDataTable)this.appearance;
			}
		}

		// Token: 0x17004589 RID: 17801
		// (get) Token: 0x0600E3B5 RID: 58293 RVA: 0x00327805 File Offset: 0x00325A05
		internal List<ChartMarker> SeriesMarkers
		{
			get
			{
				return this.seriesMarkers;
			}
		}

		// Token: 0x0600E3B6 RID: 58294 RVA: 0x00327810 File Offset: 0x00325A10
		private void FillData(ChartSeriesCollection seriesCollection)
		{
			this.dataTableData = new string[seriesCollection.Count + 1][];
			this.SeriesMarkers.Clear();
			ChartValueLimits valueLimits = this.dataTablePlotArea.Chart.Series.GetValueLimits();
			this.dataTablePlotArea.XAxis.Initialize(valueLimits.MinXValue, valueLimits.MaxXValue);
			int count = this.dataTablePlotArea.XAxis.Items.Count;
			this.dataTableData[0] = new string[count + 1];
			this.dataTableData[0][0] = string.Empty;
			int num = 1;
			foreach (ChartAxisItem chartAxisItem in this.dataTablePlotArea.XAxis.Items)
			{
				if (string.IsNullOrEmpty(chartAxisItem.TextBlock.Text))
				{
					this.dataTableData[0][num++] = this.dataTablePlotArea.XAxis.FormatLabel(Convert.ToDouble(chartAxisItem.Value));
				}
				else
				{
					this.dataTableData[0][num++] = chartAxisItem.TextBlock.VisibleText;
				}
			}
			for (int i = 1; i < this.dataTableData.Length; i++)
			{
				this.dataTableData[i] = new string[count + 1];
				this.dataTableData[i][0] = seriesCollection[i - 1].Name;
				ChartMarker chartMarker = new ChartMarker(this);
				chartMarker.appearance = (StyleMarker)this.PlotArea.Chart.Legend.Appearance.ItemMarkerAppearance.Clone();
				chartMarker.Visible = true;
				this.SeriesMarkers.Add(chartMarker);
				num = 1;
				int num2 = 0;
				while (num2 < count && num <= this.dataTableData[i].Length - 1)
				{
					this.dataTableData[i][num++] = string.Empty;
					num2++;
				}
				num = 1;
				foreach (ChartSeriesItem chartSeriesItem in seriesCollection[i - 1].Items)
				{
					if (num > this.dataTableData[i].Length - 1)
					{
						break;
					}
					if (seriesCollection[i - 1].IsXDependent && seriesCollection[i - 1].IsXDependentSeriesType)
					{
						for (int j = 0; j < count; j++)
						{
							double num3 = double.IsNaN(chartSeriesItem.XValue) ? 0.0 : chartSeriesItem.XValue;
							if (Math.Abs(num3 - (double)this.dataTablePlotArea.XAxis.Items[j].Value) < 1E-15)
							{
								this.dataTableData[i][j + 1] = chartSeriesItem.Parent.FormatValues(chartSeriesItem.Parent.DefaultLabelValue, chartSeriesItem);
								break;
							}
						}
					}
					else
					{
						this.dataTableData[i][num++] = chartSeriesItem.Parent.FormatValues(chartSeriesItem.Parent.DefaultLabelValue, chartSeriesItem);
					}
				}
			}
		}

		// Token: 0x0600E3B7 RID: 58295 RVA: 0x00327B6C File Offset: 0x00325D6C
		internal void Reset()
		{
			this.appearance = new StyleChartDataTable(this);
			this.dataTableShouldCalculate = true;
		}

		// Token: 0x0600E3B8 RID: 58296 RVA: 0x00327B84 File Offset: 0x00325D84
		internal void Initilaize()
		{
			this.dataTableData = new string[0][];
			if (this.dataTablePlotArea != null)
			{
				ChartSeriesCollection chartSeriesCollection = this.dataTablePlotArea.SeriesCollection();
				if (chartSeriesCollection.Count > 0)
				{
					this.FillData(chartSeriesCollection);
					this.dataTableSizesW = new float[this.dataTableData[0].Length];
					this.dataTableSizesH = new float[this.dataTableData.Length];
				}
			}
		}

		// Token: 0x0600E3B9 RID: 58297 RVA: 0x00327BE9 File Offset: 0x00325DE9
		private string WrapText(string str, RenderEngine renderEngine)
		{
			return this.WrapText(str, renderEngine, -1f);
		}

		// Token: 0x0600E3BA RID: 58298 RVA: 0x00327BF8 File Offset: 0x00325DF8
		private string WrapText(string str, RenderEngine renderEngine, float width)
		{
			ChartText chartText = new ChartText(str, this.Appearance.TextProperties.Font, renderEngine.graphics);
			if (width > 0f)
			{
				chartText.Distibute(renderEngine.chart.TextWrapFactor, width);
			}
			else
			{
				chartText.Distibute(renderEngine.chart.TextWrapFactor);
			}
			return chartText.ToString();
		}

		// Token: 0x0600E3BB RID: 58299 RVA: 0x00327C58 File Offset: 0x00325E58
		internal void Measure(RenderEngine renderEngine)
		{
			if (this.IsVisible)
			{
				this.Initilaize();
				if (this.Data.Length > 0)
				{
					bool flag = renderEngine.chart.ShouldApplyTextWrapping(this.Appearance.AutoTextWrap);
					for (int i = 0; i < this.Data.Length; i++)
					{
						if (flag)
						{
							this.Data[i][0] = this.WrapText(this.Data[i][0], renderEngine);
						}
						SizeF sizeF = renderEngine.graphics.MeasureString(this.Data[i][0], this.Appearance.TextProperties.Font);
						if (i > 0 && this.SeriesMarkers.Count >= i)
						{
							ChartMarker chartMarker = this.SeriesMarkers[i - 1];
							float num = (float)this.Appearance.TextProperties.Font.Height * 0.75f;
							chartMarker.Appearance.Dimensions.SetDimensions(num, num);
							sizeF.Width += num + 4f;
						}
						if (sizeF.Width > this.SizesW[0])
						{
							this.SizesW[0] = sizeF.Width + this.Appearance.Dimensions.Paddings.Left.PixelValue + this.Appearance.Dimensions.Paddings.Right.PixelValue;
						}
						if (this.SizesH[i] < sizeF.Height)
						{
							this.SizesH[i] = (float)Math.Ceiling((double)sizeF.Height);
						}
					}
					int num2 = this.Data.Length;
					int num3 = this.SizesW.Length;
					int num4 = this.SizesH.Length;
					for (int j = 0; j < num2; j++)
					{
						int num5 = this.Data[j].Length;
						for (int k = 1; k < num5; k++)
						{
							try
							{
								switch (this.Appearance.RenderType)
								{
								case TableRenderType.AutoSize:
								{
									if (flag)
									{
										this.Data[j][k] = this.WrapText(this.Data[j][k], renderEngine);
									}
									SizeF sizeF2 = renderEngine.graphics.MeasureString(this.Data[j][k], this.Appearance.TextProperties.Font);
									if (this.SizesW[k] < sizeF2.Width)
									{
										this.SizesW[k] = (float)Math.Round((double)sizeF2.Width) + this.Appearance.Dimensions.Paddings.Left.PixelValue + this.Appearance.Dimensions.Paddings.Right.PixelValue;
									}
									if (this.SizesH[j] < sizeF2.Height)
									{
										this.SizesH[j] = (float)Math.Round((double)sizeF2.Height) + this.Appearance.Dimensions.Paddings.Top.PixelValue + this.Appearance.Dimensions.Paddings.Bottom.PixelValue;
									}
									if (k == 0 && j == num2 - 1)
									{
										this.SizesW[k] += Convert.ToSingle((double)this.Appearance.TextProperties.Font.SizeInPoints * 1.5);
									}
									break;
								}
								case TableRenderType.CellFixedSize:
									this.SizesW[k] = (float)this.Appearance.CellWidth + this.Appearance.Dimensions.Paddings.Left.PixelValue + this.Appearance.Dimensions.Paddings.Right.PixelValue;
									if (flag)
									{
										this.Data[j][k] = this.WrapText(this.Data[j][k], renderEngine, this.SizesW[k]);
									}
									this.SizesH[j] = (float)this.Appearance.CellHeight + this.Appearance.Dimensions.Paddings.Top.PixelValue + this.Appearance.Dimensions.Paddings.Bottom.PixelValue;
									break;
								case TableRenderType.TableFixedSize:
									this.SizesW[k] = (this.Appearance.Dimensions.Width.PixelValue - this.SizesW[0]) / (float)(num3 - 1);
									if (flag)
									{
										this.Data[j][k] = this.WrapText(this.Data[j][k], renderEngine, this.SizesW[k]);
									}
									this.SizesH[j] = (this.Appearance.Dimensions.Height.PixelValue - this.Appearance.Border.Width * (float)(num4 - 1)) / (float)num4;
									break;
								case TableRenderType.PlotAreaRelative:
								{
									this.SizesW[k] = this.PlotArea.Appearance.Dimensions.Width.PixelValue / (float)(num3 - 1);
									if (flag)
									{
										this.Data[j][k] = this.WrapText(this.Data[j][k], renderEngine, this.SizesW[k]);
									}
									SizeF sizeF2 = renderEngine.graphics.MeasureString(this.Data[j][k], this.Appearance.TextProperties.Font);
									if (this.SizesH[j] < sizeF2.Height)
									{
										this.SizesH[j] = (float)Math.Ceiling((double)sizeF2.Height) + this.Appearance.Dimensions.Paddings.Top.PixelValue + this.Appearance.Dimensions.Paddings.Bottom.PixelValue;
									}
									break;
								}
								}
							}
							catch
							{
								return;
							}
						}
					}
					if (this.Appearance.RenderType != TableRenderType.TableFixedSize)
					{
						float num6 = Tools.ArraySum(this.SizesH);
						if (this.Appearance.Border.Width > 1f)
						{
							num6 += this.Appearance.Border.Width * (float)(num4 - 1);
						}
						this.Appearance.Dimensions.SetDimensions(Tools.ArraySum(this.SizesW), num6);
					}
				}
			}
		}

		// Token: 0x0600E3BC RID: 58300 RVA: 0x00328284 File Offset: 0x00326484
		internal override void CalculatePosition(RenderEngine renderEngine)
		{
			if (!this.IsVisible || !this.PlotArea.Visible || this.PlotArea.EmptySeriesMessage.IsVisible())
			{
				return;
			}
			if (this.Data.Length > 0)
			{
				if (this.Appearance.Position.Auto && this.Appearance.RenderType == TableRenderType.PlotAreaRelative)
				{
					this.Appearance.Position.Reset();
					this.Appearance.Position.X = this.PlotArea.Appearance.Position.X - this.SizesW[0];
					this.Appearance.Position.Y = (float)((int)Math.Round((double)(this.PlotArea.Appearance.Position.Y + this.PlotArea.Appearance.Dimensions.Height.PixelValue) + Math.Ceiling((double)(this.PlotArea.Appearance.Border.Width / 2f))));
					if (this.PlotArea.Chart.SeriesOrientation == ChartSeriesOrientation.Horizontal)
					{
						if (this.PlotArea.Appearance.Dimensions.AutoSize)
						{
							if (this.dataTablePlotArea.Chart.AutoLayoutWrapper)
							{
								this.Appearance.Position.Y += this.PlotArea.YAxis.GetHeight() + (float)this.PlotArea.YAxis.TicksLength;
							}
							else
							{
								this.Appearance.Position.Y += this.PlotArea.YAxis.GetHeight() + (float)this.PlotArea.YAxis.TicksLength;
							}
						}
						else
						{
							this.Appearance.Position.Y = this.PlotArea.Appearance.Dimensions.Height.PixelValue + this.PlotArea.Appearance.Position.Y + this.PlotArea.YAxis.GetHeight();
						}
						this.Appearance.Position.Y += this.Appearance.Dimensions.Margins.Top.PixelValue + this.Appearance.Border.Width / 2f;
					}
				}
				base.CalculatePosition(renderEngine);
			}
		}

		// Token: 0x0600E3BD RID: 58301 RVA: 0x003284F0 File Offset: 0x003266F0
		public ChartDataTable(ChartPlotArea plotArea) : this(plotArea, null)
		{
		}

		// Token: 0x0600E3BE RID: 58302 RVA: 0x003284FC File Offset: 0x003266FC
		public ChartDataTable(ChartPlotArea plotArea, IContainer container) : base(new StyleChartDataTable(), container)
		{
			this.dataTableData = new string[0][];
			this.dataTableSizesW = new float[0];
			this.dataTableSizesH = new float[0];
			this.dataTablePlotArea = plotArea;
			this.seriesMarkers = new List<ChartMarker>();
			this.Reset();
		}

		// Token: 0x040041B3 RID: 16819
		private string[][] dataTableData;

		// Token: 0x040041B4 RID: 16820
		private ChartPlotArea dataTablePlotArea;

		// Token: 0x040041B5 RID: 16821
		private float[] dataTableSizesW;

		// Token: 0x040041B6 RID: 16822
		private float[] dataTableSizesH;

		// Token: 0x040041B7 RID: 16823
		private readonly List<ChartMarker> seriesMarkers;

		// Token: 0x040041B8 RID: 16824
		internal bool dataTableShouldCalculate;
	}
}
