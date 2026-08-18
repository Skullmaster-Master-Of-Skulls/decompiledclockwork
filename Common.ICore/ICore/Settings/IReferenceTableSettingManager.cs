using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x02000039 RID: 57
	public interface IReferenceTableSettingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600016F RID: 367
		IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql);

		// Token: 0x06000170 RID: 368
		IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted);

		// Token: 0x06000171 RID: 369
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName);

		// Token: 0x06000172 RID: 370
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted);

		// Token: 0x06000173 RID: 371
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql);
	}
}
