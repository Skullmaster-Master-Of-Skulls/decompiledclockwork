using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A2 RID: 162
	[ServiceContract(Name = "SurveyService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ISurvey : IService
	{
		// Token: 0x060004BA RID: 1210
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllSurveysResp GetAllSurveys(GetAllSurveysReq request);

		// Token: 0x060004BB RID: 1211
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveSurveysResp GetActiveSurveys(GetActiveSurveysReq request);

		// Token: 0x060004BC RID: 1212
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSurveyResp GetSurvey(GetSurveyReq request);

		// Token: 0x060004BD RID: 1213
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateSurvey(UpdateSurveyReq request);

		// Token: 0x060004BE RID: 1214
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateNewSurveyResp CreateNewSurvey(CreateNewSurveyReq request);

		// Token: 0x060004BF RID: 1215
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteSurvey(DeleteSurveyReq Request);

		// Token: 0x060004C0 RID: 1216
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DisableSurvey(DisableSurveyReq Request);

		// Token: 0x060004C1 RID: 1217
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void EnableSurvey(EnableSurveyReq Request);
	}
}
