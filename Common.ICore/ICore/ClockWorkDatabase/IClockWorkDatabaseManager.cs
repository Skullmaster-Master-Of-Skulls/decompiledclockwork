using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.ClockWorkDatabase
{
	// Token: 0x020000B5 RID: 181
	public interface IClockWorkDatabaseManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600056A RID: 1386
		bool DoesTableExist(string TableName);

		// Token: 0x0600056B RID: 1387
		string[] LoadAllTableNames();
	}
}
