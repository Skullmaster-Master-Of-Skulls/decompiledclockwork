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
	// Token: 0x02000063 RID: 99
	[ServiceContract(Name = "AlternateContactService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAlternateContact : IService
	{
		// Token: 0x060002E0 RID: 736
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAlternateContactResp CreateAlternateContact(CreateAlternateContactReq Request);

		// Token: 0x060002E1 RID: 737
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAlternateContactByIdResp LoadAlternateContactById(LoadAlternateContactByIdReq Request);

		// Token: 0x060002E2 RID: 738
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAlternateContactsByCourseResp LoadAlternateContactsByCourse(LoadAlternateContactsByCourseReq Request);

		// Token: 0x060002E3 RID: 739
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAlternateContactsBySearchStringResp LoadAlternateContactsBySearchString(LoadAlternateContactsBySearchStringReq Request);

		// Token: 0x060002E4 RID: 740
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAlternateContact(UpdateAlternateContactReq Request);

		// Token: 0x060002E5 RID: 741
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAlternateContact(DeleteAlternateContactReq Request);

		// Token: 0x060002E6 RID: 742
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAlternateContactByUsernameResp LoadAlternateContactByUsername(LoadAlternateContactByUsernameReq Request);

		// Token: 0x060002E7 RID: 743
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void AssignAlternateContactToCourse(AssignAlternateContactToCourseReq Request);

		// Token: 0x060002E8 RID: 744
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RemoveAlternateContactFromCourse(RemoveAlternateContactFromCourseReq Request);

		// Token: 0x060002E9 RID: 745
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAlternateContactByEmployeeIdResp LoadAlternateContactByEmployeeId(LoadAlternateContactByEmployeeIdReq Request);

		// Token: 0x060002EA RID: 746
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetUniqueCourseRegistrationStartDatesByAlternateContactResp GetUniqueCourseRegistrationStartDatesByAlternateContact(GetUniqueCourseRegistrationStartDatesByAlternateContactReq Request);
	}
}
