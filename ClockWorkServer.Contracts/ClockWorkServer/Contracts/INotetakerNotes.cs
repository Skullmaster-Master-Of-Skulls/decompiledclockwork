using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200006B RID: 107
	[ServiceContract(Name = "NotetakerNotesService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface INotetakerNotes : IService
	{
		// Token: 0x0600031F RID: 799
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLectureNoteDescriptionsResp LoadLectureNoteDescriptions(LoadLectureNoteDescriptionsReq Request);

		// Token: 0x06000320 RID: 800
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteAllNotesMarkedForDeletionTodayOrEarlierResp DeleteAllNotesMarkedForDeletionTodayOrEarlier(DeleteAllNotesMarkedForDeletionTodayOrEarlierReq Request);

		// Token: 0x06000321 RID: 801
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteAllNotesMarkedForDeletionResp DeleteAllNotesMarkedForDeletion(DeleteAllNotesMarkedForDeletionReq Request);

		// Token: 0x06000322 RID: 802
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveAllNotesDeletionMarksResp RemoveAllNotesDeletionMarks(RemoveAllNotesDeletionMarksReq Request);

		// Token: 0x06000323 RID: 803
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveNotesDeletionMarksResp RemoveNotesDeletionMarks(RemoveNotesDeletionMarksReq Request);

		// Token: 0x06000324 RID: 804
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddNotesDeletionMarksResp AddNotesDeletionMarks(AddNotesDeletionMarksReq Request);

		// Token: 0x06000325 RID: 805
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DownloadLectureNoteResp DownloadLectureNote(DownloadLectureNoteReq Request);

		// Token: 0x06000326 RID: 806
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTotalFileSizeByMonthResp GetTotalFileSizeByMonth(GetTotalFileSizeByMonthReq Request);
	}
}
