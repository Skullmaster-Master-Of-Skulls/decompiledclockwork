using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.ClockWorkDatabase
{
	// Token: 0x0200009A RID: 154
	public interface IClockWorkDatabaseDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003FF RID: 1023
		bool DoesTableExist(string TableName);

		// Token: 0x06000400 RID: 1024
		string[] LoadAllTableNames();
	}
}
