using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AF RID: 175
	public static class QueryStorageLegacyWebSettings
	{
		// Token: 0x04000247 RID: 583
		internal const string QS_GET_SETTING_VAL = "SELECT settingstringvalue FROM websettings2 WHERE instancename=@iname AND settingcode=@code";
	}
}
