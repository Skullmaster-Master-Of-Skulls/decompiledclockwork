using System;

namespace TechnoPro.Common.DAO.Impl.People.QueryStorage
{
	// Token: 0x0200007C RID: 124
	public static class QueryStorageStaffCommonInfo
	{
		// Token: 0x04000162 RID: 354
		internal const string QS_STAFF_COMMON_INFO = "EXEC CommonStaff @pid,@staffgrouponly";

		// Token: 0x04000163 RID: 355
		internal const string QS_STAFF_COMMON_INFO_BY_GROUP = "EXEC CommonStaffByGroup @grouptitle,@altgrouptitle";
	}
}
