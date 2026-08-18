using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000006 RID: 6
	[ServiceContract(Name = "BookSearchService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IBookSearch : IService
	{
		// Token: 0x06000004 RID: 4
		[OperationContract(Name = "SearchForVolumesAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SearchForVolumesResp> SearchForVolumesAsync(SearchForVolumesReq request);

		// Token: 0x06000005 RID: 5
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SearchForVolumesResp SearchForVolumes(SearchForVolumesReq request);

		// Token: 0x06000006 RID: 6
		[OperationContract(Name = "GetVolumeByISBNAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<GetVolumeByISBNResp> GetVolumeByISBNAsync(GetVolumeByISBNReq request);

		// Token: 0x06000007 RID: 7
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetVolumeByISBNResp GetVolumeByISBN(GetVolumeByISBNReq request);

		// Token: 0x06000008 RID: 8
		[OperationContract(Name = "GetVolumeByIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<GetVolumeByIdResp> GetVolumeByIdAsync(GetVolumeByIdReq request);

		// Token: 0x06000009 RID: 9
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetVolumeByIdResp GetVolumeById(GetVolumeByIdReq request);
	}
}
