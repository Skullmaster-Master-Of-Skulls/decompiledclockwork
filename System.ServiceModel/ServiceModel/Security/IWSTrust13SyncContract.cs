using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036D RID: 877
	[ServiceContract(Name = "IWSTrust13Sync", Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/securitytokenservice")]
	public interface IWSTrust13SyncContract
	{
		// Token: 0x0600201A RID: 8218
		[OperationContract(Name = "Trust13Cancel", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal")]
		Message ProcessTrust13Cancel(Message message);

		// Token: 0x0600201B RID: 8219
		[OperationContract(Name = "Trust13Issue", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal")]
		Message ProcessTrust13Issue(Message message);

		// Token: 0x0600201C RID: 8220
		[OperationContract(Name = "Trust13Renew", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal")]
		Message ProcessTrust13Renew(Message message);

		// Token: 0x0600201D RID: 8221
		[OperationContract(Name = "Trust13Validate", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal")]
		Message ProcessTrust13Validate(Message message);

		// Token: 0x0600201E RID: 8222
		[OperationContract(Name = "Trust13CancelResponse", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", ReplyAction = "*")]
		Message ProcessTrust13CancelResponse(Message message);

		// Token: 0x0600201F RID: 8223
		[OperationContract(Name = "Trust13IssueResponse", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", ReplyAction = "*")]
		Message ProcessTrust13IssueResponse(Message message);

		// Token: 0x06002020 RID: 8224
		[OperationContract(Name = "Trust13RenewResponse", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", ReplyAction = "*")]
		Message ProcessTrust13RenewResponse(Message message);

		// Token: 0x06002021 RID: 8225
		[OperationContract(Name = "Trust13ValidateResponse", Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate", ReplyAction = "*")]
		Message ProcessTrust13ValidateResponse(Message message);
	}
}
