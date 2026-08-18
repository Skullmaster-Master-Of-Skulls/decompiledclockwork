using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.ClockWorkServer;

namespace TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection
{
	// Token: 0x02000070 RID: 112
	public interface IClockWorkServerDiscoveryClientManager
	{
		// Token: 0x0600034E RID: 846
		IList<Uri> GetAvailableClockWorkServerList(Uri discoveryScopeUri = null, int discoveryDurationInSeconds = 5);

		// Token: 0x0600034F RID: 847
		object FindAvailableClockWorkServerListAsync(Uri discoveryScopeUri = null, int discoveryDurationInSeconds = 5);

		// Token: 0x06000350 RID: 848
		void CancelFindAvailableClockWorkServerListAsync(object userState);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000351 RID: 849
		// (remove) Token: 0x06000352 RID: 850
		event EventHandler<ServiceDiscoveryInfo> FindProgressChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000353 RID: 851
		// (remove) Token: 0x06000354 RID: 852
		event EventHandler<IList<ServiceDiscoveryInfo>> FindCompleted;
	}
}
