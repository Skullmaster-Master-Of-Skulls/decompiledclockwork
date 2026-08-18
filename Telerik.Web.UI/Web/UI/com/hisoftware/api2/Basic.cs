using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001362 RID: 4962
	[ServiceContract(Namespace = "urn:hisoftware:compliancesheriff:services", ConfigurationName = "BasicServiceReference.Basic")]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public interface Basic
	{
		// Token: 0x0600CF54 RID: 53076
		[FaultContract(typeof(NotAvailableForUse), Action = "urn:hisoftware:compliancesheriff:services/Basic/DoFormSubmitNotAvailableForUseFault", Name = "NotAvailableForUse", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/DoFormSubmit", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/DoFormSubmitResponse")]
		void DoFormSubmit(Stream input);

		// Token: 0x0600CF55 RID: 53077
		[FaultContract(typeof(OnDemandScanCouldNotRunException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContentOnDemandScanCouldNotRunExceptionFault", Name = "OnDemandScanCouldNotRunException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(RunsLimitReachedException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContentRunsLimitReachedExceptionFault", Name = "RunsLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(PageCountLimitReachedException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContentPageCountLimitReachedExceptionFault", Name = "PageCountLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContentInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContent", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanContentResponse")]
		ResultInformation RunOnDemandScanContent(string apiKey, string displayName, string url, List<string> checkpointGroupIds, byte[] content, string encoding, int expiryTime);

		// Token: 0x0600CF56 RID: 53078
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScan", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanResponse")]
		[FaultContract(typeof(PageCountLimitReachedException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanPageCountLimitReachedExceptionFault", Name = "PageCountLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(RunsLimitReachedException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanRunsLimitReachedExceptionFault", Name = "RunsLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(OnDemandScanCouldNotRunException), Action = "urn:hisoftware:compliancesheriff:services/Basic/RunOnDemandScanOnDemandScanCouldNotRunExceptionFault", Name = "OnDemandScanCouldNotRunException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		ResultInformation RunOnDemandScan(string apiKey, string displayName, string url, List<string> checkpointGroupIds, string httpUserAgent, string encoding, int expiryTime);

		// Token: 0x0600CF57 RID: 53079
		[FaultContract(typeof(EmailIsInvalidException), Action = "urn:hisoftware:compliancesheriff:services/Basic/CreateAccountEmailIsInvalidExceptionFault", Name = "EmailIsInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/CreateAccount", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/CreateAccountResponse")]
		bool CreateAccount(string email);

		// Token: 0x0600CF58 RID: 53080
		[FaultContract(typeof(EmailIsInvalidException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ConfirmAccountEmailIsInvalidExceptionFault", Name = "EmailIsInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/ConfirmAccount", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/ConfirmAccountResponse")]
		[FaultContract(typeof(EmailIsEmptyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ConfirmAccountEmailIsEmptyExceptionFault", Name = "EmailIsEmptyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(ConfirmationCodeInvalidException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ConfirmAccountConfirmationCodeInvalidExceptionFault", Name = "ConfirmationCodeInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		string ConfirmAccount(string email, string confirmationCode);

		// Token: 0x0600CF59 RID: 53081
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetAccountInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/GetAccount", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/GetAccountResponse")]
		Account GetAccount(string apiKey);

		// Token: 0x0600CF5A RID: 53082
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/ResetApiKey", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/ResetApiKeyResponse")]
		[FaultContract(typeof(ConfirmationCodeInvalidException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ResetApiKeyConfirmationCodeInvalidExceptionFault", Name = "ConfirmationCodeInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(EmailIsEmptyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ResetApiKeyEmailIsEmptyExceptionFault", Name = "EmailIsEmptyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(EmailIsInvalidException), Action = "urn:hisoftware:compliancesheriff:services/Basic/ResetApiKeyEmailIsInvalidExceptionFault", Name = "EmailIsInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		string ResetApiKey(string email, string confirmationCode);

		// Token: 0x0600CF5B RID: 53083
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsSimpleInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsSimple", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsSimpleResponse")]
		[FaultContract(typeof(UnknownScanIDException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsSimpleUnknownScanIDExceptionFault", Name = "UnknownScanIDException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		List<Result> GetResultsSimple(string apiKey, string scanID);

		// Token: 0x0600CF5C RID: 53084
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsFull", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsFullResponse")]
		[FaultContract(typeof(UnknownScanIDException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsFullUnknownScanIDExceptionFault", Name = "UnknownScanIDException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetResultsFullInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		ResultInformation GetResultsFull(string apiKey, string scanID);

		// Token: 0x0600CF5D RID: 53085
		[FaultContract(typeof(InvalidApiKeyException), Action = "urn:hisoftware:compliancesheriff:services/Basic/GetCheckpointGroupsInvalidApiKeyExceptionFault", Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
		[OperationContract(Action = "urn:hisoftware:compliancesheriff:services/Basic/GetCheckpointGroups", ReplyAction = "urn:hisoftware:compliancesheriff:services/Basic/GetCheckpointGroupsResponse")]
		List<CheckpointGroup> GetCheckpointGroups(string apiKey, bool includeSubgroups);
	}
}
