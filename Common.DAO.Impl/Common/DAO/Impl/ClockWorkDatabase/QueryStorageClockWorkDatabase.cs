using System;

namespace TechnoPro.Common.DAO.Impl.ClockWorkDatabase
{
	// Token: 0x02000112 RID: 274
	internal static class QueryStorageClockWorkDatabase
	{
		// Token: 0x04000492 RID: 1170
		internal const string QS_TABLE_BY_NAME = "SELECT * FROM information_schema.tables WHERE TABLE_NAME=@tablename";

		// Token: 0x04000493 RID: 1171
		internal const string QS_ALL_TABLE_NAMES = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES\r\nwhere TABLE_TYPE = 'BASE TABLE'\r\nORDER BY TABLE_NAME";
	}
}
