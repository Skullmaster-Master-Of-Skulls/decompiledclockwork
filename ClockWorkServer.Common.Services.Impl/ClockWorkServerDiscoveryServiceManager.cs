using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Core.ClockWorkServerConnection;
using TechnoPro.Common.Core.Mappers.ClockWorkServer;
using TechnoPro.Common.Core.Mappers.ClockWorkServerConnection;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002D RID: 45
	public class ClockWorkServerDiscoveryServiceManager : IClockWorkServerDiscovery, IService
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000093B4 File Offset: 0x000075B4
		public GetClockWorkServerConnectionInfoResp GetClockWorkServerConnectionInfo(GetClockWorkServerConnectionInfoReq request)
		{
			IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			ClockWorkServerConnectionInfo clockWorkServerConnectionInfo = clockWorkServerConnectionInfoManager.GetClockWorkServerConnectionInfo();
			ClockWorkServerPreferredConnectionInfo clockWorkServerPreferredConnectionInfo = new ClockWorkServerPreferredConnectionInfo
			{
				VirtualDirectory = clockWorkServerConnectionInfo.VirtualDirectory,
				Certificate = clockWorkServerConnectionInfo.Certificate,
				ExternalHostname = clockWorkServerConnectionInfo.HttpHostname,
				ExternalPort = clockWorkServerConnectionInfo.HttpPort,
				Hostname = clockWorkServerConnectionInfo.TcpHostname,
				Port = clockWorkServerConnectionInfo.TcpPort,
				IISVersion = clockWorkServerConnectionInfo.IISVersion,
				IdentityDNS = clockWorkServerConnectionInfo.IdentityDNS,
				BindingType = clockWorkServerConnectionInfoManager.GetClockWorkServerPreferedBindingType()
			};
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Insert("cClockWorkServerPreferredConnectionInfo", clockWorkServerPreferredConnectionInfo);
			ICacheStorageManager cacheStorageManager2 = cacheStorageManager;
			object key = "ServerCertificateString";
			CertificateInfo certificate = clockWorkServerConnectionInfo.Certificate;
			cacheStorageManager2.Insert(key, (certificate != null) ? certificate.CertificatePublicKey : null);
			return new GetClockWorkServerConnectionInfoResp
			{
				ServerConnectionInfo = clockWorkServerPreferredConnectionInfo.ToDTO()
			};
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000949C File Offset: 0x0000769C
		public GetClockWorkServerInfoResp GetClockWorkServerInfo(GetClockWorkServerInfoReq request)
		{
			IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new GetClockWorkServerInfoResp
			{
				ServerInfo = clockWorkServerConnectionInfoManager.GetClockWorkServerInfo().ToDTO()
			};
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000094D4 File Offset: 0x000076D4
		[DebuggerStepThrough]
		public Task<GetClockWorkServerInfoResp> GetClockWorkServerInfoAsync(GetClockWorkServerInfoReq request)
		{
			ClockWorkServerDiscoveryServiceManager.<GetClockWorkServerInfoAsync>d__2 <GetClockWorkServerInfoAsync>d__ = new ClockWorkServerDiscoveryServiceManager.<GetClockWorkServerInfoAsync>d__2();
			<GetClockWorkServerInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetClockWorkServerInfoResp>.Create();
			<GetClockWorkServerInfoAsync>d__.<>4__this = this;
			<GetClockWorkServerInfoAsync>d__.request = request;
			<GetClockWorkServerInfoAsync>d__.<>1__state = -1;
			<GetClockWorkServerInfoAsync>d__.<>t__builder.Start<ClockWorkServerDiscoveryServiceManager.<GetClockWorkServerInfoAsync>d__2>(ref <GetClockWorkServerInfoAsync>d__);
			return <GetClockWorkServerInfoAsync>d__.<>t__builder.Task;
		}
	}
}
