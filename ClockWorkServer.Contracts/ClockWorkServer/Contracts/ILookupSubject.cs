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
	// Token: 0x02000066 RID: 102
	[ServiceContract(Name = "LookupSubjectService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILookupSubject : IService
	{
		// Token: 0x060002FF RID: 767
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupSubjectsBySessionResp LoadLookupSubjectsBySession(LoadLookupSubjectsBySessionReq Request);

		// Token: 0x06000300 RID: 768
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupSubjectByIdResp LoadLookupSubjectById(LoadLookupSubjectByIdReq Request);

		// Token: 0x06000301 RID: 769
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveSubjectResp SaveSubject(SaveSubjectReq Request);

		// Token: 0x06000302 RID: 770
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupSubjectBySubjectCodeResp LoadLookupSubjectBySubjectCode(LoadLookupSubjectBySubjectCodeReq Request);

		// Token: 0x06000303 RID: 771
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupSubjectBySubjectDescriptionResp LoadLookupSubjectBySubjectDescription(LoadLookupSubjectBySubjectDescriptionReq Request);

		// Token: 0x06000304 RID: 772
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupSubjectResp LoadLookupSubject(LoadLookupSubjectReq Request);

		// Token: 0x06000305 RID: 773
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllLookupSubjectsResp LoadAllLookupSubjects(LoadAllLookupSubjectsReq Request);
	}
}
