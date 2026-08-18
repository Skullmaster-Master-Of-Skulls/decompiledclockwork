using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000749 RID: 1865
	public interface IReceiveContextSettings
	{
		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06004757 RID: 18263
		// (set) Token: 0x06004758 RID: 18264
		bool Enabled { get; set; }

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06004759 RID: 18265
		TimeSpan ValidityDuration { get; }
	}
}
