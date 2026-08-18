using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.ICore.Membership
{
	// Token: 0x02000062 RID: 98
	public interface IUserManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002AA RID: 682
		User AddUser(User user);

		// Token: 0x060002AB RID: 683
		User GetUser(string userName);

		// Token: 0x060002AC RID: 684
		IList<User> GetUsers(string role);

		// Token: 0x060002AD RID: 685
		bool IsAdmin(User user);

		// Token: 0x060002AE RID: 686
		void Remove(User user);

		// Token: 0x060002AF RID: 687
		void Remove(string username);

		// Token: 0x060002B0 RID: 688
		int RemoveAll(Predicate<User> userCond);

		// Token: 0x060002B1 RID: 689
		bool Exists(string username);

		// Token: 0x060002B2 RID: 690
		bool ValidateUserPassword(string UserName, string password);

		// Token: 0x060002B3 RID: 691
		bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword, out string msg);

		// Token: 0x060002B4 RID: 692
		bool UserMustChangePassword(string UserName);

		// Token: 0x060002B5 RID: 693
		bool ChangeUserPasswordByAdmin(string UserName, string NewPassword, out string msg);
	}
}
