using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Settings
{
	// Token: 0x02000018 RID: 24
	public interface IReferenceTableSettingClientManager : IWebService
	{
		// Token: 0x0600008E RID: 142
		IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql);

		// Token: 0x0600008F RID: 143
		IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted);

		// Token: 0x06000090 RID: 144
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName);

		// Token: 0x06000091 RID: 145
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted);

		// Token: 0x06000092 RID: 146
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql);
	}
}
