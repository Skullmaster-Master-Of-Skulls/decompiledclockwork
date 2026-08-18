using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015E RID: 350
	internal class UserAccountClientBaseProxy : ClientBase<IUserAccount>, IUserAccount, IService
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x000215C0 File Offset: 0x0001F7C0
		public UserAccountClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000215CB File Offset: 0x0001F7CB
		public UserAccountClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000215D7 File Offset: 0x0001F7D7
		public void ClearAllPasswords(ClearAllPasswordsReq Request)
		{
			base.Channel.ClearAllPasswords(Request);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000215E7 File Offset: 0x0001F7E7
		public void ClearPrimaryPassword(ClearPrimaryPasswordReq Request)
		{
			base.Channel.ClearPrimaryPassword(Request);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public CreatePasswordResp CreatePassword(CreatePasswordReq Request)
		{
			return base.Channel.CreatePassword(Request);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00021618 File Offset: 0x0001F818
		public LoadPrimaryPasswordResp LoadPrimaryPassword(LoadPrimaryPasswordReq Request)
		{
			return base.Channel.LoadPrimaryPassword(Request);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00021636 File Offset: 0x0001F836
		public void RemovePassword(RemovePasswordReq Request)
		{
			base.Channel.RemovePassword(Request);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00021648 File Offset: 0x0001F848
		public UpdatePasswordResp UpdatePassword(UpdatePasswordReq Request)
		{
			return base.Channel.UpdatePassword(Request);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00021666 File Offset: 0x0001F866
		public void UpdatePasswordRequireChange(UpdatePasswordRequireChangeReq Request)
		{
			base.Channel.UpdatePasswordRequireChange(Request);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00021678 File Offset: 0x0001F878
		public UpdatePrimaryPasswordResp UpdatePrimaryPassword(UpdatePrimaryPasswordReq Request)
		{
			return base.Channel.UpdatePrimaryPassword(Request);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00021696 File Offset: 0x0001F896
		public void UpdatePrimaryPasswordRequireChange(UpdatePrimaryPasswordRequireChangeReq Request)
		{
			base.Channel.UpdatePrimaryPasswordRequireChange(Request);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000216A8 File Offset: 0x0001F8A8
		public UpdatePrimaryPassword2Resp UpdatePrimaryPassword2(UpdatePrimaryPassword2Req Request)
		{
			return base.Channel.UpdatePrimaryPassword2(Request);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000216C6 File Offset: 0x0001F8C6
		public void UpdatePrimaryPasswordExpiry(UpdatePrimaryPasswordExpiryReq Request)
		{
			base.Channel.UpdatePrimaryPasswordExpiry(Request);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000216D8 File Offset: 0x0001F8D8
		public LoadPasswordPolicyResp LoadPasswordPolicy(LoadPasswordPolicyReq Request)
		{
			return base.Channel.LoadPasswordPolicy(Request);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000216F8 File Offset: 0x0001F8F8
		public ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(ValidatePasswordAgainstPolicyReq Request)
		{
			return base.Channel.ValidatePasswordAgainstPolicy(Request);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00021716 File Offset: 0x0001F916
		public void UpdatePasswordPolicy(UpdatePasswordPolicyReq Request)
		{
			base.Channel.UpdatePasswordPolicy(Request);
		}
	}
}
