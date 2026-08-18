using System;

namespace Telerik.Charting
{
	// Token: 0x0200173E RID: 5950
	internal class ChartValueLimits
	{
		// Token: 0x0600E837 RID: 59447 RVA: 0x00340E8F File Offset: 0x0033F08F
		public ChartValueLimits(double minXValue, double maxXValue, double minYValue, double maxYValue)
		{
			this.MinXValue = minXValue;
			this.MaxXValue = maxXValue;
			this.MinYValue = minYValue;
			this.MaxYValue = maxYValue;
		}

		// Token: 0x04004294 RID: 17044
		public double MinXValue;

		// Token: 0x04004295 RID: 17045
		public double MaxXValue;

		// Token: 0x04004296 RID: 17046
		public double MinYValue;

		// Token: 0x04004297 RID: 17047
		public double MaxYValue;
	}
}
