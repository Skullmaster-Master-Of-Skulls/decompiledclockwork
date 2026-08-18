using System;

namespace TechnoPro.Common.DAO.Impl.Institution
{
	// Token: 0x020000C9 RID: 201
	internal class QueryStorageInstitution
	{
		// Token: 0x040002D8 RID: 728
		internal const string SQ_GET_INSTITUTION_UNIQUE_NAME = "SELECT UniqueName FROM UniqueDatabaseName2()";

		// Token: 0x040002D9 RID: 729
		internal const string SQ_GET_INSTITUTION_NAME = "select settingstringvalue from SettingsGroups where groupid = -1 and settingcode = 312";
	}
}
