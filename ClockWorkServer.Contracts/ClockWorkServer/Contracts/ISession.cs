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
	// Token: 0x0200009D RID: 157
	[ServiceContract(Name = "SessionService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ISession : IService
	{
		// Token: 0x06000467 RID: 1127
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddSessionResp AddSession(AddSessionReq request);

		// Token: 0x06000468 RID: 1128
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SubtractSessionResp SubtractSession(SubtractSessionReq request);

		// Token: 0x06000469 RID: 1129
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GoToTodaysSessionResp GoToTodaysSession(GoToTodaysSessionReq request);

		// Token: 0x0600046A RID: 1130
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GotoSessionResp GotoSession(GotoSessionReq request);

		// Token: 0x0600046B RID: 1131
		[Obsolete("Use IAcademicTerm instead")]
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request);

		// Token: 0x0600046C RID: 1132
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CopySessionResp CopySession(CopySessionReq request);

		// Token: 0x0600046D RID: 1133
		[Obsolete("Use IAcademicTerm instead")]
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request);

		// Token: 0x0600046E RID: 1134
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCurrentSessionResp GetCurrentSession(GetCurrentSessionReq request);

		// Token: 0x0600046F RID: 1135
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSessionByDateResp GetSessionByDate(GetSessionByDateReq request);

		// Token: 0x06000470 RID: 1136
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetSessionChooserDefaultValue(SetSessionChooserDefaultValueReq Request);

		// Token: 0x06000471 RID: 1137
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSessionChooserDefaultValueResp GetSessionChooserDefaultValue();
	}
}
