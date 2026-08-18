using System;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x02000048 RID: 72
	public class QueryStorageMiscTable
	{
		// Token: 0x040000CF RID: 207
		internal static readonly string QS_MISC_SETTING_VALUE = "SELECT miscstring FROM misc WHERE misccode=@code";

		// Token: 0x040000D0 RID: 208
		internal static readonly string QI_MISC_SETTING_VALUE = "IF EXISTS(SELECT misccode FROM misc WHERE misccode=@code)\r\n    UPDATE misc SET miscstring=@value WHERE misccode=@code\r\nELSE\r\n    INSERT INTO misc (misccode,miscstring) VALUES (@code,@value)";
	}
}
