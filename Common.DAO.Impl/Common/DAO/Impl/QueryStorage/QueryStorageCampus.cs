using System;

namespace TechnoPro.Common.DAO.Impl.QueryStorage
{
	// Token: 0x02000119 RID: 281
	public static class QueryStorageCampus
	{
		// Token: 0x040004B1 RID: 1201
		internal const string QS_GET_ALL_CAMPUS = "Select CampusId,CampusName,CampusDescription from CampusLookup where IsActive=1 order by OrderNum";

		// Token: 0x040004B2 RID: 1202
		internal const string QI_CREATE_CAMPUS = "insert into CampusLookup\r\n\t\t\t(CampusName\r\n\t\t\t,CampusDescription)\r\nvalues\r\n\t\t\t(@campusname\r\n\t\t\t,@campusdescription)\r\nset @campusid = SCOPE_IDENTITY()";

		// Token: 0x040004B3 RID: 1203
		internal const string QU_UPDATE_CAMPUS = "update CampusLookup\r\nset\t CampusName = @campusname\r\n\t,CampusDescription = @campusdescription\r\nwhere CampusId=@campusid";

		// Token: 0x040004B4 RID: 1204
		internal const string QD_DELETE_CAMPUS = "delete from CampusLookup where CampusId=@campusid";
	}
}
