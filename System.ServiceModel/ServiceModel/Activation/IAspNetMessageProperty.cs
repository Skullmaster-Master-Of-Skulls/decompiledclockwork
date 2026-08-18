using System;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005BE RID: 1470
	internal interface IAspNetMessageProperty
	{
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003975 RID: 14709
		Uri OriginalRequestUri { get; }

		// Token: 0x06003976 RID: 14710
		IDisposable ApplyIntegrationContext();

		// Token: 0x06003977 RID: 14711
		IDisposable Impersonate();

		// Token: 0x06003978 RID: 14712
		void Close();
	}
}
