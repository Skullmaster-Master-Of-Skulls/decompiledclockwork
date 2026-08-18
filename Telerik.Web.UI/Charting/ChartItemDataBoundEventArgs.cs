using System;

namespace Telerik.Charting
{
	// Token: 0x020016F2 RID: 5874
	public class ChartItemDataBoundEventArgs : EventArgs
	{
		// Token: 0x170045A5 RID: 17829
		// (get) Token: 0x0600E42B RID: 58411 RVA: 0x0032AA09 File Offset: 0x00328C09
		// (set) Token: 0x0600E42C RID: 58412 RVA: 0x0032AA11 File Offset: 0x00328C11
		public object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x0600E42D RID: 58413 RVA: 0x0032AA1A File Offset: 0x00328C1A
		public ChartItemDataBoundEventArgs(ChartSeriesItem seriesItem, ChartSeries chartSeries, object dataItem)
		{
			this.seriesItem = seriesItem;
			this.chartSeries = chartSeries;
			this.dataItem = dataItem;
		}

		// Token: 0x170045A6 RID: 17830
		// (get) Token: 0x0600E42E RID: 58414 RVA: 0x0032AA37 File Offset: 0x00328C37
		// (set) Token: 0x0600E42F RID: 58415 RVA: 0x0032AA3F File Offset: 0x00328C3F
		public ChartSeries ChartSeries
		{
			get
			{
				return this.chartSeries;
			}
			set
			{
				this.chartSeries = value;
			}
		}

		// Token: 0x170045A7 RID: 17831
		// (get) Token: 0x0600E430 RID: 58416 RVA: 0x0032AA48 File Offset: 0x00328C48
		// (set) Token: 0x0600E431 RID: 58417 RVA: 0x0032AA50 File Offset: 0x00328C50
		public ChartSeriesItem SeriesItem
		{
			get
			{
				return this.seriesItem;
			}
			set
			{
				this.seriesItem = value;
			}
		}

		// Token: 0x040041E0 RID: 16864
		private ChartSeriesItem seriesItem;

		// Token: 0x040041E1 RID: 16865
		private ChartSeries chartSeries;

		// Token: 0x040041E2 RID: 16866
		private object dataItem;
	}
}
