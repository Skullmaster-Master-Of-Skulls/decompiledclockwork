using System;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions.QueryStorage
{
	// Token: 0x02000029 RID: 41
	public static class QueryStorageMiscCode
	{
		// Token: 0x04000050 RID: 80
		internal const string QS_MISCCODE_VALUE_BY_CODE = "SELECT miscstring FROM misc WHERE misccode=@misccode";

		// Token: 0x04000051 RID: 81
		internal const string QI_MISCCODE_VALUE_BY_CODE = "IF EXISTS(SELECT misccode FROM misc WHERE misccode=@misccode)\r\n    UPDATE misc SET miscstring=@miscstring WHERE misccode=@misccode\r\nELSE \r\n    INSERT INTO misc(misccode,miscstring) VALUES (@misccode,@miscstring)";

		// Token: 0x04000052 RID: 82
		internal const string QD_MISCCODE_AND_VALUE = "DELETE FROM misc WHERE misccode=@misccode";
	}
}
