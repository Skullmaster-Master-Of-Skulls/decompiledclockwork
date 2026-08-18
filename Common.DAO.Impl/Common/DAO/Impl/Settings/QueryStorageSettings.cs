using System;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x02000047 RID: 71
	internal class QueryStorageSettings
	{
		// Token: 0x040000CA RID: 202
		internal static readonly string QS_SINGLE_SETTING = "SELECT settingcode, settingstringvalue, usercomment \r\n                                                                 FROM websettings2 \r\n                                                                 WHERE instancename=@instance and settingcode=@settingcode";

		// Token: 0x040000CB RID: 203
		internal static readonly string QS_SETTINGS_BY_GROUP = "SELECT settingcode, settingstringvalue, usercomment \r\n                                                                 FROM websettings2 \r\n                                                                 WHERE instancename=@instance and settingcode>=@start and settingcode<@end";

		// Token: 0x040000CC RID: 204
		internal const string QS_INSTANCE_NAMES = "SELECT DISTINCT instancename FROM websettings2";

		// Token: 0x040000CD RID: 205
		internal static readonly string QS_ALL_VALUES = "SELECT DISTINCT {0}, {1}\r\n                                                          FROM {2}";

		// Token: 0x040000CE RID: 206
		internal static readonly string IUS_INSERT_OR_UPDATE_SETTING = "IF EXISTS(SELECT * FROM websettings2 WHERE instancename=@instance and settingcode=@settingcode) \r\n                                                                         BEGIN \r\n                                                                            UPDATE websettings2 SET settingstringvalue = @settingvalue WHERE instancename=@instance and settingcode=@settingcode\r\n                                                                         END\r\n                                                                         ELSE\r\n                                                                         BEGIN\r\n                                                                            INSERT INTO websettings2 VALUES(@instance, @settingcode, @settingvalue, null)\r\n                                                                         END";
	}
}
