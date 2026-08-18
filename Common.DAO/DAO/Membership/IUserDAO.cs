using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.DAO.Membership
{
	// Token: 0x02000050 RID: 80
	public interface IUserDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B9 RID: 441
		bool Exists(string username);

		// Token: 0x060001BA RID: 442
		User GetUser(string username);

		// Token: 0x060001BB RID: 443
		bool ValidateUserPassword(string userName, string password);

		// Token: 0x060001BC RID: 444
		bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword);

		// Token: 0x060001BD RID: 445
		bool UserMustChangePassword(string UserName);

		// Token: 0x060001BE RID: 446
		bool ClearUserPassword(string UserName);

		// Token: 0x060001BF RID: 447
		bool SetUserPassword(string userName, string newPassword);
	}
}
