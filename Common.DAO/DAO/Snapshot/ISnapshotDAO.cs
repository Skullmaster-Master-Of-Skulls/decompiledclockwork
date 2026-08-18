using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Snapshot;

namespace TechnoPro.Common.DAO.Snapshot
{
	// Token: 0x0200002C RID: 44
	public interface ISnapshotDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000BE RID: 190
		IList<string> GenerateSqlQueries(string DestinationClockWorkDatabasePassword, params eSnapshotArea[] areas);
	}
}
