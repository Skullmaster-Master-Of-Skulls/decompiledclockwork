using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000370 RID: 880
	[ServiceContract(Name = "IWSTrustFeb2005Async", Namespace = "http://schemas.microsoft.com/ws/2008/06/identity/securitytokenservice")]
	public interface IWSTrustFeb2005AsyncContract
	{
		// Token: 0x0600203B RID: 8251
		[OperationContract(Name = "TrustFeb2005CancelAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel")]
		IAsyncResult BeginTrustFeb2005Cancel(Message request, AsyncCallback callback, object state);

		// Token: 0x0600203C RID: 8252
		Message EndTrustFeb2005Cancel(IAsyncResult ar);

		// Token: 0x0600203D RID: 8253
		[OperationContract(Name = "TrustFeb2005IssueAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue")]
		IAsyncResult BeginTrustFeb2005Issue(Message request, AsyncCallback callback, object state);

		// Token: 0x0600203E RID: 8254
		Message EndTrustFeb2005Issue(IAsyncResult ar);

		// Token: 0x0600203F RID: 8255
		[OperationContract(Name = "TrustFeb2005RenewAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew")]
		IAsyncResult BeginTrustFeb2005Renew(Message request, AsyncCallback callback, object state);

		// Token: 0x06002040 RID: 8256
		Message EndTrustFeb2005Renew(IAsyncResult ar);

		// Token: 0x06002041 RID: 8257
		[OperationContract(Name = "TrustFeb2005ValidateAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate")]
		IAsyncResult BeginTrustFeb2005Validate(Message request, AsyncCallback callback, object state);

		// Token: 0x06002042 RID: 8258
		Message EndTrustFeb2005Validate(IAsyncResult ar);

		// Token: 0x06002043 RID: 8259
		[OperationContract(Name = "TrustFeb2005CancelResponseAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel")]
		IAsyncResult BeginTrustFeb2005CancelResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002044 RID: 8260
		Message EndTrustFeb2005CancelResponse(IAsyncResult ar);

		// Token: 0x06002045 RID: 8261
		[OperationContract(Name = "TrustFeb2005IssueResponseAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue")]
		IAsyncResult BeginTrustFeb2005IssueResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002046 RID: 8262
		Message EndTrustFeb2005IssueResponse(IAsyncResult ar);

		// Token: 0x06002047 RID: 8263
		[OperationContract(Name = "TrustFeb2005RenewResponseAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew")]
		IAsyncResult BeginTrustFeb2005RenewResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x06002048 RID: 8264
		Message EndTrustFeb2005RenewResponse(IAsyncResult ar);

		// Token: 0x06002049 RID: 8265
		[OperationContract(Name = "TrustFeb2005ValidateResponseAsync", AsyncPattern = true, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate")]
		IAsyncResult BeginTrustFeb2005ValidateResponse(Message request, AsyncCallback callback, object state);

		// Token: 0x0600204A RID: 8266
		Message EndTrustFeb2005ValidateResponse(IAsyncResult ar);
	}
}
