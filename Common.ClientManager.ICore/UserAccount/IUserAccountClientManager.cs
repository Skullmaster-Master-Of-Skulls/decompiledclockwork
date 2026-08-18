using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.UserAccount
{
	// Token: 0x02000008 RID: 8
	public interface IUserAccountClientManager : IWebService
	{
		// Token: 0x06000019 RID: 25
		void RemovePassword(int PersonId, string UserName);

		// Token: 0x0600001A RID: 26
		CreatePasswordResp CreatePassword(UserInfoPasswordDTO PasswordInfo);

		// Token: 0x0600001B RID: 27
		void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange);

		// Token: 0x0600001C RID: 28
		UpdatePasswordResp UpdatePassword(int PersonId, string UserName, string NewPassword);

		// Token: 0x0600001D RID: 29
		void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true);

		// Token: 0x0600001E RID: 30
		UserInfoPasswordDTO LoadPrimaryPassword(int PersonId);

		// Token: 0x0600001F RID: 31
		void ClearPrimaryPassword(int PersonId);

		// Token: 0x06000020 RID: 32
		void UpdatePrimaryPasswordRequireChange(int PersonId, bool NewDoesRequirePasswordChange);

		// Token: 0x06000021 RID: 33
		UpdatePrimaryPassword2Resp UpdatePrimaryPassword(UserInfoPasswordDTO PasswordInfo);

		// Token: 0x06000022 RID: 34
		void UpdatePrimaryPasswordExpiry(int PersonId, DateTime? NewExpiryDate);

		// Token: 0x06000023 RID: 35
		ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(string Password);

		// Token: 0x06000024 RID: 36
		PasswordPolicyDTO LoadPasswordPolicy();

		// Token: 0x06000025 RID: 37
		void UpdatePasswordPolicy(PasswordPolicyDTO Policy);
	}
}
