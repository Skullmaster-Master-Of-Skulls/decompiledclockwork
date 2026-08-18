using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x02000040 RID: 64
	public interface IStaffCommonInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000130 RID: 304
		StaffWithCommonInfo LoadStaffWithCommonInfoById(int PersonId);

		// Token: 0x06000131 RID: 305
		T LoadStaffWithCommonInfoById<T>(int PersonId) where T : StaffWithCommonInfo;

		// Token: 0x06000132 RID: 306
		IList<T> LoadStaffWithCommonInfoByGroupTitle<T>(params string[] GroupTitles) where T : StaffWithCommonInfo;
	}
}
