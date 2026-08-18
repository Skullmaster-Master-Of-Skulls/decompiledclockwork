using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036C RID: 876
	[ServiceContract(Name = "IWSTrust13Async", Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/securitytokenservice")]
	public interface IWSTrust13AsyncContract
	{
		// Token: 0x0600200A RID: 8202
		[OperationContract(Name = "Trust13CancelAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal")]
		IAsyncResult BeginTrust13Cancel(Message request, AsyncCallback callback, object state);

		// Token: 0x0600200B RID: 8203
		Message EndTrust13Cancel(IAsyncResult ar);

		// Token: 0x0600200C RID: 8204
		[OperationContract(Name = "Trust13IssueAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal")]
		IAsyncResult BeginTrust13Issue(Message request, AsyncCallback callback, object state);

		// Token: 0x0600200D RID: 8205
		Message EndTrust13Issue(IAsyncResult ar);

		// Token: 0x0600200E RID: 8206
		[OperationContract(Name = "Trust13RenewAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal")]
		IAsyncResult BeginTrust13Renew(Message request, AsyncCallback callback, object state);

		// Token: 0x0600200F RID: 8207
		Message EndTrust13Renew(IAsyncResult ar);

		// Token: 0x06002010 RID: 8208
		[OperationContract(Name = "Trust13ValidateAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal")]
		IAsyncResult BeginTrust13Validate(Message request, AsyncCallback callback, object state);

		// Token: 0x06002011 RID: 8209
		Message EndTrust13Validate(IAsyncResult ar);

		// Token: 0x06002012 RID: 8210
		[OperationContract(Name = "Trust13CancelResponseAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", ReplyAction = "*")]
		IAsyncResult BeginTrust13CancelResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002013 RID: 8211
		Message EndTrust13CancelResponse(IAsyncResult ar);

		// Token: 0x06002014 RID: 8212
		[OperationContract(Name = "Trust13IssueResponseAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", ReplyAction = "*")]
		IAsyncResult BeginTrust13IssueResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002015 RID: 8213
		Message EndTrust13IssueResponse(IAsyncResult ar);

		// Token: 0x06002016 RID: 8214
		[OperationContract(Name = "Trust13RenewResponseAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", ReplyAction = "*")]
		IAsyncResult BeginTrust13RenewResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002017 RID: 8215
		Message EndTrust13RenewResponse(IAsyncResult ar);

		// Token: 0x06002018 RID: 8216
		[OperationContract(Name = "Trust13ValidateResponseAsync", AsyncPattern = true, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate", ReplyAction = "*")]
		IAsyncResult BeginTrust13ValidateResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002019 RID: 8217
		Message EndTrust13ValidateResponse(IAsyncResult ar);
	}
}
