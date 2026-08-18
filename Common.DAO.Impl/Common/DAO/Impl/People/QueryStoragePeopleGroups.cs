using System;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000073 RID: 115
	public static class QueryStoragePeopleGroups
	{
		// Token: 0x04000122 RID: 290
		internal const string QS_GROUP_IDS_BY_PID = "SELECT DISTINCT groupid FROM peoplegroups WHERE personid=@pid ORDER BY groupid";

		// Token: 0x04000123 RID: 291
		internal const string QS_GROUPS_BASE = "SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n";

		// Token: 0x04000124 RID: 292
		internal const string QS_GROUPS_BY_GROUPIDS = "SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\nWHERE g.groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')) ORDER BY g.description";

		// Token: 0x04000125 RID: 293
		internal const string QS_ALL_GROUPS = "SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n ORDER BY g.description";

		// Token: 0x04000126 RID: 294
		internal const string QS_GROUP_BY_ID = "SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n WHERE g.groupid=@gid";

		// Token: 0x04000127 RID: 295
		internal const string QS_GROUP_BY_TITLE = "SELECT g.groupid,g.description,g.viewAppsVisible,g.fulldescription,g.ordernum,g.isprimary \r\nFROM groups g \r\n WHERE g.description=@grouptitle";

		// Token: 0x04000128 RID: 296
		internal const string QS_GROUP_MEMBER_COUNT = "SELECT COUNT(personid) AS ct FROM peoplegroups WHERE groupid=@groupid";
	}
}
