using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServer;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection
{
	// Token: 0x0200006F RID: 111
	public interface IClockWorkServerConnectionsClientManager : IWebService
	{
		// Token: 0x0600034A RID: 842
		ClockWorkServerPreferredConnectionInfoDTO GetClockWorkServerConnectionInfo(Uri uri);

		// Token: 0x0600034B RID: 843
		ClockWorkServerInfoDTO GetClockWorkServerInfo(Uri uri);

		// Token: 0x0600034C RID: 844
		Task<ClockWorkServerInfoDTO> GetClockWorkServerInfoAsync(Uri uri);

		// Token: 0x0600034D RID: 845
		ClockWorkServerPreferredConnectionInfoDTO GetClockWorkPreferedConnection(string appStartupPath);
	}
}
