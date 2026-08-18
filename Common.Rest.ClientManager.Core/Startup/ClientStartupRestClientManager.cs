using System;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Startup
{
	// Token: 0x02000013 RID: 19
	public class ClientStartupRestClientManager : AnonymousRestProxy<IClientStartupClientManager>, IClientStartupClientManager, IWebService
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003839 File Offset: 0x00001A39
		public ClientStartupRestClientManager(string serviceAddress, string clientId) : base(serviceAddress, clientId)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003843 File Offset: 0x00001A43
		public ClientStartupRestClientManager(string serviceAddress, string serviceAddressSuffix, string clientId) : base(serviceAddress, serviceAddressSuffix, clientId)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000384E File Offset: 0x00001A4E
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest request)
		{
			return base.Get<UpdateRequiredResponse>(string.Format("clientstartup/isupdaterequired/filetype/{0}/addrsize/{1}/clientversion/{2}", request.FileType, (int)request.AddressSize, request.ClientVersion), true);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003878 File Offset: 0x00001A78
		public CertificateInfo GetClockWorkServerCertificate()
		{
			return base.Get<CertificateInfo>("clientstartup/servercertificate", true);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003888 File Offset: 0x00001A88
		public bool CheckConnectivityToServer()
		{
			bool result;
			try
			{
				CWLogger.Logger.Trace("Starting to check connectivity to server (updater.CheckConnectivity())...");
				int num = base.Get<int>("clientstartup/checkconnectivity", true);
				CWLogger.Logger.Trace("Finished checking connectivity to server; result=" + ((num == 1) ? "Success" : "Failed"));
				result = (num == 1);
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
