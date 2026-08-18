using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036E RID: 878
	[ServiceContract]
	[ComVisible(false)]
	public interface IWSTrustChannelContract : IWSTrustContract
	{
		// Token: 0x06002022 RID: 8226
		RequestSecurityTokenResponse Cancel(RequestSecurityToken request);

		// Token: 0x06002023 RID: 8227
		IAsyncResult BeginCancel(RequestSecurityToken request, AsyncCallback callback, object state);

		// Token: 0x06002024 RID: 8228
		void EndCancel(IAsyncResult result, out RequestSecurityTokenResponse response);

		// Token: 0x06002025 RID: 8229
		SecurityToken Issue(RequestSecurityToken request);

		// Token: 0x06002026 RID: 8230
		SecurityToken Issue(RequestSecurityToken request, out RequestSecurityTokenResponse response);

		// Token: 0x06002027 RID: 8231
		IAsyncResult BeginIssue(RequestSecurityToken request, AsyncCallback callback, object asyncState);

		// Token: 0x06002028 RID: 8232
		SecurityToken EndIssue(IAsyncResult result, out RequestSecurityTokenResponse response);

		// Token: 0x06002029 RID: 8233
		RequestSecurityTokenResponse Renew(RequestSecurityToken request);

		// Token: 0x0600202A RID: 8234
		IAsyncResult BeginRenew(RequestSecurityToken request, AsyncCallback callback, object state);

		// Token: 0x0600202B RID: 8235
		void EndRenew(IAsyncResult result, out RequestSecurityTokenResponse response);

		// Token: 0x0600202C RID: 8236
		RequestSecurityTokenResponse Validate(RequestSecurityToken request);

		// Token: 0x0600202D RID: 8237
		IAsyncResult BeginValidate(RequestSecurityToken request, AsyncCallback callback, object state);

		// Token: 0x0600202E RID: 8238
		void EndValidate(IAsyncResult result, out RequestSecurityTokenResponse response);
	}
}
