using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200000E RID: 14
	[ServiceContract(Name = "StudentMediaRequestService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentMediaRequest : IService
	{
		// Token: 0x0600006E RID: 110
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateStudentMediaResp CreateStudentMediaRequest(CreateStudentMediaReq request);

		// Token: 0x0600006F RID: 111
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateStudentMediaResp UpdateStudentMediaRequest(UpdateStudentMediaReq request);

		// Token: 0x06000070 RID: 112
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentMediaRequestByIdResp LoadStudentMediaRequestById(LoadStudentMediaRequestByIdReq request);

		// Token: 0x06000071 RID: 113
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentMediaRequestByStatusResp LoadStudentMediaRequestByStatus(LoadStudentMediaRequestByStatusReq request);

		// Token: 0x06000072 RID: 114
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllApprovedMediaRequestResp LoadAllApprovedMediaRequest(LoadAllApprovedMediaRequestReq request);

		// Token: 0x06000073 RID: 115
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequest(LoadAllToBeApprovedMediaRequestReq request);

		// Token: 0x06000074 RID: 116
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequestByStudent(LoadAllToBeApprovedMediaRequestByStudentReq request);

		// Token: 0x06000075 RID: 117
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllInProgressStudentMediaRequestResp LoadAllInProgressStudentMediaRequest(LoadAllInProgressStudentMediaRequestReq request);

		// Token: 0x06000076 RID: 118
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllInProgressStudentMediaRequestByStudentResp LoadAllInProgressStudentMediaRequestByStudent(LoadAllInProgressStudentMediaRequestByStudentReq request);

		// Token: 0x06000077 RID: 119
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequest(LoadAllCompletedStudentMediaRequestReq request);

		// Token: 0x06000078 RID: 120
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByDate(LoadAllCompletedStudentMediaRequestByDateReq request);

		// Token: 0x06000079 RID: 121
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudent(LoadAllCompletedStudentMediaRequestByStudentReq request);

		// Token: 0x0600007A RID: 122
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudentAndDate(LoadAllCompletedStudentMediaRequestByStudentAndDateReq request);

		// Token: 0x0600007B RID: 123
		[OperationContract(Name = "LoadAllStudentMediaRequestByStudentAndDatesAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAllStudentMediaRequestByStudentAndDatesResp> LoadAllStudentMediaRequestByStudentAndDatesAsync(LoadAllStudentMediaRequestByStudentAndDatesReq request);

		// Token: 0x0600007C RID: 124
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateStudentContentMediaRequestInfoResp UpdateStudentContentMediaRequestInfo(UpdateStudentContentMediaRequestInfoReq request);

		// Token: 0x0600007D RID: 125
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddStudentContentMediaRequestInfoResp AddStudentContentMediaRequestInfo(AddStudentContentMediaRequestInfoReq request);

		// Token: 0x0600007E RID: 126
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteStudentContentMediaRequestInfoResp DeleteStudentContentMediaRequestInfo(DeleteStudentContentMediaRequestInfoReq request);

		// Token: 0x0600007F RID: 127
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteStudentContentMediaRequestInfoByIdResp DeleteStudentContentMediaRequestInfoById(DeleteStudentContentMediaRequestInfoByIdReq request);

		// Token: 0x06000080 RID: 128
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DownloadProofOfPurchaseResp DownloadProofOfPurchase(DownloadProofOfPurchaseReq request);

		// Token: 0x06000081 RID: 129
		[OperationContract(Name = "DownloadProofOfPurchaseAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DownloadProofOfPurchaseResp> DownloadProofOfPurchaseAsync(DownloadProofOfPurchaseReq request);

		// Token: 0x06000082 RID: 130
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadProofOfPurchaseResp UploadProofOfPurchase(UploadProofOfPurchaseReq request);

		// Token: 0x06000083 RID: 131
		[OperationContract(Name = "UploadProofOfPurchaseAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UploadProofOfPurchaseResp> UploadProofOfPurchaseAsync(UploadProofOfPurchaseReq request);

		// Token: 0x06000084 RID: 132
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllMediaRequestInfoByJobIdResp LoadAllMediaRequestInfoByJobId(LoadAllMediaRequestInfoByJobIdReq request);

		// Token: 0x06000085 RID: 133
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsMediaContentAlreadyRequestedResp IsMediaContentAlreadyRequested(IsMediaContentAlreadyRequestedReq request);

		// Token: 0x06000086 RID: 134
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentRequestedInfoByIdResp LoadMediaContentRequestedInfoById(LoadMediaContentRequestedInfoByIdReq request);

		// Token: 0x06000087 RID: 135
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AcceptProofOfPurchaseReceiptResp AcceptProofOfPurchaseReceipt(AcceptProofOfPurchaseReceiptReq request);

		// Token: 0x06000088 RID: 136
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RejectProofOfPurchaseReceiptResp RejectProofOfPurchaseReceipt(RejectProofOfPurchaseReceiptReq request);

		// Token: 0x06000089 RID: 137
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllowedMediaContentFormatsForStudentToRequestResp GetAllowedMediaContentFormatsForStudentToRequest(GetAllowedMediaContentFormatsForStudentToRequestReq Request);
	}
}
