using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036F RID: 879
	[ServiceContract]
	public interface IWSTrustContract
	{
		// Token: 0x0600202F RID: 8239
		[OperationContract(Name = "Cancel", Action = "*", ReplyAction = "*")]
		Message Cancel(Message message);

		// Token: 0x06002030 RID: 8240
		[OperationContract(AsyncPattern = true, Name = "Cancel", Action = "*", ReplyAction = "*")]
		IAsyncResult BeginCancel(Message message, AsyncCallback callback, object asyncState);

		// Token: 0x06002031 RID: 8241
		Message EndCancel(IAsyncResult asyncResult);

		// Token: 0x06002032 RID: 8242
		[OperationContract(Name = "Issue", Action = "*", ReplyAction = "*")]
		Message Issue(Message message);

		// Token: 0x06002033 RID: 8243
		[OperationContract(AsyncPattern = true, Name = "Issue", Action = "*", ReplyAction = "*")]
		IAsyncResult BeginIssue(Message message, AsyncCallback callback, object asyncState);

		// Token: 0x06002034 RID: 8244
		Message EndIssue(IAsyncResult asyncResult);

		// Token: 0x06002035 RID: 8245
		[OperationContract(Name = "Renew", Action = "*", ReplyAction = "*")]
		Message Renew(Message message);

		// Token: 0x06002036 RID: 8246
		[OperationContract(AsyncPattern = true, Name = "Renew", Action = "*", ReplyAction = "*")]
		IAsyncResult BeginRenew(Message message, AsyncCallback callback, object asyncState);

		// Token: 0x06002037 RID: 8247
		Message EndRenew(IAsyncResult asyncResult);

		// Token: 0x06002038 RID: 8248
		[OperationContract(Name = "Validate", Action = "*", ReplyAction = "*")]
		Message Validate(Message message);

		// Token: 0x06002039 RID: 8249
		[OperationContract(AsyncPattern = true, Name = "Validate", Action = "*", ReplyAction = "*")]
		IAsyncResult BeginValidate(Message message, AsyncCallback callback, object asyncState);

		// Token: 0x0600203A RID: 8250
		Message EndValidate(IAsyncResult asyncResult);
	}
}
