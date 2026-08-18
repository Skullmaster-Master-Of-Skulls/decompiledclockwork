using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000064 RID: 100
	[ServiceContract(Name = "LookupInstructorService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILookupInstructor : IService
	{
		// Token: 0x060002EB RID: 747
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateInstructorDataSyncExemptionResp UpdateInstructorDataSyncExemption(UpdateInstructorDataSyncExemptionReq Request);

		// Token: 0x060002EC RID: 748
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorResp LoadInstructor(LoadInstructorReq Request);

		// Token: 0x060002ED RID: 749
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveInstructorResp SaveInstructor(SaveInstructorReq Request);

		// Token: 0x060002EE RID: 750
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveInstructorsForCourseResp SaveInstructorsForCourse(SaveInstructorsForCourseReq Request);

		// Token: 0x060002EF RID: 751
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorByUsernameResp LoadInstructorByUsername(LoadInstructorByUsernameReq Request);

		// Token: 0x060002F0 RID: 752
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorByEmployeeIdResp LoadInstructorByEmployeeId(LoadInstructorByEmployeeIdReq Request);

		// Token: 0x060002F1 RID: 753
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorByEmailResp LoadInstructorByEmail(LoadInstructorByEmailReq Request);

		// Token: 0x060002F2 RID: 754
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorCoursesResp LoadInstructorCourses(LoadInstructorCoursesReq Request);

		// Token: 0x060002F3 RID: 755
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAssignedInstructorsResp LoadAllAssignedInstructors(LoadAllAssignedInstructorsReq Request);

		// Token: 0x060002F4 RID: 756
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorsBySearchStringResp LoadInstructorsBySearchString(LoadInstructorsBySearchStringReq Request);

		// Token: 0x060002F5 RID: 757
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void AssignInstructorToCourse(AssignInstructorToCourseReq Request);

		// Token: 0x060002F6 RID: 758
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RemoveInstructorFromCourse(RemoveInstructorFromCourseReq Request);

		// Token: 0x060002F7 RID: 759
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorsByCourseResp LoadInstructorsByCourse(LoadInstructorsByCourseReq Request);

		// Token: 0x060002F8 RID: 760
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetUniqueCourseRegistrationStartDatesByInstructorResp GetUniqueCourseRegistrationStartDatesByInstructor(GetUniqueCourseRegistrationStartDatesByInstructorReq Request);

		// Token: 0x060002F9 RID: 761
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp LoadInstructorCoursesWithAtLeastOneStudentRegistered(LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq Request);

		// Token: 0x060002FA RID: 762
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetStudentsWithApprovedRequestsByCourseDateResp GetStudentsWithApprovedRequestsByCourseDate(GetStudentsWithApprovedRequestsByCourseDateReq Request);

		// Token: 0x060002FB RID: 763
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsWithCourseAndAccommodationInfosByCoursesResp LoadStudentsWithCourseAndAccommodationInfosByCourses(LoadStudentsWithCourseAndAccommodationInfosByCoursesReq Request);
	}
}
