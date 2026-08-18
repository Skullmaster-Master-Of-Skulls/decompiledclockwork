using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A7 RID: 167
	[ServiceContract(Name = "VetsChapterService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IVetsChapter : IService
	{
		// Token: 0x060004E2 RID: 1250
		[OperationContract(Name = "GetChaptersAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<GetChaptersResp> GetChaptersAsync(GetChaptersReq Request);

		// Token: 0x060004E3 RID: 1251
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetChaptersResp GetChapters(GetChaptersReq Request);
	}
}
