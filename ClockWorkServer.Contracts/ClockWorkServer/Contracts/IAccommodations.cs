using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000041 RID: 65
	[ServiceContract(Name = "AccommodationsService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAccommodations : IService
	{
		// Token: 0x060001F7 RID: 503
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAccommodationChangesResp LoadAccommodationChanges(LoadAccommodationChangesReq Request);

		// Token: 0x060001F8 RID: 504
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsRegisteredCoursesWithAccommodationsResp LoadStudentsRegisteredCoursesWithAccommodations(LoadStudentsRegisteredCoursesWithAccommodationsReq Request);

		// Token: 0x060001F9 RID: 505
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq Request);

		// Token: 0x060001FA RID: 506
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAccommodationsByStudentAndCourseOrTemplateResp LoadAccommodationsByStudentAndCourseOrTemplate(LoadAccommodationsByStudentAndCourseOrTemplateReq Request);

		// Token: 0x060001FB RID: 507
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearAccommodations(ClearAccommodationsReq Request);

		// Token: 0x060001FC RID: 508
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkAccommodationLetterIssued(MarkAccommodationLetterIssuedReq Request);

		// Token: 0x060001FD RID: 509
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MergeOrReplaceAccommodations(MergeOrReplaceAccommodationsReq Request);

		// Token: 0x060001FE RID: 510
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetStudentAccommodationsExpiryDateResp GetStudentAccommodationsExpiryDate(GetStudentAccommodationsExpiryDateReq Request);
	}
}
