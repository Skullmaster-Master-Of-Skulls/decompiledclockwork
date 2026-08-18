using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000049 RID: 73
	[ServiceContract(Name = "FormApprovalTraineeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFormApprovalTrainee : IService
	{
		// Token: 0x0600024B RID: 587
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormApprovalForTraineeResp LoadFormApprovalForTrainee(LoadFormApprovalForTraineeReq Request);

		// Token: 0x0600024C RID: 588
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddFormApprovalCommentForTraineeResp AddFormApprovalCommentForTrainee(AddFormApprovalCommentForTraineeReq Request);

		// Token: 0x0600024D RID: 589
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ReSubmitFormApprovalFormResp ReSubmitFormApprovalForm(ReSubmitFormApprovalFormReq Request);

		// Token: 0x0600024E RID: 590
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateFormApprovalFormResp CreateFormApprovalForm(CreateFormApprovalFormReq Request);

		// Token: 0x0600024F RID: 591
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTraineeSignatureResp LoadTraineeSignature(LoadTraineeSignatureReq Request);
	}
}
