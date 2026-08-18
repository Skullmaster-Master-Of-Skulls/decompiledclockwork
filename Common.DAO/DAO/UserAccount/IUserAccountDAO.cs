using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.Common.DAO.UserAccount
{
	// Token: 0x0200001B RID: 27
	public interface IUserAccountDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600004E RID: 78
		void RemovePassword(int PersonId, string UserName);

		// Token: 0x0600004F RID: 79
		void CreatePassword(UserInfoPassword PasswordInfo);

		// Token: 0x06000050 RID: 80
		void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange);

		// Token: 0x06000051 RID: 81
		void UpdatePassword(int PersonId, string UserName, string NewPassword);

		// Token: 0x06000052 RID: 82
		void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true);

		// Token: 0x06000053 RID: 83
		UserInfoPassword LoadPassword(string UserName, int PersonId = 0);

		// Token: 0x06000054 RID: 84
		void UpdatePassword2(string UserName, UserInfoPassword PasswordInfo);

		// Token: 0x06000055 RID: 85
		void UpdatePrimaryPasswordExpiry(int PersonId, string UserName, DateTime? NewExpiryDate);

		// Token: 0x06000056 RID: 86
		IList<int> LoadPersonIdsWithUsername(string Username, bool includeDeletedAccounts = false);
	}
}
