using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.UserAccount;
using TechnoPro.Common.DAO.UserAccount;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;

namespace TechnoPro.Common.Core.UserAccount
{
	// Token: 0x0200002E RID: 46
	public class UserLoginTrackingManager : IUserLoginTrackingManager, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00008F1F File Offset: 0x0000711F
		public UserLoginTrackingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008F31 File Offset: 0x00007131
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00008F39 File Offset: 0x00007139
		public OperationContext OpContext { get; set; }

		// Token: 0x060001BC RID: 444 RVA: 0x00008F44 File Offset: 0x00007144
		public void RecordNewLogin(LoginInfo LoginInfo)
		{
			IUserLoginTrackingDAO userLoginTrackingDAO = new UserLoginTrackingDAO(this.OpContext);
			userLoginTrackingDAO.RecordNewLogin(LoginInfo);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008F68 File Offset: 0x00007168
		public LoginInfo LoadLoginInfoByPersonId(int PersonId)
		{
			IUserLoginTrackingDAO userLoginTrackingDAO = new UserLoginTrackingDAO(this.OpContext);
			return userLoginTrackingDAO.LoadLoginInfoByPersonId(PersonId);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008F90 File Offset: 0x00007190
		public IList<LoginInfo> LoadAllLoginInfos()
		{
			IUserLoginTrackingDAO userLoginTrackingDAO = new UserLoginTrackingDAO(this.OpContext);
			return userLoginTrackingDAO.LoadAllLoginInfos();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008FB4 File Offset: 0x000071B4
		public IList<LoginInfo> LoadLoginInfosByDateRange(DateTime StartDate, DateTime EndDate)
		{
			IUserLoginTrackingDAO userLoginTrackingDAO = new UserLoginTrackingDAO(this.OpContext);
			return userLoginTrackingDAO.LoadLoginInfosByDateRange(StartDate, EndDate);
		}
	}
}
