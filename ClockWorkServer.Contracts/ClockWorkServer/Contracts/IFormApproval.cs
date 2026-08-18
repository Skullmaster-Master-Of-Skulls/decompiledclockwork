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
	// Token: 0x02000047 RID: 71
	[ServiceContract(Name = "FormApprovalService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFormApproval : IService
	{
		// Token: 0x0600023F RID: 575
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetFormApprovalScreenUserForLoggedInUserOptionsResp GetFormApprovalScreenUserForLoggedInUserOptions(GetFormApprovalScreenUserForLoggedInUserOptionsReq Request);

		// Token: 0x06000240 RID: 576
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPendingFormApprovalItemsForCurrentUserResp LoadPendingFormApprovalItemsForCurrentUser(LoadPendingFormApprovalItemsForCurrentUserReq Request);

		// Token: 0x06000241 RID: 577
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormApprovalStatusResp LoadFormApprovalStatus(LoadFormApprovalStatusReq Request);

		// Token: 0x06000242 RID: 578
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq Request);

		// Token: 0x06000243 RID: 579
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AreAnyFormApprovalScreensEnabledForLoggedInUserResp AreAnyFormApprovalScreensEnabledForLoggedInUser(AreAnyFormApprovalScreensEnabledForLoggedInUserReq Request);

		// Token: 0x06000244 RID: 580
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq Request);
	}
}
