using System;

namespace Telerik.Charting
{
	// Token: 0x0200175C RID: 5980
	public class ChartClickEventArgs : EventArgs
	{
		// Token: 0x170046E3 RID: 18147
		// (get) Token: 0x0600E923 RID: 59683 RVA: 0x00345B6C File Offset: 0x00343D6C
		// (set) Token: 0x0600E924 RID: 59684 RVA: 0x00345B74 File Offset: 0x00343D74
		public IActiveRegion Element
		{
			get
			{
				return this.activeRegion;
			}
			set
			{
				this.activeRegion = value;
			}
		}

		// Token: 0x170046E4 RID: 18148
		// (get) Token: 0x0600E925 RID: 59685 RVA: 0x00345B7D File Offset: 0x00343D7D
		// (set) Token: 0x0600E926 RID: 59686 RVA: 0x00345B85 File Offset: 0x00343D85
		public ChartSeries Series
		{
			get
			{
				return this.series;
			}
			set
			{
				this.series = value;
			}
		}

		// Token: 0x170046E5 RID: 18149
		// (get) Token: 0x0600E927 RID: 59687 RVA: 0x00345B8E File Offset: 0x00343D8E
		// (set) Token: 0x0600E928 RID: 59688 RVA: 0x00345B96 File Offset: 0x00343D96
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

		// Token: 0x0600E929 RID: 59689 RVA: 0x00345B9F File Offset: 0x00343D9F
		public ChartClickEventArgs(IActiveRegion element, ChartSeries series, ChartSeriesItem seriesItem)
		{
			this.activeRegion = element;
			this.SeriesItem = seriesItem;
			this.Series = series;
		}

		// Token: 0x0600E92A RID: 59690 RVA: 0x00345BBC File Offset: 0x00343DBC
		public ChartClickEventArgs(IActiveRegion element, ChartSeries series) : this(element, series, null)
		{
		}

		// Token: 0x0600E92B RID: 59691 RVA: 0x00345BC7 File Offset: 0x00343DC7
		public ChartClickEventArgs(IActiveRegion element) : this(element, null, null)
		{
		}

		// Token: 0x04004308 RID: 17160
		private IActiveRegion activeRegion;

		// Token: 0x04004309 RID: 17161
		private ChartSeries series;

		// Token: 0x0400430A RID: 17162
		private ChartSeriesItem seriesItem;
	}
}
