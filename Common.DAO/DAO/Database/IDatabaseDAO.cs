using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Database
{
	// Token: 0x02000091 RID: 145
	public interface IDatabaseDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003BC RID: 956
		bool DoesTableExist(string tableName);

		// Token: 0x060003BD RID: 957
		bool DoesColumnExist(string tableName, string colName);

		// Token: 0x060003BE RID: 958
		void ExecuteCommands(IList<string> commands, bool useTransactions = true);
	}
}
