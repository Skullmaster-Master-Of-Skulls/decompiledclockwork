using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000034 RID: 52
	[ServiceContract(Namespace = "http://tpro.ca")]
	[DiscoverService]
	[NoSslCertificate]
	[AllowAnonymous]
	public interface IClockWorkServerDiscovery : IService
	{
		// Token: 0x060001A5 RID: 421
		[OperationContract]
		GetClockWorkServerConnectionInfoResp GetClockWorkServerConnectionInfo(GetClockWorkServerConnectionInfoReq request);

		// Token: 0x060001A6 RID: 422
		[OperationContract]
		GetClockWorkServerInfoResp GetClockWorkServerInfo(GetClockWorkServerInfoReq request);

		// Token: 0x060001A7 RID: 423
		[OperationContract(Name = "GetClockWorkServerInfoAsync")]
		Task<GetClockWorkServerInfoResp> GetClockWorkServerInfoAsync(GetClockWorkServerInfoReq request);
	}
}
