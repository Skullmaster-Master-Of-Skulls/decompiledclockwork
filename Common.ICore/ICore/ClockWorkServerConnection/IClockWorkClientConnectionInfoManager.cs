using System;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ICore.ClockWorkServerConnection
{
	// Token: 0x020000B3 RID: 179
	public interface IClockWorkClientConnectionInfoManager
	{
		// Token: 0x06000563 RID: 1379
		ClockWorkClientConnectionInfo GetConnectionInfoFromStorageString(string StorageString);

		// Token: 0x06000564 RID: 1380
		string GetStorageStringFromConnectionInfo(ClockWorkClientConnectionInfo ConnectionInfo);
	}
}
