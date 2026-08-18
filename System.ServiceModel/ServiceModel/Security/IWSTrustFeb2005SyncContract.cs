using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000371 RID: 881
	[ServiceContract(Name = "IWSTrustFeb2005Sync", Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/securitytokenservice")]
	public interface IWSTrustFeb2005SyncContract
	{
		// Token: 0x0600204B RID: 8267
		[OperationContract(Name = "TrustFeb2005Cancel", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel")]
		Message ProcessTrustFeb2005Cancel(Message message);

		// Token: 0x0600204C RID: 8268
		[OperationContract(Name = "TrustFeb2005Issue", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue")]
		Message ProcessTrustFeb2005Issue(Message message);

		// Token: 0x0600204D RID: 8269
		[OperationContract(Name = "TrustFeb2005Renew", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew")]
		Message ProcessTrustFeb2005Renew(Message message);

		// Token: 0x0600204E RID: 8270
		[OperationContract(Name = "TrustFeb2005Validate", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate")]
		Message ProcessTrustFeb2005Validate(Message message);

		// Token: 0x0600204F RID: 8271
		[OperationContract(Name = "TrustFeb2005CancelResponse", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel")]
		Message ProcessTrustFeb2005CancelResponse(Message message);

		// Token: 0x06002050 RID: 8272
		[OperationContract(Name = "TrustFeb2005IssueResponse", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue")]
		Message ProcessTrustFeb2005IssueResponse(Message message);

		// Token: 0x06002051 RID: 8273
		[OperationContract(Name = "TrustFeb2005RenewResponse", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew")]
		Message ProcessTrustFeb2005RenewResponse(Message message);

		// Token: 0x06002052 RID: 8274
		[OperationContract(Name = "TrustFeb2005ValidateResponse", Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate")]
		Message ProcessTrustFeb2005ValidateResponse(Message message);
	}
}
