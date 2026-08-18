using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000030 RID: 48
	[ServiceContract(Name = "CasesService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICases : IService
	{
		// Token: 0x06000196 RID: 406
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCasesForDisplayForStudentResp LoadCasesForDisplayForStudent(LoadCasesForDisplayForStudentReq Request);

		// Token: 0x06000197 RID: 407
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCaseByIdResp LoadCaseById(LoadCaseByIdReq Request);

		// Token: 0x06000198 RID: 408
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateCaseResp CreateCase(CreateCaseReq Request);

		// Token: 0x06000199 RID: 409
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteCase(DeleteCaseReq Request);

		// Token: 0x0600019A RID: 410
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateCase(UpdateCaseReq Request);

		// Token: 0x0600019B RID: 411
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadBasicAppointmentsByCaseResp LoadBasicAppointmentsByCase(LoadBasicAppointmentsByCaseReq Request);
	}
}
