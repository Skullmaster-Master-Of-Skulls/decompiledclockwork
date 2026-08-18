using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;

namespace TechnoPro.Common.ICore.UserAccount
{
	// Token: 0x02000017 RID: 23
	public interface IUserLoginTrackingManager : IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000098 RID: 152
		void RecordNewLogin(LoginInfo LoginInfo);

		// Token: 0x06000099 RID: 153
		LoginInfo LoadLoginInfoByPersonId(int PersonId);

		// Token: 0x0600009A RID: 154
		IList<LoginInfo> LoadAllLoginInfos();

		// Token: 0x0600009B RID: 155
		IList<LoginInfo> LoadLoginInfosByDateRange(DateTime StartDate, DateTime EndDate);
	}
}
