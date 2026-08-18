using System;

namespace System.Security.Policy
{
	// Token: 0x020004A6 RID: 1190
	internal interface IDelayEvaluatedEvidence
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002F36 RID: 12086
		bool IsVerified { get; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002F37 RID: 12087
		bool WasUsed { get; }

		// Token: 0x06002F38 RID: 12088
		void MarkUsed();
	}
}
