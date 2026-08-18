using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.ICore.ClockWorkServer
{
	// Token: 0x020000D6 RID: 214
	public interface IServerInstanceInfoManager
	{
		// Token: 0x060006A4 RID: 1700
		IList<ServerInstanceInfo> GetServerInstancesInfo();

		// Token: 0x060006A5 RID: 1701
		bool IsRunningFromClockWorkServerComputer();

		// Token: 0x060006A6 RID: 1702
		ServerInstanceInfo GetServerInstanceInfoByName(string serverVirtualDir);

		// Token: 0x060006A7 RID: 1703
		ServerInstanceInfo GetServerInstanceInfoByInstanceName(eClockWorkServerInstanceName clockWorkServerInstanceName);
	}
}
