using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection
{
	// Token: 0x0200006E RID: 110
	public interface IClockWorkClientConnectionInfoClientManager : IWebService
	{
		// Token: 0x06000348 RID: 840
		ClockWorkClientConnectionInfo GetConnectionInfoFromStorageString(string StorageString);

		// Token: 0x06000349 RID: 841
		string GetStorageStringFromConnectionInfo(ClockWorkClientConnectionInfo ConnectionInfo);
	}
}
