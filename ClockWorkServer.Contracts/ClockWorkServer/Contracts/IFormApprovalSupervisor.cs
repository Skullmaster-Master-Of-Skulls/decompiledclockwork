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
	// Token: 0x02000048 RID: 72
	[ServiceContract(Name = "FormApprovalSupervisorService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFormApprovalSupervisor : IService
	{
		// Token: 0x06000245 RID: 581
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormApprovalForSupervisorResp LoadFormApprovalForSupervisor(LoadFormApprovalForSupervisorReq Request);

		// Token: 0x06000246 RID: 582
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddFormApprovalCommentForSupervisorResp AddFormApprovalCommentForSupervisor(AddFormApprovalCommentForSupervisorReq Request);

		// Token: 0x06000247 RID: 583
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ApproveFormResp ApproveForm(ApproveFormReq Request);

		// Token: 0x06000248 RID: 584
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SendFormBackToTraineeResp SendFormBackToTrainee(SendFormBackToTraineeReq Request);

		// Token: 0x06000249 RID: 585
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UnApproveFormApprovalResp UnApproveFormApproval(UnApproveFormApprovalReq Request);

		// Token: 0x0600024A RID: 586
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadSupervisorSignatureResp LoadSupervisorSignature(LoadSupervisorSignatureReq Request);
	}
}
