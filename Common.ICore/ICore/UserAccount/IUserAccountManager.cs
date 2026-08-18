using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.Common.ICore.UserAccount
{
	// Token: 0x02000018 RID: 24
	public interface IUserAccountManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600009C RID: 156
		void RemovePassword(int PersonId, string UserName);

		// Token: 0x0600009D RID: 157
		bool CreatePassword(UserInfoPassword PasswordInfo, out string message);

		// Token: 0x0600009E RID: 158
		void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange);

		// Token: 0x0600009F RID: 159
		bool UpdatePassword(int PersonId, string UserName, string NewPassword, out string message);

		// Token: 0x060000A0 RID: 160
		void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true);

		// Token: 0x060000A1 RID: 161
		UserInfoPassword LoadPassword(int PersonId, string UserName);

		// Token: 0x060000A2 RID: 162
		UserInfoPassword LoadPrimaryPassword(int PersonId);

		// Token: 0x060000A3 RID: 163
		void ClearPrimaryPassword(int PersonId);

		// Token: 0x060000A4 RID: 164
		void UpdatePrimaryPasswordRequireChange(int PersonId, bool NewDoesRequirePasswordChange);

		// Token: 0x060000A5 RID: 165
		bool UpdatePrimaryPassword(int PersonId, string NewPassword, out string message);

		// Token: 0x060000A6 RID: 166
		bool UpdatePrimaryPassword2(UserInfoPassword PasswordInfo, out string message);

		// Token: 0x060000A7 RID: 167
		void UpdatePrimaryPasswordExpiry(int PersonId, DateTime? NewExpiryDate);

		// Token: 0x060000A8 RID: 168
		IList<int> LoadPersonIdsWithUsername(string Username, bool includeDeletedAccounts = false);

		// Token: 0x060000A9 RID: 169
		bool ValidatePasswordAgainstPolicy(string Password, out string message);

		// Token: 0x060000AA RID: 170
		PasswordPolicy LoadPasswordPolicy();

		// Token: 0x060000AB RID: 171
		void UpdatePasswordPolicy(PasswordPolicy Policy);
	}
}
