using System;
using Telerik.Web.UI.Common;

namespace Telerik.Charting
{
	// Token: 0x02001740 RID: 5952
	internal class ChartDesignTimeSeriesItem : ChartSeriesItem
	{
		// Token: 0x0600E86D RID: 59501 RVA: 0x00342596 File Offset: 0x00340796
		public ChartDesignTimeSeriesItem(ChartSeries series) : base(series)
		{
			this.Init();
		}

		// Token: 0x0600E86E RID: 59502 RVA: 0x003425A5 File Offset: 0x003407A5
		public ChartDesignTimeSeriesItem(string itemName, ChartSeries series) : this(series)
		{
			base.Name = itemName;
		}

		// Token: 0x0600E86F RID: 59503 RVA: 0x003425B8 File Offset: 0x003407B8
		private void Init()
		{
			this.chartDesignTimeSeriesItemTempXValue = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt(-100, 100));
			this.chartDesignTimeSeriesItemTempXValue2 = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt(-100, 100));
			this.chartDesignTimeSeriesItemTempYValue = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt(-100, 100));
			this.chartDesignTimeSeriesItemTempYValue2 = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt(-100, 100));
			double num = Math.Max(this.chartDesignTimeSeriesItemTempYValue, this.chartDesignTimeSeriesItemTempYValue2);
			double num2 = Math.Min(this.chartDesignTimeSeriesItemTempYValue, this.chartDesignTimeSeriesItemTempYValue2);
			this.chartDesignTimeSeriesItemTempYValue3 = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt((int)num, (int)(num * 1.4)));
			this.chartDesignTimeSeriesItemTempYValue4 = (double)Math.Abs(ChartDesignTimeSeriesItem.chartDesignTimeSeriesItemRandom.GetInt((int)(num2 * 0.6), (int)num2));
		}

		// Token: 0x0600E870 RID: 59504 RVA: 0x0034269C File Offset: 0x0034089C
		private void ClearValues()
		{
			base.XValue = double.NaN;
			base.XValue2 = double.NaN;
			base.YValue = double.NaN;
			base.YValue2 = double.NaN;
			base.YValue3 = double.NaN;
			base.YValue4 = double.NaN;
		}

		// Token: 0x0600E871 RID: 59505 RVA: 0x00342704 File Offset: 0x00340904
		public void SetCorrectValues()
		{
			if (base.Parent != null)
			{
				this.ClearValues();
				switch (base.Parent.Type)
				{
				default:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					return;
				case ChartSeriesType.StackedBar:
				case ChartSeriesType.StackedBar100:
				case ChartSeriesType.Line:
				case ChartSeriesType.StackedArea:
				case ChartSeriesType.StackedArea100:
				case ChartSeriesType.Pie:
				case ChartSeriesType.Spline:
				case ChartSeriesType.Point:
				case ChartSeriesType.SplineArea:
				case ChartSeriesType.StackedSplineArea:
				case ChartSeriesType.StackedSplineArea100:
				case ChartSeriesType.StackedLine:
				case ChartSeriesType.StackedSpline:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					break;
				case ChartSeriesType.Area:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					return;
				case ChartSeriesType.Gantt:
					base.XValue = this.chartDesignTimeSeriesItemTempXValue;
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					base.YValue2 = this.chartDesignTimeSeriesItemTempYValue2;
					return;
				case ChartSeriesType.Bezier:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					return;
				case ChartSeriesType.Bubble:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					base.YValue2 = this.chartDesignTimeSeriesItemTempYValue2;
					return;
				case ChartSeriesType.CandleStick:
					base.YValue = this.chartDesignTimeSeriesItemTempYValue;
					base.YValue2 = this.chartDesignTimeSeriesItemTempYValue2;
					base.YValue3 = this.chartDesignTimeSeriesItemTempYValue3;
					base.YValue4 = this.chartDesignTimeSeriesItemTempYValue4;
					return;
				}
			}
		}

		// Token: 0x040042A0 RID: 17056
		private double chartDesignTimeSeriesItemTempXValue;

		// Token: 0x040042A1 RID: 17057
		private double chartDesignTimeSeriesItemTempXValue2;

		// Token: 0x040042A2 RID: 17058
		private double chartDesignTimeSeriesItemTempYValue;

		// Token: 0x040042A3 RID: 17059
		private double chartDesignTimeSeriesItemTempYValue2;

		// Token: 0x040042A4 RID: 17060
		private double chartDesignTimeSeriesItemTempYValue3;

		// Token: 0x040042A5 RID: 17061
		private double chartDesignTimeSeriesItemTempYValue4;

		// Token: 0x040042A6 RID: 17062
		private static readonly TelerikRandom chartDesignTimeSeriesItemRandom = new TelerikRandom();
	}
}
