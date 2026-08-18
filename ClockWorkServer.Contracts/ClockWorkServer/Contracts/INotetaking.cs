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
	// Token: 0x0200006C RID: 108
	[ServiceContract(Name = "NotetakingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface INotetaking : IService
	{
		// Token: 0x06000327 RID: 807
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotetakerBaseByUsernameResp LoadNotetakerBaseByUsername(LoadNotetakerBaseByUsernameReq Request);

		// Token: 0x06000328 RID: 808
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLectureNoteDescriptionsByStudentAndCourseResp LoadLectureNoteDescriptionsByStudentAndCourse(LoadLectureNoteDescriptionsByStudentAndCourseReq Request);

		// Token: 0x06000329 RID: 809
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotetakerBaseByIdResp LoadNotetakerBaseById(LoadNotetakerBaseByIdReq Request);

		// Token: 0x0600032A RID: 810
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotetakerBaseByNotetakeeAndCourseResp LoadNotetakerBaseByNotetakeeAndCourse(LoadNotetakerBaseByNotetakeeAndCourseReq Request);

		// Token: 0x0600032B RID: 811
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLectureNoteDescriptionsByNotetakerAndCourseResp LoadLectureNoteDescriptionsByNotetakerAndCourse(LoadLectureNoteDescriptionsByNotetakerAndCourseReq Request);

		// Token: 0x0600032C RID: 812
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLectureNoteByIdResp LoadLectureNoteById(LoadLectureNoteByIdReq Request);

		// Token: 0x0600032D RID: 813
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp LoadMatchingNotetakersWithLectureNoteUploadsByCourse(LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq Request);

		// Token: 0x0600032E RID: 814
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadEquivalentCoursesResp LoadEquivalentCourses(LoadEquivalentCoursesReq Request);

		// Token: 0x0600032F RID: 815
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void AddPotentialCoursesForNotetaker(AddPotentialCoursesForNotetakerReq Request);

		// Token: 0x06000330 RID: 816
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateNotetakerAccountResp CreateNotetakerAccount(CreateNotetakerAccountReq Request);

		// Token: 0x06000331 RID: 817
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RecordStudentDownloadedLectureNote(RecordStudentDownloadedLectureNoteReq Request);

		// Token: 0x06000332 RID: 818
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentDownloadedLectureNoteHistoryResp LoadStudentDownloadedLectureNoteHistory(LoadStudentDownloadedLectureNoteHistoryReq Request);

		// Token: 0x06000333 RID: 819
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq request);

		// Token: 0x06000334 RID: 820
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateLectureNoteResp CreateLectureNote(CreateLectureNoteReq Request);

		// Token: 0x06000335 RID: 821
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateLectureNoteResp UpdateLectureNote(UpdateLectureNoteReq Request);

		// Token: 0x06000336 RID: 822
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteLectureNote(DeleteLectureNoteReq Request);

		// Token: 0x06000337 RID: 823
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUniqueAvailableCourseStartDatesByNotetakerResp LoadUniqueAvailableCourseStartDatesByNotetaker(LoadUniqueAvailableCourseStartDatesByNotetakerReq Request);

		// Token: 0x06000338 RID: 824
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadNotetakerAvailableCoursesResp LoadNotetakerAvailableCourses(LoadNotetakerAvailableCoursesReq Request);

		// Token: 0x06000339 RID: 825
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUniqueStudentsReceivingNotesResp LoadUniqueStudentsReceivingNotes(LoadUniqueStudentsReceivingNotesReq Request);

		// Token: 0x0600033A RID: 826
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AssignNotetakerResp AssignNotetaker(AssignNotetakerReq Request);

		// Token: 0x0600033B RID: 827
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelNotetakerAssignmentResp CancelNotetakerAssignment(CancelNotetakerAssignmentReq Request);
	}
}
