using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200006E RID: 110
	[ServiceContract(Name = "OnlineFormQueueService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IOnlineFormQueue : IService
	{
		// Token: 0x06000344 RID: 836
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupOnlineFormStatusesResp LoadLookupOnlineFormStatuses(LoadLookupOnlineFormStatusesReq request);

		// Token: 0x06000345 RID: 837
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadOnlineFormQueueItemsResp LoadOnlineFormQueueItems(LoadOnlineFormQueueItemsReq request);

		// Token: 0x06000346 RID: 838
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteOnlineFormQueueItemResp DeleteOnlineFormQueueItem(DeleteOnlineFormQueueItemReq request);

		// Token: 0x06000347 RID: 839
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadOnlineFormQueueItemFormDataItemsResp LoadOnlineFormQueueItemFormDataItems(LoadOnlineFormQueueItemFormDataItemsReq request);

		// Token: 0x06000348 RID: 840
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadOnlineFormQueueItemResp LoadOnlineFormQueueItem(LoadOnlineFormQueueItemReq request);

		// Token: 0x06000349 RID: 841
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedOnlineFormsResp LoadAllowedOnlineForms(LoadAllowedOnlineFormsReq request);

		// Token: 0x0600034A RID: 842
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateOnlineFormQueueItemStaffNoteAndStatusResp UpdateOnlineFormQueueItemStaffNoteAndStatus(UpdateOnlineFormQueueItemStaffNoteAndStatusReq request);

		// Token: 0x0600034B RID: 843
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateOnlineFormQueueItemStaffNoteResp UpdateOnlineFormQueueItemStaffNote(UpdateOnlineFormQueueItemStaffNoteReq request);

		// Token: 0x0600034C RID: 844
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateOnlineFormQueueItemStatusResp UpdateOnlineFormQueueItemStatus(UpdateOnlineFormQueueItemStatusReq request);

		// Token: 0x0600034D RID: 845
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllStudentOnlineFormsResp LoadAllStudentOnlineForms(LoadAllStudentOnlineFormsReq Request);

		// Token: 0x0600034E RID: 846
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadOnlineFormQueueFormsWithOpenItemsCountResp LoadOnlineFormQueueFormsWithOpenItemsCount(LoadOnlineFormQueueFormsWithOpenItemsCountReq request);
	}
}
