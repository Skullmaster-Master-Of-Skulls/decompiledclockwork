using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.UserAccount
{
	// Token: 0x02000007 RID: 7
	public class UserAccountRestClientManager : BearerTokenRestProxy<IUserAccountClientManager>, IUserAccountClientManager, IWebService
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002760 File Offset: 0x00000960
		public UserAccountRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000276A File Offset: 0x0000096A
		public UserAccountRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002778 File Offset: 0x00000978
		public void RemovePassword(int PersonId, string UserName)
		{
			RemovePasswordReq removePasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemovePasswordReq>();
			removePasswordReq.PersonId = PersonId;
			removePasswordReq.UserName = UserName;
			base.Post<RemovePasswordReq>(removePasswordReq, "useraccount/removepassword");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027AA File Offset: 0x000009AA
		public CreatePasswordResp CreatePassword(UserInfoPasswordDTO PasswordInfo)
		{
			return base.Post<UserInfoPasswordDTO, CreatePasswordResp>(PasswordInfo, "useraccount/createpassword");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027B8 File Offset: 0x000009B8
		public void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange)
		{
			UpdatePasswordRequireChangeReq updatePasswordRequireChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePasswordRequireChangeReq>();
			updatePasswordRequireChangeReq.PersonId = PersonId;
			updatePasswordRequireChangeReq.UserName = UserName;
			updatePasswordRequireChangeReq.NewDoesRequirePasswordChange = NewDoesRequirePasswordChange;
			base.Put<UpdatePasswordRequireChangeReq>(updatePasswordRequireChangeReq, "useraccount/passwordrequirechange");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027F4 File Offset: 0x000009F4
		public UpdatePasswordResp UpdatePassword(int PersonId, string UserName, string NewPassword)
		{
			UpdatePasswordReq updatePasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePasswordReq>();
			updatePasswordReq.PersonId = PersonId;
			updatePasswordReq.UserName = UserName;
			updatePasswordReq.NewPassword = NewPassword;
			return base.Post<UpdatePasswordReq, UpdatePasswordResp>(updatePasswordReq, "useraccount/password");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002830 File Offset: 0x00000A30
		public void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true)
		{
			ClearAllPasswordsReq clearAllPasswordsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAllPasswordsReq>();
			clearAllPasswordsReq.PersonId = PersonId;
			clearAllPasswordsReq.ClearPrimaryPassword = ClearPrimaryPassword;
			base.Post<ClearAllPasswordsReq>(clearAllPasswordsReq, "useraccount/clearallpasswords");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002862 File Offset: 0x00000A62
		public UserInfoPasswordDTO LoadPrimaryPassword(int PersonId)
		{
			return base.Get<UserInfoPasswordDTO>(string.Format("useraccount/primarypassword/personid/{0}", PersonId), true);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000287B File Offset: 0x00000A7B
		public void ClearPrimaryPassword(int PersonId)
		{
			base.Post<int>(PersonId, "useraccount/clearprimarypassword");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000288C File Offset: 0x00000A8C
		public void UpdatePrimaryPasswordRequireChange(int PersonId, bool NewDoesRequirePasswordChange)
		{
			UpdatePrimaryPasswordRequireChangeReq updatePrimaryPasswordRequireChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrimaryPasswordRequireChangeReq>();
			updatePrimaryPasswordRequireChangeReq.PersonId = PersonId;
			updatePrimaryPasswordRequireChangeReq.NewDoesRequirePasswordChange = NewDoesRequirePasswordChange;
			base.Put<UpdatePrimaryPasswordRequireChangeReq>(updatePrimaryPasswordRequireChangeReq, "useraccount/primarypasswordrequirechange");
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028BE File Offset: 0x00000ABE
		public UpdatePrimaryPassword2Resp UpdatePrimaryPassword(UserInfoPasswordDTO PasswordInfo)
		{
			return base.Post<UserInfoPasswordDTO, UpdatePrimaryPassword2Resp>(PasswordInfo, "useraccount/primarypassword/v2");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028CC File Offset: 0x00000ACC
		public void UpdatePrimaryPasswordExpiry(int PersonId, DateTime? NewExpiryDate)
		{
			UpdatePrimaryPasswordExpiryReq updatePrimaryPasswordExpiryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrimaryPasswordExpiryReq>();
			updatePrimaryPasswordExpiryReq.PersonId = PersonId;
			updatePrimaryPasswordExpiryReq.NewExpiryDate = NewExpiryDate;
			base.Put<UpdatePrimaryPasswordExpiryReq>(updatePrimaryPasswordExpiryReq, "useraccount/primarypasswordexpiry");
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028FE File Offset: 0x00000AFE
		public ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(string Password)
		{
			return base.Post<string, ValidatePasswordAgainstPolicyResp>(Password, "useraccount/validatepasswordagainstpolicy");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000290C File Offset: 0x00000B0C
		public PasswordPolicyDTO LoadPasswordPolicy()
		{
			return base.Get<PasswordPolicyDTO>("useraccount/passwordpolicy", true);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000291A File Offset: 0x00000B1A
		public void UpdatePasswordPolicy(PasswordPolicyDTO Policy)
		{
			base.Put<PasswordPolicyDTO>(Policy, "useraccount/passwordpolicy");
		}
	}
}
