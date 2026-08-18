using System;

namespace Telerik.Licensing
{
	// Token: 0x0200041B RID: 1051
	internal interface IUsageTracker
	{
		// Token: 0x060025ED RID: 9709
		void Track(RequestPayload data);

		// Token: 0x060025EE RID: 9710
		void StartTracking();

		// Token: 0x060025EF RID: 9711
		bool IsTracking();

		// Token: 0x060025F0 RID: 9712
		void StopTracking();
	}
}
