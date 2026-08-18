using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServer;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ICore.ClockWorkServerConnection
{
	// Token: 0x020000B4 RID: 180
	public interface IClockWorkServerConnectionInfoManager : IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x06000565 RID: 1381
		ClockWorkServerConnectionInfo GetClockWorkServerConnectionInfo();

		// Token: 0x06000566 RID: 1382
		void SaveClockWorkServerConnectionInfo(ClockWorkServerConnectionInfo clockWorkServerConnectionInfo);

		// Token: 0x06000567 RID: 1383
		ClockWorkServerInfo GetClockWorkServerInfo();

		// Token: 0x06000568 RID: 1384
		Task<ClockWorkServerInfo> GetClockWorkServerInfoAsync();

		// Token: 0x06000569 RID: 1385
		eBindingType GetClockWorkServerPreferedBindingType();
	}
}
