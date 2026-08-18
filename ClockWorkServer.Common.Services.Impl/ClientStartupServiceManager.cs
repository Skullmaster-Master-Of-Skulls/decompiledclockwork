using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Core.Startup;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008A RID: 138
	public class ClientStartupServiceManager : IClientStartup, IService, IConnectivity
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00017A0C File Offset: 0x00015C0C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017A1F File Offset: 0x00015C1F
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest updateRequiredReq)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017A28 File Offset: 0x00015C28
		public GetClockWorkServerCertificateResp GetClockWorkServerCertificate(GetClockWorkServerCertificateReq request)
		{
			IClientStartupManager clientStartupManager = new ClientStartupManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			CertificateInfo clockWorkServerCertificate = clientStartupManager.GetClockWorkServerCertificate();
			return new GetClockWorkServerCertificateResp
			{
				CertificatePublicKey = ((clockWorkServerCertificate != null) ? clockWorkServerCertificate.CertificatePublicKey : string.Empty),
				IdentityDNS = ((clockWorkServerCertificate != null) ? clockWorkServerCertificate.IdentityDNS : string.Empty),
				Thumbprint = ((clockWorkServerCertificate != null) ? clockWorkServerCertificate.Thumbprint : string.Empty)
			};
		}
	}
}
