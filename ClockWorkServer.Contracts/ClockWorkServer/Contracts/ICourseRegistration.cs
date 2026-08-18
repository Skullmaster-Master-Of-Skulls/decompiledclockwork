using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000037 RID: 55
	[ServiceContract(Name = "CourseRegistrationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICourseRegistration : IService
	{
		// Token: 0x060001B7 RID: 439
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsCoursesResp LoadStudentsCourses(LoadStudentsCoursesReq Request);

		// Token: 0x060001B8 RID: 440
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ChangeCourseRegistrationStatus(ChangeCourseRegistrationStatusReq Request);

		// Token: 0x060001B9 RID: 441
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RegisterStudentInCourseResp RegisterStudentInCourse(RegisterStudentInCourseReq Request);

		// Token: 0x060001BA RID: 442
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteCourseRegistration(DeleteCourseRegistrationReq Request);

		// Token: 0x060001BB RID: 443
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetUniqueCourseRegistrationStartDatesByStudentResp GetUniqueCourseRegistrationStartDatesByStudent(GetUniqueCourseRegistrationStartDatesByStudentReq Request);

		// Token: 0x060001BC RID: 444
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetDateLetterIssuedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request);

		// Token: 0x060001BD RID: 445
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetDateLetterReturnedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request);

		// Token: 0x060001BE RID: 446
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetProfLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request);

		// Token: 0x060001BF RID: 447
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetStudentLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request);

		// Token: 0x060001C0 RID: 448
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetDateLetterIssuedByCourses(SetCourseLetterDateByCoursesReq Request);

		// Token: 0x060001C1 RID: 449
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetDateLetterReturnedByCourses(SetCourseLetterDateByCoursesReq Request);

		// Token: 0x060001C2 RID: 450
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetProfLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request);

		// Token: 0x060001C3 RID: 451
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetStudentLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request);

		// Token: 0x060001C4 RID: 452
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCoursesStudentIsAllowedToBookTestsForNowResp LoadCoursesStudentIsAllowedToBookTestsForNow(LoadCoursesStudentIsAllowedToBookTestsForNowReq Request);

		// Token: 0x060001C5 RID: 453
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp LoadCoursesStudentIsAllowedToBookFinalExamsForNow(LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq Request);

		// Token: 0x060001C6 RID: 454
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseRegistrationsByStudentAndCourseResp LoadCourseRegistrationsByStudentAndCourse(LoadCourseRegistrationsByStudentAndCourseReq Request);

		// Token: 0x060001C7 RID: 455
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsInstructorOrAltContactTeachingStudentsCourseResp IsInstructorOrAltContactTeachingStudentsCourse(IsInstructorOrAltContactTeachingStudentsCourseReq Request);

		// Token: 0x060001C8 RID: 456
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsCoursesWithStudentSpecificInfosResp LoadStudentsCoursesWithStudentSpecificInfos(LoadStudentsCoursesWithStudentSpecificInfosReq Request);
	}
}
