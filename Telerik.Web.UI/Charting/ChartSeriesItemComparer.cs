using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x02001742 RID: 5954
	internal class ChartSeriesItemComparer : IComparer
	{
		// Token: 0x0600E885 RID: 59525 RVA: 0x00342F48 File Offset: 0x00341148
		int IComparer.Compare(object x, object y)
		{
			return (int)Math.Ceiling(((ChartSeriesItem)y).YValue - ((ChartSeriesItem)x).YValue);
		}
	}
}
