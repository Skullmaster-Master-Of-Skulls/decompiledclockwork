using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000008 RID: 8
	[ServiceContract(Name = "MediaContentFileService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraSizeService]
	public interface IMediaContentFile : IService
	{
		// Token: 0x06000022 RID: 34
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaContentFileInfoResp CreateMediaContentFileInfo(CreateMediaContentFileInfoReq request);

		// Token: 0x06000023 RID: 35
		[OperationContract(Name = "CreateMediaContentFileInfoAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateMediaContentFileInfoResp> CreateMediaContentFileInfoAsync(CreateMediaContentFileInfoReq request);

		// Token: 0x06000024 RID: 36
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentFileByContentResp LoadMediaContentFileByContent(LoadMediaContentFileByContentReq request);

		// Token: 0x06000025 RID: 37
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentFileByStudentIdResp LoadMediaContentFileByStudentId(LoadMediaContentFileByStudentIdReq request);

		// Token: 0x06000026 RID: 38
		[OperationContract(Name = "LoadAvailableMediaContentFileByStudentIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAvailableMediaContentFileByStudentIdResp> LoadAvailableMediaContentFileByStudentIdAsync(LoadAvailableMediaContentFileByStudentIdReq request);

		// Token: 0x06000027 RID: 39
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaContentFileWithoutDataResp UpdateMediaContentFileWithoutData(UpdateMediaContentFileWithoutDataReq mediaContentFile);

		// Token: 0x06000028 RID: 40
		[OperationContract(Name = "DeleteMediaContentFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteMediaContentFileResp> DeleteMediaContentFileAsync(DeleteMediaContentFileReq request);

		// Token: 0x06000029 RID: 41
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentFileMatchingResp GetMediaContentFileMatching(GetMediaContentFileMatchingReq request);

		// Token: 0x0600002A RID: 42
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentFileByMediaContentPerFormatIdResp LoadMediaContentFileByMediaContentPerFormatId(LoadMediaContentFileByMediaContentPerFormatIdReq request);

		// Token: 0x0600002B RID: 43
		[OperationContract(Name = "LoadMediaContentFileByMediaContentPerFormatIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadMediaContentFileByMediaContentPerFormatIdResp> LoadMediaContentFileByMediaContentPerFormatIdAsync(LoadMediaContentFileByMediaContentPerFormatIdReq request);

		// Token: 0x0600002C RID: 44
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentFileByMediaContentAndFormatResp LoadMediaContentFileByMediaContentAndFormat(LoadMediaContentFileByMediaContentAndFormatReq request);

		// Token: 0x0600002D RID: 45
		[OperationContract(Name = "LoadAvailableMediaContentFileByStudentAndMediaContentAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAvailableMediaContentFileByStudentAndMediaContentResp> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(LoadAvailableMediaContentFileByStudentAndMediaContentReq request);
	}
}
