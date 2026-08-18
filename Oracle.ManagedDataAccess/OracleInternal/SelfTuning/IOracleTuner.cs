using System;
using System.Collections.Generic;

namespace OracleInternal.SelfTuning
{
	// Token: 0x02000196 RID: 406
	internal interface IOracleTuner
	{
		// Token: 0x06000F5E RID: 3934
		bool Register(IOracleTunable tunable);

		// Token: 0x06000F5F RID: 3935
		bool Unregister(IOracleTunable tunable);

		// Token: 0x06000F60 RID: 3936
		bool SubmitData(IOracleTunable tunable, int scs, int connCount, Dictionary<string, int> sample);

		// Token: 0x06000F61 RID: 3937
		void setThreshold(IOracleTunable tunable, int val);

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F62 RID: 3938
		bool HighMemoryUsageAlert { get; }
	}
}
