using System;

namespace System.Web.Util
{
	// Token: 0x020001D2 RID: 466
	internal interface IPerfCounters
	{
		// Token: 0x0600177D RID: 6013
		void IncrementCounter(AppPerfCounter counter);

		// Token: 0x0600177E RID: 6014
		void IncrementCounter(AppPerfCounter counter, int value);

		// Token: 0x0600177F RID: 6015
		void DecrementCounter(AppPerfCounter counter);

		// Token: 0x06001780 RID: 6016
		void SetCounter(AppPerfCounter counter, int value);
	}
}
