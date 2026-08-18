using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015D RID: 349
	public class UserAccountReusableClientProxy : WCFTokenBasedReusableClientProxy<IUserAccount>, IUserAccount, IService
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x00021261 File Offset: 0x0001F461
		public UserAccountReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002126C File Offset: 0x0001F46C
		public UserAccountReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00021278 File Offset: 0x0001F478
		public void ClearAllPasswords(ClearAllPasswordsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearAllPasswords(Request);
			});
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000212B0 File Offset: 0x0001F4B0
		public void ClearPrimaryPassword(ClearPrimaryPasswordReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearPrimaryPassword(Request);
			});
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000212E8 File Offset: 0x0001F4E8
		public CreatePasswordResp CreatePassword(CreatePasswordReq Request)
		{
			return this.WrapServiceMethod<CreatePasswordResp>(() => this.Proxy.CreatePassword(Request));
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00021320 File Offset: 0x0001F520
		public LoadPrimaryPasswordResp LoadPrimaryPassword(LoadPrimaryPasswordReq Request)
		{
			return this.WrapServiceMethod<LoadPrimaryPasswordResp>(() => this.Proxy.LoadPrimaryPassword(Request));
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00021358 File Offset: 0x0001F558
		public void RemovePassword(RemovePasswordReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RemovePassword(Request);
			});
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00021390 File Offset: 0x0001F590
		public UpdatePasswordResp UpdatePassword(UpdatePasswordReq Request)
		{
			return this.WrapServiceMethod<UpdatePasswordResp>(() => this.Proxy.UpdatePassword(Request));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public void UpdatePasswordRequireChange(UpdatePasswordRequireChangeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePasswordRequireChange(Request);
			});
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00021400 File Offset: 0x0001F600
		public UpdatePrimaryPasswordResp UpdatePrimaryPassword(UpdatePrimaryPasswordReq Request)
		{
			return this.WrapServiceMethod<UpdatePrimaryPasswordResp>(() => this.Proxy.UpdatePrimaryPassword(Request));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00021438 File Offset: 0x0001F638
		public void UpdatePrimaryPasswordRequireChange(UpdatePrimaryPasswordRequireChangeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePrimaryPasswordRequireChange(Request);
			});
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00021470 File Offset: 0x0001F670
		public UpdatePrimaryPassword2Resp UpdatePrimaryPassword2(UpdatePrimaryPassword2Req Request)
		{
			return this.WrapServiceMethod<UpdatePrimaryPassword2Resp>(() => this.Proxy.UpdatePrimaryPassword2(Request));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000214A8 File Offset: 0x0001F6A8
		public void UpdatePrimaryPasswordExpiry(UpdatePrimaryPasswordExpiryReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePrimaryPasswordExpiry(Request);
			});
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000214E0 File Offset: 0x0001F6E0
		public LoadPasswordPolicyResp LoadPasswordPolicy(LoadPasswordPolicyReq Request)
		{
			return this.WrapServiceMethod<LoadPasswordPolicyResp>(() => this.Proxy.LoadPasswordPolicy(Request));
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00021518 File Offset: 0x0001F718
		public ValidatePasswordAgainstPolicyResp PasswordMeetsRequirements(ValidatePasswordAgainstPolicyReq Request)
		{
			return this.WrapServiceMethod<ValidatePasswordAgainstPolicyResp>(() => this.Proxy.ValidatePasswordAgainstPolicy(Request));
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00021550 File Offset: 0x0001F750
		public void UpdatePasswordPolicy(UpdatePasswordPolicyReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePasswordPolicy(Request);
			});
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00021588 File Offset: 0x0001F788
		public ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(ValidatePasswordAgainstPolicyReq Request)
		{
			return this.WrapServiceMethod<ValidatePasswordAgainstPolicyResp>(() => this.Proxy.ValidatePasswordAgainstPolicy(Request));
		}
	}
}
