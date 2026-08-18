using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200004F RID: 79
	[ServiceContract(Name = "IntakeAccountService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IIntakeAccount : IService
	{
		// Token: 0x0600025D RID: 605
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateNewIntakeAccountResp CreateNewIntakeAccount(CreateNewIntakeAccountReq Request);

		// Token: 0x0600025E RID: 606
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPendingIntakeEntriesResp LoadPendingIntakeEntries(LoadPendingIntakeEntriesReq Request);

		// Token: 0x0600025F RID: 607
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPendingIntakeEntryQueueItemsResp LoadPendingIntakeEntryQueueItems(LoadPendingIntakeEntryQueueItemsReq Request);

		// Token: 0x06000260 RID: 608
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateActiveIntakeStatusAndNoteResp UpdateActiveIntakeStatusAndNote(UpdateActiveIntakeStatusAndNoteReq Request);

		// Token: 0x06000261 RID: 609
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateActiveIntakeStatusResp UpdateActiveIntakeStatus(UpdateActiveIntakeStatusReq Request);

		// Token: 0x06000262 RID: 610
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateActiveIntakeNoteResp UpdateActiveIntakeNote(UpdateActiveIntakeNoteReq Request);

		// Token: 0x06000263 RID: 611
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveIntakeResp RemoveIntake(RemoveIntakeReq Request);

		// Token: 0x06000264 RID: 612
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupStatusesResp LoadLookupStatuses(LoadLookupStatusesReq Request);

		// Token: 0x06000265 RID: 613
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateRealStudentAccountFromIntakeAndRemoveIntakeResp CreateRealStudentAccountFromIntakeAndRemoveIntake(CreateRealStudentAccountFromIntakeAndRemoveIntakeReq Request);

		// Token: 0x06000266 RID: 614
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadIntakeFormDataResp LoadIntakeFormData(LoadIntakeFormDataReq Request);

		// Token: 0x06000267 RID: 615
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetIntakeStatusesResp GetIntakeStatuses(GetIntakeStatusesReq Request);

		// Token: 0x06000268 RID: 616
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveIntakesResp RemoveIntakes(RemoveIntakesReq Request);

		// Token: 0x06000269 RID: 617
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SyncIntakeDataResp SyncIntakeData(SyncIntakeDataReq Request);
	}
}
