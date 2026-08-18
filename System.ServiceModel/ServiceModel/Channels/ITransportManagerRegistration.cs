using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000785 RID: 1925
	internal interface ITransportManagerRegistration
	{
		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x0600498A RID: 18826
		HostNameComparisonMode HostNameComparisonMode { get; }

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x0600498B RID: 18827
		Uri ListenUri { get; }

		// Token: 0x0600498C RID: 18828
		IList<TransportManager> Select(TransportChannelListener factory);
	}
}
