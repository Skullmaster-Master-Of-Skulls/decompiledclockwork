using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200013A RID: 314
	internal class RLBMetricsComparer : IComparer
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x00082E14 File Offset: 0x00081E14
		public int Compare(object x, object y)
		{
			RLBMetrics rlbmetrics = (RLBMetrics)x;
			RLBMetrics rlbmetrics2 = (RLBMetrics)y;
			if (rlbmetrics.MaxDistribFreq < rlbmetrics2.MaxDistribFreq)
			{
				return 1;
			}
			if (rlbmetrics.MaxDistribFreq == rlbmetrics2.MaxDistribFreq)
			{
				return 0;
			}
			return -1;
		}
	}
}
