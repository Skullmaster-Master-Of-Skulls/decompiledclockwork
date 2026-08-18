using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000090 RID: 144
	[ServiceContract(Name = "SurveyQueueService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ISurveyQueue : IService
	{
		// Token: 0x060003E4 RID: 996
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadLookupSurveyStatusesResp> LoadLookupSurveyStatusesAsync(LoadLookupSurveyStatusesReq request);

		// Token: 0x060003E5 RID: 997
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadSurveyQueueItemsResp> LoadSurveyQueueItemsAsync(LoadSurveyQueueItemsReq request);

		// Token: 0x060003E6 RID: 998
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteSurveyQueueItemResp> DeleteSurveyQueueItemAsync(DeleteSurveyQueueItemReq request);

		// Token: 0x060003E7 RID: 999
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadSurveyQueueItemFormDataItemsResp> LoadSurveyQueueItemFormDataItemsAsync(LoadSurveyQueueItemFormDataItemsReq request);

		// Token: 0x060003E8 RID: 1000
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadSurveyQueueItemResp> LoadSurveyQueueItemAsync(LoadSurveyQueueItemReq request);

		// Token: 0x060003E9 RID: 1001
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAllowedSurveysResp> LoadAllowedSurveysAsync(LoadAllowedSurveysReq request);

		// Token: 0x060003EA RID: 1002
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateSurveyQueueItemStaffNoteAndStatusResp> UpdateSurveyQueueItemStaffNoteAndStatusAsync(UpdateSurveyQueueItemStaffNoteAndStatusReq request);

		// Token: 0x060003EB RID: 1003
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateSurveyQueueItemStaffNoteResp> UpdateSurveyQueueItemStaffNoteAsync(UpdateSurveyQueueItemStaffNoteReq request);

		// Token: 0x060003EC RID: 1004
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateSurveyQueueItemStatusResp> UpdateSurveyQueueItemStatusAsync(UpdateSurveyQueueItemStatusReq request);
	}
}
