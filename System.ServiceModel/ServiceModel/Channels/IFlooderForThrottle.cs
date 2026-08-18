using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FA RID: 2554
	internal interface IFlooderForThrottle
	{
		// Token: 0x0600654C RID: 25932
		void OnThrottleReached();

		// Token: 0x0600654D RID: 25933
		void OnThrottleReleased();
	}
}
