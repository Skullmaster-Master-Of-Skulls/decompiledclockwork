using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ClientManager.ICore.Startup
{
	// Token: 0x02000016 RID: 22
	public interface IClientStartupClientManager : IWebService
	{
		// Token: 0x0600008A RID: 138
		UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest Request);

		// Token: 0x0600008B RID: 139
		CertificateInfo GetClockWorkServerCertificate();

		// Token: 0x0600008C RID: 140
		bool CheckConnectivityToServer();
	}
}
