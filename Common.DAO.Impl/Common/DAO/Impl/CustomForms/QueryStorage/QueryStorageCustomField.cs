using System;

namespace TechnoPro.Common.DAO.Impl.CustomForms.QueryStorage
{
	// Token: 0x02000103 RID: 259
	public static class QueryStorageCustomField
	{
		// Token: 0x0400043E RID: 1086
		internal const string QI_CREATE_DATA_INSTANCE = "INSERT INTO CustomDataInstance(caption,datapurposecode,datatypecode,ishidden) OUTPUT inserted.DataInstanceId AS DataInstanceId VALUES (@title,@purposecode,@datatypecode,@ishidden)";

		// Token: 0x0400043F RID: 1087
		internal const string QU_UPDATE_DATA_INSTANCE = "UPDATE CustomDataInstance SET caption=@title,datapurposecode=@purposecode,ishidden=@ishidden WHERE datainstanceid=@datainstanceid";

		// Token: 0x04000440 RID: 1088
		internal const string QD_DELETE_DATA_INSTANCE = "UPDATE CustomDataInstance SEt ishidden=1 WHERE datainstanceid=@datainstanceid";
	}
}
