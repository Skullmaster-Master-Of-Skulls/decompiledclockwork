using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200008B RID: 139
	[ServiceContract(Name = "InMemoryFilesStorageService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraSizeService]
	public interface IInMemoryFilesStorage : IService
	{
		// Token: 0x060003C7 RID: 967
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DownloadFileResp DownloadFile(DownloadFileReq request);

		// Token: 0x060003C8 RID: 968
		[OperationContract(Name = "DownloadFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DownloadFileResp> DownloadFileAsync(DownloadFileReq request);

		// Token: 0x060003C9 RID: 969
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadFileResp UploadFile(UploadFileReq request);

		// Token: 0x060003CA RID: 970
		[OperationContract(Name = "UploadFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UploadFileResp> UploadFileAsync(UploadFileReq request);

		// Token: 0x060003CB RID: 971
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DownloadFileResp DownloadTempFile(DownloadFileReq request);

		// Token: 0x060003CC RID: 972
		[OperationContract(Name = "DownloadTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DownloadFileResp> DownloadTempFileAsync(DownloadFileReq request);

		// Token: 0x060003CD RID: 973
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadFileResp UploadTempFile(UploadFileReq request);

		// Token: 0x060003CE RID: 974
		[OperationContract(Name = "UploadTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UploadFileResp> UploadTempFileAsync(UploadFileReq request);

		// Token: 0x060003CF RID: 975
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteFileResp DeleteFile(DeleteFileReq request);

		// Token: 0x060003D0 RID: 976
		[OperationContract(Name = "DeleteFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteFileResp> DeleteFileAsync(DeleteFileReq request);

		// Token: 0x060003D1 RID: 977
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteFileResp DeleteTempFile(DeleteFileReq request);

		// Token: 0x060003D2 RID: 978
		[OperationContract(Name = "DeleteTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteFileResp> DeleteTempFileAsync(DeleteFileReq request);
	}
}
