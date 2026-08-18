using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000779 RID: 1913
	internal interface IConnectionOrientedConnectionSettings
	{
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004913 RID: 18707
		int ConnectionBufferSize { get; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06004914 RID: 18708
		TimeSpan MaxOutputDelay { get; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004915 RID: 18709
		TimeSpan IdleTimeout { get; }
	}
}
