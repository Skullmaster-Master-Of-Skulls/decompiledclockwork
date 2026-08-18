using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting
{
	// Token: 0x0200173B RID: 5947
	public class ChartSeriesCollection : ChartingStateManagedCollection<ChartSeries>
	{
		// Token: 0x170046A1 RID: 18081
		// (get) Token: 0x0600E802 RID: 59394 RVA: 0x0033EA3A File Offset: 0x0033CC3A
		// (set) Token: 0x0600E803 RID: 59395 RVA: 0x0033EA44 File Offset: 0x0033CC44
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Chart Parent
		{
			get
			{
				return this.chartSeriesCollectionParent;
			}
			set
			{
				this.chartSeriesCollectionParent = value;
				foreach (ChartSeries chartSeries in this)
				{
					chartSeries.Appearance.styleChart = this.chartSeriesCollectionParent;
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						chartSeriesItem.Appearance.styleChart = this.chartSeriesCollectionParent;
						chartSeriesItem.PointAppearance.styleChart = this.chartSeriesCollectionParent;
					}
				}
			}
		}

		// Token: 0x170046A2 RID: 18082
		[Browsable(false)]
		[NotifyParentProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ChartSeries this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
				base.List[index].SetParent(this);
			}
		}

		// Token: 0x170046A3 RID: 18083
		// (get) Token: 0x0600E806 RID: 59398 RVA: 0x0033EB24 File Offset: 0x0033CD24
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsXDepended
		{
			get
			{
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.IsXDependent)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600E807 RID: 59399 RVA: 0x0033EB74 File Offset: 0x0033CD74
		public ChartSeriesCollection()
		{
		}

		// Token: 0x0600E808 RID: 59400 RVA: 0x0033EB7C File Offset: 0x0033CD7C
		public ChartSeriesCollection(Chart parent)
		{
			this.chartSeriesCollectionParent = parent;
		}

		// Token: 0x0600E809 RID: 59401 RVA: 0x0033EB8C File Offset: 0x0033CD8C
		private double GetMinStacked100Value(ChartSeriesType seriesType)
		{
			double positiveInfinity = double.PositiveInfinity;
			if (seriesType == ChartSeriesType.StackedBar100 || seriesType == ChartSeriesType.StackedArea100 || seriesType == ChartSeriesType.StackedSplineArea100)
			{
				if (this.GetMinStackedValue(seriesType) < 0.0)
				{
					return -100.0;
				}
				bool flag = false;
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.Type == seriesType)
					{
						flag = true;
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (chartSeriesItem.YValue < 0.0)
							{
								return -100.0;
							}
						}
					}
				}
				if (flag)
				{
					return 0.0;
				}
				return positiveInfinity;
			}
			return positiveInfinity;
		}

		// Token: 0x0600E80A RID: 59402 RVA: 0x0033EC84 File Offset: 0x0033CE84
		private double GetMaxStacked100Value()
		{
			double num = double.NegativeInfinity;
			foreach (ChartSeries chartSeries in this)
			{
				if (chartSeries.IsStacked100)
				{
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						num = Math.Max(num, chartSeriesItem.YValue);
					}
				}
			}
			if (num > 0.0)
			{
				return 100.0;
			}
			if (num != double.NegativeInfinity)
			{
				return 0.0;
			}
			return double.NegativeInfinity;
		}

		// Token: 0x0600E80B RID: 59403 RVA: 0x0033ED54 File Offset: 0x0033CF54
		private double GetMinStackedValue(ChartSeriesType seriesType)
		{
			double num = double.PositiveInfinity;
			double num2 = double.PositiveInfinity;
			int maxItemsCount = this.GetMaxItemsCount(seriesType);
			bool flag = false;
			if (this.IsXDepended && seriesType == ChartSeriesType.StackedBar)
			{
				ChartSeriesCollection seriesCollection = this.GetSeriesCollection(ChartSeriesType.StackedBar);
				if (seriesCollection.Count > 0 && seriesCollection.IsXDepended)
				{
					flag = true;
					Dictionary<double, double> dictionary = new Dictionary<double, double>();
					foreach (ChartSeries chartSeries in seriesCollection)
					{
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (chartSeriesItem.YValue <= 0.0)
							{
								if (!double.IsNaN(chartSeriesItem.XValue))
								{
									if (dictionary.ContainsKey(chartSeriesItem.XValue))
									{
										Dictionary<double, double> dictionary2;
										double xvalue;
										(dictionary2 = dictionary)[xvalue = chartSeriesItem.XValue] = dictionary2[xvalue] + chartSeriesItem.YValue;
									}
									else
									{
										dictionary.Add(chartSeriesItem.XValue, chartSeriesItem.YValue);
									}
								}
								else if (dictionary.ContainsKey(0.0))
								{
									Dictionary<double, double> dictionary3;
									(dictionary3 = dictionary)[0.0] = dictionary3[0.0] + chartSeriesItem.YValue;
								}
								else
								{
									dictionary.Add(0.0, chartSeriesItem.YValue);
								}
							}
						}
					}
					foreach (KeyValuePair<double, double> keyValuePair in dictionary)
					{
						if (num2 > keyValuePair.Value)
						{
							num2 = keyValuePair.Value;
						}
					}
				}
			}
			for (int i = 0; i < maxItemsCount; i++)
			{
				double num3 = double.PositiveInfinity;
				foreach (ChartSeries chartSeries2 in this)
				{
					if (chartSeries2.Type == seriesType && i < chartSeries2.Items.Count)
					{
						if (chartSeries2.Type == ChartSeriesType.StackedBar)
						{
							if (!flag)
							{
								if (chartSeries2.Items[i].YValue < num3 && num3 >= 0.0 && chartSeries2.Items[i].YValue != 0.0)
								{
									num3 = chartSeries2.Items[i].YValue;
								}
								else if (chartSeries2.Items[i].YValue < 0.0 && num3 < 0.0)
								{
									num3 += chartSeries2.Items[i].YValue;
								}
							}
						}
						else if (chartSeries2.Items[i].YValue <= 0.0 && num3 != double.PositiveInfinity)
						{
							num3 += chartSeries2.Items[i].YValue;
						}
						else if (chartSeries2.Items[i].YValue < num3)
						{
							num3 = chartSeries2.Items[i].YValue;
						}
					}
				}
				if (num3 < num)
				{
					num = num3;
				}
			}
			if (num2 < num)
			{
				num = num2;
			}
			return num;
		}

		// Token: 0x0600E80C RID: 59404 RVA: 0x0033F138 File Offset: 0x0033D338
		private double GetMaxStackedValue(ChartSeriesType seriesType)
		{
			double num = double.NegativeInfinity;
			double num2 = double.NegativeInfinity;
			int maxItemsCount = this.GetMaxItemsCount(seriesType);
			bool flag = false;
			if (this.IsXDepended && seriesType == ChartSeriesType.StackedBar)
			{
				ChartSeriesCollection seriesCollection = this.GetSeriesCollection(ChartSeriesType.StackedBar);
				if (seriesCollection.Count > 0 && seriesCollection.IsXDepended)
				{
					flag = true;
					Dictionary<double, double> dictionary = new Dictionary<double, double>();
					foreach (ChartSeries chartSeries in seriesCollection)
					{
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (chartSeriesItem.YValue >= 0.0)
							{
								if (!double.IsNaN(chartSeriesItem.XValue))
								{
									if (dictionary.ContainsKey(chartSeriesItem.XValue))
									{
										Dictionary<double, double> dictionary2;
										double xvalue;
										(dictionary2 = dictionary)[xvalue = chartSeriesItem.XValue] = dictionary2[xvalue] + chartSeriesItem.YValue;
									}
									else
									{
										dictionary.Add(chartSeriesItem.XValue, chartSeriesItem.YValue);
									}
								}
								else if (dictionary.ContainsKey(0.0))
								{
									Dictionary<double, double> dictionary3;
									(dictionary3 = dictionary)[0.0] = dictionary3[0.0] + chartSeriesItem.YValue;
								}
								else
								{
									dictionary.Add(0.0, chartSeriesItem.YValue);
								}
							}
						}
					}
					foreach (KeyValuePair<double, double> keyValuePair in dictionary)
					{
						if (num2 < keyValuePair.Value)
						{
							num2 = keyValuePair.Value;
						}
					}
				}
			}
			for (int i = 0; i < maxItemsCount; i++)
			{
				double num3 = double.NegativeInfinity;
				foreach (ChartSeries chartSeries2 in this)
				{
					if (chartSeries2.Type == seriesType && i < chartSeries2.Items.Count)
					{
						if (chartSeries2.Type == ChartSeriesType.StackedBar)
						{
							if (!flag)
							{
								if (chartSeries2.Items[i].YValue > num3 && num3 < 0.0 && chartSeries2.Items[i].YValue != 0.0)
								{
									num3 = chartSeries2.Items[i].YValue;
								}
								else if (chartSeries2.Items[i].YValue > 0.0 && num3 >= 0.0)
								{
									num3 += chartSeries2.Items[i].YValue;
								}
							}
						}
						else if (chartSeries2.Items[i].YValue >= 0.0 && num3 != double.NegativeInfinity)
						{
							num3 += chartSeries2.Items[i].YValue;
						}
						else if (chartSeries2.Items[i].YValue > num3)
						{
							num3 = chartSeries2.Items[i].YValue;
						}
					}
				}
				if (num3 > num)
				{
					num = num3;
				}
			}
			if (num2 > num)
			{
				num = num2;
			}
			return num;
		}

		// Token: 0x0600E80D RID: 59405 RVA: 0x0033F51C File Offset: 0x0033D71C
		private static double GetMinYValue(double value1, double value2, bool checkNaN)
		{
			if (double.IsNaN(value2))
			{
				if (checkNaN)
				{
					return value1;
				}
				value2 = 0.0;
			}
			if (double.IsNaN(value1))
			{
				if (checkNaN)
				{
					return value2;
				}
				value1 = 0.0;
			}
			if (value1 >= value2)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x0600E80E RID: 59406 RVA: 0x0033F555 File Offset: 0x0033D755
		private static double GetMaxYValue(double value1, double value2, bool checkNaN)
		{
			if (double.IsNaN(value2))
			{
				if (checkNaN)
				{
					return value1;
				}
				value2 = 0.0;
			}
			if (double.IsNaN(value1))
			{
				if (checkNaN)
				{
					return value2;
				}
				value1 = 0.0;
			}
			if (value1 <= value2)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x0600E80F RID: 59407 RVA: 0x0033F590 File Offset: 0x0033D790
		private bool OnlyBezierSeries()
		{
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				if (this[i].Type != ChartSeriesType.Bezier)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E810 RID: 59408 RVA: 0x0033F5C4 File Offset: 0x0033D7C4
		internal bool OnlyPieSeries()
		{
			foreach (ChartSeries chartSeries in this)
			{
				if (chartSeries.Type != ChartSeriesType.Pie)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E811 RID: 59409 RVA: 0x0033F618 File Offset: 0x0033D818
		internal void DefineItemsLabelText()
		{
			ChartPlotArea plotArea = this.Parent.PlotArea;
			Chart parent = this.Parent;
			plotArea.PopularValues = PopularCollection.GetPopularValues(parent);
			ChartSeriesCollection seriesCollection = this.GetSeriesCollection(ChartSeriesType.StackedBar100);
			int maxItemsCount = seriesCollection.GetMaxItemsCount(ChartSeriesType.StackedBar100);
			plotArea.XAxis.GetPixelStep();
			Dictionary<double, double> dictionary = new Dictionary<double, double>();
			bool flag = false;
			if (plotArea.XAxis.OrderingMode != BarOrderingMode.Classic)
			{
				flag = true;
				if (seriesCollection.Count > 0 && seriesCollection.IsXDepended)
				{
					dictionary = parent.Series.GetSumsForStacked(ChartSeriesType.StackedBar100);
				}
			}
			for (int i = 0; i < maxItemsCount; i++)
			{
				double num = 0.0;
				if (!flag)
				{
					foreach (ChartSeries chartSeries in seriesCollection)
					{
						if (i < chartSeries.Items.Count)
						{
							num += (chartSeries[i].Empty ? 0.0 : Math.Abs(chartSeries[i].YValue));
						}
					}
				}
				foreach (ChartSeries chartSeries2 in seriesCollection)
				{
					if (i < chartSeries2.Items.Count)
					{
						ChartSeriesItem chartSeriesItem = chartSeries2[i];
						double relativeValue = 0.0;
						double key = double.IsNaN(chartSeriesItem.XValue) ? 0.0 : chartSeriesItem.XValue;
						if (flag && dictionary.ContainsKey(key))
						{
							num = dictionary[key];
						}
						if (!chartSeriesItem.Empty && num != 0.0)
						{
							relativeValue = chartSeriesItem.YValue / num;
						}
						chartSeriesItem.RelativeValue = relativeValue;
					}
				}
			}
			foreach (ChartSeries chartSeries3 in this)
			{
				chartSeries3.Items.DefineLabelText(chartSeries3);
			}
		}

		// Token: 0x0600E812 RID: 59410 RVA: 0x0033F84C File Offset: 0x0033DA4C
		internal void ClearAutoGeneratedItemsLabelText()
		{
			foreach (ChartSeries chartSeries in this)
			{
				chartSeries.Items.ClearAutoGeneratedLabelText();
			}
		}

		// Token: 0x0600E813 RID: 59411 RVA: 0x0033F898 File Offset: 0x0033DA98
		internal string CheckForErrors()
		{
			int num = 0;
			int num2 = -1;
			int num3 = 0;
			int num4 = -1;
			int num5 = 0;
			int num6 = -1;
			int num7 = 0;
			int num8 = -1;
			int num9 = 0;
			int num10 = -1;
			int num11 = 0;
			int num12 = -1;
			string arg = "series must have equal items count";
			foreach (ChartSeries chartSeries in this)
			{
				if (chartSeries.Type == ChartSeriesType.Bezier)
				{
					string result = "Bezier type chart requires 1+3 series items to draw.\nYou supplied wrong items count in series: ";
					if (!chartSeries.CheckBezierSeriesForItemsCount(ref result))
					{
						return result;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedArea)
				{
					num++;
					if (num > 1)
					{
						if (num2 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num2 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedLine)
				{
					num9++;
					if (num9 > 1)
					{
						if (num10 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num10 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedSpline)
				{
					num11++;
					if (num11 > 1)
					{
						if (num12 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num12 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedSplineArea)
				{
					num3++;
					if (num3 > 1)
					{
						if (num4 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num4 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedArea100)
				{
					num5++;
					if (num5 > 1)
					{
						if (num6 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num6 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.StackedSplineArea100)
				{
					num7++;
					if (num7 > 1)
					{
						if (num8 != chartSeries.Items.Count)
						{
							return string.Format("{0} {1}", chartSeries.Type.ToString(), arg);
						}
					}
					else
					{
						num8 = chartSeries.Items.Count;
					}
				}
				else if (chartSeries.Type == ChartSeriesType.CandleStick)
				{
					bool flag = false;
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						if (double.IsNaN(chartSeriesItem.YValue) || double.IsNaN(chartSeriesItem.YValue2) || double.IsNaN(chartSeriesItem.YValue3) || double.IsNaN(chartSeriesItem.YValue4))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						return "CandleStick series requires all Y values to be set.";
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x170046A4 RID: 18084
		// (get) Token: 0x0600E814 RID: 59412 RVA: 0x0033FC20 File Offset: 0x0033DE20
		internal int BarSeriesCount
		{
			get
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.Items.Count > 0)
					{
						ChartSeriesType type = chartSeries.Type;
						switch (type)
						{
						case ChartSeriesType.Bar:
							break;
						case ChartSeriesType.StackedBar:
							num2++;
							continue;
						case ChartSeriesType.StackedBar100:
							num3++;
							continue;
						default:
							if (type != ChartSeriesType.Gantt && type != ChartSeriesType.CandleStick)
							{
								continue;
							}
							break;
						}
						num++;
					}
				}
				if (num2 > 0)
				{
					num++;
				}
				if (num3 > 0)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x0600E815 RID: 59413 RVA: 0x0033FCC4 File Offset: 0x0033DEC4
		internal static bool Stacked(ChartSeries chartSeries)
		{
			return chartSeries.IsStacked || chartSeries.IsStacked100;
		}

		// Token: 0x0600E816 RID: 59414 RVA: 0x0033FCD6 File Offset: 0x0033DED6
		internal static bool Stacked100(ChartSeries chartSeries)
		{
			return chartSeries.IsStacked100;
		}

		// Token: 0x0600E817 RID: 59415 RVA: 0x0033FCE0 File Offset: 0x0033DEE0
		internal int GetMaxItemsCount(ChartSeriesType seriesType)
		{
			int num = 0;
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeries.Type == seriesType)
				{
					int count2 = chartSeries.Items.Count;
					if (count2 > num)
					{
						num = count2;
					}
				}
			}
			return num;
		}

		// Token: 0x0600E818 RID: 59416 RVA: 0x0033FD2C File Offset: 0x0033DF2C
		internal int GetSeriesCount(ChartSeriesType seriesType)
		{
			int num = 0;
			foreach (ChartSeries chartSeries in this)
			{
				if (chartSeries.Type == seriesType)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600E819 RID: 59417 RVA: 0x0033FD80 File Offset: 0x0033DF80
		internal double GetSumForStacked(int itemsPosition)
		{
			if (itemsPosition == -1)
			{
				return 0.0;
			}
			double num = 0.0;
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				try
				{
					num += this[i].Items[itemsPosition].YValue;
				}
				catch
				{
					num += 0.0;
				}
			}
			return num;
		}

		// Token: 0x0600E81A RID: 59418 RVA: 0x0033FDF4 File Offset: 0x0033DFF4
		internal Dictionary<double, double> GetSumsForStacked(ChartSeriesType seriesType)
		{
			Dictionary<double, double> dictionary = new Dictionary<double, double>();
			ChartSeriesCollection seriesCollection = this.GetSeriesCollection(seriesType);
			foreach (ChartSeries chartSeries in seriesCollection)
			{
				foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
				{
					double num = Math.Abs(chartSeriesItem.YValue);
					if (!double.IsNaN(chartSeriesItem.XValue))
					{
						if (dictionary.ContainsKey(chartSeriesItem.XValue))
						{
							Dictionary<double, double> dictionary2;
							double xvalue;
							(dictionary2 = dictionary)[xvalue = chartSeriesItem.XValue] = dictionary2[xvalue] + num;
						}
						else
						{
							dictionary.Add(chartSeriesItem.XValue, num);
						}
					}
					else if (dictionary.ContainsKey(0.0))
					{
						Dictionary<double, double> dictionary3;
						(dictionary3 = dictionary)[0.0] = dictionary3[0.0] + num;
					}
					else
					{
						dictionary.Add(0.0, num);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600E81B RID: 59419 RVA: 0x0033FF30 File Offset: 0x0033E130
		internal bool HaveXValue()
		{
			foreach (ChartSeries chartSeries in this)
			{
				if (chartSeries.Type == ChartSeriesType.Bar | chartSeries.Type == ChartSeriesType.StackedBar | chartSeries.Type == ChartSeriesType.StackedBar100 | chartSeries.Type == ChartSeriesType.CandleStick)
				{
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						if (!double.IsNaN(chartSeriesItem.XValue))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600E81C RID: 59420 RVA: 0x0033FFEC File Offset: 0x0033E1EC
		internal ChartValueLimits GetValueLimits()
		{
			ChartValueLimits chartValueLimits = new ChartValueLimits(double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity);
			double num = double.PositiveInfinity;
			double num2 = double.NegativeInfinity;
			double minYValue = this.GetMinYValue();
			double maxYValue = this.GetMaxYValue();
			bool checkNaN = this.HaveXValue();
			bool flag = this.OnlyBezierSeries();
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				ChartSeries chartSeries = this[i];
				bool flag2 = ChartSeriesCollection.Stacked(chartSeries);
				ChartSeriesType type = chartSeries.Type;
				int count2 = chartSeries.Items.Count;
				for (int j = 0; j < count2; j++)
				{
					ChartSeriesItem chartSeriesItem = chartSeries.Items[j];
					if (!flag2 || chartSeries.IsXDependent)
					{
						double num3 = chartSeriesItem.XValue;
						double num4 = chartSeriesItem.XValue2;
						if (chartSeries.IsXDependent)
						{
							num3 = (double.IsNaN(num3) ? 0.0 : num3);
							num4 = (double.IsNaN(num4) ? 0.0 : num4);
						}
						num = ChartSeriesCollection.GetMinYValue(num, num3, checkNaN);
						if (chartSeries.Type == ChartSeriesType.Gantt)
						{
							num = ChartSeriesCollection.GetMinYValue(num, num4, true);
						}
						num2 = ChartSeriesCollection.GetMaxYValue(num2, num3, checkNaN);
						if (chartSeries.Type == ChartSeriesType.Gantt)
						{
							num2 = ChartSeriesCollection.GetMaxYValue(num2, num4, true);
						}
					}
					if (!flag && !flag2 && !chartSeriesItem.Empty)
					{
						ChartSeriesType chartSeriesType = type;
						if (chartSeriesType <= ChartSeriesType.Gantt)
						{
							if (chartSeriesType == ChartSeriesType.StackedBar)
							{
								goto IL_279;
							}
							if (chartSeriesType != ChartSeriesType.Gantt)
							{
								goto IL_259;
							}
						}
						else if (chartSeriesType != ChartSeriesType.Bubble)
						{
							if (chartSeriesType != ChartSeriesType.CandleStick)
							{
								goto IL_259;
							}
							minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue, true);
							minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue2, true);
							minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue3, true);
							minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue4, true);
							maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue, true);
							maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue2, true);
							maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue3, true);
							maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue4, true);
							goto IL_279;
						}
						minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue, true);
						minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue2, true);
						maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue, true);
						maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue2, true);
						goto IL_279;
						IL_259:
						minYValue = ChartSeriesCollection.GetMinYValue(minYValue, chartSeriesItem.YValue, true);
						maxYValue = ChartSeriesCollection.GetMaxYValue(maxYValue, chartSeriesItem.YValue, true);
					}
					IL_279:;
				}
			}
			chartValueLimits.MinXValue = num;
			chartValueLimits.MaxXValue = num2;
			chartValueLimits.MinYValue = minYValue;
			chartValueLimits.MaxYValue = maxYValue;
			return chartValueLimits;
		}

		// Token: 0x0600E81D RID: 59421 RVA: 0x003402B0 File Offset: 0x0033E4B0
		internal void ClearColors()
		{
			foreach (ChartSeries chartSeries in this)
			{
				chartSeries.Appearance.FillStyle.MainColor = Color.Empty;
				chartSeries.Appearance.FillStyle.SecondColor = Color.Empty;
				foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
				{
					chartSeriesItem.Appearance.FillStyle.MainColor = Color.Empty;
					chartSeriesItem.Appearance.FillStyle.SecondColor = Color.Empty;
				}
			}
		}

		// Token: 0x0600E81E RID: 59422 RVA: 0x00340380 File Offset: 0x0033E580
		internal bool IsSeriesEmpty()
		{
			int num = 0;
			foreach (ChartSeries chartSeries in this)
			{
				num += chartSeries.Items.Count;
			}
			return num == 0;
		}

		// Token: 0x0600E81F RID: 59423 RVA: 0x003403D8 File Offset: 0x0033E5D8
		internal int GetSeriesCollectionCount(ChartSeriesType chartSeriesType, int startIndex)
		{
			int num = 0;
			for (int i = startIndex; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeries.Type == chartSeriesType && chartSeries.Items.Count > 0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600E820 RID: 59424 RVA: 0x0034041C File Offset: 0x0033E61C
		internal ChartSeriesCollection GetSeriesCollection(ChartSeriesType chartSeriesType)
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.Parent);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeriesType == chartSeries.Type && chartSeries.Items.Count > 0)
				{
					chartSeriesCollection.Add(chartSeries);
				}
			}
			return chartSeriesCollection;
		}

		// Token: 0x0600E821 RID: 59425 RVA: 0x00340470 File Offset: 0x0033E670
		internal ChartSeriesCollection GetSeriesCollection(ChartSeriesType[] chartSeriesTypes)
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.Parent);
			foreach (ChartSeriesType chartSeriesType in chartSeriesTypes)
			{
				ChartSeriesCollection seriesCollection = this.GetSeriesCollection(chartSeriesType);
				foreach (ChartSeries item in seriesCollection)
				{
					chartSeriesCollection.Add(item);
				}
			}
			return chartSeriesCollection;
		}

		// Token: 0x0600E822 RID: 59426 RVA: 0x003404F0 File Offset: 0x0033E6F0
		internal ChartSeriesCollection GetXUsedSeriesCollection()
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.Parent);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeries.IsXDependentSeriesType && chartSeries.IsXDependent)
				{
					chartSeriesCollection.Add(chartSeries);
				}
			}
			return chartSeriesCollection;
		}

		// Token: 0x0600E823 RID: 59427 RVA: 0x0034053C File Offset: 0x0033E73C
		internal ChartSeriesCollection GetClonedXUsedSeriesCollection()
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.Parent);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i].CloneSeries();
				if (chartSeries.IsXDependentSeriesType)
				{
					chartSeries.PrepareSeriesByXValues();
				}
				chartSeriesCollection.Add(chartSeries);
			}
			return chartSeriesCollection;
		}

		// Token: 0x0600E824 RID: 59428 RVA: 0x0034058C File Offset: 0x0033E78C
		internal ChartSeriesCollection GetYUsedSeriesCollection()
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.Parent);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeries.Type != ChartSeriesType.Pie)
				{
					chartSeriesCollection.Add(chartSeries);
				}
			}
			return chartSeriesCollection;
		}

		// Token: 0x170046A5 RID: 18085
		// (get) Token: 0x0600E825 RID: 59429 RVA: 0x003405D0 File Offset: 0x0033E7D0
		internal bool IsScalable
		{
			get
			{
				bool flag = true;
				foreach (ChartSeries chartSeries in this)
				{
					flag &= chartSeries.IsScalable;
				}
				return flag;
			}
		}

		// Token: 0x170046A6 RID: 18086
		// (get) Token: 0x0600E826 RID: 59430 RVA: 0x00340620 File Offset: 0x0033E820
		internal bool IsUnScalable
		{
			get
			{
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.IsScalable)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0600E827 RID: 59431 RVA: 0x00340670 File Offset: 0x0033E870
		internal void PrepareForScale()
		{
			if (this.IsScalable)
			{
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.IsScalable && !chartSeries.IsXDependent)
					{
						ChartXAxis xaxis = this.Parent.PlotArea.XAxis;
						int count = xaxis.Items.Count;
						int num = 1;
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (xaxis.AutoScale)
							{
								chartSeriesItem.XValue = (double)num++;
							}
							else
							{
								chartSeriesItem.XValue = xaxis.MinValue + xaxis.Step * (double)(num - 1);
								num++;
							}
							chartSeriesItem.haveRealXValue = false;
						}
					}
				}
			}
		}

		// Token: 0x0600E828 RID: 59432 RVA: 0x00340770 File Offset: 0x0033E970
		internal void RestoreAfterScale()
		{
			if (this.IsScalable)
			{
				foreach (ChartSeries chartSeries in this)
				{
					if (chartSeries.IsScalable && chartSeries.IsXDependent)
					{
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (!chartSeriesItem.haveRealXValue)
							{
								chartSeriesItem.XValue = double.NaN;
								chartSeriesItem.haveRealXValue = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600E829 RID: 59433 RVA: 0x0034081C File Offset: 0x0033EA1C
		protected override void OnInsertComplete(int index, object value)
		{
			ChartSeries chartSeries = value as ChartSeries;
			if (chartSeries != null)
			{
				chartSeries.SetParent(this);
				if (this.chartSeriesCollectionParent != null)
				{
					chartSeries.chartSeriesPlotArea = this.chartSeriesCollectionParent.PlotArea;
				}
			}
			base.OnInsertComplete(index, value);
		}

		// Token: 0x0600E82A RID: 59434 RVA: 0x0034085C File Offset: 0x0033EA5C
		public override void Add(ChartSeries chartSeries)
		{
			if (base.List.IndexOf(chartSeries) == -1)
			{
				chartSeries.Appearance.styleChart = (chartSeries.Appearance.PointMark.styleChart = (chartSeries.Appearance.EmptyValue.PointMark.styleChart = (chartSeries.Appearance.LabelAppearance.styleChart = this.Parent)));
				if (chartSeries.Appearance.BarWidthPercent == 0m)
				{
					chartSeries.Appearance.BarWidthPercent = this.Parent.Appearance.BarWidthPercent;
				}
				base.Add(chartSeries);
			}
		}

		// Token: 0x0600E82B RID: 59435 RVA: 0x00340904 File Offset: 0x0033EB04
		public void ClearItems()
		{
			foreach (ChartSeries chartSeries in this)
			{
				chartSeries.Clear();
			}
		}

		// Token: 0x0600E82C RID: 59436 RVA: 0x0034094C File Offset: 0x0033EB4C
		public void RemoveSeries()
		{
			base.Clear();
		}

		// Token: 0x0600E82D RID: 59437 RVA: 0x00340954 File Offset: 0x0033EB54
		public override void Insert(int index, ChartSeries item)
		{
			item.SetParent(this);
			base.Insert(index, item);
		}

		// Token: 0x0600E82E RID: 59438 RVA: 0x00340965 File Offset: 0x0033EB65
		public void InsertSeries(int index, ChartSeries item)
		{
			this.Insert(index, item);
		}

		// Token: 0x0600E82F RID: 59439 RVA: 0x00340970 File Offset: 0x0033EB70
		public ChartSeries GetByName(string name)
		{
			foreach (ChartSeries chartSeries in base.List)
			{
				if (string.Compare(chartSeries.Name, name, true) == 0)
				{
					return chartSeries;
				}
			}
			return null;
		}

		// Token: 0x0600E830 RID: 59440 RVA: 0x003409CC File Offset: 0x0033EBCC
		public ChartSeries GetSeries(int index)
		{
			return base.List[index];
		}

		// Token: 0x0600E831 RID: 59441 RVA: 0x003409DC File Offset: 0x0033EBDC
		public int GetMaxItemsCount()
		{
			int num = 0;
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				int count = chartSeries.Items.Count;
				if (count > num)
				{
					num = count;
				}
			}
			return num;
		}

		// Token: 0x0600E832 RID: 59442 RVA: 0x00340A18 File Offset: 0x0033EC18
		public void ClearDataBoundState()
		{
			foreach (ChartSeries chartSeries in this)
			{
				chartSeries.ClearDataBoundState();
			}
		}

		// Token: 0x0600E833 RID: 59443 RVA: 0x00340A60 File Offset: 0x0033EC60
		public ChartSeriesCollection GetFilteredSeriesByYAxis(ChartYAxisType yAxisType)
		{
			ChartSeriesCollection chartSeriesCollection = new ChartSeriesCollection(this.chartSeriesCollectionParent);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (chartSeries.Type == ChartSeriesType.Pie)
				{
					if (yAxisType != ChartYAxisType.Secondary)
					{
						chartSeriesCollection.Add(chartSeries);
					}
				}
				else if (chartSeries.YAxisType == yAxisType)
				{
					chartSeriesCollection.Add(chartSeries);
				}
			}
			return chartSeriesCollection;
		}

		// Token: 0x0600E834 RID: 59444 RVA: 0x00340ABC File Offset: 0x0033ECBC
		public double GetMinYValue()
		{
			double num = double.PositiveInfinity;
			double[] array = new double[]
			{
				this.GetMinStackedValue(ChartSeriesType.StackedBar),
				this.GetMinStacked100Value(ChartSeriesType.StackedBar100),
				this.GetMinStackedValue(ChartSeriesType.StackedArea),
				this.GetMinStacked100Value(ChartSeriesType.StackedArea100),
				this.GetMinStackedValue(ChartSeriesType.StackedSplineArea),
				this.GetMinStacked100Value(ChartSeriesType.StackedSplineArea100),
				this.GetMinStackedValue(ChartSeriesType.StackedLine),
				this.GetMinStackedValue(ChartSeriesType.StackedSpline)
			};
			Array.Sort<double>(array);
			for (int i = 0; i < base.Count; i++)
			{
				ChartSeries chartSeries = this[i];
				if (!chartSeries.IsStacked)
				{
					ChartSeriesType type = chartSeries.Type;
					int count = chartSeries.Items.Count;
					for (int j = 0; j < count; j++)
					{
						ChartSeriesItem chartSeriesItem = chartSeries.Items[j];
						if (!chartSeriesItem.Empty)
						{
							ChartSeriesType chartSeriesType = type;
							if (chartSeriesType != ChartSeriesType.Gantt && chartSeriesType != ChartSeriesType.Bubble)
							{
								if (chartSeriesType != ChartSeriesType.CandleStick)
								{
									if (chartSeriesItem.YValue < num)
									{
										num = chartSeriesItem.YValue;
									}
								}
								else
								{
									if (chartSeriesItem.YValue < num)
									{
										num = chartSeriesItem.YValue;
									}
									if (chartSeriesItem.YValue2 < num)
									{
										num = chartSeriesItem.YValue2;
									}
									if (chartSeriesItem.YValue3 < num)
									{
										num = chartSeriesItem.YValue3;
									}
									if (chartSeriesItem.YValue4 < num)
									{
										num = chartSeriesItem.YValue4;
									}
								}
							}
							else
							{
								if (chartSeriesItem.YValue < num)
								{
									num = chartSeriesItem.YValue;
								}
								if (chartSeriesItem.YValue2 < num)
								{
									num = chartSeriesItem.YValue2;
								}
							}
						}
					}
				}
			}
			if (array[0] >= num)
			{
				return num;
			}
			return array[0];
		}

		// Token: 0x0600E835 RID: 59445 RVA: 0x00340C48 File Offset: 0x0033EE48
		public double GetMaxYValue()
		{
			double num = double.NegativeInfinity;
			double[] array = new double[]
			{
				this.GetMaxStackedValue(ChartSeriesType.StackedArea),
				this.GetMaxStacked100Value(),
				this.GetMaxStackedValue(ChartSeriesType.StackedBar),
				this.GetMaxStacked100Value(),
				this.GetMaxStackedValue(ChartSeriesType.StackedSplineArea),
				this.GetMaxStacked100Value(),
				this.GetMaxStackedValue(ChartSeriesType.StackedLine),
				this.GetMaxStacked100Value(),
				this.GetMaxStackedValue(ChartSeriesType.StackedSpline)
			};
			Array.Sort<double>(array);
			foreach (ChartSeries chartSeries in this)
			{
				if (!ChartSeriesCollection.Stacked(chartSeries))
				{
					ChartSeriesType type = chartSeries.Type;
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						if (!chartSeriesItem.Empty)
						{
							ChartSeriesType chartSeriesType = type;
							if (chartSeriesType != ChartSeriesType.Gantt && chartSeriesType != ChartSeriesType.Bubble)
							{
								if (chartSeriesType != ChartSeriesType.CandleStick)
								{
									if (chartSeriesItem.YValue > num)
									{
										num = chartSeriesItem.YValue;
									}
								}
								else
								{
									if (chartSeriesItem.YValue > num)
									{
										num = chartSeriesItem.YValue;
									}
									if (chartSeriesItem.YValue2 > num)
									{
										num = chartSeriesItem.YValue2;
									}
									if (chartSeriesItem.YValue3 > num)
									{
										num = chartSeriesItem.YValue3;
									}
									if (chartSeriesItem.YValue4 > num)
									{
										num = chartSeriesItem.YValue4;
									}
								}
							}
							else
							{
								if (chartSeriesItem.YValue > num)
								{
									num = chartSeriesItem.YValue;
								}
								if (chartSeriesItem.YValue2 > num)
								{
									num = chartSeriesItem.YValue2;
								}
							}
						}
					}
				}
			}
			if (array[8] <= num)
			{
				return num;
			}
			return array[8];
		}

		// Token: 0x0600E836 RID: 59446 RVA: 0x00340E20 File Offset: 0x0033F020
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				Pair pair = state as Pair;
				if (pair != null)
				{
					int num = (int)pair.First;
					object[] array = (object[])pair.Second;
					base.Clear();
					foreach (object state2 in array)
					{
						ChartSeries chartSeries = new ChartSeries(this);
						((IChartingStateManager)chartSeries).TrackViewState();
						((IChartingStateManager)chartSeries).LoadViewState(state2);
						this.Add(chartSeries);
					}
				}
			}
		}

		// Token: 0x0400428C RID: 17036
		private Chart chartSeriesCollectionParent;
	}
}
