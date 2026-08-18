using System;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Startup
{
	// Token: 0x02000019 RID: 25
	public class ClientStartupClientManager : IClientStartupClientManager, IWebService
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004F9C File Offset: 0x0000319C
		public bool CheckConnectivityToServer()
		{
			bool result;
			try
			{
				CWLogger.Logger.Trace("Starting to check connectivity to server (updater.CheckConnectivity())...");
				IClientStartup clientInstance = ClientServiceFactory.GetClientInstance<IClientStartup>(true, true);
				CWLogger.Logger.Trace("Received updater object: " + ((clientInstance == null) ? "NULL" : "Not Null"));
				bool flag = clientInstance != null && clientInstance.CheckConnectivity() > 0;
				CWLogger.Logger.Trace("Finished checking connectivity to server; result=" + flag.ToString());
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005030 File Offset: 0x00003230
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest request)
		{
			IClientStartup clientInstance = ClientServiceFactory.GetClientInstance<IClientStartup>(true, true);
			UpdateRequiredResponse result;
			if (clientInstance == null)
			{
				(result = new UpdateRequiredResponse()).IsUpdateRequired = false;
			}
			else
			{
				result = clientInstance.IsUpdateRequired(request);
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005064 File Offset: 0x00003264
		public CertificateInfo GetClockWorkServerCertificate()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			CertificateInfo serverCertificateInfo = clientCache.ServerCertificateInfo;
			bool flag = serverCertificateInfo == null;
			CertificateInfo result;
			if (flag)
			{
				IClientStartup clientInstance = ClientServiceFactory.GetClientInstance<IClientStartup>(true, true);
				bool flag2 = clientInstance == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					GetClockWorkServerCertificateReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerCertificateReq>();
					GetClockWorkServerCertificateResp clockWorkServerCertificate = clientInstance.GetClockWorkServerCertificate(request);
					ClientCache clientCache2 = clientCache;
					CertificateInfo certificateInfo = new CertificateInfo();
					certificateInfo.CertificatePublicKey = (clientCache.ServerCertificateString = clockWorkServerCertificate.CertificatePublicKey);
					certificateInfo.IdentityDNS = clockWorkServerCertificate.IdentityDNS;
					certificateInfo.Thumbprint = clockWorkServerCertificate.Thumbprint;
					CertificateInfo certificateInfo2 = certificateInfo;
					clientCache2.ServerCertificateInfo = certificateInfo;
					result = certificateInfo2;
				}
			}
			else
			{
				result = serverCertificateInfo;
			}
			return result;
		}
	}
}
