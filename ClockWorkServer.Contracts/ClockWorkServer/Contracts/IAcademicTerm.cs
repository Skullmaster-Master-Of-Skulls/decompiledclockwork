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
	// Token: 0x0200009E RID: 158
	[ServiceContract(Name = "AcademicTermService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAcademicTerm : IService
	{
		// Token: 0x06000472 RID: 1138
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request);

		// Token: 0x06000473 RID: 1139
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request);

		// Token: 0x06000474 RID: 1140
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAcademicTermResp GetAcademicTerm(GetAcademicTermReq request);

		// Token: 0x06000475 RID: 1141
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeCurrentAcademicTermsResp ChangeCurrentAcademicTerms(ChangeCurrentAcademicTermsReq request);

		// Token: 0x06000476 RID: 1142
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ValidateAcademicTermListResp ValidateAcademicTermList(ValidateAcademicTermListReq request);
	}
}
