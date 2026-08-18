using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Settings
{
	// Token: 0x0200002E RID: 46
	public interface IReferenceTableSettingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000C1 RID: 193
		IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql);

		// Token: 0x060000C2 RID: 194
		IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName);
	}
}
