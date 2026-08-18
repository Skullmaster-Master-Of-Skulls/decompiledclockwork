using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;

namespace TechnoPro.Common.DAO.UserAccount
{
	// Token: 0x0200001A RID: 26
	public interface IUserLoginTrackingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600004A RID: 74
		void RecordNewLogin(LoginInfo LoginInfo);

		// Token: 0x0600004B RID: 75
		LoginInfo LoadLoginInfoByPersonId(int PersonId);

		// Token: 0x0600004C RID: 76
		IList<LoginInfo> LoadAllLoginInfos();

		// Token: 0x0600004D RID: 77
		IList<LoginInfo> LoadLoginInfosByDateRange(DateTime StartDate, DateTime EndDate);
	}
}
